# SAMAS 2.0

---



#  .NET Core vs .NET Framework
---

**Overview:**

- **.NET Core**: A cross-platform, high-performance, open-source framework for building modern, cloud-based, internet-connected applications.
- **.NET Framework**: A Windows-only framework used for building and running desktop and web applications.
**Key Differences:**

| Feature | .NET Core | .NET Framework |
| ----- | ----- | ----- |
| Cross-platform | Yes (Windows, macOS, Linux) | No (Windows only) |
| Performance | Higher performance, optimized for modern workloads | Good performance, but less optimized for modern needs |
| Deployment | Flexible deployment (self-contained, side-by-side) | Limited deployment options |
| Microservices | Ideal for microservices and containers | Not optimized for microservices |
| Open-source | Fully open-source | Partially open-source |
**Key Points:**

- **Modernization**: .NET Core is the preferred choice for new applications and modernization of existing apps due to its performance and cross-platform capabilities.
- **Ecosystem**: .NET Core has a vibrant, growing ecosystem with regular updates and improvements.


# .NET Core Web API
---

**Overview:**

- A framework for building HTTP services that can be accessed from various clients, including browsers and mobile devices.
#### **Key Points**
1. **Lightweight and Modular**: Designed for building modern web apps and services.
2. **Cross-Platform**: Can run on Windows, Linux, and macOS.
3. **High Performance**: Optimized for speed and efficiency.
4. **Middleware**: Simplifies the processing pipeline with custom middleware components.


![image.png](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/5M8pQnYTZFkK8WxwBwxOK.png?ixlib=js-3.7.0 "image.png")





# **Cross**-**Origin Resource Sharing** (CORS)
---

### What is CORS?
CORS stands for Cross-Origin Resource Sharing. It is a security feature implemented by web browsers that controls access to resources from a different origin (domain, protocol, or port) than the one that served the original web page.



### Why CORS is Needed?
Web browsers enforce the Same-Origin Policy, which means by default, scripts running on a web page can only make requests to the same origin (domain, protocol, and port) as the page itself. CORS allows servers to specify who can access their resources, extending the flexibility of web applications.



### How CORS Works?
When a browser makes a request across origins, it sends an HTTP request with an `Origin` header that indicates where the request originated. The server then responds with headers indicating whether the request is allowed. These headers include:

- `Access-Control-Allow-Origin` : Specifies which origins are allowed to access the resource.
- `Access-Control-Allow-Methods` : Specifies the HTTP methods (GET, POST, etc.) allowed when accessing the resource.
- `Access-Control-Allow-Headers` : Specifies which headers can be used in the actual request.


## Same origin
Two URLs have the same origin if they have identical schemes, hosts, and ports.

These two URLs have the same origin:

- `https://example.com/foo.html` 
- `https://example.com/bar.html` 
These URLs have different origins than the previous two URLs:

- `https://example.net` : Different domain
- `https://www.example.com/foo.html` : Different subdomain
- `http://example.com/foo.html` : Different scheme
- `https://example.com:9000/foo.html` : Different port




### Ways to Enable CORS in .NET Core
In .NET Core, you can enable CORS in several ways depending on your application architecture and requirements:

- **Global CORS Policy**: Apply CORS globally to all controllers and actions in your application.
```
// In ConfigureServices method of Startup.cs
services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
```
And then, in `Configure` method:

```
// In Configure method of Startup.cs
app.UseCors("MyPolicy");
```
- **Controller-level CORS**: Apply CORS to specific controllers or actions.
```
[EnableCors("MyPolicy")]
public class MyController : ControllerBase
{
    // Controller actions
}
```
- **Middleware-based CORS**: Implement CORS as a middleware if you need more control over the CORS process.
```
// In Configure method of Startup.cs
app.UseMiddleware<CorsMiddleware>();
```
Where `CorsMiddleware`  is a custom middleware handling CORS headers.



### Summary
CORS is essential for allowing secure cross-origin requests in web applications. By configuring CORS policies in your .NET Core application, you can specify which origins, methods, and headers are permitted to access your resources, ensuring both security and flexibility.











# Domain-Driven Design (DDD)
---

Domain-Driven Design is a way to design software by focusing on the core business (domain) and its logic. It helps create a clear structure that matches the business needs.



### Key Concepts
1. **Domain**:
    - This is the area of knowledge or activity that your software is about.
    - Example: If you're building software for a library, the domain is "library management."
2. **Entities**:
    - Objects that have a distinct identity.
    - Example: A book in the library system. Even if two books have the same title, each book has a unique ID.
3. **Value Objects**:
    - Objects that don’t have a distinct identity and are defined by their attributes.
    - Example: An address (Street, City, Zip Code). Two addresses are the same if all their attributes are the same.
