using Models.Parking.Model;

namespace Models.Vehicles.Model;

public class Vehicles
{
  public int VehicleId { get; set; }
  public string Name { get; set; } = null!;
  public string LicensePlate { get; set; } = null!;
  public string Phone { get; set; } = null!;
  public VehicleTypeEnum VehicleTypeId { get; set; }
  public int Height { get; set; }
  public int Width { get; set; }
  public int Length { get; set; }
  public TicketTypeEnum TicketTypeId { get; set; }
  public DateTime CheckInTime { get; set; }
  public List<ParkingAssignments> ParkingAssignments { get; set; } = [];

}
