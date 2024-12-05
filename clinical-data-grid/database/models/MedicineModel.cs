namespace clinical_data_grid.database.models;
using System.ComponentModel.DataAnnotations; // For validation attributes
using System.ComponentModel.DataAnnotations.Schema; // For database-specific annotations
[Table("medicines")]
public class Medicines
{

    [Key]
    [Column("id")]
    public int id { get; set; }

    [Required] // Ensures this property cannot be null
    [MaxLength(255)] // Limits the length of the name
    [Column("name")]
    public required string name { get; set; }

    [MaxLength(255)] // Limits the length of the name
    [Column("details")]
    public required string details { get; set; } = string.Empty;
    public ICollection<Diseases> Diseases { get; set; } = new List<Diseases>();

    // represents many side of a many to many relationship
    public ICollection<MedicineBenefits> MedicineBenefits { get; set; } = new List<MedicineBenefits>();
    // represents many side of a many to many relationship
    public ICollection<MedicalSideEffects> MedicalSideEffects { get; set; } = new List<MedicalSideEffects>();

}
