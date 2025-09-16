namespace Models.Ticket.Model
{
  public class VehicleByTicket
  {
    public int VehicleId { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int VehicleTypeId { get; set; }
    public int VehicleTicketTypeId { get; set; }

    public int AssignmentId { get; set; }
    public DateTime CheckInTime { get; set; }

    public int ParkingLotId { get; set; }
    public int LotTicketTypeId { get; set; }
  }
}
