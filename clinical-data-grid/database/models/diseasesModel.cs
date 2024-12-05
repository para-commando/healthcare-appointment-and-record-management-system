namespace clinical_data_grid.database.models;
using System.ComponentModel.DataAnnotations; // For validation attributes
using System.ComponentModel.DataAnnotations.Schema; // For database-specific annotations

[Table("diseases")]
public class Diseases
{
  [Key]
  [Column("id")]
  public int id { get; set; }

  [Required] // Ensures this property cannot be null
  [MaxLength(255)] // Limits the length of the name
  [Column("name")]
  public required string name { get; set; }


  public ICollection<Symptoms> Symptoms { get; set; } = new List<Symptoms>();

  public ICollection<Medicines> Medicines { get; set; } = new List<Medicines>();
}
