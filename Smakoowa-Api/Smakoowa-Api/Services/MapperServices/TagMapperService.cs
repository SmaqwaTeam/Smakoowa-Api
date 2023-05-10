namespace Smakoowa_Api.Services.MapperServices
{
    public class TagMapperService : ITagMapperService
    {
        private readonly IMapper _mapper;
        private readonly ITagLikeService _tagLikeService;

        public TagMapperService(IMapper mapper, ITagLikeService tagLikeService)
        {
            _mapper = mapper;
            _tagLikeService = tagLikeService;
        }

        public Tag MapCreateTagRequestDto(TagRequestDto createTagRequestDto)
        {
            return _mapper.Map<Tag>(createTagRequestDto);
        }

        public async Task<TagResponseDto> MapGetTagResponseDto(Tag tag)
        {
            var mappedTag = _mapper.Map<TagResponseDto>(tag);
            mappedTag.LikeCount = await _tagLikeService.GetTagLikeCount(tag.Id);
            return mappedTag;
        }

        public Tag MapEditTagRequestDto(TagRequestDto editTagRequestDto, Tag editedTag)
        {
            editedTag.Name = editTagRequestDto.Name;
            editedTag.TagType = editTagRequestDto.TagType;
            return editedTag;
        }
    }
}