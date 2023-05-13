namespace Smakoowa_Api.Services
{
    public class TagGetterService : ITagGetterService
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITagMapperService _tagMapperService;
        private readonly IHelperService<TagGetterService> _helperService;
        private readonly ITagLikeService _tagLikeService;

        public TagGetterService(ITagLikeService tagLikeService, IHelperService<TagGetterService> helperService,
            ITagMapperService tagMapperService, ITagRepository tagRepository)
        {
            _tagLikeService = tagLikeService;
            _helperService = helperService;
            _tagMapperService = tagMapperService;
            _tagRepository = tagRepository;
        }

        public async Task<ServiceResponse> GetAll()
        {
            try
            {
                var tags = await _tagRepository.FindAll();

                var getTagsResponseDto = new List<TagResponseDto>();
                foreach (Tag tag in tags) getTagsResponseDto.Add(await _tagMapperService.MapGetTagResponseDto(tag));
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
                if (tag == null) return ServiceResponse.Error($"Tag with id: {tagId} not found.", HttpStatusCode.NotFound);

                var getTagResponseDto = await _tagMapperService.MapGetTagResponseDto(tag);
                return ServiceResponse<TagResponseDto>.Success(getTagResponseDto, "Tag retrieved.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while accessing the tag.");
            }
        }

        public async Task<ServiceResponse> GetByIds(List<int> tagIds)
        {
            return await GetTagsByConditions(t => tagIds.Contains(t.Id));
        }

        public async Task<ServiceResponse> GetByType(TagType tagType)
        {
            return await GetTagsByConditions(t => t.TagType == tagType);
        }

        public async Task<ServiceResponse> GetUserLikedTags()
        {
            var likedTagIds = (await _tagLikeService.GetUserTagLikes()).Select(tl => tl.LikedId);
            return await GetTagsByConditions(t => likedTagIds.Any(tl => tl == t.Id));
        }

        private async Task<ServiceResponse> GetTagsByConditions(Expression<Func<Tag, bool>> expresion)
        {
            try
            {
                var tags = await _tagRepository.FindByConditions(expresion);

                List<TagResponseDto> getTagResponseDtos = new();
                foreach (var tag in tags) getTagResponseDtos.Add(await _tagMapperService.MapGetTagResponseDto(tag));
                return ServiceResponse<List<TagResponseDto>>.Success(getTagResponseDtos, "Tags retrieved.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while accessing the tags.");
            }
        }
    }
}
