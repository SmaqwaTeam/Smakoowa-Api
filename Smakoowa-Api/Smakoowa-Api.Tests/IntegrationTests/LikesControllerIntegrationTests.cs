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
        public async Task TestAddRecipeLike()
        {
            // Arrange
            var testRecipe = await AddRecipeToDatabase();

            string addRecipeLikeUrl = $"/api/Likes/AddRecipeLike/{testRecipe.Id}";

            // Act
            var addRecipeLikeResponseContent = await DeserializeResponse<ServiceResponse>
                (await _HttpClient.PostAsJsonAsync(addRecipeLikeUrl, testRecipe.Id));

            // Assert
            AssertResponseSuccess(addRecipeLikeResponseContent);
            Assert.True(await _context.Recipes.AnyAsync(c => c.Likes.Count > 0 && c.Id == testRecipe.Id));
        }

        [Fact]
        public async Task TestAddRecipeCommentLike()
        {
            // Arrange
            var testRecipe = await AddRecipeToDatabase();
            var testRecipeComment = await AddRecipeCommentToDatabase(testRecipe.Id, "TestAddLikes");

            string addRecipeCommentLikeUrl = $"/api/Likes/AddRecipeCommentLike/{testRecipeComment.Id}";

            // Act
            var addRecipeCommentLikeResponseContent = await DeserializeResponse<ServiceResponse>
                (await _HttpClient.PostAsJsonAsync(addRecipeCommentLikeUrl, testRecipeComment.Id));

            // Assert
            AssertResponseSuccess(addRecipeCommentLikeResponseContent);
            Assert.True(await _context.RecipeComments.AnyAsync(c => c.Likes.Count > 0 && c.Id == testRecipeComment.Id));
        }

        [Fact]
        public async Task TestAddCommentReplyLike()
        {
            // Arrange
            var testRecipe = await AddRecipeToDatabase();
            var testRecipeComment = await AddRecipeCommentToDatabase(testRecipe.Id, "TestAddLikes");
            var testCommentReply = await AddCommentReplyToDatabase(testRecipeComment.Id, "TestAddLikes");

            string addCommentReplyLikeUrl = $"/api/Likes/AddCommentReplyLike/{testCommentReply.Id}";

            // Act
            var addCommentReplyLikeResponseContent = await DeserializeResponse<ServiceResponse>
                (await _HttpClient.PostAsJsonAsync(addCommentReplyLikeUrl, testCommentReply.Id));

            // Assert
            AssertResponseSuccess(addCommentReplyLikeResponseContent);
            Assert.True(await _context.CommentReplies.AnyAsync(c => c.Likes.Count > 0 && c.Id == testCommentReply.Id));
        }

        [Fact]
        public async Task TestAddTagLike()
        {
            // Arrange
            var testTag = (Tag)await AddToDatabase(new Tag { Name = "TestLikedTag", TagType = TagType.Diet });

            string addTagLikeUrl = $"/api/Likes/AddTagLike/{testTag.Id}";

            // Act
            var addTagLikeResponseContent = await DeserializeResponse<ServiceResponse>
                (await _HttpClient.PostAsJsonAsync(addTagLikeUrl, testTag.Id));

            // Assert
            AssertResponseSuccess(addTagLikeResponseContent);
            Assert.True(await _context.Tags.AnyAsync(c => c.Likes.Count > 0 && c.Id == testTag.Id));
        }


        [Fact]
        public async Task TestDeleteRecipeLike()
        {
            // Arrange
            var testRecipe = await AddRecipeToDatabase();

            var recipeLike = await AddToDatabase(new RecipeLike
            { RecipeId = testRecipe.Id, LikeableType = LikeableType.Recipe });

            string removeRecipeLikeUrl = $"/api/Likes/RemoveRecipeLike/{testRecipe.Id}";

            // Act
            var removeRecipeLikeResponseContent = await DeserializeResponse<ServiceResponse>
                (await _HttpClient.DeleteAsync(removeRecipeLikeUrl));

            // Assert
            AssertResponseSuccess(removeRecipeLikeResponseContent);
            Assert.True(await _context.Recipes.AnyAsync(c => c.Likes.Count == 0 && c.Id == testRecipe.Id));
        }

        [Fact]
        public async Task TestDeleteRecipeCommentLike()
        {
            // Arrange
            var testRecipe = await AddRecipeToDatabase();
            var testRecipeComment = await AddRecipeCommentToDatabase(testRecipe.Id, "TestDeleteLikes");

            var recipeCommentLike = await AddToDatabase(new RecipeCommentLike
            { RecipeCommentId = testRecipeComment.Id, LikeableType = LikeableType.RecipeComment });

            string removeRecipeCommentLikeUrl = $"/api/Likes/RemoveRecipeCommentLike/{testRecipeComment.Id}";

            // Act
            var removeRecipeCommentLikeResponseContent = await DeserializeResponse<ServiceResponse>
                (await _HttpClient.DeleteAsync(removeRecipeCommentLikeUrl));

            // Assert
            AssertResponseSuccess(removeRecipeCommentLikeResponseContent);
            Assert.True(await _context.RecipeComments.AnyAsync(c => c.Likes.Count == 0 && c.Id == testRecipeComment.Id));
        }

        [Fact]
        public async Task TestDeleteCommentReplyLike()
        {
            // Arrange
            var testRecipe = await AddRecipeToDatabase();
            var testRecipeComment = await AddRecipeCommentToDatabase(testRecipe.Id, "TestDeleteLikes");
            var testCommentReply = await AddCommentReplyToDatabase(testRecipeComment.Id, "TestDeleteLikes");

            var commentReplyLike = await AddToDatabase(new CommentReplyLike
            { CommentReplyId = testCommentReply.Id, LikeableType = LikeableType.CommentReply });

            string removeCommentReplyLikeUrl = $"/api/Likes/RemoveCommentReplyLike/{testCommentReply.Id}";

            // Act
            var removeCommentReplyLikeResponseContent = await DeserializeResponse<ServiceResponse>
                (await _HttpClient.DeleteAsync(removeCommentReplyLikeUrl));

            // Assert
            AssertResponseSuccess(removeCommentReplyLikeResponseContent);
            Assert.True(await _context.CommentReplies.AnyAsync(c => c.Likes.Count == 0 && c.Id == testCommentReply.Id));
        }

        [Fact]
        public async Task TestDeleteTagLike()
        {
            // Arrange
            var testTag = (Tag)await AddToDatabase(new Tag { Name = "TestDeleteLikedTag", TagType = TagType.Diet });

            var tagLike = await AddToDatabase(new TagLike { TagId = testTag.Id, LikeableType = LikeableType.Tag });

            string removeTagLikeUrl = $"/api/Likes/RemoveTagLike/{testTag.Id}";

            // Act
            var removeTagLikeResponseContent = await DeserializeResponse<ServiceResponse>
                (await _HttpClient.DeleteAsync(removeTagLikeUrl));

            // Assert
            AssertResponseSuccess(removeTagLikeResponseContent);
            Assert.True(await _context.Tags.AnyAsync(c => c.Likes.Count == 0 && c.Id == testTag.Id));
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
