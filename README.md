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
	1) KantarStore.Infrastructure (EF Core, External Services)
	
		Implements external concerns:
		Database (EF Core or Dapper)
		Depends on the Application and Domain layers (internal ones), not the other way around.

	2) KantarStore.Domain  (Entities & Business Rules)
	
		Contains business models and rules.
		Pure C# classes with no external dependencies.
	
	3) KantarStore.Application (Use Cases, Interfaces)
	
		Defines application logic, between the UI and Domain.
	
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

2) The domain layer contains all the lean c# domain entities, such as: Product, Basket, BasteItem,Voucher, User, etc, ammong other. This layer contains the business logig to calculate the basket totals, the line prices, and discounts.
2.1) The voucher entity is a solid approach to the discounts applyed to each product. So a voucher is associated into a voucher and it can be of different types:
	PercentageDiscountOnDifferentProduct = 1,
	PercentageDiscountOnSameProduct = 2,
	MultiBuyPercentageDiscountDifferentProduct = 3,
	MultiBuyPercentageDiscountSameProduct = 4,
	MultiBuyOfferSameProduct = 5,
	MultiBuyOfferDiffentProduct = 6

2.2) The Voucher entity and db table contains all the fields and properties to handle of these types of vouchers. Because of time contrastraints, I have implemented just the 2 and 3 type ( the ones required for the exercise)
2.3) Any product as only one voucher associated. 
2.4) Any time a product is added to a basket, each basket item is recalculated, because it can affect an existing product price on the basket, and vice-versa.
2.5) The calculation of multibuys takes in consideration the fracions, so if we add 4 cans of soup we can have 2 loafs of bread with 50% of discount.

3) The applictation layer act as an orchestrator between the UI and the domain and infrastructur layer. It maps the DTo objects from the UI into Domain object using Automapper profiles, and orcestrates the calls for the repositories.

4) UI layer is represented for our Web API: https://kantarstore-e6gqgphjc6ghf5fd.uksouth-01.azurewebsites.net/swagger/index.html. This layer exposes a Rest API set of services to be consumed by the react app. Please note that on the basket service, I explicitly decided to call /KantarStore/api/Baskets/{userid} instead of /KantarStore/api/Baskets/{basketid}, because I have implemented the assumption that one user a single basket open. So if we pass the user, we get the basket. This removes the need to call an endpoint to the the users basket first then pass the current basket to the api to get the basket itself.

5) I have created the most important unit tests to test the calculations:
	RecalculateTotals_NoVouchers_SumsUnitPriceTimesQuantity
	RecalculateTotals_DiscountOnSameProduct
	RecalculateTotals_MultiBuyPercentageDiscountDifferentProduct

5.1) Did not have the time to finish the ui tests using selenium.

7) 



