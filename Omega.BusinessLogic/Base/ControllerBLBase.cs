using Omega.Entities;
using Omega.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omega.BusinessLogic.Base
{
    public class ControllerBLBase
    {
        public ControllerBLBase(OmegaContext omegaContext)
        {
            OmegaContext = omegaContext;
        }
        public ControllerBLBase(InventoryContext context)
        {
            Context = context;
        }

        public InventoryContext _Context;

        public InventoryContext Context
        {
            get
            {
                if (_Context == null)
                {
                    _Context = OmegaContext.InventoryContext;
                }
                return _Context;
            }
            set
            {
                _Context = value;
            }
        }

        public OmegaContext OmegaContext { get; set; }
    }
}
