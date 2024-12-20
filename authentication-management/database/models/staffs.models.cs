namespace authentication_management.database.models;

using System.ComponentModel.DataAnnotations; // For validation attributes
using System.ComponentModel.DataAnnotations.Schema; // For database-specific annotations

[Table("staffs")]
public class Staff
{

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("staff_unique_id")]
    public required string StaffUniqueId { get; set; } = string.Empty;

    [MaxLength(255)]
    [Column("email")]
    public required string Email { get; set; } = string.Empty;

    [Required]
    [Column("date_of_birth")]
    public DateOnly DateOfBirth { get; set; }

    [MaxLength(500)]
    [Column("address")]
    public required string Address { get; set; } = string.Empty;

    [MaxLength(50)]
    [Column("unique_id")]
    public required string UniqueId { get; set; } = string.Empty;

    [MaxLength(15)]
    [Column("contact")]
    public required string Contact { get; set; } = string.Empty;

    [Required]
    [Column("date_of_joining")]
    public DateOnly DateOfJoining { get; set; }

    [MaxLength(30)]
    [Column("designation")]
    public required string Designation { get; set; } = string.Empty;
}
