using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyWebProgrammingProject.Migrations
{
    /// <inheritdoc />
    public partial class FixTrainerServiceCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Trainers_TrainerId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_TrainerId",
                table: "Services");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a484d1d4-77a1-4a1e-9e30-de08ade0962e");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8763fe83-af84-4301-85a2-d0388dfebd1b", "04caee47-e5aa-41d0-b16e-23cb1127756d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8763fe83-af84-4301-85a2-d0388dfebd1b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "04caee47-e5aa-41d0-b16e-23cb1127756d");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Services");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "MemberProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Goal = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTrainer",
                columns: table => new
                {
                    ServicesId = table.Column<int>(type: "int", nullable: false),
                    TrainersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTrainer", x => new { x.ServicesId, x.TrainersId });
                    table.ForeignKey(
                        name: "FK_ServiceTrainer_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceTrainer_Trainers_TrainersId",
                        column: x => x.TrainersId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "801854d6-e171-415b-84b8-0703412cb7c0", null, "Member", "MEMBER" },
                    { "fa54f39b-75da-43ce-b9de-bb28ce98b22e", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BodyType", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "Goal", "Height", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Weight" },
                values: new object[] { "fc533ffd-7582-4090-a7c3-8125df85ebc5", 0, null, "39cf352c-6418-4d04-9e6a-9bd91e23c3eb", "g231210000@sakarya.edu.tr", true, "Admin Soyad", null, null, false, null, "G231210000@SAKARYA.EDU.TR", "G231210000@SAKARYA.EDU.TR", "AQAAAAIAAYagAAAAEPJJFMwJBwoPY7KTWlt3hTVpe7rbIT7odEsSABYAlDPV33G2/XxI27BqhzePZZGSpA==", null, false, "abfbb94f-54a9-4c44-9e50-6f24a6aef965", false, "g231210000@sakarya.edu.tr", null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "fa54f39b-75da-43ce-b9de-bb28ce98b22e", "fc533ffd-7582-4090-a7c3-8125df85ebc5" });

            migrationBuilder.CreateIndex(
                name: "IX_MemberProfiles_UserId",
                table: "MemberProfiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTrainer_TrainersId",
                table: "ServiceTrainer",
                column: "TrainersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberProfiles");

            migrationBuilder.DropTable(
                name: "ServiceTrainer");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "801854d6-e171-415b-84b8-0703412cb7c0");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "fa54f39b-75da-43ce-b9de-bb28ce98b22e", "fc533ffd-7582-4090-a7c3-8125df85ebc5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa54f39b-75da-43ce-b9de-bb28ce98b22e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fc533ffd-7582-4090-a7c3-8125df85ebc5");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8763fe83-af84-4301-85a2-d0388dfebd1b", null, "Admin", "ADMIN" },
                    { "a484d1d4-77a1-4a1e-9e30-de08ade0962e", null, "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BodyType", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "Goal", "Height", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Weight" },
                values: new object[] { "04caee47-e5aa-41d0-b16e-23cb1127756d", 0, null, "1557f725-cd8c-4cdf-a016-c3c42c289166", "g231210000@sakarya.edu.tr", true, "Admin Soyad", null, null, false, null, "G231210000@SAKARYA.EDU.TR", "G231210000@SAKARYA.EDU.TR", "AQAAAAIAAYagAAAAEIsFUPgfowCx7IOeDW78ey3yY2TKOnLQbTzAyd4c52tm85jB2MkZi5wPYIcV0e+D6Q==", null, false, "5f0e2ba3-e0e2-44c5-9829-24705ed46c9c", false, "g231210000@sakarya.edu.tr", null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8763fe83-af84-4301-85a2-d0388dfebd1b", "04caee47-e5aa-41d0-b16e-23cb1127756d" });

            migrationBuilder.CreateIndex(
                name: "IX_Services_TrainerId",
                table: "Services",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Trainers_TrainerId",
                table: "Services",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id");
        }
    }
}
