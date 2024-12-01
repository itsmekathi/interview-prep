Delegates and events are core concepts in C# used to enable **callback mechanisms**, **decoupling**, and **event-driven programming**.

---

## **Delegates**

A **delegate** is a type that represents references to methods with a specific signature. In simpler terms, a delegate is like a pointer to a function. It allows you to pass methods as arguments and invoke them dynamically.

### **Key Features of Delegates**:

1. They define the method's signature they can reference (return type and parameters).
2. They can point to both static and instance methods.
3. They are type-safe.

---

### **Creating and Using Delegates**

#### 1. **Declare a Delegate**:

```csharp
public delegate void MyDelegate(string message);
```

#### 2. **Assign a Method to a Delegate**:

```csharp
void PrintMessage(string message)
{
    Console.WriteLine(message);
}

// Assigning the method
MyDelegate del = PrintMessage;

// Invoking the method
del("Hello, Delegates!");
```

#### 3. **Multicast Delegates**:

Delegates can point to multiple methods.

```csharp
void PrintUppercase(string message)
{
    Console.WriteLine(message.ToUpper());
}

void PrintLowercase(string message)
{
    Console.WriteLine(message.ToLower());
}

// Multicast delegate
del += PrintUppercase;
del += PrintLowercase;

// Invoking all methods
del("Hello, Multicast Delegates!");
```

---

### **Real-life Use Case of Delegates**

#### Callback Mechanism:

```csharp
public delegate void Notify(string message);

public class Process
{
    public void Execute(Notify callback)
    {
        Console.WriteLine("Process started...");
        // Notify when done
        callback("Process completed successfully!");
    }
}

class Program
{
    static void Main()
    {
        Process process = new Process();
        process.Execute(Console.WriteLine); // Using Console.WriteLine as a callback
    }
}
```

---

## **Events**

An **event** is a specialized delegate that is used for signaling. It allows an object to notify other objects when something happens. Events are commonly used in **publish-subscribe patterns**.

### **Key Features of Events**:

1. An event is always associated with a delegate.
2. Only the class that declares an event can invoke it; subscribers can only subscribe or unsubscribe.
3. Ensures encapsulation for notification mechanisms.

---

### **Creating and Using Events**

#### 1. **Declare an Event**:

```csharp
public delegate void Notify(string message);
public class Publisher
{
    public event Notify OnProcessCompleted; // Declaring an event
}
```

#### 2. **Raise and Handle Events**:

```csharp
public class Publisher
{
    public event Notify OnProcessCompleted;

    public void StartProcess()
    {
        Console.WriteLine("Process started...");
        // Raise the event
        OnProcessCompleted?.Invoke("Process completed!");
    }
}

public class Subscriber
{
    public void OnProcessCompletedHandler(string message)
    {
        Console.WriteLine($"Subscriber received: {message}");
    }
}

class Program
{
    static void Main()
    {
        Publisher publisher = new Publisher();
        Subscriber subscriber = new Subscriber();

        // Subscribing to the event
        publisher.OnProcessCompleted += subscriber.OnProcessCompletedHandler;

        // Start the process
        publisher.StartProcess();
    }
}
```

---

### **Real-life Use Case of Events**

#### Button Click in a GUI Application:

```csharp
public class Button
{
    public event Action OnClick;

    public void Click()
    {
        Console.WriteLine("Button clicked!");
        OnClick?.Invoke(); // Notify all subscribers
    }
}

public class Program
{
    static void Main()
    {
        Button button = new Button();

        // Subscribing to the OnClick event
        button.OnClick += () => Console.WriteLine("Event handler: Button was clicked!");

        button.Click();
    }
}
```

---

## **Comparison: Delegates vs Events**

| Feature            | Delegate                          | Event                                     |
| ------------------ | --------------------------------- | ----------------------------------------- |
| **Purpose**        | Represents a reference to methods | Encapsulation for notification            |
| **Invocation**     | Can be invoked directly           | Can only be invoked by the owning class   |
| **Subscribers**    | Multiple methods can be assigned  | Multiple subscribers via `+=`             |
| **Access Control** | Less restrictive                  | More restrictive for better encapsulation |

---

## **Summary**

- **Delegates** are type-safe method references, enabling callback mechanisms.
- **Events** encapsulate delegate invocation to provide a structured, secure way for objects to signal changes or actions.
- Together, they empower **event-driven programming** in C#.
