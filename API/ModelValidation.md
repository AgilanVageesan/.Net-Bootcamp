# Model Validation with Data Annotations

Model validation with data annotations is a way to apply validation rules directly to the properties of a model or entity. By using data annotations, you can enforce specific validation rules on the incoming data before it is processed by the API. This helps ensure that the data is valid and meets the required criteria, thus preventing invalid or inconsistent data from being saved.

## Types of Validations

In the current Banking API solution, we can apply various types of data annotations for model validation. Here are some commonly used data annotations and their purposes:

1. **Required**: Specifies that a property is required and must have a value. This ensures that the property is not null or empty.

2. **StringLength**: Limits the length of a string property to a specific maximum length.

3. **Range**: Defines the range of valid values for numeric properties.

4. **RegularExpression**: Ensures that a string property matches a specific regular expression pattern.

5. **EmailAddress**: Validates that a string property contains a valid email address.

6. **DataType**: Specifies the data type of the property, such as Date, Time, PhoneNumber, etc.

7. **Compare**: Compares the value of one property with another property in the same model.

8. **Custom Validation**: Allows you to define custom validation rules by creating custom validation attributes.

## Implementation in the Banking API

Let's apply model validation with data annotations to the `BankAccount` entity in the Banking API.

### BankAccount Entity

In the `BankAccount` entity class, we'll use data annotations to apply validation rules to its properties:

```csharp
using System.ComponentModel.DataAnnotations;

public class BankAccount
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Account number is required.")]
    public string AccountNumber { get; set; }

    [Required(ErrorMessage = "Account holder name is required.")]
    [StringLength(100, ErrorMessage = "Account holder name cannot exceed 100 characters.")]
    public string AccountHolderName { get; set; }

    [Required(ErrorMessage = "Balance is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "Balance must be a positive value.")]
    public decimal Balance { get; set; }

    [Required(ErrorMessage = "Account type is required.")]
    [EnumDataType(typeof(AccountType), ErrorMessage = "Invalid account type. Valid account types are: Savings, Checking, Investment.")]
    public AccountType AccountType { get; set; }
}
```

In this implementation, we applied the following data annotations:

- `Required`: Applied to `AccountNumber`, `AccountHolderName`, `Balance`, and `AccountType` properties to ensure they are required and cannot be null.

- `StringLength`: Applied to `AccountHolderName` to limit its maximum length to 100 characters.

- `Range`: Applied to `Balance` to ensure it is a positive value.

## Benefits of Model Validation with Data Annotations

- **Consistent Validation**: By using data annotations, the validation rules are defined directly in the model, making the validation consistent across all parts of the application.

- **Client-Side Validation**: Some data annotations (e.g., `Required`, `StringLength`) can be automatically validated on the client-side, providing immediate feedback to users when submitting forms.

- **Error Messages**: Data annotations allow us to define custom error messages that provide meaningful feedback to users when validation fails.

- **Centralized Validation Logic**: Data annotations keep the validation logic close to the properties being validated, making it easier to maintain and understand.

To create a custom validation attribute for the `AccountNumber` property to ensure that it starts with "IND" and has a maximum length of 10 characters, follow the steps below:

```csharp
using System.ComponentModel.DataAnnotations;

public class CustomAccountNumberAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null || !(value is string accountNumber))
            return false;

        // Check if the account number starts with "IND" and has a maximum length of 10 characters.
        return accountNumber.StartsWith("IND") && accountNumber.Length <= 10;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"The {name} must start with 'IND' and have a maximum length of 10 characters.";
    }
}
```

In this implementation, we created a custom validation attribute `CustomAccountNumberAttribute`, which inherits from `ValidationAttribute`. We override the `IsValid` method and provide our custom validation logic to check if the `AccountNumber` starts with "IND" and has a maximum length of 10 characters. The `FormatErrorMessage` method provides a custom error message when validation fails.

Now, apply this custom validation attribute to the `AccountNumber` property in the `BankAccount` entity:

```csharp
public class BankAccount
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Account number is required.")]
    [CustomAccountNumber(ErrorMessage = "Invalid account number. The account number must start with 'IND' and have a maximum length of 10 characters.")]
    public string AccountNumber { get; set; }

    // Other properties...

}
```

By using the `[CustomAccountNumber]` attribute, we apply the custom validation rule to the `AccountNumber` property. Now, the `AccountNumber` must start with "IND" and have a maximum length of 10 characters for the validation to pass. If the validation fails, the custom error message provided in the `CustomAccountNumberAttribute` will be displayed.
By using data annotations for model validation, the Banking API ensures that the incoming data meets the required criteria, leading to a more robust and reliable application.
