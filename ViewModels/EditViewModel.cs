using System.ComponentModel.DataAnnotations;

namespace TS.ViewModels
{
    public class EditViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} Length Must Be between {2} and {1} characters")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Use letters only")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} Length Must Be between {2} and {1} characters")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Use letters only")]
        public string LastName { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 10)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Enter a valid phone number.")]
        [Display(Name = "Phone Number")]
        public string UserPhone { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "New Email Address")]
        public string Email { get; set; }
    }
}
