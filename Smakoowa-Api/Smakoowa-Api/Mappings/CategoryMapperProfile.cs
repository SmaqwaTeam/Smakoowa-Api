namespace Smakoowa_Api.Mappings
{
    public class CategoryMapperProfile : Profile
    {
        public CategoryMapperProfile()
        {
            CreateMap<CategoryRequestDto, Category>();
            CreateMap<Category, GetCategoryResponseDto>();
        }
    }
}
