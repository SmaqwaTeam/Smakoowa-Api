﻿namespace Smakoowa_Api.Services.Interfaces.MapperServices
{
    public interface ITagMapperService
    {
        public Tag MapCreateTagRequestDto(TagRequestDto tagRequestDto);
        public Task<TagResponseDto> MapGetTagResponseDto(Tag category);
        public Tag MapEditTagRequestDto(TagRequestDto tagRequestDto, Tag editedTag);
    }
}
