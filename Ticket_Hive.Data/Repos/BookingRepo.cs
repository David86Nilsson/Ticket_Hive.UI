using Microsoft.EntityFrameworkCore;
using Ticket_Hive.Data.Models;

namespace Ticket_Hive.Data.Repos
{
    public class BookingRepo : IBookingRepo
    {
        private readonly EventDbContext context;

        public BookingRepo(EventDbContext context)
        {
            this.context = context;
        }
        public async Task AddBookingAsync(BookingModel newBooking)
        {
            await context.Bookings.AddAsync(newBooking);
            await context.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(BookingModel bookingToDelete)
        {
            BookingModel? booking = await context.Bookings.FirstOrDefaultAsync(b => b.Id == bookingToDelete.Id);
            if (booking != null)
            {
                context.Bookings.Remove(booking);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BookingModel>?> GetAllBookingsAsync()
        {
            return await context.Bookings.Include(b => b.Event).Include(b => b.User).ToListAsync();
        }

        public async Task<BookingModel?> GetBookingByIdAsync(int id)
        {
            return await context.Bookings.Include(b => b.Event).Include(b => b.User).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<bool> UpdateBookingAsync(BookingModel updatedBooking)
        {
            BookingModel? booking = await context.Bookings.FirstOrDefaultAsync(b => b.Id == updatedBooking.Id);
            if (booking != null)
            {
                booking.BookingDate = updatedBooking.BookingDate;
                booking.Event = updatedBooking.Event;
                booking.User = updatedBooking.User;
                context.Bookings.Update(booking);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
