using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalDataApps.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoctorID",
                table: "Medication",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Medication_DoctorID",
                table: "Medication",
                column: "DoctorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Medication_Doctor_DoctorID",
                table: "Medication",
                column: "DoctorID",
                principalTable: "Doctor",
                principalColumn: "DoctorID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medication_Doctor_DoctorID",
                table: "Medication");

            migrationBuilder.DropIndex(
                name: "IX_Medication_DoctorID",
                table: "Medication");

            migrationBuilder.DropColumn(
                name: "DoctorID",
                table: "Medication");
        }
    }
}
