# API Versioning in ASP.NET Core Project

In this documentation, we will explain how API versioning is implemented in an ASP.NET Core project. API versioning allows you to manage multiple versions of your APIs concurrently, providing flexibility and backward compatibility for clients consuming the API.

## Implementing API Versioning

To implement API versioning in an ASP.NET Core project, we use the `Microsoft.AspNetCore.Mvc.Versioning` package. Here's how to do it:

1. Install the `Microsoft.AspNetCore.Mvc.Versioning` NuGet package in your ASP.NET Core project.

2. Add versioning configuration in the `ConfigureServices` method of the `Startup.cs` file.

3. Decorate your API controllers with the `[ApiVersion]` attribute to specify the version for each controller or action method.

4. Use the `[ApiVersion]` attribute to define the versioning scheme, such as specifying the version number or using versioning via query parameters or headers.

## Example Implementation

### Step 1: Install NuGet Package

Install the `Microsoft.AspNetCore.Mvc.Versioning` NuGet package using the Package Manager Console or the .NET CLI:

```bash
dotnet add package Microsoft.AspNetCore.Mvc.Versioning
```

### Step 2: Configure Versioning in `Startup.cs`

In the `ConfigureServices` method of the `Startup.cs` file, add the following configuration to enable API versioning:

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            // Specify the default API version
            options.DefaultApiVersion = new ApiVersion(1, 0);

            // Specify the API versioning scheme (e.g., version by URL segment, query parameter, or header)
            options.ApiVersionReader = new UrlSegmentApiVersionReader();

            // Specify the supported API versions
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        // Other service configurations...
    }

    // Other methods...
}
```

### Step 3: Decorate Controllers with `[ApiVersion]` Attribute

In your API controllers, decorate the controller class or action methods with the `[ApiVersion]` attribute to specify the supported versions:

```csharp
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v{version:apiVersion}/bankaccounts")]
[ApiVersion("1.0")]
public class BankAccountsV1Controller : ControllerBase
{
    // Controller actions for version 1.0...
}

[ApiController]
[Route("api/v{version:apiVersion}/bankaccounts")]
[ApiVersion("2.0")]
public class BankAccountsV2Controller : ControllerBase
{
    // Controller actions for version 2.0...
}
```

### Step 4: Test the API Versions

With the above implementation, you can now access different versions of the API using the specified versioning scheme. For example:

- Version 1.0: `/api/v1.0/bankaccounts`
- Version 2.0: `/api/v2.0/bankaccounts`

## Benefits of API Versioning

API versioning provides several benefits, especially in real-world use cases:

1. **Backward Compatibility**: With API versioning, you can make changes and updates to your API without breaking existing client applications. Clients can continue using the older version of the API until they are ready to migrate to the latest version.

2. **API Evolution**: As your application evolves, new features and improvements can be introduced in newer versions, while older versions remain stable and operational.

3. **Client-Specific Versions**: Different clients may require different features or have varying levels of support for API changes. API versioning allows you to cater to each client's needs with appropriate versions.

4. **Gradual Upgrades**: By allowing multiple versions, clients can choose when to upgrade to the latest version based on their own timelines and requirements.

5. **API Lifecycle Management**: API versioning facilitates the management of the API's lifecycle, including deprecation and sunset policies.

Overall, API versioning provides a flexible approach to managing APIs, enabling smooth and controlled transitions while supporting the evolving needs of clients and the application itself. It ensures a seamless experience for developers and users as the API evolves over time.
