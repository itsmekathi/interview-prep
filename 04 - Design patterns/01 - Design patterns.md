Design patterns are established solutions to common software design problems, helping to write clean, maintainable, and scalable code. Below is a comprehensive overview of various design patterns, categorized into Creational, Structural, and Behavioral patterns, along with C# examples for each.

## 1. Creational Design Patterns

Creational patterns focus on object creation mechanisms, trying to create objects in a manner suitable to the situation.

### a. Singleton Pattern

**Purpose**: Ensure a class has only one instance and provide a global point of access to it.

**Example**:

```csharp
public class Singleton
{
    private static Singleton _instance;
    private static readonly object _lock = new object();

    private Singleton() { }

    public static Singleton Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new Singleton();
                }
                return _instance;
            }
        }
    }
}
```

### b. Factory Pattern

**Purpose**: Provide an interface for creating objects but allow subclasses to alter the type of objects that will be created.

**Example**:

```csharp
public interface IAnimal
{
    void Speak();
}

public class Dog : IAnimal
{
    public void Speak() => Console.WriteLine("Woof!");
}

public class Cat : IAnimal
{
    public void Speak() => Console.WriteLine("Meow!");
}

public class AnimalFactory
{
    public static IAnimal GetAnimal(string animalType)
    {
        return animalType.ToLower() switch
        {
            "dog" => new Dog(),
            "cat" => new Cat(),
            _ => throw new ArgumentException("Invalid animal type")
        };
    }
}

// Usage
IAnimal animal = AnimalFactory.GetAnimal("dog");
animal.Speak(); // Output: Woof!
```

### c. Abstract Factory Pattern

**Purpose**: Provide an interface for creating families of related or dependent objects without specifying their concrete classes.

**Example**:

```csharp
public interface IAnimalFactory
{
    IAnimal CreateAnimal();
}

public class DogFactory : IAnimalFactory
{
    public IAnimal CreateAnimal() => new Dog();
}

public class CatFactory : IAnimalFactory
{
    public IAnimal CreateAnimal() => new Cat();
}

// Usage
IAnimalFactory factory = new DogFactory();
IAnimal animal = factory.CreateAnimal();
animal.Speak(); // Output: Woof!
```

### d. Builder Pattern

**Purpose**: Separate the construction of a complex object from its representation, allowing the same construction process to create different representations.

**Example**:

```csharp
public class Car
{
    public string Model { get; set; }
    public string Engine { get; set; }
}

public class CarBuilder
{
    private readonly Car _car = new Car();

    public CarBuilder SetModel(string model)
    {
        _car.Model = model;
        return this;
    }

    public CarBuilder SetEngine(string engine)
    {
        _car.Engine = engine;
        return this;
    }

    public Car Build() => _car;
}

// Usage
Car car = new CarBuilder()
    .SetModel("Tesla Model S")
    .SetEngine("Electric")
    .Build();
```

### e. Prototype Pattern

**Purpose**: Specify the kinds of objects to create using a prototypical instance, and create new objects by copying this prototype.

**Example**:

```csharp
public class Prototype
{
    public string Name { get; set; }

    public Prototype Clone() => (Prototype)this.MemberwiseClone();
}

// Usage
var original = new Prototype { Name = "Original" };
var clone = original.Clone();
clone.Name = "Clone";

Console.WriteLine(original.Name); // Output: Original
Console.WriteLine(clone.Name);     // Output: Clone
```

## 2. Structural Design Patterns

Structural patterns deal with object composition, creating relationships between objects.

### a. Adapter Pattern

**Purpose**: Allow incompatible interfaces to work together by converting the interface of a class into another interface that clients expect.

**Example**:

```csharp
public interface ITarget
{
    string Request();
}

public class Adaptee
{
    public string SpecificRequest() => "Specific Request";
}

public class Adapter : ITarget
{
    private readonly Adaptee _adaptee;

    public Adapter(Adaptee adaptee)
    {
        _adaptee = adaptee;
    }

    public string Request() => _adaptee.SpecificRequest();
}

// Usage
Adaptee adaptee = new Adaptee();
ITarget target = new Adapter(adaptee);
Console.WriteLine(target.Request()); // Output: Specific Request
```

### b. Bridge Pattern

**Purpose**: Decouple an abstraction from its implementation so that the two can vary independently.

**Example**:

