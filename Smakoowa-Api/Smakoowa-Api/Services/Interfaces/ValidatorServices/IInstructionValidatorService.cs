namespace Smakoowa_Api.Services.Interfaces.ValidatorServices
{
    public interface IInstructionValidatorService
    {
        public Task<ServiceResponse> ValidateInstructionRequestDtos(List<InstructionRequestDto> instructionRequestDtos);
    }
}
