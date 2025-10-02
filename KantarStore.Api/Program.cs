using KantarStore.Infrastructure.Extensions;
using KantarStore.Infrastructure.Seeders;
using KantarStore.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();
var scope = app.Services.CreateScope();

var seedProducts = scope.ServiceProvider.GetRequiredService<IProductSeeder>();
await seedProducts.Seed();

var seedVouchers = scope.ServiceProvider.GetRequiredService<IVoucherSeeder>();
await seedVouchers.Seed();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
