namespace crud_desafio.Helpers;

using Microsoft.EntityFrameworkCore;
using crud_desafio.Entities;
public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to mysql with connection string from app settings
        var connectionString = Configuration.GetConnectionString("ApiDatabase");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).
        UseSnakeCaseNamingConvention();
    }

    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }
}
