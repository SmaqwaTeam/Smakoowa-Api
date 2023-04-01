using Microsoft.EntityFrameworkCore;
using Smakoowa_Api.Models.DatabaseModels;
using Smakoowa_Api.Models.RequestDtos;
using Smakoowa_Api.Models.ResponseDtos;
using Smakoowa_Api.Models.Services;
using System.Net.Http.Json;
using Xunit;

namespace Smakoowa_Api.Tests.IntegrationTests
{
    public class TagsControllerIntegrationTests : ControllerIntegrationTests
    {
        private readonly int MaxTagNameLength;
        private readonly int MinTagNameLength;

        public TagsControllerIntegrationTests(CustomWebApplicationFactory<Program> fixture) : base(fixture)
        {
            MaxTagNameLength = int.Parse(_configuration.GetSection($"Validation:Tag:MaxNameLength").Value);
            MinTagNameLength = int.Parse(_configuration.GetSection($"Validation:Tag:MinNameLength").Value);
        }

        [Theory]
        [InlineData("TestGetAllTag1", "TestGetAllTag2")]
        public async Task TestGetAll(string testName1, string testName2)
        {
            // Arrange
            var tag1 = new Tag { Name = testName1 };
            var tag2 = new Tag { Name = testName2 };
            await AddToDatabase(
                new List<Tag> {
            tag1,
            tag2
                });

            string url = "/api/Tags/GetAll";

            // Act
            var response = await _HttpClient.GetAsync(url);
            var responseContent = await DeserializeResponse<ServiceResponse<List<TagResponseDto>>>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.True(responseContent.Content.Exists(c => c.Name == testName1) && responseContent.Content.Exists(c => c.Name == testName2));
        }

        [Theory]
        [InlineData("TestGetByIdTag")]
        public async Task TestGetById(string testName)
        {
            // Arrange
            var testTag = new Tag { Name = testName };
            await AddToDatabase(testTag);
            var savedTag = await FindInDatabaseByConditionsFirstOrDefault<Tag>(c => c.Name == testName);
            string url = $"/api/Tags/GetById/{savedTag.Id}";

            // Act
            var response = await _HttpClient.GetAsync(url);
            var responseContent = await DeserializeResponse<ServiceResponse<TagResponseDto>>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.True(responseContent.Content.Id == savedTag.Id);
        }

        [Theory]
        [InlineData("TestCreateTag")]
        public async Task TestCreate(string testName)
        {
            // Arrange
            string url = $"/api/Tags/Create";
            TagRequestDto tagRequest = new TagRequestDto { Name = testName };

            // Act
            var response = await _HttpClient.PostAsJsonAsync(url, tagRequest);
            var responseContent = await DeserializeResponse<ServiceResponse>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.True(await _context.Tags.AnyAsync(c => c.Name == testName));
        }

        [Theory]
        [InlineData("TestEditTag")]
        public async Task TestEdit(string testName)
        {
            // Arrange
            var testTag = new Tag { Name = "UneditedTag" };
            await AddToDatabase(testTag);
            var uneditedTag = await FindInDatabaseByConditionsFirstOrDefault<Tag>(c => c.Name == "UneditedTag");
            string url = $"/api/Tags/Edit/{uneditedTag.Id}";
            TagRequestDto tagRequest = new TagRequestDto { Name = testName };

            // Act
            var response = await _HttpClient.PutAsJsonAsync(url, tagRequest);
            var responseContent = await DeserializeResponse<ServiceResponse>(response);

            // Assert
            AssertResponseSuccess(response, responseContent);
            Assert.True(await _context.Tags.AnyAsync(c => c.Name == testName));
        }

        [Theory]
        [InlineData("TestDeleteTag")]
        public async Task TestDelete(string testName)
        {
            // Arrange
            var testTag = new Tag { Name = testName };
            await AddToDatabase(testTag);
            var uneditedTag = await FindInDatabaseByConditionsFirstOrDefault<Tag>(c => c.Name == testName);
            string url = $"/api/Tags/Delete/{uneditedTag.Id}";
        }

        [Theory]
        [InlineData()]
        public async Task TestTagValidation()
        {
            // Arrange
            string minName = "", maxName = "";
            while (minName.Length < MinTagNameLength - 1) minName += "a";
            while (maxName.Length <= MaxTagNameLength + 1) maxName += "a";

            Tag tagMinName = new Tag { Name = minName };
            Tag tagMaxName = new Tag { Name = maxName };
            TagRequestDto TagRequestMinName = new TagRequestDto { Name = minName };
            TagRequestDto TagRequestMaxName = new TagRequestDto { Name = maxName };

            await AddToDatabase(new List<Tag> { tagMinName, tagMaxName });
            var uneditedTags = await FindInDatabaseByConditions<Tag>(t => t.Name == minName || t.Name == maxName);

            string createUrl = $"/api/Tags/Create";
            string editUrlMin = $"/api/Tags/Edit/{uneditedTags[0].Id}";
            string editUrlMax = $"/api/Tags/Edit/{uneditedTags[1].Id}";

            // Act
            var responseCreateTagRequestMinName = await _HttpClient.PostAsJsonAsync(createUrl, TagRequestMinName);
            var responseContentCreateTagRequestMinName = await DeserializeResponse<ServiceResponse>(responseCreateTagRequestMinName);

            var responseCreateTagRequestMaxName = await _HttpClient.PostAsJsonAsync(createUrl, TagRequestMaxName);
            var responseContentCreateTagRequestMaxName = await DeserializeResponse<ServiceResponse>(responseCreateTagRequestMaxName);

            var responseEditTagRequestMinName = await _HttpClient.PutAsJsonAsync(editUrlMin, TagRequestMinName);
            var responseContentEditTagRequestMinName = await DeserializeResponse<ServiceResponse>(responseEditTagRequestMinName);

            var responseEditTagRequestMaxName = await _HttpClient.PutAsJsonAsync(editUrlMax, TagRequestMaxName);
            var responseContentEditTagRequestMaxName = await DeserializeResponse<ServiceResponse>(responseEditTagRequestMaxName);

            // Assert
            AssertResponseFailure(responseCreateTagRequestMinName, responseContentCreateTagRequestMinName);
            AssertResponseFailure(responseCreateTagRequestMaxName, responseContentCreateTagRequestMaxName);
            AssertResponseFailure(responseEditTagRequestMinName, responseContentEditTagRequestMinName);
            AssertResponseFailure(responseEditTagRequestMaxName, responseContentEditTagRequestMaxName);
        }
    }
}
