using System.ComponentModel.DataAnnotations;
using System;
namespace LanguageInformant.WebUI.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "First Name Required")]
        [Display(Name = "First Name:")]
        [StringLength(50, ErrorMessage = "Less than 50 characters")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last Name Required")]
        [Display(Name = "Last Name:")]
        [StringLength(50, ErrorMessage = "Less than 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "EmailId Required:")]
        [Display(Name = "Email:")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
                                                ErrorMessage = "Email Format is wrong")]
        [StringLength(50, ErrorMessage = "Less than 50 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Street Address Required")]
        [Display(Name = "Street Address:")]
        [StringLength(100, ErrorMessage = "Less than 100 characters")]
        public string StreetAddress { get; set; }


        [Required(ErrorMessage = "City Required")]
        [Display(Name = "City:")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]
        [StringLength(50, ErrorMessage = "Less than 50 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "State Required")]
        [Display(Name = "State:")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]
        [StringLength(50, ErrorMessage = "Less than 50 characters")]
        public string State { get; set; }

        [Required(ErrorMessage = "ZipCode Required")]
        [Display(Name = "Zip Code:")]
        [StringLength(20, ErrorMessage = "Less than 20 characters")]
        public string ZipCode { get; set; }

        [Display(Name = "Country:")]
        [StringLength(20, ErrorMessage = "Less than 20 characters")]
        public string Country { get; set; }
    }
}