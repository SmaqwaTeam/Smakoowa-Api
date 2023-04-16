﻿namespace Smakoowa_Api.Models.ResponseDtos
{
    public class RecipeResponseDto : UpdateableResponseDto, IResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public PublishStatus PublishStatus { get; set; }
        public ServingsTier ServingsTier { get; set; }
        public TimeToMakeTier TimeToMakeTier { get; set; }
        public int CategoryId { get; set; }
        public List<int>? TagIds { get; set; }
        public string? ThumbnailImageUrl { get; set; }
    }
}
