namespace Smakoowa_Api.Data.DatabaseSeeds
{
    public static class TagSeed
    {
        public static void SeedTags(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().HasData(
                new Tag { Id = 1, Name = "American", TagType = TagType.Cuisine },
                new Tag { Id = 2, Name = "Asian", TagType = TagType.Cuisine },
                new Tag { Id = 3, Name = "Czech", TagType = TagType.Cuisine },
                new Tag { Id = 4, Name = "Polish", TagType = TagType.Cuisine },
                new Tag { Id = 5, Name = "Italian", TagType = TagType.Cuisine },
                new Tag { Id = 6, Name = "Mexican", TagType = TagType.Cuisine },
                new Tag { Id = 7, Name = "Indian", TagType = TagType.Cuisine },
                new Tag { Id = 8, Name = "French", TagType = TagType.Cuisine },
                new Tag { Id = 9, Name = "Chinese", TagType = TagType.Cuisine },
                new Tag { Id = 10, Name = "Greek", TagType = TagType.Cuisine },
                new Tag { Id = 11, Name = "Balkan", TagType = TagType.Cuisine },
                new Tag { Id = 12, Name = "Thai", TagType = TagType.Cuisine },
                new Tag { Id = 13, Name = "Hungarian", TagType = TagType.Cuisine },
                new Tag { Id = 14, Name = "Mediterranean", TagType = TagType.Cuisine },
                new Tag { Id = 15, Name = "Ukrainian", TagType = TagType.Cuisine },
                new Tag { Id = 16, Name = "Jewish", TagType = TagType.Cuisine },

                new Tag { Id = 17, Name = "Gluten-free", TagType = TagType.Diet },
                new Tag { Id = 18, Name = "Lactose-free", TagType = TagType.Diet },
                new Tag { Id = 19, Name = "Sugar-free", TagType = TagType.Diet },
                new Tag { Id = 20, Name = "For children", TagType = TagType.Diet },
                new Tag { Id = 21, Name = "Dietary", TagType = TagType.Diet },
                new Tag { Id = 22, Name = "Vegetarian", TagType = TagType.Diet },
                new Tag { Id = 23, Name = "Vegan", TagType = TagType.Diet },
                new Tag { Id = 24, Name = "For health", TagType = TagType.Diet },

                new Tag { Id = 25, Name = "Easter", TagType = TagType.Occasion },
                new Tag { Id = 26, Name = "Christmas", TagType = TagType.Occasion },
                new Tag { Id = 27, Name = "Party", TagType = TagType.Occasion },
                new Tag { Id = 28, Name = "Grill", TagType = TagType.Occasion },
                new Tag { Id = 29, Name = "Fat Thursday", TagType = TagType.Occasion },
                new Tag { Id = 30, Name = "Valentine's Day", TagType = TagType.Occasion },
                new Tag { Id = 31, Name = "Halloween", TagType = TagType.Occasion },
                new Tag { Id = 32, Name = "Communion", TagType = TagType.Occasion },
                new Tag { Id = 33, Name = "For work", TagType = TagType.Occasion }
            );
        }
    }
}
