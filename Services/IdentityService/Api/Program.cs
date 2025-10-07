using Microsoft.EntityFrameworkCore;
using IdentityService.Api;
using IdentityService.Dal.EF;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connectionString = builder.Configuration.GetValue<string>("DefaultConnection");
builder.Services.AddDbContext<DBContext>(opt =>
{
    opt.UseNpgsql(connectionString);
},
ServiceLifetime.Scoped);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
ConfigureService.Configure(builder.Services);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
