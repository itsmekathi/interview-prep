### **Records in C#**

Records are a special type of class in C# introduced in **C# 9.0**, designed to create **immutable** data objects with value-based equality. Unlike regular classes, records focus on **data immutability** and are ideal for scenarios where the primary purpose of an object is to hold data.

---

### **Key Features of Records**

1. **Immutability**:

   - Records are designed to be immutable by default.
   - You can use `init` accessors to set properties during object initialization but not modify them afterward.

2. **Value-Based Equality**:

   - Records compare objects based on their **values**, not references.
   - If two records have the same values for all their properties, they are considered equal.

3. **Concise Syntax**:

   - Records can be declared concisely using **positional syntax**.
   - Automatically provides a lot of boilerplate code like equality members, `ToString`, etc.

4. **With Expressions**:

   - Supports `with` expressions to create copies of records with modifications.

5. **Deconstruction**:

   - Records support deconstruction for extracting property values.

6. **Inheritance**:
   - Records can be inherited like classes, enabling hierarchical structures.

---

### **Declaration of Records**

#### **Standard Declaration**

```csharp
public record Person
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
}
```

#### **Positional Declaration**

```csharp
public record Person(string FirstName, string LastName);
```

---

### **Key Differences Between Class and Record**

| **Aspect**       | **Class**                                                  | **Record**                                       |
| ---------------- | ---------------------------------------------------------- | ------------------------------------------------ |
| **Equality**     | Reference-based by default.                                | Value-based by default.                          |
| **Immutability** | Mutable by default.                                        | Immutable by default.                            |
| **Purpose**      | Used for encapsulating behavior and state.                 | Used for data modeling and immutability.         |
| **Conciseness**  | Requires manual implementation of equality and `ToString`. | Automatically generates equality and `ToString`. |

---

### **Features in Detail**

#### **1. Immutability**

```csharp
public record Person
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
}

var person = new Person { FirstName = "John", LastName = "Doe" };
// person.FirstName = "Jane"; // Error: Cannot modify `init` property after initialization.
```

---

#### **2. Value-Based Equality**

```csharp
var person1 = new Person { FirstName = "John", LastName = "Doe" };
var person2 = new Person { FirstName = "John", LastName = "Doe" };

Console.WriteLine(person1 == person2); // Output: True
```

---

#### **3. Positional Syntax**

```csharp
public record Person(string FirstName, string LastName);

var person = new Person("John", "Doe");

Console.WriteLine(person.FirstName); // Output: John
```

---

#### **4. With Expressions**

```csharp
var person1 = new Person("John", "Doe");
var person2 = person1 with { LastName = "Smith" };

Console.WriteLine(person2); // Output: Person { FirstName = John, LastName = Smith }
```

---

#### **5. Deconstruction**

```csharp
public record Person(string FirstName, string LastName);

var person = new Person("John", "Doe");
var (firstName, lastName) = person;

Console.WriteLine(firstName); // Output: John
Console.WriteLine(lastName);  // Output: Doe
```

---

#### **6. Inheritance**

```csharp
public record Person(string FirstName, string LastName);
public record Employee(string FirstName, string LastName, string Department) : Person(FirstName, LastName);

var employee = new Employee("John", "Doe", "IT");
Console.WriteLine(employee); // Output: Employee { FirstName = John, LastName = Doe, Department = IT }
```

---

### **Use Cases for Records**

1. **Modeling Data**:

   - Use records to represent data objects like DTOs, models, and immutable configurations.
   - Example: JSON response objects, configuration settings.

2. **Immutability Requirement**:

   - Ideal for scenarios requiring immutability, such as functional programming or thread-safe data structures.

3. **Simplified Equality**:
   - When objects need value-based equality, such as in LINQ queries or collections like dictionaries.

---

### **Performance Considerations**

- Records can be efficient due to immutability and value-based equality, especially in scenarios with frequent comparisons.
- For large data objects, copying via `with` expressions might introduce some overhead, so use cautiously.

---

### **Advanced Topics**

#### **1. Mutable Records**

While records are immutable by default, you can make them mutable (not recommended):

```csharp
public record Person
{
    public string FirstName { get; set; } // Mutable property
    public string LastName { get; set; }
}
```

#### **2. Record Structs**

Introduced in **C# 10**, **record structs** combine records' benefits with the lightweight nature of structs.

```csharp
public record struct Point(int X, int Y);
```

---

### **Records vs Structs**

| **Aspect**            | **Record**            | **Record Struct**      |
| --------------------- | --------------------- | ---------------------- |
| **Type**              | Reference type        | Value type             |
| **Equality**          | Value-based equality  | Value-based equality   |
| **Memory Allocation** | Allocated on the heap | Allocated on the stack |

---

### **When to Use Records**

- When you need **immutable** objects.
- When value-based equality is important.
- For simplifying boilerplate code in data models.

Would you like to dive deeper into a specific record-related feature?
