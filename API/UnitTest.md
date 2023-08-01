# Unit Testing with MOQ, FluentAssertions, and xUnit

## Introduction

Unit testing is a crucial part of software development that ensures individual components (units) of the code behave as expected. It helps identify bugs early, improves code quality, and provides confidence in the application's functionality. In this documentation, we will cover unit testing using three popular libraries:

1. MOQ: A mocking library used to create mock objects for dependencies in unit tests.
2. FluentAssertions: A library that provides a more expressive and readable syntax for writing assertions in unit tests.
3. xUnit: A unit testing framework for .NET that allows writing and executing unit tests.

## MOQ

MOQ is a mocking library for .NET that enables the creation of mock objects for dependencies in unit tests. Mock objects mimic the behavior of real objects but allow you to set up their behavior, making it easier to isolate and test specific units of code.

### Functionalities of MOQ:

1. **Mocking Dependencies**: MOQ allows you to create mock objects for interfaces or classes, which are used as dependencies in your code, enabling you to control their behavior during testing.

2. **Setup Behavior**: With MOQ, you can set up the mock object's behavior, specifying return values for methods or properties, defining callbacks, or throwing exceptions when specific methods are called.

3. **Verification**: MOQ provides methods to verify that certain methods were called on the mock object with specific arguments.

## FluentAssertions

FluentAssertions is an assertion library that enhances the readability and expressiveness of unit test assertions. It provides a more fluent syntax for writing assertions, making the tests easier to read and understand.

### Functionalities of FluentAssertions:

1. **Fluent Syntax**: FluentAssertions allows you to chain multiple assertions together in a fluent manner, improving the readability of test assertions.

2. **Extensive Assertions**: It offers a wide range of built-in assertions for different data types, including collections, strings, numeric types, and more.

3. **Custom Assertions**: FluentAssertions allows you to create custom assertions to meet specific testing requirements.

## xUnit

xUnit is a unit testing framework for .NET that enables writing and executing unit tests. It provides a rich set of features to create and organize test cases, run tests in parallel, and manage test lifecycle.

### Functionalities of xUnit:

1. **Test Cases**: xUnit allows you to create test cases using attributes like `[Fact]`, `[Theory]`, and more, making it easy to define individual test scenarios.

2. **Test Lifecycle Management**: xUnit provides hooks for setting up test data and cleaning up after tests, using `[SetUp]` and `[TearDown]` attributes.

3. **Parameterized Tests**: With `[Theory]` and `[InlineData]` attributes, xUnit supports parameterized tests, enabling you to test the same functionality with different inputs.

## Unit Testing BankingService with MOQ and FluentAssertions

