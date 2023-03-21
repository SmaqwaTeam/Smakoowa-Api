namespace Smakoowa_Api.Models.DatabaseModels
{
    public class Category : IDbKey
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Recipe>? Recipes { get; set; }
    }
}
