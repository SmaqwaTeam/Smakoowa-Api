using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Smakoowa_Api.Models.DatabaseModels;
using Smakoowa_Api.Models.Enums;
using Smakoowa_Api.Models.RequestDtos;
using Smakoowa_Api.Models.ResponseDtos;
using Smakoowa_Api.Models.Services;
using System.Net.Http.Json;
using Xunit;

namespace Smakoowa_Api.Tests.IntegrationTests
{
    public class RecipesControllerIntegrationTests : ControllerIntegrationTests
    {
        private readonly int MaxRecipeNameLength;
        private readonly int MinRecipeNameLength;

        public RecipesControllerIntegrationTests(CustomWebApplicationFactory<Program> fixture) : base(fixture)
        {
            MaxRecipeNameLength = int.Parse(_configuration.GetSection($"Validation:Recipe:MaxNameLength").Value);
            MinRecipeNameLength = int.Parse(_configuration.GetSection($"Validation:Recipe:MinNameLength").Value);
        }

        [Theory]
        [InlineData("TestGetAllRecipe1", "TestGetAllRecipe2")]
        public async Task TestGetAll(string testName1, string testName2)
        {
            // Arrange
            var testRecipe1 = await GetRecipe(testName1);
            var testRecipe2 = await GetRecipe(testName2);
            await AddToDatabase(new List<Recipe> { testRecipe1, testRecipe2 });
            string url = "/api/Recipes/GetAll";

            // Act
            var response = await _HttpClient.GetAsync(url);
            var responseContent = await DeserializeResponse<ServiceResponse<List<RecipeResponseDto>>>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.True(responseContent.Content.Exists(c => c.Name == testName1) && responseContent.Content.Exists(c => c.Name == testName2));
        }

        [Theory]
        [InlineData("TestGetByIdRecipe")]
        public async Task TestGetById(string testName)
        {
            // Arrange
            var testRecipe = await GetRecipe(testName);
            await AddToDatabase(testRecipe);
            var savedRecipe = await FindInDatabaseByConditionsFirstOrDefault<Recipe>(c => c.Name == testName);
            string url = $"/api/Recipes/GetById/{savedRecipe.Id}";
            // Act
            var response = await _HttpClient.GetAsync(url);
            var responseContent = await DeserializeResponse<ServiceResponse<RecipeResponseDto>>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.True(responseContent.Content.Id == savedRecipe.Id);

        }

        [Theory]
        [InlineData("TestCreateRecipe")]
        public async Task TestCreate(string testName)
        {
            // Arrange
            RecipeRequestDto recipeRequest = await GetTestRecipeRequestDto(testName);

            string url = $"/api/Recipes/Create";

            // Act
            var response = await _HttpClient.PostAsJsonAsync(url, recipeRequest);
            var responseContent = await DeserializeResponse<ServiceResponse>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.True(await _context.Recipes.AnyAsync(c => c.Name == testName));

        }

        [Theory]
        [InlineData("TestEditRecipe")]
        public async Task TestEdit(string testName)
        {
            // Arrange
            var testRecipe = await GetRecipe("UneditedRecipe");
            await AddToDatabase(testRecipe);
            var uneditedRecipe = await FindInDatabaseByConditionsFirstOrDefault<Recipe>(c => c.Name == "UneditedRecipe");
            string url = $"/api/Recipes/Edit/{uneditedRecipe.Id}";
            RecipeRequestDto recipeRequest = await GetTestRecipeRequestDto(testName);

            // Act
            var response = await _HttpClient.PutAsJsonAsync(url, recipeRequest);
            var responseContent = await DeserializeResponse<ServiceResponse>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.True(await _context.Recipes.AnyAsync(c => c.Name == testName));
        }

        [Theory]
        [InlineData("TestDeleteRecipe")]
        public async Task TestDelete(string testName)
        {
            // Arrange
            var testRecipe = new Recipe { Name = testName };
            await AddToDatabase(testRecipe);
            var uneditedRecipe = await FindInDatabaseByConditionsFirstOrDefault<Recipe>(c => c.Name == testName);
            string url = $"/api/Recipes/Delete/{uneditedRecipe.Id}";
        }

