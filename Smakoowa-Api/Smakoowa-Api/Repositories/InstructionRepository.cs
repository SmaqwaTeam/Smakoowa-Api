namespace Smakoowa_Api.Repositories
{
    public class InstructionRepository : BaseRepository<Instruction>, IInstructionRepository
    {
        public InstructionRepository(DataContext context) : base(context) { }
    }
}
