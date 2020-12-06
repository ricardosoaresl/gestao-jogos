using GestaoJogos.Domain.Principal.Entities;
using GestaoJogos.Infrastructure.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoJogos.Infrastructure.EntityFramework.Persistence.Mappings
{
    public class JogoMap : EntityTypeConfiguration<Jogo>
    {
        public override void Map(EntityTypeBuilder<Jogo> builder)
        {
            builder.ToTable("Jogo");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(p => p.Nome)
                .HasColumnName("Nome")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(i => i.AmigoId)
                .HasColumnName("AmigoId")
                .HasColumnType("UniqueIdentifier");

            builder.HasOne(a => a.Amigo)
                   .WithMany(e => e.Jogos)
                   .HasForeignKey(p => p.AmigoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.DataCadastro)
                .HasColumnName("DataCadastro")
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()")
                .IsRequired();
        }
    }
}
