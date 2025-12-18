using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyWebProgrammingProject.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4afff304-1c6f-4f3f-b815-77b81f1bea0b", null, "Member", "MEMBER" },
                    { "c516be96-ff2c-4ccb-8b57-42e46d76821b", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BodyType", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "Goal", "Height", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Weight" },
                values: new object[] { "f90241c4-2cd1-45f2-b19e-189ebe59b6c2", 0, null, "2fcc5a61-4562-4ad2-b498-33252431ac52", "g231210000@sakarya.edu.tr", true, "Admin Soyad", null, null, false, null, "G231210000@SAKARYA.EDU.TR", "G231210000@SAKARYA.EDU.TR", "AQAAAAIAAYagAAAAEJ3bYZe13KMDqBlhnylVteU+TVgVoCSuGDFb8IjT5N3/Z5u2FHvm6WCqvCCk72wIkA==", null, false, "45177a18-38cd-4312-a12c-5095a9b2d902", false, "g231210000@sakarya.edu.tr", null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c516be96-ff2c-4ccb-8b57-42e46d76821b", "f90241c4-2cd1-45f2-b19e-189ebe59b6c2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4afff304-1c6f-4f3f-b815-77b81f1bea0b");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c516be96-ff2c-4ccb-8b57-42e46d76821b", "f90241c4-2cd1-45f2-b19e-189ebe59b6c2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c516be96-ff2c-4ccb-8b57-42e46d76821b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f90241c4-2cd1-45f2-b19e-189ebe59b6c2");

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
        }
    }
}
