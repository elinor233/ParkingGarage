using DB.Models;
using DB.Models.Parking.Model;
using DB.Models.Vehicles.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.Parking.Model;
using Models.Vehicles.Model;
using Moq;
using Services.Services;

namespace UnitTest;

public class ParkingServiceTests
{
  private readonly ParkingService _parkingService;
  private readonly TicketsService _ticketsService;

  private GarageContext _context;

  public ParkingServiceTests()
  {
    var options = new DbContextOptionsBuilder<GarageContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

    _context = new GarageContext(options);
    var loggerMock = new Mock<ILogger<ParkingService>>();

    _ticketsService = new TicketsService();
    _parkingService = new ParkingService(_context, loggerMock.Object, _ticketsService);
  }

  private static async Task createParkingLotsAsync(GarageContext db)
  {
    if (await db.TbParkingLots.AnyAsync()) return;

    // 1–10 VIP, 11–30 Value, 31–60 Regular
    var lots = Enumerable.Range(1, 60).Select(i => new TbParkingLots
    {
      ParkingLotId = i,
      TicketTypeId = i <= 10 ? (int)TicketTypeEnum.VIP
                 : i <= 30 ? (int)TicketTypeEnum.Value
                           : (int)TicketTypeEnum.Regular,
      IsOccupied = false
    });

    await db.TbParkingLots.AddRangeAsync(lots);
    await db.SaveChangesAsync();
  }

  [Fact]
  public async Task CheckIn_NewVehicle()
  {
    await createParkingLotsAsync(_context);

    var req = new CheckInRequest
    {
      Name = "Test",
      LicensePlate = "11-111-11",
      Phone = "0500000000",
      VehicleTypeId = VehicleTypeEnum.Private,
      Height = 1600,
      Width = 1800,
      Length = 4300,
      TicketTypeId = TicketTypeEnum.Value 
    };

    var res = await _parkingService.CheckInAsync(req);

    Assert.Equal(ResponseEnum.Success, res.Response);
    Assert.NotNull(res.Data);
    Assert.InRange(res.Data!.AssignedLotNumber!.Value, 11, 30);

    var lot = await _context.TbParkingLots.FindAsync(res.Data.AssignedLotNumber);
    Assert.True(lot!.IsOccupied);

    var vehicle = await _context.TbVehicles.FirstOrDefaultAsync(v => v.LicensePlate == req.LicensePlate);
    Assert.NotNull(vehicle);

    var assignment = await _context.TbParkingAssignments
      .FirstOrDefaultAsync(a => a.VehicleId == vehicle!.VehicleId && a.CheckOutTime == null);
    Assert.NotNull(assignment);
  }

  [Fact]
  public async Task CheckIn_RequiresUpgrade()
  {
    // Arrange
    await createParkingLotsAsync(_context); 

    var req = new CheckInRequest
    {
      Name = "Truck Driver",
      LicensePlate = "77-777-77",
      Phone = "0507777777",
      VehicleTypeId = VehicleTypeEnum.Truck,   
      Height = 2300,
      Width = 2300,
      Length = 4800,
      TicketTypeId = TicketTypeEnum.Regular 
    };

    var res = await _parkingService.CheckInAsync(req);

    // Assert
    Assert.Equal(ResponseEnum.RequiresUpgrade, res.Response);
    Assert.NotNull(res.Data);
    Assert.NotNull(res.Data!.Upgrade);

    Assert.Equal(TicketTypeEnum.VIP, res.Data.Upgrade!.SuggestedTicket);

    Assert.Null(res.Data.AssignedLotNumber);

    var veh = await _context.TbVehicles.FirstOrDefaultAsync(v => v.LicensePlate == req.LicensePlate);
    var hasActiveAssign = veh != null && await _context.TbParkingAssignments
        .AnyAsync(a => a.VehicleId == veh.VehicleId && a.CheckOutTime == null);

    Assert.False(hasActiveAssign);
  }

  [Fact]
  public async Task CheckOut()
  {
    await createParkingLotsAsync(_context);

    // ניצור רכב + הקצאה פעילה
    var veh = new TbVehicles
    {
      Name = "Lea",
      LicensePlate = "55-555-55",
      Phone = "0504",
      VehicleTypeId = (int)VehicleTypeEnum.Private,
      Height = 1600,
      Width = 1800,
      Length = 4300,
      TicketTypeId = (int)TicketTypeEnum.Value,
      CheckInTime = DateTime.UtcNow
    };
    _context.TbVehicles.Add(veh);
    await _context.SaveChangesAsync();

    var lot = await _context.TbParkingLots.FindAsync(20); // Value
    lot!.IsOccupied = true;

    _context.TbParkingAssignments.Add(new TbParkingAssignments
    {
      VehicleId = veh.VehicleId,
      ParkingLotId = lot.ParkingLotId,
      CheckInTime = DateTime.UtcNow,
      CheckOutTime = null
    });

    await _context.SaveChangesAsync();

    var res = await _parkingService.CheckOutAsync(veh.LicensePlate);

    Assert.Equal(ResponseEnum.Success, res.Response);

    var updatedLot = await _context.TbParkingLots.FindAsync(lot.ParkingLotId);
    Assert.False(updatedLot!.IsOccupied);
  }

  [Fact]
  public async Task GetParkingLotState_FreeLot()
  {
    await createParkingLotsAsync(_context);

    var parkinglotId = 12; 
    var res = await _parkingService.GetParkingLotStateAsync(parkinglotId);

    Assert.Equal(ResponseEnum.Success, res.Response);
    Assert.NotNull(res.Data);
    Assert.Equal(parkinglotId, res.Data!.ParkingLotId);
    Assert.False(res.Data.IsOccupied);
    Assert.Equal(0, res.Data.VehicleId); // 0- לא קיים רכב בחנייה
  }

  [Fact]
  public async Task GetParkingLotState_OccupiedLot()
  {
    await createParkingLotsAsync(_context);

    var lotId = 12; // Value
    var veh = new TbVehicles
    {
      Name = "StateVeh",
      LicensePlate = "77-777-77",
      Phone = "0507",
      VehicleTypeId = (int)VehicleTypeEnum.Private,
      Height = 1600,
      Width = 1800,
      Length = 4300,
      TicketTypeId = (int)TicketTypeEnum.Value,
      CheckInTime = DateTime.UtcNow
    };
    _context.TbVehicles.Add(veh);
    await _context.SaveChangesAsync();

    var lot = await _context.TbParkingLots.FindAsync(lotId);
    lot!.IsOccupied = true;

    _context.TbParkingAssignments.Add(new TbParkingAssignments
    {
      VehicleId = veh.VehicleId,
      ParkingLotId = lotId,
      CheckInTime = DateTime.UtcNow,
      CheckOutTime = null
    });
    await _context.SaveChangesAsync();

    var res = await _parkingService.GetParkingLotStateAsync(lotId);

    Assert.Equal(ResponseEnum.Success, res.Response);
    Assert.NotNull(res.Data);
    Assert.True(res.Data!.IsOccupied);
    Assert.Equal(veh.VehicleId, res.Data.VehicleId);
  }

}