        [Theory]
        [InlineData()]
        public async Task TestRecipeValidation()
        {
            // Arrange
            string minName = "", maxName = "";
            while (minName.Length < MinRecipeNameLength - 1) minName += "a";
            while (maxName.Length <= MaxRecipeNameLength + 1) maxName += "a";
            Recipe recipeMinName = new Recipe { Name = minName };
            Recipe recipeMaxName = new Recipe { Name = maxName };
            RecipeRequestDto RecipeRequestMinName = new RecipeRequestDto { Name = minName };
            RecipeRequestDto RecipeRequestMaxName = new RecipeRequestDto { Name = maxName };

            await AddToDatabase(new List<Recipe> { recipeMinName, recipeMaxName });
            var uneditedRecipes = await FindInDatabaseByConditions<Recipe>(t => t.Name == minName || t.Name == maxName);

            string createUrl = $"/api/Recipes/Create";
            string editUrlMin = $"/api/Recipes/Edit/{uneditedRecipes[0].Id}";
            string editUrlMax = $"/api/Recipes/Edit/{uneditedRecipes[1].Id}";

            // Act
            var responseCreateRecipeRequestMinName = await _HttpClient.PostAsJsonAsync(createUrl, RecipeRequestMinName);
            var responseContentCreateRecipeRequestMinName = await DeserializeResponse<ServiceResponse>(responseCreateRecipeRequestMinName);

            var responseCreateRecipeRequestMaxName = await _HttpClient.PostAsJsonAsync(createUrl, RecipeRequestMaxName);
            var responseContentCreateRecipeRequestMaxName = await DeserializeResponse<ServiceResponse>(responseCreateRecipeRequestMaxName);

            var responseEditRecipeRequestMinName = await _HttpClient.PutAsJsonAsync(editUrlMin, RecipeRequestMinName);
            var responseContentEditRecipeRequestMinName = await DeserializeResponse<ServiceResponse>(responseEditRecipeRequestMinName);

            var responseEditRecipeRequestMaxName = await _HttpClient.PutAsJsonAsync(editUrlMax, RecipeRequestMaxName);
            var responseContentEditRecipeRequestMaxName = await DeserializeResponse<ServiceResponse>(responseEditRecipeRequestMaxName);

            // Assert
            AssertResponseFailure(responseCreateRecipeRequestMinName, responseContentCreateRecipeRequestMinName);
            AssertResponseFailure(responseCreateRecipeRequestMaxName, responseContentCreateRecipeRequestMaxName);
            AssertResponseFailure(responseEditRecipeRequestMinName, responseContentEditRecipeRequestMinName);
            AssertResponseFailure(responseEditRecipeRequestMaxName, responseContentEditRecipeRequestMaxName);
        }

        private async Task<RecipeRequestDto> GetTestRecipeRequestDto(string testName)
        {
            var testRecipeCategory = new Category { Name = "testRecipeCategory" };
            await AddToDatabase(testRecipeCategory);

            RecipeRequestDto recipeRequest = new RecipeRequestDto
            {
                Name = testName,
                ServingsTier = 0,
                TimeToMakeTier = 0,
                CategoryId = (await FindInDatabaseByConditionsFirstOrDefault<Category>(c => c.Name == testRecipeCategory.Name)).Id,
                Ingredients = new List<IngredientRequestDto> { new IngredientRequestDto { Name = "testIngredient", Group = 1, Position = 1 } },
                Instructions = new List<InstructionRequestDto> { new InstructionRequestDto { Content = "testInstruction", Position = 1 } }
            };
            return recipeRequest;
        }

        private async Task<Recipe> GetRecipe(string testName)
        {
            var testRecipeCategory = new Category { Name = "testRecipeCategory" };
            await AddToDatabase(testRecipeCategory);

            Recipe recipeRequest = new Recipe
            {
                Name = testName,
                ServingsTier = 0,
                TimeToMakeTier = 0,
                CategoryId = (await FindInDatabaseByConditionsFirstOrDefault<Category>(c => c.Name == testRecipeCategory.Name)).Id,
                Ingredients = new List<Ingredient> { new Ingredient { Name = "testIngredient", Group = 1, Position = 1 } },
                Instructions = new List<Instruction> { new Instruction { Content = "testInstruction", Position = 1 } }
            };
            return recipeRequest;
        }
    }
}
