using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smakoowa_Api.Migrations
{
    /// <inheritdoc />
    public partial class addRecipeCommentContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CommentContent",
                table: "RecipeComments",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "ReplyContent",
                table: "CommentReplies",
                newName: "Content");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "060768c2-9403-4e0e-ba2c-a20d5ef28a4a");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "RecipeComments",
                newName: "CommentContent");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "CommentReplies",
                newName: "ReplyContent");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "be655c9b-d352-4eef-b867-e1fc376fee0e");
        }
    }
}
