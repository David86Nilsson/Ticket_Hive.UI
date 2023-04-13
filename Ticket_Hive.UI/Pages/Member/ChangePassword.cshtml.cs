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
    /// ChangePasswordModel-klassen som �rver fr�n PageModel.
    ///  kr�ver att anv�ndaren �r auktoriserad.
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
        /// <param name="userManager">En instans av UserManager f�r hantering av anv�ndare.</param>
        /// <param name="signInManager">En instans av SignInManager f�r hantering av inloggning.</param>
        /// <param name="logger">En instans av ILogger f�r att logga h�ndelser.</param>
        public ChangePasswordModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// H�mtar eller s�tter meddelandet f�r varningsmeddelandet.
        /// </summary>
        public string AlertMessage { get; set; }

        /// <summary>
        /// H�mtar eller s�tter typen av varningsmeddelande.
        /// </summary>
        public string AlertType { get; set; }

        /// <summary>
        /// Representerar en klass f�r att binda inputf�lt i change password form.
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
        /// Hanterar POST-beg�ran f�r att �ndra l�senordet.
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
