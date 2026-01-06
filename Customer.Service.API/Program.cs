using Customer.Service.API.Data.Models;
using Customer.Service.API.Data.Repository.BaseRepository;
using Customer.Service.API.Data.Repository.BaseRepository.Interface;
using Customer.Service.API.Data.Repository.CustomerRepository;
using Customer.Service.API.Features.Customer.Services;
using Customer.Service.API.Features.Customer.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // required for Swagger

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Customer Business Solutions API",
        Version = "v1",
        Description = "A multi-tenant business management API"
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IRepository<Customer.Service.API.Data.Models.Customer>, Repository< Customer.Service.API.Data.Models.Customer>> ();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Business Solutions API v1");
        options.RoutePrefix = string.Empty; // Swagger opens at root URL
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
