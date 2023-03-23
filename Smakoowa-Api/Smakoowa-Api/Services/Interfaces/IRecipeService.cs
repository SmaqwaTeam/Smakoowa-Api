using Smakoowa_Api.Services.Interfaces.Base;

namespace Smakoowa_Api.Services.Interfaces
{
    public interface IRecipeService : ICreatorService, IUpdaterService, IDeleterService, IGetterService
    {
    }
}
