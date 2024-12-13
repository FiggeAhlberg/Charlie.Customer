using Charlie.Customer.API;
using Charlie.Customer.DataAccess;
using Charlie.Customer.DataAccess.Repositories;
using Charlie.Customer.RMQ;
using Charlie.Customer.Shared.Mappers;
using Charlie.Customer.Shared.Models;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("CustomerDb");

builder.Services.AddDbContext<CustomerDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddSingleton<RabbitMqClient>();

builder.Services.AddScoped<ICustomerRepository<CustomerModel>, CustomerRepository>();
builder.Services.AddSingleton<CustomerMapper>();

builder.Services.AddHostedService<Worker>();



var host = builder.Build();
await host.RunAsync();
