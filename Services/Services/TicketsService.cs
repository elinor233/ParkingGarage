
using Models;
using Models.Ticket;
using Models.Ticket.Model;
using Models.Vehicles.Model;

namespace Services.Services;

public class TicketsService
{

  private static readonly Dictionary<TicketTypeEnum, Ticket> _map = new() // הגדרה של כרטיסים לפי פירוט
  {
    [TicketTypeEnum.VIP] = new Ticket
    {
      //TicketType = TicketTypeEnum.VIP,
      MinLotNumber = 1,
      MaxLotNumber = 10,
      MaxVehicleHeight = null,
      MaxVehicleWidth = null,
      MaxVehicleLength = null,
      AllowedVehicleTypes = new HashSet<VehicleTypeEnum>
      {
          VehicleTypeEnum.Motorcycle, VehicleTypeEnum.Private, VehicleTypeEnum.Crossover,
          VehicleTypeEnum.SUV, VehicleTypeEnum.Van, VehicleTypeEnum.Truck
      },
      Cost = 200,
      MaxParkingDuration = null
    },

    [TicketTypeEnum.Value] = new Ticket
    {
      //TicketType = TicketTypeEnum.Value,
      MinLotNumber = 11,
      MaxLotNumber = 30,
      MaxVehicleHeight = 2500,
      MaxVehicleWidth = 2400,
      MaxVehicleLength = 5000,
      AllowedVehicleTypes = new HashSet<VehicleTypeEnum>
      {
          VehicleTypeEnum.Motorcycle, VehicleTypeEnum.Private, VehicleTypeEnum.Crossover,
          VehicleTypeEnum.SUV, VehicleTypeEnum.Van
      },
      Cost = 100,
      MaxParkingDuration = TimeSpan.FromHours(72)
    },

    [TicketTypeEnum.Regular] = new Ticket
    {
      //TicketType = TicketTypeEnum.Regular,
      MinLotNumber = 31,
      MaxLotNumber = 60,
      MaxVehicleHeight = 2000,
      MaxVehicleWidth = 2000,
      MaxVehicleLength = 3000,
      AllowedVehicleTypes = new HashSet<VehicleTypeEnum>
      {
          VehicleTypeEnum.Motorcycle, VehicleTypeEnum.Private, VehicleTypeEnum.Crossover
      },
      Cost = 50,
      MaxParkingDuration = TimeSpan.FromHours(24)
    },
  };

  public Ticket Get(TicketTypeEnum ticketType) => _map[ticketType];

  public (int minLotNumber, int maxLotNumber) GetAllowedLotNumberRange(TicketTypeEnum ticketType)
  {
    var t = _map[ticketType];
    return (t.MinLotNumber, t.MaxLotNumber);
  }

  public decimal GetCost(TicketTypeEnum ticketType) => _map[ticketType].Cost;

  public TimeSpan? GetMaxParkingDuration(TicketTypeEnum ticketType) => _map[ticketType].MaxParkingDuration;

  public UpgradeSuggestionTicket? SuggestUpgrade(TicketTypeEnum ticketType, VehicleTypeEnum vehicleType, int height, int width, int length)
  {
    if (IsVehicleAllowed(ticketType, vehicleType, height, width, length))
      return null;

    var recommendedTicketType = FindCheapestFittingTicket(vehicleType, height, width, length);
    if (recommendedTicketType is null) return null;

    var currentCost = _map[ticketType].Cost;
    var newCost = _map[recommendedTicketType.Value].Cost;

    return new UpgradeSuggestionTicket(recommendedTicketType.Value, currentCost, newCost, newCost - currentCost);
  }

  /*  public TicketTypeEnum? FindCheapestFittingTicket(
      VehicleTypeEnum vehicleType, int height, int width, int length)
      => _map.Values.OrderBy(t => t.Cost)
             .FirstOrDefault(t => IsVehicleAllowed(t, vehicleType, height, width, length))
             ?.TicketType;*/

  public TicketTypeEnum? FindCheapestFittingTicket(
    VehicleTypeEnum vehicleType, int height, int width, int length)
  {
    return _map
      .OrderBy(kv => kv.Value.Cost) // kv.Key = TicketTypeEnum, kv.Value = Ticket
      .Where(kv => IsVehicleAllowed(kv.Value, vehicleType, height, width, length))
      .Select(kv => (TicketTypeEnum?)kv.Key)
      .FirstOrDefault();
  }

  public bool IsVehicleAllowed(TicketTypeEnum ticketType, VehicleTypeEnum vehicleType, int height, int width, int length)
  => IsVehicleAllowed(_map[ticketType], vehicleType, height, width, length);
  public bool IsVehicleAllowed(Ticket t, VehicleTypeEnum vehicleType, int height, int width, int length)
  {
    if (!t.AllowedVehicleTypes.Contains(vehicleType)) return false;
    if (t.MaxVehicleHeight.HasValue && height > t.MaxVehicleHeight.Value) return false;
    if (t.MaxVehicleWidth.HasValue && width > t.MaxVehicleWidth.Value) return false;
    if (t.MaxVehicleLength.HasValue && length > t.MaxVehicleLength.Value) return false;
    return true;
  }
}
