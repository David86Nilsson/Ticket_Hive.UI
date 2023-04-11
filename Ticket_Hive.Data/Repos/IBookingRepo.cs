using Ticket_Hive.Data.Models;

namespace Ticket_Hive.Data.Repos
{
    public interface IBookingRepo
    {
        public Task<IEnumerable<BookingModel>> GetAllBookingsAsync();
        public Task<BookingModel?> GetBookingByIdAsync(int id);
        public Task AddBookingAsync(BookingModel newBooking);
        public Task<bool> UpdateBookingAsync(BookingModel updatedBooking);
        public Task DeleteBookingAsync(BookingModel bookingToDelete);
        public Task SaveAsync();

    }
}
