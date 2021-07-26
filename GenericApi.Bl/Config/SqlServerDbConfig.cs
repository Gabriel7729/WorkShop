using GenericApi.Model.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericApi.Bl.Config
{
    public static class SqlServerDbConfig
    {
        public static IServiceCollection ConfigSqlServerDbContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<WorkShopContext>(options => options.UseSqlServer(connection));

            return services;
        }
    }
}
