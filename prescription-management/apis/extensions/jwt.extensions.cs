
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace clinical_data_grid.apis.extensions;

public static class JwtExtensions
{
  public static WebApplicationBuilder ConfigureJwtAuthenticationCustExt(this WebApplicationBuilder builder)
  {
    // .AddJwtBearer() is required because the authentication scheme must be specified there and if not specified or added then this exception will be thrown " System.InvalidOperationException: No authenticationScheme was specified, and there was no DefaultChallengeScheme found. The default schemes can be set using either AddAuthentication(string defaultScheme) or AddAuthentication(Action<AuthenticationOptions> configureOptions). "
    builder.Services.AddAuthentication(x =>
    {
      x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
      x.TokenValidationParameters = new TokenValidationParameters
      {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bAafd@A7d9#@F4*V!LHZs#ebKQrkE6pad2f3kj34c3dXy@")),
        ValidateIssuer = false,
        ValidateLifetime = true,
        ValidateAudience = false
      };

      x.Events = new JwtBearerEvents
      {
        OnChallenge = context =>
                 {
                   Console.WriteLine("OnChallenge was called");
                   context.HandleResponse(); // Skip default behavior

                   if (!context.Response.HasStarted)
                   {
                     context.Response.StatusCode = 401;
                     context.Response.ContentType = "application/json";

                     var result = new
                     {
                       message = "Access denied. Token is invalid or missing.",
                       error = "Unauthorized"
                     };

                     return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(result));
                   }

                   Console.WriteLine("Response already started in OnChallenge.");
                   return Task.CompletedTask;
                 },
        OnAuthenticationFailed = context =>
       {
         Console.WriteLine("OnAuthenticationFailed was called");

         if (!context.Response.HasStarted)
         {
           context.Response.StatusCode = 500;


           context.Response.ContentType = "application/json";

           // // Write a custom JSON response
           var result = new
           {
             message = "Authentication failed. Please provide a valid token.",
             error = "Unauthorized"
           };

           return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(result));
         }
         return Task.CompletedTask;

       },
        OnMessageReceived = context =>
       {

         Console.WriteLine("OnMessageReceived was called");
         return Task.CompletedTask;

       },
        OnTokenValidated = context =>
       {
         Console.WriteLine("OnTokenValidated was called");
         return Task.CompletedTask;
       }
      };
    });
    return builder;
  }

  public static WebApplicationBuilder ConfigureJwtAuthorizationCustExt(this WebApplicationBuilder builder)
  {

    builder.Services.AddAuthorization(options =>
              {
                options.AddPolicy("alpha-doc", policy =>
            {
              policy.RequireRole("alpha");
              policy.RequireClaim("Designation", "Doctor");

            });
                options.AddPolicy("alpha-sys-admin", policy =>
                {
                  policy.RequireRole("alpha");
                  policy.RequireClaim("Designation", "System Administrator");
                });

              });

    return builder;

  }

}
