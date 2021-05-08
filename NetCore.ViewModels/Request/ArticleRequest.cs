using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.ViewModels.Request
{
    public class ArticleRequest
    {
        public int TopRow { get; set; }
        public int ArticleID { get; set; }
        public string Title { get; set; }
        public int MenuID { get; set; }
        public string MenuIDs { get; set; }
        public string UrlRedirect { get; set; }
        public string Tags { get; set; }
        public int isHot { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int GetChild { get; set; }
        public int Status { get; set; }
    }
}
