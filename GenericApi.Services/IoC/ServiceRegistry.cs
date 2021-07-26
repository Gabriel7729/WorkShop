using GenericApi.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericApi.Services.IoC
{
    public static class ServiceRegistry
    {
        public static void AddServiceRegistry(this IServiceCollection services)
        {
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWorkShopService, WorkShopService>();
            services.AddScoped<IWorkShopDayService, WorkShopDayService>();
            services.AddScoped<IWorkShopMemberService, WorkShopMemberService>();
        }
    }
}
