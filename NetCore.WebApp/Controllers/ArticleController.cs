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

        public async Task<IActionResult> NewsDetail(int Id)
        {
            string url = _appSetting.UrlApi + "api/article/get-list?TopRow=1&ArticleID=" + Id + "&isHot=-1&Page=1&PageSize=1";
            var list = await ApiService.GetAsync<RootObject<ArticleModel>>(url);

            var data = list.Items.FirstOrDefault();

            var _data = new ArticleDetail();
            if (data.MenuID == 10)
            {
                string urlImage = _appSetting.UrlApi + "api/article/image/get?TopRow=1000&ArticleID=" + Id + "&Status=1&Page=1&PageSize=1000";
                var listImage = await ApiService.GetAsync<RootObject<ArticleImage>>(urlImage);

                _data.ListImage = listImage.Items;
            }

            if (!String.IsNullOrEmpty(data.Detail))
            {
                data.Detail = data.Detail.Replace("<img ", "<img class=\"img-responsive\"");
            }

            _data.Detail = data;

            return View(_data);
        }

        public async Task<IActionResult> NewsRelation(int Id, int Page)
        {
            string url = _appSetting.UrlApi + "api/article/relation/get?TopRow=10&ArticleID=" + Id + "&Page=1&PageSize=2";
            var list = await ApiService.GetAsync<RootObject<ArticleModel>>(url);
            var _data = new DonateOnlineModel();
            _data.Page = Page;
            _data.Total = list.TotalRow;
            _data.ListArt = list.Items;
            _data.TotalPage = (int)Math.Ceiling((decimal)(list.TotalRow / 2));

            ViewBag.Id = Id;
            ViewBag.UrlRoot = _appSetting.UrlRoot;

            return PartialView(_data);
        }
        #endregion

        #region Video hình ảnh
        public async Task<IActionResult> VideoImage()
        {
            string url = _appSetting.UrlApi + "api/article/get-list?TopRow=1&MenuID=9&isHot=-1&Page=1&PageSize=1";
            var article = await ApiService.GetAsync<RootObject<ArticleModel>>(url);

            string urlVideo = _appSetting.UrlApi + "api/article/get-list?TopRow=4&MenuID=11&isHot=-1&Page=1&PageSize=4";
            var listVideo = await ApiService.GetAsync<RootObject<ArticleModel>>(urlVideo);

            string urlAlbum = _appSetting.UrlApi + "api/article/get-list?TopRow=4&MenuID=10&isHot=-1&Page=1&PageSize=4";
            var listAlbum = await ApiService.GetAsync<RootObject<ArticleModel>>(urlAlbum);

            var data = new NewsPageModel
            {
                ArtHot = article.Items.FirstOrDefault(),
                ListAlbum = listAlbum.Items,
                ListVideo = listVideo.Items
            };

            ViewBag.UrlRoot = _appSetting.UrlRoot;
            return View(data);
        }
        #endregion

        #region Ủng hộ trực tuyến
        public async Task<IActionResult> DonateOnline(int Page = 1, int PageSize = 10)
        {
            string url = _appSetting.UrlApi + "api/article/get-list?TopRow=1000&MenuID=0&UrlRedirect=ung-ho-truc-tuyen&isHot=-1&Page=" + Page + "&PageSize=" + PageSize;
            var list = await ApiService.GetAsync<RootObject<ArticleModel>>(url);

            var data = new DonateOnlineModel
            {
                ListArt = list.Items,
                Page = Page,
                PageSize = PageSize,
                Total = list.TotalRow,
                TotalPage = (int)Math.Ceiling((decimal)(list.TotalRow / PageSize))
            };

            ViewBag.UrlRoot = _appSetting.UrlRoot;
            return View(data);
        }

        public async Task<IActionResult> DonateOnlinePart(int Page = 1, int PageSize = 10)
        {
            string url = _appSetting.UrlApi + "api/article/get-list?TopRow=1000&MenuID=0&UrlRedirect=ung-ho-truc-tuyen&isHot=-1&Page=" + Page + "&PageSize=" + PageSize;
            var list = await ApiService.GetAsync<RootObject<ArticleModel>>(url);

            var data = new DonateOnlineModel
            {
                ListArt = list.Items,
                Page = Page,
                PageSize = PageSize,
                Total = list.TotalRow,
                TotalPage = (int)Math.Ceiling((decimal)(list.TotalRow / PageSize))
            };

            ViewBag.UrlRoot = _appSetting.UrlRoot;
            return View(data);
        }
        #endregion

    }
}
