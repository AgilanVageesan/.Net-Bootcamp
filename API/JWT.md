# Implementing JWT Authentication and Authorization in .NET Core 3.1 API

In this guide, we will implement JWT (JSON Web Token) based authentication and authorization in a .NET Core 3.1 API. We will use authentication and authorization filters to secure the endpoints. JWT is a secure and widely used method for implementing authentication in web applications.

## Step 1: Set up the Project

Create a new .NET Core 3.1 Web API project using the following command:

```bash
dotnet new webapi -n YourProjectName
cd YourProjectName
```

## Step 2: Install Required Packages

We will use the `Microsoft.AspNetCore.Authentication.JwtBearer` package to enable JWT authentication and authorization in our API. Run the following command to install the package:

```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 3.1.0
```

## Step 3: Configure JWT Authentication

In the `Startup.cs` file, configure JWT authentication in the `ConfigureServices` method:

```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public void ConfigureServices(IServiceCollection services)
{
    // ... Other configurations ...

    // Configure JWT Authentication
    var jwtSecretKey = "yourSecretKey"; // Replace with your secret key
    var tokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "yourIssuer", // Replace with your issuer
        ValidAudience = "yourAudience", // Replace with your audience
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
    };

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = tokenValidationParameters;
    });

    // ... Other configurations ...
}
```

Replace `yourSecretKey`, `yourIssuer`, and `yourAudience` with appropriate values. These values are essential for generating and validating JWT tokens.

## Step 4: Create the Authenticate Controller

Create a new controller named `AuthenticateController` to handle user authentication. In this example, we'll use a simple username/password authentication mechanism, but in a real-world scenario, you should use a more secure authentication method like Identity or OAuth.

```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthenticateController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel loginModel)
    {
        // Replace this with your actual user authentication logic
        if (IsValidUser(loginModel.Username, loginModel.Password))
        {
            var token = GenerateJwtToken(loginModel.Username);
            return Ok(new { token });
        }

        return Unauthorized();
    }

    private bool IsValidUser(string username, string password)
    {
        // Replace this with your actual user authentication logic
        // For simplicity, we'll assume the user is valid if the username and password are both "test"
        return username == "test" && password == "test";
    }

    private string GenerateJwtToken(string username)
    {
        var jwtSecretKey = "yourSecretKey"; // Same secret key used in ConfigureServices

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            // Add additional claims here as needed (e.g., roles, permissions)
        };

        var token = new JwtSecurityToken(
            issuer: "yourIssuer", // Same issuer used in ConfigureServices
            audience: "yourAudience", // Same audience used in ConfigureServices
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1), // Token expiration time
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)), SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}
```

In this example, the `Login` method handles the authentication process by validating the provided username and password. If the user is valid, it generates a JWT token using the `GenerateJwtToken` method and returns it to the client.

## Step 5: Apply Authentication and Authorization Filters

Now, let's secure our endpoints using authentication and authorization filters. In the `Startup.cs` file, inside the `Configure` method, add the following lines:

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // ... Other configurations ...

    app.UseRouting();

    // Enable authentication and authorization
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
```

With these lines, we are enabling authentication and authorization for our API.

## Step 6: Protect the Endpoints

To protect specific endpoints, you can use the `[Authorize]` attribute on the controllers or action methods.

```csharp
[Route("api/[controller]")]
[ApiController]
public class SampleController : ControllerBase
{
    [HttpGet]
    [Authorize] // Require authentication to access this endpoint
    public IActionResult Get()
    {
        // Your logic here
        return Ok("Authenticated and Authorized!");
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")] // Require authentication and the "Admin" role to access this endpoint
    public IActionResult GetAdmin()
    {
        // Your logic here
        return Ok("Authenticated and Authorized as Admin!");
    }
}
```

In this example, the `Get` method can be accessed by any authenticated user, while the `GetAdmin` method requires the user to have the "Admin" role in their JWT token.

That's it! Now you have a .NET Core 3.1 API with JWT-based authentication and authorization. Remember to use proper security practices, such as storing sensitive information securely and implementing a robust user authentication mechanism, when deploying this in a production environment.
