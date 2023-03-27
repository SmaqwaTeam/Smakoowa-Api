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
        public List<IngredientRequestDto>? Ingredients { get; set; }
        public List<InstructionRequestDto>? Instructions { get; set; }
    }
}
