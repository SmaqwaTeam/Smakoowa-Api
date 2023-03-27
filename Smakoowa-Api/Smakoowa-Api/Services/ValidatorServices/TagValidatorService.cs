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

        public async Task<ServiceResponse> ValidateTagRequestDto(TagRequestDto tagRequestDto)
        {
            var validationResponse = ValidateNameLength(tagRequestDto.Name, "Tag");

            if (!validationResponse.SuccessStatus)
            {
                return validationResponse;
            }

            if (await _tagRepository.CheckIfExists(t => t.Name == tagRequestDto.Name))
            {
                return ServiceResponse.Error($"A tag with name {tagRequestDto.Name} already exists.");
            }

            if (!Enum.IsDefined(typeof(TagType), tagRequestDto.TagType))
            {
                return ServiceResponse.Error($"Invalid tag type.");
            }

            return ServiceResponse.Success();
        }
    }
}