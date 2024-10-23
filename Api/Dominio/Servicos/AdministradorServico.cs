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

    public Adm Incluir(Adm adm)
    {
        _contexto.Admnistradores.Add(adm);
        _contexto.SaveChanges();

        return adm;
    }

    public Adm? BuscarPorId(int id)
    {
        return _contexto.Admnistradores.Where(v => v.Id == id).FirstOrDefault();
    }

    public List<Adm> Todos(int? pagina)
    {
        var query = _contexto.Admnistradores.AsQueryable();

        var itensPorPagina = 10;

        if(pagina != null)
        {
            query = query.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);
        }

        return query.ToList();

    }
}