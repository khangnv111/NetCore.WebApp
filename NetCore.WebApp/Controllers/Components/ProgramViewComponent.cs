using Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCore.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.WebApp.Controllers.Components
{
    public class ProgramViewComponent : ViewComponent
    {
        private readonly AppSetting _appSetting;
        public ProgramViewComponent(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string url = _appSetting.UrlApi + "api/program/get?IsSetted=-1&Page=1&PageSize=6";
            var data = await ApiService.GetAsync<RootObject<ProgramModel>>(url);
            NLogLogger.Info("Program InvokeAsync: " + JsonConvert.SerializeObject(data));
            ViewBag.UrlRoot = _appSetting.UrlRoot;
            return View(data.Items);
        }
    }
}
