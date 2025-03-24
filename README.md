# **Community Management API**

## **Project Overview**

The **Community Management API** is a RESTful web service built using **ASP.NET Core, Dapper, and SQL Server**. It provides functionalities for managing users, blog posts, categories, and comments. This project follows best practices such as **dependency injection, repository pattern, and stored procedures** for database operations.

---

## **Key Features**

### **🔹 User Management**

* Create, update, delete, and retrieve user details.

* Authenticate users to perform actions based on permissions.

  ### **🔹 Blog Post Management**

* Registered users can create, update, and delete blog posts.

* Only the **post owner** can modify or delete their own posts.

* Unregistered users can only view and search for blog posts.

  ### **🔹 Category Management**

* Create and organize blog posts into different categories.

* Retrieve all categories to filter posts effectively.

  ### **🔹 Comment System**

* Registered users can add comments on blog posts.

* **Users cannot comment on their own posts.**

* Retrieve all comments for a specific post.

  ---

  ## **Technologies Used**

✅ **ASP.NET Core 6+** – For building the API.  
 ✅ **Dapper ORM** – For fast and efficient database operations.  
 ✅ **SQL Server** – To store users, blog posts, comments, and categories.  
 ✅ **Dependency Injection** – For managing application services efficiently.  
 ✅ **Repository Pattern** – For structured and scalable data handling.  
 ✅ **Stored Procedures** – For optimized database queries and actions.  
 ✅ **Swagger (OpenAPI)** – For API documentation and testing.

---

## **Project Structure**

📂 **Community.Repository.Entities** – Contains entity classes (User, BlogPost, Comment, etc.).  
 📂 **Community.Repository.Interfaces** – Defines interfaces for repository methods.  
 📂 **Community.Repository.Repos** – Implements repositories using Dapper.  
 📂 **Community.Controllers** – API controllers handling HTTP requests.  
 📂 **CommunityContext** – Manages database connections.
