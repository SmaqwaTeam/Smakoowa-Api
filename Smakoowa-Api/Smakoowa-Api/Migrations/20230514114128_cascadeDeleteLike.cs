using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smakoowa_Api.Migrations
{
    /// <inheritdoc />
    public partial class cascadeDeleteLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_CommentReplyLikes_CommentReplies_LikedId",
                table: "CommentReplyLikes",
                column: "LikedId",
                principalTable: "CommentReplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeCommentLikes_RecipeComments_LikedId",
                table: "RecipeCommentLikes",
                column: "LikedId",
                principalTable: "RecipeComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeLikes_Recipes_LikedId",
                table: "RecipeLikes",
                column: "LikedId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagLikes_Tags_LikedId",
                table: "TagLikes",
                column: "LikedId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
    }
}
