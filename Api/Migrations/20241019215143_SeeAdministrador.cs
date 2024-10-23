using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalsApi.Migrations
{
    /// <inheritdoc />
    public partial class SeeAdministrador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admnistradores",
                columns: new[] { "Id", "Email", "Perfil", "Senha" },
                values: new object[] { 1, "administrador@teste.com", "Adm", "1234567" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admnistradores",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
