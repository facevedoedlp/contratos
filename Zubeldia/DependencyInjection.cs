namespace Zubeldia
{
    using System.Diagnostics.CodeAnalysis;
    using AutoMapper;
    using FluentValidation;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.OpenApi.Models;
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Domain.Interfaces.Providers;
    using Zubeldia.Domain.Interfaces.Services;
    using Zubeldia.Domain.Mappers.Profiles;
    using Zubeldia.Domain.Validators.Contract;
    using Zubeldia.Dtos.Models.User;
    using Zubeldia.Dtos.Validatiors.User;
    using Zubeldia.Providers;
    using Zubeldia.Providers.Repositories;
    using Zubeldia.Providers.Repositories.User;
    using Zubeldia.Services;
    using Zubeldia.Services.Session;

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
                options.UseSqlServer(
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
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITokenConfiguration, TokenService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IPdfContractProcessor, PdfContractProcessor>();
        }

        private static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RegisterUserRequest>, UserValidator>();
            services.AddScoped<IValidator<CreateContractRequest>, ContractValidator>();
        }

        private static void AddMappers(this IServiceCollection services)
        {
            services.AddSingleton(provider => new MapperConfiguration(m =>
            {
                m.AddProfile(new UserProfile());
                m.AddProfile(new ValidatorResultProfile());
                m.AddProfile(new ContractProfile());
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