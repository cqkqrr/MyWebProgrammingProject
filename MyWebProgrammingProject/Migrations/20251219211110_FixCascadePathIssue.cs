using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyWebProgrammingProject.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadePathIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bcd321b9-6c51-46d4-9b74-659f737bf161", null, "Admin", "ADMIN" },
                    { "d897c8b5-22c5-4a4e-855e-921535b45e06", null, "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BodyType", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "Goal", "Height", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Weight" },
                values: new object[] { "70d01384-77ce-40fb-a940-fc1ae82f2ee6", 0, null, "7586e7eb-2d93-4457-93e7-e6812163f080", "g231210000@sakarya.edu.tr", true, "Admin Soyad", null, null, false, null, "G231210000@SAKARYA.EDU.TR", "G231210000@SAKARYA.EDU.TR", "AQAAAAIAAYagAAAAEANOwMTER2XjpMUY5RygTPngFE6eMhMknhd8r9h4CAS6BbCYKUvETKQaqsR5l3+kyQ==", null, false, "52dfb522-a988-4893-aef1-e25fd24bdcc9", false, "g231210000@sakarya.edu.tr", null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "bcd321b9-6c51-46d4-9b74-659f737bf161", "70d01384-77ce-40fb-a940-fc1ae82f2ee6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d897c8b5-22c5-4a4e-855e-921535b45e06");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bcd321b9-6c51-46d4-9b74-659f737bf161", "70d01384-77ce-40fb-a940-fc1ae82f2ee6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bcd321b9-6c51-46d4-9b74-659f737bf161");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "70d01384-77ce-40fb-a940-fc1ae82f2ee6");

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
        }
    }
}
