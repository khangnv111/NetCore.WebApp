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
            string url = _appSetting.UrlApi + "api/advert/get?position=" + pos;
            var list = await ApiService.GetAsync<RootObject<AdvertModel>>(url);
            var data = list.Items != null && list.Items.Count > 0 ? list.Items[0] : null;

            ViewBag.Pos = pos;
            return View(data);
        }
    }
}
