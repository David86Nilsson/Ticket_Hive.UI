﻿using Ticket_Hive.Data.Models;

namespace Ticket_Hive.Data.Repos
{
    public class BookingRepo : IBookingRepo
    {
        public Task<bool> AddBookingAsync(BookingModel newBooking)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBookingAsync(BookingModel bookingToDelete)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookingModel>> GetAllBookingsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BookingModel?> GetBookingByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBookingAsync(BookingModel updatedBooking)
        {
            throw new NotImplementedException();
        }
    }
}
