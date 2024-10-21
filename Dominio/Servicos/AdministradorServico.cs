using MinimalsApi.Dominio.Interfaces;
using MinimalsApi.DTOs;
using MinimalsApi.Entidades;
using MinimalsApi.Infraestrutura.Db;

namespace MinimalsApi.Dominio.Servicos;

public class AdministradorServico : IAdministradorServico
{
    private readonly DbContexto _contexto;

    public AdministradorServico(DbContexto contexto)
    {
        _contexto = contexto;
    }

    public Adm? Login(LoginDTO loginDTO)
    {
        var adm = _contexto.Admnistradores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha).FirstOrDefault();
        
        return adm;
    }
}