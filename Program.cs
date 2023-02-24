//https://www.youtube.com/watch?v=_vzTewWNI4k&t=909s

using Microsoft.EntityFrameworkCore;
using WebApiMinimal.Contexto;
using WebApiMinimal.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<Contexto>
    (options => options.UseSqlServer(
        "Data Source=TULIO-DKT\\SQLEXPRESS;Initial Catalog=MINIMA_API_AULA;Integrated Security=False"));

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.MapPost("AdicionaProduto", async (Produto produto, Contexto contexto) =>
{
    contexto.Produto.Add(produto);
    await contexto.SaveChangesAsync();
});

app.MapPost("ExcluirProduto/{id}", async (int id, Contexto contexto) =>
{
    var produtoExcluir = await contexto.Produto.FirstOrDefaultAsync(p => p.Id == id);

    if (produtoExcluir != null)
    {
        contexto.Produto.Remove(produtoExcluir);
        await contexto.SaveChangesAsync();
    }
});

app.MapPost("ListarProdutos", async (int id, Contexto contexto) =>
{
    return await contexto.Produto.ToListAsync();
});

app.MapPost("ObterProduto/{id}", async (int id, Contexto contexto) =>
{
    return await contexto.Produto.FirstOrDefaultAsync(p => p.Id == id);
});

app.UseSwaggerUI();

app.Run();
