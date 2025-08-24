using Ticketing.Domain.Models;

namespace Ticketing.Services.Interfaces;

public interface ITicketService
{
    Task<IEnumerable<Ticket>> GetTicketsAsync();
    Task<Ticket> GetTicketByIdAsync(int id);
    Task<Ticket> CreateTicketAsync(Ticket ticket);
}
