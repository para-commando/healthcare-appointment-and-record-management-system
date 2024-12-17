
using appointment_details.apis.contracts;
using appointment_details.database.models;


namespace patient_management.apis.extensions;

public static class ContractsExtensions
{
  public static AppointmentDetails ReturnAnEntityObject(this AppointmentDetailsContract appointmentDetailsContract)
  {
    return new AppointmentDetails
    {
      PatientId = appointmentDetailsContract.PatientId,
      DoctorId = appointmentDetailsContract.DoctorId,
      AppointmentDate = appointmentDetailsContract.AppointmentDate,
      AppointmentTimeOfDay = appointmentDetailsContract.AppointmentTimeOfDay,
      AppointmentBookedOn = appointmentDetailsContract.AppointmentBookedOn,
      AppointmentStatus = appointmentDetailsContract.AppointmentStatus,
    };
  }

}
