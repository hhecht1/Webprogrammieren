using Scalar.AspNetCore;
using Microsoft.AspNetCore.Rewrite;
using ToDoList.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
var app = builder.Build();
app.UseHttpsRedirection();
app.UseRewriter(new RewriteOptions().AddRedirect("task/(.*)", "todos/$1"));

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

var toDos = new List<ToDo>
{
    new ToDo { Id = 1, ToDoText = "Buy groceries", IsDone = false, DateTime = DateTime.Now },
    new ToDo { Id = 2, ToDoText = "Clean the house", IsDone = true, DateTime = DateTime.Now },
    new ToDo { Id = 3, ToDoText = "Finish project", IsDone = false, DateTime = DateTime.Now }
};


app.MapGet("/todos", () => toDos);

app.MapGet("/todos/{id}", (int id) =>
{
    var toDo = toDos.FirstOrDefault(t => t.Id == id);
    return toDo is not null
    ? Results.Ok(toDo)
     : Results.NotFound();
});

app.MapPost("/todos", (ToDo newToDo) =>
{
    newToDo.Id = toDos.Max(t => t.Id) + 1;
    toDos.Add(newToDo);
    return Results.Created($"/todos/{newToDo.Id}", newToDo);
});





app.Run();
