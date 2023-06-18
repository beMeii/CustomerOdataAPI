using CustomerOdataAPI.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EcommerceDbContext>();

var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntityType<Order>();
modelBuilder.EntityType<OrderDetail>();
modelBuilder.EntityType<Product>();
modelBuilder.EntitySet<Customer>("Customers");
modelBuilder.EntitySet<Order>("Orders");
modelBuilder.EntitySet<OrderDetail>("OrderDetail");
modelBuilder.EntitySet<Product>("Products");

// Add services to the container.
builder.Services.AddDbContext<EcommerceDbContext>(options =>
    options.UseSqlServer("Server=DESKTOP-D33LIPC\\MSSQLSERVER02;Database=DemoPRN231_19062023;Trusted_Connection=True;"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).EnableQueryFeatures(2)
    .AddRouteComponents("api",

        modelBuilder.GetEdmModel()));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
