﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace Web.CustomerCenter.AjaxRequest
{
    /// <summary>
    /// 客户管理 欠款记录
    /// 创建者:钱琦
    /// 时间 :2011-10-1
    /// </summary>
    public partial class AjaxOwed : BackPage
    {
        protected int pageSize = 10;
        protected int pageIndex = 0;
        protected int recordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string crmId = Utils.GetQueryStringValue("crmId");
                string type = Utils.GetQueryStringValue("type");
                pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
                BindData(crmId, type);
            }
        }

        private void BindData(string crmId, string type)
        {
            EyouSoft.BLL.CrmStructure.BCrm crmBll = new EyouSoft.BLL.CrmStructure.BCrm();
            IList<EyouSoft.Model.CrmStructure.MOwedRecord> list = crmBll.GetOwedRecordList(base.SiteUserInfo.CompanyId, crmId, pageIndex, pageSize, ref recordCount);
            if (list != null && list.Count > 0)
            {
                rptQKXXList.DataSource = list;
                rptQKXXList.DataBind();
                BindPage(type);
            }
            else
            {
                this.rptQKXXList.Controls.Add(new Label() { Text = "<tr><td colspan='6' align='center'>未找到相关数据!</td></tr>" });
                ExporPageInfoSelect1.Visible = false;
            }
        }


        protected void BindPage(string type)
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
            if (type == "same")
            {
                this.ExporPageInfoSelect1.PageLinkURL = "#/CustomerCenter/Same/CustomerEdit.aspx?";
            }
            else
            {
                this.ExporPageInfoSelect1.PageLinkURL = "#/CustomerCenter/Unit/CustomerEdit.aspx?";
            }
        }

        
    }
   
}
