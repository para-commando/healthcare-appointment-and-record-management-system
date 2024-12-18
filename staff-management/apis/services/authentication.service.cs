using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using staff_management.database.models;
using Microsoft.IdentityModel.Tokens;

namespace staff_management.apis.services;

public class AuthService
{
  private readonly IConfiguration _config;
  private IConfiguration _configuration;


  public AuthService(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public string Create(User user)
  {
    string ConnectionString = _configuration["jwtVariables:privateKey"];
    Console.WriteLine(ConnectionString);
    var handler = new JwtSecurityTokenHandler();

    var privateKey = Encoding.ASCII.GetBytes(ConnectionString); // Replace with your private key. Ensure it's a valid Base64 encoded string.

    var credentials = new SigningCredentials(
        new SymmetricSecurityKey(privateKey),
        SecurityAlgorithms.HmacSha256);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      SigningCredentials = credentials,
      Expires = DateTime.UtcNow.AddHours(1),
      Subject = GenerateClaims(user)
    };

    var token = handler.CreateToken(tokenDescriptor);
    return handler.WriteToken(token);
  }
  private static ClaimsIdentity GenerateClaims(User user)
  {
    var ci = new ClaimsIdentity();

    ci.AddClaim(new Claim("id", user.Id.ToString()));
    ci.AddClaim(new Claim(ClaimTypes.Name, user.Username));
    ci.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
    // if added as "email" then "emailaddress" will be assigned by default
    ci.AddClaim(new Claim("officeMailId", user.Email));
    ci.AddClaim(new Claim("department", user.Department));

    foreach (var role in user.Roles)
      ci.AddClaim(new Claim(ClaimTypes.Role, role));

    return ci;
  }
}