4. **Aggregates**:
    - A group of related objects treated as a single unit.
    - Example: An order in an e-commerce system, which includes the order itself, line items, and payment details.
5. **Repositories**:
    - A way to access and store aggregates.
    - Example: A repository that handles storing and retrieving book data from the database.
6. **Services**:
    - Operations or business logic that don't naturally fit within entities or value objects.
    - Example: A service to calculate the total price of an order.


### Summary
1. **Understand the domain**: Know the business area.
2. **Create entities and value objects**: Represent the core data and logic.
3. **Create aggregates**: Group related entities and value objects.
4. **Create repositories**: Handle data storage and retrieval.
5. **Create services**: Implement business logic.
6. **Create controllers**: Handle HTTP requests.






# Clean Architecture
---

Building scalable, maintainable, and testable applications is paramount in the ever-evolving landscape of software development. One architectural approach that has gained significant popularity for achieving these goals is Clean Architecture.



The Clean Architecture pattern organizes the application into four layers:

1. Domain: This is the innermost layer that houses the core business logic, which is encapsulated in entities and value objects.
2. Application: This layer defines use cases and acts as an intermediary between the Domain and the outer layers.
3. Infrastructure: This layer provides concrete implementations of the interfaces defined in the inner layers. It may include operations related to databases, file systems, and other external resources.
4. Presentation: This is the outermost layer responsible for presenting information to the user and receiving user input.


![image-removebg-preview (1).png](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/nex4wKZin1dZKmWpSUUib.png?ixlib=js-3.7.0 "image-removebg-preview (1).png")





![Clean Architecture Diagram.png](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/1vJ82bBRc6oBhdVVngdp5.png?ixlib=js-3.7.0 "Clean Architecture Diagram.png")

####                                                                                  Clean Architecture


# Step 1: Organize the Solution
To start, create a blank solution in Visual Studio and add four projects to it, one for each layer:

- **Domain** (Class Library)
- **Application** (Class Library)
- **Infrastructure** (Class Library)
- **WebUI** (ASP.NET Core Web Application)


# Folder Structure
![1_BaUqBgsbjXiYS2NAP9V51w.webp](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/0wd2VOesKjXSRBqPV81Dc.webp?ixlib=js-3.7.0 "1_BaUqBgsbjXiYS2NAP9V51w.webp")



# Step 2: Define Entities (Domain Layer)
In the Domain project, define your entities and value objects. For example, for a blogging application, we could have a `Post` entity:

![1_BgipMZOwQ_2Z_uJ2UF2bNw.webp](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/kKMt7fib_ogIbObF5Uhyk.webp?ixlib=js-3.7.0 "1_BgipMZOwQ_2Z_uJ2UF2bNw.webp")





# Step 3: Define Interfaces (Application Layer)
In the Application layer, define the interfaces that the outer layers will implement. Here’s an example of a repository interface:

![1_iahprxiG2Aj7i6r_DW0SmQ.webp](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/nHuAIbsklrs2wQSI_aSoG.webp?ixlib=js-3.7.0 "1_iahprxiG2Aj7i6r_DW0SmQ.webp")



# Step 4: Implement Interfaces (Infrastructure Layer)
In the Infrastructure project, implement the interfaces defined in the Application layer. This is where your application will interact with external systems like databases and file systems:

![1_WQKdUbLpgeXLkR38rel9Kg.webp](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/wCcl6Nz_UXNmQ4GAUk3A6.webp?ixlib=js-3.7.0 "1_WQKdUbLpgeXLkR38rel9Kg.webp")





# Step 5: Build User Interfaces (WebUI Layer)
The WebUI layer is where you interact with users. Leverage Dependency Injection to utilize services defined in the Infrastructure layer:

![1_S91D0ELB-zDDnDUSfCPg8Q.webp](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/jCc5JUExRjSNemjwCu0O7.webp?ixlib=js-3.7.0 "1_S91D0ELB-zDDnDUSfCPg8Q.webp")



In the example above, `PostsController` injects an instance of `IPostRepository`, which was implemented in the Infrastructure layer. The controller provides two endpoints: a GET for all posts and a GET for a single post by ID. Other endpoints for creating, updating, or deleting a post can be added similarly.

Remember to register the `IPostRepository` and its implementation (`PostRepository`) in the dependency injection container. This is typically done in the `Startup.cs` file of the WebUI project:



![1_ajJK-21QEGhCrVxB5DaELA.webp](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/yqZ801U-aN9bMh97NbcYB.webp?ixlib=js-3.7.0 "1_ajJK-21QEGhCrVxB5DaELA.webp")



This ensures that an instance of `PostRepository` is provided whenever `IPostRepository` is required in your controllers or other services.





---



