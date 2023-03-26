namespace Smakoowa_Api.Services.Interfaces.ValidatorServices
{
    public interface IRecipeValidatorService
    {
        public Task<ServiceResponse> ValidateRecipeRequestDto(RecipeRequestDto recipeRequestDto);
    }
}