using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;

namespace Ticket_Hive.UI.Pages
{

    [BindProperties]
    public class RegistreringsSidaModel : PageModel
    {
        // Dependency injection of SignInManager and IAppUserModelRepo
        private readonly SignInManager<IdentityUser> signInManager;
		private readonly IAppUserModelRepo AppUser;

		[Required (ErrorMessage = "Username cannot be empty!")]
        public string? Username { get; set; }

		[Required(ErrorMessage = "Password cannot be empty!")]
		public string? Password { get; set; }


        // Constructor to inject SignInManager and IAppUserModelRepo
        public RegistreringsSidaModel(SignInManager<IdentityUser> signInManager, IAppUserModelRepo AppUser)
        {
            this.signInManager = signInManager;
			this.AppUser = AppUser;
        }

        public void OnGet()
        {
        }
		//Register a new úser using POST
		public async Task<IActionResult> OnPost()
		{
            // Validate the ModelState
            if (ModelState.IsValid)
			{

                // Create a new instance of IdentityUser with the provided Username
                IdentityUser user = new()
				{
					UserName = Username
				};

                // Register the user using the repository
                var registerResult = await signInManager.UserManager.CreateAsync(user, Password);

				if (registerResult.Succeeded)
				{
                    // Test to log in with the provided password
                    AppUserModel newUser = new()
					{
						Username = Username

					};

                    // Add the new user to the repository
                    await AppUser.AddAppUserAsync(newUser);

                    // Attempt to sign in the user using the provided credentials
                    var signInResult = await signInManager.PasswordSignInAsync(Username, Password, false, false);

					if (signInResult.Succeeded)
					{
                        // Redirect the user to the SignIn page upon successful registration
                        return RedirectToPage("/AppPages/SignIn");
					}
				}
			}
            // If ModelState is not valid, return the same page
            return Page();
		}
	}
}
