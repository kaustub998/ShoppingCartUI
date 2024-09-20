using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace EcorpUI.Models
{

    public class UserDetails
    {
        public int userId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string email { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public int? gender { get; set; }

        public bool isAdmin { get; set; }
        public bool? isDeactivated { get; set; }

        public bool? isDeleted { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public DateTime dateOfBirth { get; set; } = DateTime.Now;

        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$", ErrorMessage = "Invalid phone number")]
        public string contactNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#^&()_+=-])[A-Za-z\d@$!%*?&#^&()_+=-]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one lowercase letter, one uppercase letter, one digit, and one special character.")]
        public string? password { get; set; }
        public byte[]? image { get; set; }
    }

    public class UserEditDetails
    {
        public int userId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string email { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public int? gender { get; set; }

        public bool isAdmin { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public DateTime dateOfBirth { get; set; } = DateTime.Now;

        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$", ErrorMessage = "Invalid phone number")]
        public string contactNumber { get; set; }
        public byte[]? image { get; set; }
    }

    public class LoginDetail
    {
        [Required(ErrorMessage = "Please Enter Email.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please Enter Password.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }

    public partial class ChangePasswordModel
    {
        public int UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#^&()_+=-])[A-Za-z\d@$!%*?&#^&()_+=-]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one lowercase letter, one uppercase letter, one digit, and one special character.")]

        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }

    public partial class ForgotPasswordModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
    }

    public class ResetPasswordModel
    {
        public string? TokenId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#^&()_+=-])[A-Za-z\d@$!%*?&#^&()_+=-]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one lowercase letter, one uppercase letter, one digit, and one special character.")]
        public string? password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
