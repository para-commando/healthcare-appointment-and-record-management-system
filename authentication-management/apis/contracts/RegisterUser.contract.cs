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

  private bool IsActive { get; set; } = true;

  [MaxLength(50)]
  public required string NationalUniqueId { get; set; } = string.Empty;

  [MaxLength(50)]
  public required string StaffUniqueId { get; set; } = string.Empty;


  [MaxLength(15)]
  public required string Contact { get; set; } = string.Empty;
  public DateOnly DateOfBirth { get; set; }
  public required string Email { get; set; } = string.Empty;

  public DateOnly DateOfJoining { get; set; }

  [MaxLength(30)]
  public required string Designation { get; set; } = string.Empty;

  [MaxLength(500)]
  public required string Address { get; set; } = string.Empty;


}
