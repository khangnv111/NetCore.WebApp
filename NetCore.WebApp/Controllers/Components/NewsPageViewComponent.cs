using Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.WebApp.Controllers.Components
{
    public class NewsPageViewComponent : ViewComponent
    {
        private readonly AppSetting _appSetting;
        public NewsPageViewComponent(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync(string Url = "")
        {
            string url = _appSetting.UrlApi + "api/article/get-list?TopRow=10&UrlRedirect=" +  Url + "&isHot=-1&Page=1&PageSize=4";
            var data = await ApiService.GetAsync<RootObject<ArticleModel>>(url);
            //NLogLogger.Info("InvokeAsync: " + JsonConvert.SerializeObject(data));
            ViewBag.UrlRoot = _appSetting.UrlRoot;
            return View(data.Items);
        }
    }
}
