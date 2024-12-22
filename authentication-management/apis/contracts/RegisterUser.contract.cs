namespace authentication_management.database.contracts;

using System.ComponentModel.DataAnnotations;

public record RegisterUser
{

  [Required(ErrorMessage = "FullName is required.")]
  [MaxLength(255, ErrorMessage = "FullName cannot exceed 255 characters.")]
  public required string FullName { get; init; }

  [Required(ErrorMessage = "Specialization is required.")]
  [MaxLength(255, ErrorMessage = "Specialization cannot exceed 255 characters.")]
  public required string Specialization { get; init; }

  [Required(ErrorMessage = "UserName is required.")]
  [MaxLength(255, ErrorMessage = "UserName cannot exceed 255 characters.")]
  public required string UserName { get; init; }

  [Required(ErrorMessage = "Password is required.")]
  [MaxLength(255, ErrorMessage = "Password cannot exceed 255 characters.")]
  public required string Password { get; init; }

  public bool IsActive { get; init; } = true;

  [Required(ErrorMessage = "NationalUniqueId is required.")]
  [MaxLength(50)]
  public required string NationalUniqueId { get; set; } = string.Empty;

  [Required(ErrorMessage = "StaffUniqueId is required.")]
  [MaxLength(50)]
  public required string StaffUniqueId { get; set; } = string.Empty;

  [Required(ErrorMessage = "Contact is required.")]
  [MaxLength(15)]
  public required string Contact { get; set; } = string.Empty;

  [Required(ErrorMessage = "DateOfBirth is required.")]
  public DateOnly DateOfBirth { get; set; }

  [Required(ErrorMessage = "Email is required.")]
  [MaxLength(200)]
  public required string Email { get; set; } = string.Empty;

  [Required(ErrorMessage = "DateOfJoining is required.")]
  public DateOnly DateOfJoining { get; set; }

  [Required(ErrorMessage = "Designation is required.")]
  [MaxLength(30)]
  public required string Designation { get; set; } = string.Empty;

  [Required(ErrorMessage = "Address is required.")]
  [MaxLength(500)]
  public required string Address { get; set; } = string.Empty;

  [Required(ErrorMessage = "IsDoctor is required.")]
  public required bool IsDoctor { get; set; } = false;


  [Required(ErrorMessage = "Roles is required.")]
  [RegularExpression("^(Alpha|Bravo|Charlie|Delta)(,(Alpha|Bravo|Charlie|Delta))*$", ErrorMessage = "Roles must be a comma-separated list of 'Alpha', 'Bravo', 'Charlie', or 'Delta'.")]
  [MaxLength(100)]
  public required string Roles { get; set; }
}
public class ValidationRequest
{
  public string Field { get; set; }
  public string Value { get; set; }
}