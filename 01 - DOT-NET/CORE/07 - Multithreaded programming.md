### **Multi-threaded Programming in .NET Core**

**Multi-threaded programming** in .NET Core allows developers to execute multiple threads concurrently, enabling better use of system resources and improving application performance, especially in multi-core environments. It is a foundation for building high-performance, scalable, and responsive applications.

---

### **What is Multi-threading?**

Multi-threading is the ability of a program to manage multiple threads of execution within a single process. Each thread runs independently, allowing tasks to be executed concurrently.

---

### **Key Concepts in Multi-threading**

1. **Thread**:

   - A basic unit of execution within a process.
   - Managed in .NET using the `Thread` class in the `System.Threading` namespace.

2. **Thread Pool**:

   - A pool of worker threads managed by the runtime.
   - Provides efficient management of threads for short-lived operations.

3. **Tasks**:

   - High-level abstraction for asynchronous and parallel operations.
   - Managed using `Task` and `Task<T>`.

4. **Synchronization**:
   - Techniques to coordinate access to shared resources, such as `lock`, `Monitor`, `Mutex`, and `Semaphore`.

---

### **Thread Management in .NET Core**

#### **1. Using `Thread` Class**

The `Thread` class provides direct control over threads.

**Example**:

```csharp
using System;
using System.Threading;

class Program
{
    static void PrintNumbers()
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId} - {i}");
            Thread.Sleep(1000); // Simulate work
        }
    }

    static void Main()
    {
        Thread thread = new Thread(PrintNumbers);
        thread.Start(); // Starts the thread
        thread.Join();  // Waits for the thread to complete
    }
}
```

#### **2. Using Thread Pool**

The `ThreadPool` manages a pool of worker threads that can be reused for short tasks, reducing thread creation overhead.

**Example**:

```csharp
using System;
using System.Threading;

class Program
{
    static void PrintNumbers(object state)
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"ThreadPool Thread: {Thread.CurrentThread.ManagedThreadId} - {i}");
            Thread.Sleep(1000);
        }
    }

    static void Main()
    {
        ThreadPool.QueueUserWorkItem(PrintNumbers);
        Console.ReadLine(); // Keep the application running
    }
}
```

#### **3. Using Tasks (`Task` and `Task<T>`)**

Tasks are a high-level abstraction over threads, offering better management and scalability.

**Example**:

```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static async Task PrintNumbersAsync()
    {
        await Task.Run(() =>
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"Task Thread: {Task.CurrentId} - {i}");
                Task.Delay(1000).Wait();
            }
        });
    }

    static async Task Main()
    {
        await PrintNumbersAsync();
    }
}
```

---

### **Synchronization Techniques**

When multiple threads access shared resources, synchronization ensures data integrity and prevents race conditions.

#### **1. Locking with `lock`**

The `lock` keyword ensures that only one thread can access a critical section at a time.

**Example**:

```csharp
using System;
using System.Threading;

class Program
{
    private static readonly object _lock = new object();
    private static int _counter = 0;

    static void IncrementCounter()
    {
        lock (_lock)
        {
            _counter++;
            Console.WriteLine($"Counter: {_counter} - Thread: {Thread.CurrentThread.ManagedThreadId}");
        }
    }

    static void Main()
    {
        for (int i = 0; i < 5; i++)
        {
            Thread thread = new Thread(IncrementCounter);
            thread.Start();
        }
    }
}
```

#### **2. Using `Monitor`**

`Monitor` provides more control than `lock` for thread synchronization.

**Example**:

```csharp
using System;
using System.Threading;

class Program
{
    private static readonly object _lock = new object();

    static void PrintNumbers()
    {
        Monitor.Enter(_lock);
        try
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId} - {i}");
            }
        }
        finally
        {
            Monitor.Exit(_lock);
        }
    }

    static void Main()
    {
        Thread thread1 = new Thread(PrintNumbers);
        Thread thread2 = new Thread(PrintNumbers);

        thread1.Start();
        thread2.Start();
    }
}
```

#### **3. Mutex**

A `Mutex` is used for synchronization across multiple processes.

**Example**:

```csharp
using System;
using System.Threading;

class Program
{
    static void Main()
    {
        using (var mutex = new Mutex(false, "GlobalMutexExample"))
        {
            if (!mutex.WaitOne(TimeSpan.FromSeconds(5), false))
            {
                Console.WriteLine("Another instance is running.");
                return;
            }

            Console.WriteLine("Application is running.");
            Console.ReadLine(); // Simulate work
        }
    }
}
```

---

### **Parallel Programming**

#### **1. Parallel Loops**

`Parallel.For` and `Parallel.ForEach` execute loops concurrently.

**Example**:

```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Parallel.For(0, 10, i =>
        {
            Console.WriteLine($"Processing {i} - Thread: {Task.CurrentId}");
        });
    }
}
```

