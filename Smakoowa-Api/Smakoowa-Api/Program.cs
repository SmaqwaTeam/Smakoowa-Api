using Smakoowa_Api.Services.MapperServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SmakoowaApiDBConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddLogging();

builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IHelperService<>), typeof(HelperService<>));

builder.Services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
builder.Services.AddScoped(typeof(ICategoryValidatorService), typeof(CategoryValidatorService));
builder.Services.AddScoped(typeof(ICategoryMapperService), typeof(CategoryMapperService));
builder.Services.AddScoped(typeof(ICategoryService), typeof(CategoryService));

builder.Services.AddScoped(typeof(ITagRepository), typeof(TagRepository));
builder.Services.AddScoped(typeof(ITagValidatorService), typeof(TagValidatorService));
builder.Services.AddScoped(typeof(ITagMapperService), typeof(TagMapperService));
builder.Services.AddScoped(typeof(ITagService), typeof(TagService));

builder.Services.AddScoped(typeof(IRecipeRepository), typeof(RecipeRepository));
builder.Services.AddScoped(typeof(IRecipeValidatorService), typeof(RecipeValidatorService));
builder.Services.AddScoped(typeof(IRecipeMapperService), typeof(RecipeMapperService));
builder.Services.AddScoped(typeof(IRecipeService), typeof(RecipeService));

builder.Services.AddScoped(typeof(IIngredientRepository), typeof(IngredientRepository));
builder.Services.AddScoped(typeof(IIngredientValidatorService), typeof(IngredientValidatorService));
builder.Services.AddScoped(typeof(IIngredientMapperService), typeof(IngredientMapperService));

builder.Services.AddScoped(typeof(IInstructionRepository), typeof(InstructionRepository));
builder.Services.AddScoped(typeof(IInstructionValidatorService), typeof(InstructionValidatorService));
builder.Services.AddScoped(typeof(IInstructionMapperService), typeof(InstructionMapperService));



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
