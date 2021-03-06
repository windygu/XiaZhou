﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;

using EyouSoft.Model.SourceStructure;
using EyouSoft.Model.EnumType.PlanStructure;
using EyouSoft.BLL.SourceStructure;
using EyouSoft.Common.Page;
using EyouSoft.Model.EnumType.PrivsStructure;
using EyouSoft.BLL.ComStructure;

namespace Web.ResourceCenter.LoosePulley
{
    /// <summary>
    /// 游轮预控-列表-
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-7
    public partial class List : BackPage
    {
        protected string YKQRDPrintUri = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();
            //初始化
            DataInit();

        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            BComSetting comSettingBll = new BComSetting();
            YKQRDPrintUri = comSettingBll.GetPrintUri(CurrentUserCompanyID, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.游轮预控确认单);
            #region 分页参数
            int pageSize = 20;
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int recordCount = 0;
            #endregion
            #region 查询实体
            MSourceSueShipSearch queryModel = new MSourceSueShipSearch();
            queryModel.CompanyId = CurrentUserCompanyID;
            //游船公司
            queryModel.ShipCompany = Utils.InputText(Utils.GetQueryStringValue("loosePulleyCor"));
            //游轮名称
            queryModel.SourceName = Utils.InputText(Utils.GetQueryStringValue("loosePulleyName"));
            queryModel.StartTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("periodStartTime"));
            queryModel.EndTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("periodEndTime"));
            #endregion

            //预控BLL 
            IList<MSourceSueShip> ls = new BSourceControl().GetListSueShip(pageIndex, pageSize, ref recordCount, queryModel);
            if (ls != null && ls.Count > 0)
            {
                pan_Msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                BindPage(pageSize, pageIndex, recordCount);
            }
            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && recordCount > pageSize;

        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage(int pageSize, int pageIndex, int recordCount)
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(Privs.资源预控_游船预控_栏目))
            {
                Utils.ResponseNoPermit(Privs.资源预控_游船预控_栏目, true);
                return;
            }
            pan_Add.Visible = CheckGrant(Privs.资源预控_游船预控_新增);
        }

        #endregion
    }
}
