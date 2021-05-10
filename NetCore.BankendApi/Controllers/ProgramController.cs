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
    public class ProgramController : ControllerBase
    {
        private readonly AppSetting _appSetting;
        private readonly ProgramAccess _programAccess;
        public ProgramController(IOptions<AppSetting> appSetting, ProgramAccess programAccess)
        {
            _appSetting = appSetting.Value;
            _programAccess = programAccess;
        }

        [HttpGet]
        [Route("get")]
        public IActionResult ProgramGet([FromQuery] ProgramRequest request)
        {
            int totalRow = 0;
            var list = _programAccess.SP_Event_GetList_Web(request.EventID, request.EventName, request.IsSetted, request.Page, request.PageSize, out totalRow);
            return Ok(new { TotalRow = totalRow, Items = list });
        }
    }
}
