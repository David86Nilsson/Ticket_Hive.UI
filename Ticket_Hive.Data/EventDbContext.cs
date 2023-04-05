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
        public DbSet<ShoppingCartModel> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUserModel>().HasData(
                new AppUserModel()
                {
                    Id = 1,
                    Username = "admin"
                },
                new AppUserModel()
                {
                    Id = 2,
                    Username = "user",
                }
                );


            modelBuilder.Entity<EventModel>().HasData(
                new EventModel()
                {
                    Id = 1,
                    Name = "Kareoke bowling",
                    DateTime = DateTime.Now.AddDays(10),
                    EventType = "Nightclub",
                    Location = "Lund",
                    Price = 100m,
                    Capacity = 100,
                    TicketsSold = 0,
                },
                new EventModel()
                {
                    Id = 2,
                    Name = "Mama Mia",
                    DateTime = DateTime.Now.AddDays(5),
                    EventType = "Musical",
                    Location = "Malmö",
                    Price = 200m,
                    Capacity = 300,
                    TicketsSold = 0,
                },
                new EventModel()
                {
                    Id = 3,
                    Name = "AIK - Hammarby",
                    DateTime = DateTime.Now.AddDays(20),
                    EventType = "Sport",
                    Location = "Stockholm",
                    Price = 500m,
                    Capacity = 30000,
                    TicketsSold = 0,
                },
                new EventModel()
                {
                    Id = 4,
                    Name = "Gästföreläsning med Steve Jobs",
                    DateTime = DateTime.Now.AddDays(2),
                    EventType = "Övrigt",
                    Location = "Halmstad",
                    Price = 10m,
                    Capacity = 20,
                    TicketsSold = 20,
                },
                new EventModel()
                {
                    Id = 5,
                    Name = "VM i Rally-Pingis",
                    DateTime = DateTime.Now.AddDays(25),
                    EventType = "Sport",
                    Location = "Köpenhamn",
                    Price = 25m,
                    Capacity = 10,
                    TicketsSold = 0,
                }
             );
        }

    }

}
