using Smakoowa_Api.Repositories.Interfaces.Base;

namespace Smakoowa_Api.Repositories.Interfaces
{
    public interface ICategoryRepository : ICreatorRepository<Category>, IEditorRepository<Category>,
        IDeleterRepository<Category>, IGetterRepository<Category>
    {
    }
}
