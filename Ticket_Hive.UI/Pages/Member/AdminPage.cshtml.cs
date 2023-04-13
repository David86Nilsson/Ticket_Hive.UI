using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;

namespace Ticket_Hive.UI.Pages.Member
{
    [BindProperties]
    public class AdminPageModel : PageModel
    {
        private readonly IEventModelRepo eventModelRepo;

        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }
        public int Capacity { get; set; }
        [Required(ErrorMessage = "Please enter location")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Please enter event type")]
        public string EventType { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Price { get; set; }

        public List<EventModel> Events;

        public int EventToDelete { get; set; }

        public AdminPageModel(IEventModelRepo eventModelRepo)
        {
            this.eventModelRepo = eventModelRepo;
        }
        public async Task OnGet()
        {
            Events = await eventModelRepo.GetAllEventsAsync();
        }
        public async Task<IActionResult> OnPostAdd()
        {
            var newEvent = new EventModel
            {
                Name = Name,
                Capacity = Capacity,
                Location = Location,
                EventType = EventType,
                DateTime = DateTime,
                Price = Price,
                Image = $"/Images/EventImages/image {new Random().Next(1, 10)}.png"
            };
            await eventModelRepo.AddEventAsync(newEvent);
            Events = await eventModelRepo.GetAllEventsAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostDelete()
        {
            await eventModelRepo.DeleteEventAsync(await eventModelRepo.GetEventByIdAsync(EventToDelete));
            Events = await eventModelRepo.GetAllEventsAsync();
            return Page();
        }

        public IActionResult OnPost()
        {


            return RedirectToPage("/AppPages/CreateEvent");
        }


    }
}
