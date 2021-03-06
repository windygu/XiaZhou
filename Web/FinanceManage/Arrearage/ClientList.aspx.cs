﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Model.FinStructure;
using EyouSoft.Common.Page;
using EyouSoft.Model.EnumType.PrivsStructure;
using EyouSoft.Model.EnumType.FinStructure;
using EyouSoft.Model.CrmStructure;
using System.Text;

namespace Web.FinanceManage.Arrearage
{
    /// <summary>
    /// 客户欠款预警-列表-
    /// </summary>
    /// 创建人;柴逸宁
    /// 创建时间：2011-9-13
    public partial class ClientList : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();
            //if (UtilsCommons.IsToXls())
            //{
            //    ToXls();
            //}
            //初始化
            DataInit();

        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            #region 分页参数
            int pageSize = 20;
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int recordCount = 0;
            #endregion
            txt_SellsSelect.SetTitle = "选择 销售员";
            txt_SellsSelect.SellsName = Utils.GetQueryStringValue(txt_SellsSelect.SellsNameClient);
            txt_SellsSelect.SellsID = Utils.GetQueryStringValue(txt_SellsSelect.SellsIDClient);
            MWarningBase model = new MWarningBase();
            model.CrmId = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHBH);
            model.Customer = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHMC);
            model.DeptId = SiteUserInfo.DeptId;
            model.SellerId = Utils.GetQueryStringValue(txt_SellsSelect.SellsIDClient);
            model.SellerName = Utils.GetQueryStringValue(txt_SellsSelect.SellsNameClient);
            model.SignArrear = (EqualSign?)Utils.GetEnumValueNull(typeof(EqualSign), Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperator));
            model.Arrear = Utils.GetDecimalNull(Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperatorNumber));            
            model.SignTransfinite = (EqualSign?)Utils.GetEnumValueNull(typeof(EqualSign), Utils.GetQueryStringValue(CaiWuShaiXuan2.ClientUniqueIDOperator));
            model.Transfinite = Utils.GetDecimalNull(Utils.GetQueryStringValue(CaiWuShaiXuan2.ClientUniqueIDOperatorNumber));

            IList<MCustomerWarning> ls = new BFinance().GetCustomerWarningLst(
                  pageSize,
                  pageIndex,
                  ref recordCount,
                  CurrentUserCompanyID,
                  model);
            if (ls != null && ls.Count > 0)
            {
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                //绑定分页
                BindPage(pageSize, pageIndex, recordCount);
                pan_Msg.Visible = false;
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
            if (!CheckGrant(Privs.财务管理_报销报账_栏目))
            {
                Utils.ResponseNoPermit(Privs.财务管理_报销报账_栏目, true);
                return;
            }

        }
        /// <summary>
        /// 联系人泡泡提示
        /// </summary>
        /// <param name="ls"></param>
        /// <returns></returns>
        protected string LinkMan(object list)
        {
            if (list != null)
            {
                IList<MCrmLinkman> ls = (IList<MCrmLinkman>)list;
                if (ls.Count > 0)
                {
                    return "联系人：" + ls[0].Name + "<br />联系方式：" + ls[0].Telephone;
                }
            }
            return "联系人：暂无<br />联系方式：暂无";

        }
        ///// <summary>
        ///// 导出
        ///// </summary>
        //private void ToXls()
        //{
        //    int recordCount = 0;
        //    IList<MCustomerWarning> ls = new BFinance().GetCustomerWarningLst(
        //         UtilsCommons.GetToXlsRecordCount(),
        //         1,
        //         ref recordCount,
        //         CurrentUserCompanyID,
        //         null);
        //    if (ls != null && ls.Count > 0)
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\n",
        //            "客户单位",
        //            "销售员",
        //            "欠款额度",
        //            "拖欠金额",
        //            "超限金额",
        //            "超限时间",
        //            "单团账龄期限",
        //            "最长超时天数",
        //            "超时团数");

        //        foreach (MCustomerWarning item in ls)
        //        {
        //            sb.Append(item.Customer + "\t");
        //            sb.Append(item.SellerName + "\t");
        //            sb.Append(item.SellerName + "\t");
        //            sb.Append(UtilsCommons.GetMoneyString(item.AmountOwed, ProviderToMoney) + "\t");
        //            sb.Append(UtilsCommons.GetMoneyString(item.Arrear, ProviderToMoney) + "\t");
        //            sb.Append(UtilsCommons.GetMoneyString(item.Transfinite, ProviderToMoney) + "\t");
        //            sb.Append(item.Deadline + "\t");
        //            sb.Append((item.DeadDay <= 0 ? 0 : item.DeadDay).ToString() + "\t");
        //            sb.Append(item.TourCount + "\n");

        //        }
        //        ResponseToXls(sb.ToString());
        //    }
        //    ResponseToXls(string.Empty);

        //}
        #endregion
    }
}
