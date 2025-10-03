using KantarStore.Infrastructure.Extensions;
using KantarStore.Infrastructure.Seeders;
using KantarStore.Application.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true; // Optional: for better readability
    });
var app = builder.Build();
var scope = app.Services.CreateScope();

var seedProducts = scope.ServiceProvider.GetRequiredService<IProductSeeder>();
await seedProducts.Seed();

var seedVouchers = scope.ServiceProvider.GetRequiredService<IVoucherSeeder>();
await seedVouchers.Seed();

var seedUsers = scope.ServiceProvider.GetRequiredService<IUserSeeder>();
await seedUsers.Seed();

var seedBaskets = scope.ServiceProvider.GetRequiredService<IBasketSeeder>();
await seedBaskets.Seed();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
