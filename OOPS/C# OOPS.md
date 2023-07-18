# Object-Oriented Programming (OOP) Concepts Explained with Real-Time Examples

Object-Oriented Programming (OOP) is a programming paradigm that revolves around the concept of objects. An object is a self-contained unit that consists of both data and behavior. These objects can interact with each other to perform specific tasks, making OOP a powerful and flexible way to model real-world scenarios in code. Here, we'll explore four fundamental OOP concepts: **Encapsulation, Inheritance, Polymorphism, and Abstraction**.

## 1. Encapsulation

**Encapsulation** is the process of hiding the internal workings of an object and exposing only the necessary information and methods to the outside world. It's like packaging functionality inside a box, where you interact with the box's interface without worrying about what's inside.

**Real-Time Example:**
Let's take the example of a **smartphone**. When you use a smartphone, you don't need to know how its internal components work. You interact with it through its user interface, such as buttons, touch screen, and applications. The smartphone encapsulates its complex internal processes, allowing you to use it without understanding the intricacies of its hardware and software.

**C# .NET Code Example:**
```csharp
public class Smartphone
{
    // Private fields, not accessible from outside
    private string model;
    private int batteryPercentage;

    // Public method to get the model (accessible from outside)
    public string GetModel()
    {
        return model;
    }

    // Public method to update the battery percentage (accessible from outside)
    public void UpdateBattery(int percentage)
    {
        // Perform some validation before updating the battery
        if (percentage >= 0 && percentage <= 100)
        {
            batteryPercentage = percentage;
        }
    }
}
```
## 2. Inheritance

## 1. Single Inheritance

**Single Inheritance** is a type of inheritance where a class can inherit from only one superclass. This is the most common type of inheritance, where a subclass extends a single base class.

**Real-Time Example:**
Let's consider a **Vehicle** superclass with a **Car** subclass. The Car class inherits from the Vehicle class and extends it with specific attributes and methods related to cars.

**C# .NET Code Example:**
```csharp
public class Vehicle
{
    public string Make { get; set; }
    public string Model { get; set; }

    public void StartEngine()
    {
        // Some implementation to start the engine
    }
}

public class Car : Vehicle
{
    public int NumberOfDoors { get; set; }
    public void Drive()
    {
        // Some implementation to drive the car
    }
}
```

## 2. Multiple Inheritance (Not Supported in .NET)

**Multiple Inheritance** is a type of inheritance where a class can inherit from multiple superclasses. However, **multiple inheritance is not directly supported in .NET** (C#) for the following reasons.

**Real-Time Example:**
Consider a scenario where we have a class **Bird** and another class **Mammal**. We might want to create a class called **Bat** that inherits from both Bird and Mammal since a bat is both a bird and a mammal (can fly like a bird and has characteristics of a mammal). But this kind of inheritance leads to the "Diamond Problem."

```csharp
// This code will lead to compilation error due to multiple inheritance
public class Bird
{
    public void Fly()
    {
        // Some implementation for flying
    }
}

public class Mammal
{
    public void Run()
    {
        // Some implementation for running
    }
}

public class Bat : Bird, Mammal
{
    // Implementation
}
```

To avoid the complexities and ambiguity caused by multiple inheritance, C# uses single inheritance, where a class can inherit from only one superclass.

## 3. Multilevel Inheritance

**Multilevel Inheritance** is a type of inheritance where a class derives from another class, which itself is derived from another class.

**Real-Time Example:**
Let's consider a **Animal** superclass with a **Mammal** subclass, and then a **Dog** subclass that inherits from Mammal.

**C# .NET Code Example:**
```csharp
public class Animal
{
    public void Eat()
    {
        // Some implementation for eating
    }
}

public class Mammal : Animal
{
    public void Sleep()
    {
        // Some implementation for sleeping
    }
}

public class Dog : Mammal
{
    public void Bark()
    {
        // Some implementation for barking
    }
}
```

## 4. Hierarchical Inheritance

**Hierarchical Inheritance** is a type of inheritance where multiple subclasses inherit from the same superclass.

**Real-Time Example:**
Let's consider an **Animal** superclass with **Cat** and **Dog** subclasses, both inheriting from the Animal class.

**C# .NET Code Example:**
```csharp
public class Animal
{
    public void Eat()
    {
        // Some implementation for eating
    }
}

public class Cat : Animal
{
    public void Meow()
    {
        // Some implementation for meowing
    }
}

public class Dog : Animal
{
    public void Bark()
    {
        // Some implementation for barking
    }
}
```

## 3. Polymorphism

**Polymorphism** allows objects of different classes to be treated as objects of a common superclass. It enables flexibility by providing a single interface to various implementations.

**Types of Polymorphism:**
There are two types of polymorphism:
1. **Compile-Time Polymorphism (Method Overloading)**: In this type, the decision on which method to call is made at compile-time based on the number or type of arguments.

2. **Run-Time Polymorphism (Method Overriding)**: In this type, the decision on which method to call is made at run-time based on the actual object type.

**Real-Time Example:**
Consider a **Shape** superclass with subclasses like **Circle** and **Rectangle**. All shapes can have an area, and they can calculate it differently. By using polymorphism, we can call a common method `CalculateArea()` on any shape object without worrying about its specific implementation.

**C# .NET Code Example:**
```csharp
public class Shape
{
    public virtual double CalculateArea()
    {
        return 0;
    }
}

public class Circle : Shape
{
    public double Radius { get; set; }

    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
}

public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public override double CalculateArea()
    {
        return Width * Height;
    }
}
```

## 4. Abstraction

**Abstraction** is the process of simplifying complex reality by modeling classes based on relevant attributes and behavior. It allows you to focus on essential features while hiding unnecessary details.

**Real-Time Example:**
Let's consider a **Television**. When you use a TV remote, you interact with abstracted buttons like power, volume, and channel without knowing the inner workings of the TV's electronics.

**C# .NET Code Example:**
```csharp
public abstract class RemoteControl
{
    public abstract void PowerOn();
    public abstract void AdjustVolume(int level);
    public abstract void ChangeChannel(int channel);
}
```

In this example, we create an abstract class `RemoteControl`, defining the basic operations. Specific TV brands would implement this abstract class and provide their implementations for the abstract methods, encapsulating the TV's internal functionality.
