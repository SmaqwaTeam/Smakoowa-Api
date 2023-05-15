namespace Smakoowa_Api
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

        public static void ConfigureBackgroundQueue(WebApplicationBuilder builder)
        {
            builder.Services.AddHostedService<QueuedHostedService>();
            builder.Services.AddSingleton<IBackgroundTaskQueue>(_ =>
            {
                if (!int.TryParse(builder.Configuration["QueueCapacity"], out var queueCapacity))
                {
                    queueCapacity = 1000;
                }
                return new BackgroundTaskQueue(queueCapacity);
            });
        }

        public static void ConfigureCors(WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("corspolicy",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173", "https://localhost:5173", "https://10.0.2.2:7188")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IHelperService<>), typeof(HelperService<>));
            services.AddTransient(typeof(IJsonKeyValueGetter), typeof(JsonKeyValueGetter));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddLogging();

            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddScoped(typeof(ICategoryValidatorService), typeof(CategoryValidatorService));
            services.AddScoped(typeof(ICategoryMapperService), typeof(CategoryMapperService));
            services.AddScoped(typeof(ICategoryService), typeof(CategoryService));

            services.AddScoped(typeof(ITagRepository), typeof(TagRepository));
            services.AddScoped(typeof(ITagValidatorService), typeof(TagValidatorService));
            services.AddScoped(typeof(ITagMapperService), typeof(TagMapperService));
            services.AddScoped(typeof(ITagService), typeof(TagService));
            services.AddScoped(typeof(ITagGetterService), typeof(TagGetterService));

            services.AddScoped(typeof(IRecipeRepository), typeof(RecipeRepository));
            services.AddScoped(typeof(IRecipeValidatorService), typeof(RecipeValidatorService));
            services.AddScoped(typeof(IRecipeMapperService), typeof(RecipeMapperService));
            services.AddScoped(typeof(IRecipeService), typeof(RecipeService));
            services.AddScoped(typeof(IRecipeGetterService), typeof(RecipeGetterService));

            services.AddScoped(typeof(IIngredientRepository), typeof(IngredientRepository));
            services.AddScoped(typeof(IIngredientValidatorService), typeof(IngredientValidatorService));
            services.AddScoped(typeof(IIngredientMapperService), typeof(IngredientMapperService));

            services.AddScoped(typeof(IInstructionRepository), typeof(InstructionRepository));
            services.AddScoped(typeof(IInstructionValidatorService), typeof(InstructionValidatorService));
            services.AddScoped(typeof(IInstructionMapperService), typeof(InstructionMapperService));

            services.AddScoped(typeof(IRecipeCommentService), typeof(RecipeCommentService));
            services.AddScoped(typeof(IRecipeCommentValidatorService), typeof(RecipeCommentValidatorService));
            services.AddScoped(typeof(IRecipeCommentMapperService), typeof(RecipeCommentMapperService));

            services.AddScoped(typeof(ICommentReplyValidatorService), typeof(CommentReplyValidatorService));
            services.AddScoped(typeof(ICommentReplyMapperService), typeof(CommentReplyMapperService));
            services.AddScoped(typeof(ICommentReplyService), typeof(CommentReplyService));

            services.AddScoped(typeof(IRecipeLikeService), typeof(RecipeLikeService));
            services.AddScoped(typeof(IRecipeCommentLikeService), typeof(RecipeCommentLikeService));
            services.AddScoped(typeof(ICommentReplyLikeService), typeof(CommentReplyLikeService));
            services.AddScoped(typeof(ITagLikeService), typeof(TagLikeService));

            services.AddScoped(typeof(IRecipeLikeValidatorService), typeof(RecipeLikeValidatorService));
            services.AddScoped(typeof(IRecipeCommentLikeValidatorService), typeof(RecipeCommentLikeValidatorService));
            services.AddScoped(typeof(ICommentReplyLikeValidatorService), typeof(CommentReplyLikeValidatorService));
            services.AddScoped(typeof(ITagLikeValidatorService), typeof(TagLikeValidatorService));

            services.AddScoped(typeof(IApiUserService), typeof(ApiUserService));
            services.AddScoped(typeof(IApiUserMapperService), typeof(ApiUserMapperService));
            services.AddScoped(typeof(IApiUserGetterService), typeof(ApiUserGetterService));
            services.AddScoped(typeof(IAccountService), typeof(AccountService));
            services.AddScoped(typeof(IApiUserRepository), typeof(ApiUserRepository));
            services.AddScoped(typeof(IApiUserValidatorService), typeof(ApiUserValidatorService));
            services.AddScoped<RoleManager<ApiRole>>();

            services.AddSingleton(typeof(IRequestCounterService), typeof(RequestCounterService));
            services.AddScoped(typeof(IControllerStatisticsService), typeof(ControllerStatisticsService));
            services.AddScoped(typeof(IRequestCountMapperService), typeof(RequestCountMapperService));
            services.AddScoped(typeof(IRequestCountRepository), typeof(RequestCountRepository));

            services.AddScoped(typeof(IImageService), typeof(ImageService));
            services.AddScoped(typeof(IImageValidatorService), typeof(ImageValidatorService));
        }
    }
}
