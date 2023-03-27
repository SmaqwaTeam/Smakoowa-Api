namespace Smakoowa_Api.Mappings
{
    public class RecipeMapperProfile : Profile
    {
        public RecipeMapperProfile()
        {
            CreateMap<RecipeRequestDto, Recipe>();
            CreateMap<Recipe, GetRecipeResponseDto>()
                .AfterMap((src, dest) => dest.TagIds = src.Tags?.Select(t => t.Id).ToList());
            CreateMap<Recipe, GetDetailedRecipeResponseDto>()
                .AfterMap((src, dest) => dest.TagIds = src.Tags?.Select(t => t.Id).ToList());
        }
    }
}
