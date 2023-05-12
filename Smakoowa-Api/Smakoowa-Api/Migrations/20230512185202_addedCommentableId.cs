using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smakoowa_Api.Migrations
{
    /// <inheritdoc />
    public partial class addedCommentableId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReplies_RecipeComments_RepliedCommentId",
                table: "CommentReplies");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeComments_Recipes_RecipeId",
                table: "RecipeComments");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "RecipeComments",
                newName: "CommentedId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeComments_RecipeId",
                table: "RecipeComments",
                newName: "IX_RecipeComments_CommentedId");

            migrationBuilder.RenameColumn(
                name: "RepliedCommentId",
                table: "CommentReplies",
                newName: "CommentedId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentReplies_RepliedCommentId",
                table: "CommentReplies",
                newName: "IX_CommentReplies_CommentedId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "deb06764-fc86-45e4-80ef-1f99b5274bce");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b84b80a3-1a30-4d6c-90fc-bd5ada24998d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "cef80aac-83d8-4a8f-a3f5-ceaa1a9f5fc2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "fe3285ab-355c-4459-a9dd-d4119c4e70b3");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReplies_RecipeComments_CommentedId",
                table: "CommentReplies",
                column: "CommentedId",
                principalTable: "RecipeComments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeComments_Recipes_CommentedId",
                table: "RecipeComments",
                column: "CommentedId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReplies_RecipeComments_CommentedId",
                table: "CommentReplies");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeComments_Recipes_CommentedId",
                table: "RecipeComments");

            migrationBuilder.RenameColumn(
                name: "CommentedId",
                table: "RecipeComments",
                newName: "RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeComments_CommentedId",
                table: "RecipeComments",
                newName: "IX_RecipeComments_RecipeId");

            migrationBuilder.RenameColumn(
                name: "CommentedId",
                table: "CommentReplies",
                newName: "RepliedCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentReplies_CommentedId",
                table: "CommentReplies",
                newName: "IX_CommentReplies_RepliedCommentId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "506ff66d-cf41-44bf-b398-aa26cc7cd889");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "6761c73a-8141-4eae-b8e1-f7c308980b2f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "20cb2ca1-36a9-4748-b191-f9a80f1218df");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9c92d66c-8659-421e-a0a6-e85810c5b7cb");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReplies_RecipeComments_RepliedCommentId",
                table: "CommentReplies",
                column: "RepliedCommentId",
                principalTable: "RecipeComments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeComments_Recipes_RecipeId",
                table: "RecipeComments",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }
    }
}
