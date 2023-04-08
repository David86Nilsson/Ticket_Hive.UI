using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;
using Ticket_Hive.Logic;

namespace Ticket_Hive.UI.Pages.Member
{
    public class BookingPageModel : PageModel
    {

		private readonly SignInManager<IdentityUser> signInManager;
		private readonly IEventModelRepo eventRepo;
		private readonly IAppUserModelRepo appUserModelRepo;
		public string Name { get; set; }
        public string EventType { get; set; }
        public string Location { get; set; }

        public List<EventModel> SearchResults { get; set; }

        public List<EventModel> Events { get; set; } = new();

        public BookingPageModel(SignInManager<IdentityUser> signInManager, IEventModelRepo eventRepo, IAppUserModelRepo appUserModelRepo)
        {
			this.signInManager = signInManager;
			    this.eventRepo = eventRepo;
			this.appUserModelRepo = appUserModelRepo;


		}
        public async Task OnGet()
        {
			Events = await eventRepo.GetAllEventsAsync();
            Name = Request.Query["name"];
            EventType = Request.Query["eventType"];
            Location = Request.Query["location"];

            if (!string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(EventType) || !string.IsNullOrEmpty(Location))
            {
                GetSearchResults();
            }
        }

        private void GetSearchResults()
        {
           
            SearchResults = Events
                .Where(e => e.Name.Contains(Name))
                .Where(e => e.EventType.Contains(EventType))
                .Where(e => e.Location.Contains(Location))
                .ToList();
        }
    }

}
