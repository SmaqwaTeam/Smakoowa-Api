namespace Smakoowa_Api.Services.Interfaces.ValidatorServices
{
    public interface IIngredientValidatorService
    {
        public Task<ServiceResponse> ValidateIngredientRequestDtos(List<IngredientRequestDto> ingredientRequestDtos);
    }
}
