using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistradorDeProcessos.WebAPI.Migrations
{
    public partial class CatalogoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Processos",
                columns: table => new
                {
                    Numero = table.Column<string>(nullable: false),
                    Classe = table.Column<string>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    Assunto = table.Column<string>(nullable: true),
                    Origem = table.Column<string>(nullable: true),
                    Distribuicao = table.Column<string>(nullable: true),
                    Relator = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processos", x => x.Numero);
                });

            migrationBuilder.CreateTable(
                name: "Movimentacao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    PossuiAnexo = table.Column<bool>(nullable: false),
                    Legenda = table.Column<string>(nullable: true),
                    ProcessoNumero = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimentacao_Processos_ProcessoNumero",
                        column: x => x.ProcessoNumero,
                        principalTable: "Processos",
                        principalColumn: "Numero",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacao_ProcessoNumero",
                table: "Movimentacao",
                column: "ProcessoNumero");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimentacao");

            migrationBuilder.DropTable(
                name: "Processos");
        }
    }
}
