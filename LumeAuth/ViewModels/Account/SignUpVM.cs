using LumeAuth.ViewModels.Validation;
using System.ComponentModel.DataAnnotations;

namespace LumeAuth.ViewModels.Account
{
    /// <summary>
    /// Represents the view model for the sign up page, containing the details needed to create an account.
    /// </summary>
    public class SignUpVM
    {
        /// <summary>
        /// Gets or sets the full name of the new user. Maps to <c>User.fullName</c> in LumeData.
        /// </summary>
        [Required(ErrorMessage = "Enter your full name.")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "Your name must be between 2 and 80 characters.")]
        public string FullName        { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address of the new user.
        /// </summary>
        [Required(ErrorMessage = "Enter your email address.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        [StringLength(254, ErrorMessage = "That email address is too long.")]
        public string Email           { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password. Must be at least 8 characters with a letter and a number.
        /// </summary>
        [Required(ErrorMessage = "Create a password.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).{8,100}$",
            ErrorMessage = "Use 8 or more characters with at least one letter and one number.")]
        [DataType(DataType.Password)]
        public string Password        { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password confirmation. Must match <see cref="Password"/>.
        /// </summary>
        [Required(ErrorMessage = "Confirm your password.")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the user accepted the terms and privacy policy.
        /// </summary>
        [MustBeTrue(ErrorMessage = "You need to accept the terms to continue.")]
        public bool AcceptTerms       { get; set; }
    }
}
