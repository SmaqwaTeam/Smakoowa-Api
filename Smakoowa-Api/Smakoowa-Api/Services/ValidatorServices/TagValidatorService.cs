using Smakoowa_Api.Models.RequestDtos.Tag;
using System;

namespace Smakoowa_Api.Services.ValidatorServices
{
    public class TagValidatorService : BaseValidatorService, ITagValidatorService
    {
        private readonly ITagRepository _tagRepository;

        public TagValidatorService(IConfiguration configuration, ITagRepository tagRepository)
            : base(configuration, "Validation:Tag")
        {
            _tagRepository = tagRepository;
        }

        public async Task<ServiceResponse> ValidateCreateTagRequestDto(CreateTagRequestDto createTagRequestDto)
        {
            var validationResponse = ValidateNameLength(createTagRequestDto.Name, "Tag");

            if (!validationResponse.SuccessStatus)
            {
                return validationResponse;
            }

            if (await _tagRepository.CheckIfExists(t => t.Name == createTagRequestDto.Name))
            {
                return ServiceResponse.Error($"A tag with name {createTagRequestDto.Name} already exists.");
            }

            if (!Enum.IsDefined(typeof(TagType), createTagRequestDto.TagType))
            {
                return ServiceResponse.Error($"Invalid tag type.");
            }

            return ServiceResponse.Success();
        }

        public async Task<ServiceResponse> ValidateEditTagRequestDto(EditTagRequestDto editTagRequestDto)
        {
            var validationResponse = ValidateNameLength(editTagRequestDto.Name, "Tag");

            if (!validationResponse.SuccessStatus)
            {
                return validationResponse;
            }

            if (await _tagRepository.CheckIfExists(t => t.Name == editTagRequestDto.Name))
            {
                return ServiceResponse.Error($"A tag with name {editTagRequestDto.Name} already exists.");
            }

            if (!Enum.IsDefined(typeof(TagType), editTagRequestDto.TagType))
            {
                return ServiceResponse.Error($"Invalid tag type.");
            }

            return ServiceResponse.Success();
        }
    }
}