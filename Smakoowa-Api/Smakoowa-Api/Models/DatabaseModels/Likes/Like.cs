using Smakoowa_Api.Models.Auditables;

namespace Smakoowa_Api.Models.DatabaseModels.Likes
{
    public abstract class Like : Creatable, IDbModel
    {
        public int Id { get; set; }
        public LikeableType LikeableType { get; set; }
    }
}
