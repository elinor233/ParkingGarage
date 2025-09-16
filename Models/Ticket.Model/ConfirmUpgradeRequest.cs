namespace Models.Ticket.Model;

public record ConfirmUpgradeRequest
  (
    string LicensePlate,
    TicketTypeEnum SelectedTicketType
  );
