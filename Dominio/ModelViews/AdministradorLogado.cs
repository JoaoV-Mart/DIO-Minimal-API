using MinimalsApi.Dominio.Enuns;
namespace MinimalsApi.Dominio.ModelViews;

public record AdministradorLogado
{
    public string Token { get; set; } = default!;

    public string Email{ get; set; } = default!;

    public string Perfil { get; set; } = default!;
    
}