```csharp
public abstract class Shape
{
    protected IDrawAPI drawAPI;

    protected Shape(IDrawAPI drawAPI)
    {
        this.drawAPI = drawAPI;
    }

    public abstract void Draw();
}

public class Circle : Shape
{
    private int x, y, radius;

    public Circle(int x, int y, int radius, IDrawAPI drawAPI)
        : base(drawAPI)
    {
        this.x = x;
        this.y = y;
        this.radius = radius;
    }

    public override void Draw() => drawAPI.DrawCircle(x, y, radius);
}

public interface IDrawAPI
{
    void DrawCircle(int x, int y, int radius);
}

public class RedCircle : IDrawAPI
{
    public void DrawCircle(int x, int y, int radius) =>
        Console.WriteLine($"Drawing Circle [color: red, x: {x}, y: {y}, radius: {radius}]");
}

// Usage
Shape circle = new Circle(10, 10, 5, new RedCircle());
circle.Draw(); // Output: Drawing Circle [color: red, x: 10, y: 10, radius: 5]
```

### c. Composite Pattern

**Purpose**: Compose objects into tree structures to represent part-whole hierarchies. It lets clients treat individual objects and compositions uniformly.

**Example**:

```csharp
public interface IComponent
{
    void Operation();
}

public class Leaf : IComponent
{
    public void Operation() => Console.WriteLine("Leaf operation");
}

public class Composite : IComponent
{
    private readonly List<IComponent> _children = new List<IComponent>();

    public void Add(IComponent component) => _children.Add(component);
    public void Remove(IComponent component) => _children.Remove(component);

    public void Operation()
    {
        Console.WriteLine("Composite operation");
        foreach (var child in _children)
        {
            child.Operation();
        }
    }
}

// Usage
Composite root = new Composite();
root.Add(new Leaf());
root.Add(new Leaf());
root.Operation();
// Output:
// Composite operation
// Leaf operation
// Leaf operation
```

### d. Decorator Pattern

**Purpose**: Allow behavior to be added to individual objects, either statically or dynamically, without affecting the behavior of other objects from the same class.

**Example**:

```csharp
public interface ICoffee
{
    string GetDescription();
    double Cost();
}

public class SimpleCoffee : ICoffee
{
    public string GetDescription() => "Simple Coffee";
    public double Cost() => 2.00;
}

public abstract class CoffeeDecorator : ICoffee
{
    protected readonly ICoffee _coffee;

    protected CoffeeDecorator(ICoffee coffee)
    {
        _coffee = coffee;
    }

    public abstract string GetDescription();
    public abstract double Cost();
}

public class MilkDecorator : CoffeeDecorator
{
    public MilkDecorator(ICoffee coffee) : base(coffee) { }

    public override string GetDescription() => _coffee.GetDescription() + ", Milk";

    public override double Cost() => _coffee.Cost() + 0.50;
}

// Usage
ICoffee coffee = new SimpleCoffee();
coffee = new MilkDecorator(coffee);
Console.WriteLine($"{coffee.GetDescription()}: ${coffee.Cost()}"); // Output: Simple Coffee, Milk: $2.50
```

### e. Facade Pattern

**Purpose**: Provide a simplified interface to a complex subsystem.

**Example**:

```csharp
public class SubsystemA
{
    public void OperationA() => Console.WriteLine("Subsystem A, Method A");
}

public class SubsystemB
{
    public void OperationB() => Console.WriteLine("Subsystem B, Method B");
}

public class Facade
{
    private readonly SubsystemA _a = new SubsystemA();
    private readonly SubsystemB _b = new SubsystemB();

    public void Operation()
    {
        _a.OperationA();
        _b.OperationB();
    }
}

// Usage
var facade = new Facade();
facade.Operation();
// Output:
// Subsystem A, Method A
// Subsystem B, Method B
```

## 3. Behavioral Design Patterns

Behavioral patterns deal with object interaction and responsibility.

### a. Observer Pattern

**Purpose**: Defines a one-to-many dependency between objects so that when one object changes state, all its dependents are notified and updated automatically.

**Example**:

```csharp
public interface IObserver
{
    void Update(string message);
}

public class ConcreteObserver : IObserver
{
    public string Name { get; }

    public ConcreteObserver(string name)
    {
        Name = name;
    }

    public void Update(string message) => Console.WriteLine($"{Name} received message: {message}");
}

public class Subject
{
    private readonly List<IObserver> _observers = new List<IObserver>();

    public void Attach(IObserver observer) => _observers.Add(observer);
    public void Detach(IObserver

 observer) => _observers.Remove(observer);

    public void Notify(string message)
    {
        foreach (var observer in _observers)
        {
            observer.Update(message);
        }
    }
}

// Usage
var subject = new Subject();
var observer1 = new ConcreteObserver("Observer 1");
var observer2 = new ConcreteObserver("Observer 2");

subject.Attach(observer1);
subject.Attach(observer2);
subject.Notify("Hello Observers!");
// Output:
// Observer 1 received message: Hello Observers!
// Observer 2 received message: Hello Observers!
```

