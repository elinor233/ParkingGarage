namespace Models.Parking.Model;

public class ParkingLots
{
  public int ParkingLotId { get; set; }
  public TicketTypeEnum TicketTypeId { get; set; }
  public bool IsOccupied { get; set; }
  //public ParkingAssignments? ParkingAssignments { get; set; }
  public int VehicleId { get; set; }

}
