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
    public class ArticleAccess
    {
        private DBHelper db = null;
        private readonly ConnectionString _myConnect;

        public ArticleAccess(IOptions<ConnectionString> myConnect)
        {
            _myConnect = myConnect.Value;
            db = new DBHelper(_myConnect.DataConnection);
        }

        #region Web
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

        public List<ArticleImage> SP_ArticleImage_GetList(int TopRow, int ImageID, int ArticleID, int Status, int Page, int PageSize, out int TotalRow)
        {
            try
            {
                var pars = new SqlParameter[7];
                pars[0] = new SqlParameter("@TopRow", TopRow);
                pars[1] = new SqlParameter("@ImageID", ImageID);
                pars[2] = new SqlParameter("@ArticleID", ArticleID);
                pars[3] = new SqlParameter("@Status", Status);
                pars[4] = new SqlParameter("@Page", Page);
                pars[5] = new SqlParameter("@PageSize", PageSize);
                pars[6] = new SqlParameter("@TotalRow", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var list = db.GetListSP<ArticleImage>("SP_ArticleImage_GetList", pars);
                TotalRow = Convert.ToInt32(pars[6].Value);
                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.Exception(ex);
                TotalRow = 0;
                return new List<ArticleImage>();
            }

        }

        public List<ArticleModel> SP_Article_GetListSameMenu_Web(int TopRow, int ArticleID, int Page, int PageSize, out int TotalRow)
        {
            TotalRow = 0;
            try
            {
                var pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@TopRow", TopRow);
                pars[1] = new SqlParameter("@ArticleID", ArticleID);
                pars[2] = new SqlParameter("@Page", Page);
                pars[3] = new SqlParameter("@PageSize", PageSize);
                pars[4] = new SqlParameter("@TotalRow", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var list = db.GetListSP<ArticleModel>("SP_Article_GetListSameMenu_Web", pars);

                if (list != null || list.Count >= 0)
                {
                    TotalRow = Convert.ToInt32(pars[4].Value);
                }
                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.Exception(ex);
                return new List<ArticleModel>();
            }
        }
        #endregion

        #region CMS
        public List<ArticleModel> SP_Article_GetList_CMS(int ArticleID, string Title, int MenuID, string Tags, int isHot, int Status, string FromDate, string ToDate, int Page, int PageSize, out int TotalRow)
        {
            try
            {
                var pars = new SqlParameter[11];
                pars[0] = new SqlParameter("@ArticleID", ArticleID);
                pars[1] = new SqlParameter("@Title", Title);
                pars[2] = new SqlParameter("@MenuID", MenuID);
                pars[3] = new SqlParameter("@Tags", Tags);
                if (isHot == -1)
                {
                    pars[4] = new SqlParameter("@isHot", DBNull.Value);
                }
                else if (isHot == 0)
                {
                    pars[4] = new SqlParameter("@isHot", false);
                }
                else
                {
                    pars[4] = new SqlParameter("@isHot", true);
                }
                pars[5] = new SqlParameter("@Status", Status);
                pars[6] = new SqlParameter("@FromDate", FromDate);
                pars[7] = new SqlParameter("@ToDate", ToDate);
                pars[8] = new SqlParameter("@Page", Page);
                pars[9] = new SqlParameter("@PageSize", PageSize);
                pars[10] = new SqlParameter("@TotalRow", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var list = db.GetListSP<ArticleModel>("SP_Article_GetList_CMS", pars);
                TotalRow = Convert.ToInt32(pars[10].Value);
                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.Exception(ex);
                TotalRow = 0;
                return new List<ArticleModel>();
            }

        }

        public int SP_Article_INUP_CMS(ArticleModel article)
        {
            try
            {
                var pars = new SqlParameter[16];
                pars[0] = new SqlParameter("@ArticleID", article.ArticleID);
                pars[1] = new SqlParameter("@Title", article.Title);
                pars[2] = new SqlParameter("@MenuID", article.MenuID);
                pars[3] = new SqlParameter("@Description", article.Description);
                pars[4] = new SqlParameter("@Detail", article.Detail);
                pars[5] = new SqlParameter("@Image", article.Image);
                pars[6] = new SqlParameter("@ImageDetail", article.ImageDetail);
                pars[7] = new SqlParameter("@ImageHot", article.ImageHot);
                pars[8] = new SqlParameter("@MetaData", article.MetaData);
                pars[9] = new SqlParameter("@BottomDesc", article.BottomDesc);
                pars[10] = new SqlParameter("@Tags", article.Tags);
                pars[11] = new SqlParameter("@PublishDate", article.PublishDate);
                pars[12] = new SqlParameter("@isHot", article.isHot);
                pars[13] = new SqlParameter("@Author", article.Author);
                pars[14] = new SqlParameter("@CreateUser", article.CreateUser);
                pars[15] = new SqlParameter("@ResponseStatus", DbType.Int32) { Direction = ParameterDirection.Output };

                db.ExecuteNonQuerySP("SP_Article_INUP_CMS", pars);
                return Convert.ToInt32(pars[15].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.Exception(ex);
                return -99;
            }
        }
        #endregion
    }
}
