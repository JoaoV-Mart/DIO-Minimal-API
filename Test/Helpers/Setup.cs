using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using MinimalsApi.Dominio.Interfaces;
using Test.Mocks;
using Microsoft.Extensions.DependencyInjection;


namespace Test.Helpers;

public class SecuritySchemeType
{
    public const string PORT = "5001";
    public static TestContext textContext = default!;
    public static WebApplicationFactory<Startup> http = default!;
    public static HttpClient client = default!;

    public static void ClassInit(TestContext textContext)
    {
        Setup.testContext = testContext;
        Setup.http = new WebApplicationFactory<Startup>();

        Setup.http = Setup.http.WithWebHostBuilder(builder =>
        {
            builder.UseSetting("hhtps_port", Setup.PORT).UseEnvironmet("Testing");

            builder.ConfigureServices(services =>
            {
                services.AddScoped<IAdministradorServico, AdministradorServicoMock>()
                {
                    new Adm{
                        Id = 1,
                        Email = "adm@teste.com",
                        Senha = "1234567",
                        Perfil = "Adm"
                    },
                    {
                        Id = 1,
                        Email = "editor@teste.com",
                        Senha = "1234567",
                        Perfil = "Editor"
                    }
                };
            });
        });

        Setup.client = Setup.http.CreateClient();
    }

    public static void ClassCLeanup()
    {
        Setup.http.Dispose();
    }
}