using System.ComponentModel.DataAnnotations; // For validation attributes
using System.ComponentModel.DataAnnotations.Schema; // For database-specific annotations

namespace clinical_data_grid.database.models;
[Table("symptoms")]
public class Symptoms
{
  [Key]
  [Column("id")]
  public int id { get; set; }

  [Required] // Ensures this property cannot be null
  [MaxLength(255)] // Limits the length of the name
  [Column("name")]
  public required string name { get; set; }

  public ICollection<Diseases> Diseases { get; set; } = new List<Diseases>();
}
