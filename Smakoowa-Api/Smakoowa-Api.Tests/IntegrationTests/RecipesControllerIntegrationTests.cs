//using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;
//using Smakoowa_Api.Models.ResponseDtos;
//using Smakoowa_Api.Models.Services;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace Smakoowa_Api.Tests.IntegrationTests
//{
//    public class RecipesControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
//    {
//        private HttpClient _HttpClient;
//        private readonly IConfiguration _configuration;
//        private readonly int MaxNameLength;

//        public RecipesControllerIntegrationTests(CustomWebApplicationFactory<Program> fixture)
//        {
//            MaxNameLength = int.Parse(_configuration.GetSection($"Validation:Category:MaxNameLength").Value);
//            _HttpClient = fixture._httpClient;
//        }

//        [Theory]
//        [InlineData("/api/Categories/GetAll")]
//        public async Task GetAllCategories(string url)
//        {
//            // Arrange

//            // Act
//            var response = await _HttpClient.GetAsync(url);
//            var resp = await response.Content.ReadAsStringAsync();
//            ServiceResponse<List<CategoryResponseDto>> categoryResponses = JsonConvert.DeserializeObject<ServiceResponse<List<CategoryResponseDto>>>(resp);

//            // Assert
//            response.EnsureSuccessStatusCode();
//            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
//        }
//    }
//}
