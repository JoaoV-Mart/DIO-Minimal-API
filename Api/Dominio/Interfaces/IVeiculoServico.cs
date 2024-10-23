using MinimalsApi.DTOs;
using MinimalsApi.Entidades;

namespace MinimalsApi.Dominio.Interfaces;

public interface IVeiculoServico
{
    List<Veiculo> Todos(int? pagina=1, string? nome=null, string? marca=null);

    Veiculo? BuscarPorId(int id);

    void Inserir(Veiculo veiculo);

    void Atualizar(Veiculo veiculo);

    void Deletar(Veiculo veiculo);
}