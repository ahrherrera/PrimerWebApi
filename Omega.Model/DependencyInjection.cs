using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omega.Model
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddContext(this IServiceCollection services, IConfigurationRoot Configuration) {


            services.AddDbContext<InventoryContext>(options =>
            {
                string connection = Configuration.GetConnectionString("InventoryContext");
                //TODO: Desencriptar el string de conexión

                options.EnableSensitiveDataLogging();
                options.UseSqlServer(
                    connection,
                    connectionOptions =>
                    {
                        connectionOptions.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
                        //connectionOptions.UseRowNumberForPaging();
                        connectionOptions.EnableRetryOnFailure(6, TimeSpan.FromSeconds(10), null);

                        connectionOptions.MigrationsHistoryTable("__EFMigrationsHistory");
                    });
            }, ServiceLifetime.Transient);

            return services;
        }
    }
}
