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
    public class ProgramAccess
    {
        private DBHelper db = null;
        private readonly ConnectionString _myConnect;

        public ProgramAccess(IOptions<ConnectionString> myConnect)
        {
            _myConnect = myConnect.Value;
            db = new DBHelper(_myConnect.DataConnection);
        }

        public List<ProgramModel> SP_Event_GetList_Web(int EventID, string EventName, int IsSetted, int Page, int PageSize, out int TotalRow)
        {
            TotalRow = 0;
            try
            {
                var pars = new SqlParameter[6];
                pars[0] = new SqlParameter("@EventID", EventID);
                pars[1] = new SqlParameter("@EventName", EventName);
                if (IsSetted == -1)
                    pars[2] = new SqlParameter("@IsSetted", DBNull.Value);
                else if (IsSetted == 1)
                    pars[2] = new SqlParameter("@IsSetted", true);
                pars[3] = new SqlParameter("@Page", Page);
                pars[4] = new SqlParameter("@PageSize", PageSize);
                pars[5] = new SqlParameter("@TotalRow", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var list = db.GetListSP<ProgramModel>("SP_Event_GetList_Web", pars);
                if (list != null || list.Count >= 0)
                {
                    TotalRow = Convert.ToInt32(pars[5].Value);
                }
                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.Exception(ex);
                return new List<ProgramModel>();
            }
        }
    }
}
