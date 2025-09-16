using Models.Vehicles.Model;

namespace Models.Ticket;

public class Ticket
{
  //public TicketTypeEnum TicketType { get; init; }
  public int MinLotNumber { get; init; }
  public int MaxLotNumber { get; init; }
  public int? MaxVehicleHeight { get; init; }
  public int? MaxVehicleWidth { get; init; }
  public int? MaxVehicleLength { get; init; }
  public decimal Cost { get; init; }
  public TimeSpan? MaxParkingDuration { get; init; }
  public HashSet<VehicleTypeEnum> AllowedVehicleTypes { get; init; } = new();

}
