namespace Smakoowa_Api.Services.MapperServices
{
    public class InstructionMapperService : IInstructionMapperService
    {
        private readonly IMapper _mapper;
        public InstructionMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<Instruction> MapCreateInstructionRequestDtos(List<InstructionRequestDto> instructionRequestDtos, int recipeId)
        {
            List<Instruction> mappedInstructions = new();
            foreach (var instruction in instructionRequestDtos) mappedInstructions.Add(_mapper.Map<Instruction>(instruction));
            foreach (Instruction instruction in mappedInstructions) instruction.RecipeId = recipeId;
            return mappedInstructions;
        }
    }
}
