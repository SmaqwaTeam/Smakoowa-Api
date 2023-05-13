namespace Smakoowa_Api.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITagMapperService _tagMapperService;
        private readonly ITagValidatorService _tagValidatorService;
        private readonly IHelperService<TagService> _helperService;

        public TagService(ITagRepository tagRepository, ITagMapperService tagMapperService,
            ITagValidatorService tagValidatorService, IHelperService<TagService> helperService)
        {
            _tagRepository = tagRepository;
            _tagMapperService = tagMapperService;
            _tagValidatorService = tagValidatorService;
            _helperService = helperService;
        }

        public async Task<ServiceResponse> Create(TagRequestDto tagRequestDto)
        {
            var validationResult = await _tagValidatorService.ValidateTagRequestDto(tagRequestDto);
            if (!validationResult.SuccessStatus)
            {
                return validationResult;
            }

            var mappedNewTag = _tagMapperService.MapCreateTagRequestDto(tagRequestDto);

            try
            {
                await _tagRepository.Create(mappedNewTag);
                return ServiceResponse.Success("Tag created.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while creating a tag.");
            }
        }

        public async Task<ServiceResponse> Delete(int tagId)
        {
            var tagToDelete = await _tagRepository.FindByConditionsFirstOrDefault(t => t.Id == tagId);
            if (tagToDelete == null)
            {
                return ServiceResponse.Error($"Tag with id: {tagId} not found.", HttpStatusCode.NotFound);
            }

            try
            {
                await _tagRepository.Delete(tagToDelete);
                return ServiceResponse.Success("Tag deleted.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while deleting a tag.");
            }
        }

        public async Task<ServiceResponse> Edit(TagRequestDto tagRequestDto, int tagId)
        {
            var tagToEdit = await _tagRepository.FindByConditionsFirstOrDefault(t => t.Id == tagId);
            if (tagToEdit == null)
            {
                return ServiceResponse.Error($"Tag with id: {tagId} not found.", HttpStatusCode.NotFound);
            }

            var validationResult = await _tagValidatorService.ValidateTagRequestDto(tagRequestDto);
            if (!validationResult.SuccessStatus)
            {
                return validationResult;
            }

            var mappedTagToEdit = _tagMapperService.MapEditTagRequestDto(tagRequestDto, tagToEdit);

            try
            {
                await _tagRepository.Edit(mappedTagToEdit);
                return ServiceResponse.Success("Tag edited.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while editing a tag.");
            }
        }
    }
}