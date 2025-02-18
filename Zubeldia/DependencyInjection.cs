namespace Zubeldia
{
    using System.Diagnostics.CodeAnalysis;
    using AutoMapper;
    using FluentValidation;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.OpenApi.Models;
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Domain.Dtos.Player;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Interfaces.Providers;
    using Zubeldia.Domain.Interfaces.Services;
    using Zubeldia.Domain.Mappers.Profiles;
    using Zubeldia.Domain.Validators.Contract;
    using Zubeldia.Domain.Validators.Player;
    using Zubeldia.Dtos.Models.User;
    using Zubeldia.Dtos.Validatiors.User;
    using Zubeldia.Providers;
    using Zubeldia.Providers.Repositories;
    using Zubeldia.Providers.Repositories.Category;
    using Zubeldia.Providers.Repositories.Discipline;
    using Zubeldia.Providers.Repositories.Position;
    using Zubeldia.Providers.Repositories.User;
    using Zubeldia.Services;
    using Zubeldia.Services.Category;
    using Zubeldia.Services.City;
    using Zubeldia.Services.Currency;
    using Zubeldia.Services.Files;
    using Zubeldia.Services.Player;
    using Zubeldia.Services.Session;
    using Zubeldia.Services.State;

    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static void InjectionStart(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment hostEnvironment)
        {
            services.AddHttpContextAccessorCustom();
            services.AddDatabase(configuration);
            services.AddHttpClient();
            services.AddSwaggerCustom(configuration);
            services.AddMappers();
            services.AddProviders();
            services.AddServices();
            services.AddValidators();
        }

        private static void AddHttpContextAccessorCustom(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<ISessionAccessor, SessionAccessor>();
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ZubeldiaDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("ZUBELDIA_DB_CONNECTION")
                ));

            services.AddScoped<IZubeldiaDbContext>(provider =>
                provider.GetService<ZubeldiaDbContext>());
        }


        private static void AddProviders(this IServiceCollection services)
        {
            services.AddScoped(typeof(IReadRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserDao, UserDao>();
            services.AddScoped<IContractDao, ContractDao>();
            services.AddScoped<IPlayerDao, PlayerDao>();
            services.AddScoped<ICurrencyDao, CurrencyDao>();
            services.AddScoped<ICountryDao, CountryDao>();
            services.AddScoped<ICityDao, CityDao>();
            services.AddScoped<IStateDao, StateDao>();
            services.AddScoped<ICategoryDao, CategoryDao>();
            services.AddScoped<IDisciplineDao, DisciplineDao>();
            services.AddScoped<IPositionDao, PositionDao>();
            services.AddScoped<IHealthcarePlanDao, HealthcarePlanDao>();
            services.AddScoped<IAgentDao, AgentDao>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITokenConfiguration, TokenService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IPdfContractProcessor, PdfContractProcessor>();
            services.AddScoped<IFileStorageService, FileStorageService>();
        }

        private static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RegisterUserRequest>, UserValidator>();
            services.AddScoped<IValidator<CreateContractRequest>, ContractValidator>();
            services.AddScoped<IValidator<CreatePlayerRequest>, PlayerValidator>();
        }

        private static void AddMappers(this IServiceCollection services)
        {
            services.AddSingleton(provider => new MapperConfiguration(m =>
            {
                m.AddProfile(new UserProfile());
                m.AddProfile(new ValidatorResultProfile());
                m.AddProfile(new ContractProfile());
                m.AddProfile(new PlayerProfile());
                m.AddProfile(new FilterOptionsProfile());
            }).CreateMapper());
        }

        private static IServiceCollection AddSwaggerCustom(this IServiceCollection services, IConfiguration configuration)
        {
            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "Using the Authorization header with the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
            };

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", securitySchema);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } },
                });

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = configuration.GetSection("ApplicationName")?.Value,
                    Version = "v1",
                    Description = ".NET 8 API Backend",
                });
            });

            return services;
        }
    }
}