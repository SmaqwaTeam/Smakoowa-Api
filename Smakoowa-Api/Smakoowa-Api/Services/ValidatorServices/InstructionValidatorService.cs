namespace Smakoowa_Api.Services.ValidatorServices
{
    public class InstructionValidatorService : IInstructionValidatorService
    {
        private readonly int _maxContentLength;

        public InstructionValidatorService(IConfiguration configuration)
        {
            _maxContentLength = int.Parse(configuration.GetSection("Validation:Instruction:MaxContentLength").Value);
        }

        public async Task<ServiceResponse> ValidateInstructionRequestDtos(List<InstructionRequestDto> instructionRequestDtos)
        {
            foreach (var instructionRequestDto in instructionRequestDtos)
            {
                if (instructionRequestDto.Content.Length > _maxContentLength)
                {
                    return ServiceResponse.Error($"Description must be max {_maxContentLength} characters.", HttpStatusCode.BadRequest);
                }

                if (instructionRequestDto.Position < 1)
                {
                    return ServiceResponse.Error("Instruction position needs to be of non-zero value.", HttpStatusCode.BadRequest);
                }
            }

            return ServiceResponse.Success();
        }
    }
}