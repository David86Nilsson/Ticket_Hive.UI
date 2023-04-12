using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Ticket_Hive.UI.Pages.AppPages
{
    public class SignInModel : PageModel
    {

        [BindProperty]
        
        [Required(ErrorMessage = "Invalid Username, please try again")]
        public string UserName { get; set; }


        [BindProperty]        
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Wrong Password, please try again")]
        public string Password { get; set; }

        public void OnGet()
        {
        }
    }
}
