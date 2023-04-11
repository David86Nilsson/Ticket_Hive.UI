using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;

namespace Ticket_Hive.Logic
{
    public class EventManager
    {
        public decimal TotalPriceInShoopingCart(ShoppingCartModel cart)
        {
            decimal totalPrice = 0m;
            foreach (BookingModel booking in cart.Bookings)
            {
                totalPrice += (booking.NbrOfTickets * booking.Event.Price);
            }
            return Math.Round(totalPrice, 2);
        }
        public async Task BuyTicketsAsync(ShoppingCartModel? shoppingCart, IBookingRepo bookingRepo, IEventModelRepo eventModelRepo, IAppUserModelRepo appUserModelRepo, string buyerName)
        {
            List<BookingModel> bookings = shoppingCart.Bookings;
            foreach (BookingModel booking in bookings)
            {
                int eventId = booking.EventId;
                EventModel eventModel = booking.Event;
                if (TicketsLeft(eventModel) < booking.NbrOfTickets)
                {
                    booking.NbrOfTickets = 0;
                }
                else
                {
                    await CompleteBookingAsync(booking, bookingRepo, eventModelRepo, appUserModelRepo, buyerName);
                }

            }
        }
        public async Task CompleteBookingAsync(BookingModel bookingModel, IBookingRepo bookingRepo, IEventModelRepo eventModelRepo, IAppUserModelRepo appUserModelRepo, string buyerName)
        {
            EventModel? eventToBook = await eventModelRepo.GetEventByIdAsync(bookingModel.Event.Id);
            AppUserModel appUser = await appUserModelRepo.GetUserByUserNameAsync(buyerName);
            bookingModel.User = appUser;
            bookingModel.Event = eventToBook;
            bookingModel.Event.TicketsSold += bookingModel.NbrOfTickets;
            bookingModel.BookingDate = DateTime.Now;

            //await eventModelRepo.UpdateEventAsync(eventToBook);
            await bookingRepo.AddBookingAsync(bookingModel);
        }



        public bool IsEventFullyBooked(EventModel eventModel)
        {
            return eventModel.Capacity <= eventModel.TicketsSold;
        }
        public int TicketsLeft(EventModel eventModel)
        {
            return eventModel.Capacity - eventModel.TicketsSold;
        }
    }
}
