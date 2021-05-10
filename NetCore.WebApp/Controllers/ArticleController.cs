using Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCore.ViewModels;
using NetCore.WebApp.DataAccess;
using NetCore.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.WebApp.Controllers
{
    public class ArticleController : Controller
    {
        //private readonly ArticleAccess _articleAccess;
        private readonly AppSetting _appSetting;
        public ArticleController(IOptions<AppSetting> appSetting)
        {
            //_articleAccess = articleAccess;
            _appSetting = appSetting.Value;
        }

        #region Tin tức
        public async Task<IActionResult> NewsPage()
        {
            string url = _appSetting.UrlApi + "api/article/get-list?TopRow=1&MenuID=5&isHot=-1&Page=1&PageSize=10";
            var listArt = await ApiService.GetAsync<RootObject<ArticleModel>>(url);
            var data = new NewsPageModel { ArtHot = listArt.Items.FirstOrDefault() };

            ViewBag.UrlRoot = _appSetting.UrlRoot;

            return View(data);
        }
        #endregion

        #region Video hình ảnh
        public IActionResult VideoImage()
        {
            return View();
        }
        #endregion

    }
}
