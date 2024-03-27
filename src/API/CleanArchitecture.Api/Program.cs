using CleanArchitecture.Api.Configurations;
using CleanArchitecture.Api.Extensions;
using CleanArchitecture.Api.OpenApi;
using CleanArchitecture.Application;
using CleanArchitecture.Identity;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();

builder.AddConfigurations();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
builder.Services.AddApiServices();
builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration) ;
builder.Services.AddInfrastureIdentity(builder.Configuration);
builder.Services.AddInfrastructurePersistence(builder.Configuration);

var app = builder.Build();

//Seeding
await app.Services.InitializeDatabasesAsync();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwaggerExtension();
//}

app.UseSwaggerExtension();

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseInfrastructure(builder.Configuration);
app.UseApiApplication();
app.UseInfrastructureIndentity();

app.Services.AddOutBoxJob(builder.Configuration);

app.Run();
