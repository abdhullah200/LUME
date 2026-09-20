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
        /// Gets or sets the username (the public handle used in profile links and mentions).
        /// 3 to 30 characters: letters, numbers, underscores and periods.
        /// Uniqueness (case-insensitive) is checked against the database when sign up is wired up.
        /// </summary>
        [Required(ErrorMessage = "Choose a username.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Your username must be between 3 and 30 characters.")]
        [RegularExpression(@"^[A-Za-z0-9._]+$", ErrorMessage = "Use only letters, numbers, underscores and periods.")]
        public string Username        { get; set; } = string.Empty;

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
        /// Gets or sets the birth month (1 to 12).
        /// </summary>
        public int? BirthMonth        { get; set; }

        /// <summary>
        /// Gets or sets the birth day (1 to 31).
        /// </summary>
        public int? BirthDay          { get; set; }

        /// <summary>
        /// Gets or sets the birth year. Carries the whole date of birth rule: the date must be complete,
        /// real, not in the future, and the user must be at least 13 years old.
        /// </summary>
        [BirthDate(nameof(BirthMonth), nameof(BirthDay), MinimumAge = 13)]
        public int? BirthYear         { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user accepted the terms and privacy policy.
        /// </summary>
        [MustBeTrue(ErrorMessage = "You need to accept the terms to continue.")]
        public bool AcceptTerms       { get; set; }

        /// <summary>
        /// Builds the date of birth from the three posted parts.
        /// A method rather than a property so MVC does not try to bind or validate it.
        /// </summary>
        /// <returns>The date of birth, or <c>null</c> when the parts do not form a real date.</returns>
        public DateOnly? GetDateOfBirth()
        {
            return BirthDateAttribute.TryBuildDate(BirthYear, BirthMonth, BirthDay, out DateOnly dateOfBirth)
                ? dateOfBirth
                : null;
        }
    }
}