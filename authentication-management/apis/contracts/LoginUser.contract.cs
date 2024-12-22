namespace authentication_management.database.contracts;

using System.ComponentModel.DataAnnotations;

public record LoginUser
{

  [Required(ErrorMessage = "UserName is required.")]
  [MaxLength(255, ErrorMessage = "UserName cannot exceed 255 characters.")]
  public required string UserName { get; init; }

  [Required(ErrorMessage = "Password is required.")]
  [MaxLength(255, ErrorMessage = "Password cannot exceed 255 characters.")]
  public required string Password { get; init; }

  [Required(ErrorMessage = "StaffUniqueId is required.")]
  [MaxLength(50)]
  public required string StaffUniqueId { get; set; } = string.Empty;


}
