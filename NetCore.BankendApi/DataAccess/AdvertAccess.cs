using Lib;
using Lib.Database;
using Microsoft.Extensions.Options;
using NetCore.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.BankendApi.DataAccess
{
    public class AdvertAccess
    {
        private DBHelper db = null;
        private readonly ConnectionString _myConnect;

        public AdvertAccess(IOptions<ConnectionString> myConnect)
        {
            _myConnect = myConnect.Value;
            db = new DBHelper(_myConnect.DataConnection);
        }

        public List<AdvertModel> SP_Advert_GetList(int PostID, int Type, int Status)
        {
            try
            {
                var pars = new SqlParameter[] {
                    new SqlParameter("@_PostID", PostID),
                    new SqlParameter("@_Type", Type),
                    new SqlParameter("@_Status", Status)
                };

                var list = db.GetListSP<AdvertModel>("SP_Advert_GetList", pars);
                NLogLogger.Info(JsonConvert.SerializeObject(list));

                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.Exception(ex);
                return new List<AdvertModel>();
            }
        }

        public int SP_Advert_Insert(AdvertModel data)
        {
            try
            {
                var pars = new SqlParameter[] {
                    new SqlParameter("@_Id", data.Id),
                    new SqlParameter("@_Name", data.Name),
                    new SqlParameter("@_Type", data.Type),
                    new SqlParameter("@_Position", data.Position),
                    new SqlParameter("@_Image", data.Image),
                    new SqlParameter("@_Link", data.Link),
                    new SqlParameter("@_ScriptData", data.ScriptData)
                };
                //pars[15] = new SqlParameter("@ResponseStatus", DbType.Int32) { Direction = ParameterDirection.Output };

                db.ExecuteNonQuerySP("SP_Advert_Insert", pars);
                return 1;
            }
            catch (Exception ex)
            {
                NLogLogger.Exception(ex);
                return -99;
            }
        }

        public int SP_Advert_Delete(int Id)
        {
            try
            {
                var pars = new SqlParameter[] {
                    new SqlParameter("@_ID", Id),
                };

                db.ExecuteNonQuerySP("SP_Advert_Delete", pars);
                return 1;
            }
            catch (Exception ex)
            {
                NLogLogger.Exception(ex);
                return -99;
            }
        }

        public List<AdvertPosModel> SP_AdvertPosition_Get()
        {
            try
            {
                var pars = new SqlParameter[] {
                };

                var list = db.GetListSP<AdvertPosModel>("SP_AdvertPosition_Get", pars);
                NLogLogger.Info(JsonConvert.SerializeObject(list));

                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.Exception(ex);
                return new List<AdvertPosModel>();
            }
        }
    }
}
