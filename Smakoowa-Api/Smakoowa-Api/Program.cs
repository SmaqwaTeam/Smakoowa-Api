using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Smakoowa_Api.Middlewares;
using Smakoowa_Api.Services.BackgroundTaskQueue;
using Smakoowa_Api.Services.MapperServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"])),
        ValidateLifetime = false,
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    c.AddSecurityDefinition("Bearer", securitySchema);

    var securityRequirement = new OpenApiSecurityRequirement();
    securityRequirement.Add(securitySchema, new[] { "Bearer" });
    c.AddSecurityRequirement(securityRequirement);
});

builder.Services.AddScoped(sp => new HttpClient());
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SmakoowaApiDBConnection")), contextLifetime: ServiceLifetime.Scoped);
builder.Services.AddDbContext<BackgroundDataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SmakoowaApiDBConnectionBackground")), contextLifetime: ServiceLifetime.Singleton);
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
builder.Services.AddScoped(typeof(ITagLikeService), typeof(TagLikeService));
builder.Services.AddScoped(typeof(ILikeValidatorService), typeof(LikeValidatorService));

builder.Services.AddScoped(typeof(IApiUserService), typeof(ApiUserService));
builder.Services.AddScoped(typeof(IAccountService), typeof(AccountService));
builder.Services.AddScoped(typeof(IApiUserRepository), typeof(ApiUserRepository));
builder.Services.AddScoped<RoleManager<ApiRole>>();

builder.Services.AddSingleton(typeof(IRequestCounterService), typeof(RequestCounterService));
builder.Services.AddScoped(typeof(IControllerStatisticsService), typeof(ControllerStatisticsService));
builder.Services.AddScoped(typeof(IRequestCountMapperService), typeof(RequestCountMapperService));
builder.Services.AddScoped(typeof(IRequestCountRepository), typeof(RequestCountRepository));

builder.Services.AddHostedService<QueuedHostedService>();
builder.Services.AddSingleton<IBackgroundTaskQueue>(_ =>
{
    if (!int.TryParse(builder.Configuration["QueueCapacity"], out var queueCapacity))
    {
        queueCapacity = 100;
    }

    return new DefaultBackgroundTaskQueue(queueCapacity);
});

builder.Services.AddIdentity<ApiUser, ApiRole>(opt =>
{
    opt.Password.RequiredLength = 7;
    opt.Password.RequireDigit = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireNonAlphanumeric = false;
    opt.User.RequireUniqueEmail = true;
    opt.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

builder.Services.AddCors(options =>
{
    options.AddPolicy("corspolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "https://localhost:5173") //enter your url here
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

configuration = builder.Configuration;

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
    if (exception is SecurityTokenValidationException) context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
    else context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    await context.Response.WriteAsJsonAsync(ServiceResponse.Error(exception.Message, HttpStatusCode.InternalServerError));
}));

app.UseCors("corspolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.UseMiddleware<RequestCountMiddleware>();

app.Run();

public partial class Program
{
    public static IConfiguration configuration { get; private set; }
}
