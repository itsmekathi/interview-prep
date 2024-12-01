### **Domain-Driven Design (DDD) Core Concepts with Examples in .NET Core**

**Domain-Driven Design (DDD)** is a software development approach that emphasizes collaboration between domain experts and developers to design software that models complex domains. The key focus is on the **domain** and its **business logic**, ensuring the software reflects the real-world problems it solves.

---

### **Core Concepts of DDD**

1. **Domain**:

   - Represents the core business logic and the problem space.
   - Example: In an e-commerce application, the domain includes concepts like `Product`, `Order`, and `Customer`.

2. **Entity**:

   - Represents a unique object with an identity.
   - Example: A `Product` with a unique `Id`.

3. **Value Object**:

   - An immutable object defined by its values, not by identity.
   - Example: A `Money` object with `Amount` and `Currency`.

4. **Aggregate**:

   - A cluster of domain objects that are treated as a single unit.
   - Example: An `Order` aggregate that includes `OrderItems`.

5. **Aggregate Root**:

   - The entry point to an aggregate.
   - Example: The `Order` entity is the root of the `OrderItems` aggregate.

6. **Domain Event**:

   - Represents something significant that happened in the domain.
   - Example: `OrderPlaced`, `ProductOutOfStock`.

7. **Repository**:

   - A pattern for retrieving and persisting aggregates.
   - Example: `IOrderRepository` to handle `Order` persistence.

8. **Service**:

   - Encapsulates domain logic that doesn't naturally belong to an entity or value object.
   - Example: A `PaymentService` for handling payment logic.

9. **Bounded Context**:
   - Defines the boundary within which a domain model is valid.
   - Example: The `Sales` bounded context is separate from the `Inventory` context.

---

### **Example Use Case: Order Management System**

#### **Domain Modeling**

---

#### **1. Entities**

**Order Entity**:

```csharp
namespace DDDExample.Domain.Entities {
    public class Order {
        public Guid Id { get; private set; }
        public List<OrderItem> Items { get; private set; } = new();
        public DateTime OrderDate { get; private set; }
        public decimal TotalAmount => Items.Sum(item => item.TotalPrice);

        public Order() {
            Id = Guid.NewGuid();
            OrderDate = DateTime.UtcNow;
        }

        public void AddItem(OrderItem item) {
            Items.Add(item);
        }
    }

    public class OrderItem {
        public Guid Id { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice => Quantity * UnitPrice;

        public OrderItem(string productName, int quantity, decimal unitPrice) {
            Id = Guid.NewGuid();
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
```

---

#### **2. Value Objects**

**Address Value Object**:

```csharp
namespace DDDExample.Domain.ValueObjects {
    public class Address {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }

        public Address(string street, string city, string zipCode) {
            Street = street;
            City = city;
            ZipCode = zipCode;
        }

        public override bool Equals(object? obj) {
            if (obj is not Address other) return false;
            return Street == other.Street && City == other.City && ZipCode == other.ZipCode;
        }

        public override int GetHashCode() => HashCode.Combine(Street, City, ZipCode);
    }
}
```

---

#### **3. Domain Events**

**OrderPlaced Event**:

```csharp
namespace DDDExample.Domain.Events {
    public class OrderPlaced {
        public Guid OrderId { get; }
        public DateTime PlacedAt { get; }

        public OrderPlaced(Guid orderId, DateTime placedAt) {
            OrderId = orderId;
            PlacedAt = placedAt;
        }
    }
}
```

---

#### **4. Aggregate and Aggregate Root**

The `Order` is an aggregate root, and `OrderItem` is part of its aggregate.

---

#### **5. Repository**

**IOrderRepository**:

```csharp
namespace DDDExample.Domain.Repositories {
    using DDDExample.Domain.Entities;

    public interface IOrderRepository {
        void Add(Order order);
        Order GetById(Guid id);
    }
}
```

**OrderRepository Implementation**:

```csharp
namespace DDDExample.Infrastructure.Repositories {
    using DDDExample.Domain.Entities;
    using DDDExample.Domain.Repositories;

    public class InMemoryOrderRepository : IOrderRepository {
        private readonly List<Order> _orders = new();

        public void Add(Order order) => _orders.Add(order);

        public Order GetById(Guid id) => _orders.FirstOrDefault(o => o.Id == id);
    }
}
```

---

#### **6. Service**

**OrderService**:

```csharp
namespace DDDExample.Domain.Services {
    using DDDExample.Domain.Entities;
    using DDDExample.Domain.Events;
    using DDDExample.Domain.Repositories;

    public class OrderService {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository) {
            _orderRepository = orderRepository;
        }

        public Order PlaceOrder(List<OrderItem> items) {
            var order = new Order();
            foreach (var item in items) {
                order.AddItem(item);
            }

            _orderRepository.Add(order);

            // Raise domain event
            var orderPlacedEvent = new OrderPlaced(order.Id, DateTime.UtcNow);
            // Handle the event (can use Mediator or Event Bus here)

            return order;
        }
    }
}
```

---

#### **7. Bounded Context**

- The **Order Management** context focuses on orders.
- Another context, like **Inventory Management**, may focus on product stock and warehouse management.
- Bounded contexts communicate via domain events or APIs.

---

#### **8. Application Layer**

**OrderController**:

```csharp
namespace DDDExample.API.Controllers {
    using Microsoft.AspNetCore.Mvc;
    using DDDExample.Domain.Entities;
    using DDDExample.Domain.Services;

    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService) {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] List<OrderItem> items) {
            var order = _orderService.PlaceOrder(items);
            return Ok(order);
        }
    }
}
```

---

### **Advantages of DDD**

1. **Focus on Core Domain**:
   - Ensures the system models the real-world problem effectively.
2. **Maintainability**:
   - Clear separation of concerns with entities, value objects, services, and repositories.
3. **Scalability**:
   - Complex systems are easier to scale with bounded contexts.
4. **Extensibility**:
   - Easy to add new features without breaking existing functionality.

---

### **Conclusion**

Domain-Driven Design (DDD) enables developers to model complex domains effectively. By structuring applications around business logic and domain concepts, it ensures a more maintainable, testable, and scalable architecture. The provided example demonstrates the key concepts of DDD implemented in .NET Core. Let me know if you'd like to explore any specific part further!
