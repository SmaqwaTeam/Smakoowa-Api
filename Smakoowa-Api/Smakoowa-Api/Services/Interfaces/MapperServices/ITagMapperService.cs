namespace Smakoowa_Api.Services.Interfaces.MapperServices
{
    public interface ITagMapperService
    {
        public Tag MapCreateTagRequestDto(CreateTagRequestDto createTagRequestDto);
        public GetTagResponseDto MapGetTagResponseDto(Tag category);
        public Tag MapEditTagRequestDto(EditTagRequestDto editTagRequestDto, Tag editedTag);
    }
}
