using FinancasPessoaisWeb.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoaisWeb.Data.Configurations
{
    public class TabelaAccountConfiguration : IEntityTypeConfiguration<AccountModel>
    {
        public void Configure(EntityTypeBuilder<AccountModel> builder)
        {
            builder.ToTable("account");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome).HasColumnName("Nome").IsRequired();
            builder.Property(e => e.CPF).HasColumnName("CPF").IsRequired().HasMaxLength(11);
            builder.Property(e => e.Telefone).HasColumnName("Telefone").IsRequired();
            builder.Property(e => e.Senha).HasColumnName("Senha").IsRequired();
            builder.Property(e => e.Saldo).HasColumnName("Saldo").IsRequired();
        }
    }
}

