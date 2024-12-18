namespace staff_management.database.models;
using System.ComponentModel.DataAnnotations; // For validation attributes
using System.ComponentModel.DataAnnotations.Schema; // For database-specific annotations
[Table("doctor-details")]
public class DoctorDetails
{

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [MaxLength(255)]
    [Column("doctor_name")]
    public required string DoctorName { get; set; } = string.Empty;

    [MaxLength(255)]
    [Column("doctor_specialization")]
    public required string DoctorSpecialization { get; set; } = string.Empty;

    [MaxLength(500)]
    [Column("doctor_address")]
    public required string DoctorAddress { get; set; } = string.Empty;

    [MaxLength(50)]
    [Column("doctor_contact")]
    public required string DoctorContact { get; set; } = string.Empty;

    [MaxLength(20)]
    [Column("doctor_unique_id")]
    public required string DoctorUniqueId { get; set; } = string.Empty;

    [Required]
    [Column("doctor_registration_date")]
    public DateOnly DoctorDateOfJoining { get; set; }



}
