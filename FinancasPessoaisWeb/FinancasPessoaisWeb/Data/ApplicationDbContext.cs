namespace FinancasPessoaisWeb.Data
{
    using FinancasPessoaisWeb.Data.Configurations;
    using FinancasPessoaisWeb.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<AccountModel> Account { get; set; }
        public DbSet<DespesaModel> Despesa { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfiguration(new TabelaAccountConfiguration());
            modelBuilder.ApplyConfiguration(new TabelaDespesaConfiguration());
        }       
    }
}
