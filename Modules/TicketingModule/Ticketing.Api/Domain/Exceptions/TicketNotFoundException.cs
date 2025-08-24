namespace Ticketing.Domain.Exceptions;

public class TicketNotFoundException : Exception
{
    public TicketNotFoundException(int ticketId)
        : base($"Ticket with Id {ticketId} not found.")
    {
    }
}
