using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using authentication_management.database.models;
using Microsoft.IdentityModel.Tokens;

namespace authentication_management.apis.services;

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

    ci.AddClaim(new Claim("id", user.StaffUniqueId.ToString()));
    ci.AddClaim(new Claim("Username", user.Username));
    ci.AddClaim(new Claim("Designation", user.Department));
    foreach (var role in user.Roles)
    {
      ci.AddClaim(new Claim("roles", role));
    }

    return ci;
  }
}