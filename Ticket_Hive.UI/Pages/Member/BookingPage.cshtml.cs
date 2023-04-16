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

		public async Task OnGetAsync()
		{

			
		}
	}
}

				




