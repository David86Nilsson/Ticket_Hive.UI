using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ticket_Hive.UI.Pages
{

    public class IndexModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        public IndexModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task OnGetAsync()
        {
            //Test
            //Test login
            //Implement
        }
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