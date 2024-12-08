In C#, **classes** and **structs** are both used to define custom data types, but they have significant differences in their behavior, memory allocation, and use cases. Here's a detailed comparison:

---

### **Key Differences Between Class and Struct**

| **Aspect**              | **Class**                                                               | **Struct**                                                                            |
| ----------------------- | ----------------------------------------------------------------------- | ------------------------------------------------------------------------------------- |
| **Type**                | Reference type                                                          | Value type                                                                            |
| **Memory Allocation**   | Allocated on the **heap** (via references).                             | Allocated on the **stack** (when used as local variables).                            |
| **Default Behavior**    | Variables of a class type hold a reference to the object.               | Variables of a struct type hold the data directly.                                    |
| **Inheritance**         | Supports inheritance.                                                   | Does not support inheritance (can implement interfaces).                              |
| **Constructors**        | Can have a parameterless constructor.                                   | Cannot have a parameterless constructor (unless using `default` keyword from C# 10+). |
| **Immutability**        | Can be mutable or immutable.                                            | Typically used as immutable types (recommended).                                      |
| **Performance**         | May have higher overhead due to heap allocation and garbage collection. | Lightweight and efficient for small data structures.                                  |
| **Nullability**         | Can be `null`.                                                          | Cannot be `null` (unless nullable struct: `int?`).                                    |
| **Default Constructor** | Default constructor initializes reference to `null`.                    | Default constructor initializes fields to their default values.                       |

---

### **Detailed Explanation**

#### 1. **Memory Management**

- **Class**:

  - Objects are allocated on the heap.
  - Managed by the Garbage Collector, which can introduce some overhead.
  - Multiple variables can point to the same object (reference semantics).

  Example:

  ```csharp
  class MyClass
  {
      public int Value;
  }

  var obj1 = new MyClass { Value = 5 };
  var obj2 = obj1;
  obj2.Value = 10;

  Console.WriteLine(obj1.Value); // Output: 10 (same reference)
  ```

- **Struct**:

  - Allocated on the stack when used as a local variable.
  - Independent copies are created during assignments (value semantics).

  Example:

  ```csharp
  struct MyStruct
  {
      public int Value;
  }

  var obj1 = new MyStruct { Value = 5 };
  var obj2 = obj1;
  obj2.Value = 10;

  Console.WriteLine(obj1.Value); // Output: 5 (independent copy)
  ```

---

#### 2. **Inheritance**

- **Class**:

  - Supports inheritance and polymorphism.
  - Can be extended and overridden.

  ```csharp
  class BaseClass
  {
      public virtual void Display() => Console.WriteLine("BaseClass");
  }

  class DerivedClass : BaseClass
  {
      public override void Display() => Console.WriteLine("DerivedClass");
  }

  var obj = new DerivedClass();
  obj.Display(); // Output: DerivedClass
  ```

- **Struct**:

  - Does not support inheritance, but can implement interfaces.

  ```csharp
  interface IDisplay
  {
      void Show();
  }

  struct MyStruct : IDisplay
  {
      public void Show() => Console.WriteLine("Struct Implementation");
  }

  var obj = new MyStruct();
  obj.Show(); // Output: Struct Implementation
  ```

---

#### 3. **Usage Scenarios**

- **Class**:

  - Best for complex objects with behavior and mutable state.
  - Example: Entities in a database, business logic models.

  ```csharp
  class Employee
  {
      public string Name { get; set; }
      public int Age { get; set; }
  }
  ```

- **Struct**:
  - Best for small, lightweight data structures.
  - Example: Points, colors, geometric shapes.
  ```csharp
  struct Point
  {
      public int X { get; set; }
      public int Y { get; set; }
  }
  ```

---

### **When to Use Class vs Struct**

| **Class**                                           | **Struct**                                     |
| --------------------------------------------------- | ---------------------------------------------- |
| When objects have a large size or complex behavior. | When objects are small and frequently created. |
| When objects need inheritance or polymorphism.      | When objects are immutable and independent.    |
| When you expect reference semantics.                | When you expect value semantics.               |

---

### **Examples: Class vs Struct**

#### **Class Example**

```csharp
class Car
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
}
```

#### **Struct Example**

```csharp
struct Rectangle
{
    public int Width { get; set; }
    public int Height { get; set; }
}
```

---

### **Performance Considerations**

- **Struct**:

  - Avoid for large objects because copying can be costly.
  - Ideal for small objects frequently used in performance-critical scenarios.

- **Class**:
  - More memory overhead but better for large or mutable objects.
  - Avoid excessive use in performance-critical scenarios where structs are better suited.

Would you like more specific examples or scenarios comparing the two?
