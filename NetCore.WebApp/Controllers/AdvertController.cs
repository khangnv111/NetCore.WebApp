using Lib;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.WebApp.Controllers
{
    public class AdvertController : Controller
    {
        public IActionResult HomeHorizontal()
        {
            NLogLogger.Info("HomeHorizontal.....");
            return PartialView();
        }
    }
}
