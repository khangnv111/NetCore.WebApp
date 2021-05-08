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
    public class ArticleViewComponent : ViewComponent
    {
        private readonly AppSetting _appSetting;
        public ArticleViewComponent(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync(int CateId = -1)
        {
            int MenuID = 5;
            if (CateId != -1)
                MenuID = CateId;

            string url = _appSetting.UrlApi + "api/article/get-list?TopRow=10&MenuID=" + MenuID + "&isHot=-1&Page=1&PageSize=4";
            var data = await ApiService.GetAsync<Rootobject<ArticleModel>>(url);
            //NLogLogger.Info("InvokeAsync: " + JsonConvert.SerializeObject(data));
            return View(data.Items);
        }
    }
}
