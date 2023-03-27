using Smakoowa_Api.Models.RequestDtos;

namespace Smakoowa_Api.Services.MapperServices
{
    public class TagMapperService : ITagMapperService
    {
        private readonly IMapper _mapper;

        public TagMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Tag MapCreateTagRequestDto(TagRequestDto createTagRequestDto)
        {
            return _mapper.Map<Tag>(createTagRequestDto);
        }

        public TagResponseDto MapGetTagResponseDto(Tag category)
        {
            return _mapper.Map<TagResponseDto>(category);
        }

        public Tag MapEditTagRequestDto(TagRequestDto editTagRequestDto, Tag editedTag)
        {
            editedTag.Name = editTagRequestDto.Name;
            editedTag.TagType = editTagRequestDto.TagType;
            return editedTag;
        }
    }
}