namespace Smakoowa_Api.Models.DatabaseModels
{
    public class Ingredient : IDbModel, INameable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public int Group { get; set; }

        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
