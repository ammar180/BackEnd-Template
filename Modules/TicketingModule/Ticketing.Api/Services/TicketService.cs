using Ticketing.Data.Repositories.Interfaces;
using Ticketing.Domain.Exceptions;
using Ticketing.Domain.Models;
using Ticketing.Services.Interfaces;

namespace Ticketing.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;

    public TicketService(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<IEnumerable<Ticket>> GetTicketsAsync()
    {
        return await _ticketRepository.GetTicketsAsync();
    }

    public async Task<Ticket> GetTicketByIdAsync(int id)
    {
        var ticket = await _ticketRepository.GetTicketByIdAsync(id);

        if (ticket == null)
        {
            throw new TicketNotFoundException(id);
        }
        return ticket;
    }

    public async Task<Ticket> CreateTicketAsync(Ticket ticket)
    {
        return await _ticketRepository.AddTicketAsync(ticket);
    }
}