using Microsoft.EntityFrameworkCore;
using MinimalsApi.DTOs;
using MinimalsApi.Infraestrutura.Db;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContexto>(options => {
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Conexao padrao")
    );
    }
);

var app = builder.Build();


app.MapPost("/login", (LoginDTO loginDTO) =>
{
    if(loginDTO.Email == "adm@test.com" && loginDTO.Senha == "1234567")
    {
        return Results.Ok("Login realizado com sucesso!");
    } else 
    {
        return Results.Unauthorized();
    }
});

app.Run();


