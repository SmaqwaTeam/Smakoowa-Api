﻿namespace Smakoowa_Api.Services.ValidatorServices
{
    public class IngredientValidatorService : BaseValidatorService, IIngredientValidatorService
    {
        public IngredientValidatorService(IConfiguration configuration) : base(configuration, "Validation:Ingredient")
        {
        }

        public async Task<ServiceResponse> ValidateIngredientRequestDtos(List<IngredientRequestDto> ingredientRequestDtos)
        {
            foreach (var ingredientRequestDto in ingredientRequestDtos)
            {
                var validationResponse = ValidateNameLength(ingredientRequestDto.Name, "Ingredient");

                if (!validationResponse.SuccessStatus)
                {
                    return validationResponse;
                }

                if (ingredientRequestDto.Position < 1)
                {
                    return ServiceResponse.Error("Ingredient position needs to be of non-zero value.", HttpStatusCode.BadRequest);
                }

                if (ingredientRequestDto.Group < 1)
                {
                    return ServiceResponse.Error("Ingredient group needs to be of non-zero value.", HttpStatusCode.BadRequest);
                }
            }

            return ServiceResponse.Success();
        }
    }
}