### Summary
- **DDD**: Best for complex domains where business logic is the focus. It provides a rich domain model and ensures the core business logic is independent.
- **Clean Architecture**: Best for creating maintainable, flexible, and testable systems. It ensures separation of concerns and manages dependencies effectively.
Both can be combined. You can use DDD principles within a Clean Architecture framework to get the best of both worlds: a well-modeled domain and a clean, maintainable structure.

















# CQRS (Command Query Responsibility Segregation)
---

## What is CQRS?
CQRS stands for Command Query Responsibility Segregation. It's a design pattern that helps manage complex data in software systems by separating operations that read data (queries) from operations that write data (commands).

## Why use CQRS?
As software systems become more complex, traditional approaches to handling data can become inadequate. CQRS addresses this by splitting the responsibility for reading and writing data into separate components, which can improve performance, scalability, and security.

## Basic Architecture of CQRS
### Commands
Commands are instructions that trigger changes in the application's state, such as creating, updating, or deleting data. They do not return data but modify the application's state.

### Command Handlers
Command Handlers receive commands, interpret them, and execute the corresponding operations (like updating a database). They then emit events to indicate the success or failure of the operation.

### Queries
Queries are used to retrieve data from the application. They do not modify data but return requested information. Queries are typically used for displaying data to users.

### Query Handlers
Query Handlers receive queries, process them, and retrieve data from the appropriate data store (like a database). They return the requested data to the user interface for display.



![CQRS-new.webp](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/hCrpF2agUgfDU8YyYbGBw.webp?ixlib=js-3.7.0 "CQRS-new.webp")

![What-is-CQRS-Design-Pattern.webp](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/tILZhafXZSHcHuEsDZbIe.webp?ixlib=js-3.7.0 "What-is-CQRS-Design-Pattern.webp")



## **Example of CQRS Design Pattern**


![Example-of-CQRS-Design-Pattern-.webp](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/ui30EXvAbRXySPahNRZVz.webp?ixlib=js-3.7.0 "Example-of-CQRS-Design-Pattern-.webp")



Let’s understand CQRS Design Pattern through the example of E-commerce Website.

In our Ordering E-commerce microservices architecture, we’re introducing a new approach to database design using the CQRS pattern. We’ve decided to split our databases into two separate parts to better manage our data and improve performance.

- Firstly, we’ll have a write database that focuses on handling all write operations, such as creating and updating orders. This database will be optimized for transactional consistency and relational data modeling, making it suitable for managing the core data changes.
- Secondly, we’ll introduce a read database dedicated to handling read operations, such as querying for order details and order history. This database will be designed for high performance and scalability, using a NoSQL database like MongoDB or Cassandra.
### Synchronization
To keep these two databases in sync, we’ll implement a messaging system using Apache Kafka. Kafka’s publish/subscribe model will allow us to propagate changes from the write database to the read database in real-time, ensuring that the data remains consistent across both databases.



### Tech-Stack
For the tech stack, we’re considering using MySQL or PostgreSQL for the write database due to their strong support for ACID transactions and relational data modeling. For the read database, we’re leaning towards using MongoDB or Cassandra for their scalability and ability to handle large volumes of data efficiently.

By adopting the CQRS pattern and splitting our databases in this way, we believe we can improve the performance and scalability of our Ordering microservices, providing a better experience for our users.















# Mediator design pattern
---

The Mediator pattern is a way to organize how objects interact with each other. It helps keep things organized and prevents chaos in complex systems.

![Mediator-Design-Pattern.webp](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/2B1TwNMOV-pUPrmNAGJQQ.webp?ixlib=js-3.7.0 "Mediator-Design-Pattern.webp")

**Real-Life Analogy:**

Imagine a group project in a classroom:

- **Colleagues (Students)**: Each student has their own tasks but needs to work together on the project.
- **Mediator (Teacher)**: The teacher acts as the mediator. Instead of students talking directly to each other, they talk to the teacher.
**How it Works:**

- If a student needs something from another, they ask the teacher.
- The teacher figures out how to get the needed information from the other students.
- Then, the teacher gives the information back to the student who asked.
This way, students communicate through the teacher, keeping things organized without directly dealing with each other’s details.



**Components of the Mediator Design Pattern:**

1. **Mediator**: The mediator sets the rules for how colleagues (students) should communicate.
2. **Colleague**: Colleagues are like students who only talk to the mediator (teacher), not directly to each other.
3. **Concrete Mediator**: This is the actual teacher who coordinates and manages how students interact.
4. **Concrete Colleague**: These are specific students who follow the rules set by the mediator (teacher) to communicate.


![MediatorPatternClassDiagram.webp](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/Toj-7J7m9SMtqRF3qjL3j.webp?ixlib=js-3.7.0 "MediatorPatternClassDiagram.webp")



**How They Interact:**

- A student tells the teacher (mediator) when they need something from another student.
- The teacher (mediator) decides how to help and tells the other students (colleagues) what to do.
- Colleagues (students) work together through the teacher (mediator), keeping their tasks separate and organized.


