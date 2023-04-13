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
		
		private readonly IEventModelRepo eventRepo;

		
		public List<EventModel> SearchResults { get; set; }
		public List<EventModel> Events { get; set; } = new();
        public string search { get; set; }

        public BookingPageModel(IEventModelRepo eventRepo)
		{
			
			this.eventRepo = eventRepo;
		}

		public async Task OnGetAsync(string nameSearch)
		{

			Events = await eventRepo.GetAllEventsAsync();
			SearchResults = Events;


			nameSearch = Request.Query["search"].ToString();
			if (!string.IsNullOrEmpty(nameSearch))
			{

				var searchTerms = nameSearch.Split(' ');


				search = searchTerms.FirstOrDefault();


				SearchResults = SearchResults
					.Where(e =>
						(string.IsNullOrEmpty(search) ||
						e.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
						e.EventType.Contains(search, StringComparison.OrdinalIgnoreCase) ||
						e.Location.Contains(search, StringComparison.OrdinalIgnoreCase)))
					.ToList();
				
			}
			var sortField = Request.Query["sortField"].ToString().ToLower();
			switch (sortField)
			{
				case "name":
					SearchResults = SearchResults.OrderBy(e => e.Name).ToList();
					break;
				case "eventtype":
					SearchResults = SearchResults.OrderBy(e => e.EventType).ToList();
					break;
				case "location":
					SearchResults = SearchResults.OrderBy(e => e.Location).ToList();
					break;
				case "price":
					SearchResults = SearchResults.OrderBy(e => e.Price).ToList();
					break;
				case "date":
					SearchResults = SearchResults.OrderBy(e => e.DateTime).ToList();
					break;
				default:
					break;
			}
		}
		public IActionResult OnPostGoToHome()
		{
			return RedirectToPage("/Member/Home");
		}
	}
}

				




