using System.Globalization;
using e_parliament.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using BackEndStructuer.DATA;
using BackEndStructuer.Helpers;
using BackEndStructuer.Helpers.OneSignal;
using BackEndStructuer.Repository;
using BackEndStructuer.Services;
using Gaz_BackEnd.DATA;
using Gaz_BackEnd.Services;

namespace BackEndStructuer.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(options => options.UseNpgsql(config.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IArticleServices, ArticleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGovernorateService, GovernoratesService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IOtpService, OtpService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IProductServices, ProductSerivce>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IStationService, StationService>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<INotificationService, NotificationService>();
            
            
            return services;
        }


    }
}