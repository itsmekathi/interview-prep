Here’s a list of the top 20 SQL coding interview questions along with their answers:

### 1. What is SQL?

**Answer**: SQL (Structured Query Language) is a standard programming language used to manage and manipulate relational databases. It allows for querying, updating, and managing data within a database.

### 2. What is the difference between `UNION` and `UNION ALL`?

**Answer**:

- **UNION** combines the result sets of two or more SELECT statements and removes duplicate rows.
- **UNION ALL** combines the result sets without removing duplicates, resulting in potentially faster performance.

### 3. Write a SQL query to find the duplicate records in a table.

**Answer**:

```sql
SELECT column_name, COUNT(*)
FROM table_name
GROUP BY column_name
HAVING COUNT(*) > 1;
```

### 4. How do you retrieve the first 10 rows from a table?

**Answer**:

```sql
SELECT *
FROM table_name
ORDER BY column_name
OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY;  -- For SQL Server and PostgreSQL
```

### 5. Write a SQL query to get the third highest salary from a table named `Employees`.

**Answer**:

```sql
SELECT DISTINCT Salary
FROM Employees
ORDER BY Salary DESC
OFFSET 2 ROWS FETCH NEXT 1 ROWS ONLY;  -- SQL Server and PostgreSQL
```

### 6. What is a subquery? Provide an example.

**Answer**: A subquery is a query nested within another query. It can be used in the SELECT, INSERT, UPDATE, or DELETE statement.

**Example**:

```sql
SELECT FirstName
FROM Employees
WHERE EmployeeID IN (SELECT EmployeeID FROM Departments WHERE DepartmentName = 'Sales');
```

### 7. Explain the difference between `INNER JOIN` and `LEFT JOIN`.

**Answer**:

- **INNER JOIN** returns only the rows with matching values in both tables.
- **LEFT JOIN** returns all rows from the left table and matched rows from the right table, with NULLs for non-matching rows.

### 8. How can you find the total number of employees in each department?

**Answer**:

```sql
SELECT DepartmentID, COUNT(*) AS TotalEmployees
FROM Employees
GROUP BY DepartmentID;
```

### 9. What is normalization? Explain its types.

**Answer**: Normalization is the process of organizing data in a database to minimize redundancy and dependency. Common types of normalization include:

- **1NF (First Normal Form)**: Eliminate repeating groups.
- **2NF (Second Normal Form)**: Remove partial dependencies.
- **3NF (Third Normal Form)**: Remove transitive dependencies.

### 10. Write a SQL query to get the employees whose salary is higher than the average salary.

**Answer**:

```sql
SELECT *
FROM Employees
WHERE Salary > (SELECT AVG(Salary) FROM Employees);
```

### 11. What is a primary key, and how is it different from a foreign key?

**Answer**:

- A **primary key** is a unique identifier for each record in a table, ensuring that no two rows have the same value.
- A **foreign key** is a field in one table that refers to the primary key in another table, establishing a relationship between the two tables.

### 12. Write a SQL query to delete duplicate rows from a table.

**Answer**:

```sql
WITH CTE AS (
    SELECT *, ROW_NUMBER() OVER (PARTITION BY column_name ORDER BY (SELECT NULL)) AS rn
    FROM table_name
)
DELETE FROM CTE WHERE rn > 1;
```

### 13. What are aggregate functions in SQL? Provide examples.

**Answer**: Aggregate functions perform a calculation on a set of values and return a single value. Common aggregate functions include:

- **COUNT()**: Counts the number of rows.
- **SUM()**: Calculates the sum of a numeric column.
- **AVG()**: Calculates the average of a numeric column.
- **MIN()**: Returns the minimum value.
- **MAX()**: Returns the maximum value.

**Example**:

```sql
SELECT COUNT(*) AS TotalEmployees, AVG(Salary) AS AverageSalary
FROM Employees;
```

### 14. Explain what a view is and its advantages.

**Answer**: A view is a virtual table based on the result of a SELECT query. Advantages of using views include:

- Simplifying complex queries.
- Providing a layer of security by restricting access to specific columns or rows.
- Encapsulating complex logic.

### 15. Write a SQL query to update the salary of an employee based on their ID.

**Answer**:

```sql
UPDATE Employees
SET Salary = Salary * 1.10  -- Increase salary by 10%
WHERE EmployeeID = 1;  -- Replace 1 with the specific ID
```

### 16. How can you handle NULL values in SQL?

**Answer**: You can handle NULL values using:

- **IS NULL**: To check if a value is NULL.
- **IS NOT NULL**: To check if a value is not NULL.
- **COALESCE()**: To return the first non-null value in a list.
- **IFNULL() or NVL()**: To replace NULL with a specified value.

### 17. Write a SQL query to retrieve the employees who joined in the last 30 days.

**Answer**:

```sql
SELECT *
FROM Employees
WHERE JoinDate >= DATEADD(DAY, -30, GETDATE());
```

### 18. What is a stored procedure, and how is it different from a function?

**Answer**: A stored procedure is a precompiled collection of SQL statements that can perform operations on the database. Unlike functions, which must return a single value and cannot modify database state, stored procedures can return multiple values and modify data.

### 19. Write a SQL query to find the maximum salary from each department.

**Answer**:

```sql
SELECT DepartmentID, MAX(Salary) AS MaxSalary
FROM Employees
GROUP BY DepartmentID;
```

### 20. Explain the concept of transactions in SQL.

**Answer**: A transaction is a sequence of operations performed as a single logical unit of work. Transactions ensure data integrity and consistency. They follow the ACID properties:

- **Atomicity**: Ensures that all operations within the transaction are completed successfully or none at all.
- **Consistency**: Ensures the database changes from one valid state to another.
- **Isolation**: Ensures that transactions occur independently without interference.
- **Durability**: Ensures that once a transaction is committed, it will remain so, even in the event of a system failure.

**Example of a transaction**:

```sql
BEGIN TRANSACTION;

UPDATE Accounts SET Balance = Balance - 100 WHERE AccountID = 1;
UPDATE Accounts SET Balance = Balance + 100 WHERE AccountID = 2;

COMMIT TRANSACTION;  -- Or ROLLBACK TRANSACTION if needed
```

This list covers a wide range of SQL topics and can serve as a valuable resource for interview preparation or further study. If you need any specific clarifications or additional topics, feel free to ask!
