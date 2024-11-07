using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommerceApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatientVitalSigns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // If you want to make it nullable
            migrationBuilder.AlterColumn<string>(
                name: "VitalSigns",
                table: "Patients",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VitalSigns",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
