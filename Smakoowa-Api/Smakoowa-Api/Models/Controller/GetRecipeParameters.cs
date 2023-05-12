namespace Smakoowa_Api.Models.Controller
{
    public class GetRecipeParameters
    {
        public int? recipeCount { get; set; } = null;
        public DateTime? startDate { get; set; } = DateTime.MinValue;
        public DateTime? endDate { get; set; } = DateTime.MaxValue;
    }
}
