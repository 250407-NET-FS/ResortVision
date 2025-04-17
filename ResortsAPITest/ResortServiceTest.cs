using ResortsAPI.DTOs;
using ResortsAPI.Models;
using ResortsAPI.Repositories;
using ResortsAPI.Services;
using Moq;

namespace ResortsAPI.Tests;

public class ResortServiceTests
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
    private readonly ResortService _resortService;

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
    public ResortServiceTests()
    {
        _resortService = new ResortService(
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

    public void AddResortPerk_InvalidInput_ThrowsException(string? email, string? perk){
        ResortPerkDTO resortPerkDTO = new ResortPerkDTO {Email = email, Perk = perk};

        Assert.Throws<Exception>(() => _resortService.AddResortPerk(resortPerkDTO));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetResortPerks_InvalidInput_ThrowsException(string? email){
        Assert.Throws<Exception>(() => _resortService.GetResortPerks(email!));
    }

    [Fact]
    public void CreateResort_Valid(){
        PostResortDTO postResortDTO = new PostResortDTO {Name = "Johns", Email = ValidEmail2, Price = "10.00"};

        Resort resort = new Resort("Johns", ValidEmail2, "10.00");
        _mockResortRepo.Setup(r => r.AddResort(It.IsAny<Resort>())).Returns(resort);

        Resort ex = _resortService.CreateResort(postResortDTO);
        Assert.Equal(ValidEmail2, ex.Email);
    }
}