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
    public class TagsControllerIntegrationTests : ControllerIntegrationTests
    {
        private readonly int MaxTagNameLength;
        private readonly int MinTagNameLength;

        public TagsControllerIntegrationTests(CustomWebApplicationFactory<Program> fixture) : base(fixture)
        {
            MaxTagNameLength = int.Parse(_configuration.GetSection($"Validation:Tag:MaxNameLength").Value);
            MinTagNameLength = int.Parse(_configuration.GetSection($"Validation:Tag:MinNameLength").Value);
        }

        [Fact]
        public async Task TestGetAll()
        {
            // Arrange
            var tag1 = new Tag { Name = "TestGetAllTag1" };
            var tag2 = new Tag { Name = "TestGetAllTag2" };
            await AddToDatabase(
                new List<Tag> { tag1, tag2 });

            string url = "/api/Tags/GetAll";

            // Act
            var responseContent = await DeserializeResponse<ServiceResponse<List<TagResponseDto>>>(await _HttpClient.GetAsync(url));

            // Assert
            AssertResponseSuccess(responseContent);
            Assert.True(responseContent.Content.Exists(c => c.Name == "TestGetAllTag1")
                && responseContent.Content.Exists(c => c.Name == "TestGetAllTag2"));
        }

        [Fact]
        public async Task TestGetById()
        {
            // Arrange
            var testTag = new Tag { Name = "TestGetByIdTag" };
            await AddToDatabase(testTag);
            var savedTag = await FindInDatabaseByConditionsFirstOrDefault<Tag>(c => c.Name == "TestGetByIdTag");
            string url = $"/api/Tags/GetById/{savedTag.Id}";

            // Act
            var responseContent = await DeserializeResponse<ServiceResponse<TagResponseDto>>(await _HttpClient.GetAsync(url));

            // Assert
            AssertResponseSuccess(responseContent);
            Assert.True(responseContent.Content.Id == savedTag.Id);
        }

        [Fact]
        public async Task TestCreate()
        {
            // Arrange
            string url = $"/api/Tags/Create";
            TagRequestDto tagRequest = new TagRequestDto { Name = "TestCreateTag" };

            // Act
            var responseContent = await DeserializeResponse<ServiceResponse>(await _HttpClient.PostAsJsonAsync(url, tagRequest));

            // Assert
            AssertResponseSuccess(responseContent);
            Assert.True(await _context.Tags.AnyAsync(c => c.Name == "TestCreateTag"));
        }

        [Fact]
        public async Task TestEdit()
        {
            // Arrange
            var testTag = new Tag { Name = "UneditedTag" };
            await AddToDatabase(testTag);
            var uneditedTag = await FindInDatabaseByConditionsFirstOrDefault<Tag>(c => c.Name == "UneditedTag");
            string url = $"/api/Tags/Edit/{uneditedTag.Id}";
            TagRequestDto tagRequest = new TagRequestDto { Name = "TestEditTag" };

            // Act
            var responseContent = await DeserializeResponse<ServiceResponse>(await _HttpClient.PutAsJsonAsync(url, tagRequest));

            // Assert
            AssertResponseSuccess(responseContent);
            Assert.True(await _context.Tags.AnyAsync(c => c.Name == "TestEditTag"));
        }

        [Fact]
        public async Task TestDelete()
        {
            // Arrange
            var testTag = new Tag { Name = "TestDeleteTag" };
            var tagToDelete = await AddToDatabase(testTag);
            string url = $"/api/Tags/Delete/{tagToDelete.Id}";

            // Act
            var responseContent = await DeserializeResponse<ServiceResponse>(await _HttpClient.DeleteAsync(url));

            // Assert
            AssertResponseSuccess(responseContent);
            Assert.True(!await _context.Tags.AnyAsync(c => c.Id == tagToDelete.Id));
        }

        [Fact]
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

            string createUrl = $"/api/Tags/Create";

            // Act
            var responseContentCreateTagRequestMinName = await DeserializeResponse<ServiceResponse>
                (await _HttpClient.PostAsJsonAsync(createUrl, TagRequestMinName));

            var responseContentCreateTagRequestMaxName = await DeserializeResponse<ServiceResponse>
                (await _HttpClient.PostAsJsonAsync(createUrl, TagRequestMaxName));

            // Assert
            AssertResponseFailure(responseContentCreateTagRequestMinName);
            AssertResponseFailure(responseContentCreateTagRequestMaxName);
        }
    }
}
