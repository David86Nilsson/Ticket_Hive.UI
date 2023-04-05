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
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly IAppUserModelRepo AppUser;

		[Required (ErrorMessage = "Username cannot be empty!")]
        public string? Username { get; set; }

		[Required(ErrorMessage = "Password cannot be empty!")]
		public string? Password { get; set; }

        public RegistreringsSidaModel(SignInManager<IdentityUser> signInManager, IAppUserModelRepo AppUser)
        {
            this.signInManager = signInManager;
			this.AppUser = AppUser;
        }

        public void OnGet()
        {
        }

		public async Task<IActionResult> OnPost()
		{
			if (ModelState.IsValid)
			{
				// Registrera en anv�ndare

				// Skapa en ny instans av IdentityUser med anv�ndarnamn
				IdentityUser user = new()
				{
					UserName = Username
				};

				// Registrera anv�ndaren med hj�lp av repot
				var registerResult = await signInManager.UserManager.CreateAsync(user, Password);

				if (registerResult.Succeeded)
				{
					// Testa att logga in med l�senord
					AppUserModel newUser = new()
					{
						Username = Username

					};

					await AppUser.AddAppUserAsync(newUser);

					var signInResult = await signInManager.PasswordSignInAsync(Username, Password, false, false);

					if (signInResult.Succeeded)
					{
						// Skicka anv�ndaren till Klotterplanket
						return RedirectToPage("/AppPages/SignIn");
					}
				}
			}

			return Page();
		}
	}
}
