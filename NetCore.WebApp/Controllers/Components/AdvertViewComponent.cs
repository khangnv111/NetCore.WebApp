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
    public class AdvertViewComponent : ViewComponent
    {
        private readonly AppSetting _appSetting;
        public AdvertViewComponent(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string url = _appSetting.UrlApi + "api/article/get-list?TopRow=1&isHot=1&Page=1&PageSize=5";
            var data = await ApiService.GetAsync<RootObject<ArticleModel>>(url);
            //NLogLogger.Info("InvokeAsync: " + JsonConvert.SerializeObject(data));
            return View(data.Items);
        }
    }
}
