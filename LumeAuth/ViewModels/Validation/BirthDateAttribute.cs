using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace LumeAuth.ViewModels.Validation
{
    /// <summary>
    /// Validates a date of birth that is posted as three separate values (month, day and year).
    /// Put it on the year property and name the month and day properties.
    /// Checks that all three are chosen, that they form a real calendar date that is not in the future,
    /// and that the person is at least <see cref="MinimumAge"/> years old.
    /// The rules run on the server (the source of truth) and are mirrored in the browser by the
    /// "birthdate" adapter in auth.js, using the same messages.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class BirthDateAttribute : ValidationAttribute, IClientModelValidator
    {
        /// <summary>
        /// Gets the name of the property that holds the birth month (1 to 12).
        /// </summary>
        public string MonthProperty { get; }

        /// <summary>
        /// Gets the name of the property that holds the birth day (1 to 31).
        /// </summary>
        public string DayProperty { get; }

        /// <summary>
        /// Gets or sets the minimum age in years. Defaults to 13.
        /// </summary>
        public int MinimumAge { get; set; } = 13;

        /// <summary>
        /// Gets the message shown when the month, day or year has not been chosen.
        /// </summary>
        public string MissingMessage => "Enter your date of birth.";

        /// <summary>
        /// Gets the message shown when the date does not exist (for example 31 February) or is in the future.
        /// </summary>
        public string InvalidMessage => "Enter a valid date of birth.";

        /// <summary>
        /// Gets the message shown when the person is younger than <see cref="MinimumAge"/>.
        /// </summary>
        public string TooYoungMessage => $"You must be at least {MinimumAge} years old to join Lume.";

        /// <summary>
        /// Creates the attribute.
        /// </summary>
        /// <param name="monthProperty">The name of the property that holds the birth month.</param>
        /// <param name="dayProperty">The name of the property that holds the birth day.</param>
        public BirthDateAttribute(string monthProperty, string dayProperty)
        {
            MonthProperty = monthProperty;
            DayProperty = dayProperty;
        }

        /// <summary>
        /// Runs the server side check. The value being validated is the year.
        /// </summary>
        /// <param name="value">The posted birth year.</param>
        /// <param name="validationContext">The context that gives access to the rest of the model.</param>
        /// <returns>Success, or a result carrying the message for the first rule that failed.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            int? year = value as int?;
            int? month = ReadInt(validationContext.ObjectInstance, MonthProperty);
            int? day = ReadInt(validationContext.ObjectInstance, DayProperty);

            if (year is null || month is null || day is null)
            {
                return new ValidationResult(MissingMessage);
            }

            if (!TryBuildDate(year, month, day, out DateOnly dateOfBirth))
            {
                return new ValidationResult(InvalidMessage);
            }

            // UTC keeps the rule stricter, never looser, than the user's local date.
            DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);

            if (dateOfBirth > today)
            {
                return new ValidationResult(InvalidMessage);
            }

            if (CalculateAge(dateOfBirth, today) < MinimumAge)
            {
                return new ValidationResult(TooYoungMessage);
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Adds the data-val attributes that the browser side "birthdate" adapter reads.
        /// The month and day parameters are input names, so they assume the form has no model prefix.
        /// </summary>
        /// <param name="context">The context used to add the HTML attributes.</param>
        public void AddValidation(ClientModelValidationContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            context.Attributes.TryAdd("data-val", "true");
            context.Attributes.TryAdd("data-val-birthdate", MissingMessage);
            context.Attributes.TryAdd("data-val-birthdate-month", MonthProperty);
            context.Attributes.TryAdd("data-val-birthdate-day", DayProperty);
            context.Attributes.TryAdd("data-val-birthdate-minimumage", MinimumAge.ToString(CultureInfo.InvariantCulture));
            context.Attributes.TryAdd("data-val-birthdate-missing", MissingMessage);
            context.Attributes.TryAdd("data-val-birthdate-invalid", InvalidMessage);
            context.Attributes.TryAdd("data-val-birthdate-tooyoung", TooYoungMessage);
        }

        /// <summary>
        /// Builds a date from its parts, returning <c>false</c> when a part is missing or the date does not exist.
        /// </summary>
        /// <param name="year">The birth year.</param>
        /// <param name="month">The birth month (1 to 12).</param>
        /// <param name="day">The birth day (1 to 31).</param>
        /// <param name="date">The resulting date when the method returns <c>true</c>.</param>
        /// <returns><c>true</c> when the parts form a real calendar date.</returns>
        public static bool TryBuildDate(int? year, int? month, int? day, out DateOnly date)
        {
            date = default;

            if (year is not { } y || month is not { } m || day is not { } d)
            {
                return false;
            }

            if (y < 1 || y > 9999 || m < 1 || m > 12 || d < 1 || d > DateTime.DaysInMonth(y, m))
            {
                return false;
            }

            date = new DateOnly(y, m, d);
            return true;
        }

        /// <summary>
        /// Calculates the age in whole years on the given day.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth.</param>
        /// <param name="today">The day the age is measured on.</param>
        /// <returns>The age in completed years.</returns>
        public static int CalculateAge(DateOnly dateOfBirth, DateOnly today)
        {
            int age = today.Year - dateOfBirth.Year;

            if (today < dateOfBirth.AddYears(age))
            {
                age--;
            }

            return age;
        }

        /// <summary>
        /// Reads an integer property from the model by name.
        /// </summary>
        private static int? ReadInt(object instance, string propertyName)
        {
            return instance.GetType().GetProperty(propertyName)?.GetValue(instance) as int?;
        }
    }
}