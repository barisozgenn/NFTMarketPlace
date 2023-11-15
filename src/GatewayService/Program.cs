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
// Now, we've not needed to do this so far because we're not our client browser is not making a connection to any of our API servers on the back end.
// our browsers are going to need to make a connection to our SignalR servers and that means it's a cross origin request 
// and that means we need to send back a header so that our browser allows that request to complete.
builder.Services.AddCors(options => 
{
    //we added in appSettings.json
    options.AddPolicy("customBarisPolicy", b => 
    {
        //and also we added ClientApp in appSetting.json "ClientApp": "http://localhost:3000"
        //because NextJS app running on this url
        b.AllowAnyHeader()
            .AllowAnyMethod().AllowCredentials().WithOrigins(builder.Configuration["ClientApp"]);
    });
});

// Building the web application.
var app = builder.Build();
//now our app will be able to see our cors policies
app.UseCors();

// Mapping the Reverse Proxy middleware.
app.MapReverseProxy();
app.UseAuthentication();//If we don't do this, we cannot authenticate, it should be before UseAuthorization
app.UseAuthorization();
// Running the application.
app.Run();
