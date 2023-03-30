using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Smakoowa_Api.Models.RequestDtos;
using Smakoowa_Api.Models.ResponseDtos;
using Smakoowa_Api.Models.Services;
using Xunit;

namespace Smakoowa_Api.Tests
{
    public class IndexPageTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        //private readonly HttpClient _client;
        //private readonly CustomWebApplicationFactory<Program> _factory;

        //public IndexPageTests(CustomWebApplicationFactory<Program> factory)
        //{
        //    _factory = factory;
        //    _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        //    {
        //        AllowAutoRedirect = true
        //    });
        //}

        private HttpClient _HttpClient;
        ////private const string _BaseRequestUri = "/api/myentities";

        public IndexPageTests(CustomWebApplicationFactory<Program> fixture)
        {
            _HttpClient = fixture._httpClient;
            //fixture.SeedDataToContext();
        }

        [Theory]
        //[InlineData("/api/Categories/GetAll")]
        [InlineData("/api/Categories/GetAll")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {

            // Arrange
            var client = _HttpClient;//_factory.CreateClient();

            // Act
            //var request1 = await client.DeleteAsync("/api/Categories/Delete/3");
            //var request2 = await client.DeleteAsync("/api/Categories/Delete/3");

            //ServiceResponse req1 = JsonConvert.DeserializeObject<ServiceResponse>(await request1.Content.ReadAsStringAsync());
            //ServiceResponse req2 = JsonConvert.DeserializeObject<ServiceResponse>(await request2.Content.ReadAsStringAsync());

            //CategoryRequestDto categoryRequestDto = new CategoryRequestDto { Name = "" };

            //var response = await client.PostAsync(url, categoryRequestDto);
            var response = await client.GetAsync(url);

            var resp = await response.Content.ReadAsStringAsync();


            ServiceResponse<List<CategoryResponseDto>> categoryResponses = JsonConvert.DeserializeObject<ServiceResponse<List<CategoryResponseDto>>>(resp);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

    }
}
