using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smakoowa_Api.Migrations
{
    /// <inheritdoc />
    public partial class cascadeDeleteComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReplies_RecipeComments_CommentedId",
                table: "CommentReplies");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "eccdc42f-c508-4447-8083-da2e8d3abfaf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "447d39d8-3330-44ac-9d8a-475158d9e1ad");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c5a5f432-d084-4f26-868a-165796e7269e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a36e47d8-1ae0-4343-8c78-aa5f2cb54026");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReplies_RecipeComments_CommentedId",
                table: "CommentReplies",
                column: "CommentedId",
                principalTable: "RecipeComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReplies_RecipeComments_CommentedId",
                table: "CommentReplies");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2fa7bd53-12dc-47d9-ad73-8aac9d35a20f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "91d88998-0732-4c18-97cc-babe6048d647");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "5f290757-cbaa-4713-810c-4197bba08b43");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "15b37ad4-76ce-40eb-a70c-c86a05b2e1f8");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReplies_RecipeComments_CommentedId",
                table: "CommentReplies",
                column: "CommentedId",
                principalTable: "RecipeComments",
                principalColumn: "Id");
        }
    }
}
