namespace Smakoowa_Api.Models.Auditables
{
    public class Creatable
    {
        [Key]
        public int CreatorId { get; set; }
        public ApiUser Creator { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
