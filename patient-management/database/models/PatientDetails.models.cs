namespace patient_management.database.models;
using System.ComponentModel.DataAnnotations; // For validation attributes
using System.ComponentModel.DataAnnotations.Schema; // For database-specific annotations
using System.Text.Json.Serialization;
using patient_management.database.validations;
[Table("patient-details")]
public class PatientDetails
{

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [MaxLength(255)]
    [Column("patient_name")]
    public required string PatientName { get; set; } = string.Empty;

    [MaxLength(500)]
    [Column("patient_address")]
    public required string PatientAddress { get; set; } = string.Empty;

    [MaxLength(50)]
    [Column("patient_contact")]
    public required string PatientContact { get; set; } = string.Empty;

    [MaxLength(20)]
    [Column("patient_unique_id")]
    public required string PatientUniqueId { get; set; } = string.Empty;

    [Required]
    [Column("patient_registration_date")]
    public DateOnly PatientRegistrationDate { get; set; }

    [Column("patient_latest_date_of_visit")]
    public DateOnly PatientLatestDateOfVisit { get; set; }

}
