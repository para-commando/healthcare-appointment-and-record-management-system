using System.ComponentModel.DataAnnotations;
namespace clinical_data_grid.contracts.customValidations;
public class CustomKeyValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var name = value as string;

        if (name?.Length >255)
        {
            return new ValidationResult("uses names cannot be longer than 255 characters.");
        }

        return ValidationResult.Success!;
    }
}
