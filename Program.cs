using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Tarefa> tarefas = [];

app.MapGet("tarefas", () => tarefas);

app.MapPost("tarefas/add", ([FromBody] Tarefa tarefa) =>
{
    tarefas.Add(tarefa);
    return Results.Ok("Tarefa criada!");
});

app.MapDelete("tarefas/delete/{id}", (int id) =>
{
    foreach (var item in tarefas)
    {
        if (item.ID == id)
        {
            tarefas.Remove(item);
            return Results.Ok("Tarefa deletada!");
        }
    }
    return Results.NotFound();
});

app.MapPut("tarefas/done/{id}", (int id) =>
{
    for (int i = 0; i < tarefas.Count; i++)
    {
        if (tarefas[i].ID == id)
        {
            tarefas[i] = tarefas[i] with { Done = true };
            return Results.Ok("Tarefa concluída!");
        }
    }
    return Results.NotFound();
});

app.Run();

public record Tarefa(int ID, string Name, string Description, DateTime CreatedAt, bool Done);
