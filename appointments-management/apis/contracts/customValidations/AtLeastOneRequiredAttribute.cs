using System.ComponentModel.DataAnnotations;
namespace appointment_details.contracts.customValidations;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class AtLeastOneRequiredAttribute : ValidationAttribute
{
    private readonly string[] _propertyNames;

    public AtLeastOneRequiredAttribute(params string[] propertyNames)
    {
        _propertyNames = propertyNames;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return ValidationResult.Success!;

        // Using reflections to get property values
        var type = value.GetType();
        var hasValue = _propertyNames.Any(propertyName =>
        {
            var property = type.GetProperty(propertyName);
            var propertyValue = property?.GetValue(value);
            return propertyValue != null && !string.IsNullOrEmpty(propertyValue?.ToString()?.Trim());
        });

        if (!hasValue)
        {
            return new ValidationResult($"At least one of the following properties must have a value: {string.Join(", ", _propertyNames)}");
        }

        return ValidationResult.Success!;
    }
}
