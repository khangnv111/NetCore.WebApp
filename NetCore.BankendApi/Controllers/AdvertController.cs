using Lib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCore.BankendApi.DataAccess;
using NetCore.BankendApi.Service;
using NetCore.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.BankendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {
        private readonly AdvertAccess _advertAccess;
        private readonly AppSetting _appSetting;
        private readonly JwtAuth _jwtAuth;
        public AdvertController(AdvertAccess advertAccess, IOptions<AppSetting> appSetting, JwtAuth jwtAuth)
        {
            _advertAccess = advertAccess;
            _appSetting = appSetting.Value;
            _jwtAuth = jwtAuth;
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetList([FromQuery] AdvertModel request)
        {
            var list = _advertAccess.SP_Advert_GetList(request.Position, request.Type, request.Status);
            return Ok(new { TotalRow = list.Count, Items = list });
        }

        [HttpPost("cms/insert-update")]
        [Authorize]
        public async Task<IActionResult> InsertUpdate([FromForm] AdvertModel data)
        {
            var file = data.fileUpload;

            if (file != null && file.Length > 0)
            {
                string url = _appSetting.UrlWeb + "api/web/save-file";

                var resss = await ApiService.PostAsyncWithFile<RootObject<dynamic>>(url, JsonConvert.SerializeObject(data), file);
                var link = resss.link;
                data.Image = link;
            }

            var res = _advertAccess.SP_Advert_Insert(data);

            if (res > 0)
            {
                return Ok("Thành công");
            }
            else
                return BadRequest("Không thành công");
        }

        [HttpGet]
        [Route("pos/get-list")]
        public IActionResult GetPositionList()
        {
            var list = _advertAccess.SP_AdvertPosition_Get();
            return Ok(new { TotalRow = list.Count, Items = list });
        }
    }
}
