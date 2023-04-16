using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Ticket_Hive.UI.Pages.AppPages
{
    public class SignInModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        [BindProperty]

        [Required(ErrorMessage = "No username, please try again")]
        public string UserName { get; set; }


        [BindProperty]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "No password, please try again")]
        public string Password { get; set; }

        public string FailedLogIn;

        public SignInModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        /// <summary>
        /// Handles GET requests for the Sign In page
        /// </summary>
        public void OnGet()
        {
            FailedLogIn = "";
        }

        /// <summary>
        /// Async OnPost method for sign in page
        /// </summary>
        /// <returns>Returns a Task of type IActionResult</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Attempts to sign in the user with the provided credentials
                var signInResult = await signInManager.PasswordSignInAsync(UserName, Password, false, false);
                if (signInResult.Succeeded)
                {
                    // Redirects the user to  home page if the sign in is successful
                    return RedirectToPage("/Member/Home");
                }
                else
                {
                    // Alerting the user indicating that the sign in failed
                    FailedLogIn = "Wrong Username or Password";
                }
            }
            // Returns the current page if the ModelState is invalid
            return Page();
        }
    }
}
