using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using clinical_data_grid.database.models;
using Microsoft.IdentityModel.Tokens;

namespace clinical_data_grid.apis.services;

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
      Expires = DateTime.UtcNow.AddMilliseconds(100),
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
    ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));

    foreach (var role in user.Roles)
      ci.AddClaim(new Claim(ClaimTypes.Role, role));

    return ci;
  }
}