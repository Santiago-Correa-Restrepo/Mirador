using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OctavaPrueba.Migrations
{
    /// <inheritdoc />
    public partial class eliminarusuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Reservas__NroDoc__0B91BA14",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_NroDocumentoUsuario",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "NroDocumentoUsuario",
                table: "Reservas");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioNroDocumento",
                table: "Reservas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_UsuarioNroDocumento",
                table: "Reservas",
                column: "UsuarioNroDocumento");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Usuarios_UsuarioNroDocumento",
                table: "Reservas",
                column: "UsuarioNroDocumento",
                principalTable: "Usuarios",
                principalColumn: "NroDocumento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Usuarios_UsuarioNroDocumento",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_UsuarioNroDocumento",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "UsuarioNroDocumento",
                table: "Reservas");

            migrationBuilder.AddColumn<int>(
                name: "NroDocumentoUsuario",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_NroDocumentoUsuario",
                table: "Reservas",
                column: "NroDocumentoUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK__Reservas__NroDoc__0B91BA14",
                table: "Reservas",
                column: "NroDocumentoUsuario",
                principalTable: "Usuarios",
                principalColumn: "NroDocumento");
        }
    }
}
