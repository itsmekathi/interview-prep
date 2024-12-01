### **Event-Driven Architecture (EDA) in .NET Core**

Event-Driven Architecture (EDA) is a software architectural pattern that promotes the production, detection, consumption of, and reaction to events. In this architecture, components communicate by producing and consuming events, allowing for a more decoupled and scalable system.

### **Key Concepts of Event-Driven Architecture**

1. **Event**: A significant change in state or occurrence that is broadcasted to interested parties.

   - Example: `OrderPlaced`, `ProductAdded`.

2. **Event Producer**: A component that publishes events to an event bus or message broker.

   - Example: An e-commerce service that creates an order and emits an `OrderPlaced` event.

3. **Event Consumer**: A component that listens for and reacts to events.

   - Example: An inventory service that updates stock levels upon receiving an `OrderPlaced` event.

4. **Event Bus / Message Broker**: A middleware that facilitates communication between producers and consumers by handling the transmission of events.

   - Examples: RabbitMQ, Kafka, Azure Service Bus.

5. **Event Store**: A persistent storage for events, which can be used for event sourcing.

---

### **Example Use Case: E-Commerce Application**

#### **Scenario**: When an order is placed, multiple services need to react to this event to update inventory, notify the customer, and process payment.

---

### **Implementation Steps**

#### **1. Define Event Classes**

Create classes for events that will be published. For example, an `OrderPlaced` event.

```csharp
namespace ECommerce.Events {
    public class OrderPlaced {
        public Guid OrderId { get; }
        public string CustomerId { get; }
        public DateTime OrderDate { get; }

        public OrderPlaced(Guid orderId, string customerId, DateTime orderDate) {
            OrderId = orderId;
            CustomerId = customerId;
            OrderDate = orderDate;
        }
    }
}
```

---

#### **2. Event Producer**

An order service that produces the `OrderPlaced` event when an order is created.

**Order Service**:

```csharp
using ECommerce.Events;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services {
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase {
        private readonly IEventBus _eventBus;

        public OrderController(IEventBus eventBus) {
            _eventBus = eventBus;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order) {
            // Logic to save the order in the database

            // Publish the event
            var orderPlacedEvent = new OrderPlaced(order.Id, order.CustomerId, DateTime.UtcNow);
            _eventBus.Publish(orderPlacedEvent);

            return Ok(order);
        }
    }
}
```

---

#### **3. Event Bus Implementation**

An interface for the event bus and a basic implementation using an in-memory list.

**IEventBus**:

```csharp
using ECommerce.Events;

namespace ECommerce.Bus {
    public interface IEventBus {
        void Publish<T>(T @event) where T : class;
        void Subscribe<T>(Action<T> handler) where T : class;
    }
}
```

**InMemoryEventBus**:

```csharp
using System.Collections.Concurrent;
using ECommerce.Events;

namespace ECommerce.Bus {
    public class InMemoryEventBus : IEventBus {
        private readonly ConcurrentDictionary<string, List<Delegate>> _subscribers = new();

        public void Publish<T>(T @event) where T : class {
            var key = typeof(T).Name;
            if (_subscribers.TryGetValue(key, out var handlers)) {
                foreach (var handler in handlers) {
                    handler.DynamicInvoke(@event);
                }
            }
        }

        public void Subscribe<T>(Action<T> handler) where T : class {
            var key = typeof(T).Name;
            if (!_subscribers.ContainsKey(key)) {
                _subscribers[key] = new List<Delegate>();
            }
            _subscribers[key].Add(handler);
        }
    }
}
```

---

#### **4. Event Consumer**

A service that consumes the `OrderPlaced` event and performs an action, like updating the inventory.

**Inventory Service**:

```csharp
using ECommerce.Events;

namespace ECommerce.Consumers {
    public class InventoryService {
        public void HandleOrderPlaced(OrderPlaced orderPlacedEvent) {
            // Logic to update inventory based on the order
            Console.WriteLine($"Inventory updated for order {orderPlacedEvent.OrderId}");
        }
    }
}
```

**Subscription Setup**:
In the `Startup.cs` or `Program.cs`, set up the event bus and subscribe to events.

```csharp
using ECommerce.Bus;
using ECommerce.Events;
using ECommerce.Consumers;

// Register services
builder.Services.AddSingleton<IEventBus, InMemoryEventBus>();
builder.Services.AddSingleton<InventoryService>();

// Subscribe to events
var serviceProvider = builder.Build();
var eventBus = serviceProvider.GetRequiredService<IEventBus>();
var inventoryService = serviceProvider.GetRequiredService<InventoryService>();
eventBus.Subscribe<OrderPlaced>(inventoryService.HandleOrderPlaced);
```

---

### **5. Testing the Implementation**

You can test the entire flow by creating an order via the `OrderController`. When the order is created, the `OrderPlaced` event is published, and the `InventoryService` consumes it, updating the inventory.

### **6. Advantages of Event-Driven Architecture**

- **Decoupling**: Producers and consumers are decoupled, enabling easier modifications and maintenance.
- **Scalability**: Services can scale independently based on the volume of events they handle.
- **Resilience**: Systems can continue functioning as they respond to events asynchronously.
- **Flexibility**: New services can be added that consume existing events without modifying the producers.

---

### **Conclusion**

Event-Driven Architecture provides a robust framework for building scalable and maintainable applications. By leveraging events, services can react to changes in state asynchronously, promoting loose coupling and greater flexibility. The provided example illustrates a simple implementation using .NET Core. Let me know if you would like to explore any specific aspect further!
