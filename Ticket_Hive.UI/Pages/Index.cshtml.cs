using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ticket_Hive.UI.Pages
{

    public class IndexModel : PageModel
    {
        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SignIn");
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Register");
        }
    }
}