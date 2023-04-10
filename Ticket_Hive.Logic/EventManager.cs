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
        public async Task BuyTicketsAsync(ShoppingCartModel? shoppingCart, IBookingRepo bookingRepo, IEventModelRepo eventModelRepo, IAppUserModelRepo appUserModelRepo)
        {
            List<BookingModel> bookings = shoppingCart.Bookings;
            foreach (BookingModel booking in bookings)
            {
                EventModel eventModel = booking.Event;
                if (TicketsLeft(eventModel) < booking.NbrOfTickets)
                {
                    booking.NbrOfTickets = 0;
                }
                else
                {
                    await CompleteBookingAsync(booking, bookingRepo, eventModelRepo, appUserModelRepo);
                }

            }
        }
        public async Task CompleteBookingAsync(BookingModel bookingModel, IBookingRepo bookingRepo, IEventModelRepo eventModelRepo, IAppUserModelRepo appUserModelRepo)
        {
            EventModel? eventToBook = await eventModelRepo.GetEventByIdAsync(bookingModel.EventId);
            AppUserModel appUser = await appUserModelRepo.GetUserByUserNameAsync(bookingModel.User.Username);
            eventToBook.TicketsSold += bookingModel.NbrOfTickets;
            eventToBook.Users.Add(appUser);
            bookingRepo.AddBookingAsync(bookingModel);

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
