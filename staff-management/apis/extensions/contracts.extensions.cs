
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using staff_management.database.contracts;
using staff_management.database.models;

namespace staff_management.apis.extensions;

public static class ContractsExtensions
{

  public static DoctorDetails ReturnAnEntityObject(this CreateDoctorDetailsValidation doctorDetailsValidation)
  {
    return new DoctorDetails
    {
      DoctorName = doctorDetailsValidation.DoctorName,
      DoctorAddress = doctorDetailsValidation.DoctorAddress,
      DoctorSpecialization = doctorDetailsValidation.DoctorSpecialization,
      DoctorContact = doctorDetailsValidation.DoctorContact,
      DoctorUniqueId = doctorDetailsValidation.DoctorUniqueId,
      DoctorDateOfJoining = doctorDetailsValidation.DoctorDateOfJoining,
    };
  }
}
