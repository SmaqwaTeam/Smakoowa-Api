namespace Smakoowa_Api.Models.ResponseDtos.Auditables
{
    public class CreatableResponseDto : IResponseDto
    {
        public int? CreatorId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
