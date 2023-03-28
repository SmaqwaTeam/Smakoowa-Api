namespace Smakoowa_Api.Models.DatabaseModels.Comments
{
    public abstract class Comment : Updatable, IDbModel, ILikeable
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}
