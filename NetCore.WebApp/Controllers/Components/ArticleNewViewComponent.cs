using Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCore.ViewModels;
using System.Threading.Tasks;

namespace NetCore.WebApp.Controllers.Components
{
    public class ArticleNewViewComponent : ViewComponent
    {
        private readonly AppSetting _appSetting;
        public ArticleNewViewComponent(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string url = _appSetting.UrlApi + "api/article/top-new?TopRow=5";
            var data = await ApiService.GetAsync<RootObject<ArticleModel>>(url);
            //NLogLogger.Info("InvokeAsync: " + JsonConvert.SerializeObject(data));
            return View(data.Items);
        }
    }
}
