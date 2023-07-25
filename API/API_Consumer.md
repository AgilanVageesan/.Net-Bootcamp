# Banking API - CRUD API with Exception Handling and Logging

In this project, we will create a .NET Core 3.1 CRUD API for a banking system. The API will allow us to perform basic CRUD operations (Create, Read, Update, Delete) on bank accounts. We will follow best coding standards, REST API conventions, and implement common exception handling and logging to make the API more robust and reliable.

## Project Structure

```
BankingAPI
│   BankingAPI.csproj
│   Program.cs
│   Startup.cs
│   README.md
│
└───Controllers
│   └───V1
│       │   BankAccountsController.cs
│
└───Entities
│       BankAccount.cs
│
└───BankingService (Class Library)
│   │   BankingService.csproj
│   └───Interfaces
│   │       IBankingService.cs
│   │
│   └───Services
│           BankingService.cs
│
└───BankingRepository (Class Library)
│   │   BankingRepository.csproj
│   └───Interfaces
│   │       IBankingRepository.cs
│   │
│   └───Repositories
│           InMemoryBankingRepository.cs
│
└───BankingAPI.Tests
    │   BankingAPI.Tests.csproj
    │   BankAccountsControllerTests.cs
```

## Project Overview

- **BankingAPI:** The main ASP.NET Core Web API project.

- **Entities:** Contains the `BankAccount` entity representing a bank account.

- **BankingService:** A class library that contains the business logic for the banking system. It provides an interface for the banking service (`IBankingService`) and its implementation (`BankingService`).

- **BankingRepository:** A class library that handles data access for the banking system. It provides an interface for the repository (`IBankingRepository`) and its implementation (`InMemoryBankingRepository`).

- **Controllers/V1:** Contains the API controller `BankAccountsController` responsible for handling CRUD operations on bank accounts.

- **BankingAPI.Tests:** Contains unit tests for the API controller.

## Step-by-Step Implementation

### Step 1: Create the ASP.NET Core Web API Project

Create a new ASP.NET Core Web API project using the .NET CLI or Visual Studio. In this example, we'll name the project "BankingAPI."

```bash
dotnet new webapi -n BankingAPI
```

### Step 2: Set up Dependencies

Open the `BankingAPI.csproj` file and add references to the class library projects (BankingService and BankingRepository) that contain the core business logic and data access.

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

    <PropertyGroup>
        <TargetFramework>netcoreapp3.1</TargetFramework>
    </PropertyGroup>

    <ItemGroup>
        <ProjectReference Include="..\BankingService\BankingService.csproj" />
        <ProjectReference Include="..\BankingRepository\BankingRepository.csproj" />
    </ItemGroup>

</Project>
```

### Step 3: Implement the API Controllers

Create the API controllers to handle CRUD operations. Use attribute routing for versioning, and follow REST API conventions for response codes.

#### Controllers/V1/BankAccountsController.cs

```csharp
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BankingService; // Add reference to BankingService
using BankingService.Interfaces;

namespace BankingAPI.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/bankaccounts")]
    [ApiVersion("1.0")]
    public class BankAccountsController : ControllerBase
    {
        private readonly IBankingService _bankingService;

        public BankAccountsController(IBankingService bankingService)
        {
            _bankingService = bankingService;
        }

        // GET: api/v1/bankaccounts
        [HttpGet]
        public IActionResult GetAllBankAccounts()
        {
            IEnumerable<BankAccount> bankAccounts = _bankingService.GetAllAccounts();
            return Ok(bankAccounts);
        }

        // GET: api/v1/bankaccounts/{id}
        [HttpGet("{id}")]
        public IActionResult GetBankAccountById(int id)
        {
            BankAccount bankAccount = _bankingService.GetAccountById(id);
            if (bankAccount == null)
            {
                return NotFound();
            }
            return Ok(bankAccount);
        }

        // POST: api/v1/bankaccounts
        [HttpPost]
        public IActionResult CreateBankAccount([FromBody] BankAccount bankAccount)
        {
            if (bankAccount == null)
            {
                return BadRequest("Invalid request body.");
            }

            // Perform additional input validation if required.

            _bankingService.CreateAccount(bankAccount);
            return CreatedAtAction(nameof(GetBankAccountById), new { id = bankAccount.Id }, bankAccount);
        }

        // PUT: api/v1/bankaccounts/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateBankAccount(int id, [FromBody] BankAccount updatedAccount)
        {
            if (updatedAccount == null)
            {
                return BadRequest("Invalid request body.");
            }

            BankAccount existingAccount = _bankingService.GetAccountById(id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            // Perform additional input validation if required.

            existingAccount.AccountHolderName = updatedAccount.AccountHolderName;
            _bankingService.UpdateAccount(existingAccount);
            return Ok(existingAccount);
        }

        // DELETE: api/v1/bankaccounts/{id}
        [HttpDelete("{id}")]
        public IActionResult CloseBankAccount(int id)
        {
            try
            {
                _bankingService.CloseAccount(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
```

### Step 4: Configure API Versioning

In the `Startup.cs` file, add the following code to configure API versioning.

#### Startup.cs

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;

namespace BankingAPI
{
    public class Startup
    {
        // ...

        public void ConfigureServices(IServiceCollection services)
        {
            // ...
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            // Register the BankingService as a scoped service.
            services.AddScoped<IBankingService, BankingService.BankingService>();

            // ...

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Banking API", Version

 = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ...

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Banking API v1");
            });

            app.UseRouting();

            // ...

            // Global exception handling middleware
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500; // Internal Server Error
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        // Log the error
                        Log.Error(error.Error, "Unhandled Exception");

                        // Return a custom error response
                        await context.Response.WriteAsync(new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error"
                        }.ToString());
                    }
                });
            });
        }
    }
}
```

### Step 5: Implement Logging

In the `Program.cs` file, configure Serilog as the logging provider.

#### Program.cs

```csharp
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace BankingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
```
