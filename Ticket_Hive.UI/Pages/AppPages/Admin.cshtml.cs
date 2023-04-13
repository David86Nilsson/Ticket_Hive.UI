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
        public int Capacity { get; set; }
        [Required(ErrorMessage = "Please enter a location")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Please enter an event type")]
        public string EventType { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }

        public List<EventModel> Events;

        public int EventToDelete { get; set; }

        public AdminModel(IEventModelRepo eventModelRepo)
        {
            this.eventModelRepo = eventModelRepo;
        }
        public async Task OnGet()
        {
            Year = DateTime.Now.Year;
            Month = DateTime.Now.Month;
            Day = DateTime.Now.Day;
            Hour = DateTime.Now.Hour;
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
                    DateTime = new DateTime(Year, Month, Day, Hour, Minute, 0),
                    Price = Price,
                    Image = $"/Images/EventImages/image {new Random().Next(1, 10)}.png"
                };
                await eventModelRepo.AddEventAsync(newEvent);
            }
            Year = DateTime.Now.Year;
            Month = DateTime.Now.Month;
            Day = DateTime.Now.Day;
            Hour = DateTime.Now.Hour;

            Events = await eventModelRepo.GetAllEventsAsync();

            return Page();
        }
        public async Task<IActionResult> OnPostDelete()
        {
            await eventModelRepo.DeleteEventAsync(await eventModelRepo.GetEventByIdAsync(EventToDelete));

            Year = DateTime.Now.Year;
            Month = DateTime.Now.Month;
            Day = DateTime.Now.Day;
            Hour = DateTime.Now.Hour;

            Events = await eventModelRepo.GetAllEventsAsync();

            return Page();
        }
    }
}
