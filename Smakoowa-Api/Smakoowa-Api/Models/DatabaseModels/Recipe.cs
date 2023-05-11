namespace Smakoowa_Api.Models.DatabaseModels
{
    public class Recipe : Updatable, IDbModel, ILikeable, INameable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public string? ImageId { get; set; }
        public ServingsTier ServingsTier { get; set; }
        public TimeToMakeTier TimeToMakeTier { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public virtual List<Instruction> Instructions { get; set; }
        public virtual List<Ingredient> Ingredients { get; set; }
        public virtual List<RecipeComment>? RecipeComments { get; set; }
        public virtual List<Tag>? Tags { get; set; }
        public virtual List<RecipeLike>? Likes { get; set; }
    }
}
