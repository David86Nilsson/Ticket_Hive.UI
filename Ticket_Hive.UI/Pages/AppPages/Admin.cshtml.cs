using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;

namespace Ticket_Hive.UI.Pages.AppPages
{
    /// <summary>
    /// Page Model for the Admin page, which allows an administrator to add and delete events
    /// </summary>
    [BindProperties]
    public class AdminModel : PageModel
    {
        private readonly IEventModelRepo eventModelRepo;

        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a capacity")]
        public int Capacity { get; set; }
        [Required(ErrorMessage = "Please enter a location")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Please enter an event type")]
        public string EventType { get; set; }
        [Required(ErrorMessage = "Please Choose a date")]
        public DateTime DateTime { get; set; }
        public TimeSpan EventTime { get; set; }
        [Required(ErrorMessage = "Please choose a ticketprice")]
        public decimal Price { get; set; }

        public List<EventModel> Events;

        public int EventToDelete { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminModel"/> class.
        /// </summary>
        /// <param name="eventModelRepo">The event model repository.</param>
        public AdminModel(IEventModelRepo eventModelRepo)
        {
            this.eventModelRepo = eventModelRepo;
        }
        /// <summary>
        /// Handles GET requests for the Admin page, populating the list of events and setting default values for the capacity and price properties.
        /// </summary>
        public async Task OnGet()
        {
            Events = await eventModelRepo.GetAllEventsAsync();
            Capacity = 1;
            Price = 1;

        }
        /// <summary>
        /// Handles POST requests for creating an event, adding a new event to the database if the provided data is valid.
        /// </summary>
        /// <returns>The current page.</returns>
        public async Task<IActionResult> OnPostAdd()
        {
            if (ModelState.IsValid)
            {
                var newEvent = new EventModel
                {
                    Name = Name,
                    Capacity = Capacity,
                    Location = Location,
                    EventType = EventType,
                    DateTime = DateTime.Add(EventTime),
                    Price = Price,
                    Image = $"/Images/EventImages/image {new Random().Next(1, 10)}.png"
                };
                await eventModelRepo.AddEventAsync(newEvent);
            }

            Events = await eventModelRepo.GetAllEventsAsync();

            return Page();
        }
        /// <summary>
        /// Handles POST requests for deleting an event, deleting a event of the database if the provided data is valid.
        /// </summary>
        /// <returns>Redirect page.</returns>
        public async Task<IActionResult> OnPostDelete()
        {
            await eventModelRepo.DeleteEventAsync(await eventModelRepo.GetEventByIdAsync(EventToDelete));


            Events = await eventModelRepo.GetAllEventsAsync();

            return RedirectToPage();
        }
    }
}
