using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TestProgrammationConformit.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "tbl_evenement",
                schema: "public",
                columns: table => new
                {
                    EvenementId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titre = table.Column<string>(maxLength: 100, nullable: false),
                    Personne = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_evenement", x => x.EvenementId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_commentaire",
                schema: "public",
                columns: table => new
                {
                    CommentaireId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(maxLength: 300, nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    EvenementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_commentaire", x => x.CommentaireId);
                    table.ForeignKey(
                        name: "FK_tbl_commentaire_tbl_evenement_EvenementId",
                        column: x => x.EvenementId,
                        principalSchema: "public",
                        principalTable: "tbl_evenement",
                        principalColumn: "EvenementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_commentaire_EvenementId",
                schema: "public",
                table: "tbl_commentaire",
                column: "EvenementId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_commentaire",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tbl_evenement",
                schema: "public");
        }
    }
}
