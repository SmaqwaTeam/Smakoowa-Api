namespace Smakoowa_Api.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITagMapperService _tagMapperService;
        private readonly ITagValidatorService _tagValidatorService;
        private readonly IHelperService<TagService> _helperService;
        private readonly ITagLikeService _tagLikeService;

        public TagService(ITagRepository tagRepository, ITagMapperService tagMapperService,
            ITagValidatorService tagValidatorService, IHelperService<TagService> helperService, ITagLikeService tagLikeService)
        {
            _tagRepository = tagRepository;
            _tagMapperService = tagMapperService;
            _tagValidatorService = tagValidatorService;
            _helperService = helperService;
            _tagLikeService = tagLikeService;
        }

        public async Task<ServiceResponse> Create(TagRequestDto tagRequestDto)
        {
            var validationResult = await _tagValidatorService.ValidateTagRequestDto(tagRequestDto);
            if (!validationResult.SuccessStatus) return ServiceResponse.Error(validationResult.Message);

            var tag = _tagMapperService.MapCreateTagRequestDto(tagRequestDto);

            try
            {
                await _tagRepository.Create(tag);
                return ServiceResponse.Success("Tag created.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while creating a tag.");
            }
        }

        public async Task<ServiceResponse> Delete(int tagId)
        {
            var tag = await _tagRepository.FindByConditionsFirstOrDefault(t => t.Id == tagId);
            if (tag == null) return ServiceResponse.Error($"Tag with id: {tagId} not found.");

            try
            {
                await _tagRepository.Delete(tag);
                return ServiceResponse.Success("Tag deleted.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while deleting a tag.");
            }
        }

        public async Task<ServiceResponse> Edit(TagRequestDto tagRequestDto, int tagId)
        {
            var tag = await _tagRepository.FindByConditionsFirstOrDefault(t => t.Id == tagId);
            if (tag == null) return ServiceResponse.Error($"Tag with id: {tagId} not found.");

            var validationResult = await _tagValidatorService.ValidateTagRequestDto(tagRequestDto);
            if (!validationResult.SuccessStatus) return ServiceResponse.Error(validationResult.Message);

            var updatedTag = _tagMapperService.MapEditTagRequestDto(tagRequestDto, tag);

            try
            {
                await _tagRepository.Edit(updatedTag);
                return ServiceResponse.Success("Tag edited.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while editing a tag.");
            }
        }

        public async Task<ServiceResponse> GetAll()
        {
            try
            {
                var tags = await _tagRepository.FindAll();

                var getTagsResponseDto = new List<TagResponseDto>();
                foreach (Tag tag in tags) getTagsResponseDto.Add(_tagMapperService.MapGetTagResponseDto(tag));
                return ServiceResponse<List<TagResponseDto>>.Success(getTagsResponseDto, "Tags retrieved.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while accessing the tags.");
            }
        }

        public async Task<ServiceResponse> GetById(int tagId)
        {
            try
            {
                var tag = await _tagRepository.FindByConditionsFirstOrDefault(t => t.Id == tagId);
                if (tag == null) return ServiceResponse.Error($"Tag with id: {tagId} not found.");

                var getTagResponseDto = _tagMapperService.MapGetTagResponseDto(tag);
                return ServiceResponse<TagResponseDto>.Success(getTagResponseDto, "Tag retrieved.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while accessing the tag.");
            }
        }

        public async Task<ServiceResponse> GetByIds(List<int> tagIds)
        {
            return await GetByConditions(t => tagIds.Contains(t.Id));
        }

        public async Task<ServiceResponse> GetByType(TagType tagType)
        {
            return await GetByConditions(t => t.TagType == tagType);
        }

        public async Task<ServiceResponse> GetUserLikedTags()
        {
            var likedTagIds = (await _tagLikeService.GetUserTagLikes()).Select(tl => tl.TagId);
            return await GetByConditions(t => likedTagIds.Any(tl => tl == t.Id));
        }

        private async Task<ServiceResponse> GetByConditions(Expression<Func<Tag, bool>> expresion)
        {
            try
            {
                var tags = await _tagRepository.FindByConditions(expresion);

                List<TagResponseDto> getTagResponseDtos = new();
                foreach (var tag in tags) getTagResponseDtos.Add(_tagMapperService.MapGetTagResponseDto(tag));
                return ServiceResponse<List<TagResponseDto>>.Success(getTagResponseDtos, "Tags retrieved.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while accessing the tags.");
            }
        }
    }
}