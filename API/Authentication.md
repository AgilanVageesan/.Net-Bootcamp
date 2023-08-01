# Implementing JWT Authentication and Authorization for the Banking API

JSON Web Token (JWT) authentication and authorization are essential aspects of securing an API. In this updated implementation, we'll integrate JWT authentication, as well as authentication and authorization filters, to ensure that only authenticated and authorized users can access specific endpoints of the Banking API.

## Step 1: Install Required NuGet Packages

Install the `System.IdentityModel.Tokens.Jwt` NuGet package, as well as the `Microsoft.AspNetCore.Authentication.JwtBearer` NuGet package, in your ASP.NET Core project.

```bash
dotnet add package System.IdentityModel.Tokens.Jwt
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

## Step 2: Configure JWT Authentication

In the `ConfigureServices` method of the `Startup.cs` file, add the JWT authentication configuration using the `JwtBearerDefaults.AuthenticationScheme`:

```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public class Startup
{
    // Other configurations...

    public void ConfigureServices(IServiceCollection services)
    {
        // Configure JWT authentication
        var tokenKey = "your_secret_key_here";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key
            };
        });

        // Other service configurations...
    }

    // Other methods...
}
```

## Step 3: Implement Authentication and Authorization Filters

Next, we'll implement custom authentication and authorization filters to apply authentication and authorization logic to specific API endpoints.

### Authentication Filter Example:

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthenticationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Check if the user is authenticated
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
```

### Authorization Filter Example:

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthorizationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Check if the user is authorized to access the endpoint based on roles or other criteria
        // For example, check the user's role from the JWT token
        if (!context.HttpContext.User.IsInRole("Admin"))
        {
            context.Result = new ForbidResult();
        }
    }
}
```

## Step 4: Apply Authentication and Authorization Filters

Apply the authentication and authorization filters to specific API endpoints using the `[ServiceFilter]` attribute.

### Example Endpoint Protected with Authentication Filter:

```csharp
[HttpGet("secured")]
[ServiceFilter(typeof(AuthenticationFilter))]
public IActionResult SecuredEndpoint()
{
    // Endpoint logic accessible only to authenticated users
    // ...
}
```

### Example Endpoint Protected with Authorization Filter:

```csharp
[HttpGet("admin")]
[ServiceFilter(typeof(AuthorizationFilter))]
public IActionResult AdminEndpoint()
{
    // Endpoint logic accessible only to users with the "Admin" role
    // ...
}
```

## Real-Time Layman Example of JWT with Authentication and Authorization

Imagine a company with different office areas that require access control. The company issues access cards (JWT tokens) to employees. Each access card contains the employee's name (claim) and the type of access they have (role), such as "Employee" or "Manager." When an employee wants to enter a restricted office area (protected endpoint), they must show their access card (JWT token) to the security personnel (authentication filter). The security personnel verify the card's validity (authentication) and check if the employee's access level matches the area they want to enter (authorization). If everything checks out, the employee is granted access; otherwise, they are denied access. The access card also has an expiration time, ensuring that employees cannot use an expired card to access restricted areas.
