using DB.Models;
using DB.Models.Aggregators;
using DB.Models.Parking.Model;
using DB.Models.Vehicles.Model;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Models;
using Models.Parking.Model;
using Models.Ticket.Model;
using Models.Vehicles.Model;

namespace Services.Services;

public class ParkingService
{
  private GarageContext dbContext;
  private readonly ILogger<ParkingService> _logger;
  private readonly TicketsService _tickets;


  public ParkingService(GarageContext dbContext, ILogger<ParkingService> logger, TicketsService tickets)
  {
    this.dbContext = dbContext;
    _logger = logger;
    _tickets = tickets;
  }

  public async Task<CheckInResultResponse> CheckInAsync(CheckInRequest checkInRequest)
  {
    try
    {
      IDbContextTransaction? transaction = null;

      var dbVehicleId = await dbContext.TbVehicles.Where(v => v.LicensePlate == checkInRequest.LicensePlate).FirstOrDefaultAsync(); // חיפוש הרכב לפי מספר רישוי

      if (dbVehicleId != null)
      {
        var alreadyActive = await dbContext.TbParkingAssignments
          .Where(pa => pa.VehicleId == dbVehicleId.VehicleId && pa.CheckOutTime == null).AnyAsync();

        if (alreadyActive)
          return new CheckInResultResponse() { Response = ResponseEnum.AlreadyExists };
      }

      if (!_tickets.IsVehicleAllowed(checkInRequest.TicketTypeId, checkInRequest.VehicleTypeId, checkInRequest.Height, checkInRequest.Width, checkInRequest.Length))
      {
        var upgrade = _tickets.SuggestUpgrade(checkInRequest.TicketTypeId, checkInRequest.VehicleTypeId, checkInRequest.Height, checkInRequest.Width, checkInRequest.Length);
        if (upgrade != null)
        {
          var checkIn = new CheckInResult()
          {
            Upgrade = upgrade
          };
          return new CheckInResultResponse { Response = ResponseEnum.RequiresUpgrade, Data = checkIn };
        }
        return new CheckInResultResponse { Response = ResponseEnum.NotEligible };

      }

      //using var transaction = dbContext.Database.BeginTransaction();
      if (dbContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
      {
        transaction = dbContext.Database.BeginTransaction();
      }

      var (minLot, maxLot) = _tickets.GetAllowedLotNumberRange(checkInRequest.TicketTypeId); // האם יש חנייה פנויה

      var parkingLot = await dbContext.TbParkingLots
        .Where(pl => !pl.IsOccupied
                 && pl.TicketTypeId == (int)checkInRequest.TicketTypeId
                 && pl.ParkingLotId >= minLot && pl.ParkingLotId <= maxLot)
        .OrderBy(l => l.ParkingLotId).FirstOrDefaultAsync();

      if (parkingLot == null)
        return new CheckInResultResponse { Response = ResponseEnum.NoDataFound };

      var vehicle = new TbVehicles
      {
        Name = checkInRequest.Name,
        LicensePlate = checkInRequest.LicensePlate,
        Phone = checkInRequest.Phone,
        VehicleTypeId = (int)checkInRequest.VehicleTypeId,
        Height = checkInRequest.Height,
        Width = checkInRequest.Width,
        Length = checkInRequest.Length,
        TicketTypeId = (int)checkInRequest.TicketTypeId,
        CheckInTime = DateTime.UtcNow
      };
      int vehicleId;
      if (dbVehicleId == null) // רכב חדש
      {
        dbContext.TbVehicles.Add(vehicle);
        await dbContext.SaveChangesAsync();
        vehicleId = vehicle.VehicleId;
      }
      else
        vehicleId = dbVehicleId.VehicleId;

      parkingLot.IsOccupied = true;

      var parkingA = new TbParkingAssignments()
      {
        VehicleId = vehicleId,
        ParkingLotId = parkingLot.ParkingLotId,
        CheckInTime = DateTime.UtcNow
      };
      dbContext.TbParkingAssignments.Add(parkingA);

      await dbContext.SaveChangesAsync();
      //await transaction.CommitAsync();
      if (transaction != null)
        await transaction.CommitAsync();
      var result = new CheckInResult() { AssignedLotNumber = parkingLot.ParkingLotId, };
      return new CheckInResultResponse { Response = ResponseEnum.Success, Data = result };
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "CheckIn failed");
      return new CheckInResultResponse { Response = ResponseEnum.BadRequest, Error = ex.Message };
    }
  }

  public async Task<CheckOutResultResponse> CheckOutAsync(string licensePlate)
  {
    try
    {
      var dbVehicleId = await dbContext.TbVehicles.Where(v => v.LicensePlate == licensePlate).FirstOrDefaultAsync();
      if (dbVehicleId == null)
        return new CheckOutResultResponse { Response = ResponseEnum.NoDataFound };

      var active = await dbContext.TbParkingAssignments
        .AsNoTracking()
        .FirstOrDefaultAsync(pa =>
            pa.VehicleId == dbVehicleId.VehicleId &&
            pa.CheckOutTime == null);

      if (active == null)
        return new CheckOutResultResponse { Response = ResponseEnum.NoDataFound };

      active.CheckOutTime = DateTime.UtcNow;
      //active.Lot.IsOccupied = false;

      var parkingLot = await dbContext.TbParkingLots
        .Where(pl => pl.ParkingLotId == active.ParkingLotId).FirstOrDefaultAsync();
      if (parkingLot != null)
        parkingLot.IsOccupied = false;

      await dbContext.SaveChangesAsync();
      return new CheckOutResultResponse { Response = ResponseEnum.Success, Data = active.ParkingLotId };
    }
    catch (Exception ex)
    {
      return new CheckOutResultResponse { Response = ResponseEnum.BadRequest, Error = ex.Message };
    }
  }

