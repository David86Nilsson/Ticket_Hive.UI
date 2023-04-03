using Microsoft.EntityFrameworkCore;
using Ticket_Hive.Data.Models;

namespace Ticket_Hive.Data
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options)
            : base(options)
        {
        }

        public DbSet<EventModel> Events { get; set; }
        public DbSet<AppUserModel> AppUsers { get; set; }
        public DbSet<BookingModel> Bookings { get; set; }

    }

}
