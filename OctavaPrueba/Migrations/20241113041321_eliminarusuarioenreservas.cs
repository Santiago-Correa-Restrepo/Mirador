using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OctavaPrueba.Migrations
{
    /// <inheritdoc />
    public partial class eliminarusuarioenreservas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Eliminar el índice en la columna NroDocumentoUsuario
            migrationBuilder.DropIndex(
                name: "IX_Reservas_NroDocumentoUsuario",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK__Reservas__NroDoc__0B91BA14",
                table: "Reservas");

            // Eliminar la columna NroDoc en la tabla Reservas
            migrationBuilder.DropColumn(
                name: "NroDocumentoUsuario",
                table: "Reservas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Restaurar la columna NroDoc en la tabla Reservas
            migrationBuilder.AddColumn<int>(
                name: "NroDoc",
                table: "Reservas",
                type: "int",
                nullable: true); // Cambia el tipo y nulabilidad según tus necesidades

            // Restaurar el índice en la columna NroDocumentoUsuario
            migrationBuilder.CreateIndex(
                name: "IX_Reservas_NroDocumentoUsuario",
                table: "Reservas",
                column: "NroDocumentoUsuario");


            // Restaurar la clave foránea en la tabla Reservas
            migrationBuilder.AddForeignKey(
                name: "FK__Reservas__NroDoc__0B91BA14",
                table: "Reservas",
                column: "NroDoc",
                principalTable: "Usuarios",
                principalColumn: "NroDocumento",
                onDelete: ReferentialAction.Cascade);

        }
    }
}
