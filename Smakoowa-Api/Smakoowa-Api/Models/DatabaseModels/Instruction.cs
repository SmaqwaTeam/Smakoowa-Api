namespace Smakoowa_Api.Models.DatabaseModels
{
    public class Instruction : IDbModel, IStringContent
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Position { get; set; }
        public string? ImageUrl { get; set; }

        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