  public Task<ParkingLotStateResponse> GetParkingLotStateAsync(int parkingLotId)
  {
    return GetParkingLotState(parkingLotId);
  }

  public async Task<ParkingLotStateResponse> GetParkingLotState(int parkingLotId)
  {
    try
    {
      var dbParkingLot = await dbContext.TbParkingLots.FirstOrDefaultAsync(v => v.ParkingLotId == parkingLotId);

      if (dbParkingLot != null)
      {
        var parking = Mapper.Map<TbParkingLots, ParkingLots>(dbParkingLot);
        if (parking.IsOccupied) // true
        {
          var assignment = await dbContext.TbParkingAssignments
            .Where(a => a.ParkingLotId == parkingLotId && a.CheckOutTime == null).Select(a => a.VehicleId).FirstOrDefaultAsync();
          parking.VehicleId = assignment;

        }
        return new ParkingLotStateResponse { Response = ResponseEnum.Success, Data = parking };
      }
      return new ParkingLotStateResponse { Response = ResponseEnum.NoDataFound };
    }
    catch (Exception ex)
    {
      return new ParkingLotStateResponse { Response = ResponseEnum.BadRequest, Error = ex.Message };
    }
  }

  public async Task<GarageStateResponse> GetGarageStateAsync()
  {
    try
    {
      var dbParkingLots = await dbContext.TbParkingLots.ToListAsync();// אם נרצה רק את החניות התפוסות נוסיף התנייה בשאילתה

      var parkingList = Mapper.Map<List<TbParkingLots>, List<ParkingLots>>(dbParkingLots);

      if (dbParkingLots != null)
      {
        foreach (var parkingLot in parkingList)
        {
          if (parkingLot.IsOccupied) // true
          {
            var assignment = await dbContext.TbParkingAssignments
              .Where(a => a.ParkingLotId == parkingLot.ParkingLotId && a.CheckOutTime == null).Select(a => a.VehicleId).FirstOrDefaultAsync();
            parkingLot.VehicleId = assignment;

          }
        }
        return new GarageStateResponse { Response = ResponseEnum.Success, Data = parkingList };
      }
      return new GarageStateResponse { Response = ResponseEnum.NoDataFound };
    }
    catch (Exception ex)
    {
      return new GarageStateResponse { Response = ResponseEnum.BadRequest, Error = ex.Message };
    }
  }

  public List<VehicleByTicketAggregator> VehicleByTicket(int? TicketTypeId)
  => dbContext.GetVehicleByTicketAggregators(TicketTypeId);

  public VehicleByTicketListResponse GetVehicleByTicket(int? TicketTypeId)
  {
    try
    {
      var vehicleByTicketList = VehicleByTicket(TicketTypeId)
        .Select(t => new VehicleByTicket
        {
          AssignmentId = t.AssignmentId,
          VehicleId = t.VehicleId,
          CheckInTime = t.CheckInTime,
          LicensePlate = t.LicensePlate,
          LotTicketTypeId = t.LotTicketTypeId,
          Name = t.Name,
          ParkingLotId = t.ParkingLotId,
          Phone = t.Phone,
          VehicleTicketTypeId = (int)t.VehicleTicketTypeId,
          VehicleTypeId = t.VehicleTypeId
        }).ToList();

      if (vehicleByTicketList.Any())
      {
        return new VehicleByTicketListResponse { Response = ResponseEnum.Success, Data = vehicleByTicketList };
      }
      return new VehicleByTicketListResponse { Response = ResponseEnum.NoDataFound };
    }
    catch (Exception ex)
    {
      return new VehicleByTicketListResponse { Response = ResponseEnum.BadRequest, Error = ex.Message };
    }
  }

  public async Task<List<CheckInResultResponse>> CheckInRandomVehiclesAsync(int count = 5)
  {
    var results = new List<CheckInResultResponse>();

    for (int i = 0; i < count; i++)
    {
      var random = new Random(Guid.NewGuid().GetHashCode());

      var request = new CheckInRequest
      {
        Name = "AutoGenerated",
        LicensePlate = $"RAND-{random.Next(10000, 99999)}",
        Phone = $"05{random.Next(10000000, 99999999)}",
        TicketTypeId = (TicketTypeEnum)random.Next(1, 4), // 1-3
        VehicleTypeId = (VehicleTypeEnum)random.Next(1, 7), // 1-6
        Height = random.Next(1500, 2600),
        Width = random.Next(1500, 2500),
        Length = random.Next(2500, 5000)
      };

      var result = await CheckInAsync(request);
      results.Add(result);
    }

    return results;
  }

}

