// Importing the necessary namespace for JWT Bearer Authentication.
using Microsoft.AspNetCore.Authentication.JwtBearer;

// Creating a new web application builder.
var builder = WebApplication.CreateBuilder(args);

// Adding Reverse Proxy services and configuring them from the specified configuration section.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// Adding JWT Bearer authentication services.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Setting the authority for token validation.
        options.Authority = builder.Configuration["IdentityServerBarisDevUrl"];
        // Disabling HTTPS metadata requirement for development.
        options.RequireHttpsMetadata = false;
        // Disabling audience validation for simplicity.
        options.TokenValidationParameters.ValidateAudience = false;
        // Setting the claim type for the user's name.
        options.TokenValidationParameters.NameClaimType = "username";
    });

// Building the web application.
var app = builder.Build();

// Mapping the Reverse Proxy middleware.
app.MapReverseProxy();
app.UseAuthentication();//If we don't do this, we cannot authenticate, it should be before UseAuthorization
app.UseAuthorization();
// Running the application.
app.Run();
