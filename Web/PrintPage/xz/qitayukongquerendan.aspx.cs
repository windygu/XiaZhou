﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Common;
using EyouSoft.Model.SourceStructure;
using EyouSoft.BLL.SourceStructure;
using EyouSoft.Model.EnumType.ComStructure;
using System.Collections.Generic;

namespace EyouSoft.Web.PrintPage.xz
{
    public partial class qitayukongquerendan : System.Web.UI.Page
    {
        protected MUserInfo SiteUserInfo = null;
        protected string ProviderToMoney = "zh-cn";
        protected void Page_Load(object sender, EventArgs e)
        {
            string sueId = Utils.GetQueryStringValue("sueId");
            bool _IsLogin = EyouSoft.Security.Membership.UserProvider.IsLogin(out SiteUserInfo);
            if (!string.IsNullOrEmpty(sueId) && SiteUserInfo != null)
            {
                InitPage(sueId);
            }
            this.Page.Title = PrintTemplateType.其他预控确认单.ToString();
        }

        protected void InitPage(string sueId)
        {
            IList<MSueUse> ls = null;
            IList<MSueUse> newls = null;
            int recordCount = 0;
            ls = new BSourceControl().GetOtherUseList(this.SiteUserInfo.CompanyId, sueId, 1, 1, ref recordCount);
            if (recordCount > 0)
            {
                newls = new BSourceControl().GetOtherUseList(this.SiteUserInfo.CompanyId, sueId, 1, recordCount, ref recordCount);
            }
            if (newls != null && newls.Count > 0)
            {
                this.rpt_tuandui.DataSource = newls;
                this.rpt_tuandui.DataBind();
            }
            else
            {
                this.ph_tuandui.Visible = false;
            }
            var carModel = new EyouSoft.BLL.SourceStructure.BSourceControl().GetModelByOtherId(sueId, this.SiteUserInfo.CompanyId);
            if (carModel != null)
            {
                this.lbCarName.Text = carModel.SourceName;
                this.lbCarType.Text = carModel.TypeName;
                this.lbCarNum.Text = carModel.ControlNum.ToString();
                this.lbPreDate.Text = (carModel.SueStartTime.HasValue ? carModel.SueStartTime.Value.ToString("yyyy-MM-dd") : "") + "-" +
                    (carModel.SueEndTime.HasValue ? carModel.SueEndTime.Value.ToString("yyyy-MM-dd") : "");
                this.lbLastDate.Text = carModel.LastTime.HasValue ? carModel.LastTime.Value.ToString("yyyy-MM-dd") : "";
                this.lbCarPrice.Text = UtilsCommons.GetMoneyString(carModel.UnitPrice, ProviderToMoney);
                this.lbTotalPrice.Text = UtilsCommons.GetMoneyString(carModel.TotalPrice, ProviderToMoney);
                this.lbRemark.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(carModel.Remark);
            }
        }

        /// <summary>
        /// 获取导游导游电话或者游客游客电话
        /// </summary>
        /// <param name="list">导游列表</param>
        /// <returns></returns>
        protected string GetGuideOrTouristStr(object guideList)
        {
            try
            {
                IList<MSourceGuide> ls = (IList<MSourceGuide>)guideList;
                if (ls != null && ls.Count > 0)
                {
                    return ls[0].Name + "</td><td align=center>" + ls[0].Mobile;
                }
                return "</td><td align=center>";
            }
            catch
            {
                return "</td><td align=center>";
            }
        }
    }
}
