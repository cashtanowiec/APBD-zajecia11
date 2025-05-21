using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_zajecia11.Migrations
{
    /// <inheritdoc />
    public partial class RelationsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicamentIdMedicament",
                table: "Medicaments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicaments_MedicamentIdMedicament",
                table: "Medicaments",
                column: "MedicamentIdMedicament");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicaments_Medicaments_MedicamentIdMedicament",
                table: "Medicaments",
                column: "MedicamentIdMedicament",
                principalTable: "Medicaments",
                principalColumn: "IdMedicament");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicaments_Medicaments_MedicamentIdMedicament",
                table: "Medicaments");

            migrationBuilder.DropIndex(
                name: "IX_Medicaments_MedicamentIdMedicament",
                table: "Medicaments");

            migrationBuilder.DropColumn(
                name: "MedicamentIdMedicament",
                table: "Medicaments");
        }
    }
}
