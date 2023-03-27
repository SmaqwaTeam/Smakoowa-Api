namespace Smakoowa_Api.Mappings
{
    public class InstructionMapperProfile : Profile
    {
        public InstructionMapperProfile()
        {
            CreateMap<InstructionRequestDto, Instruction>();
            CreateMap<Instruction, InstructionResponseDto>();
        }
    }
}
