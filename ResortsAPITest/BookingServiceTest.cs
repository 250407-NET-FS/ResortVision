using ResortsAPI.DTOs;
using ResortsAPI.Models;
using ResortsAPI.Repositories;
using ResortsAPI.Services;
using Moq;

namespace ResortsAPI.Tests;

public class BookingServiceTests
{
    //Mock Dependencies: We mock dependencies, in this case, to isolate our service layer.
    //We don't want to write tests that may fail because of code outside of the specific unit we are testing.
    //We can also avoid unwanted secondary effects of our tests being run.

    //In order to test CheckoutService I need to satisfy its dependencies, since we need to construct a
    //CheckoutService object. We are going to mock these dependencies

    private readonly Mock<IBookingRepository> _mockBookingRepo = new();
    private readonly Mock<ICustomerRepository> _mockCustomerRepo = new();
    private readonly Mock<IResortRepository> _mockResortRepo = new();

    //Finally, we create an instance of the object who's code we are going to be testing.
    private readonly BookingService _bookingService;

    // Test data constants: Centralized test data can make tests more maintainable and easier to write.

    private const string ValidFName = "John";
    private const string ValidLName = "Wayne";
    private const string ValidEmail = "test@gmail.com";

    private const string ValidEmail2 = "test2@gmail.com";
    private readonly Customer _validCustomer = new Customer(ValidFName, ValidLName, ValidEmail);
    private readonly Resort _validResort = new Resort("Johns", ValidEmail2, "2.00");

    // Unit testing class constructor
    //We need to initalize it with our CheckoutService, that itself needs our mock dependencies
    //in order to be created
    public BookingServiceTests()
    {
        _bookingService = new BookingService(
            _mockBookingRepo.Object, //Satisfying our CheckoutService class's constructor
            _mockCustomerRepo.Object, // with our mock objects
            _mockResortRepo.Object
        );
    }

    [Theory]
    [InlineData(null, ValidEmail2)]
    [InlineData("", ValidEmail2)]
    [InlineData(ValidEmail, null)]
    [InlineData(ValidEmail, "")]
    public void CreateBooking_InvalidBookingEmail_Request(string? customerEmail, string? resortEmail){
        PostBookingDTO postBookingDTO = new PostBookingDTO {CustomerEmail = customerEmail, ResortEmail = resortEmail};

        Assert.Throws<Exception>(() => _bookingService.CreateBooking(postBookingDTO));
    }

    [Fact]
    public void CreateBooking_Valid(){
        PostBookingDTO postBookingDTO = new PostBookingDTO {CustomerEmail = ValidEmail, ResortEmail = ValidEmail2};
        _validResort.Price = "3.00";
        _validCustomer.Balance = "5.00";

        Booking booking = new Booking(_validCustomer, _validResort, "5.00");
        _mockCustomerRepo.Setup(r => r.Find(ValidEmail)).Returns(_validCustomer);
        _mockResortRepo.Setup(r => r.Find(ValidEmail2)).Returns(_validResort);
        _mockBookingRepo.Setup(r => r.AddBooking(It.IsAny<Booking>())).Returns(booking);

        Booking ex = _bookingService.CreateBooking(postBookingDTO);
        Assert.Equal(ValidEmail, ex.Customer!.Email);
        Assert.Equal(ValidEmail2, ex.Resort!.Email);
    }
}