using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smakoowa_Api.Migrations
{
    /// <inheritdoc />
    public partial class addedLikedId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReplyLikes_CommentReplies_CommentReplyId",
                table: "CommentReplyLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeCommentLikes_RecipeComments_RecipeCommentId",
                table: "RecipeCommentLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeLikes_Recipes_RecipeId",
                table: "RecipeLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_TagLikes_Tags_TagId",
                table: "TagLikes");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "TagLikes",
                newName: "LikedId");

            migrationBuilder.RenameIndex(
                name: "IX_TagLikes_TagId",
                table: "TagLikes",
                newName: "IX_TagLikes_LikedId");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "RecipeLikes",
                newName: "LikedId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeLikes_RecipeId",
                table: "RecipeLikes",
                newName: "IX_RecipeLikes_LikedId");

            migrationBuilder.RenameColumn(
                name: "RecipeCommentId",
                table: "RecipeCommentLikes",
                newName: "LikedId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeCommentLikes_RecipeCommentId",
                table: "RecipeCommentLikes",
                newName: "IX_RecipeCommentLikes_LikedId");

            migrationBuilder.RenameColumn(
                name: "CommentReplyId",
                table: "CommentReplyLikes",
                newName: "LikedId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentReplyLikes_CommentReplyId",
                table: "CommentReplyLikes",
                newName: "IX_CommentReplyLikes_LikedId");

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
                name: "FK_CommentReplyLikes_CommentReplies_LikedId",
                table: "CommentReplyLikes",
                column: "LikedId",
                principalTable: "CommentReplies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeCommentLikes_RecipeComments_LikedId",
                table: "RecipeCommentLikes",
                column: "LikedId",
                principalTable: "RecipeComments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeLikes_Recipes_LikedId",
                table: "RecipeLikes",
                column: "LikedId",
                principalTable: "Recipes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TagLikes_Tags_LikedId",
                table: "TagLikes",
                column: "LikedId",
                principalTable: "Tags",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReplyLikes_CommentReplies_LikedId",
                table: "CommentReplyLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeCommentLikes_RecipeComments_LikedId",
                table: "RecipeCommentLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeLikes_Recipes_LikedId",
                table: "RecipeLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_TagLikes_Tags_LikedId",
                table: "TagLikes");

            migrationBuilder.RenameColumn(
                name: "LikedId",
                table: "TagLikes",
                newName: "TagId");

            migrationBuilder.RenameIndex(
                name: "IX_TagLikes_LikedId",
                table: "TagLikes",
                newName: "IX_TagLikes_TagId");

            migrationBuilder.RenameColumn(
                name: "LikedId",
                table: "RecipeLikes",
                newName: "RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeLikes_LikedId",
                table: "RecipeLikes",
                newName: "IX_RecipeLikes_RecipeId");

            migrationBuilder.RenameColumn(
                name: "LikedId",
                table: "RecipeCommentLikes",
                newName: "RecipeCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeCommentLikes_LikedId",
                table: "RecipeCommentLikes",
                newName: "IX_RecipeCommentLikes_RecipeCommentId");

            migrationBuilder.RenameColumn(
                name: "LikedId",
                table: "CommentReplyLikes",
                newName: "CommentReplyId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentReplyLikes_LikedId",
                table: "CommentReplyLikes",
                newName: "IX_CommentReplyLikes_CommentReplyId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "02aa8a8a-a429-40b8-9dc3-5dafe54cc0de");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "93ff19c2-e402-4962-ae64-75a946e90d46");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f70f2508-8410-4f2d-a42d-0ebbde9c68cc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "01cfc594-5dda-4ba2-81e4-ab8a4f21491f");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReplyLikes_CommentReplies_CommentReplyId",
                table: "CommentReplyLikes",
                column: "CommentReplyId",
                principalTable: "CommentReplies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeCommentLikes_RecipeComments_RecipeCommentId",
                table: "RecipeCommentLikes",
                column: "RecipeCommentId",
                principalTable: "RecipeComments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeLikes_Recipes_RecipeId",
                table: "RecipeLikes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TagLikes_Tags_TagId",
                table: "TagLikes",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id");
        }
    }
}
