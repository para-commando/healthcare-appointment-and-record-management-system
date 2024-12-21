
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using authentication_management.database.contracts;
using authentication_management.database.models;
using System.Security.Cryptography;

namespace authentication_management.apis.extensions;

public static class ContractsExtensions
{
  private static bool ValidateEncryptedData(string valueToValidate, string valueFromDatabase)
  {
    string[] arrValues = valueFromDatabase.Split(':');
    string encryptedDbValue = arrValues[0];
    string salt = arrValues[1];
    byte[] saltedValue = Encoding.UTF8.GetBytes(salt + valueToValidate);
    using var hashstr = SHA256.Create();
    byte[] hash = hashstr.ComputeHash(saltedValue);
    string enteredValueToValidate = Convert.ToBase64String(hash);
    return encryptedDbValue.Equals(enteredValueToValidate);
  }
  private static string EncryptData(string valueToEncrypt)
  {
    string GenerateSalt()
    {
      byte[] salt = new byte[32];
      using (var crypto = RandomNumberGenerator.Create())
      {
        crypto.GetBytes(salt);
      }
      return Convert.ToBase64String(salt);
    }
    string EncryptValue(string strvalue)
    {
      string saltValue = GenerateSalt();
      byte[] saltedPassword = Encoding.UTF8.GetBytes(saltValue + strvalue);
      using var hashstr = SHA256.Create();
      byte[] hash = hashstr.ComputeHash(saltedPassword);
      return $"{Convert.ToBase64String(hash)}:{saltValue}";
    }
    return EncryptValue(valueToEncrypt);
  }
  public static Staff ReturnAnStaffsObject(this RegisterUser registerUser)
  {
    return new Staff
    {
      StaffUniqueId = registerUser.StaffUniqueId,
      Email = registerUser.Email,
      DateOfBirth = registerUser.DateOfBirth,
      Address = registerUser.Address,
      UniqueId = registerUser.NationalUniqueId,
      Contact = registerUser.Contact,
      DateOfJoining = registerUser.DateOfJoining,
      Designation = registerUser.Designation,
      Specialization = registerUser.Specialization
    };
  }


  public static Authentication ReturnAnAuthenticationObject(this RegisterUser registerUser)
  {
    return new Authentication
    {
      UserName = registerUser.UserName,
      Password = EncryptData(registerUser.Password),
      IsActive = registerUser.IsActive,
      Roles = registerUser.Roles
    };
  }
}