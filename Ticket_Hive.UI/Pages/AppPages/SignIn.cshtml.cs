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

        [Required(ErrorMessage = "Invalid Username, please try again")]
        public string UserName { get; set; }


        [BindProperty]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Wrong Password, please try again")]
        public string Password { get; set; }

        public SignInModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var signInResult = await signInManager.PasswordSignInAsync("user", "Password1234!", false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToPage("/Member/Home");
                }
            }
            return Page();
        }
    }
}
