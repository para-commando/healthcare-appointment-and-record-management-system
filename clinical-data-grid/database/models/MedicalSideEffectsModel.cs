namespace clinical_data_grid.database.models;
using System.ComponentModel.DataAnnotations; // For validation attributes
using System.ComponentModel.DataAnnotations.Schema; // For database-specific annotations
[Table("medical_side_effects")]
public class MedicalSideEffects
{
    [Key]
    [Column("id")]
    public int id { get; set; }

    [Required] // Ensures this property cannot be null
    [MaxLength(255)] // Limits the length of the name
    [Column("side_effects")]
    public required string sideEffects { get; set; }

    public ICollection<Medicines> Medicines { get; set; } = new List<Medicines>();

}
