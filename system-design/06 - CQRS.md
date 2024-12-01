### **Command Query Responsibility Segregation (CQRS) in .NET Core**

**CQRS** is a software design pattern that separates the responsibilities of reading (querying) and writing (commands) data in an application. By segregating these responsibilities, it provides better scalability, performance, and maintainability for applications with complex domains or high throughput requirements.

---

### **Core Concepts of CQRS**

1. **Command**:

   - Represents operations that change the state of the system.
   - Example: Adding a product, updating an order, deleting a record.

2. **Query**:

   - Represents operations that retrieve data without modifying it.
   - Example: Fetching a list of products or getting order details.

3. **Separation**:

   - Command and query operations are handled by different models or services.
   - Commands focus on **modifying state**.
   - Queries focus on **retrieving data**.

4. **Read and Write Models**:
   - The **Write Model** handles commands and implements business logic for state changes.
   - The **Read Model** is optimized for queries and may have a separate data schema.

---

### **Benefits of CQRS**

- **Scalability**:
  - Read and write operations can scale independently.
- **Performance Optimization**:
  - Read models can be denormalized for faster queries.
- **Separation of Concerns**:
  - Clear separation makes the system easier to maintain and test.
- **Flexibility**:
  - Enables using different databases for read and write operations.

---

### **CQRS Implementation in .NET Core**

#### **Use Case**: A Simple Product Management System

---

#### **1. Setting up the Domain**

**Product Entity**:

```csharp
namespace CQRSExample.Domain.Entities {
    public class Product {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
```

---

#### **2. Commands: Handling Write Operations**

**CreateProductCommand**:

```csharp
namespace CQRSExample.Application.Commands {
    public class CreateProductCommand {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
```

**Command Handler**:

```csharp
namespace CQRSExample.Application.Handlers {
    using CQRSExample.Domain.Entities;
    using CQRSExample.Infrastructure.Repositories;

    public class CreateProductCommandHandler {
        private readonly IProductRepository _repository;

        public CreateProductCommandHandler(IProductRepository repository) {
            _repository = repository;
        }

        public void Handle(CreateProductCommand command) {
            var product = new Product {
                Name = command.Name,
                Price = command.Price
            };

            _repository.Add(product);
        }
    }
}
```

---

#### **3. Queries: Handling Read Operations**

**GetAllProductsQuery**:

```csharp
namespace CQRSExample.Application.Queries {
    public class GetAllProductsQuery { }
}
```

**Query Handler**:

```csharp
namespace CQRSExample.Application.Handlers {
    using CQRSExample.Domain.Entities;
    using CQRSExample.Infrastructure.Repositories;

    public class GetAllProductsQueryHandler {
        private readonly IProductRepository _repository;

        public GetAllProductsQueryHandler(IProductRepository repository) {
            _repository = repository;
        }

        public IEnumerable<Product> Handle(GetAllProductsQuery query) {
            return _repository.GetAll();
        }
    }
}
```

---

#### **4. Infrastructure: Repository Layer**

**IProductRepository**:

```csharp
namespace CQRSExample.Infrastructure.Repositories {
    using CQRSExample.Domain.Entities;

    public interface IProductRepository {
        void Add(Product product);
        IEnumerable<Product> GetAll();
    }
}
```

**In-Memory Product Repository Implementation**:

```csharp
namespace CQRSExample.Infrastructure.Repositories {
    using CQRSExample.Domain.Entities;
    using System.Collections.Generic;

    public class InMemoryProductRepository : IProductRepository {
        private readonly List<Product> _products = new();

        public void Add(Product product) {
            product.Id = _products.Count + 1;
            _products.Add(product);
        }

        public IEnumerable<Product> GetAll() => _products;
    }
}
```

---

#### **5. Presentation Layer**

**Product Controller**:

```csharp
namespace CQRSExample.API.Controllers {
    using Microsoft.AspNetCore.Mvc;
    using CQRSExample.Application.Commands;
    using CQRSExample.Application.Handlers;
    using CQRSExample.Application.Queries;

    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase {
        private readonly CreateProductCommandHandler _createHandler;
        private readonly GetAllProductsQueryHandler _queryHandler;

        public ProductController(CreateProductCommandHandler createHandler, GetAllProductsQueryHandler queryHandler) {
            _createHandler = createHandler;
            _queryHandler = queryHandler;
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateProductCommand command) {
            _createHandler.Handle(command);
            return Ok("Product created successfully.");
        }

        [HttpGet]
        public IActionResult GetProducts() {
            var products = _queryHandler.Handle(new GetAllProductsQuery());
            return Ok(products);
        }
    }
}
```

---

### **Adding Dependency Injection**

In `Startup.cs` or `Program.cs` (for .NET 6+):

```csharp
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddTransient<CreateProductCommandHandler>();
builder.Services.AddTransient<GetAllProductsQueryHandler>();
```

---

### **Advantages of CQRS in This Scenario**

1. **Command Handlers**: Focus solely on the logic for creating products.
2. **Query Handlers**: Focus solely on retrieving data, allowing for optimized query models.
3. **Scalability**: If the application grows, read operations can be separated into a dedicated read database.

---

### **When to Use CQRS**

1. **High Complexity**:

   - When business logic and domain complexity demand separate models for commands and queries.

2. **High Scalability**:

   - When read and write workloads are significantly different.

3. **Event Sourcing**:
   - Often combined with CQRS to record every state change as an event.

---

### **CQRS with MediatR in .NET Core**

For larger projects, frameworks like **MediatR** simplify CQRS implementation by handling the pipeline and communication between handlers and controllers.

**Installing MediatR**:

```bash
dotnet add package MediatR.Extensions.Microsoft.DependencyInjection
```

**Command with MediatR**:

```csharp
public record CreateProductCommand(string Name, decimal Price) : IRequest;
```

**Command Handler**:

```csharp
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand> {
    private readonly IProductRepository _repository;

    public CreateProductCommandHandler(IProductRepository repository) {
        _repository = repository;
    }

    public Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken) {
        var product = new Product { Name = request.Name, Price = request.Price };
        _repository.Add(product);
        return Task.FromResult(Unit.Value);
    }
}
```

---

### **Conclusion**

CQRS simplifies the separation of read and write operations, enabling scalability and maintainability in .NET Core applications. Combined with **MediatR** or other libraries, it becomes even more powerful and easier to implement. Let me know if you'd like further details or enhancements!
