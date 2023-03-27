using Smakoowa_Api.Models.RequestDtos;

namespace Smakoowa_Api.Services.Interfaces
{
    public interface IRecipeService : ICreatorService<RecipeRequestDto>, IEditorService<RecipeRequestDto>, IDeleterService, IGetterService
    {
        public Task<ServiceResponse> GetByIdDetailed(int recipeId);
    }
}
