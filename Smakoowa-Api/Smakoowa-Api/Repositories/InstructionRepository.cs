using ModernPantryBackend.Repositories;
using Smakoowa_Api.Repositories.Interfaces;

namespace Smakoowa_Api.Repositories
{
    public class InstructionRepository : BaseRepository<Instruction>, ICategoryRepository
    {
        public InstructionRepository(DataContext context) : base(context) { }
    }
}
