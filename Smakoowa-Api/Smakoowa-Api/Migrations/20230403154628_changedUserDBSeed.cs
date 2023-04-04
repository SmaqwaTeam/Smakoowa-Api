using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smakoowa_Api.Migrations
{
    /// <inheritdoc />
    public partial class changedUserDBSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "b4e18a37-0650-479f-9b42-8f4e4a0469e9", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "0776b492-a223-4946-be8c-048a7f4fe4a3", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash" },
                values: new object[] { "e8256c57-6202-4366-9024-1fd651155728", null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash" },
                values: new object[] { "6349eb2c-a4bd-4d13-a974-94b65df60d12", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "b90cb068-5cdf-4deb-af7a-cb1e4324b29e", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "5f20fd1c-6433-4a4c-8ec2-77f178038678", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash" },
                values: new object[] { "78858c04-50bf-4392-9e0e-24c33bdd784b", "placeholderAdmin@test.com", "123" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash" },
                values: new object[] { "3e8a6eb5-2eea-40d1-a4d3-16c7ef4a625e", "placeholderUser@test.com", "123" });
        }
    }
}
