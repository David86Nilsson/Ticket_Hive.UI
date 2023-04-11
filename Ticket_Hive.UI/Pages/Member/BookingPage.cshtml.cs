using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

		public string search;


		public List<EventModel> SearchResults { get; set; }

		public List<EventModel> Events { get; set; } = new();

		public BookingPageModel(SignInManager<IdentityUser> signInManager, IEventModelRepo eventRepo, IAppUserModelRepo appUserModelRepo)
		{
			this.signInManager = signInManager;
			this.eventRepo = eventRepo;
			this.appUserModelRepo = appUserModelRepo;
		}

		public async Task OnGetAsync(string sortOrder)
		{

			Events = await eventRepo.GetAllEventsAsync();
			SearchResults = Events;


			var searchQuery = Request.Query["search"].ToString();
			if (!string.IsNullOrEmpty(searchQuery))
			{

				var searchTerms = searchQuery.Split(' ');


				search = searchTerms.FirstOrDefault();


				SearchResults = SearchResults
					.Where(e =>
						(string.IsNullOrEmpty(search) ||
						e.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
						e.EventType.Contains(search, StringComparison.OrdinalIgnoreCase) ||
						e.Location.Contains(search, StringComparison.OrdinalIgnoreCase)))
					.ToList();

			}
		}
	}
}

				




