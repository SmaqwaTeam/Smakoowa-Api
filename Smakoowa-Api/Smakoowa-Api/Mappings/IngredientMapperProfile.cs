namespace Smakoowa_Api.Mappings
{
    public class IngredientMapperProfile : Profile
    {
        public IngredientMapperProfile()
        {
            CreateMap<IngredientRequestDto, Ingredient>();
            CreateMap<Ingredient, GetIngredientResponseDto>();
        }
    }
}
