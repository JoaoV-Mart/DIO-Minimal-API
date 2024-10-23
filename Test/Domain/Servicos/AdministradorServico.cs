using MinimalsApi.Dominio.Entidades;
using MinimalsApi.Infraestrutura.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalsApi.Dominio.Servicos;

namespace Test.Domain.Entidades;

[TestClass]
public class AdministradorServicoTest
{
    private DbContexto CriarContextoDeTeste()
    {
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

        var builder = new ConfigurationBuilder()
            .SetBasePath(path ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        return new DbContexto(configuration);
    }

    [TestMethod]
    public void TestandoSalvarAdministrador()
    {
        //Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Admnistradores");

        var adm = new Adm();
        adm.Id = 1;
        adm.Email = "teste@teste.com";
        adm.Senha = "teste";
        adm.Perfil = "Adm";
        var context = CriarContextoDeTeste();
        var admServico = new AdministradorServico(context);

        //Act
        admServico.Incluir(adm);

        //Assert
        Assert.AreEqual(1, admServico.Todos(1).Count());
        Assert.AreEqual("teste@teste.com", adm.Email);
        Assert.AreEqual("teste", adm.Senha);
        Assert.AreEqual("Adm", adm.Perfil);

    }

    public void TestandoBuscaPorId()
    {
        //Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Admnistradores");

        var adm = new Adm();
        adm.Id = 1;
        adm.Email = "teste@teste.com";
        adm.Senha = "teste";
        adm.Perfil = "Adm";
        var context = CriarContextoDeTeste();
        var admServico = new AdministradorServico(context);

        //Act
        admServico.Incluir(adm);
        var admDoBanco = admServico.BuscarPorId(adm.Id);

        //Assert
        Assert.AreEqual(1, admDoBanco.Id);

    }

    public void TestandoBuscaPorTodos()
    {
        //Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Admnistradores");

        var adm = new Adm();
        adm.Id = 1;
        adm.Email = "teste@teste.com";
        adm.Senha = "teste";
        adm.Perfil = "Adm";
        var context = CriarContextoDeTeste();
        var admServico = new AdministradorServico(context);

        //Act
        admServico.Incluir(adm);
        var paginaDoBanco = admServico.Todos(pagina);

        //Assert
        Assert.True(paginaDoBanco.Count() > 1);

    }
}