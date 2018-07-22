using System.ComponentModel.DataAnnotations;

namespace vsx.Validators
{
    public class NotNullOrEmptyAttribute : ValidationAttribute
    {
        public NotNullOrEmptyAttribute()
            : base ("The value for {0} cannot be null or empty!")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string str && string.IsNullOrEmpty(str))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }
    }
}
