using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smakoowa_Api.Migrations
{
    /// <inheritdoc />
    public partial class likeReowrk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "09b50486-bd15-4391-a07b-8c0e5f862b05");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "060768c2-9403-4e0e-ba2c-a20d5ef28a4a");
        }
    }
}
