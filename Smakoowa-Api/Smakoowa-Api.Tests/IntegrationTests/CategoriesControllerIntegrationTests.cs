using Microsoft.EntityFrameworkCore;
using Smakoowa_Api.Models.DatabaseModels;
using Smakoowa_Api.Models.RequestDtos;
using Smakoowa_Api.Models.ResponseDtos;
using Smakoowa_Api.Models.Services;
using System.Net.Http.Json;
using Xunit;

namespace Smakoowa_Api.Tests.IntegrationTests
{
    [Trait("Category", "Integration")]
    public class CategoriesControllerIntegrationTests : ControllerIntegrationTests
    {
        private readonly int MaxCategoryNameLength;
        private readonly int MinCategoryNameLength;
        public CategoriesControllerIntegrationTests(CustomWebApplicationFactory<Program> fixture) : base(fixture)
        {
            MaxCategoryNameLength = int.Parse(_configuration.GetSection($"Validation:Category:MaxNameLength").Value);
            MinCategoryNameLength = int.Parse(_configuration.GetSection($"Validation:Category:MinNameLength").Value);
        }

        [Fact]
        public async Task TestGetAll()
        {
            // Arrange
            var category1 = new Category { Name = "TestGetAllCategory1" };
            var category2 = new Category { Name = "TestGetAllCategory2" };
            await AddToDatabase(
                new List<Category> {
                    category1,
                    category2
                });

            string url = "/api/Categories/GetAll";
            // Act
            var response = await _HttpClient.GetAsync(url);
            var responseContent = await DeserializeResponse<ServiceResponse<List<CategoryResponseDto>>>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.True(responseContent.Content.Exists(c => c.Name == "TestGetAllCategory1") && responseContent.Content.Exists(c => c.Name == "TestGetAllCategory2"));
        }

        [Fact]
        public async Task TestGetById()
        {
            // Arrange
            var testCategory = new Category { Name = "TestGetByIdCategory" };
            await AddToDatabase(testCategory);
            var savedCategory = await FindInDatabaseByConditionsFirstOrDefault<Category>(c => c.Name == "TestGetByIdCategory");
            string url = $"/api/Categories/GetById/{savedCategory.Id}";

            // Act
            var response = await _HttpClient.GetAsync(url);
            var responseContent = await DeserializeResponse<ServiceResponse<CategoryResponseDto>>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.True(responseContent.Content.Id == savedCategory.Id);
        }

        [Fact]
        public async Task TestCreate()
        {
            // Arrange
            string url = $"/api/Categories/Create";
            CategoryRequestDto categoryRequest = new CategoryRequestDto { Name = "TestCreateCategory" };

            // Act
            var response = await _HttpClient.PostAsJsonAsync(url, categoryRequest);
            var responseContent = await DeserializeResponse<ServiceResponse>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.True(await _context.Categories.AnyAsync(c => c.Name == "TestCreateCategory"));
        }

        [Fact]
        public async Task TestEdit()
        {
            // Arrange
            var testCategory = new Category { Name = "UneditedCategory" };
            await AddToDatabase(testCategory);
            var uneditedCategory = await FindInDatabaseByConditionsFirstOrDefault<Category>(c => c.Name == "UneditedCategory");
            string url = $"/api/Categories/Edit/{uneditedCategory.Id}";
            CategoryRequestDto categoryRequest = new CategoryRequestDto { Name = "TestEditCategory" };

            // Act
            var response = await _HttpClient.PutAsJsonAsync(url, categoryRequest);
            var responseContent = await DeserializeResponse<ServiceResponse>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.True(await _context.Categories.AnyAsync(c => c.Name == "TestEditCategory"));
        }

        [Fact]
        public async Task TestDelete()
        {
            // Arrange
            var testCategory = new Category { Name = "TestDeleteCategory" };
            await AddToDatabase(testCategory);
            var uneditedCategory = await FindInDatabaseByConditionsFirstOrDefault<Category>(c => c.Name == "TestDeleteCategory");
            string url = $"/api/Categories/Delete/{uneditedCategory.Id}";

            // Act
            var response = await _HttpClient.DeleteAsync(url);
            var responseContent = await DeserializeResponse<ServiceResponse>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.False(await _context.Categories.AnyAsync(c => c.Name == "TestDeleteCategory"));
        }

        [Fact]
        public async Task TestCategoryValidation()
        {
            // Arrange
            string minName = "", maxName = "";

            while (minName.Length < MinCategoryNameLength - 1) minName += "a";
            while (maxName.Length <= MaxCategoryNameLength + 1) maxName += "a";

            Category categoryMinName = new Category { Name = minName };
            Category categoryMaxName = new Category { Name = maxName };
            CategoryRequestDto CategoryRequestMinName = new CategoryRequestDto { Name = minName };
            CategoryRequestDto CategoryRequestMaxName = new CategoryRequestDto { Name = maxName };

            await AddToDatabase(new List<Category> { categoryMinName, categoryMaxName });
            var uneditedCategories = await FindInDatabaseByConditions<Category>(c => c.Name == minName || c.Name == maxName);

            string createUrl = $"/api/Categories/Create";
            string editUrlMin = $"/api/Categories/Edit/{uneditedCategories[0].Id}";
            string editUrlMax = $"/api/Categories/Edit/{uneditedCategories[1].Id}";

            // Act
            var responseCreateCategoryRequestMinName = await _HttpClient.PostAsJsonAsync(createUrl, CategoryRequestMinName);
            var responseContentCreateCategoryRequestMinName = await DeserializeResponse<ServiceResponse>(responseCreateCategoryRequestMinName);

            var responseCreateCategoryRequestMaxName = await _HttpClient.PostAsJsonAsync(createUrl, CategoryRequestMaxName);
            var responseContentCreateCategoryRequestMaxName = await DeserializeResponse<ServiceResponse>(responseCreateCategoryRequestMaxName);

            var responseEditCategoryRequestMinName = await _HttpClient.PutAsJsonAsync(editUrlMin, CategoryRequestMinName);
            var responseContentEditCategoryRequestMinName = await DeserializeResponse<ServiceResponse>(responseEditCategoryRequestMinName);

            var responseEditCategoryRequestMaxName = await _HttpClient.PutAsJsonAsync(editUrlMax, CategoryRequestMaxName);
            var responseContentEditCategoryRequestMaxName = await DeserializeResponse<ServiceResponse>(responseEditCategoryRequestMaxName);

            // Assert
            AssertResponseFailure(responseCreateCategoryRequestMinName, responseContentCreateCategoryRequestMinName);
            AssertResponseFailure(responseCreateCategoryRequestMaxName, responseContentCreateCategoryRequestMaxName);
            AssertResponseFailure(responseEditCategoryRequestMinName, responseContentEditCategoryRequestMinName);
            AssertResponseFailure(responseEditCategoryRequestMaxName, responseContentEditCategoryRequestMaxName);
        }
    }
}
