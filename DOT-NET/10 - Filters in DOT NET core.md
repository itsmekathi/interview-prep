In ASP.NET Core, **filters** are a powerful mechanism that allows developers to execute custom logic at various points in the request processing pipeline, particularly during the execution of controller actions. Filters can be used for a variety of tasks, such as authorization, logging, caching, handling exceptions, and modifying the request or response.

### **Types of Filters**

ASP.NET Core supports several types of filters, each serving a specific purpose:

1. **Authorization Filters**:

   - These filters are executed first and are responsible for checking if the user is authorized to access a particular resource. If authorization fails, the action is not executed, and an appropriate response is returned.
   - **Example**: `[Authorize]` attribute is a built-in authorization filter.

2. **Action Filters**:

   - Action filters run before and after an action method executes. They can be used to perform operations such as logging, modifying the result, or changing the input parameters.
   - **Example**: Custom action filters can be created by implementing the `IActionFilter` interface.

3. **Result Filters**:

   - Result filters execute after the action method has executed but before the result is sent to the client. They are often used to modify the response, such as adding headers or formatting the response data.
   - **Example**: Implementing `IResultFilter` allows you to create custom result filters.

4. **Exception Filters**:

   - Exception filters handle exceptions that occur during the execution of an action method. They provide a centralized way to manage error handling and can return custom error responses.
   - **Example**: Implementing `IExceptionFilter` or using the `[ServiceFilter]` attribute to use a custom service that implements error handling logic.

5. **Resource Filters**:
   - Resource filters run before any other type of filter and are used to implement logic that applies to all actions in a controller or application, such as caching.
   - **Example**: Implementing `IResourceFilter` can be used for tasks like response caching.

### **Filter Pipeline Order**

The order of filter execution is important, and ASP.NET Core follows a specific order:

1. Authorization filters
2. Resource filters
3. Action filters (before the action method)
4. Action method execution
5. Action filters (after the action method)
6. Result filters
7. Exception filters (if an exception occurs)

### **Creating Custom Filters**

Developers can create custom filters by implementing one of the filter interfaces or deriving from the base filter classes. Here’s how to create different types of filters:

#### **1. Creating an Action Filter**

You can create a custom action filter by implementing the `IActionFilter` interface:

```csharp
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

public class CustomActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Logic before the action executes
        Console.WriteLine("Before Action Executing");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Logic after the action executes
        Console.WriteLine("After Action Executed");
    }
}
```

#### **2. Using the Action Filter**

You can apply the action filter globally, at the controller level, or at the action method level:

```csharp
[ServiceFilter(typeof(CustomActionFilter))] // For dependency injection
public class MyController : Controller
{
    [ServiceFilter(typeof(CustomActionFilter))]
    public IActionResult MyAction()
    {
        return Ok("Hello World");
    }
}
```

#### **3. Creating an Exception Filter**

You can create a custom exception filter by implementing the `IExceptionFilter` interface:

```csharp
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        // Handle the exception and set a custom response
        context.Result = new ObjectResult(new { error = context.Exception.Message })
        {
            StatusCode = 500 // Internal Server Error
        };
        context.ExceptionHandled = true; // Mark exception as handled
    }
}
```

#### **4. Using the Exception Filter**

You can apply the exception filter globally or at the controller or action level:

```csharp
[ServiceFilter(typeof(CustomExceptionFilter))]
public class MyController : Controller
{
    public IActionResult MyAction()
    {
        throw new Exception("An error occurred");
    }
}
```

### **Global Filters**

You can register filters globally in the `Startup.cs` file using the `AddMvc` method:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews(options =>
    {
        options.Filters.Add<CustomActionFilter>(); // Register global action filter
        options.Filters.Add<CustomExceptionFilter>(); // Register global exception filter
    });
}
```

### **Filter Attributes**

ASP.NET Core provides several built-in attributes that can be used as filters:

- `[Authorize]`: Authorizes users based on roles, policies, or claims.
- `[AllowAnonymous]`: Allows anonymous access to specific actions or controllers.
- `[ValidateAntiForgeryToken]`: Validates anti-forgery tokens to prevent CSRF attacks.
- `[ServiceFilter]`: Enables dependency injection for filters that require services.
- `[TypeFilter]`: Similar to `ServiceFilter`, but for filters that require constructor parameters.

### **Conclusion**

Filters in ASP.NET Core provide a powerful way to implement cross-cutting concerns in your web applications. They allow you to encapsulate logic such as authorization, exception handling, and logging, enhancing the maintainability and organization of your code. By understanding and utilizing filters effectively, you can create robust and efficient ASP.NET Core applications.
