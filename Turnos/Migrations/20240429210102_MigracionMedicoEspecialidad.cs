using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Turnos.Migrations
{
    /// <inheritdoc />
    public partial class MigracionMedicoEspecialidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicoEspecialidad",
                columns: table => new
                {
                    MedicoID = table.Column<int>(type: "int", nullable: false),
                    EspecialidadID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicoEspecialidad", x => new { x.MedicoID, x.EspecialidadID });
                    table.ForeignKey(
                        name: "FK_MedicoEspecialidad_Especialidad_EspecialidadID",
                        column: x => x.EspecialidadID,
                        principalTable: "Especialidad",
                        principalColumn: "EspecialidadID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicoEspecialidad_Medico_MedicoID",
                        column: x => x.MedicoID,
                        principalTable: "Medico",
                        principalColumn: "MedicoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicoEspecialidad_EspecialidadID",
                table: "MedicoEspecialidad",
                column: "EspecialidadID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicoEspecialidad");
        }
    }
}
