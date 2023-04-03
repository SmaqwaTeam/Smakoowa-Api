namespace Smakoowa_Api.Models.Identity
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public ApiUserResponseDto User { get; set; }
    }
}
