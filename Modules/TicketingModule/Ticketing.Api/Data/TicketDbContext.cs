using Microsoft.EntityFrameworkCore;
using Ticketing.Domain.Models;

namespace Ticketing.Data;

public class TicketDbContext(DbContextOptions<TicketDbContext> options) : DbContext(options)
{
    public required DbSet<Ticket> Tickets { get; init; }
}
