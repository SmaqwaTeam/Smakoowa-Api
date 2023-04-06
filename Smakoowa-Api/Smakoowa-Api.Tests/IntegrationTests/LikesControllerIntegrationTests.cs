using Microsoft.EntityFrameworkCore;
using Smakoowa_Api.Models.DatabaseModels;
using Smakoowa_Api.Models.DatabaseModels.Comments;
using Smakoowa_Api.Models.DatabaseModels.Likes;
using Smakoowa_Api.Models.Enums;
using Smakoowa_Api.Models.Services;
using System.Net.Http.Json;
using Xunit;

namespace Smakoowa_Api.Tests.IntegrationTests
{
    [Trait("Category", "Integration")]
    public class LikesControllerIntegrationTests : ControllerIntegrationTests
    {
        public LikesControllerIntegrationTests(CustomWebApplicationFactory<Program> fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task TestAddLikes()
        {
            // Arrange
            var testRecipe = await AddRecipeToDatabase();
            var testRecipeComment = await AddRecipeCommentToDatabase(testRecipe.Id, "TestAddLikes");
            var testCommentReply = await AddCommentReplyToDatabase(testRecipeComment.Id, "TestAddLikes");

            string addRecipeLikeUrl = $"/api/Likes/AddRecipeLike/{testRecipe.Id}";
            string addRecipeCommentLikeUrl = $"/api/Likes/AddRecipeCommentLike/{testRecipeComment.Id}";
            string addCommentReplyLikeUrl = $"/api/Likes/AddCommentReplyLike/{testCommentReply.Id}";

            // Act
            var addRecipeLikeResponse = await _HttpClient.PostAsJsonAsync(addRecipeLikeUrl, testRecipe.Id);
            var addRecipeLikeResponseContent = await DeserializeResponse<ServiceResponse>(addRecipeLikeResponse);

            var addRecipeCommentLikeResponse = await _HttpClient.PostAsJsonAsync(addRecipeCommentLikeUrl, testRecipeComment.Id);
            var addRecipeCommentLikeResponseContent = await DeserializeResponse<ServiceResponse>(addRecipeCommentLikeResponse);

            var addCommentReplyLikeResponse = await _HttpClient.PostAsJsonAsync(addCommentReplyLikeUrl, testCommentReply.Id);
            var addCommentReplyLikeResponseContent = await DeserializeResponse<ServiceResponse>(addCommentReplyLikeResponse);

            // Assert
            AssertResponseSuccess(addRecipeLikeResponse, addRecipeLikeResponseContent);
            Assert.True(await _context.Recipes.AnyAsync(c => c.Likes.Count > 0 && c.Id == testRecipe.Id));

            AssertResponseSuccess(addRecipeCommentLikeResponse, addRecipeCommentLikeResponseContent);
            Assert.True(await _context.RecipeComments.AnyAsync(c => c.Likes.Count > 0 && c.Id == testRecipeComment.Id));

            AssertResponseSuccess(addCommentReplyLikeResponse, addCommentReplyLikeResponseContent);
            Assert.True(await _context.CommentReplies.AnyAsync(c => c.Likes.Count > 0 && c.Id == testCommentReply.Id));
        }

        [Fact]
        public async Task TestDeleteLikes()
        {
            var testRecipe = await AddRecipeToDatabase();
            var testRecipeComment = await AddRecipeCommentToDatabase(testRecipe.Id, "TestDeleteLikes");
            var testCommentReply = await AddCommentReplyToDatabase(testRecipeComment.Id, "TestDeleteLikes");

            var recipeLike = await AddToDatabase(new RecipeLike { RecipeId = testRecipe.Id, LikeableType = LikeableType.Recipe });
            var recipeCommentLike = await AddToDatabase(new RecipeCommentLike { RecipeCommentId = testRecipeComment.Id, LikeableType = LikeableType.RecipeComment });
            var commentReplyLike = await AddToDatabase(new CommentReplyLike { CommentReplyId = testCommentReply.Id, LikeableType = LikeableType.CommentReply });

            string removeRecipeLikeUrl = $"/api/Likes/RemoveRecipeLike/{testRecipe.Id}";
            string removeRecipeCommentLikeUrl = $"/api/Likes/RemoveRecipeCommentLike/{testRecipeComment.Id}";
            string removeCommentReplyLikeUrl = $"/api/Likes/RemoveCommentReplyLike/{testCommentReply.Id}";

            // Act
            var removeRecipeLikeResponse = await _HttpClient.DeleteAsync(removeRecipeLikeUrl);
            var removeRecipeLikeResponseContent = await DeserializeResponse<ServiceResponse>(removeRecipeLikeResponse);

            var removeRecipeCommentLikeResponse = await _HttpClient.DeleteAsync(removeRecipeCommentLikeUrl);
            var removeRecipeCommentLikeResponseContent = await DeserializeResponse<ServiceResponse>(removeRecipeCommentLikeResponse);

            var removeCommentReplyLikeResponse = await _HttpClient.DeleteAsync(removeCommentReplyLikeUrl);
            var removeCommentReplyLikeResponseContent = await DeserializeResponse<ServiceResponse>(removeCommentReplyLikeResponse);

            // Assert
            AssertResponseSuccess(removeRecipeLikeResponse, removeRecipeLikeResponseContent);
            Assert.True(await _context.Recipes.AnyAsync(c => c.Likes.Count == 0 && c.Id == testRecipe.Id));

            AssertResponseSuccess(removeRecipeCommentLikeResponse, removeRecipeCommentLikeResponseContent);
            Assert.True(await _context.RecipeComments.AnyAsync(c => c.Likes.Count == 0 && c.Id == testRecipeComment.Id));

            AssertResponseSuccess(removeCommentReplyLikeResponse, removeCommentReplyLikeResponseContent);
            Assert.True(await _context.CommentReplies.AnyAsync(c => c.Likes.Count == 0 && c.Id == testCommentReply.Id));
        }

        private async Task<Recipe> AddRecipeToDatabase()
        {
            var testRecipeCategory = new Category { Name = "testRecipeCategory" };
            await AddToDatabase(testRecipeCategory);
            Recipe recipeRequest = new Recipe
            {
                Name = "commentTestRecipe",
                ServingsTier = 0,
                TimeToMakeTier = 0,
                CategoryId = (await FindInDatabaseByConditionsFirstOrDefault<Category>(c => c.Name == testRecipeCategory.Name)).Id,
                Ingredients = new List<Ingredient> { new Ingredient { Name = "testIngredient", Group = 1, Position = 1 } },
                Instructions = new List<Instruction> { new Instruction { Content = "testInstruction", Position = 1 } },
            };
            return (Recipe)await AddToDatabase(recipeRequest);
        }

        private async Task<RecipeComment> AddRecipeCommentToDatabase(int recipeId, string content)
        {
            return (RecipeComment)await AddToDatabase(new RecipeComment { Content = content, RecipeId = recipeId });
        }

        private async Task<CommentReply> AddCommentReplyToDatabase(int recipeCommentId, string content)
        {
            return (CommentReply)await AddToDatabase(new CommentReply { Content = content, RepliedCommentId = recipeCommentId });
        }
    }
}
