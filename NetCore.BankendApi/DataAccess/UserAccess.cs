using Lib;
using Lib.Database;
using Microsoft.Extensions.Options;
using NetCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.BankendApi.DataAccess
{
    public class UserAccess
    {
        private DBHelper db = null;
        private readonly ConnectionString _myConnect;
        public UserAccess(IOptions<ConnectionString> myConnect)
        {
            _myConnect = myConnect.Value;
            db = new DBHelper(_myConnect.DataConnection);
        }

        /// <summary>
        /// Xác thực người dùng
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="isSucess"></param>
        /// <returns></returns>
        public int sp_User_Authenticate(string username, string password, string IpAddress)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_Username", username);
                pars[1] = new SqlParameter("@_Password", password);
                pars[2] = new SqlParameter("@_ClientIP", IpAddress);
                pars[3] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                db.ExecuteNonQuerySP("sp_User_Authenticate", pars);

                return Convert.ToInt32(pars[3].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.Exception(ex);
                return -99;
            }
        }

        /// <summary>
        /// Get User theo Username
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public UserModel SP_User_GetByCondition(string Username)
        {
            try
            {
                return db.GetInstanceSP<UserModel>("SP_User_GetByCondition", new SqlParameter("@_Username", Username));
            }
            catch (Exception ex)
            {
                NLogLogger.Exception(ex);
                return new UserModel();
            }
        }
    }
}
