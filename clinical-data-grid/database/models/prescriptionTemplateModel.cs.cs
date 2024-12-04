using System.ComponentModel.DataAnnotations; // For validation attributes
using System.ComponentModel.DataAnnotations.Schema; // For database-specific annotations

namespace database.models;
[Table("prescription_template")]
public class PrescriptionTemplate
{
  [Key]
  [Column("id")]
  public int id { get; set; }

  [Required] // Ensures this property cannot be null
  [MaxLength(255)] // Limits the length of the name
  [Column("name")]
  public required string name { get; set; }

}
