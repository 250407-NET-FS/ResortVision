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
builder.Services.AddSingleton<ICustomerService, CustomerService>();
builder.Services.AddSingleton<IBookingService, BookingService>();
builder.Services.AddSingleton<IResortService, ResortService>();

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
app.MapPost(
    "/customer",
    (PostCustomerDTO customerDTO, ICustomerService service) =>
    {
        try
        {
            var createdCustomer = service.CreateCustomer(customerDTO);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 400);
        }
    }
);

//  -Create Resort
app.MapPost(
    "/resort",
    (PostResortDTO resortDTO, IResortService service) =>
    {
        try
        {
            var createdResort = service.CreateResort(resortDTO);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 400);
        }
    }
);

//  -Customer can become member of Resort
app.MapPut(
    "/member",
    (ResortMemberDTO request, ICustomerService service) =>
    {
        try
        {
            return Results.Ok(service.AddMember(request));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
);

//  -Customer can add to balance
app.MapPut(
    "/customer/balance",
    (AddToCustomerBalanceDTO request, ICustomerService service) =>
    {
        try
        {
            return Results.Ok(service.AddToCustomerBalance(request));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
);

// //  -Customer can get booking history
// app.MapGet(
//     "/customer/booking",
//     (string email, IBookingService service) =>
//     {
//         try
//         {
//             return Results.Ok(service.GetCustomerBooking());
//         }
//         catch (Exception ex)
//         {
//             return Results.Problem(ex.Message);
//         }
//     }
// );

// //  -Customer can get Resort memberships
// app.MapGet(
//     "/customer/member",
//     (string email, IBookingService service) =>
//     {
//         try
//         {
//             return Results.Ok(service.GetCustomerMemberships());
//         }
//         catch (Exception ex)
//         {
//             return Results.Problem(ex.Message);
//         }
//     }
// );

// //  -Resorts can retrieve Members
// app.MapGet(
//     "/Resort/member",
//     (string email, IBookingService service) =>
//     {
//         try
//         {
//             return Results.Ok(service.GetResortMemberships());
//         }
//         catch (Exception ex)
//         {
//             return Results.Problem(ex.Message);
//         }
//     }
// );

// //  -Resorts can retrieve Bookings
// app.MapGet(
//     "/Resort/booking",
//     (string email, IBookingService service) =>
//     {
//         try
//         {
//             return Results.Ok(service.GetResortBookings());
//         }
//         catch (Exception ex)
//         {
//             return Results.Problem(ex.Message);
//         }
//     }
// );

// //  -Resorts can update Price
// app.MapPut(
//     "/resort/price",
//     (UpdateResortPriceDTO request, IBookingService service) =>
//     {
//         try
//         {
//             return Results.Ok(service.UpdateResortPrice(request));
//         }
//         catch (Exception ex)
//         {
//             return Results.Problem(ex.Message);
//         }
//     }
// );

// //  -Customer can get Resort Perks
// app.MapGet(
//     "/Resort/perk",
//     (string email, IBookingService service) =>
//     {
//         try
//         {
//             return Results.Ok(service.GetResortPerks());
//         }
//         catch (Exception ex)
//         {
//             return Results.Problem(ex.Message);
//         }
//     }
// );

// //  -Customer can get Resort Price
// app.MapGet(
//     "/Resort/price",
//     (string email, IBookingService service) =>
//     {
//         try
//         {
//             return Results.Ok(service.GetResortPrice());
//         }
//         catch (Exception ex)
//         {
//             return Results.Problem(ex.Message);
//         }
//     }
// );

// //  -Customer can book a Resort
// app.MapPost(
//     "/customer/book",
//     (ResortMemberDTO request, IBookingService service) =>
//     {
//         try
//         {
//             return Results.Ok(service.Book(request));
//         }
//         catch (Exception ex)
//         {
//             return Results.Problem(ex.Message);
//         }
//     }
// );

// //  -Customer can delete Resort Memberiship
// app.MapDelete(
//     "/customer/member",
//     (ResortMemberDTO request, IBookingService service) =>
//     {
//         try
//         {
//             return Results.Ok(service.DeleteMembership(request));
//         }
//         catch (Exception ex)
//         {
//             return Results.Problem(ex.Message);
//         }
//     }
// );

app.Run();
