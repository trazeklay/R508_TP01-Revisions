using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TP01_Révisions.Migrations
{
    public partial class initDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_e_marque_mar",
                columns: table => new
                {
                    mar_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mar_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mar", x => x.mar_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typeproduit_tpd",
                columns: table => new
                {
                    tpd_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tpd_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tpd", x => x.tpd_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_produit_pdt",
                columns: table => new
                {
                    pdt_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pdt_name = table.Column<string>(type: "text", nullable: false),
                    pdt_description = table.Column<string>(type: "text", nullable: true),
                    pdt_photo_name = table.Column<string>(type: "text", nullable: true),
                    pdt_photo_uri = table.Column<string>(type: "text", nullable: true),
                    pdt_tpd_id = table.Column<int>(type: "integer", nullable: false),
                    pdt_mar_id = table.Column<int>(type: "integer", nullable: false),
                    pdt_stock_reel = table.Column<int>(type: "integer", nullable: false),
                    pdt_stock_min = table.Column<int>(type: "integer", nullable: false),
                    pdt_stock_max = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pdt", x => x.pdt_id);
                    table.ForeignKey(
                        name: "fk_pdt_mar",
                        column: x => x.pdt_mar_id,
                        principalTable: "t_e_marque_mar",
                        principalColumn: "mar_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pdt_tpd",
                        column: x => x.pdt_tpd_id,
                        principalTable: "t_e_typeproduit_tpd",
                        principalColumn: "tpd_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_produit_pdt_pdt_mar_id",
                table: "t_e_produit_pdt",
                column: "pdt_mar_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_produit_pdt_pdt_tpd_id",
                table: "t_e_produit_pdt",
                column: "pdt_tpd_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_e_produit_pdt");

            migrationBuilder.DropTable(
                name: "t_e_marque_mar");

            migrationBuilder.DropTable(
                name: "t_e_typeproduit_tpd");
        }
    }
}
