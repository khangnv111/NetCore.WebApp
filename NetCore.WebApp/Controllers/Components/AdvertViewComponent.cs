using Microsoft.AspNetCore.Mvc;
using NetCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.WebApp.Controllers.Components
{
    public class AdvertViewComponent : ViewComponent
    {
        private readonly AppSetting _appSetting;
        public AdvertViewComponent() { }
    }
}
