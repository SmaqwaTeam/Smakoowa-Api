using Smakoowa_Api.Models.Services;
using Smakoowa_Api.Services.Interfaces;

namespace Smakoowa_Api.Services
{
    public class RecipeCommentService : IRecipeCommentService
    {
        public Task<ServiceResponse> Create(IRequestDto model)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> Edit(IRequestDto model)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
