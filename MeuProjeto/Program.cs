using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using MeuProjeto.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseMySql(
builder.Configuration.GetConnectionString("DefaultConnection"),
ServerVersion.AutoDetect(
builder.Configuration.GetConnectionString("DefaultConnection")
)

)
);
var app = builder.Build();


app.MapGet("/", async (AppDbContext context) =>
{
    try
    {
        // Teste de conexão com o banco
        await context.Database.CanConnectAsync();
        return "Conexão com o banco de dados estabelecida com sucesso!";
    }
    catch (Exception ex)
    {
        return $"Erro na conexão: {ex.Message}";
    }
});

// Endpoint para testar inserção de dados
app.MapGet("/test-insert", async (AppDbContext context) =>
{
    try
    {
        var usuario = new Usuario
        {
            Nome = "Teste",
            Email = "teste@email.com"
        };

        context.Usuarios.Add(usuario);
        await context.SaveChangesAsync();

        return "Usuário inserido com sucesso!";
    }
    catch (Exception ex)
    {
        return $"Erro ao inserir: {ex.Message}";
    }

});
app.Run();
