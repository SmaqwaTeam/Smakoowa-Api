namespace Smakoowa_Api.Services.Interfaces.MapperServices
{
    public interface IInstructionMapperService
    {
        public List<Instruction> MapCreateInstructionRequestDtos(List<InstructionRequestDto> instructionRequestDtos, int recipeId);
    }
}
