﻿// <auto-generated />
using System;
using GestaoJogos.Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GestaoJogos.Infrastructure.EntityFramework.Migrations
{
    [DbContext(typeof(GestaoJogosContext))]
    [Migration("20201206221918_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GestaoJogos.Domain.Principal.Entities.Amigo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DataCadastro")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("Nome")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Amigo");
                });

            modelBuilder.Entity("GestaoJogos.Domain.Principal.Entities.Jogo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AmigoId")
                        .HasColumnName("AmigoId")
                        .HasColumnType("UniqueIdentifier");

                    b.Property<DateTime>("DataCadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DataCadastro")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("Nome")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("AmigoId");

                    b.ToTable("Jogo");
                });

            modelBuilder.Entity("GestaoJogos.Domain.Principal.Entities.Jogo", b =>
                {
                    b.HasOne("GestaoJogos.Domain.Principal.Entities.Amigo", "Amigo")
                        .WithMany("Jogos")
                        .HasForeignKey("AmigoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