### b. Strategy Pattern

**Purpose**: Defines a family of algorithms, encapsulates each one, and makes them interchangeable. This pattern lets the algorithm vary independently from clients that use it.

**Example**:

```csharp
public interface IStrategy
{
    int Execute(int a, int b);
}

public class AddStrategy : IStrategy
{
    public int Execute(int a, int b) => a + b;
}

public class SubtractStrategy : IStrategy
{
    public int Execute(int a, int b) => a - b;
}

public class Context
{
    private IStrategy _strategy;

    public void SetStrategy(IStrategy strategy) => _strategy = strategy;

    public int ExecuteStrategy(int a, int b) => _strategy.Execute(a, b);
}

// Usage
var context = new Context();

context.SetStrategy(new AddStrategy());
Console.WriteLine(context.ExecuteStrategy(3, 4)); // Output: 7

context.SetStrategy(new SubtractStrategy());
Console.WriteLine(context.ExecuteStrategy(7, 4)); // Output: 3
```

### c. Command Pattern

**Purpose**: Encapsulates a request as an object, thereby allowing for parameterization of clients with queues, requests, and operations.

**Example**:

```csharp
public interface ICommand
{
    void Execute();
}

public class Light
{
    public void TurnOn() => Console.WriteLine("Light is On");
    public void TurnOff() => Console.WriteLine("Light is Off");
}

public class TurnOnCommand : ICommand
{
    private readonly Light _light;

    public TurnOnCommand(Light light)
    {
        _light = light;
    }

    public void Execute() => _light.TurnOn();
}

public class TurnOffCommand : ICommand
{
    private readonly Light _light;

    public TurnOffCommand(Light light)
    {
        _light = light;
    }

    public void Execute() => _light.TurnOff();
}

// Usage
Light light = new Light();
ICommand turnOn = new TurnOnCommand(light);
ICommand turnOff = new TurnOffCommand(light);

turnOn.Execute(); // Output: Light is On
turnOff.Execute(); // Output: Light is Off
```

### d. Mediator Pattern

**Purpose**: Defines an object that encapsulates how a set of objects interact. Promotes loose coupling by preventing objects from referring to each other explicitly.

**Example**:

```csharp
public class Mediator
{
    public void Notify(object sender, string ev)
    {
        if (ev == "A")
        {
            Console.WriteLine("Mediator reacts to A");
        }
        else if (ev == "B")
        {
            Console.WriteLine("Mediator reacts to B");
        }
    }
}

public class ColleagueA
{
    private readonly Mediator _mediator;

    public ColleagueA(Mediator mediator)
    {
        _mediator = mediator;
    }

    public void DoSomething()
    {
        Console.WriteLine("Colleague A does something");
        _mediator.Notify(this, "A");
    }
}

// Usage
var mediator = new Mediator();
var colleagueA = new ColleagueA(mediator);
colleagueA.DoSomething(); // Output: Colleague A does something
                           // Mediator reacts to A
```

### e. State Pattern

**Purpose**: Allows an object to alter its behavior when its internal state changes. The object will appear to change its class.

**Example**:

```csharp
public interface IState
{
    void Handle(Context context);
}

public class ConcreteStateA : IState
{
    public void Handle(Context context)
    {
        Console.WriteLine("State A");
        context.State = new ConcreteStateB();
    }
}

public class ConcreteStateB : IState
{
    public void Handle(Context context)
    {
        Console.WriteLine("State B");
        context.State = new ConcreteStateA();
    }
}

public class Context
{
    private IState _state;

    public Context(IState state)
    {
        _state = state;
    }

    public IState State
    {
        get => _state;
        set
        {
            _state = value;
            _state.Handle(this);
        }
    }
}

// Usage
var context = new Context(new ConcreteStateA());
context.State.Handle(context); // Output: State A
context.State.Handle(context); // Output: State B
```

### Conclusion

Design patterns provide a structured approach to solving common software design problems. Understanding and implementing these patterns can significantly improve the quality and maintainability of your code. If you need further details or additional examples for any specific design pattern, feel free to ask!
