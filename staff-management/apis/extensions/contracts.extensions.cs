
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using staff_management.database.contracts;
using staff_management.database.models;

namespace staff_management.apis.extensions;

public static class ContractsExtensions
{
  public static PatientDetails ReturnAnEntityObject(this CreatePatientDetailsValidation patientDetails)
  {
    return new PatientDetails
    {
      PatientName = patientDetails.PatientName,
      PatientAddress = patientDetails.PatientAddress,
      PatientContact = patientDetails?.PatientContact != null
               ? string.Join(", ", patientDetails.PatientContact)
               : string.Empty,
      PatientUniqueId = patientDetails?.PatientUniqueId ?? string.Empty,
      PatientRegistrationDate = patientDetails.PatientRegistrationDate,
      PatientLatestDateOfVisit = patientDetails.PatientLatestDateOfVisit,
    };
  }

}
