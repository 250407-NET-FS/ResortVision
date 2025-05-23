using ResortsAPI.DTOs;
using ResortsAPI.Models;
using ResortsAPI.Repositories;
using ResortsAPI.Services;
using Moq;

namespace ResortsAPI.Tests;

public class CustomerServiceTests
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
    private readonly CustomerService _customerService;

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
    public CustomerServiceTests()
    {
        _customerService = new CustomerService(
            _mockBookingRepo.Object, //Satisfying our CheckoutService class's constructor
            _mockCustomerRepo.Object, // with our mock objects
            _mockResortRepo.Object
        );
    }

    //We are now ready to test.

    [Theory]
    [InlineData(null, ValidLName, ValidEmail)] // Null fName
    [InlineData(ValidFName, ValidLName, null)] //null email
    [InlineData("", ValidLName, ValidEmail)] //empty First Name
    [InlineData(ValidFName, ValidLName, "")] //empty email string
    public void AddCustomer_InvalidRequest_ThrowsExpection(string? FName, string LName, string? Email)
    {
        //Arrange - Creating a CheckoutRequestDTO we will purposfully populate with bad data
        var invalidRequest = new Customer(FName!, LName, Email!);
        //Act

        //Assert - In this case our assert is also our act. When the method runs, if the request is invalid
        //we are asserting an exception is thrown. If the exception is thrown, the test passes.
        Assert.Throws<Exception>(() => _customerService.AddCustomer(invalidRequest));
    }

    [Fact]
    public void AddMember_MemberValid()
    {
        //Arrange -
        var request = new ResortMemberDTO() { CustomerEmail = ValidEmail, ResortEmail = ValidEmail2 };

        //Setting 
        //Here we define our return when our mockBookRepo calls its mock implementation
        _mockCustomerRepo.Setup(r => r.Find(ValidEmail)).Returns(_validCustomer);
        _mockResortRepo.Setup(r => r.Find(ValidEmail2)).Returns(_validResort);
        //ergo, wrong@mail.com is not a valid member

        //Act & Assert
        bool ex = _customerService.AddMember(request);
        Assert.True(ex); 

        //Verify 
        _mockCustomerRepo.Verify(r => r.Find(ValidEmail), Times.Once);
        _mockCustomerRepo.Verify(r => r.Update(_validCustomer), Times.Once);
        _mockResortRepo.Verify(r => r.Find(ValidEmail2), Times.Once);
        _mockResortRepo.Verify(r => r.Update(_validResort), Times.Once);

        //_mockResortRepo.Verify(r => r.AddCheckout(It.IsAny<List<Checkout>>()), Times.Never);
    }
}