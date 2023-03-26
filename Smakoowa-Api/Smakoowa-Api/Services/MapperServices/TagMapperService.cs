namespace Smakoowa_Api.Services.MapperServices
{
    public class TagMapperService : ITagMapperService
    {
        private readonly IMapper _mapper;

        public TagMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Tag MapCreateTagRequestDto(CreateTagRequestDto createTagRequestDto)
        {
            return _mapper.Map<Tag>(createTagRequestDto);
        }

        public GetTagResponseDto MapGetTagResponseDto(Tag category)
        {
            return _mapper.Map<GetTagResponseDto>(category);
        }

        public Tag MapEditTagRequestDto(EditTagRequestDto editTagRequestDto, Tag editedTag)
        {
            editedTag.Name = editTagRequestDto.Name;
            editedTag.TagType = editTagRequestDto.TagType;
            return editedTag;
        }
    }
}