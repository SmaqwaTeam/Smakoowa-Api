using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Smakoowa_Api.Tests
{
    public class BasicTests
    : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public BasicTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        //[Theory]
        //[InlineData("api/Categories/GetAll")]
        ////[InlineData("/")]
        ////[InlineData("/Index")]
        ////[InlineData("/About")]
        ////[InlineData("/Privacy")]
        ////[InlineData("/Contact")]
        //public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        //{
        //    // Arrange
        //    var client = _factory.CreateClient();

        //    // Act
        //    var response = await client.GetAsync(url);

        //    // Assert
        //    response.EnsureSuccessStatusCode(); // Status Code 200-299
        //    Assert.Equal("text/html; charset=utf-8",
        //        response.Content.Headers.ContentType.ToString());
        //}
    }
}
