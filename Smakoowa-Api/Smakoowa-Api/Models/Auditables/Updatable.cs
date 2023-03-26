namespace Smakoowa_Api.Models.Auditables
{
    public class Updatable : Creatable
    {
        [Key]
        public int? UpdaterId { get; set; }
        public virtual ApiUser? Updater { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
