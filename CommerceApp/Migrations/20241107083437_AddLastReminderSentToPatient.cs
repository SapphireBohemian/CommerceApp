using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommerceApp.Migrations
{
    /// <inheritdoc />
    public partial class AddLastReminderSentToPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastReminderSent",
                table: "Patients",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastReminderSent",
                table: "Patients");
        }
    }
}
