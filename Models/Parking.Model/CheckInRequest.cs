using Models.Vehicles.Model;

namespace Models.Parking.Model;

public class CheckInRequest
{
  public string Name { get; set; } = null!;
  public string LicensePlate { get; set; } = null!;
  public string Phone { get; set; } = null!;
  public VehicleTypeEnum VehicleTypeId { get; set; }
  public int Height { get; set; }
  public int Width { get; set; }
  public int Length { get; set; }

  public TicketTypeEnum TicketTypeId { get; set; }
}
