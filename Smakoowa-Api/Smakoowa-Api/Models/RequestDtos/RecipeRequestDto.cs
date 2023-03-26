namespace Smakoowa_Api.Models.RequestDtos
{
    public class RecipeRequestDto : IRequestDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public ServingsTier ServingsTier { get; set; }
        public TimeToMakeTier TimeToMakeTier { get; set; }

        public int CategoryId { get; set; }
        public List<int>? TagIds { get; set; }

        //To be implemented:
        //public List<Instruction> Instructions { get; set; }
        //public List<Ingredient> Ingredients { get; set; }
    }
}
