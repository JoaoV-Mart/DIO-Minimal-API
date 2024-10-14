using MinimalsApi.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

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


