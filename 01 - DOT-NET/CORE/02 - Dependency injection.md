### **Dependency Injection (DI) in .NET Core**

Dependency Injection (DI) is a **design pattern** used to achieve **Inversion of Control (IoC)**. It allows you to manage and inject dependencies into classes in a flexible, maintainable, and testable way. In .NET Core, DI is built into the framework, making it easy to configure and use.

---

### **Why Use Dependency Injection?**

1. **Decouples Components**:
   - Classes depend on abstractions (interfaces) rather than concrete implementations, promoting flexibility.
2. **Improves Testability**:

   - Dependencies can be replaced with mocks during unit testing.

3. **Centralized Configuration**:

   - All dependencies are registered in one place, making the system easier to manage.

4. **Promotes Single Responsibility Principle**:
   - Classes don’t manage the lifecycle of their dependencies.

---

### **How Dependency Injection Works**

#### **Components**

1. **Service**:
   - A class that provides some functionality (e.g., a logging service, repository).
2. **Consumer**:
   - A class that requires a service to function.
3. **IoC Container**:
   - A framework feature that manages the lifecycle of services and resolves dependencies.

#### **Steps**

1. **Register** the services in the IoC container.
2. **Inject** the services where needed (e.g., via constructor injection).
3. The IoC container **resolves** the dependencies when creating the consumer.

---

### **Types of DI**

1. **Constructor Injection** (most common)

   - Dependencies are passed via the class constructor.

   ```csharp
   public class UserController
   {
       private readonly IUserService _userService;

       public UserController(IUserService userService)
       {
           _userService = userService;
       }
   }
   ```

2. **Method Injection**

   - Dependencies are passed as parameters to a method.

   ```csharp
   public void UpdateUser(IUserService userService)
   {
       userService.Update();
   }
   ```

3. **Property Injection**

   - Dependencies are assigned to public properties.

   ```csharp
   public IUserService UserService { get; set; }
   ```

---

### **DI in .NET Core**

#### **1. Registering Services**

In **Startup.cs**, services are registered in the `ConfigureServices` method.

**Service Lifetimes**:

- **Transient**: A new instance is created every time it's requested.
- **Scoped**: A single instance is created per request.
- **Singleton**: A single instance is shared across the application.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddTransient<IUserService, UserService>(); // Transient
    services.AddScoped<IOrderService, OrderService>();  // Scoped
    services.AddSingleton<ILogger, ConsoleLogger>();    // Singleton
}
```

---

#### **2. Injecting Services**

Inject services into controllers, middleware, or other components using constructor injection.

**Example: Injecting into a Controller**

```csharp
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = _userService.GetUserById(id);
        return Ok(user);
    }
}
```

---

#### **3. Built-in DI in Middleware**

Middleware can also use DI for accessing services.

**Example: Middleware with DI**

```csharp
public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation($"Request Path: {context.Request.Path}");
        await _next(context);
    }
}
```

---

### **Custom Service Example**

#### **Service Interface**

```csharp
public interface IUserService
{
    User GetUserById(int id);
}
```

#### **Service Implementation**

```csharp
public class UserService : IUserService
{
    public User GetUserById(int id)
    {
        return new User { Id = id, Name = "John Doe" };
    }
}
```

#### **Register Service**

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IUserService, UserService>();
}
```

#### **Inject into Consumer**

```csharp
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = _userService.GetUserById(id);
        return Ok(user);
    }
}
```

---

### **Advanced Topics in DI**

1. **Named Dependencies**

   - You can register multiple implementations of the same interface and resolve by name.

2. **Factory Services**

   - Use a factory to create instances dynamically.

   ```csharp
   services.AddTransient<IUserService>(sp => new UserService());
   ```

3. **Conditional Registrations**

   - Register services based on runtime conditions.

   ```csharp
   if (useMockService)
   {
       services.AddTransient<IUserService, MockUserService>();
   }
   else
   {
       services.AddTransient<IUserService, RealUserService>();
   }
   ```

---

### **DI Best Practices**

1. **Use Interfaces**: Always depend on abstractions.
2. **Avoid Service Locator Pattern**: Don’t fetch dependencies manually from the container.
3. **Keep Services Stateless**: Avoid storing state in services unless they are registered as Singleton.
4. **Use the Right Lifetime**:
   - **Transient** for lightweight services.
   - **Scoped** for services tied to a request.
   - **Singleton** for shared resources like configuration.

---

### **Summary**

Dependency Injection in .NET Core simplifies application design by managing object creation and dependency resolution. By leveraging DI effectively, you can create a system that is modular, testable, and easy to maintain.
