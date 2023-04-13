using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Ticket_Hive.UI.Data;

namespace Ticket_Hive.UI.Pages.Member
{

    /// <summary>
    /// ChangePasswordModel-klassen som ärver från PageModel.
    ///  kräver att användaren är auktoriserad.
    /// </summary>
    [Authorize]
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        /// <summary>
        /// Skapar en ny instans av ChangePasswordModel-klassen.
        /// </summary>
        /// <param name="userManager">En instans av UserManager för hantering av användare.</param>
        /// <param name="signInManager">En instans av SignInManager för hantering av inloggning.</param>
        /// <param name="logger">En instans av ILogger för att logga händelser.</param>
        public ChangePasswordModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// Hämtar eller sätter meddelandet för varningsmeddelandet.
        /// </summary>
        public string AlertMessage { get; set; }

        /// <summary>
        /// Hämtar eller sätter typen av varningsmeddelande.
        /// </summary>
        public string AlertType { get; set; }

        /// <summary>
        /// Representerar en klass för att binda inputfält i change password form.
        /// </summary>
        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string CurrentPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        /// <summary>
        /// Hanterar POST-begäran för att ändra lösenordet.
        /// </summary>
        /// <returns>En IActionResult som representerar resultatet av operationen.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Model state is not valid");
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogInformation("User not found");
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.CurrentPassword, Input.NewPassword);
            if (changePasswordResult.Succeeded)
            {
                _logger.LogInformation("Password change succeeded");
                await _signInManager.RefreshSignInAsync(user);
                AlertMessage = "Password changed successfully!";
                AlertType = "success";
                return Page();
            }
            else
            {
                _logger.LogInformation("Password change failed");
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
        }
    }
 
}
