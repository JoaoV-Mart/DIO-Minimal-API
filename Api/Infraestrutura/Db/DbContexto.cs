using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using MinimalsApi.Entidades;

namespace MinimalsApi.Infraestrutura.Db;

public class DbContexto :  DbContext
{
    public readonly IConfiguration _configuracaoAppSettings;

    public DbContexto(IConfiguration configuracaoAppSettings)
    {
        _configuracaoAppSettings = configuracaoAppSettings;
    }

    public DbSet<Adm> Admnistradores { get; set; } = default!;

    public DbSet<Veiculo> Veiculos { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adm>().HasData(
            new Adm {
                Id = 1,
                Email = "administrador@teste.com",
                Senha = "1234567",
                Perfil = "Adm"
            }
        );
            
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
            {
            var stringConexao = _configuracaoAppSettings.GetConnectionString("Conexao padrao").ToString();

            if(!string.IsNullOrEmpty(stringConexao))
            {
                optionsBuilder.UseSqlServer(stringConexao);
            }
        }

    }
}