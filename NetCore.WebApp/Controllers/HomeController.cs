using Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCore.ViewModels;
using NetCore.WebApp.DataAccess;
using NetCore.WebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppSetting _appSetting;

        public HomeController(ILogger<HomeController> logger, IOptions<AppSetting> appSetting)
        {
            _logger = logger;

            _appSetting = appSetting.Value;
        }

        #region Home
        public async Task<IActionResult> Index()
        {
            string url = _appSetting.UrlApi + "api/article/get-list?TopRow=10&MenuID=5&isHot=-1&Page=1&PageSize=4";
            var listArt = await ApiService.GetAsync<RootObject<ArticleModel>>(url);

            string urlVideo = _appSetting.UrlApi + "api/article/get-list?TopRow=4&MenuID=9&isHot=-1&Page=1&PageSize=4";
            var listVideo = await ApiService.GetAsync<RootObject<ArticleModel>>(urlVideo);

            string urlMenu = _appSetting.UrlApi + "api/article/menu/get?MenuIDs=5&GetChild=1&Status=1";
            var listMenu = await ApiService.GetAsync<RootObject<MenuModel>>(urlMenu);

            string urlAdv = _appSetting.UrlApi + "api/advert/get?position=5";
            var dataAdv = await ApiService.GetAsync<RootObject<AdvertModel>>(urlAdv);

            var data = new HomeViewModel
            {
                ListArticle = listArt.Items,
                ListVideo = listVideo.Items,
                ListMenu = listMenu.Items,
                Advert = dataAdv.Items != null && dataAdv.Items.Count > 0 ? dataAdv.Items[0] : null
            };
            ViewBag.UrlRoot = _appSetting.UrlRoot;

            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult MenuBar()
        {
            return PartialView();
        }
        #endregion 

        public IActionResult ArticlePartial()
        {
            return PartialView();
        }

        public IActionResult MenuArticleHome(string UrlRewrite)
        {
            return PartialView();
        }
    }
}
