namespace Smakoowa_Api.Services.ValidatorServices
{
    public class InstructionValidatorService : IInstructionValidatorService
    {
        private readonly int MaxContentLength;

        public InstructionValidatorService(IConfiguration configuration)
        {
            MaxContentLength = int.Parse(configuration.GetSection("Validation:Instruction:MaxContentLength").Value);
        }

        public async Task<ServiceResponse> ValidateInstructionRequestDtos(List<InstructionRequestDto> instructionRequestDtos)
        {
            foreach (var instructionRequestDto in instructionRequestDtos)
            {
                if (instructionRequestDto.Content.Length > MaxContentLength)
                {
                    return ServiceResponse.Error($"Description must be max {MaxContentLength} characters.");
                }

                if (instructionRequestDto.Position < 1)
                {
                    return ServiceResponse.Error("Instruction position needs to be of non-zero value.");
                }
            }

            return ServiceResponse.Success();
        }
    }
}