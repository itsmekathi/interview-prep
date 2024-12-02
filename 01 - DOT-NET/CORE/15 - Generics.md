### **Generics in C#**

**Generics** in C# allow you to define type-safe and reusable data structures, classes, methods, interfaces, and delegates without specifying the exact data types. This feature ensures type safety, reduces code duplication, and improves performance by avoiding boxing and unboxing.

---

### **1. Basics of Generics**

Generics are defined using the `<T>` syntax, where `T` is a placeholder for a type that will be specified at runtime.

#### Example: Generic Class

```csharp
public class GenericBox<T>
{
    private T item;

    public void Add(T newItem)
    {
        item = newItem;
    }

    public T GetItem()
    {
        return item;
    }
}

class Program
{
    static void Main()
    {
        GenericBox<int> intBox = new GenericBox<int>();
        intBox.Add(42);
        Console.WriteLine(intBox.GetItem()); // Output: 42

        GenericBox<string> strBox = new GenericBox<string>();
        strBox.Add("Hello");
        Console.WriteLine(strBox.GetItem()); // Output: Hello
    }
}
```

---

### **2. Generic Methods**

A generic method defines type parameters that apply only to that method.

#### Example:

```csharp
public class Utilities
{
    public T GetMax<T>(T x, T y) where T : IComparable<T>
    {
        return x.CompareTo(y) > 0 ? x : y;
    }
}

class Program
{
    static void Main()
    {
        Utilities util = new Utilities();
        Console.WriteLine(util.GetMax(10, 20)); // Output: 20
        Console.WriteLine(util.GetMax("Apple", "Banana")); // Output: Banana
    }
}
```

---

### **3. Generic Interfaces**

Interfaces can also be generic, allowing you to define type-safe contracts.

#### Example:

```csharp
public interface IRepository<T>
{
    void Add(T item);
    T Get(int id);
}

public class Repository<T> : IRepository<T>
{
    private readonly Dictionary<int, T> data = new();

    public void Add(T item)
    {
        data[data.Count + 1] = item;
    }

    public T Get(int id)
    {
        return data.ContainsKey(id) ? data[id] : default;
    }
}

class Program
{
    static void Main()
    {
        IRepository<string> repo = new Repository<string>();
        repo.Add("Item1");
        Console.WriteLine(repo.Get(1)); // Output: Item1
    }
}
```

---

### **4. Constraints in Generics**

Constraints allow you to enforce rules on the type parameters, such as requiring a default constructor or an interface implementation.

#### Common Constraints:

- `where T : struct` – Must be a value type.
- `where T : class` – Must be a reference type.
- `where T : new()` – Must have a parameterless constructor.
- `where T : SomeBaseClass` – Must inherit from a specific base class.
- `where T : ISomeInterface` – Must implement a specific interface.

#### Example:

```csharp
public class Factory<T> where T : new()
{
    public T CreateInstance()
    {
        return new T();
    }
}

class Program
{
    static void Main()
    {
        Factory<StringBuilder> factory = new Factory<StringBuilder>();
        var sb = factory.CreateInstance();
        sb.Append("Hello, Generics!");
        Console.WriteLine(sb); // Output: Hello, Generics!
    }
}
```

---

### **5. Generic Delegates**

Generics can be used in delegates, enabling type-safe callback methods.

#### Example:

```csharp
public delegate T Operation<T>(T a, T b);

class Program
{
    static int Add(int x, int y) => x + y;

    static void Main()
    {
        Operation<int> addOperation = Add;
        Console.WriteLine(addOperation(10, 20)); // Output: 30
    }
}
```

---

### **6. Generic Collections**

The .NET framework provides a rich set of generic collections in the `System.Collections.Generic` namespace.

#### Examples:

- **`List<T>`**: A dynamic array.
  ```csharp
  List<int> numbers = new List<int> { 1, 2, 3 };
  numbers.Add(4);
  Console.WriteLine(string.Join(", ", numbers)); // Output: 1, 2, 3, 4
  ```
- **`Dictionary<TKey, TValue>`**: A key-value pair collection.
  ```csharp
  Dictionary<int, string> dict = new Dictionary<int, string>
  {
      { 1, "One" },
      { 2, "Two" }
  };
  Console.WriteLine(dict[1]); // Output: One
  ```
- **`Stack<T>`** and **`Queue<T>`**: LIFO and FIFO collections.
  ```csharp
  Stack<string> stack = new Stack<string>();
  stack.Push("First");
  stack.Push("Second");
  Console.WriteLine(stack.Pop()); // Output: Second
  ```

---

### **7. Benefits of Generics**

1. **Type Safety**:

   - Catch type-related errors at compile time.
   - Example:
     ```csharp
     List<int> numbers = new List<int>();
     numbers.Add(42); // Type-safe
     // numbers.Add("String"); // Compile-time error
     ```

2. **Performance**:

   - Avoids boxing/unboxing for value types.
   - Reduces runtime type checking.

3. **Reusability**:

   - Write once, reuse with multiple data types.

4. **Code Clarity**:
   - Reduces redundant code and simplifies maintenance.

---

### **8. Use Cases of Generics**

#### **Scenario 1: Strongly Typed Data Structures**

- **Problem**: Managing collections without losing type safety.
- **Solution**: Use `List<T>`, `Dictionary<TKey, TValue>`, etc.

#### **Scenario 2: Code Reusability**

- Define reusable methods and classes for different types.

  ```csharp
  public class Pair<T1, T2>
  {
      public T1 First { get; set; }
      public T2 Second { get; set; }
  }

  // Reuse with any types
  Pair<int, string> pair = new Pair<int, string> { First = 1, Second = "One" };
  ```

#### **Scenario 3: Data Access Layers**

- Create a generic repository for interacting with databases.
  ```csharp
  public interface IRepository<T> { /* CRUD methods */ }
  ```

#### **Scenario 4: Custom Sorting or Filtering**

- Write generic methods for sorting or filtering data.
  ```csharp
  public List<T> Filter<T>(List<T> items, Predicate<T> predicate)
  {
      return items.FindAll(predicate);
  }
  ```

#### **Scenario 5: Polymorphic Behavior**

- Implement generic interfaces for type-specific behavior.
  ```csharp
  public interface ITransformer<T>
  {
      T Transform(T input);
  }
  ```

---

Generics are one of the most powerful features in C#, enabling clean, reusable, and performant code. Would you like further examples or dive into advanced topics like covariance and contravariance in generics?
