using Microsoft.AspNetCore.Mvc;
using Omega.Entities;
using Omega.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerWebApi.Controllers.Base
{
    [ApiController]
    public class OmegaControllerBase<T> : Controller
    {
        public OmegaControllerBase(IOmegaContext omegaContext)
        {
            OmegaContext = omegaContext as OmegaContext;
        }

        OmegaContext OmegaContext { get; set; }

        protected InventoryContext _context
        {
            get
            {
                return OmegaContext.InventoryContext;
            }
        }
    }
}
