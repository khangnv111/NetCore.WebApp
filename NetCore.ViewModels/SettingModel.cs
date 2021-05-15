using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.ViewModels
{
    public class ConnectionString
    {
        public string DataConnection { get; set; }
    }

    public class AppSetting
    {
        public string UrlRoot { get; set; }
        public string UrlApi { get; set; }
        public string UrlWeb { get; set; }
        public string JwtKey { get; set; }
        public int TokenExpire { get; set; }
    }
}
