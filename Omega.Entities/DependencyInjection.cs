using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Omega.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omega.Entities
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMainOmegaContext(this IServiceCollection service, IConfigurationRoot Configuration)
        {
            service.AddTransient<IOmegaContext, OmegaContext>((ctx) =>
            {
                InventoryContext inventoryContext = ctx.GetService<InventoryContext>();

                OmegaContext omegaContext = new OmegaContext();
                omegaContext.InventoryContext = inventoryContext;

                return omegaContext;
            });
            return service;
        }
    }
}
