Data structures are essential components of programming that enable efficient organization, storage, and retrieval of data. In C#, several built-in data structures provide various functionalities for developers. Below are some of the most common data structures used in C# along with examples.

### 1. **Arrays**

**Description**: An array is a fixed-size collection of elements of the same type. It allows for efficient access to elements via their index.

**Example**:

```csharp
int[] numbers = new int[5]; // Declaration of an array of integers
numbers[0] = 10;
numbers[1] = 20;
numbers[2] = 30;

foreach (var number in numbers)
{
    Console.WriteLine(number); // Output: 10, 20, 30, 0, 0
}
```

### 2. **Lists**

**Description**: A `List<T>` is a dynamic array that can grow or shrink as needed. It is part of the `System.Collections.Generic` namespace.

**Example**:

```csharp
using System;
using System.Collections.Generic;

List<string> fruits = new List<string>();
fruits.Add("Apple");
fruits.Add("Banana");
fruits.Add("Cherry");

foreach (var fruit in fruits)
{
    Console.WriteLine(fruit); // Output: Apple, Banana, Cherry
}
```

### 3. **Linked List**

**Description**: A `LinkedList<T>` is a collection of nodes where each node contains a value and a reference to the next node. It allows for efficient insertions and deletions.

**Example**:

```csharp
using System;
using System.Collections.Generic;

LinkedList<int> linkedList = new LinkedList<int>();
linkedList.AddLast(1);
linkedList.AddLast(2);
linkedList.AddLast(3);

foreach (var item in linkedList)
{
    Console.WriteLine(item); // Output: 1, 2, 3
}
```

### 4. **Stacks**

**Description**: A `Stack<T>` is a Last-In-First-Out (LIFO) collection that allows adding and removing elements only from the top.

**Example**:

```csharp
using System;
using System.Collections.Generic;

Stack<string> stack = new Stack<string>();
stack.Push("First");
stack.Push("Second");
stack.Push("Third");

while (stack.Count > 0)
{
    Console.WriteLine(stack.Pop()); // Output: Third, Second, First
}
```

### 5. **Queues**

**Description**: A `Queue<T>` is a First-In-First-Out (FIFO) collection that allows adding elements at the end and removing them from the front.

**Example**:

```csharp
using System;
using System.Collections.Generic;

Queue<string> queue = new Queue<string>();
queue.Enqueue("First");
queue.Enqueue("Second");
queue.Enqueue("Third");

while (queue.Count > 0)
{
    Console.WriteLine(queue.Dequeue()); // Output: First, Second, Third
}
```

### 6. **Dictionaries**

**Description**: A `Dictionary<TKey, TValue>` is a collection of key-value pairs that allows for fast lookups by key. It is part of the `System.Collections.Generic` namespace.

**Example**:

```csharp
using System;
using System.Collections.Generic;

Dictionary<string, int> ages = new Dictionary<string, int>();
ages["Alice"] = 30;
ages["Bob"] = 25;

foreach (var kvp in ages)
{
    Console.WriteLine($"{kvp.Key} is {kvp.Value} years old."); // Output: Alice is 30 years old. Bob is 25 years old.
}
```

### 7. **HashSet**

**Description**: A `HashSet<T>` is a collection that contains no duplicate elements and has no specific order. It provides high-performance set operations like union, intersection, and difference.

**Example**:

```csharp
using System;
using System.Collections.Generic;

HashSet<string> uniqueNames = new HashSet<string>();
uniqueNames.Add("Alice");
uniqueNames.Add("Bob");
uniqueNames.Add("Alice"); // Duplicate entry, will not be added

foreach (var name in uniqueNames)
{
    Console.WriteLine(name); // Output: Alice, Bob
}
```

### 8. **SortedList**

**Description**: A `SortedList<TKey, TValue>` is a collection of key-value pairs that are sorted by the keys. It combines the features of a dictionary and a list.

**Example**:

```csharp
using System;
using System.Collections.Generic;

SortedList<int, string> sortedList = new SortedList<int, string>();
sortedList.Add(2, "Bob");
sortedList.Add(1, "Alice");
sortedList.Add(3, "Charlie");

foreach (var kvp in sortedList)
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value}"); // Output: 1: Alice, 2: Bob, 3: Charlie
}
```

### Conclusion

C# offers a rich set of data structures that can be utilized to solve various programming problems effectively. Understanding these data structures and their use cases can significantly improve the performance and maintainability of applications. If you have any specific questions about any of these data structures or need further examples, feel free to ask!
