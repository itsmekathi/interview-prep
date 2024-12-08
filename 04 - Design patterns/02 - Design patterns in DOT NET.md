C# and the .NET Core framework come with built-in support and implementations of several commonly used design patterns, which help developers create robust, reusable, and maintainable code. Here's a summary of some of these patterns:

---

### **1. Singleton Pattern**

Ensures that a class has only one instance and provides a global point of access to it.

**Built-In Support in .NET Core:**

- The `AddSingleton` method in ASP.NET Core's Dependency Injection (DI) framework:
  ```csharp
  services.AddSingleton<IMyService, MyService>();
  ```
  - Ensures the same instance of `MyService` is used throughout the application.

**Use Case:**

- Logging, configuration management, or caching where only one instance is needed globally.

---

### **2. Factory Pattern**

Defines an interface for creating objects but lets subclasses decide which class to instantiate.

**Built-In Support in .NET Core:**

- `IServiceProvider` and the DI framework allow factories to resolve dependencies dynamically:
  ```csharp
  services.AddTransient<IMyService>(provider => new MyService("param"));
  ```
- The `Task.Factory` class is an example of a factory for creating tasks:
  ```csharp
  Task task = Task.Factory.StartNew(() => Console.WriteLine("Task running"));
  ```

**Use Case:**

- Creating objects dynamically based on runtime conditions.

---

### **3. Builder Pattern**

Separates the construction of a complex object from its representation so that the same construction process can create different representations.

**Built-In Support in .NET Core:**

- `StringBuilder`:
  ```csharp
  var builder = new StringBuilder();
  builder.Append("Hello ").Append("World!");
  string result = builder.ToString();
  ```
- `ConfigurationBuilder` in ASP.NET Core:
  ```csharp
  var configuration = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json")
      .Build();
  ```

**Use Case:**

- Building complex objects like query strings, JSON objects, or configuration settings.

---

### **4. Adapter Pattern**

Allows incompatible interfaces to work together by providing a wrapper that translates calls from one interface to another.

**Built-In Support in .NET Core:**

- `Stream` classes, such as `StreamReader` and `StreamWriter`, act as adapters between a stream and its textual representation.
- Example of wrapping:
  ```csharp
  var fileStream = new FileStream("file.txt", FileMode.Open);
  var reader = new StreamReader(fileStream);
  ```

**Use Case:**

- Integrating third-party libraries or legacy systems.

---

### **5. Decorator Pattern**

Attaches additional responsibilities to an object dynamically without modifying its structure.

**Built-In Support in .NET Core:**

- Middleware in ASP.NET Core is a practical example:
  ```csharp
  app.Use(async (context, next) =>
  {
      // Pre-processing
      await next.Invoke();
      // Post-processing
  });
  ```
- Streams in .NET also use this pattern:
  ```csharp
  var gzipStream = new GZipStream(new FileStream("file.txt", FileMode.Open), CompressionMode.Compress);
  ```

**Use Case:**

- Adding cross-cutting concerns like logging, authentication, or caching.

---

### **6. Observer Pattern**

Defines a dependency between objects so that when one object changes state, all its dependents are notified.

**Built-In Support in .NET Core:**

- `IObservable<T>` and `IObserver<T>` interfaces in the `System` namespace:
  ```csharp
  public class TemperatureMonitor : IObservable<float>
  {
      // Implementation
  }
  ```
- Event handlers and `event` keyword in C# are also based on this pattern:
  ```csharp
  public event EventHandler SomethingChanged;
  ```

**Use Case:**

- Implementing event-driven architectures, notifications, or real-time updates.

---

### **7. Strategy Pattern**

Defines a family of algorithms, encapsulates each one, and makes them interchangeable.

**Built-In Support in .NET Core:**

- DI allows easy swapping of implementations at runtime:
  ```csharp
  services.AddScoped<ICompressionStrategy, GzipCompression>();
  ```
- LINQ's `OrderBy` uses a strategy by allowing a custom comparison delegate:
  ```csharp
  var sortedList = myList.OrderBy(x => x.Name);
  ```

**Use Case:**

- Switching between different algorithms or business logic without modifying the client code.

---

### **8. Proxy Pattern**

Provides a surrogate or placeholder for another object to control access to it.

**Built-In Support in .NET Core:**

- `HttpClient` acts as a proxy for making HTTP requests.
- WCF's service proxies act as client-side stubs for remote services.

**Use Case:**

- Remote method invocation, lazy initialization, or logging access to resources.

---

### **9. Chain of Responsibility Pattern**

Passes a request along a chain of handlers until one handles it.

**Built-In Support in .NET Core:**

- ASP.NET Core middleware pipeline:
  ```csharp
  app.UseAuthentication();
  app.UseAuthorization();
  ```

**Use Case:**

- Building request-processing pipelines, such as in web applications or message handlers.

---

### **10. Dependency Injection (DI)**

A pattern for providing dependencies to classes rather than letting them instantiate dependencies themselves.

**Built-In Support in .NET Core:**

- ASP.NET Core has a built-in DI container:
  ```csharp
  services.AddScoped<IMyService, MyService>();
  ```

**Use Case:**

- Promotes loose coupling and testability.

---

### **11. Template Method Pattern**

Defines the skeleton of an algorithm, letting subclasses override certain steps.

**Built-In Support in .NET Core:**

- Abstract classes with virtual/abstract methods:

  ```csharp
  public abstract class DataExporter
  {
      public void Export()
      {
          LoadData();
          FormatData();
          WriteData();
      }

      protected abstract void LoadData();
      protected abstract void FormatData();
      protected abstract void WriteData();
  }
  ```

**Use Case:**

- Implementing workflows with varying steps in subclasses.

---

These patterns demonstrate how the .NET Core framework encourages clean, maintainable, and extensible designs by embedding these principles directly into its APIs and architecture. Let me know if you’d like a deeper dive into any specific pattern!
