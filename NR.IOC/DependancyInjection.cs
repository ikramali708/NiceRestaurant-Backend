using Microsoft.Extensions.DependencyInjection;
using NR.Data;
using NR.Domain.Interfaces;
using NR.Core.Services;
using NR.Core.Interface;
using NR.Core.Service;
using NR.Data.Repository;
using NR.Domain.Interface;

namespace NR.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IGalleryImageService, GalleryImageService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IChefService, ChefService>();
            services.AddScoped<ITestimonialService, TestimonialService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IHeroService, HeroService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IContactSubmissionService, ContactSubmissionService>();
            return services;
        }
    }
}