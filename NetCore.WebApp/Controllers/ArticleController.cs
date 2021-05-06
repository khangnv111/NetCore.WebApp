using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        private readonly ArticleAccess _articleAccess;
        private readonly AppSetting _appSetting;
        public ArticleController(ArticleAccess articleAccess, IOptions<AppSetting> appSetting)
        {
            _articleAccess = articleAccess;
            _appSetting = appSetting.Value;
        }
        public IActionResult ArticleHomePartial(int CateId)
        {
            int Total = 0;
            var lst = new List<ArticleModel>();
            var article = new ArticleModel();
            if (CateId == -1)
            {
                lst = _articleAccess.SP_Article_GetList_Web(10, 0, "", 5, "", "", -1, 1, 4, out Total);
            }
            else
            {
                lst = _articleAccess.SP_Article_GetList_Web(10, 0, "", CateId, "", "", -1, 1, 4, out Total);
            }

            ViewBag.UrlRoot = _appSetting.UrlRoot;

            return PartialView(lst);
        }
    }
}
