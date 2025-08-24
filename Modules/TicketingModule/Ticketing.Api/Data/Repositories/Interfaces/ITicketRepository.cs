using Ticketing.Domain.Models;

namespace Ticketing.Data.Repositories.Interfaces;

public interface ITicketRepository
{
    Task<IEnumerable<Ticket>> GetTicketsAsync();
    Task<Ticket> GetTicketByIdAsync(int id);
    Task<Ticket> AddTicketAsync(Ticket ticket);
}