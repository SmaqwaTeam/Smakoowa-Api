namespace Smakoowa_Api.Services
{
    public class RecipeCommentService : CommentService, IRecipeCommentService
    {
        private readonly IRecipeCommentRepository _recipeCommentRepository;
        private readonly IRecipeCommentMapperService _recipeCommentMapperService;
        private readonly IRecipeCommentValidatorService _recipeCommentValidatorService;

        public RecipeCommentService(IHelperService<CommentService> helperService, ICommentRepository commentRepository,
            IRecipeCommentValidatorService recipeCommentValidatorService, IRecipeCommentMapperService recipeCommentMapperService,
            IRecipeCommentRepository recipeCommentRepository)
            : base(helperService, commentRepository)
        {
            _recipeCommentValidatorService = recipeCommentValidatorService;
            _recipeCommentMapperService = recipeCommentMapperService;
            _recipeCommentRepository = recipeCommentRepository;
        }

        public async Task<ServiceResponse> AddRecipeComment(RecipeCommentRequestDto recipeCommentRequestDto, int recipeId)
        {
            var recipeCommentValidationResult = await _recipeCommentValidatorService.
                ValidateCreateRecipeCommentRequestDto(recipeCommentRequestDto, recipeId);

            if (!recipeCommentValidationResult.SuccessStatus) return ServiceResponse.Error(recipeCommentValidationResult.Message);

            var mappedRecipeComment = _recipeCommentMapperService.MapCreateRecipeCommentRequestDto(recipeCommentRequestDto, recipeId);
            return await AddComment(mappedRecipeComment);
        }

        public async Task<ServiceResponse> EditRecipeComment(RecipeCommentRequestDto recipeCommentRequestDto, int recipeCommentId)
        {
            var editedRecipeComment = await _recipeCommentRepository.FindByConditionsFirstOrDefault(rc => rc.Id == recipeCommentId);
            if (editedRecipeComment == null) return ServiceResponse.Error($"Recipe comment with id: {recipeCommentId} not found.");

            var recipeCommentValidationResult = await _recipeCommentValidatorService
                .ValidateEditRecipeCommentRequestDto(recipeCommentRequestDto, editedRecipeComment);

            if (!recipeCommentValidationResult.SuccessStatus) return ServiceResponse.Error(recipeCommentValidationResult.Message);

            var mappedRecipeComment = _recipeCommentMapperService.MapEditRecipeCommentRequestDto(recipeCommentRequestDto, editedRecipeComment);

            return await EditComment(mappedRecipeComment);
        }

        public async Task<ServiceResponse> DeleteRecipeComment(int recipeCommentId)
        {
            var recipeCommentToDelete = await _recipeCommentRepository.FindByConditionsFirstOrDefault(c => c.Id == recipeCommentId);
            if (recipeCommentToDelete == null) return ServiceResponse.Error($"Recipe comment with id: {recipeCommentId} not found.");

            try
            {
                await _recipeCommentRepository.Delete(recipeCommentToDelete);
                return ServiceResponse.Success("Comment deleted.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while deleting a comment.");
            }
        }
    }
}
