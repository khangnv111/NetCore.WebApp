using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.ViewModels
{
    public class AdvertModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int Position { get; set; }
        public string Image { get; set; }
        public string ScriptData { get; set; }
        public int Status { get; set; }
        public IFormFile fileUpload { get; set; }
    }

    public class AdvertPosModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
