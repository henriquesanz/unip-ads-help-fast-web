using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppSuporteIA.Migrations
{
    /// <inheritdoc />
    public partial class AddChatIaResultsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Usuarios",
                schema: "dbo",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "HistoricoChamados",
                schema: "dbo",
                newName: "HistoricoChamados");

            migrationBuilder.RenameTable(
                name: "Faqs",
                schema: "dbo",
                newName: "Faqs");

            migrationBuilder.RenameTable(
                name: "Chats",
                schema: "dbo",
                newName: "Chats");

            migrationBuilder.RenameTable(
                name: "Chamados",
                schema: "dbo",
                newName: "Chamados");

            migrationBuilder.RenameTable(
                name: "Cargos",
                schema: "dbo",
                newName: "Cargos");

            migrationBuilder.CreateTable(
                name: "ChatIaResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Resolvido = table.Column<bool>(type: "bit", nullable: false),
                    Mensagem = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatIaResults", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatIaResults");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuarios",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "HistoricoChamados",
                newName: "HistoricoChamados",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Faqs",
                newName: "Faqs",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Chats",
                newName: "Chats",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Chamados",
                newName: "Chamados",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Cargos",
                newName: "Cargos",
                newSchema: "dbo");
        }
    }
}
