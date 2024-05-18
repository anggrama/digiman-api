using Asp.Versioning;
using digiman_common.Dto.Shared;
using digiman_dal.Models;
using digiman_service.DigiDocu.v1;
using Microsoft.EntityFrameworkCore;

namespace digiman_api.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddTransient<UserService>();
            services.AddTransient<StorageService>();
            services.AddTransient<GroupService>();
            services.AddTransient<ParameterService>();

            return services;
        }

        public static IServiceCollection AddDatabaseContext(this IServiceCollection services)
        {
            services.AddDbContext<DigimanContext>(
                    options => options.UseSqlServer("name=ConnectionStrings:sqlDb")
                );

            return services;
        }

        public static IServiceCollection AddUserLoginInfo(this IServiceCollection services)
        {
            services.AddScoped<UserLoginInfo>();

            return services;
        }

        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddEndpointsApiExplorer();

            return services;
        }
    }
}
