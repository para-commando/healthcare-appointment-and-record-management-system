using System.ComponentModel.DataAnnotations;

public class ValidTimeOfDayAttribute : ValidationAttribute
{
    private readonly HashSet<string> _validValues = new() { "Morning", "Noon", "Evening", "Night" };

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is List<string> list)
        {
            foreach (var item in list)
            {
                if (!_validValues.Contains(item))
                {
                    return new ValidationResult($"Invalid value '{item}' in TimeOfDay. Allowed values are: Morning, Noon, Evening, Night.");
                }
            }
            return ValidationResult.Success;
        }

        return new ValidationResult("TimeOfDay must be a list of strings.");
    }
}