#### **2. Parallel Tasks**

Use `Parallel.Invoke` to run multiple actions in parallel.

**Example**:

```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Parallel.Invoke(
            () => Console.WriteLine("Task 1"),
            () => Console.WriteLine("Task 2"),
            () => Console.WriteLine("Task 3")
        );
    }
}
```

---

### **Semaphore in C# .NET**

A **Semaphore** is a synchronization primitive in .NET used to control access to a shared resource by multiple threads in a concurrent environment. It limits the number of threads that can access the resource simultaneously.

---

### **Types of Semaphores in .NET**

1. **`Semaphore`**:

   - A classic semaphore that works across multiple processes (system-wide).

2. **`SemaphoreSlim`**:
   - A lightweight version designed for single-process usage.
   - Recommended for most applications due to better performance and lower overhead.

---

### **How Does a Semaphore Work?**

- A semaphore maintains a count, which represents the number of threads that can access the resource at a given time.
- When a thread enters the semaphore:
  - The count decreases.
- When a thread exits the semaphore:
  - The count increases.
- If the count reaches zero, additional threads attempting to enter the semaphore will block until another thread exits.

---

### **Use Case of Semaphore**

#### **Scenario:**

Suppose a program downloads files from the internet. To avoid overloading the server or consuming too much bandwidth, you want to allow only **3 threads** to download files concurrently.

---

### **Simple Example Using `SemaphoreSlim`**

```csharp
using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static SemaphoreSlim semaphore = new SemaphoreSlim(3); // Limit to 3 threads

    static async Task DownloadFileAsync(int fileId)
    {
        Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} waiting to download File {fileId}...");
        await semaphore.WaitAsync(); // Wait for a slot
        try
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} downloading File {fileId}...");
            await Task.Delay(2000); // Simulate file download
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} completed downloading File {fileId}.");
        }
        finally
        {
            semaphore.Release(); // Release the slot
        }
    }

    static async Task Main(string[] args)
    {
        Task[] tasks = new Task[10];

        for (int i = 0; i < 10; i++)
        {
            int fileId = i + 1;
            tasks[i] = DownloadFileAsync(fileId);
        }

        await Task.WhenAll(tasks); // Wait for all tasks to complete
        Console.WriteLine("All files downloaded.");
    }
}
```

---

### **Explanation**

1. **`SemaphoreSlim semaphore = new SemaphoreSlim(3);`**

   - Creates a semaphore that allows up to **3 threads** to enter concurrently.

2. **`semaphore.WaitAsync();`**

   - Asynchronously waits for a thread slot. If the count is greater than zero, it decrements the count and allows the thread to proceed. Otherwise, the thread waits.

3. **`semaphore.Release();`**

   - Increments the count, signaling that a thread slot is available.

4. **Tasks and Concurrency**:
   - The program starts 10 tasks, but only 3 can download files at the same time due to the semaphore.

---

### **Output Example**

```
Thread 1 waiting to download File 1...
Thread 2 waiting to download File 2...
Thread 3 waiting to download File 3...
Thread 1 downloading File 1...
Thread 2 downloading File 2...
Thread 3 downloading File 3...
Thread 4 waiting to download File 4...
Thread 5 waiting to download File 5...
Thread 1 completed downloading File 1.
Thread 1 downloading File 4...
Thread 2 completed downloading File 2.
Thread 2 downloading File 5...
...
All files downloaded.
```

---

### **Real-World Use Cases**

1. **Throttling Concurrent Operations**:

   - Managing API calls, database connections, or file downloads.

2. **Limiting Access to Shared Resources**:

   - Controlling access to a fixed pool of resources (e.g., a connection pool).

3. **Concurrent Processing**:
   - Ensuring a manageable level of parallelism in data processing.

---

### **Best Practices for Multi-threading**

1. **Avoid Over-Threading**:

   - Excessive threads can lead to context-switching overhead and degrade performance.

2. **Use High-Level APIs**:

   - Prefer `Task` and `Parallel` APIs over manual thread management.

3. **Minimize Locking**:

   - Locking impacts performance; use fine-grained locking or lock-free techniques.

4. **Handle Exceptions Gracefully**:

   - Ensure proper exception handling for all threads.

5. **Use Cancellation Tokens**:

   - Provide a way to cancel long-running threads or tasks.

6. **Profile and Optimize**:
   - Use tools like **dotTrace**, **PerfView**, or **Visual Studio Profiler** to analyze thread behavior.

---

### **Summary**

Multi-threaded programming in .NET Core provides powerful mechanisms for executing tasks concurrently, making applications responsive and efficient. By leveraging threads, thread pools, tasks, and synchronization primitives, developers can build robust applications that utilize system resources effectively. High-level abstractions like `Task` and `Parallel` APIs simplify threading while maintaining scalability and reliability.
