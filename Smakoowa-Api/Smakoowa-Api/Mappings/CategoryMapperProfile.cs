namespace Smakoowa_Api.Mappings
{
    public class CategoryMapperProfile : Profile
    {
        public CategoryMapperProfile()
        {
            CreateMap<CreateCategoryRequestDto, Category>();
            CreateMap<Category, GetCategoryResponseDto>();
        }
    }
}
