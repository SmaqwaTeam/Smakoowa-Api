namespace Smakoowa_Api.Models.RequestDtos.Category
{
    public class CreateCategoryRequestDto : IRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
