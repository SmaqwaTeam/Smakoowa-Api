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
        private readonly int _maxCategoryNameLength;
        private readonly int _minCategoryNameLength;
        public CategoriesControllerIntegrationTests(CustomWebApplicationFactory<Program> fixture) : base(fixture)
        {
            _maxCategoryNameLength = int.Parse(_configuration.GetSection($"Validation:Category:MaxNameLength").Value);
            _minCategoryNameLength = int.Parse(_configuration.GetSection($"Validation:Category:MinNameLength").Value);
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
            var responseContent = await DeserializeResponse<ServiceResponse<List<CategoryResponseDto>>>(await _HttpClient.GetAsync(url));

            // Assert
            AssertResponseSuccess(responseContent);
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
            var responseContent = await DeserializeResponse<ServiceResponse<CategoryResponseDto>>(await _HttpClient.GetAsync(url));

            // Assert
            AssertResponseSuccess(responseContent);
            Assert.True(responseContent.Content.Id == savedCategory.Id);
        }

        [Fact]
        public async Task TestCreate()
        {
            // Arrange
            string url = $"/api/Categories/Create";
            CategoryRequestDto categoryRequest = new CategoryRequestDto { Name = "TestCreateCategory" };

            // Act
            var responseContent = await DeserializeResponse<ServiceResponse>(await _HttpClient.PostAsJsonAsync(url, categoryRequest));

            // Assert
            AssertResponseSuccess(responseContent);
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
            var responseContent = await DeserializeResponse<ServiceResponse>(await _HttpClient.PutAsJsonAsync(url, categoryRequest));

            // Assert
            AssertResponseSuccess(responseContent);
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
            var responseContent = await DeserializeResponse<ServiceResponse>(await _HttpClient.DeleteAsync(url));

            // Assert
            AssertResponseSuccess(responseContent);
            Assert.False(await _context.Categories.AnyAsync(c => c.Name == "TestDeleteCategory"));
        }

        [Fact]
        public async Task TestCategoryValidation()
        {
            // Arrange
            string minName = "", maxName = "";

            while (minName.Length < _minCategoryNameLength - 1) minName += "a";
            while (maxName.Length <= _maxCategoryNameLength + 1) maxName += "a";

            Category categoryMinName = new Category { Name = minName };
            Category categoryMaxName = new Category { Name = maxName };
            CategoryRequestDto CategoryRequestMinName = new CategoryRequestDto { Name = minName };
            CategoryRequestDto CategoryRequestMaxName = new CategoryRequestDto { Name = maxName };

            await AddToDatabase(new List<Category> { categoryMinName, categoryMaxName });

            string createUrl = $"/api/Categories/Create";

            // Act
            var responseContentCreateCategoryRequestMinName = await DeserializeResponse<ServiceResponse>
                (await _HttpClient.PostAsJsonAsync(createUrl, CategoryRequestMinName));

            var responseContentCreateCategoryRequestMaxName = await DeserializeResponse<ServiceResponse>
                (await _HttpClient.PostAsJsonAsync(createUrl, CategoryRequestMaxName));

            // Assert
            AssertResponseFailure(responseContentCreateCategoryRequestMinName);
            AssertResponseFailure(responseContentCreateCategoryRequestMaxName);
        }
    }
}
