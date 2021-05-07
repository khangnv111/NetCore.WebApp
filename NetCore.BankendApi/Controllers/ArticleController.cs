using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCore.BankendApi.DataAccess;
using NetCore.ViewModels;
using NetCore.ViewModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.BankendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleAccess _articleAccess;
        private readonly AppSetting _appSetting;
        public ArticleController(ArticleAccess articleAccess, IOptions<AppSetting> appSetting)
        {
            _articleAccess = articleAccess;
            _appSetting = appSetting.Value;
        }

        [HttpGet]
        [Route("get-list")]
        public IActionResult GetList([FromQuery] ArticleRequest request)
        {
            int totalRow = 0;
            var list = _articleAccess.SP_Article_GetList_Web(request.TopRow, request.ArticleID, request.Title, request.MenuID,
                request.UrlRedirect, request.Tags, request.isHot, request.Page, request.PageSize, out totalRow);
            return Ok(new { TotalRow = totalRow , Items = list});
        }
    }
}
