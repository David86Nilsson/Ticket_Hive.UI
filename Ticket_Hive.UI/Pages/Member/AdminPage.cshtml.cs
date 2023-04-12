using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ticket_Hive.UI.Pages.Member
{
    [BindProperties]
    public class AdminPageModel : PageModel
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public string EventType { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Price { get; set; }

        public AdminPageModel()
        {
        }
        public void OnGet()
        {
        }
    }
}
