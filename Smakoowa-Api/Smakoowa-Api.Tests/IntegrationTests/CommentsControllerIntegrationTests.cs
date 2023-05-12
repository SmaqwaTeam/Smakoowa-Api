using Microsoft.EntityFrameworkCore;
using Smakoowa_Api.Models.DatabaseModels;
using Smakoowa_Api.Models.DatabaseModels.Comments;
using Smakoowa_Api.Models.RequestDtos;
using Smakoowa_Api.Models.Services;
using System.Net.Http.Json;
using Xunit;

namespace Smakoowa_Api.Tests.IntegrationTests
{
    [Trait("Category", "Integration")]
    public class CommentsControllerIntegrationTests : ControllerIntegrationTests
    {
        public CommentsControllerIntegrationTests(CustomWebApplicationFactory<Program> fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task TestCreateRecipeComment()
        {
            // Arrange
            var testRecipe = await AddRecipeToDatabase();
            string url = $"/api/Comments/AddRecipeComment/{testRecipe.Id}";

            RecipeCommentRequestDto recipeCommentRequest = new RecipeCommentRequestDto { Content = "TestCreateRecipeComment" };

            // Act
            var responseContent = await DeserializeResponse<ServiceResponse>(await _HttpClient.PostAsJsonAsync(url, recipeCommentRequest));

            // Assert
            AssertResponseSuccess(responseContent);
            Assert.True(await _context.RecipeComments.AnyAsync(c => c.Content == "TestCreateRecipeComment"));
        }

        [Fact]
        public async Task TestDeleteRecipeComment()
        {
            var testRecipe = await AddRecipeToDatabase();
            var testRecipeComment = await AddRecipeCommentToDatabase(testRecipe.Id, "TestDeleteRecipeComment");
            string url = $"/api/Comments/DeleteRecipeComment/{testRecipeComment.Id}";

            // Act
            var responseContent = await DeserializeResponse<ServiceResponse>(await _HttpClient.DeleteAsync(url));

            // Assert
            AssertResponseSuccess(responseContent);
            Assert.True(!await _context.RecipeComments.AnyAsync(c => c.Id == testRecipeComment.Id));
        }

        [Fact]
        public async Task TestCreateCommentReply()
        {
            // Arrange
            var testRecipe = await AddRecipeToDatabase();
            var testRecipeComment = await AddRecipeCommentToDatabase(testRecipe.Id, "TestCreateCommentReply");
            string url = $"/api/Comments/AddCommentReply/{testRecipeComment.Id}";

            CommentReplyRequestDto commentReplyRequestDto = new CommentReplyRequestDto { Content = "TestCreateCommentReply" };

            // Act
            var responseContent = await DeserializeResponse<ServiceResponse>(await _HttpClient.PostAsJsonAsync(url, commentReplyRequestDto));

            // Assert
            AssertResponseSuccess(responseContent);
            Assert.True(await _context.RecipeComments.AnyAsync(c => c.Content == "TestCreateCommentReply"));
        }

        [Fact]
        public async Task TestDeleteCommentReply()
        {
            var testRecipe = await AddRecipeToDatabase();
            var testRecipeComment = await AddRecipeCommentToDatabase(testRecipe.Id, "TestDeleteCommentReply");
            var testCommentReply = await AddCommentReplyToDatabase(testRecipeComment.Id, "TestDeleteCommentReply");
            string url = $"/api/Comments/DeleteCommentReply/{testCommentReply.Id}";

            // Act
            var responseContent = await DeserializeResponse<ServiceResponse>(await _HttpClient.DeleteAsync(url));

            // Assert
            AssertResponseSuccess(responseContent);
            Assert.True(!await _context.CommentReplies.AnyAsync(c => c.Id == testCommentReply.Id));
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
            await AddToDatabase(recipeRequest);
            return await FindInDatabaseByConditionsFirstOrDefault<Recipe>(c => c.Name == "commentTestRecipe");
        }

        private async Task<RecipeComment> AddRecipeCommentToDatabase(int recipeId, string content)
        {
            await AddToDatabase(new RecipeComment { Content = content, CommentedId = recipeId });
            return await FindInDatabaseByConditionsFirstOrDefault<RecipeComment>(c => c.Content == content && c.CommentedId == recipeId);
        }

        private async Task<CommentReply> AddCommentReplyToDatabase(int recipeCommentId, string content)
        {
            await AddToDatabase(new CommentReply { Content = content, CommentedId = recipeCommentId });
            return await FindInDatabaseByConditionsFirstOrDefault<CommentReply>(c => c.Content == content && c.CommentedId == recipeCommentId);
        }
    }
}
