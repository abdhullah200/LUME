using System.ComponentModel.DataAnnotations;

namespace LumeAuth.ViewModels.Validation
{
    /// <summary>
    /// Validates that a boolean property is <c>true</c>. Used for "I agree to the terms" style checkboxes,
    /// where <see cref="RequiredAttribute"/> does not work because a bool is never null.
    /// Server side only. The browser side is handled by the <c>required</c> attribute on the checkbox.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MustBeTrueAttribute : ValidationAttribute
    {
        /// <summary>
        /// Returns <c>true</c> only when the value is a boolean <c>true</c>.
        /// </summary>
        /// <param name="value">The value being validated.</param>
        public override bool IsValid(object? value) => value is bool accepted && accepted;
    }
}
