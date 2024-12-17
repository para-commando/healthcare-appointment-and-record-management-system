namespace appointment_details.apis.contracts;
using System.ComponentModel.DataAnnotations; // For validation attributes

public record AppointmentDetailsContract
{
  [Required(ErrorMessage = "PatientId field is required.")]
  public int PatientId { get; init; }

  [Required(ErrorMessage = "DoctorId field is required.")]
  public int DoctorId { get; init; }

  [Required(ErrorMessage = "AppointmentDate field is required.")]
  public DateOnly AppointmentDate { get; init; }

  [Required(ErrorMessage = "AppointmentTimeOfDay is required.")]
  [RegularExpression("^(Morning|Noon|Evening|Night)$", ErrorMessage = "Gender must be 'Morning', 'Noon', 'Evening' or 'Night'")]
  public string AppointmentTimeOfDay { get; init; }

  [Required(ErrorMessage = "AppointmentBookedOn is required.")]
  public DateTime AppointmentBookedOn { get; init; }

  [Required(ErrorMessage = "AppointmentStatus is required.")]
  [RegularExpression("^(Scheduled|Cancelled|PatientAbsent|DoctorOnLeave|Completed|DoctorArrivingLate|PatientArrivingLate)$", ErrorMessage = "Gender must be 'Scheduled', 'Cancelled', 'PatientAbsent', 'DoctorOnLeave','Completed','DoctorArrivingLate' or 'PatientArrivingLate'")]
  public string AppointmentStatus { get; init; }

}
