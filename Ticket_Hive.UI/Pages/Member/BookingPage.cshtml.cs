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

		public string search;
		

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
			// Get all events initially
			Events = await eventRepo.GetAllEventsAsync();
			SearchResults = Events;

			// Check if a search query is present
			var searchQuery = Request.Query["search"].ToString();
			if (!string.IsNullOrEmpty(searchQuery))
			{
				// Parse the search query into separate terms
				var searchTerms = searchQuery.Split(' ');

				// Set the search properties to the parsed terms
				search = searchTerms.FirstOrDefault();

				// Filter the search results based on the search properties
				SearchResults = SearchResults
					.Where(e =>
						(string.IsNullOrEmpty(search) || 
						e.Name.Contains(search, StringComparison.OrdinalIgnoreCase)  || 
						e.EventType.Contains(search, StringComparison.OrdinalIgnoreCase)  || 
						e.Location.Contains(search, StringComparison.OrdinalIgnoreCase)))
					.ToList();
			}
		}

		//public IActionResult OnPostSearch()
		//{
		//	// Call the method to perform the search
		//	PerformSearch();

		//	return Page();
		//}

		//private void PerformSearch()
		//{
		//	SearchResults = Events
		//.Where(e =>
		//				(string.IsNullOrEmpty(search) || 
		//				e.Name.Contains(search, StringComparison.OrdinalIgnoreCase) || 
		//				e.EventType.Contains(search, StringComparison.OrdinalIgnoreCase) || 
		//				e.Location.Contains(search, StringComparison.OrdinalIgnoreCase)))
		//			.ToList();
		//}
	}
}

