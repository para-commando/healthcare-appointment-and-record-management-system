namespace appointment_details.database.models;

using System.ComponentModel.DataAnnotations; // For validation attributes
using System.ComponentModel.DataAnnotations.Schema; // For database-specific annotations

[Table("appointment_details")] // Table name
public class AppointmentDetails
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Primary Key
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("patient_id")]
    public int PatientId { get; set; }

    [Required]
    [Column("doctor_id")]
    public int DoctorId { get; set; }

    [Required]
    [Column("appointment_date", TypeName = "DATE")]
    public DateOnly AppointmentDate { get; set; }

    [Required]
    [Column("appointment_time_of_day")]
    [RegularExpression("^(Morning|Noon|Evening|Night)$", ErrorMessage = "AppointmentTimeOfDay must be 'Morning', 'Noon', 'Evening' or 'Night'")]
    public string AppointmentTimeOfDay { get; set; } = string.Empty;

    [Required]
    [Column("appointment_booked_on")]
    public DateTime AppointmentBookedOn { get; set; }

    [Required]
    [Column("appointment_status")]
    [RegularExpression("^(Scheduled|Cancelled|PatientAbsent|DoctorOnLeave|Completed|DoctorArrivingLate|PatientArrivingLate)$",
        ErrorMessage = "AppointmentStatus must be 'Scheduled', 'Cancelled', 'PatientAbsent', 'DoctorOnLeave', 'Completed', 'DoctorArrivingLate', or 'PatientArrivingLate'")]
    public string AppointmentStatus { get; set; } = string.Empty;
}
