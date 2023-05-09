namespace Smakoowa_Api.Models.ResponseDtos
{
    public class ApiUserResponseDto : IResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
