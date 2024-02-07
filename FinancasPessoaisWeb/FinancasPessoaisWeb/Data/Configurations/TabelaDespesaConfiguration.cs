using FinancasPessoaisWeb.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoaisWeb.Data.Configurations
{
    public class TabelaDespesaConfiguration : IEntityTypeConfiguration<DespesaModel>
    {
        public void Configure(EntityTypeBuilder<DespesaModel> builder)
        {
            builder.ToTable("despesa");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.IdAccount).HasColumnName("id_account").IsRequired(); ;
            builder.Property(e => e.Descricao).HasColumnName("desc").IsRequired();
            builder.Property(e => e.Valor).HasColumnName("valor").IsRequired();
            builder.Property(e => e.Vencimento).HasColumnName("vencimento").IsRequired();
            builder.Property(e => e.Situacao).HasColumnName("situacao").IsRequired();
        }
    }
}

