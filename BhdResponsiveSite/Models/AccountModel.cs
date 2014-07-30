using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BhdResponsiveSite.Models
{
    public class AccountModel
    {
        [Required(ErrorMessage = "You must enter a username")]
        [StringLength(160, MinimumLength = 6, ErrorMessage = "Username must be at least 6 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [StringLength(160, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("Password", ErrorMessage = "The passwords must match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }        
    }
}