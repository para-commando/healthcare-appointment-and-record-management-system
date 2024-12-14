namespace clinical_data_grid.database.models;

using System.ComponentModel.DataAnnotations; // For validation attributes


public record PrescriptionContent
{
  [Required(ErrorMessage = "DoctorSpecialization field is required.")]
  [MaxLength(255, ErrorMessage = "DoctorSpecialization cannot exceed 255 characters.")]
  public string DoctorSpecialization { get; set; }

  [Required(ErrorMessage = "DoctorName field is required.")]
  [MaxLength(25, ErrorMessage = "DoctorName cannot exceed 25 characters.")]
  public string DoctorName { get; set; }

  // [Required(ErrorMessage = "Date is required.")]
  [MaxLength(25, ErrorMessage = "Date field cannot exceed 25 characters.")]
  public string Date { get; private set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

  [Required(ErrorMessage = "PatientName is required.")]
  [MaxLength(100, ErrorMessage = "PatientName cannot exceed 100 characters.")]
  public string? PatientName { get; set; } = string.Empty;

  [Required(ErrorMessage = "contact number is required.")]
  [MaxLength(15, ErrorMessage = "contact number cannot exceed 15 characters.")]
  public string? ContactNumber { get; set; } = string.Empty;

  [Required(ErrorMessage = "Age is required.")]
  [Range(0, 150, ErrorMessage = "Age must be between 0 and 150 years old")]
  public float? Age { get; set; }

  [Required(ErrorMessage = "Gender is required.")]
  [RegularExpression("^(Male|Female|Other)$", ErrorMessage = "Gender must be 'Male', 'Female', or 'Other'.")]
  public string? Gender { get; set; }


  [Required(ErrorMessage = "Diagnosis is required.")]
  [MaxLength(350, ErrorMessage = "Diagnosis cannot exceed 350 chars.")]
  public string? Diagnosis { get; set; }

  [Required(ErrorMessage = "Medicines is required.")]
  public required List<Medicines> Medicines { get; set; }
}

public record Medicines
{

  [Required(ErrorMessage = "ToCure field is required.")]
  [MaxLength(255, ErrorMessage = "ToCure cannot exceed 255 characters.")]
  public string ToCure { get; set; }

  [Required(ErrorMessage = "Quantity field is required.")]
  [MaxLength(10, ErrorMessage = "Quantity cannot exceed 10 characters.")]
  public string Quantity { get; set; }

  [Required(ErrorMessage = "Medicine name is required.")]
  [MaxLength(50, ErrorMessage = "Medicine name cannot exceed 50 characters.")]
  public string? MedicineName { get; set; } = string.Empty;

  [Required(ErrorMessage = "Duration name is required.")]
  public required Duration Duration { get; set; }

  [Required(ErrorMessage = "Frequency name is required.")]
  public float Frequency { get; set; }

  [Required(ErrorMessage = "TimeOfDay is required.")]
  public List<string>? TimeOfDay { get; set; }


  [Required(ErrorMessage = "FoodTimings is required.")]
  [RegularExpression("^(Before Food|After Food|)$", ErrorMessage = "FoodTimings must be 'Before Food', 'After Food'.")]
  public string? FoodTimings { get; set; } = string.Empty;
}

public record Duration
{
  [Required(ErrorMessage = "DurationFreq is required.")]
  [RegularExpression("^(Days|Months|Weeks|)$", ErrorMessage = "DurationFreq must be 'Days', 'Weeks', or 'Months'.")]
  public string? DurationFreq { get; set; } = string.Empty;

  [Required(ErrorMessage = "Numb field is required.")]
  [Range(1, 1000, ErrorMessage = "Numb field must be between 0 and 1000 years old")]
  public float? Numb { get; set; }
}