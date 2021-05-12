using Lib;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetCore.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.BankendApi.Service
{
    public class JwtAuth
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly AppSetting _appSetting;
        public JwtAuth(IOptions<AppSetting> appSetting, IHttpContextAccessor httpContextAccessor)
        {
            _appSetting = appSetting.Value;
            _httpContextAccessor = httpContextAccessor;

        }
        public HttpContext Current => _httpContextAccessor.HttpContext;

        public string GenerateToken(UserModel user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSetting.JwtKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.UserID.ToString()),
                        new Claim("UserName", user.Username.ToString())
                    }),
                    Expires = DateTime.Now.AddHours(_appSetting.TokenExpire),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                NLogLogger.Exception(ex);
                return null;
            }
        }

        public int UserID
        {
            get
            {
                try
                {
                    int userId = 0;
                    if (Current.User.Identity.IsAuthenticated)
                    {
                        string val = Current.User.FindFirst("UserID").Value;
                        userId = Int32.Parse(val);
                    }
                    return userId;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public string UserName
        {
            get
            {
                string val = "";
                try
                {
                    if (Current.User.Identity.IsAuthenticated)
                    {
                        val = Current.User.FindFirst("UserName").Value;
                    }
                    return val;
                }
                catch
                {
                    return val;
                }
            }
        }
    }
}
