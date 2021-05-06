using Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCore.WebApp.DataAccess;
using NetCore.WebApp.Models;
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
        private readonly ArticleAccess _articleAccess;

        public HomeController(ILogger<HomeController> logger, ArticleAccess articleAccess)
        {
            _logger = logger;
            _articleAccess = articleAccess;
        }

        #region Home
        public IActionResult Index()
        {
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
            NLogLogger.Info("ArticlePartial.....");
            return PartialView();
        }

        public IActionResult MenuArticleHome(string UrlRewrite)
        {
            NLogLogger.Info("MenuArticleHome.....");
            string MenuId = "5";//Lấy cate con của Cate tin tức
            if (UrlRewrite == "van-ban-phap-ly" || UrlRewrite == "cau-hoi-thuong-gap")
                MenuId = "1";//Lấy cate con của Cate giới thiệu

            //Album / Video
            if (UrlRewrite == "album-anh" || UrlRewrite == "video" || UrlRewrite == "video-anh")
                MenuId = "9";//Lấy cate con của Cate giới thiệu

            int Total = 0;
            var list = _articleAccess.SP_Menu_GetList(MenuId, 1, 1, out Total);

            return PartialView(list);
        }
    }
}
