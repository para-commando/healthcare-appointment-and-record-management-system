using System.ComponentModel.DataAnnotations;

public class PhoneNumberListValidationAttribute : ValidationAttribute
{
    private const string PhoneNumberRegex = @"^\+?[0-9\s\-()]+$";
    private readonly int _maxCount;

    public PhoneNumberListValidationAttribute(int maxCount)
    {
        _maxCount = maxCount;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var phoneNumbers = value as List<string>;
        if (phoneNumbers == null)
            return ValidationResult.Success;

        if (phoneNumbers.Count > _maxCount)
        {
            return new ValidationResult($"You can store up to {_maxCount} phone numbers only.");
        }

        var invalidPhoneNumbers = phoneNumbers
            .Where(phone => !System.Text.RegularExpressions.Regex.IsMatch(phone, PhoneNumberRegex))
            .ToList();

        if (invalidPhoneNumbers.Any())
        {
            return new ValidationResult($"Invalid phone number(s): {string.Join(", ", invalidPhoneNumbers)}");
        }

        return ValidationResult.Success;
    }
}
