In .NET Core, you use the `dotnet` CLI (Command Line Interface) to create, build, and run applications. Here’s a quick overview of the basic commands:

---

### 1. **Creating a New Application**

To create a new application, use the `dotnet new` command:

#### Example:

```bash
dotnet new console -n MyApp
```

**Explanation:**

- `console`: Specifies the type of application (e.g., console, web, mvc, etc.).
- `-n MyApp`: Names the application "MyApp" (a folder with this name will be created).

Other templates you can use:

- `web`: For a basic web app.
- `mvc`: For an ASP.NET Core MVC app.
- `webapi`: For a Web API app.
- `classlib`: For a class library.

---

### 2. **Building the Application**

To compile the code, use the `dotnet build` command.

#### Example:

```bash
dotnet build
```

**Explanation:**

- It compiles the application in the current directory.
- The build output is usually placed in the `bin` folder (`bin/Debug/netX.X` by default).

---

### 3. **Running the Application**

To run the application, use the `dotnet run` command.

#### Example:

```bash
dotnet run
```

**Explanation:**

- This builds (if necessary) and then runs the application.

---

### 4. **Restoring Dependencies**

If your application depends on external libraries, you can restore those dependencies with:

```bash
dotnet restore
```

**Note:** This is usually unnecessary because `dotnet build` and `dotnet run` automatically restore dependencies.

---

### 5. **Listing Available Templates**

To see all available templates for creating projects:

```bash
dotnet new --list
```

---

### 6. **Additional Commands**

- **Publishing the Application**: For deployment, publish your application with:

  ```bash
  dotnet publish -c Release -o ./publish
  ```

  This produces a ready-to-deploy output in the specified folder (`./publish` here).

- **Testing the Application**: Run tests (if any) using:

  ```bash
  dotnet test
  ```

- **Adding a Package**: Add a NuGet package to your project with:

  ```bash
  dotnet add package PackageName
  ```

- **Viewing Help**: Get help for any command by appending `--help`. For example:
  ```bash
  dotnet new --help
  ```

---

These commands form the foundation of working with .NET Core projects. Let me know if you'd like details on any specific command!
