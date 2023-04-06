using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ticket_Hive.Data.Models;

namespace Ticket_Hive.UI.Pages.Member
{
    public class ConfirmationPageModel : PageModel
    {


        public List<EventModel> Events { get; set; } = new()
        {
           new EventModel()
           {
            
               Location = "Malm�",
               Price = 500,
               Capacity = 10000,
               
           },

           new EventModel()
           {
			   Location = "G�teborg",
			   Price = 1100,
			   Capacity = 30000,

		   }
		};

		public void OnGet()
        {
        }
    }
}
