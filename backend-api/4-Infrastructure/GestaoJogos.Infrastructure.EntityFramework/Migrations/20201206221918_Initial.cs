using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoJogos.Infrastructure.EntityFramework.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amigo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(255)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amigo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(255)", nullable: false),
                    AmigoId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jogo_Amigo_AmigoId",
                        column: x => x.AmigoId,
                        principalTable: "Amigo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_AmigoId",
                table: "Jogo",
                column: "AmigoId");

            //-- SEEDS
            migrationBuilder.InsertData(
                table: "Amigo",
                columns: new[] { "Id", "Nome", },
                values: new object[] { "ca7b60ba-1cf7-437b-8ae6-c87f335cf895", "Diego Sampaio" });

            migrationBuilder.InsertData(
                table: "Amigo",
                columns: new[] { "Id", "Nome", },
                values: new object[] { "8e5a2128-f0a9-4518-8ea4-29ec1e84dda2", "Bruno Quinta" });

            migrationBuilder.InsertData(
                table: "Amigo",
                columns: new[] { "Id", "Nome", },
                values: new object[] { "ea8b60d4-0920-4c40-93be-0bb1664915bb", "Rodolfo Martins" });

            migrationBuilder.InsertData(
                table: "Jogo",
                columns: new[] { "Id", "Nome", "AmigoId" },
                values: new object[] { "ca7b60ba-1cf7-437b-8ae6-c87f335cf895", "Red Dead Redemption", "ea8b60d4-0920-4c40-93be-0bb1664915bb" });

            migrationBuilder.InsertData(
                table: "Jogo",
                columns: new[] { "Id", "Nome", "AmigoId" },
                values: new object[] { "8e5a2128-f0a9-4518-8ea4-29ec1e84dda2", "Spider Man", "ea8b60d4-0920-4c40-93be-0bb1664915bb" });

            migrationBuilder.InsertData(
                table: "Jogo",
                columns: new[] { "Id", "Nome", },
                values: new object[] { "ea8b60d4-0920-4c40-93be-0bb1664915bb", "God of War" });

            migrationBuilder.InsertData(
                table: "Jogo",
                columns: new[] { "Id", "Nome", },
                values: new object[] { "35557ade-9473-4318-bffe-a36e675a5ff8", "The Last of Us" });

            migrationBuilder.InsertData(
                table: "Jogo",
                columns: new[] { "Id", "Nome", },
                values: new object[] { "8640bbba-75c8-4c92-b1c1-320dc9941b1c", "The Last of Us II" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jogo");

            migrationBuilder.DropTable(
                name: "Amigo");
        }
    }
}
