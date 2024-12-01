Minimal APIs in .NET Core are a lightweight approach to building HTTP APIs with minimal configuration and overhead. They were introduced in **ASP.NET Core 6.0** to simplify the development of APIs by reducing boilerplate code, making them especially suitable for small, focused services like microservices or prototypes.

### Key Features of Minimal APIs

1. **Minimal Boilerplate**: No need for controllers, attributes, or extensive configurations.
2. **Simplified Routing**: Define routes directly in the `Program.cs` file.
3. **Lightweight**: Focuses only on what's necessary for handling HTTP requests and responses.
4. **High Performance**: Less abstraction results in faster execution for small-scale APIs.
5. **Integrated Features**: Supports dependency injection (DI), middleware, model binding, and validation out-of-the-box.

---

### Anatomy of a Minimal API

Here's a simple example of a Minimal API in `.NET Core 6.0+`:

#### `Program.cs`

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Define an HTTP GET endpoint
app.MapGet("/", () => "Hello, World!");

// Define an HTTP GET endpoint with a parameter
app.MapGet("/greet/{name}", (string name) => $"Hello, {name}!");

// Define an HTTP POST endpoint
app.MapPost("/echo", (Person person) => Results.Json(person));

// Run the app
app.Run();

// Simple POCO class for the POST endpoint
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}
```

---

### Key Concepts in Minimal APIs

1. **Routing**:

   - Routes are defined using methods like `MapGet`, `MapPost`, `MapPut`, `MapDelete`, etc., directly on the `WebApplication` object.
   - Route parameters are automatically mapped to handler parameters.

2. **Dependency Injection (DI)**:

   - Minimal APIs integrate seamlessly with ASP.NET Core's DI.

   ```csharp
   app.MapGet("/time", (ILogger<Program> logger) => {
       logger.LogInformation("Getting current time");
       return DateTime.Now.ToString();
   });
   ```

3. **Binding and Validation**:

   - Request data (e.g., JSON) is automatically bound to objects.
   - You can use validation attributes on models to validate incoming data.

4. **Results**:

   - Use `Results` helper methods like `Results.Json`, `Results.Ok`, `Results.NotFound`, etc., to return responses.

   ```csharp
   app.MapGet("/status", () => Results.Ok(new { Status = "Running" }));
   ```

5. **Middleware Integration**:
   - Middleware like authentication, authorization, and error handling can still be used with minimal APIs.
   ```csharp
   app.UseAuthentication();
   app.UseAuthorization();
   ```

---

### Benefits of Minimal APIs

1. **Ease of Use**: Ideal for small projects or developers new to ASP.NET Core.
2. **Faster Development**: Less setup, faster iteration.
3. **Scalable**: Can coexist with more traditional MVC architectures in larger projects.
4. **Modern Features**: Built on top of ASP.NET Core, so it includes all modern features like OpenAPI/Swagger, DI, and middleware.

---

### Suitable Use Cases

- Microservices
- Prototypes and MVPs (Minimum Viable Products)
- IoT services
- Serverless APIs
- Lightweight APIs for mobile or frontend applications

---

### Extending Minimal APIs

1. **OpenAPI/Swagger**:
   Minimal APIs support OpenAPI documentation generation using libraries like **Swashbuckle** or **NSwag**.

   ```csharp
   builder.Services.AddEndpointsApiExplorer();
   builder.Services.AddSwaggerGen();

   app.UseSwagger();
   app.UseSwaggerUI();
   ```

2. **Authentication & Authorization**:
   Use attributes like `[Authorize]` or middleware for securing endpoints.

   ```csharp
   app.MapGet("/secure", [Authorize] () => "This is secure!");
   ```

3. **Custom Middleware**:
   Add custom middleware using `app.Use`.
   ```csharp
   app.Use(async (context, next) =>
   {
       Console.WriteLine("Custom Middleware: Before");
       await next();
       Console.WriteLine("Custom Middleware: After");
   });
   ```

---

### Limitations

- Not ideal for very complex APIs with multiple layers of abstraction.
- May become harder to maintain in large projects with numerous endpoints.
- Some developers may prefer the structure and conventions of MVC.

---

### Summary

Minimal APIs in .NET Core provide a modern, efficient way to build small-scale, high-performance APIs. They reduce boilerplate and complexity while retaining the power and flexibility of ASP.NET Core's ecosystem. While perfect for microservices and small applications, they can also complement traditional architectures in larger projects.
