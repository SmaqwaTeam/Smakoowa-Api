using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Smakoowa_Api.Services.BackgroundTaskQueue;
using Smakoowa_Api.Services.Helper;
using Smakoowa_Api.Services.Interfaces.Helper;
using Smakoowa_Api.Services.MapperServices;
using System.Text;

namespace Smakoowa_Api.Services
{
    public static class ServicesConfigurator
    {
        public static void ConfigureSwagger(WebApplicationBuilder builder)
        {
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
        }

        public static void ConfigureIdentity(WebApplicationBuilder builder)
        {
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

            builder.Services.AddIdentity<ApiUser, ApiRole>(opt =>
            {
                opt.Password.RequiredLength = 7;
                opt.Password.RequireDigit = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
        }

        public static void ConfigureDatabaseConnection(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<DataContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("SmakoowaApiDBConnection")), contextLifetime: ServiceLifetime.Scoped);

            builder.Services.AddDbContext<BackgroundDataContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("SmakoowaApiDBConnectionBackground")), contextLifetime: ServiceLifetime.Singleton);
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IHelperService<>), typeof(HelperService<>));
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddScoped(typeof(ICategoryValidatorService), typeof(CategoryValidatorService));
            services.AddScoped(typeof(ICategoryMapperService), typeof(CategoryMapperService));
            services.AddScoped(typeof(ICategoryService), typeof(CategoryService));
            services.AddScoped(typeof(ITagRepository), typeof(TagRepository));
            services.AddScoped(typeof(ITagValidatorService), typeof(TagValidatorService));
            services.AddScoped(typeof(ITagMapperService), typeof(TagMapperService));
            services.AddScoped(typeof(ITagService), typeof(TagService));
            services.AddScoped(typeof(IRecipeRepository), typeof(RecipeRepository));
            services.AddScoped(typeof(IRecipeValidatorService), typeof(RecipeValidatorService));
            services.AddScoped(typeof(IRecipeMapperService), typeof(RecipeMapperService));
            services.AddScoped(typeof(IRecipeService), typeof(RecipeService));
            services.AddScoped(typeof(IIngredientRepository), typeof(IngredientRepository));
            services.AddScoped(typeof(IIngredientValidatorService), typeof(IngredientValidatorService));
            services.AddScoped(typeof(IIngredientMapperService), typeof(IngredientMapperService));
            services.AddScoped(typeof(IInstructionRepository), typeof(InstructionRepository));
            services.AddScoped(typeof(IInstructionValidatorService), typeof(InstructionValidatorService));
            services.AddScoped(typeof(IInstructionMapperService), typeof(InstructionMapperService));
            services.AddScoped(typeof(IRecipeCommentRepository), typeof(RecipeCommentRepository));
            services.AddScoped(typeof(ICommentValidatorService), typeof(CommentValidatorService));
            services.AddScoped(typeof(ICommentMapperService), typeof(CommentMapperService));
            services.AddScoped(typeof(ICommentService), typeof(CommentService));
            services.AddScoped(typeof(ICommentReplyRepository), typeof(CommentReplyRepository));
            services.AddScoped(typeof(ILikeRepository), typeof(LikeRepository));
            services.AddScoped(typeof(IRecipeLikeService), typeof(RecipeLikeService));
            services.AddScoped(typeof(IRecipeCommentLikeService), typeof(RecipeCommentLikeService));
            services.AddScoped(typeof(ICommentReplyLikeService), typeof(CommentReplyLikeService));
            services.AddScoped(typeof(ITagLikeService), typeof(TagLikeService));
            services.AddScoped(typeof(ILikeValidatorService), typeof(LikeValidatorService));
            services.AddScoped(typeof(IApiUserService), typeof(ApiUserService));
            services.AddScoped(typeof(IAccountService), typeof(AccountService));
            services.AddScoped(typeof(IApiUserRepository), typeof(ApiUserRepository));
            services.AddScoped<RoleManager<ApiRole>>();
            services.AddSingleton(typeof(IRequestCounterService), typeof(RequestCounterService));
            services.AddScoped(typeof(IControllerStatisticsService), typeof(ControllerStatisticsService));
            services.AddScoped(typeof(IRequestCountMapperService), typeof(RequestCountMapperService));
            services.AddScoped(typeof(IRequestCountRepository), typeof(RequestCountRepository));
            services.AddScoped(typeof(IImageService), typeof(ImageService));
            services.AddScoped(typeof(IImageValidatorService), typeof(ImageValidatorService));
            services.AddScoped(typeof(IRecipeCommentLikeRepository), typeof(RecipeCommentLikeRepository));
            services.AddScoped(typeof(ICommentReplyLikeRepository), typeof(CommentReplyLikeRepository));
            services.AddScoped(typeof(IRecipeLikeRepository), typeof(RecipeLikeRepository));
            services.AddScoped(typeof(ITagLikeRepository), typeof(TagLikeRepository));
            services.AddScoped(typeof(ICommentRepository), typeof(CommentRepository));
            services.AddTransient(typeof(IJsonKeyValueGetter), typeof(JsonKeyValueGetter));
            services.AddHostedService<QueuedHostedService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddLogging();
        }
    }
}
