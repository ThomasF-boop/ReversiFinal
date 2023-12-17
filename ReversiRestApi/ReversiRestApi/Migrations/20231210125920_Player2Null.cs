using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReversiRestApi.Migrations
{
    /// <inheritdoc />
    public partial class Player2Null : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_spellen",
                table: "spellen");

            migrationBuilder.RenameTable(
                name: "spellen",
                newName: "Spellen");

            migrationBuilder.AlterColumn<string>(
                name: "Speler2Token",
                table: "Spellen",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Spellen",
                table: "Spellen",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Spellen",
                table: "Spellen");

            migrationBuilder.RenameTable(
                name: "Spellen",
                newName: "spellen");

            migrationBuilder.AlterColumn<string>(
                name: "Speler2Token",
                table: "spellen",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_spellen",
                table: "spellen",
                column: "ID");
        }
    }
}