```csharp
using Xunit;
using Moq;
using FluentAssertions;
using System.Collections.Generic;

public class BankingServiceTests
{
    private readonly Mock<IBankingRepository> _mockRepository;
    private readonly IBankingService _bankingService;

    public BankingServiceTests()
    {
        // Set up mock repository and banking service
        _mockRepository = new Mock<IBankingRepository>();
        _bankingService = new BankingService(_mockRepository.Object);
    }

    [Fact]
    public void GetAllBankAccounts_Should_Return_All_Bank_Accounts()
    {
        // Arrange
        var expectedAccounts = new List<BankAccount>
        {
            new BankAccount { Id = 1, AccountNumber = "ACC001", Balance = 1000 },
            new BankAccount { Id = 2, AccountNumber = "ACC002", Balance = 2000 },
            // Add more test data as needed
        };
        _mockRepository.Setup(repo => repo.GetAllBankAccounts()).Returns(expectedAccounts);

        // Act
        var result = _bankingService.GetAllBankAccounts();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(expectedAccounts.Count);
        result.Should().BeEquivalentTo(expectedAccounts);
    }

    [Fact]
    public void GetBankAccountById_Should_Return_Bank_Account_With_Valid_Id()
    {
        // Arrange
        int accountId = 1;
        var expectedAccount = new BankAccount { Id = accountId, AccountNumber = "ACC001", Balance = 1000 };
        _mockRepository.Setup(repo => repo.GetBankAccountById(accountId)).Returns(expectedAccount);

        // Act
        var result = _bankingService.GetBankAccountById(accountId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedAccount);
    }

    [Fact]
    public void GetBankAccountById_Should_Return_Null_With_Invalid_Id()
    {
        // Arrange
        int invalidId = 100;
        _mockRepository.Setup(repo => repo.GetBankAccountById(invalidId)).Returns((BankAccount)null);

        // Act
        var result = _bankingService.GetBankAccountById(invalidId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void CreateAccount_Should_Add_New_Bank_Account()
    {
        // Arrange
        var newAccount = new BankAccount { AccountNumber = "NEW001", Balance = 500 };
        _mockRepository.Setup(repo => repo.AddBankAccount(newAccount)).Verifiable();

        // Act
        _bankingService.CreateAccount(newAccount);

        // Assert
        _mockRepository.Verify();
    }

    [Fact]
    public void UpdateAccount_Should_Update_Existing_Bank_Account()
    {
        // Arrange
        int accountId = 1;
        var updatedAccount = new BankAccount { Id = accountId, AccountNumber = "ACC001", Balance = 1500 };
        _mockRepository.Setup(repo => repo.UpdateBankAccount(updatedAccount)).Verifiable();

        // Act
        _bankingService.UpdateAccount(accountId, updatedAccount);

        // Assert
        _mockRepository.Verify();
    }

    [Fact]
    public void UpdateAccount_Should_Not_Update_With_Invalid_Id()
    {
        // Arrange
        int invalidId = 100;
        var updatedAccount = new BankAccount { Id = invalidId, AccountNumber = "ACC100", Balance = 500 };
        _mockRepository.Setup(repo => repo.UpdateBankAccount(updatedAccount)).Verifiable();

        // Act & Assert
        Assert.Throws<NotFoundException>(() => _bankingService.UpdateAccount(invalidId, updatedAccount));
        _mockRepository.Verify(repo => repo.UpdateBankAccount(updatedAccount), Times.Never);
    }

    [Fact]
    public void CloseAccount_Should_Return_True_With_Valid_Id()
    {
        // Arrange
        int accountId = 1;
        _mockRepository.Setup(repo => repo.DeleteBankAccount(accountId)).Returns(true);

        // Act
        var result = _bankingService.CloseAccount(accountId);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void CloseAccount_Should_Return_False_With_Invalid_Id()
    {
        // Arrange
        int invalidId = 100;
        _mockRepository.Setup(repo => repo.DeleteBankAccount(invalidId)).Returns(false);

        // Act
        var result = _bankingService.CloseAccount(invalidId);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void CalculateInterest_Should_Not_Throw_Exception_With_Valid_Id()
    {
        // Arrange
        int accountId = 1;
        var bankAccount = new BankAccount { Id = accountId, AccountNumber = "ACC001", Balance = 1000 };
        _mockRepository.Setup(repo => repo.GetBankAccountById(accountId)).Returns(bankAccount);

        // Act & Assert
        _bankingService.Invoking(service => service.CalculateInterest(accountId)).Should().NotThrow();
    }

    [Fact]
    public void CalculateInterest_Should_Throw_Exception_With_Invalid_Id()
    {
        // Arrange
        int invalidId = 100;
        _mockRepository.Setup(repo => repo.GetBankAccountById(invalidId)).Returns((BankAccount)null);

        // Act & Assert
        _bankingService.Invoking(service => service.CalculateInterest(invalidId)).Should().Throw<NotFoundException>();
    }
}
```
In the `BankingServiceTests` class, we have demonstrated how to use MOQ to mock the `IBankingRepository` and how to use FluentAssertions to write expressive and readable test assertions. The test cases cover different scenarios for the methods in the `IBankingService` interface, including getting bank accounts, creating and updating accounts, closing accounts, and calculating interest.

By combining MOQ, FluentAssertions, and xUnit, we can create comprehensive and effective unit tests that verify the correctness of the `BankingService` implementation and ensure it behaves as expected in various scenarios. Unit testing is a critical part of the software development process, and using these libraries can significantly improve the efficiency and effectiveness of testing efforts.
