using Community.Repository;
using Community.Repository.Interfaces;
using Community.Repository.Repos;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();


//a Swagger generator that goes through all endpoints and 
//documents these in a JSON file.
builder.Services.AddSwaggerGen();

// Add dependency injection

builder.Services.AddSingleton<ICommunityContext, CommunityContext>();

// Register UserRepo
builder.Services.AddScoped<IUser, UserRepo>();

// Register CategoryRepo
builder.Services.AddScoped<ICategory, CategoryRepo>();

// Register BlogPostRepo
builder.Services.AddScoped<IBlogPost, BlogPostRepo>();

// Register CommentRepo
builder.Services.AddScoped<IComment, CommentRepo>();




builder.Services.AddTransient<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("Community")));


var app = builder.Build();


//Here we tell the application to use Swagger + SwaggerUI
app.UseSwagger();
app.UseSwaggerUI();

// use routing
app.UseRouting();

// Map endpoints
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

// Run the application

app.Run();
