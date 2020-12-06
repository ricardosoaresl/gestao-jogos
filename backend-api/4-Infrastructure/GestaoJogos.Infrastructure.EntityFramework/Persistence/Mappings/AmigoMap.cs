using GestaoJogos.Domain.Principal.Entities;
using GestaoJogos.Infrastructure.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoJogos.Infrastructure.EntityFramework.Persistence.Mappings
{
    public class AmigoMap : EntityTypeConfiguration<Amigo>
    {
        public override void Map(EntityTypeBuilder<Amigo> builder)
        {
            builder.ToTable("Amigo");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(p => p.Nome)
                .HasColumnName("Nome")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(p => p.DataCadastro)
                .HasColumnName("DataCadastro")
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()")
                .IsRequired();
        }
    }
}
