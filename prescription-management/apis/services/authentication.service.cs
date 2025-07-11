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
        try
        {
            string ConnectionString = _configuration["jwtVariables:privateKey"];

            string privateKeyString = _configuration["jwtVariables:privateKey"];
            if (string.IsNullOrEmpty(privateKeyString))
                throw new Exception(
                    "JWT private key is not configured. Please set 'jwtVariables:privateKey' in your appsettings.json or environment variables."
                );

            var handler = new JwtSecurityTokenHandler();

            var privateKey = Encoding.ASCII.GetBytes(ConnectionString); // Replace with your private key. Ensure it's a valid Base64 encoded string.

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(privateKey),
                SecurityAlgorithms.HmacSha256
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(1),
                Subject = GenerateClaims(user),
            };

            var token = handler.CreateToken(tokenDescriptor);
            var ss = handler.WriteToken(token);
            return ss;
        }
        catch (Exception ex)
        {
            // You can log the exception here if you have a logger
            throw new Exception("An error occurred while creating the JWT token.", ex);
        }
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
