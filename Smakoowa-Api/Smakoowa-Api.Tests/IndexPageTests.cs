using JSNLog.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Smakoowa_Api.Models.ResponseDtos;
using Smakoowa_Api.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Smakoowa_Api.Tests
{
    public class IndexPageTests :
    IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program>
            _factory;

        public IndexPageTests(
            CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true
            });
        }

        [Theory]
        [InlineData("/api/Categories/GetAll")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            var resp = await response.Content.ReadAsStringAsync();

            ServiceResponse<List<CategoryResponseDto>> categoryResponses = JsonConvert.DeserializeObject<ServiceResponse<List<CategoryResponseDto>>>(resp);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

    }
}