**Example:**

In an airport:

- **Challenge**: Airplanes need to coordinate their movements to avoid collisions.
- **Without Mediator**: Direct communication between planes could lead to confusion and accidents.
- **With Mediator**: Air traffic control acts as the mediator, organizing and directing airplane movements safely.


![MediatorDesignPatternExample.webp](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/xUFUqd37DMXuuBVWNSkq0.webp?ixlib=js-3.7.0 "MediatorDesignPatternExample.webp")



**Benefits:**

- **Centralized Control**: Mediator (like air traffic control) ensures all planes know where others are and what they're doing.
- **Safety**: Reduces risks like collisions by organizing and managing interactions.




## MediatR is a popular .NET library that implements the Mediator pattern.








# MediatR with CQRS
---

_**MediatR **_is a library for implementing the mediator pattern in C#. It simplifies the implementation of CQRS by providing an easy way to manage command and query handlers. MediatR acts as a mediator that routes commands and queries to their respective handlers.



### Why use MediatR with CQRS?
1. **Easier Code Organization**: MediatR helps us keep our code organized by managing how commands and queries are sent to the right parts of our application.
2. **No More Tangled Code**: It keeps things neat by making sure that when we send a command or a query, it goes straight to its handler without us having to worry about all the details.


### Key Features of MediatR:
1. **Decoupling**: This means it separates different parts of our code so they don't depend too much on each other. It's like keeping things tidy in different boxes.
2. **Adding Extra Features Easily**: MediatR lets us add extra things to our commands and queries, like checking if they're allowed or logging what happens, without making our main code messy.
3. **Finding Things Automatically**: It's clever enough to find where the code that handles our commands and queries lives, so we don't have to tell it every time.


### In Short:
MediatR with CQRS is like having a helpful organizer for our code. It makes sure everything goes where it should, keeps things separate when needed, and makes our job of building software a lot easier.





# Implementing CQRS with MediatR
---

## _1. Create a .NET Core  Web Application_
## 2. Install MediatR Package
```
﻿dotnet add package MediatR 
```
## 3. Define Commands and Queries
```
public class CreateProductCommand : IRequest<int>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class GetProductQuery : IRequest<Product>
{
    public int Id { get; set; }
}
```
## 4. Create Command Handlers
To implement the `CreateProductCommandHandler`, you can simulate the creation of a product in a fake service or repository. Here's an example implementation of the `CreateProductCommandHandler`:

```
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DemoCQRSExample
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        // Define a fake data source (in-memory list)
        private readonly List<Product> _products = new List<Product>();
        private int _nextProductId = 1;

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Simulate creating a new product
            var product = new Product
            {
                Id = _nextProductId++,
                Name = request.Name,
                Price = request.Price
            };

            // Add the product to the fake data source
            _products.Add(product);

            // Return the newly created product's ID
            return product.Id;
        }
    }
}
```
In this implementation:

- We maintain an in-memory list `_products`  to simulate a fake data source.
- The `_nextProductId`  variable is used to generate unique IDs for each newly created product.
- In the `Handle`  method, we create a new `Product`  object based on the data provided in the `CreateProductCommand` . We increment `_nextProductId`  to ensure each product has a unique ID.
- We then add the newly created product to the `_products`  list.
- Finally, we return the ID of the newly created product.
This is a simplified example for demonstration purposes. In a real-world application, you would replace this fake implementation with actual code to persist the product data, such as storing it in a database or another data store.

## 5. Create Query Handlers
In the `GetProductQueryHandler`, you can implement a fake service that simulates retrieving a product by ID from a data source. Here's an example implementation using a simple in-memory list as a fake data source:

```
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoCQRSExample
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
    {
        // Define a fake data source (in-memory list)
        private readonly List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Product A", Price = 10.99m },
            new Product { Id = 2, Name = "Product B", Price = 19.99m },
            // Add more fake products as needed
        };

        public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            // Simulate retrieving a product by ID from the fake data source
            // You can take the records from database also.
        
            var product = _products.FirstOrDefault(p => p.Id == request.Id);

            if (product == null)
            {
                // If the product is not found, you can throw an exception or return null
                throw new NotFoundException($"Product with ID {request.Id} not found.");
            }

            return product;
        }
    }
}
```
In this implementation:

- We have a simple list `_products`  that represents the fake data source containing some sample products.
- In the `Handle`  method, we use LINQ to search for a product with the specified ID in the list. If found, we return the product; otherwise, we throw a custom exception indicating that the product was not found. You can define a custom exception class like `NotFoundException`  for this purpose.
Please note that in a real-world scenario, you would replace this fake implementation with actual data access code, such as database queries, to retrieve the product from a database or another external data source.

