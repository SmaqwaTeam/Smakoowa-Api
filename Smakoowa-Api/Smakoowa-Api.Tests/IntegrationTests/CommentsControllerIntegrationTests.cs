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

        [Theory]
        [InlineData("TestCreateRecipeComment")]
        public async Task TestCreateRecipeComment(string testContent)
        {
            // Arrange
            var testRecipe = await AddRecipeToDatabase();
            string url = $"/api/Comments/AddRecipeComment/{testRecipe.Id}";

            RecipeCommentRequestDto recipeCommentRequest = new RecipeCommentRequestDto { Content = testContent };

            // Act
            var response = await _HttpClient.PostAsJsonAsync(url, recipeCommentRequest);
            var responseContent = await DeserializeResponse<ServiceResponse>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.True(await _context.RecipeComments.AnyAsync(c => c.Content == testContent));
        }

        [Theory]
        [InlineData("TestDeleteRecipeComment")]
        public async Task TestDeleteRecipeComment(string testContent)
        {
            var testRecipe = await AddRecipeToDatabase();
            var testRecipeComment = await AddRecipeCommentToDatabase(testRecipe.Id, testContent);
            string url = $"/api/Comments/DeleteRecipeComment/{testRecipeComment.Id}";

            // Act
            var response = await _HttpClient.DeleteAsync(url);
            var responseContent = await DeserializeResponse<ServiceResponse>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.True(!await _context.RecipeComments.AnyAsync(c => c.Id == testRecipeComment.Id));
        }

        [Theory]
        [InlineData("TestCreateCommentReply")]
        public async Task TestCreateCommentReply(string testContent)
        {
            // Arrange
            var testRecipe = await AddRecipeToDatabase();
            var testRecipeComment = await AddRecipeCommentToDatabase(testRecipe.Id, testContent);
            string url = $"/api/Comments/AddCommentReply/{testRecipeComment.Id}";

            CommentReplyRequestDto commentReplyRequestDto = new CommentReplyRequestDto { Content = testContent };

            // Act
            var response = await _HttpClient.PostAsJsonAsync(url, commentReplyRequestDto);
            var responseContent = await DeserializeResponse<ServiceResponse>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.True(await _context.RecipeComments.AnyAsync(c => c.Content == testContent));
        }

        [Theory]
        [InlineData("TestDeleteCommentReply")]
        public async Task TestDeleteCommentReply(string testContent)
        {
            var testRecipe = await AddRecipeToDatabase();
            var testRecipeComment = await AddRecipeCommentToDatabase(testRecipe.Id, testContent);
            var testCommentReply = await AddCommentReplyToDatabase(testRecipeComment.Id, testContent);
            string url = $"/api/Comments/DeleteCommentReply/{testCommentReply.Id}";

            // Act
            var response = await _HttpClient.DeleteAsync(url);
            var responseContent = await DeserializeResponse<ServiceResponse>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
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
            await AddToDatabase(new RecipeComment { Content = content, RecipeId = recipeId });
            return await FindInDatabaseByConditionsFirstOrDefault<RecipeComment>(c => c.Content == content && c.RecipeId == recipeId);
        }

        private async Task<CommentReply> AddCommentReplyToDatabase(int recipeCommentId, string content)
        {
            await AddToDatabase(new CommentReply { Content = content, RepliedCommentId = recipeCommentId});
            return await FindInDatabaseByConditionsFirstOrDefault<CommentReply>(c => c.Content == content && c.RepliedCommentId == recipeCommentId);
        }
    }
}
