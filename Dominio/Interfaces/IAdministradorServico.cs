using MinimalsApi.DTOs;
using MinimalsApi.Entidades;

namespace MinimalsApi.Dominio.Interfaces;

public interface IAdministradorServico
{
    Adm? Login(LoginDTO loginDTO);
}