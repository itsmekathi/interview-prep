The **SOLID principles** guide software development to create maintainable, scalable, and testable applications. Below are the principles with detailed explanations and examples in **C# .NET Core**:

---

### 1. **Single Responsibility Principle (SRP)**

#### **Definition**:

A class should have **only one reason to change**, meaning it should have only one responsibility.

#### **Example**:

Imagine a class that handles both user operations and email notifications:

**Bad Design** (Violates SRP):

```csharp
public class UserManager {
    public void CreateUser(string name) {
        // Code to create user
    }
    public void SendEmail(string email, string message) {
        // Code to send email
    }
}
```

**Good Design** (Adheres to SRP):
Split responsibilities into separate classes:

```csharp
public class UserManager {
    private readonly EmailService _emailService;
    public UserManager(EmailService emailService) {
        _emailService = emailService;
    }
    public void CreateUser(string name) {
        // Code to create user
        _emailService.SendEmail("admin@example.com", "New user created.");
    }
}

public class EmailService {
    public void SendEmail(string email, string message) {
        // Code to send email
    }
}
```

Now, if email logic changes, it affects only the `EmailService`.

---

### 2. **Open/Closed Principle (OCP)**

#### **Definition**:

Software entities (classes, methods, modules) should be **open for extension but closed for modification**.

#### **Example**:

Consider a payment processing system that handles credit card payments. Later, you need to add support for PayPal.

**Bad Design** (Violates OCP):

```csharp
public class PaymentProcessor {
    public void ProcessPayment(string paymentType) {
        if (paymentType == "CreditCard") {
            // Credit card payment logic
        } else if (paymentType == "PayPal") {
            // PayPal payment logic
        }
    }
}
```

Adding a new payment method requires modifying the existing class.

**Good Design** (Adheres to OCP):
Use polymorphism to extend functionality:

```csharp
public interface IPayment {
    void Process();
}

public class CreditCardPayment : IPayment {
    public void Process() {
        // Credit card payment logic
    }
}

public class PayPalPayment : IPayment {
    public void Process() {
        // PayPal payment logic
    }
}

public class PaymentProcessor {
    private readonly IPayment _payment;
    public PaymentProcessor(IPayment payment) {
        _payment = payment;
    }
    public void ProcessPayment() {
        _payment.Process();
    }
}
```

Now, adding a new payment method (e.g., Google Pay) only requires creating a new implementation of `IPayment`.

---

### 3. **Liskov Substitution Principle (LSP)**

#### **Definition**:

Subtypes must be substitutable for their base types without altering the behavior of the program.

#### **Example**:

Imagine a base class `Bird` and derived classes for different bird types.

**Bad Design** (Violates LSP):

```csharp
public class Bird {
    public virtual void Fly() {
        Console.WriteLine("Flying...");
    }
}

public class Ostrich : Bird {
    public override void Fly() {
        throw new NotImplementedException("Ostriches can't fly!");
    }
}
```

An `Ostrich` cannot fly, violating the expected behavior of the `Bird` base class.

**Good Design** (Adheres to LSP):
Redesign the hierarchy to separate flying and non-flying birds:

```csharp
public abstract class Bird {
    public abstract void MakeSound();
}

public class FlyingBird : Bird {
    public void Fly() {
        Console.WriteLine("Flying...");
    }
}

public class Ostrich : Bird {
    public override void MakeSound() {
        Console.WriteLine("Hoot!");
    }
}
```

---

### 4. **Interface Segregation Principle (ISP)**

#### **Definition**:

A class should not be forced to implement interfaces it does not use.

#### **Example**:

Consider a `Printer` interface with too many responsibilities.

**Bad Design** (Violates ISP):

```csharp
public interface IPrinter {
    void Print();
    void Scan();
    void Fax();
}

public class BasicPrinter : IPrinter {
    public void Print() {
        // Print logic
    }
    public void Scan() {
        throw new NotImplementedException();
    }
    public void Fax() {
        throw new NotImplementedException();
    }
}
```

**Good Design** (Adheres to ISP):
Split the interface into smaller, specific ones:

```csharp
public interface IPrinter {
    void Print();
}

public interface IScanner {
    void Scan();
}

public class BasicPrinter : IPrinter {
    public void Print() {
        // Print logic
    }
}
```

---

### 5. **Dependency Inversion Principle (DIP)**

#### **Definition**:

High-level modules should not depend on low-level modules. Both should depend on abstractions.

#### **Example**:

Consider a class directly using a concrete implementation.

**Bad Design** (Violates DIP):

```csharp
public class NotificationService {
    private EmailService _emailService = new EmailService();
    public void Notify(string message) {
        _emailService.Send(message);
    }
}
```

**Good Design** (Adheres to DIP):
Use abstractions for dependency injection:

```csharp
public interface INotification {
    void Send(string message);
}

public class EmailNotification : INotification {
    public void Send(string message) {
        Console.WriteLine($"Email sent: {message}");
    }
}

public class NotificationService {
    private readonly INotification _notification;
    public NotificationService(INotification notification) {
        _notification = notification;
    }
    public void Notify(string message) {
        _notification.Send(message);
    }
}
```

**Usage**:

```csharp
var emailNotification = new EmailNotification();
var notificationService = new NotificationService(emailNotification);
notificationService.Notify("Hello, World!");
```

---

### Summary Table

| Principle | Key Idea                                      | Good Design Example                              |
| --------- | --------------------------------------------- | ------------------------------------------------ |
| SRP       | One class, one responsibility                 | Separate user management and email logic         |
| OCP       | Open for extension, closed for modification   | Add new payment methods using interfaces         |
| LSP       | Subtypes must be substitutable for base types | Separate flying and non-flying birds             |
| ISP       | Split large interfaces into smaller ones      | Separate printing and scanning capabilities      |
| DIP       | Depend on abstractions, not concrete classes  | Inject `INotification` instead of `EmailService` |

---

These principles ensure **modular**, **maintainable**, and **scalable** software design in **.NET Core** applications. Let me know if you'd like more examples or real-world use cases!
