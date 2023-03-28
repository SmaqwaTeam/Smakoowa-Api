namespace Smakoowa_Api.Services
{
    public class RecipeCommentService : IRecipeCommentService
    {
        private readonly IRecipeCommentRepository _recipeCommentRepository;
        private readonly IRecipeCommentMapperService _recipeCommentMapperService;
        private readonly IRecipeCommentValidatorService _recipeCommentValidatorService;
        private readonly IHelperService<RecipeCommentService> _helperService;

        public RecipeCommentService(IRecipeCommentValidatorService recipeCommentValidatorService, IRecipeCommentMapperService recipeCommentMapperService,
            IRecipeCommentRepository recipeCommentRepository, IHelperService<RecipeCommentService> helperService)
        {
            _recipeCommentValidatorService = recipeCommentValidatorService;
            _recipeCommentMapperService = recipeCommentMapperService;
            _recipeCommentRepository = recipeCommentRepository;
            _helperService = helperService;
        }

        public async Task<ServiceResponse> AddRecipeComment(RecipeCommentRequestDto recipeCommentRequestDto, int recipeId)
        {
            var recipeCommentValidationResult = await _recipeCommentValidatorService.ValidateRecipeCommentRequestDto(recipeCommentRequestDto, recipeId);
            if (!recipeCommentValidationResult.SuccessStatus) return ServiceResponse.Error(recipeCommentValidationResult.Message);

            var mappedRecipeComment = _recipeCommentMapperService.MapCreateRecipeCommentRequestDto(recipeCommentRequestDto, recipeId);

            try
            {
                await _recipeCommentRepository.Create(mappedRecipeComment);
                return ServiceResponse.Success("Recipe comment created.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while creating a recipe comment.");
            }
        }

        public async Task<ServiceResponse> DeleteRecipeComment(int recipeCommentId)
        {
            var recipeComment = await _recipeCommentRepository.FindByConditionsFirstOrDefault(rc => rc.Id == recipeCommentId);
            if (recipeComment == null) return ServiceResponse.Error($"Recipe comment with id: {recipeCommentId} not found.");

            try
            {
                await _recipeCommentRepository.Delete(recipeComment);
                return ServiceResponse.Success("Recipe comment deleted.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while deleting a recipe comment.");
            }
        }

        public async Task<ServiceResponse> EditRecipeComment(RecipeCommentRequestDto recipeCommentRequestDto, int recipeCommentId)
        {
            var editedRecipeComment = await _recipeCommentRepository.FindByConditionsFirstOrDefault(rc => rc.Id == recipeCommentId);
            if (editedRecipeComment == null) return ServiceResponse.Error($"RecipeComment with id: {recipeCommentId} not found.");

            var validationResult = await _recipeCommentValidatorService.ValidateRecipeCommentRequestDto(recipeCommentRequestDto, editedRecipeComment.RecipeId);
            if (!validationResult.SuccessStatus) return ServiceResponse.Error(validationResult.Message);

            var updatedRecipeComment = _recipeCommentMapperService.MapEditRecipeCommentRequestDto(recipeCommentRequestDto, editedRecipeComment);

            try
            {
                await _recipeCommentRepository.Edit(updatedRecipeComment);
                return ServiceResponse.Success("Recipe comment edited.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while editing a recipe comment.");
            }
        }
    }
}
