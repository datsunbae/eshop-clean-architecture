using CleanArchitecture.Api.Configurations;
using CleanArchitecture.Application;
using CleanArchitecture.Persistence;
using CleanArchitecture.Identity;
using CleanArchitecture.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddConfigurations();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration) ;
builder.Services.AddInfrastureIdentity(builder.Configuration);
builder.Services.AddInfrastructurePersistence(builder.Configuration);

var app = builder.Build();

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
