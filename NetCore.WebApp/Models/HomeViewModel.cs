using NetCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.WebApp.Models
{
    public class HomeViewModel
    {
        public List<ArticleModel> ListArticle { get; set; }
        public List<ArticleModel> ListVideo { get; set; }
    }
}
