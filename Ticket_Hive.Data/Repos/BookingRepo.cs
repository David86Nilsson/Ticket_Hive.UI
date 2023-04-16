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

        /// <summary>
        /// Adds a new Booking to the database 
        /// </summary>
        public async Task AddBookingAsync(BookingModel newBooking)
        {
            await context.Bookings.AddAsync(newBooking);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a Booking from the database asynchronously
        /// </summary>
        public async Task DeleteBookingAsync(BookingModel bookingToDelete)
        {
            BookingModel? booking = await context.Bookings.FirstOrDefaultAsync(b => b.Id == bookingToDelete.Id);
            if (booking != null)
            {
                context.Bookings.Remove(booking);
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retrieves all Bookings from the database 
        /// </summary>
        public async Task<IEnumerable<BookingModel>?> GetAllBookingsAsync()
        {
            return await context.Bookings.Include(b => b.Event).Include(b => b.User).ToListAsync();
        }

        /// <summary>
        /// Retrieves a Booking by its ID from the database 
        /// </summary>
        /// <returns>BookingModel object if found, otherwise null</returns>
        public async Task<BookingModel?> GetBookingByIdAsync(int id)
        {
            return await context.Bookings.Include(b => b.Event).Include(b => b.User).FirstOrDefaultAsync(b => b.Id == id);
        }

        /// <summary>
        /// Updates a Booking in the database
        /// </summary>
        /// <returns>True if the update was successful, otherwise false</returns>
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

        /// <summary>
        /// Retrieves confirmed Bookings by a specific user name 
        /// </summary>
        /// <returns>List of BookingModel objects</returns>
        public async Task<List<BookingModel>> GetConfirmedBookingsByUserNameAsync(string userName)
        {
            return await context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .Where(b => b.User.Username == userName)
                .ToListAsync();
        }

    }
}
