using Community.Repository;
using Community.Repository.Interfaces;
using Community.Repository.Repos;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add controllers
builder.Services.AddControllers();

// 🔹 Add CORS policy (allow any origin, header, method)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   // Allow requests from any origin (e.g., React frontend)
              .AllowAnyHeader()   // Allow any HTTP headers
              .AllowAnyMethod();  // Allow any HTTP methods (GET, POST, etc...)
    });
});

// 🔹 Add Swagger for API documentation
builder.Services.AddSwaggerGen();

// 🔹 Add Dependency Injection (DI)
builder.Services.AddSingleton<ICommunityContext, CommunityContext>();
builder.Services.AddScoped<IUser, UserRepo>();
builder.Services.AddScoped<ICategory, CategoryRepo>();
builder.Services.AddScoped<IBlogPost, BlogPostRepo>();
builder.Services.AddScoped<IComment, CommentRepo>();

// 🔹 Register Database Connection (Dapper)
builder.Services.AddTransient<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("Community")));

var app = builder.Build();

// 🔹 Enable Swagger UI (Available at root URL `/`)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "";
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
});

// 🔹 Use CORS
app.UseCors("AllowAll");

// 🔹 Enable Routing & Controllers
app.UseRouting();
app.MapControllers(); // This replaces `app.UseEndpoints(...)`

// 🔹 Run the Application
app.Run();
