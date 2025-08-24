using Microsoft.EntityFrameworkCore;
using Ticketing.Data.Repositories.Interfaces;
using Ticketing.Domain.Models;

namespace Ticketing.Data.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly TicketDbContext _context;

    public TicketRepository(TicketDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Ticket>> GetTicketsAsync()
    {
        return await _context.Tickets.ToListAsync();
    }

    public async Task<Ticket> GetTicketByIdAsync(int id)
    {
        return await _context.Tickets.FindAsync(id);
    }

    public async Task<Ticket> AddTicketAsync(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }
}
