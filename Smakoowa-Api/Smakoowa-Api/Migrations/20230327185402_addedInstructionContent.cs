using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smakoowa_Api.Migrations
{
    /// <inheritdoc />
    public partial class addedInstructionContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Instructions",
                newName: "Content");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "be655c9b-d352-4eef-b867-e1fc376fee0e");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Instructions",
                newName: "Name");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "980906d9-f6d8-40d9-9f9c-3a57fc407275");
        }
    }
}
