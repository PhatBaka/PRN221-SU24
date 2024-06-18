using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Helper
{
    public class RequireEnumAttribute : ValidationAttribute
    {
        private readonly Type _enumType;
        public RequireEnumAttribute(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("The type is not an enum", nameof(enumType));
            }
            _enumType = enumType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && Enum.IsDefined(_enumType, value))
            {
                return ValidationResult.Success;
            }

            var enumValues = Enum.GetNames(_enumType);
            return new ValidationResult($"The value for {validationContext.DisplayName} must be one of the following: {string.Join(", ", enumValues)}.");
        }
    }
}
