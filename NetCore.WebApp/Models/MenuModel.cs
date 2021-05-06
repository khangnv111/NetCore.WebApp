using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.WebApp.Models
{
    public class MenuModel
    {
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuDesc { get; set; }
        public int FatherID { get; set; }
        public string FatherName { get; set; }
        public int Status { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string Url { get; set; }
        public string UrlRedirect { get; set; }
        public string UrlFather { get; set; }
        public string UrlRedirectFather { get; set; }
    }
}
