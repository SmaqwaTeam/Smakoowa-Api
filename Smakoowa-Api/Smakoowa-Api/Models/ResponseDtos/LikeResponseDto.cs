namespace Smakoowa_Api.Models.ResponseDtos
{
    public class LikeResponseDto : IResponseDto
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
