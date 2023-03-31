//using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;
//using Smakoowa_Api.Models.ResponseDtos;
//using Smakoowa_Api.Models.Services;
//using Smakoowa_Api.Tests.Setup;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace Smakoowa_Api.Tests.IntegrationTests
//{
//    //[TestCaseOrderer(
//    //"RunTestsInOrder.XUnit.PriorityOrderer",
//    //"RunTestsInOrder.XUnit")]
//    public partial class ControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
//    {
//        //private HttpClient _HttpClient;
//        ////private readonly IConfiguration _configuration;
//        ////private readonly int MaxNameLength;

//        //public ControllerIntegrationTests(CustomWebApplicationFactory<Program> fixture)
//        //{
//        //    //MaxNameLength = int.Parse(_configuration.GetSection($"Validation:Category:MaxNameLength").Value);
//        //    _HttpClient = fixture._httpClient;
//        //}

//        [Theory, TestPriority(300)]
//        [InlineData("/api/GetAllTags/GetAll")]
//        public async Task T1_GetAllTags_Test1(string url)
//        {
//            // Arrange

//            // Act
//            var response = await _HttpClient.GetAsync(url);
//            var resp = await response.Content.ReadAsStringAsync();
//            ServiceResponse<List<TagResponseDto>> tagResponse = JsonConvert.DeserializeObject<ServiceResponse<List<TagResponseDto>>>(resp);

//            // Assert
//            response.EnsureSuccessStatusCode();
//            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
//        }
//    }
//}
