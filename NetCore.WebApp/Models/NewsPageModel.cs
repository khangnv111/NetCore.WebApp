using NetCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.WebApp.Models
{
    public class NewsPageModel
    {
        public ArticleModel ArtHot { get; set; }
        public List<ArticleModel> ListVideo { get; set; }
        public List<ArticleModel> ListAlbum { get; set; }
    }

    public class DonateOnlineModel
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public List<ArticleModel> ListArt { get; set; }
    }

    public class ArticleDetail
    {
        public ArticleModel Detail { get; set; }
        public List<ArticleImage> ListImage { get; set; }
    }
}
