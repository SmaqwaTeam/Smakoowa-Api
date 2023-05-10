namespace Smakoowa_Api.Services.ValidatorServices
{
    public class RecipeValidatorService : BaseValidatorService, IRecipeValidatorService
    {
        private readonly ITagRepository _tagRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IIngredientValidatorService _ingredientValidatorService;
        private readonly IInstructionValidatorService _instructionValidatorService;
        private readonly int MaxDescriptionLength;

        public RecipeValidatorService(IConfiguration configuration, ITagRepository tagRepository, ICategoryRepository categoryRepository, 
            IIngredientValidatorService ingredientValidatorService, IInstructionValidatorService instructionValidatorService) 
            : base(configuration, "Validation:Recipe")
        {
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
            MaxDescriptionLength = int.Parse(configuration.GetSection("Validation:Recipe:MaxDescriptionLength").Value);
            _ingredientValidatorService = ingredientValidatorService;
            _instructionValidatorService = instructionValidatorService;
        }

        public async Task<ServiceResponse> ValidateRecipeRequestDto(RecipeRequestDto recipeRequestDto)
        {
            var validationResponse = ValidateNameLength(recipeRequestDto.Name, "Recipe");

            if (!validationResponse.SuccessStatus)
            {
                return validationResponse;
            }

            if (recipeRequestDto.Description?.Length > MaxDescriptionLength)
            {
                return ServiceResponse.Error($"Description must be max {MaxDescriptionLength} characters.");
            }

            if (recipeRequestDto.TagIds is not null
                && (await _tagRepository.FindByConditions(
                    t => recipeRequestDto.TagIds.Contains(t.Id))).Count() != recipeRequestDto.TagIds.Count())
            {
                return ServiceResponse.Error("One or more of the specified tag ids are invalid.");
            }

            if (!await _categoryRepository.CheckIfExists(c => c.Id == recipeRequestDto.CategoryId))
            {
                return ServiceResponse.Error($"Category with id: {recipeRequestDto.CategoryId} does not exist.");
            }

            if (!Enum.IsDefined(typeof(TimeToMakeTier), recipeRequestDto.TimeToMakeTier))
            {
                return ServiceResponse.Error($"Invalid time to make tier.");
            }

            if (!Enum.IsDefined(typeof(ServingsTier), recipeRequestDto.ServingsTier))
            {
                return ServiceResponse.Error($"Invalid servings tier.");
            }

            if (recipeRequestDto.Ingredients == null || recipeRequestDto.Ingredients.Count < 1)
            {
                return ServiceResponse.Error("A recipe must have at least one ingredient.");
            }

            if (recipeRequestDto.Instructions == null || recipeRequestDto.Instructions.Count < 1)
            {
                return ServiceResponse.Error("A recipe must have at least one instruction.");
            }

            var ingredientValidationResult = await _ingredientValidatorService.ValidateIngredientRequestDtos(recipeRequestDto.Ingredients);
            if (!ingredientValidationResult.SuccessStatus) return ServiceResponse.Error(ingredientValidationResult.Message);

            var instructionValidationResult = await _instructionValidatorService.ValidateInstructionRequestDtos(recipeRequestDto.Instructions);
            if (!instructionValidationResult.SuccessStatus) return ServiceResponse.Error(instructionValidationResult.Message);

            return ServiceResponse.Success();
        }
    }
}