## 6. Configure MediatR In Program.cs
```
builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(Program).Assembly));
```
## 7. Use MediatR in Controllers
In your controllers, use MediatR to send commands and queries:

```
[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductCommand command)
    {
        var productId = await _mediator.Send(command);
        return Ok(productId);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var query = new GetProductQuery { Id = id };
        var product = await _mediator.Send(query);
        return Ok(product);
    }
}
```






# WebSocket
---

**WebSocket** is a protocol that allows for full-duplex communication channels over a single TCP connection. This means that a server and a client can send messages to each other at any time, unlike traditional HTTP where the client must request information.

#### Features of WebSocket:
- **Full-duplex communication**: Both server and client can send messages independently.
- **Persistent connection**: Once established, the connection remains open until explicitly closed.
- **Low latency**: Ideal for applications requiring real-time updates, such as live chat, gaming, or live sports updates.




![1_ZneDqeqhmGYCre0fUJFQzQ.webp](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/OlfrjILTeUdIh6GXpYEwg.webp?ixlib=js-3.7.0 "1_ZneDqeqhmGYCre0fUJFQzQ.webp")





# SignalR
---

**SignalR** is a library for ASP.NET that simplifies adding real-time web functionality to applications. It allows bi-directional communication between server and client, using WebSocket as the default transport protocol and falling back to other techniques when necessary.

#### Features of SignalR:
- **Real-time updates**: Ideal for chat apps, notifications, or any live updates.
- **Automatic reconnection**: Handles reconnections if the connection drops.
- **Scalability**: Can scale out to handle multiple connections using Azure SignalR Service or other backplanes.




### Summary
- **WebSocket**:
    - Enables real-time, two-way communication between server and client.
    - Requires explicit handling of messages and connections.
    - Example: Custom middleware for handling WebSocket connections.
- **SignalR**:
    - Simplifies real-time communication using WebSocket and other fallback techniques.
    - Automatically handles reconnections and scales well.
    - Example: Hub-based model for defining server-client communication.




















# Communication framework for microservices
---

### 1. **HTTP/REST**
- **Pros**: Simple to implement, widely understood, easy to test.
- **Cons**: Higher latency, not ideal for real-time communication.
- **Use Case**: Suitable for stateless communication where performance is not a critical concern.
### 2. **gRPC**
- **Pros**: High performance, low latency, supports bi-directional streaming, language-agnostic.
- **Cons**: More complex than REST, requires HTTP/2.
- **Use Case**: Ideal for high-performance, real-time communication between microservices.
### 3. **Message Brokers (RabbitMQ, Apache Kafka, etc.)**
- **Pros**: Decouples services, reliable message delivery, supports asynchronous communication, suitable for complex workflows.
- **Cons**: Adds complexity to the system, requires managing the message broker.
- **Use Case**: Best for event-driven architectures, handling high throughput, and ensuring reliable message delivery.
### 4. **Service Mesh (Istio, Linkerd, etc.)**
- **Pros**: Provides advanced traffic management, security, and observability features.
- **Cons**: Can be complex to set up and manage, introduces additional overhead.
- **Use Case**: Suitable for large, complex microservices architectures requiring advanced features.
### Recommended Framework: gRPC + RabbitMQ
#### gRPC for Synchronous Communication
**gRPC** is a high-performance RPC (Remote Procedure Call) framework that uses HTTP/2 and protocol buffers. It is ideal for low-latency, high-throughput communication between microservices.

- **Pros**:
    - **High Performance**: Lower latency and higher throughput compared to REST.
    - **Streaming**: Supports bi-directional streaming, which is useful for real-time communication.
    - **Strong Typing**: Uses protocol buffers for strongly-typed contracts.
    - **Multi-language Support**: Compatible with many programming languages, making it versatile.
- **Cons**:
    - **Complexity**: More complex than REST, requiring understanding of HTTP/2 and protocol buffers.
    - **Setup Overhead**: Requires setting up gRPC servers and clients, which might be more involved than simple HTTP/REST.
- **Use Case**: Real-time, synchronous communication between microservices where performance is critical.
#### RabbitMQ for Asynchronous Communication
**RabbitMQ** is a robust, open-source message broker that facilitates asynchronous communication through message queues.

- **Pros**:
    - **Decoupling**: Services communicate via queues, which decouples them and enhances scalability.
    - **Reliability**: Ensures message delivery even if services are temporarily unavailable.
    - **Flexibility**: Supports various messaging patterns like publish/subscribe, work queues, and more.
    - **Persistent Storage**: Can persist messages to disk, ensuring data durability.
- **Cons**:
    - **Overhead**: Adds complexity due to the need for managing the message broker.
    - **Latency**: Asynchronous by nature, which might not be suitable for all real-time scenarios.
