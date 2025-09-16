using Models.Ticket.Model;

namespace Models.Parking.Model;

public class CheckInResult
{
  public int? AssignedLotNumber { get; set; }
  public UpgradeSuggestionTicket? Upgrade { get; set; }

}
