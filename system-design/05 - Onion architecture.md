### **Onion Architecture in .NET Core**

The **Onion Architecture**, introduced by Jeffrey Palermo, is a software architectural pattern that emphasizes maintainability, testability, and scalability. It structures an application into concentric layers, where each layer has specific responsibilities and dependencies flow inward. This design minimizes coupling between components and focuses on the **core domain logic**.

---

### **Core Principles of Onion Architecture**

1. **Dependency Inversion**:

   - Higher-level layers depend on abstractions, not concrete implementations.
   - Inner layers have no dependency on outer layers.

2. **Separation of Concerns**:

   - Each layer has a well-defined responsibility.

3. **Central Domain Logic**:

   - The core domain (entities and business logic) is at the center and remains independent of external concerns.

4. **Testability**:
   - Clear separation allows easy mocking and testing.

---

### **Layers in Onion Architecture**

1. **Core Layer** (Inner Layer):

   - **Entities**: Represents the core domain and business rules.
   - **Interfaces**: Contracts for the services and repositories.
   - **Domain Logic**: Business rules and validations.

2. **Application Layer**:

   - Handles use cases, application services, and orchestration of business rules.
   - Contains interfaces and DTOs.

3. **Infrastructure Layer**:

   - Implements repository patterns, services, and external dependencies (e.g., database, logging).

4. **Presentation Layer** (Outer Layer):
   - UI and API layers that interact with users or other systems.

---

### **Onion Architecture Example in .NET Core**

#### **Use Case**: A Simple E-commerce System with Products

---

#### **Step 1: Core Layer**

Contains the domain model and interfaces.

**Entities**:

```csharp
namespace ECommerce.Core.Entities {
    public class Product {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
```

**Repository Interface**:

```csharp
namespace ECommerce.Core.Interfaces {
    public interface IProductRepository {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
    }
}
```

---

#### **Step 2: Application Layer**

Handles business logic and orchestrates calls to the repository.

**Product Service**:

```csharp
namespace ECommerce.Application.Services {
    using ECommerce.Core.Entities;
    using ECommerce.Core.Interfaces;

    public class ProductService {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository) {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetProducts() {
            return _productRepository.GetAllProducts();
        }

        public void AddProduct(Product product) {
            if (product.Price <= 0) {
                throw new ArgumentException("Price must be greater than zero.");
            }
            _productRepository.AddProduct(product);
        }
    }
}
```

---

#### **Step 3: Infrastructure Layer**

Implements repository interfaces and external services.

**Product Repository**:

```csharp
namespace ECommerce.Infrastructure.Repositories {
    using ECommerce.Core.Entities;
    using ECommerce.Core.Interfaces;

    public class ProductRepository : IProductRepository {
        private readonly List<Product> _products = new();

        public IEnumerable<Product> GetAllProducts() => _products;

        public Product GetProductById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public void AddProduct(Product product) => _products.Add(product);
    }
}
```

---

#### **Step 4: Presentation Layer**

Exposes API endpoints for user interaction.

**Product Controller**:

```csharp
namespace ECommerce.API.Controllers {
    using Microsoft.AspNetCore.Mvc;
    using ECommerce.Application.Services;
    using ECommerce.Core.Entities;

    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase {
        private readonly ProductService _productService;

        public ProductController(ProductService productService) {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts() {
            var products = _productService.GetProducts();
            return Ok(products);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product) {
            _productService.AddProduct(product);
            return Ok();
        }
    }
}
```

---

### **Dependency Flow**

1. **Core Layer**: Contains entities and interfaces, independent of other layers.
2. **Application Layer**: Depends only on the **Core Layer**.
3. **Infrastructure Layer**: Implements interfaces from the **Core Layer**.
4. **Presentation Layer**: Depends on the **Application Layer** to execute business logic.

---

### **Dependency Injection Setup**

In `Startup.cs` or `Program.cs` (in .NET 6+):

```csharp
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();
```

---

### **Advantages of Onion Architecture**

1. **Separation of Concerns**:

   - Each layer has distinct responsibilities, reducing complexity.

2. **Testability**:

   - Mocking interfaces allows easy unit testing of core and application layers.

3. **Maintainability**:

   - Changes in infrastructure (e.g., database) don’t affect the core.

4. **Scalability**:
   - New features or layers can be added with minimal impact.

---

### **Unit Testing Example**

You can mock the `IProductRepository` to test the `ProductService`.

**Unit Test**:

```csharp
using Moq;
using Xunit;
using ECommerce.Application.Services;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;

public class ProductServiceTests {
    [Fact]
    public void AddProduct_ShouldThrowException_WhenPriceIsZeroOrLess() {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        var productService = new ProductService(mockRepo.Object);

        var product = new Product { Id = 1, Name = "Test Product", Price = 0 };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => productService.AddProduct(product));
    }
}
```

---

### **Conclusion**

The Onion Architecture ensures that the **core business logic is isolated** from external concerns like UI and database, making the system more maintainable and flexible. By adhering to this architecture, you can build scalable, testable, and robust .NET Core applications.
