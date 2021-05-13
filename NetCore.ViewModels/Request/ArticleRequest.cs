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
        public int MenuID { get; set; } = 0;
        public string MenuIDs { get; set; }
        public string UrlRedirect { get; set; }
        public string Tags { get; set; }
        public int isHot { get; set; } = -1;
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int GetChild { get; set; }
        public int Status { get; set; } = -1;
        public int ImageID { get; set; }
        public string FromDate { get;set; }
        public string ToDate { get; set; }
    }
}
