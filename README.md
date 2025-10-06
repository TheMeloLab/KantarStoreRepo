The API is available in Azure: https://kantarstore-e6gqgphjc6ghf5fd.uksouth-01.azurewebsites.net/swagger/index.html

For the API project structure I have followed the clean architecture pattern:

Diagram:

[ Presentation / API ]
          ↓
     Application (DTos, Services)
          ↓
        Domain (Entities)
          ↑
   Infrastructure (EF, Repositories)

Structure:

	1) KantarStore.Domain  (Entities & Business Rules)
	
		Contains business models and rules.
		Pure C# classes with no external dependencies.
	
	2) KantarStore.Application (Use Cases, Interfaces)
	
		Defines application logic, between the UI and Domain.
	
	3) KantarStore.Infrastructure (EF Core, External Services)
	
		Implements external concerns:
		Database (EF Core or Dapper)
		Depends on the Application and Domain layers (internal ones), not the other way around.
	
	4) KantarStore.Presentation  (ASP.NET Core API)
	
		ASP.NET Core Web API 
		Minimal logic; delegates work to the Application Layer.

Solution Overview 

This sections describes my notes, comments and assumptions for the solution:

1) With regards db infrastructure, I have followed a code first approach using Entity Framework. The migrations are on the migrations folder, and where deployd to a Azure SQL database
1.1) All the dbcontext reflect the domain structure objects for the entity creation
1.2) I have creates multiple seeders to populate the products, vouchers, baskets and users
1.3) On the infratructure implemented two different repositories one for managing the products, other to manage the basket operations
1.4) To avoid any break on the clean pattern module, this repositories and services are exposed via extension method for the service collection extension

2) The domain



