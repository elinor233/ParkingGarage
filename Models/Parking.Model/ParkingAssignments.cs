namespace Models.Parking.Model;

public class ParkingAssignments
{
  public int AssignmentId { get; set; }
  public int VehicleId { get; set; }
  public int ParkingLotId { get; set; }
  public DateTime CheckInTime { get; set; }
  public DateTime? CheckOutTime { get; set; }

}
