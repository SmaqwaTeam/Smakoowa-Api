using Xunit;

namespace Smakoowa_Api.Tests.IntegrationTests
{
    [Trait("Configuration", "Integration")]
    public class ConfigurationTests : ControllerIntegrationTests
    {
        public ConfigurationTests(CustomWebApplicationFactory<Program> fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task TestConfiguration()
        {
            // Arrange
            int stash;

            string validationSectionName = "Validation", fileUploadSectionName = "FileUpload", nameMaxLength = "MaxNameLength",
                nameMinLength = "MinNameLength", commentContentMaxLength = "MaxCommentContentLength",
                commentContentMinLength = "MinCommentContentLength";

            List<string> nameLengthValidationClassesNames = new List<string> { "Category", "Tag", "Recipe", "Ingredient" };
            List<string> commentContentLengthValidationClassesNames = new List<string> { "RecipeComment", "CommentReply" };

            // Assert
            foreach (string name in nameLengthValidationClassesNames)
            {
                Assert.True(int.TryParse(_configuration.GetSection($"{validationSectionName}:{name}:{nameMaxLength}").Value, out stash));
                Assert.True(int.TryParse(_configuration.GetSection($"{validationSectionName}:{name}:{nameMinLength}").Value, out stash));
            }

            foreach (string name in commentContentLengthValidationClassesNames)
            {
                Assert.True(int.TryParse(_configuration.GetSection($"{validationSectionName}:{name}:{commentContentMaxLength}").Value, out stash));
                Assert.True(int.TryParse(_configuration.GetSection($"{validationSectionName}:{name}:{commentContentMinLength}").Value, out stash));
            }

            Assert.True(int.TryParse(_configuration.GetSection($"{validationSectionName}:Recipe:MaxDescriptionLength").Value, out stash));
            Assert.True(int.TryParse(_configuration.GetSection($"{validationSectionName}:Instruction:MaxContentLength").Value, out stash));
            Assert.True(int.TryParse(_configuration.GetSection($"{validationSectionName}:Image:MaxImageSizeBytes").Value, out stash));
            Assert.True(_configuration.GetSection($"{validationSectionName}:Image:AllowedImageExtensions").Value is not null);
            Assert.True(_configuration.GetSection($"{fileUploadSectionName}:Images:RecipeImageUploadPath").Value is not null);
            Assert.True(_configuration.GetSection($"{fileUploadSectionName}:Images:SavedImageExtension").Value is not null);
        }
    }
}