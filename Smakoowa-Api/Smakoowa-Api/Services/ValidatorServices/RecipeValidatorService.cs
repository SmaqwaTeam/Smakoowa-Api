namespace Smakoowa_Api.Services.ValidatorServices
{
    public class RecipeValidatorService : BaseValidatorService, IRecipeValidatorService
    {
        private readonly ITagRepository _tagRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IIngredientValidatorService _ingredientValidatorService;
        private readonly IInstructionValidatorService _instructionValidatorService;
        private readonly int _maxDescriptionLength;

        public RecipeValidatorService(IConfiguration configuration, ITagRepository tagRepository, ICategoryRepository categoryRepository,
            IIngredientValidatorService ingredientValidatorService, IInstructionValidatorService instructionValidatorService)
            : base(configuration, "Validation:Recipe")
        {
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
            _maxDescriptionLength = int.Parse(configuration.GetSection("Validation:Recipe:MaxDescriptionLength").Value);
            _ingredientValidatorService = ingredientValidatorService;
            _instructionValidatorService = instructionValidatorService;
        }

        public async Task<ServiceResponse> ValidateRecipeRequestDto(RecipeRequestDto recipeRequestDto)
        {
            var nameValidationResponse = ValidateNameLength(recipeRequestDto.Name, "Recipe");
            if (!nameValidationResponse.SuccessStatus) return nameValidationResponse;

            if (recipeRequestDto.Description?.Length > _maxDescriptionLength)
            {
                return ServiceResponse.Error($"Description must be max {_maxDescriptionLength} characters.", HttpStatusCode.BadRequest);
            }

            if (recipeRequestDto.TagIds is not null
                && (await _tagRepository.FindByConditions(
                    t => recipeRequestDto.TagIds.Contains(t.Id))).Count() != recipeRequestDto.TagIds.Count())
            {
                return ServiceResponse.Error("One or more of the specified tag ids are invalid.", HttpStatusCode.BadRequest);
            }

            if (!await _categoryRepository.CheckIfExists(c => c.Id == recipeRequestDto.CategoryId))
            {
                return ServiceResponse.Error($"Category with id: {recipeRequestDto.CategoryId} does not exist.", HttpStatusCode.NotFound);
            }

            if (!Enum.IsDefined(typeof(TimeToMakeTier), recipeRequestDto.TimeToMakeTier))
            {
                return ServiceResponse.Error($"Invalid time to make tier.", HttpStatusCode.BadRequest);
            }

            if (!Enum.IsDefined(typeof(ServingsTier), recipeRequestDto.ServingsTier))
            {
                return ServiceResponse.Error($"Invalid servings tier.", HttpStatusCode.BadRequest);
            }

            if (recipeRequestDto.Ingredients == null || recipeRequestDto.Ingredients.Count < 1)
            {
                return ServiceResponse.Error("A recipe must have at least one ingredient.", HttpStatusCode.BadRequest);
            }

            if (recipeRequestDto.Instructions == null || recipeRequestDto.Instructions.Count < 1)
            {
                return ServiceResponse.Error("A recipe must have at least one instruction.", HttpStatusCode.BadRequest);
            }

            var ingredientValidationResult = await _ingredientValidatorService.ValidateIngredientRequestDtos(recipeRequestDto.Ingredients);
            if (!ingredientValidationResult.SuccessStatus) return ingredientValidationResult;

            var instructionValidationResult = await _instructionValidatorService.ValidateInstructionRequestDtos(recipeRequestDto.Instructions);
            if (!instructionValidationResult.SuccessStatus) return instructionValidationResult;

            return ServiceResponse.Success();
        }
    }
}