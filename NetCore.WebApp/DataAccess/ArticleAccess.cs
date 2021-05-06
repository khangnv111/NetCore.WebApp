using Lib;
using Lib.Database;
using Microsoft.Extensions.Options;
using NetCore.WebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.WebApp.DataAccess
{
    public class ArticleAccess
    {
        private DBHelper db = null;
        private readonly ConnectionString _myConnect;

        public ArticleAccess(IOptions<ConnectionString> myConnect)
        {
            _myConnect = myConnect.Value;
            db = new DBHelper(_myConnect.DataConnection);
        }

        public List<MenuModel> SP_Menu_GetList(string MenuIDs, Int16 GetChild, Int16 Status, out int TotalRow)
        {
            TotalRow = 0;
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@MenuIDs", MenuIDs);
                pars[1] = new SqlParameter("@GetChild", GetChild);
                pars[2] = new SqlParameter("@Status", Status);
                pars[3] = new SqlParameter("@TotalRow", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var list = db.GetListSP<MenuModel>("SP_Menu_GetList", pars);
                if (list != null && list.Count >= 0)
                {
                    TotalRow = Convert.ToInt32(pars[3].Value);
                }
                NLogLogger.Info(JsonConvert.SerializeObject(list));
                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.Exception(ex);
                return new List<MenuModel>();
            }
        }

        public List<ArticleModel> SP_Article_GetList_Web(int TopRow, int ArticleID, string Title, int MenuID, string UrlRedirect, string Tags, int isHot, int Page, int PageSize, out int TotalRow)
        {
            TotalRow = 0;
            try
            {
                var pars = new SqlParameter[10];
                pars[0] = new SqlParameter("@TopRow", TopRow);
                pars[1] = new SqlParameter("@ArticleID", ArticleID);
                pars[2] = new SqlParameter("@Title", Title);
                pars[3] = new SqlParameter("@MenuID", MenuID);
                pars[4] = new SqlParameter("@UrlRedirect", UrlRedirect);
                pars[5] = new SqlParameter("@Tags", Tags);
                if (isHot == -1)
                    pars[6] = new SqlParameter("@isHot", DBNull.Value);
                else if (isHot == 1)
                    pars[6] = new SqlParameter("@isHot", true);
                else if (isHot == 0)
                    pars[6] = new SqlParameter("@isHot", false);
                pars[7] = new SqlParameter("@Page", Page);
                pars[8] = new SqlParameter("@PageSize", PageSize);
                pars[9] = new SqlParameter("@TotalRow", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var list = db.GetListSP<ArticleModel>("SP_Article_GetList_Web", pars);
                if (list != null || list.Count >= 0)
                {
                    TotalRow = Convert.ToInt32(pars[9].Value);
                }
                NLogLogger.Info(JsonConvert.SerializeObject(list));
                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.Exception(ex);
                return new List<ArticleModel>();
            }
        }
    }
}
