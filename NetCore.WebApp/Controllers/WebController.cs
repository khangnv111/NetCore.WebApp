using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NetCore.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebController : ControllerBase
    {
        private readonly AppSetting _appSetting;
        public WebController(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value;
        }

        [HttpPost]
        [Route("save-file")]
        public IActionResult SaveImage(ArticleModel data)
        {
            var folderName = Path.Combine("wwwroot", "media");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var imagePath = pathToSave + "/" + data.FileName;

            byte[] bytes = Convert.FromBase64String(data.Image.Split(',')[1]);
            using (FileStream stream = new FileStream(imagePath, FileMode.Create))
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
            string link = _appSetting.UrlRoot + "media/" + data.FileName;

            return Ok(new { link = link});
        }
    }
}
