namespace authentication_management.database.contracts;

using System.ComponentModel.DataAnnotations;

public record EditUser
{


  [MaxLength(255, ErrorMessage = "FullName cannot exceed 255 characters.")]
  public string FullName { get; init; }


  [MaxLength(255, ErrorMessage = "Specialization cannot exceed 255 characters.")]
  public string Specialization { get; init; }

  [Required(ErrorMessage = "UserName is required.")]
  [MaxLength(255, ErrorMessage = "UserName cannot exceed 255 characters.")]
  public required string UserName { get; init; }

  [MaxLength(255, ErrorMessage = "Password cannot exceed 255 characters.")]
  public string Password { get; init; }

  public bool IsActive { get; init; } = true;

  [MaxLength(50)]
  public string NationalUniqueId { get; set; } = string.Empty;

  [Required(ErrorMessage = "StaffUniqueId is required.")]
  [MaxLength(50)]
  public required string StaffUniqueId { get; set; } = string.Empty;

  [MaxLength(15)]
  public string Contact { get; set; } = string.Empty;

  public DateOnly DateOfBirth { get; set; }

  [MaxLength(200)]
  public string Email { get; set; } = string.Empty;

  public DateOnly DateOfJoining { get; set; }

  [MaxLength(30)]
  public string Designation { get; set; } = string.Empty;

  [MaxLength(500)]
  public string Address { get; set; } = string.Empty;

  public bool IsDoctor { get; set; } = false;
  public int DoctorId { get; init; }

  [MaxLength(100)]
  [RegularExpression("^(Alpha|Bravo|Charlie|Delta)(,(Alpha|Bravo|Charlie|Delta))*$", ErrorMessage = "Roles must be a comma-separated list of 'Alpha', 'Bravo', 'Charlie', or 'Delta'.")]
  public string Roles { get; set; }
}
