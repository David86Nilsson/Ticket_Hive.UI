using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;

namespace Ticket_Hive.UI.Pages.Member
{
    /// <summary>
    /// Represents the Booking page model for the Booking area of the website.
    /// </summary>
    public class BookingPageModel : PageModel
	{

		private readonly IEventModelRepo eventRepo;
		public List<EventModel> SearchResults { get; set; }
		public List<EventModel> Events { get; set; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingPageModel"/> class.
        /// </summary>
        /// <param name="eventRepo">The event model repository.</param>
        public BookingPageModel(IEventModelRepo eventRepo)
		{

			this.eventRepo = eventRepo;
		}

		public async Task OnGetAsync()
		{

		}

	}


}






