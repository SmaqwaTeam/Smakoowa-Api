namespace Smakoowa_Api.Services.Interfaces.ValidatorServices
{
    public interface ILikeValidatorService
    {
        public Task<ServiceResponse> ValidateAddLike(int likedId);
    }
}
