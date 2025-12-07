using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebProgrammingProject.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadePaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Trainers_TrainerId",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "TrainerId1",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TrainerId1",
                table: "Appointments",
                column: "TrainerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Trainers_TrainerId",
                table: "Appointments",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Trainers_TrainerId1",
                table: "Appointments",
                column: "TrainerId1",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Trainers_TrainerId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Trainers_TrainerId1",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_TrainerId1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "TrainerId1",
                table: "Appointments");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Trainers_TrainerId",
                table: "Appointments",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
