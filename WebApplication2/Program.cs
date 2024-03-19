using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Model;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Produtos") ?? "Data Source=Produtos.db";

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSqlite<ProdutoDb>(connectionString);
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/produtos", async (ProdutoDb db) => await db.Produtos.ToListAsync());

app.MapGet("/produtos/{id}", async (ProdutoDb db, int id) =>
{
    return await db.Produtos.FindAsync(id);
});

app.MapPost("/produtos", async (ProdutoDb db, Produto produto) =>
{
    await db.Produtos.AddAsync(produto);
    await db.SaveChangesAsync();
    return Results.Created($"/produtos/{produto.Id}", produto);
});

app.MapPut("/produtos/{id}", async (ProdutoDb db, int id, Produto updatedProduto) =>
{
    var produto = await db.Produtos.FindAsync(id);
    if (produto is null)
    {
        return Results.NotFound();
    }
    produto.Nome = updatedProduto.Nome;
    produto.Preco = updatedProduto.Preco;
    produto.Quantidade = updatedProduto.Quantidade;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/produtos/{id}", async (ProdutoDb db, int id) =>
{
    var produto = await db.Produtos.FindAsync(id);
    if (produto is null)
    {
        return Results.NotFound();
    }
    db.Produtos.Remove(produto);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();
