using Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCore.ViewModels;
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

        public async Task<IViewComponentResult> InvokeAsync(int pos)
        {
            string url = _appSetting.UrlApi + "api/advert/detail?id=" + pos;
            var data = await ApiService.GetAsync<RootObject2<AdvertModel>>(url);
            //NLogLogger.Info("InvokeAsync: " + JsonConvert.SerializeObject(data));
            ViewBag.Pos = pos;
            return View(data.Items);
        }
    }
}
