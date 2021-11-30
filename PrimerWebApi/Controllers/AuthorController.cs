using Microsoft.AspNetCore.Mvc;
using Omega.BusinessLogic;
using Omega.Entities;
using PrimerWebApi.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerWebApi.Controllers
{

    public class AuthorController : OmegaControllerBase<AuthorControllerBL>
    {

        public AuthorController(IOmegaContext omegaContext) : base(omegaContext)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
