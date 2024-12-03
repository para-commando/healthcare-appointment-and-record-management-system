
namespace clinical_data_grid.extensions;

public static class SwaggerExtensions
{
  public static WebApplicationBuilder AddSwaggerGenCustExt(this WebApplicationBuilder builder)
  {
    builder.Services.AddSwaggerGen(options =>
  {
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
      Title = "My API",
      Version = "v1",
      Description = "API for managing resources.",
      Contact = new Microsoft.OpenApi.Models.OpenApiContact
      {
        Name = "Ghatak Commando",
      }
    });

    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
      Title = "My API",
      Version = "v2",
      Description = "API for managing resources.",
      Contact = new Microsoft.OpenApi.Models.OpenApiContact
      {
        Name = "Para Commando",
      }
    });
  });
    return builder;
  }
  public static WebApplication UseSwaggerCustExt(this WebApplication app)
  {
    app.UseSwagger(options =>
    {
      options.RouteTemplate = "swagger/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(c =>
       {
         c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
         c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API v2");
         c.RoutePrefix = string.Empty; // Swagger at root
       });
    return app;
  }
}
