﻿namespace Smakoowa_Api.Services.Interfaces
{
    public interface IRecipeService : ICreatorService<RecipeRequestDto>, IEditorService<RecipeRequestDto>, IDeleterService
    {
    }
}
