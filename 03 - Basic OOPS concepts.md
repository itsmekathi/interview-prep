Object-Oriented Programming (OOP) is a programming paradigm based on the concept of "objects," which can contain data (fields/properties) and behavior (methods). C# fully supports OOP principles, which are encapsulation, inheritance, polymorphism, and abstraction. Let’s explore each concept with detailed explanations and examples.

---

### 1. **Encapsulation**

Encapsulation is the bundling of data and methods that operate on the data within a single unit (class). It also restricts direct access to some of the object's components to ensure controlled interaction.

#### Example:

```csharp
public class BankAccount
{
    private decimal balance; // Private field

    // Public method to access the private field
    public void Deposit(decimal amount)
    {
        if (amount > 0)
        {
            balance += amount;
        }
    }

    public decimal GetBalance()
    {
        return balance; // Controlled access to balance
    }
}

class Program
{
    static void Main()
    {
        BankAccount account = new BankAccount();
        account.Deposit(1000);
        Console.WriteLine("Balance: " + account.GetBalance()); // Output: Balance: 1000
    }
}
```

**Key Points:**

- `balance` is private, so it can't be directly modified from outside.
- Access is controlled via public methods (`Deposit` and `GetBalance`).

---

### 2. **Inheritance**

Inheritance allows a class (child/derived class) to inherit fields and methods from another class (parent/base class), promoting code reuse.

#### Example:

```csharp
// Base class
public class Vehicle
{
    public int Wheels { get; set; }
    public void StartEngine()
    {
        Console.WriteLine("Engine started.");
    }
}

// Derived class
public class Car : Vehicle
{
    public string Brand { get; set; }
}

class Program
{
    static void Main()
    {
        Car myCar = new Car();
        myCar.Wheels = 4; // Inherited property
        myCar.Brand = "Toyota"; // Property specific to Car
        myCar.StartEngine(); // Inherited method
        Console.WriteLine($"Car brand: {myCar.Brand}, Wheels: {myCar.Wheels}");
    }
}
```

**Key Points:**

- `Car` inherits properties and methods of `Vehicle`.
- Derived class can add its own properties and methods.

---

### 3. **Polymorphism**

Polymorphism allows objects to be treated as instances of their parent class, enabling one interface to be used for a general class of actions.

#### Example (Method Overriding):

```csharp
public class Animal
{
    public virtual void Speak()
    {
        Console.WriteLine("Animal makes a sound.");
    }
}

public class Dog : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Dog barks.");
    }
}

class Program
{
    static void Main()
    {
        Animal myAnimal = new Dog(); // Polymorphism
        myAnimal.Speak(); // Output: Dog barks.
    }
}
```

**Key Points:**

- `virtual` and `override` enable method overriding.
- The `myAnimal` object behaves as a `Dog` at runtime.

#### Example (Method Overloading):

```csharp
public class Calculator
{
    public int Add(int a, int b) => a + b;
    public double Add(double a, double b) => a + b;
}

class Program
{
    static void Main()
    {
        Calculator calc = new Calculator();
        Console.WriteLine(calc.Add(2, 3));       // Output: 5
        Console.WriteLine(calc.Add(2.5, 3.1)); // Output: 5.6
    }
}
```

---

### 4. **Abstraction**

Abstraction hides complex implementation details and shows only the essential features of an object.

#### Example (Using Abstract Classes):

```csharp
public abstract class Shape
{
    public abstract double CalculateArea();
}

public class Circle : Shape
{
    public double Radius { get; set; }
    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
}

class Program
{
    static void Main()
    {
        Shape shape = new Circle { Radius = 5 };
        Console.WriteLine($"Circle area: {shape.CalculateArea()}"); // Output: Circle area: 78.53981633974483
    }
}
```

#### Example (Using Interfaces):

```csharp
public interface IShape
{
    double CalculateArea();
}

public class Rectangle : IShape
{
    public double Length { get; set; }
    public double Width { get; set; }
    public double CalculateArea()
    {
        return Length * Width;
    }
}

class Program
{
    static void Main()
    {
        IShape rectangle = new Rectangle { Length = 4, Width = 5 };
        Console.WriteLine($"Rectangle area: {rectangle.CalculateArea()}"); // Output: Rectangle area: 20
    }
}
```

**Key Points:**

- Abstract classes provide a common base and can include both abstract and non-abstract methods.
- Interfaces define a contract that implementing classes must fulfill.

---

### Summary Table of OOP Principles

| Principle     | Description                                             | Key Feature                         |
| ------------- | ------------------------------------------------------- | ----------------------------------- |
| Encapsulation | Restricts access to internal details of a class.        | Private fields with public methods. |
| Inheritance   | Allows one class to inherit members of another.         | `Base` and `Derived` classes.       |
| Polymorphism  | Enables methods to behave differently based on context. | Method overriding/overloading.      |
| Abstraction   | Hides implementation details, showing only essentials.  | Abstract classes or interfaces.     |

Would you like to dive deeper into any of these principles?
