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
    [Trait("Category", "Integration")]
    public class RecipesControllerIntegrationTests : ControllerIntegrationTests
    {
        private readonly int MaxRecipeNameLength;
        private readonly int MinRecipeNameLength;

        public RecipesControllerIntegrationTests(CustomWebApplicationFactory<Program> fixture) : base(fixture)
        {
            MaxRecipeNameLength = int.Parse(_configuration.GetSection($"Validation:Recipe:MaxNameLength").Value);
            MinRecipeNameLength = int.Parse(_configuration.GetSection($"Validation:Recipe:MinNameLength").Value);
        }

        [Fact]
        public async Task TestGetAll()
        {
            // Arrange
            var testRecipe1 = await GetRecipe("TestGetAllRecipe1");
            var testRecipe2 = await GetRecipe("TestGetAllRecipe2");
            await AddToDatabase(new List<Recipe> { testRecipe1, testRecipe2 });
            string url = "/api/Recipes/GetAll";

            // Act
            var response = await _HttpClient.GetAsync(url);
            var responseContent = await DeserializeResponse<ServiceResponse<List<RecipeResponseDto>>>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.True(responseContent.Content.Exists(c => c.Name == "TestGetAllRecipe1") && responseContent.Content.Exists(c => c.Name == "TestGetAllRecipe2"));
        }

        [Fact]
        public async Task TestGetById()
        {
            // Arrange
            var testRecipe = await GetRecipe("TestGetByIdRecipe");
            await AddToDatabase(testRecipe);
            var savedRecipe = await FindInDatabaseByConditionsFirstOrDefault<Recipe>(c => c.Name == "TestGetByIdRecipe");
            string url = $"/api/Recipes/GetById/{savedRecipe.Id}";

            // Act
            var response = await _HttpClient.GetAsync(url);
            var responseContent = await DeserializeResponse<ServiceResponse<RecipeResponseDto>>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.True(responseContent.Content.Id == savedRecipe.Id);
        }

        [Fact]
        public async Task TestCreate()
        {
            // Arrange
            RecipeRequestDto recipeRequest = await GetTestRecipeRequestDto("TestCreateRecipe");

            string url = $"/api/Recipes/Create";

            // Act
            var response = await _HttpClient.PostAsJsonAsync(url, recipeRequest);
            var responseContent = await DeserializeResponse<ServiceResponse>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.True(await _context.Recipes.AnyAsync(c => c.Name == "TestCreateRecipe"));
        }

        [Fact]
        public async Task TestEdit()
        {
            // Arrange
            var testRecipe = await GetRecipe("UneditedRecipe");
            await AddToDatabase(testRecipe);
            var uneditedRecipe = await FindInDatabaseByConditionsFirstOrDefault<Recipe>(c => c.Name == "UneditedRecipe");
            string url = $"/api/Recipes/Edit/{uneditedRecipe.Id}";
            RecipeRequestDto recipeRequest = await GetTestRecipeRequestDto("TestEditRecipe");

            // Act
            var response = await _HttpClient.PutAsJsonAsync(url, recipeRequest);
            var responseContent = await DeserializeResponse<ServiceResponse>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.True(await _context.Recipes.AnyAsync(c => c.Name == "TestEditRecipe"));
        }

        [Fact]
        public async Task TestDelete()
        {
            // Arrange
            var testRecipe = await GetRecipe("TestDeleteRecipe");
            var recipeToDelete = await AddToDatabase(testRecipe);
            string url = $"/api/Recipes/Delete/{recipeToDelete.Id}";

            // Act
            var response = await _HttpClient.DeleteAsync(url);
            var responseContent = await DeserializeResponse<ServiceResponse>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.True(!await _context.Recipes.AnyAsync(c => c.Id == recipeToDelete.Id));
        }

        [Fact]
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
