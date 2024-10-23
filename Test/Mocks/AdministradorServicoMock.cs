using MinimalsApi.Dominio.Entidades;
using MinimalsApi.Dominio.Interfaces;
using MinimalsApi.DTOs;

namespace Test.Mocks;

public class AdministradorServicoMock : IAdministradorServico
{
    private static List<Adm> administradores = new List<Adm>();

    public Adm? BuscarPorId(int id)
    {
        return administradores.Find(a => a.Id == id);
    }

    public Adm Incluir(Adm adm)
    {
        adm.Id = administradores.Count() + 1;
        administradores.Add(adm);

        return adm;
    }

    public Adm? Login(LoginDTO loginDTO)
    {
        return administradores.Find(a = > a.Email == loginDTO.Email && a.Senha == loginDTO.Senha);
    }

    public List<Adm> Todos(int? pagina)
    {
        return administradores;
    }
}
