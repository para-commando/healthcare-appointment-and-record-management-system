namespace authentication_management.database.models;

using System.ComponentModel.DataAnnotations; // For validation attributes
using System.ComponentModel.DataAnnotations.Schema; // For database-specific annotations

[Table("authentication_creds")]
public class Authentication
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_name")]
    public required string UserName { get; set; } = string.Empty;

    [Column("password")]
    public required string Password { get; set; } = string.Empty;

    [Required]
    [Column("is_active")]
    public bool IsActive { get; set; }

    [MaxLength(100)]
    [Column("roles")]
    public required string Roles { get; set; } = string.Empty;
}