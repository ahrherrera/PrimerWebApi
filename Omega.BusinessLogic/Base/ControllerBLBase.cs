using Omega.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omega.BusinessLogic.Base
{
    public class ControllerBLBase
    {
        public ControllerBLBase(InventoryContext context)
        {
            Context = context;
        }

        public InventoryContext Context;


    }
}
