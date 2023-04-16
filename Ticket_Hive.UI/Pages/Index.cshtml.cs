using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ticket_Hive.UI.Pages
{
    /// <summary>
    /// Denna klass är  ansvarig för att hantera användarinteraktioner på startsidan.
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Hanterar click event för inloggning.
        /// Omdirigerar användaren till inloggningssidan när knappen "Sign In" klickas.
        /// </summary>
        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SignIn");
        }

        /// <summary>
        /// Hanterar click event för registrering.
        /// Omdirigerar användaren till registreringssidan när knappen "Register" klickas.
        /// </summary>
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Register");
        }
    }
}