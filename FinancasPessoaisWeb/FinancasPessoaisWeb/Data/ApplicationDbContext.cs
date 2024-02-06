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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configurando a tabela account
            modelBuilder.ApplyConfiguration(new TabelaAccountConfiguration());
        }       
    }
}
