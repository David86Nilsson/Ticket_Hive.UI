using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;

namespace Ticket_Hive.Logic
{
    public class EventManager
    {
        /// <summary>
        /// Calculates the total price of all bookings in the shopping cart
        /// </summary>
        public decimal TotalPriceInShoopingCart(ShoppingCartModel cart)
        {
            decimal totalPrice = 0m;
            foreach (BookingModel booking in cart.Bookings)
            {
                totalPrice += (booking.NbrOfTickets * booking.Event.Price);
            }
            return Math.Round(totalPrice, 2);
        }

        /// <summary>
        /// Completes bookings in shoppingcart and saves to database
        /// </summary>
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
        /// <summary>
        /// Completes booking and saves to Database
        /// </summary>
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


        /// <summary>
        /// checks if Event is fully booked
        /// </summary>
        public bool IsEventFullyBooked(EventModel eventModel)
        {
            return eventModel.Capacity <= eventModel.TicketsSold;
        }
        /// <summary>
        /// Returns number of tickets left to buy in Event
        /// </summary>
        public int TicketsLeft(EventModel eventModel)
        {
            return eventModel.Capacity - eventModel.TicketsSold;
        }
    }
}
