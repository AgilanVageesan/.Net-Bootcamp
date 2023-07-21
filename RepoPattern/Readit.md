# Banking API Console Application

This is a simple console application representing a Banking API with CRUD (Create, Read, Update, Delete) operations for bank accounts. The application utilizes the repository pattern to separate data access from business logic. It also incorporates interfaces and dependency injection for improved modularity and testability.

## Project Structure

The project is organized into different components to promote code separation and maintainability:

### Entities

- `BankAccount.cs`: The `BankAccount` entity class represents a bank account with properties like account number, account holder name, balance, activity status, and account type. This class serves as the data model for bank accounts throughout the application.

### Interfaces

- `IBankingRepository.cs`: The `IBankingRepository` interface defines the contract for the repository layer. It declares methods for CRUD operations on `BankAccount` entities, such as adding, updating, deleting, and retrieving accounts.
- `IBankingService.cs`: The `IBankingService` interface defines the contract for the service layer. It outlines methods responsible for handling business logic related to bank accounts, such as creating accounts, retrieving account details, updating account information, and closing accounts.

### Repositories

- `InMemoryBankingRepository.cs`: The `InMemoryBankingRepository` class implements the `IBankingRepository` interface. It provides in-memory storage for bank accounts using a list, simulating data persistence without an actual database. The class handles data manipulation and storage operations for the bank accounts.

### Services

- `BankingService.cs`: The `BankingService` class implements the `IBankingService` interface. It acts as an intermediary between the application and the repository. The class contains methods that handle the business logic for bank account operations, such as validating input data, processing account information, and invoking repository methods for data storage.

### Console Application

- `Program.cs`: The `Program` class contains the main method for the console application. When executed, it presents a menu to users, offering various options for interacting with the Banking API and performing CRUD operations on bank accounts. It processes user input, invokes the appropriate service methods, and displays messages based on the outcome of operations.

## Key Concepts

### Interfaces

Interfaces in C# provide a contract that defines a set of methods and properties without any implementation details. They allow classes to adhere to a common set of behaviors without being tied to a specific implementation. In this project, `IBankingRepository` and `IBankingService` serve as interfaces for the repository and service layers, respectively. By using interfaces, the code achieves better separation of concerns, enabling easy replacement of implementations and unit testing.

### Dependency Injection

Dependency injection (DI) is a design pattern that promotes loose coupling between classes by injecting dependencies (objects or services) into a class instead of the class creating its dependencies directly. In this project, the `BankingService` class relies on an `IBankingRepository` implementation, which is provided to it via constructor injection. This allows the application to swap the actual repository implementation (e.g., in-memory or SQLite) without modifying the `BankingService` class, making the code more flexible and maintainable.

## Getting Started

To interact with the Banking API, run the console application. It will prompt you with a menu for different operations:

1. Create Bank Account: Create a new bank account by entering account details.
2. Get All Bank Accounts: Fetch and display all existing bank accounts.
3. Get Bank Account by ID: Retrieve a specific bank account using its ID.
4. Update Bank Account: Update an existing bank account's holder name.
5. Close Bank Account: Close an existing bank account by ID.
6. Exit: Terminate the application.

