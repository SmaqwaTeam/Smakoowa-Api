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
            var nameValidationResponse = ValidateNameLength(tagRequestDto.Name, "Tag");
            if (!nameValidationResponse.SuccessStatus) return nameValidationResponse;
            
            if (await _tagRepository.CheckIfExists(t => t.Name == tagRequestDto.Name))
            {
                return ServiceResponse.Error($"A tag with name {tagRequestDto.Name} already exists.", HttpStatusCode.Conflict);
            }

            if (!Enum.IsDefined(typeof(TagType), tagRequestDto.TagType))
            {
                return ServiceResponse.Error($"Invalid tag type.", HttpStatusCode.BadRequest);
            }

            return ServiceResponse.Success();
        }
    }
}