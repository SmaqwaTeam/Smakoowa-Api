using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Smakoowa_Api.Data;
using Smakoowa_Api.Models.DatabaseModels;
using Smakoowa_Api.Models.Enums;
using Smakoowa_Api.Models.Interfaces;
using Smakoowa_Api.Models.ResponseDtos;
using Smakoowa_Api.Models.Services;
using Xunit;

namespace Smakoowa_Api.Tests.IntegrationTests
{
    public class CategoriesControllerIntegrationTests : ControllerIntegrationTests
    {
        private readonly int MaxCategoryNameLength;
        private readonly int MinCategoryNameLength;
        public CategoriesControllerIntegrationTests(CustomWebApplicationFactory<Program> fixture) : base(fixture)
        {
            MaxCategoryNameLength = int.Parse(_configuration.GetSection($"Validation:Category:MaxNameLength").Value);
            MinCategoryNameLength = int.Parse(_configuration.GetSection($"Validation:Category:MinNameLength").Value);
        }

        [Theory]
        [InlineData("/api/Tags/GetAll")]
        public async Task GetAllTags(string url)
        {
            // Arrange
            var tag1 = new Tag { Name = "TestTag1 ", TagType = TagType.Cuisine };
            var tag2 = new Tag { Name = "TestTag2 ", TagType = TagType.Diet };
            await AddToDatabase(
                new List<Tag> {
                    tag1,
                    tag2
                });

            // Act
            var response = await _HttpClient.GetAsync(url);
            var responseContent = await DeserializeResponse<ServiceResponse<List<TagResponseDto>>>(response);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.True(responseContent.SuccessStatus);
            Assert.True(responseContent.Content.Any(t => t.Name == tag1.Name || t.Name == tag2.Name));
        }

        

        //[Theory]
        //[InlineData("/api/Categories/Create")]
        //public async Task CreateCategory(string url)
        //{
        //    // Arrange
        //    CategoryRequestDto categoryRequestDto = new CategoryRequestDto { Name = "" };
        //    var myContent = JsonConvert.SerializeObject(categoryRequestDto);
        //    var stringContent = new StringContent(myContent, Encoding.UTF8, "application/json");

        //    // Act
        //    var response = await _HttpClient.PostAsync(url, stringContent);
        //    var resp = await response.Content.ReadAsStringAsync();
        //    ServiceResponse categoryResponses = JsonConvert.DeserializeObject<ServiceResponse>(resp);

        //    // Assert
        //    response.EnsureSuccessStatusCode(); // Status Code 200-299
        //    Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        //}

        //[Theory]
        //[InlineData(0)]
        //[InlineData(31)]
        //public async Task CreateCategoryValidation(int categoryNameLength)
        //{
        //    // Arrange
        //    CategoryRequestDto categoryRequestDto = new CategoryRequestDto { Name = "" };
        //    var myContent = JsonConvert.SerializeObject(categoryRequestDto);
        //    var stringContent = new StringContent(myContent, Encoding.UTF8, "application/json");

        //    // Act
        //    var response = await _HttpClient.PostAsync("/api/Categories/Create", stringContent);
        //    var resp = await response.Content.ReadAsStringAsync();
        //    ServiceResponse categoryResponses = JsonConvert.DeserializeObject<ServiceResponse>(resp);

        //    // Assert
        //    response.EnsureSuccessStatusCode(); // Status Code 200-299
        //    Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        //}
    }
}
