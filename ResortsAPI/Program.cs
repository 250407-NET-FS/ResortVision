using ResortsAPI.DTOs;
using ResortsAPI.Models;
using ResortsAPI.Repositories;
using ResortsAPI.Services;

// Here is our builder
var builder = WebApplication.CreateBuilder(args);

//==== Dependency Injection Area ====

//Repos
builder.Services.AddSingleton<ICustomerRepository, JsonCustomerRepository>();
builder.Services.AddSingleton<IBookingRepository, JsonBookingRepository>();
builder.Services.AddSingleton<IResortRepository, JsonResortRepository>();

//Services
builder.Services.AddSingleton<IBookingService, BookingService>();
// possibly more services

//Adding swagger to my dependencies
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "ResortsAPI";
    config.Title = "ResortsAPI";
    config.Version = "v1";
});

// Here the builder takes all of our DI and middleware stuff and creates our app.
var app = builder.Build();

//Telling the app to use swagger, pulling it from the DI container in ASP.NET
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "ResortsAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

// -Create Customer

//  -Create Resort

//  -Customer can become member of Resort

//  -Customer can add to balance

//  -Customer can get booking history

//  -Customer can get Resort memberships

//  -Resorts can retrieve Members

//  -Resorts can retrieve Bookings

//  -Resorts can update Price

//  -Customer can get Resort Perks

//  -Customer can get Resort Price

//  -Customer can book a Resort

//  -Customer can delete Resort Memberiship

app.Run();
