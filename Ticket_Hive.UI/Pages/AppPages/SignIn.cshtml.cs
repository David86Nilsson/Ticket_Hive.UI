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
        public void OnGet()
        {
            FailedLogIn = "";
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                //bytade Password1234! till Password för att kunna logga in med sitt nya lösenord efter det är bytat
                var signInResult = await signInManager.PasswordSignInAsync(UserName, Password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToPage("/Member/Home");
                }
                else
                {
                    FailedLogIn = "Wrong Username or Password";
                }
            }
            return Page();
        }
    }
}
