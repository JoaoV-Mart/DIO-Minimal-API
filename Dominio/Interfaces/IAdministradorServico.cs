using MinimalsApi.DTOs;
using MinimalsApi.Entidades;

namespace MinimalsApi.Dominio.Interfaces;

public interface IAdministradorServico
{
    Adm? Login(LoginDTO loginDTO);

    Adm Incluir(AdministradorDTO administradorDTO);

    Adm? BuscarPorId(int id);

    List<Adm> Todos(int? pagina);
}
