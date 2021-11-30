using Microsoft.AspNetCore.Http;
using Omega.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omega.Entities
{
    public class OmegaContext : IOmegaContext, IDisposable
    {
        public OmegaContext()
        {

        }

        public OmegaContext(InventoryContext inventoryContext)
        {

        }

        public InventoryContext InventoryContext { get; set; }

        public int? UserId { get; set; }

        public string UserName { get; set; }

        public string UserInfo { get; set; }

        public string Token { get; set; }

        public HttpRequest Request { get; set; }

        public void Dispose()
        {

        }
    }
}
