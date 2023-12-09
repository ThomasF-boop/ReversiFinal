using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReversiRestApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "spellen",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Omschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Speler1Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Speler2Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AandeBeurt = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spellen", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "spellen");
        }
    }
}
