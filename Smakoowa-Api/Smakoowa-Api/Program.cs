using Microsoft.AspNetCore.Diagnostics;
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

builder.Services.AddScoped(typeof(IRecipeCommentRepository), typeof(RecipeCommentRepository));
builder.Services.AddScoped(typeof(ICommentValidatorService), typeof(CommentValidatorService));
builder.Services.AddScoped(typeof(ICommentMapperService), typeof(CommentMapperService));
builder.Services.AddScoped(typeof(ICommentService), typeof(CommentService));

builder.Services.AddScoped(typeof(ICommentReplyRepository), typeof(CommentReplyRepository));
builder.Services.AddScoped(typeof(IBaseRepository<CommentReply>), typeof(BaseRepository<CommentReply>));
builder.Services.AddScoped(typeof(IBaseRepository<RecipeComment>), typeof(BaseRepository<RecipeComment>));

builder.Services.AddScoped(typeof(ILikeRepository), typeof(LikeRepository));
builder.Services.AddScoped(typeof(IRecipeLikeService), typeof(RecipeLikeService));
builder.Services.AddScoped(typeof(IRecipeCommentLikeService), typeof(RecipeCommentLikeService));
builder.Services.AddScoped(typeof(ICommentReplyLikeService), typeof(CommentReplyLikeService));
builder.Services.AddScoped(typeof(ILikeValidatorService), typeof(LikeValidatorService));

builder.Services.AddScoped(typeof(IApiUserService), typeof(ApiUserService));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    await context.Response.WriteAsJsonAsync(ServiceResponse.Error(exception.Message, HttpStatusCode.InternalServerError));
}));

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }