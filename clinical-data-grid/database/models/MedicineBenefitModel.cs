namespace clinical_data_grid.database.models;
using System.ComponentModel.DataAnnotations; // For validation attributes
using System.ComponentModel.DataAnnotations.Schema; // For database-specific annotations
[Table("medicine_benefits")]
public class MedicineBenefits
{
    [Key]
    [Column("id")]
    public int id { get; set; }

    [Required] // Ensures this property cannot be null
    [MaxLength(255)] // Limits the length of the name
    [Column("benefit")]
    public required string benefit { get; set; }
    // represents many side of a many to many relationship

    public ICollection<Medicines> Medicines { get; set; } = new List<Medicines>();

}
