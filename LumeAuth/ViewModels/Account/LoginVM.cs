using System.ComponentModel.DataAnnotations;

namespace LumeAuth.ViewModels.Account
{
    /// <summary>
    /// Represents the view model for the login page, containing the credentials entered by the user.
    /// </summary>
    public class LoginVM
    {
        /// <summary>
        /// Gets or sets the email address used to log in.
        /// </summary>
        [Required(ErrorMessage = "Enter your email address.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string Email      { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password used to log in.
        /// </summary>
        [Required(ErrorMessage = "Enter your password.")]
        [DataType(DataType.Password)]
        public string Password   { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the login should persist across browser sessions.
        /// </summary>
        public bool RememberMe   { get; set; }

        /// <summary>
        /// Gets or sets the local URL to send the user back to after a successful login.
        /// </summary>
        public string? ReturnUrl { get; set; }
    }
}
