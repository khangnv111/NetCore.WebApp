using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCore.BankendApi.DataAccess;
using NetCore.BankendApi.Service;
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
        private readonly JwtAuth _jwtAuth;
        public ArticleController(ArticleAccess articleAccess, IOptions<AppSetting> appSetting, JwtAuth jwtAuth)
        {
            _articleAccess = articleAccess;
            _appSetting = appSetting.Value;
            _jwtAuth = jwtAuth;
        }

        #region Web
        [HttpGet]
        [Route("menu/get")]
        public IActionResult MenuGetList([FromQuery] ArticleRequest request)
        {
            int totalRow = 0;
            var list = _articleAccess.SP_Menu_GetList(request.MenuIDs, Convert.ToInt16(request.GetChild), Convert.ToInt16(request.Status), out totalRow);
            return Ok(new { TotalRow = totalRow, Items = list });
        }

        [HttpGet]
        [Route("get-list")]
        public IActionResult GetList([FromQuery] ArticleRequest request)
        {
            int totalRow = 0;
            var list = _articleAccess.SP_Article_GetList_Web(request.TopRow, request.ArticleID, request.Title, request.MenuID,
                request.UrlRedirect, request.Tags, request.isHot, request.Page, request.PageSize, out totalRow);
            return Ok(new { TotalRow = totalRow, Items = list });
        }

        [HttpGet]
        [Route("image/get")]
        public IActionResult GetImageList([FromQuery] ArticleRequest req)
        {
            int totalRow = 0;
            var list = _articleAccess.SP_ArticleImage_GetList(req.TopRow, req.ImageID, req.ArticleID, req.Status, req.Page, req.PageSize, out totalRow);
            return Ok(new { TotalRow = totalRow, Items = list });
        }

        [HttpGet]
        [Route("relation/get")]
        public IActionResult GetArticleRelationList([FromQuery] ArticleRequest request)
        {
            int totalRow = 0;
            var list = _articleAccess.SP_Article_GetListSameMenu_Web(request.TopRow, request.ArticleID, request.Page, request.PageSize, out totalRow);
            return Ok(new { TotalRow = totalRow, Items = list });
        }
        #endregion

        #region CMS
        [HttpGet]
        [Authorize]
        [Route("cms/get")]
        public IActionResult CMSGetList([FromQuery] ArticleRequest data)
        {
            int totalRow = 0;
            var list = _articleAccess.SP_Article_GetList_CMS(data.ArticleID, data.Title, data.MenuID, data.Tags, data.isHot, data.Status, data.FromDate, data.ToDate, data.Page, data.PageSize, out totalRow);
            return Ok(new { TotalRow = totalRow, Items = list });
        }

        [HttpPost("cms/insert-update")]
        [Authorize]
        public IActionResult InsertUpdate([FromBody] ArticleModel data)
        {
            data.CreateUser = _jwtAuth.UserName;
            var res = _articleAccess.SP_Article_INUP_CMS(data);

            if(res > 0)
            {
                return Ok("Thành công");
            }
            else if (res == -55)
            {
                return BadRequest("Đã có 5 bài hot");
            }
            else if (res == -600)
            {
                return BadRequest("Đầu vào không hợp lệ");
            }
            else
                return BadRequest("Không thành công");
        }
        #endregion
    }
}
