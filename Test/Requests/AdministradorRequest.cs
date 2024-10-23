using Test.Helpers;
using MinimalsApi.DTOs;
using System.Text.Json;
using System.Text;
using MinimalsApi.Dominio.ModelViews;
using System.Net;

namespace Test.Requests;

[TestClass]
public class AdministradorRequestTest
{

    [ClassInitialize]
    public static void ClassInit(TestContext testContext)
    {
        Setup.ClassInit(testContext);
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        Setup.ClassCleanup();
    }

    [TestMethod]
    public async Task TestarGetSetPropriedades()
    {
        //Arrange
        var loginDTO = new LoginDTO{
            Email = "adm@teste.com",
            Senha = "1234567"
        };

        var content = new StringContent(JsonSerializer.Serialize(loginDTO), Enconding.UTF8, "Application/json");
        

        //Act
        var response = await Setup.client.PostAsync("/administradores/login", content);

        //Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        var admLogado = JsonSerializer.Deserialize<AdministradorLogado>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(admLogado?.Perfil ?? "");
        Assert.IsNotNull(admLogado?.Email ?? "");
        Assert.IsNotNull(admLogado?.Token ?? "");

    }
