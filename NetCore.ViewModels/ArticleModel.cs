using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.ViewModels
{
    public class ArticleModel
    {
        public int STT { get; set; }
        public int ArticleID { get; set; }
        public string Title { get; set; }
        public int MenuID { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public string Image { get; set; }
        public string ImageDetail { get; set; }
        public string ImageHot { get; set; }
        public string MetaData { get; set; }
        public string Tags { get; set; }
        public DateTime PublishDate { get; set; }
        public bool isHot { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime ConfirmDate { get; set; }
        public string ConfirmUser { get; set; }
        public int CountView { get; set; }
        public string MenuName { get; set; }
        public string BottomDesc { get; set; }
        public int OrderHot { get; set; }

        public string Author { get; set; }
        public string FileName { get; set; }
        public IFormFile fileUpload { get; set; }
    }

    public class ArticleImage
    {
        public int STT { get; set; }
        public int ImageID { get; set; }
        public int ArticleID { get; set; }
        public string ArticleTitle { get; set; }
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public string FilePath { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string Description { get; set; }
    }
}
