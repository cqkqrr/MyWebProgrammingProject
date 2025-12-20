using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebProgrammingProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialGymSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceDuration",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ServiceName",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ServicePrice",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "TrainerName",
                table: "Appointments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceDuration",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceName",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ServicePrice",
                table: "Appointments",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainerName",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
