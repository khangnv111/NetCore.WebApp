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
            string url = _appSetting.UrlApi + "api/article/menu/get";
            var items = await ApiService.GetAsync<Rootobject<ArticleModel>>(url);
            NLogLogger.Info(JsonConvert.SerializeObject(items));

            return View();
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
