namespace Models.Ticket.Model;

public record UpgradeSuggestionTicket
(
  TicketTypeEnum SuggestedTicket, 
    decimal CurrentCost, 
    decimal NewCost,
    decimal Difference
);
