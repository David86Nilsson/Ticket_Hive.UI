using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Ticket_Hive.UI.Pages.AppPages
{
    [Authorize(Roles = "Admin")]
    public class CreateEventModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
