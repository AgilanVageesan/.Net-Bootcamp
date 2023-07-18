## C# DataTypes 
```csharp
using System;

class Program
{
    static void Main()
    {
        // 1. Integer Types
        int myInt = 42; // 32-bit signed int, range: -2,147,483,648 to 2,147,483,647

        long myLong = 1234567890L; // 64-bit signed long, range: -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807

        byte myByte = 200; // 8-bit unsigned byte, range: 0 to 255

        // 2. Floating-Point Types
        float myFloat = 3.14f; // 32-bit single-precision float

        double myDouble = 3.14159; // 64-bit double-precision float

        // 3. Character Types
        char myChar = 'A'; // 16-bit Unicode character

        // 4. Boolean Type
        bool isTrue = true; // Represents true or false value

        // 5. String Type
        string myString = "Hello, World!"; // Sequence of characters (text)

        // 6. DateTime Type
        DateTime currentDate = DateTime.Now; // Represents date and time

        // 7. Enum Type
        enum DaysOfWeek { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday }
        DaysOfWeek today = DaysOfWeek.Wednesday; // Represents named integral constants

        // 8. Arrays
        int[] myArray = new int[] { 1, 2, 3, 4, 5 }; // Collection of items of the same data type

        // 9. Object Type
        object myObject = 42; // Base type of all other types, can hold any value

        // Real-Time Examples

        // 1. Working with `int` and `float` in a calculation:
        int quantity = 10;
        float pricePerItem = 2.5f;
        float totalCost = quantity * pricePerItem;
        Console.WriteLine("Total Cost: $" + totalCost);

        // 2. Converting user input to `int`:
        Console.Write("Enter your age: ");
        string userInput = Console.ReadLine();
        int age = int.Parse(userInput);
        Console.WriteLine("In five years, you will be " + (age + 5) + " years old.");

        // 3. Using `DateTime` to work with dates:
        DateTime todayDate = DateTime.Today;
        Console.WriteLine("Today is: " + todayDate.ToShortDateString());
        DateTime nextWeek = todayDate.AddDays(7);
        Console.WriteLine("Next week will be: " + nextWeek.ToShortDateString());

        // More Data Types: 
        // - `short` (16-bit signed integer)
        // - `ushort` (16-bit unsigned integer)
        // - `decimal` (128-bit decimal number)
        // - `sbyte` (8-bit signed integer)
        // - `ulong` (64-bit unsigned integer)
        // - `char[]` (character array)
        // - `bool[]` (array of booleans)
    }
}
```

In the updated code, each data type has a more brief explanation, and additional code examples are included. At the end of the code, there's a comment mentioning other data types that are not explained in detail.