- **Use Case**: Event-driven architectures, background processing, and scenarios where reliability and decoupling are crucial.
### Combining gRPC and RabbitMQ
Using **gRPC** and **RabbitMQ** together allows you to leverage the best of both synchronous and asynchronous communication:

1. **gRPC** for Real-time Requests:
    - Use gRPC for direct service-to-service communication where low latency is required.
    - Ideal for real-time processing, immediate responses, and streaming data.
2. **RabbitMQ** for Event-driven Communication:
    - Use RabbitMQ for asynchronous tasks, event notifications, and scenarios where decoupling services is beneficial.
    - Suitable for background processing, job queues, and reliable message delivery.
### Deployment Considerations
#### On-Premises Deployment
- **Infrastructure**: Ensure sufficient resources (CPU, memory, disk I/O) to handle gRPC and RabbitMQ workloads.
- **Network**: Optimize network configuration for low-latency communication.
- **Scalability**: Plan for horizontal scaling of both gRPC services and RabbitMQ nodes if needed.
- **Monitoring**: Implement robust monitoring and logging for both gRPC and RabbitMQ to manage performance and troubleshoot issues.
#### Cloud Deployment
- **Managed Services**: Consider using managed services like AWS App Mesh (for gRPC) and Amazon MQ (for RabbitMQ) to reduce operational overhead.
- **Autoscaling**: Leverage cloud autoscaling capabilities to handle varying loads dynamically.
- **Networking**: Use cloud networking features (like AWS VPC, Azure VNets) to optimize communication between services.
- **Security**: Implement appropriate security measures (TLS, IAM roles, security groups) to protect communication channels.
### Example Architecture
1. **Microservice A** (Order Service):
    - **gRPC** for synchronous communication with **Microservice B** (Inventory Service).
    - **RabbitMQ** for sending order events to **Microservice C** (Notification Service).
2. **Microservice B** (Inventory Service):
    - **gRPC** for real-time stock checks and updates.
    - **RabbitMQ** for broadcasting inventory updates to other interested services.
3. **Microservice C** (Notification Service):
    - Listens to order events from RabbitMQ and sends notifications accordingly.
### Summary
- **gRPC**: Best for real-time, synchronous communication with low latency and high throughput.
- **RabbitMQ**: Best for asynchronous, reliable message delivery and decoupled communication.
- **Hybrid Approach**: Combines the strengths of both, providing a robust framework for microservice communication suitable for both on-premises and cloud deployments.
Implementing this hybrid framework ensures that your microservices can communicate efficiently and reliably, adapting to both synchronous and asynchronous needs, regardless of the deployment environment.



---

Hosting all microservices and RabbitMQ on a single system can lead to significant load, especially under high message rates and persistence requirements. Conduct thorough load testing, monitor resource usage, and consider horizontal scaling to ensure system stability and performance.











# Installing RabbitMQ
---

### Dependencies: [﻿erlang.org/download/otp_versions_tree.html](https://erlang.org/download/otp_versions_tree.html) 
RabbitMQ requires a 64-bit supported version of Erlang for Windows to be installed.



