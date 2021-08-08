# Multi-Tenant Customer Management System

Customer management system that provides service for multiple tenants. Each tenant has its own database and databases can be from different providers (e.g MSSQL, Oracle, PostgreSQL). 

## Architecture

There are 2 separate service injection systems, one is the default ASP.NET Core service provider and other one is Autofac.Multitenant service provider. Common and shared services are registered with ASP.NET Core provider and Tenant specific services are registered with multi-tenant provider. For example for scoring customer create, every tenant shares the same interface but have different implementations injected.

```
// For Common Services
public void ConfigureServices(IServiceCollection services)

// For Multi-Tenant Services
public static void ConfigureMultiTenantServices(Tenant tenant, ContainerBuilder services)
```

Domain layer holds the business entities for each tenant. Every tenant uses `BaseEntity` as base model and implements tenant specific properties on top of it. There could also be a `BaseCustomer` but I didn't want to couple tenant entities together. 

Infrastructure layer holds data contexts for every tenant and they are injected per tenant. 

Services layer holds the business logic for each tenant. Common services reside in CustomerManagementSystem.Services and each tenant also have their own project for tenant specific logic.

Tenant specific controllers reside in their own assemblies and common controllers are in Api assembly.

CustomerManagementSystem.Multitenancy provides us with the multi-tenant support. It provides couple key middlewares to provide custom dependency injection container and tenant context.

While configuring the host multi-tenant service provider factory should be set up to use multi-tenant service provider 
```cs
var host = Host
    .CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new MultiTenantServiceProviderFactory<Tenant>(Startup.ConfigureMultiTenantServices))
    .ConfigureWebHostDefaults(webHostBuilder => webHostBuilder.UseStartup<Startup>())
    .Build();

await host.RunAsync();
```

Each tenant has its own validation system. Company A validates customers with Mernis validation service (SOAP Api that validates info validity). Company B uses OTP to validate GSM number and Company C uses email validation. There are only mock implementations for B and C but with correct token generation they could be implemented with ease.

## API

To make a request `TenantName` must be provided in url. If no tenant name is provided, app only registers default services and controllers.

Example request:
```
[GET] http://localhost:5000/company-a/customers
```


### Endpoints

| Action | Description | Endpoint  |
| ------ | ------ | --------  |
| GET    | Get All Customers  |  ```/{tenantName}/customers``` |
| GET    | Get Customer By Id |  ```/{tenantName}/customers/{id}``` |
| POST   | Create Customer    |  ```/{tenantName}/customers``` |
| PUT    | Update Customer    |  ```/{tenantName}/customers/{id}``` |
| DELETE | Delete Customer    |  ```/{tenantName}/customers/{id}``` |
| POST   | Validate User (For Tenant B and C)    |  ```/{tenantName}/validate``` |

### Models
```cs
    // Company A Customer Model
    public class CustomerModel
    {
        public int Id { get; set; }

        [Required]
        public long TCKN { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public DateTime? Birthdate { get; set; }
    }

    // Company B Customer Model
    public class CustomerModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
                      
        [Required]    
        public string Name { get; set; }
                      
        [Required]    
        public string Surname { get; set; }
                      
        [Required]    
        public string GSM { get; set; }

        [Required]
        public Education Education { get; set; }
    }

    // Company C Customer Model
    public class CustomerModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [EmailAddress, Required]
        public string Email { get; set; }

        [Required]
        public Education Education { get; set; }
    }

    public class GSMValidationModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string GSM { get; set; }
    }

    public class EmailValidationModel
    {
        [Required]
        public string Token { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
    }
```