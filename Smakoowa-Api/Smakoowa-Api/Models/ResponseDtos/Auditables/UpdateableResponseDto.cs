namespace Smakoowa_Api.Models.ResponseDtos.Auditables
{
    public class UpdateableResponseDto : CreatableResponseDto
    {
        public int? UpdaterId { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
