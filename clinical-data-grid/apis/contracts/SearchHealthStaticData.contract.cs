namespace clinical_data_grid.database.models;

using System.ComponentModel.DataAnnotations; // For validation attributes
using clinical_data_grid.contracts.customValidations;

[AtLeastOneRequired(nameof(Disease), nameof(Composition), nameof(MedicineName), nameof(Uses), nameof(SideEffects))]
public record SearchHealthStaticData
{

  [MaxLength(255, ErrorMessage = "Disease name cannot exceed 255 characters.")]
  public string? Disease { get; set; } = string.Empty;

  [MaxLength(255, ErrorMessage = "composition name cannot exceed 255 characters.")]
  public string? Composition { get; set; } = string.Empty;

  [MaxLength(255, ErrorMessage = "Medicine name cannot exceed 255 characters.")]
  public string? MedicineName { get; set; } = string.Empty;

  [CustomKeyValidationAttribute]
  public string? Uses { get; set; } = string.Empty;

  [MaxLength(255, ErrorMessage = "SideEffects names cannot exceed 255 characters.")]
  public string? SideEffects { get; set; } = string.Empty;
}
