using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyWebProgrammingProject.Migrations
{
    /// <inheritdoc />
    public partial class FixRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_MemberId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Trainers_TrainerId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Trainers_TrainerId1",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerAvailabilities_Trainers_TrainerId",
                table: "TrainerAvailabilities");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_TrainerId1",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainerAvailabilities",
                table: "TrainerAvailabilities");

            migrationBuilder.DropColumn(
                name: "TrainerId1",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "TrainerAvailabilities",
                newName: "TrainerAvailability");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Appointments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_MemberId",
                table: "Appointments",
                newName: "IX_Appointments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TrainerAvailabilities_TrainerId",
                table: "TrainerAvailability",
                newName: "IX_TrainerAvailability_TrainerId");

            migrationBuilder.AddColumn<string>(
                name: "BodyType",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Goal",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "AspNetUsers",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "AspNetUsers",
                type: "float",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainerAvailability",
                table: "TrainerAvailability",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a2c134d5-0b66-4001-9d68-e5d2e0c29e39", null, "Admin", "ADMIN" },
                    { "ab84d6a4-4b46-428f-8a7a-0f07f2062a33", null, "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BodyType", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "Goal", "Height", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Weight" },
                values: new object[] { "ab87dc3b-8a3b-4959-b8f6-ee317d912eca", 0, null, "7e4e3974-8266-48cf-a72a-a3204b1a1b3c", "g231210000@sakarya.edu.tr", true, "Admin Soyad", null, null, false, null, "G231210000@SAKARYA.EDU.TR", "G231210000@SAKARYA.EDU.TR", "AQAAAAIAAYagAAAAEB/ylyhtLaPNvzbVFNfXsAPJ8/cuAbsE8o+xBavGIrHmYclACiWDJItlkiBFgvUQhQ==", null, false, "9b815825-ae8b-427c-8dea-84de98f9eadd", false, "g231210000@sakarya.edu.tr", null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a2c134d5-0b66-4001-9d68-e5d2e0c29e39", "ab87dc3b-8a3b-4959-b8f6-ee317d912eca" });

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_UserId",
                table: "Appointments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Trainers_TrainerId",
                table: "Appointments",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerAvailability_Trainers_TrainerId",
                table: "TrainerAvailability",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_UserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Trainers_TrainerId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerAvailability_Trainers_TrainerId",
                table: "TrainerAvailability");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainerAvailability",
                table: "TrainerAvailability");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab84d6a4-4b46-428f-8a7a-0f07f2062a33");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a2c134d5-0b66-4001-9d68-e5d2e0c29e39", "ab87dc3b-8a3b-4959-b8f6-ee317d912eca" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2c134d5-0b66-4001-9d68-e5d2e0c29e39");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab87dc3b-8a3b-4959-b8f6-ee317d912eca");

            migrationBuilder.DropColumn(
                name: "BodyType",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Goal",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "TrainerAvailability",
                newName: "TrainerAvailabilities");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Appointments",
                newName: "MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_UserId",
                table: "Appointments",
                newName: "IX_Appointments_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_TrainerAvailability_TrainerId",
                table: "TrainerAvailabilities",
                newName: "IX_TrainerAvailabilities_TrainerId");

            migrationBuilder.AddColumn<int>(
                name: "TrainerId1",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainerAvailabilities",
                table: "TrainerAvailabilities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TrainerId1",
                table: "Appointments",
                column: "TrainerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_MemberId",
                table: "Appointments",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerAvailabilities_Trainers_TrainerId",
                table: "TrainerAvailabilities",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
