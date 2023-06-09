﻿namespace Smakoowa_Api.Models.DatabaseModels
{
    public class Tag : IDbModel, INameable, ILikeable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TagType TagType { get; set; }

        public virtual List<Recipe>? Recipes { get; set; }
        public virtual List<TagLike>? Likes { get; set; }
    }
}
