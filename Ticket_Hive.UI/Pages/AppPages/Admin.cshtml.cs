using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;

namespace Ticket_Hive.UI.Pages.AppPages
{
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

        public AdminModel(IEventModelRepo eventModelRepo)
        {
            this.eventModelRepo = eventModelRepo;
        }
        public async Task OnGet()
        {
            Events = await eventModelRepo.GetAllEventsAsync();
        }
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
        public async Task<IActionResult> OnPostDelete()
        {
            await eventModelRepo.DeleteEventAsync(await eventModelRepo.GetEventByIdAsync(EventToDelete));


            Events = await eventModelRepo.GetAllEventsAsync();

            return RedirectToPage();
        }
    }
}
