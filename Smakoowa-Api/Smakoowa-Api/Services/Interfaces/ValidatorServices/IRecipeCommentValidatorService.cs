using Smakoowa_Api.Models.DatabaseModels;
using Smakoowa_Api.Models.RequestDtos;

namespace Smakoowa_Api.Services.Interfaces.ValidatorServices
{
    public interface IRecipeCommentValidatorService
    {
        public Task<ServiceResponse> ValidateRecipeCommentRequestDto(RecipeCommentRequestDto recipeCommentRequestDto, int recipeId);
    }
}
