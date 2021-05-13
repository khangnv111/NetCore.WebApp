using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore.BankendApi.DataAccess;
using NetCore.BankendApi.Service;
using NetCore.ViewModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.BankendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserAccess _userAccess;
        private readonly JwtAuth _jwtAuth;
        public UserController(UserAccess userAccess, JwtAuth jwtAuth)
        {
            _userAccess = userAccess;
            _jwtAuth = jwtAuth;
        }

        #region Login
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] UserRequest data)
        {
            if (string.IsNullOrEmpty(data.Username) || string.IsNullOrEmpty(data.Password))
            {
                return BadRequest("Dữ liệu không được bỏ trống");
            }
            var check = _userAccess.sp_User_Authenticate(data.Username, data.Password, ipAddress());

            if (check == -49)
                return BadRequest("Tài khoản của bạn đã bị block");
            if (check == -50)
                return BadRequest("Tài khoản của bạn chưa được cấp quyền");
            if (check == -53)
                return BadRequest("Mật khẩu không chính xác");

            var _user = _userAccess.SP_User_GetByCondition(data.Username);

            string token = _jwtAuth.GenerateToken(_user);


            return Ok(new { UserName = data.Username, TokenKey = token });
        }
        #endregion

        [HttpGet]
        [Authorize]
        public IActionResult GetListRole()
        {
            string[] role = { "admin", "user", "table", "form", "list", "profile", "system" };
            return Ok(role);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
