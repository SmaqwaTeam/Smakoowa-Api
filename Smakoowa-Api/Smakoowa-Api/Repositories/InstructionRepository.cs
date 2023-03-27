namespace Smakoowa_Api.Repositories
{
    public class InstructionRepository : BaseRepository<Instruction>, IInstructionRepository
    {
        public InstructionRepository(DataContext context) : base(context) { }

        public override async Task<IEnumerable<Instruction>> FindAll()
        {
            return await _context.Set<Instruction>()
            .Include(i => i.Recipe)
            .ToListAsync();
        }

        public override async Task<IEnumerable<Instruction>> FindByConditions(Expression<Func<Instruction, bool>> expression)
        {
            return await _context.Set<Instruction>()
            .Where(expression)
            .Include(i => i.Recipe)
            .ToListAsync();
        }
    }
}