**Download : **[﻿github.com/rabbitmq/rabbitmq-server/releases/download/v3.13.5/rabbitmq-server-3.13.5.exe](https://github.com/rabbitmq/rabbitmq-server/releases/download/v3.13.5/rabbitmq-server-3.13.5.exe) 



rabbitmq directory :

> C:\Program Files\RabbitMQ Server\rabbitmq_server-3.7.12\sbin



## Enabling the Management Plugin
The management plugin is included in the RabbitMQ distribution. Like any other [﻿plugin](https://www.rabbitmq.com/docs/plugins), it must be enabled before it can be used. That's done using [﻿rabbitmq-plugins](https://www.rabbitmq.com/docs/man/rabbitmq-plugins.8):

```
rabbitmq-plugins enable rabbitmq_management
```
Node restart is not required after plugin activation.



## Enabling the Stream Plugin
The Stream plugin is included in the RabbitMQ distribution. Before clients can successfully connect, it must be enabled using [﻿rabbitmq-plugins](https://www.rabbitmq.com/docs/cli):

```
rabbitmq-plugins enable rabbitmq_stream
```














# RabbitMQ Queues tutorials
---

### 1. "Hello World!"
![image.png](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/ZzVQgAcR9UqF-eYpAaJFy.png?ixlib=js-3.7.0 "image.png")

### 2. Work Queues
![image.png](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/LO0weJnPuN2rnyyOnE7_L.png?ixlib=js-3.7.0 "image.png")

### 3. Publish/Subscribe
![image.png](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/F65WWaBfh72dDlWnFtb7V.png?ixlib=js-3.7.0 "image.png")

### 4. Routing
![image.png](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/KJ-2823mfm4YVvBfxZjeI.png?ixlib=js-3.7.0 "image.png")

### 5. Topics
![image.png](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/EOf04yZd1fHR45hBxwRKb.png?ixlib=js-3.7.0 "image.png")

### 6. RPC
![image.png](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/J2UJdWvhpfISLVCbwZZg5.png?ixlib=js-3.7.0 "image.png")

### 7. Reliable Publishing with Publisher Confirms






![Queues-vs-Stream-infographic-1.jpg](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/nEpY_n31XFSj1Hoe4Px1c.jpg?ixlib=js-3.7.0 "Queues-vs-Stream-infographic-1.jpg")





-  comparison between different aspects of RabbitMQ queues and streams.
|  | Queues | Streams |
| ----- | ----- | ----- |
| **Message Handling** | Messages in a queue are processed in a FIFO (First-In-First-Out) order. Once a message is consumed, it is removed from the queue. | Streams provide append-only logs where messages are appended and consumed multiple times by different consumers. |
| **Message Storage** | Messages are transient unless explicitly set to be persistent. They are removed from the queue after consumption. | Messages are retained based on configurable retention policies (time-based or size-based), allowing for longer-term storage. |
| **Consumption** | Messages are consumed by a single consumer or in a competing fashion among multiple consumers. | Allows multiple consumers to read the same message independently, offering more flexible consumption patterns. |




# RPC
---

RPC **(Remote Procedure Call)** enables two distinct computer programs to communicate with each other over a network. In this communication model, one program requests a service or procedure from another program, which processes the request and sends the results back to the first program. RPC follows the client-server model, where the requesting program acts as the client and the service-providing program operates as the server.

![image.png](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/OzKoHyuJrUcBGM7ohe9fX.png?ixlib=js-3.7.0 "image.png")



#  **gRPC**
---

gRPC is a cross-platform open source remote procedure call (RPC) framework that enables client and server applications to communicate with each other remotely and transparently. It was developed by google which uses HTTP/2. gRPC utilizes Remote Procedure Call (RPC) technology to facilitate communication between disparate services using a protocol buffer.The protocol buffer serves as a conduit for structured data exchange between systems.



![image.png](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/LGowHaUU-LIw4-USKLQ0t.png?ixlib=js-3.7.0 "image.png")



### **Protocol Buffer:**
gRPC uses protocol buffers as both its Interface Definition Language (IDL) and as its underlying message interchange format. Protocol buffers, aka protobufs, are a language-agnostic protocol for serializing structured data and they can be used between different programming languages. They are strongly typed, which ensures that the data is consistent and well-formed across different applications and services.As this is in the form of a contract, both the client and server need to have the same proto file. 

![image.png](https://eraser.imgix.net/workspaces/taeLmd0SparAzn9iGbGE/MIF6Cd5YZdOLD4uYvD0awU85o1t2/V6MlBeasxRYqOpOoergE0.png?ixlib=js-3.7.0 "image.png")



There are four types of gRPC communication. They are:

- **Unary RPC**
    - Unary RPCs where the client sends a single request to the server and gets a single response back, just like a normal function call.
- **Server Streaming RPC**
    - The client sends a single request to the server and gets back several messages that it reads sequentially.
- **Client Streaming RPC**
    - The client sends multiple messages to the server, and the server can process them as they arrive before sending a single response back to the client.
- **Bidirectional Streaming RPC**
    - Bidirectional streaming is a powerful feature of GRPC that enables the client and server to send and receive multiple streams of data simultaneously.




# WebRTC:

## 1. Introduction to WebRTC
WebRTC (Web Real-Time Communication) is an open-source project that enables peer-to-peer (P2P) audio, video, and data sharing directly between web browsers without the need for plugins. It powers a wide range of applications from video conferencing to file sharing, all within a standard web browser.

## 2. Key Components of WebRTC
### Media Streams
WebRTC allows capturing, transmitting, and receiving audio and video streams in real-time. The `getUserMedia()` API is commonly used to access media devices like cameras and microphones.

### RTCPeerConnection
This is the core of WebRTC, responsible for handling the connections between peers, including negotiating, establishing, and maintaining the connection.

### RTCDataChannel
WebRTC also supports sending arbitrary data between peers using RTCDataChannels, which are reliable and low-latency channels.

### ICE (Interactive Connectivity Establishment)
ICE is a framework used by WebRTC to find the best path to connect peers across various network configurations, using STUN and TURN servers if necessary.

## 3. WebRTC Signaling Process
### Signaling Channels
WebRTC does not define a signaling protocol, so developers can use WebSocket, HTTP, or any other method to exchange session control messages between peers.

### Offer/Answer Model
WebRTC uses the Offer/Answer model, where one peer sends an offer with connection parameters, and the other peer responds with an answer.

### SDP (Session Description Protocol)
SDP is a format for describing the multimedia communication sessions. It includes codec, format, network information, and other session details.

## 4. WebRTC Connection Flow
### Step 1: Establishing the Connection
- **Signaling:** Exchange of SDP and ICE candidates between peers.
- **ICE Candidate Exchange:** Finding the best route for communication.
- **DTLS Handshake:** Securing the connection using DTLS.

### Step 2: Media & Data Channel Setup
- **Setting up Media Streams:** Using `getUserMedia()` and `addTrack()`.
- **Creating Data Channels:** Establishing RTCDataChannels for data transfer.

### Step 3: Connection Maintenance
- **ICE Reconnection:** Handling network changes and maintaining the connection.
- **Periodic Connectivity Checks:** Using STUN/TURN servers to ensure connectivity.

### Step 4: Closing the Connection
- **Closing Peer Connection:** Releasing resources using `close()` method on RTCPeerConnection.
- **Cleanup:** Removing event listeners and clearing references.

## 5. Security in WebRTC
### Encryption
WebRTC encrypts all media streams to protect them from eavesdropping.

### DTLS (Datagram Transport Layer Security)
DTLS is used to secure the data channel and the connection between peers.

### SRTP (Secure Real-Time Protocol)
SRTP provides encryption, message authentication, and integrity, as well as replay protection for RTP data.

## 6. WebRTC API Overview
### getUserMedia()
The `getUserMedia()` API is used to access the user's camera and microphone, providing the media streams needed for communication.

### RTCPeerConnection API
This API includes methods for handling connections:
- `createOffer()`, `createAnswer()`
- `setLocalDescription()`, `setRemoteDescription()`
- `addIceCandidate()`, `onicecandidate`

### RTCDataChannel API
This API handles data channels, with methods for creating and managing data channels between peers.

## 7. Common WebRTC Use Cases
### Video Conferencing
Enabling real-time video chat applications.

### File Sharing
Using RTCDataChannels for peer-to-peer file transfer.

### Online Gaming
Low-latency data transfer for real-time multiplayer games.

## 8. Challenges and Best Practices
### Network Variability
Handling various network conditions and maintaining a stable connection.

### Scalability
Strategies for scaling WebRTC to support many users.

### Browser Support
Cross-browser compatibility and handling differences in implementation.

## 9. Advanced Topics
### Using WebRTC with mediaMTX
- Integration examples and specific use cases.

### Optimizing WebRTC Performance
- Tips for improving latency, bandwidth usage, and media quality.

## 10. Conclusion
WebRTC is a powerful tool for building real-time communication applications. It provides all the necessary components for creating secure, high-quality audio, video, and data channels directly within the browser.

## 11. References and Further Reading
- [WebRTC Official Documentation](https://webrtc.org/)
- [MDN WebRTC API Documentation](https://developer.mozilla.org/en-US/docs/Web/API/WebRTC_API)
- [WebRTC for the Curious](https://webrtcforthecurious.com/)




# STUN (Session Traversal Utilities for NAT)

## Purpose: 
STUN is used to discover the public IP address and the type of NAT (Network Address Translation) that a device is behind. It helps a device determine its external IP address and port so that it can communicate with other devices on the internet.

## How It Works: 
When a device behind a NAT wants to communicate with another device, it sends a request to a STUN server. The STUN server then responds with the external IP address and port of the device as seen by the server. This information is crucial for setting up direct peer-to-peer connections.

## Use Case: 
STUN is mainly used for scenarios where the NAT is not too restrictive and allows direct communication between peers after the public IP is discovered.

# TURN (Traversal Using Relays around NAT)

## Purpose: 
TURN is used when direct peer-to-peer communication is not possible due to strict NATs or firewalls. In such cases, TURN servers relay data between peers.

## How It Works: 
Unlike STUN, which only provides the public IP address, TURN actually relays the media traffic between the two peers. When a direct connection fails, the media traffic (audio, video, or data) is sent through a TURN server, which then forwards it to the other peer.

## Use Case: 
TURN is used when STUN alone is not sufficient to establish a connection, often in cases where both peers are behind symmetric NATs or when firewalls block direct communication.

# ICE (Interactive Connectivity Establishment)

## Purpose: 
ICE is a framework used to find the best possible path to connect peers. It combines the use of STUN and TURN to achieve this.

## How It Works: 
ICE gathers a list of potential connection candidates, which include local IP addresses, STUN-discovered public IP addresses, and TURN server addresses. It then tests all possible paths to find the most efficient route for communication.

## Use Case: 
ICE is the overall mechanism in WebRTC that ensures a robust and optimal connection by trying different methods (direct or relayed via TURN) to establish communication between peers.


## Summary

- `STUN` helps determine your public IP and NAT type, allowing for direct connections.
- `TURN` relays traffic when direct connections are not possible due to strict network conditions.
- `ICE` orchestrates the process, using STUN and TURN to find the best way to connect peers.

