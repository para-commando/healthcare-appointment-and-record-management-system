namespace database.models;
using System.ComponentModel.DataAnnotations; // For validation attributes
using System.ComponentModel.DataAnnotations.Schema; // For database-specific annotations
[Table("clinical_health_static_data")]
public class ClinicalHealthStaticData
{

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [MaxLength(255)]
    [Column("disease")]
    public required string Disease { get; set; } = string.Empty;

    [Column("composition")]
    public required string Composition { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    [Column("medicine_name")]
    public required string MedicineName { get; set; } = string.Empty;

    // column datatype is set to Text
    [Column("uses")]
    public required string Uses { get; set; } = string.Empty;

    // column datatype is set to Text
    [Column("side_effects")]
    public required string SideEffects { get; set; } = string.Empty;

}
