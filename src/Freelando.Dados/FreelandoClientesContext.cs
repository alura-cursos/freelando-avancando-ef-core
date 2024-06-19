using Microsoft.EntityFrameworkCore;

namespace Freelando.Dados;
public class FreelandoClientesContext: DbContext
{
    public FreelandoClientesContext(DbContextOptions<FreelandoClientesContext> options) : base(options)
    {
    }

    private string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FreelandoClientes;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FreelandoContext).Assembly);
    }

    public DbSet<ClienteNew> Clientes { get; set; }
    

}

public class ClienteNew
{
    public ClienteNew()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateTime DataInclusao { get; set; }
}
