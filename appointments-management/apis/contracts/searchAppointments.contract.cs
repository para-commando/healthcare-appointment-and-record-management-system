namespace appointments_management.database.contracts;

using System.ComponentModel.DataAnnotations; // For validation attributes

public record SearchAppointments
{


  public int? PatientId { get; init; }


  public int? DoctorId { get; init; }

  public DateOnly? AppointmentDateLessThan { get; init; }

  public DateOnly? AppointmentDateGreaterThan { get; init; }

  [RegularExpression("^(Morning|Noon|Evening|Night)$", ErrorMessage = "AppointmentTimeOfDay must be 'Morning', 'Noon', 'Evening' or 'Night'")]
  public string AppointmentTimeOfDay { get; set; } = string.Empty;

  public DateTime? AppointmentBookedOn { get; set; }

  public DateOnly? AppointmentDate { get; set; }
  
  public DateTime? AppointmentBookedOnGreaterThan { get; init; }

  public DateTime? AppointmentBookedOnLessThan { get; init; }

  [RegularExpression("^(Scheduled|Cancelled|PatientAbsent|DoctorOnLeave|Completed|DoctorArrivingLate|PatientArrivingLate)$",
         ErrorMessage = "AppointmentStatus must be 'Scheduled', 'Cancelled', 'PatientAbsent', 'DoctorOnLeave', 'Completed', 'DoctorArrivingLate', or 'PatientArrivingLate'")]
  public string AppointmentStatus { get; set; } = string.Empty;

  public AppointmentBookedOnRegRange? AppointmentBookedOnRange { get; init; }

  public AppointmentDateRange? AppointmentDateRange { get; init; }

  public int? Limit { get; init; }

  public string? SortBy { get; init; }
}


public record AppointmentBookedOnRegRange
{
  public DateTime? StartDate { get; set; }
  public DateTime? EndDate { get; set; }
}

public record AppointmentDateRange
{

  public DateOnly? StartDate { get; set; }
  public DateOnly? EndDate { get; set; }
}