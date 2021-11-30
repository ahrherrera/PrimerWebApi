using Omega.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omega.Entities
{
    public interface IOmegaContext
    {
        public InventoryContext InventoryContext { get; set; }
    }
}
