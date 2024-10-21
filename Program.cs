using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalsApi.Dominio.Interfaces;
using MinimalsApi.Dominio.ModelViews;
using MinimalsApi.Dominio.Servicos;
using MinimalsApi.DTOs;
using MinimalsApi.Entidades;
using MinimalsApi.Infraestrutura.Db;

#region Builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();

builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbContexto>(options => {
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Conexao padrao")
    );
    }
);

var app = builder.Build();
#endregion

#region Home
app.MapGet("/", () => Results.Json(new Home())).WithTags("Home");
#endregion

#region Administradores
app.MapPost("/Administrador/login", ([FromBody] LoginDTO loginDTO, IAdministradorServico administradorServico) =>
{
    if(administradorServico.Login(loginDTO) != null)
    {
        return Results.Ok("Login realizado com sucesso!");
    } else 
    {
        return Results.Unauthorized();
    }
}).WithTags("Administradores");

app.MapPost("/Administradores", ([FromBody] AdministradorDTO administrador DTO, IAdministradorServico administradorServico) =>
{
    var validacao = new ErrosDeValidacao{
        Mensagens = new List<string>()
    };    
    
    if(string.IsNullOrEmpty(administradorDTO.Email))
        validacao.Mensagens.Add("Email não pode ser vazio");
  
    if(string.IsNullOrEmpty(administradorDTO.Senha))
        validacao.Mensagens.Add("Senha não pode ser vazia");
    
    if(administradorDTO.Perfil == null)
        validacao.Mensagens.Add("Perfil não pode ser vazio");

    if(validacao.Mensagens.Count > 0)
        return Results.BadRequest(validacao);    

    var administrador = new Adm{
        Email = administradorDTO.Email,
        Senha = administradorDTO.Senha,
        Perfil = administradorDTO.Perfil.ToString() ?? Perfil.Editor.ToString()
    };

    administradorServico.Incluir(administrador);

    return Results.Created($"/administrador/{administrador.Id}", new AdministradorModelView
        {
            Id = administrador.Id,
            Email = administrador.Email,
            Perfil = administrador.Perfil
        });
                                     
}).WithTags("Administradores");

app.MapGet("/Administradores", ([FromQuery] int? pagina, IAdministradorServico administradorServico) =>
{
    var adm = new List<AdministradorModelView>();
    var administradores = administradorServico.Todos(pagina);

    foreach(var adm in administradores)
    {
        adms.Add(new AdministradorModelView)
        {
            Id = adm.Id,
            Email = adm.Email,
            Perfil = adm.Perfil
        }
    }

    return Results.Ok(adms);

}).WithTags("Administradores");

app.MapGet("/Administradores/{id}", ([FromRoute] int id, IAdministradorServico administradorServico) =>
{
    var administrador = administradorServico.BuscarPorId(id);

    if(administrador == null) return Results.NotFound();
    return Results.Ok
    (
        new AdministradorModelView
        {
            Id = administrador.Id,
            Email = administrador.Email,
            Perfil = administrador.Perfil
        }
    );

}).WithTags("Administradores");
#endregion

#region Veiculos
ErrosDeValidacao validaDTO(VeiculoDTO veiculoDTO)
{
    var validacao = new ErrosDeValidacao{
        Mensagens = new List<string>()
    };

    if(string.IsNullOrEmpty(veiculoDTO.Nome))
        validacao.Mensagens.Add("O nome não pode ser vazio.");
    
    if(string.IsNullOrEmpty(veiculoDTO.Marca))
        validacao.Mensagens.Add("A marca não pode ser vazia.");

    if(veiculoDTO.Ano < 1886)
        validacao.Mensagens.Add("Ano inferior ao do primeiro carro existente. Digite um ano válido.");

    return validacao;
}

app.MapPost("/veiculos", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
{
    
    var validacao = validaDTO(veiculoDTO);
    if(validacao.Mensagens.Count > 0)
        return Results.BadRequest(validacao);

    var veiculo = new Veiculo{
        Nome = veiculoDTO.Nome,
        Marca = veiculoDTO.Marca,
        Ano = veiculoDTO.Ano
    };

    veiculoServico.Inserir(veiculo);

    return Results.Created($"/veiculo/{veiculo.Id}", veiculo);
}).WithTags("Veiculos");

app.MapGet("/veiculos", ([FromQuery] int? pagina, IVeiculoServico veiculoServico) =>
{
    var veiculos = veiculoServico.Todos(pagina);

    return Results.Ok(veiculos);
}).WithTags("Veiculos");

app.MapGet("/veiculo/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
{
    var veiculo = veiculoServico.BuscarPorId(id);

    if(veiculo == null) return Results.NotFound();
    return Results.Ok(veiculo);

}).WithTags("Veiculos");

app.MapPut("/veiculo/{id}", ([FromRoute] int id, VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
{
    var veiculo = veiculoServico.BuscarPorId(id);
    
    if(veiculo == null) return Results.NotFound();

    var validacao = validaDTO(veiculoDTO);
    if(validacao.Mensagens.Count > 0)
        return Results.BadRequest(validacao);

    veiculo.Nome = veiculoDTO.Nome;
    veiculo.Marca = veiculoDTO.Marca;
    veiculo.Ano = veiculoDTO.Ano;

    veiculoServico.Atualizar(veiculo);

    return Results.Ok(veiculo);

}).WithTags("Veiculos");

app.MapDelete("/veiculo/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
{
    var veiculo = veiculoServico.BuscarPorId(id);

    if(veiculo == null) return Results.NotFound();
    veiculoServico.Deletar(veiculo);
    
    return Results.NoContent();

}).WithTags("Veiculos");
#endregion

#region App
app.UseSwagger();

app.UseSwaggerUI();

app.Run();
#endregion
