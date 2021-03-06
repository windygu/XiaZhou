﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.TourStructure;
using EyouSoft.Common.Page;
using EyouSoft.Model.EnumType.TourStructure;

namespace Web.TeamCenter
{
    /// <summary>
    /// 派团计划
    /// 创建人：田想兵 创建日期：2011.9.13
    /// </summary>
    public partial class TeamplanList : BackPage
    {

        #region 分页参数
        /// <summary>
        /// 每页显示条数(只读)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        /// 需要在程序中改变则去掉readonly修饰
        private readonly int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 100;
        #endregion

        /// <summary>
        /// 页面类型1.组团2.地接团队3.出境团队
        /// </summary>
        protected int type = 0;
        /// <summary>
        /// 二级栏目编号
        /// </summary>
        protected int sl = 0;

        /// <summary>
        /// 组团打印单链接
        /// </summary>
        protected string PrintPageZt = string.Empty;
        /// <summary>
        /// 客户名单打印链接
        /// </summary>
        protected string PrintPageYY = string.Empty;
        /// <summary>
        /// 修改，派团计调，垫付申请，复制
        /// </summary>
        protected string ListPower = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            type = Utils.GetInt(Utils.GetQueryStringValue("type"));
            sl = Utils.GetInt(Utils.GetQueryStringValue("sl"));

            #region 获得组团打印单链接
            PrintPageZt = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.团队行程单);
            #endregion
            PrintPageYY = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.游客名单);
            #region

            #endregion

            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (doType != "")
            {
                Response.Clear();
                string ids = Utils.GetQueryStringValue("ids");
                switch (doType)
                {
                    case "delete":
                        Response.Write(DeleteData(ids));
                        break;
                    case "canel":
                        string remarks = Server.HtmlDecode(Utils.GetQueryStringValue("remarks"));
                        Response.Write(CanelData(ids, remarks));
                        break;
                }
                Response.End();
            }
            #endregion
            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                //初始化
                DataInit();
            }
        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            #region 获取查询条件
            //线路区域
            int areaId = Utils.GetInt(Utils.GetQueryStringValue("ddlArea"));
            //线路名称
            string routeName = Utils.GetQueryStringValue("txtRouteName");
            //出发时间
            DateTime? txtBeginDateF = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtBeginDateF"));
            DateTime? txtBeginDateS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtBeginDateS"));
            //回团时间
            DateTime? txtEndDateF = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndDateF"));
            DateTime? txtEndDateS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndDateS"));

            //客户单位
            string comId = Utils.GetQueryStringValue(this.CustomerUnitSelect1.ClientNameKHBH);
            string comName = Utils.GetQueryStringValue(this.CustomerUnitSelect1.ClientNameKHMC);

            this.CustomerUnitSelect1.CustomerUnitId = comId;
            this.CustomerUnitSelect1.CustomerUnitName = comName;
            //销售员
            string sellerId = Utils.GetQueryStringValue(this.SellsSelect1.SellsIDClient);
            string sellerName = Utils.GetQueryStringValue(this.SellsSelect1.SellsNameClient);

            this.SellsSelect1.SellsID = sellerId;
            this.SellsSelect1.SellsName = sellerName;

            //计调员
            string opterId = Utils.GetQueryStringValue(this.SellsSelect2.SellsIDClient);
            string opterName = Utils.GetQueryStringValue(this.SellsSelect2.SellsNameClient);

            this.SellsSelect2.SellsID = opterId;
            this.SellsSelect2.SellsName = opterName;

            //团队状态
            string tourState = Utils.GetQueryStringValue("sltTourState");
            #endregion
            BTour bll = new BTour();
            MTourTeamSearch searchModel = new MTourTeamSearch();
            searchModel.AreaId = areaId;
            searchModel.BuyCompanyName = comName;
            searchModel.LDateStart = txtBeginDateF;
            searchModel.LDateEnd = txtBeginDateS;
            searchModel.Planer = opterName;
            searchModel.RDateStart = txtEndDateF;
            searchModel.RDateEnd = txtEndDateS;
            searchModel.RouteName = routeName;
            searchModel.SellerName = sellerName;
            if (tourState != "")
            {
                searchModel.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)Utils.GetInt(tourState);
            }
            searchModel.OperatorId = txtFaBuRen.SellsID = Utils.GetQueryStringValue(this.txtFaBuRen.SellsIDClient);
            searchModel.OperatorName = txtFaBuRen.SellsName = Utils.GetQueryStringValue(this.txtFaBuRen.SellsNameClient);
            searchModel.TourCode = Utils.GetQueryStringValue("txtTourCode");

            IList<MTourTeamInfo> list = bll.GetTourTeamList(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, (EyouSoft.Model.EnumType.TourStructure.ModuleType)(type - 1), searchModel);
            if (list != null && list.Count > 0)
            {
                rpt_List.DataSource = list;
                rpt_List.DataBind();
                //绑定分页
                BindPage();
                this.litMsg.Visible = false;
            }
            else
            {
                this.litMsg.Visible = true;
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;
            }

        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;


            this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect2.intPageSize = pageSize;
            this.ExporPageInfoSelect2.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect2.intRecordCount = recordCount;
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="ids">删除ID</param>
        /// <returns></returns>
        private string DeleteData(string ids)
        {
            string[] id = ids.Split(',');
            //删除操作
            bool result = false;
            if (id.Length > 0)
            {
                BTour bll = new BTour();

                result = bll.DeleteTour(SiteUserInfo.CompanyId, id);

            }
            if (result)
            {
                return UtilsCommons.AjaxReturnJson("1", "删除成功!");
            }
            else
            {
                return UtilsCommons.AjaxReturnJson("0", "删除失败!");
            }
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            switch (type)
            {
                case 1:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_栏目))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_栏目, true);
                        return;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_新增))
                    {
                        this.phForAdd.Visible = false;
                        this.phForCopy.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_删除))
                    {
                        this.phForDelete.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_修改))
                    {
                        this.phForUpdate.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_派团给计调))
                    {
                        this.phForOper.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_取消))
                    {
                        this.phForCanel.Visible = false;
                    }
                    ListPower = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_修改).ToString().ToLower() + ",";
                    ListPower += CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_派团给计调).ToString().ToLower() + ",";
                    ListPower += "true,";
                    ListPower += CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_新增).ToString().ToLower();
                    break;
                case 2:

                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_派团计划_栏目))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_派团计划_栏目, true);
                        return;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_派团计划_新增))
                    {
                        this.phForAdd.Visible = false;
                        this.phForCopy.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_派团计划_删除))
                    {
                        this.phForDelete.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_派团计划_修改))
                    {
                        this.phForUpdate.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_派团计划_派团给计调))
                    {
                        this.phForOper.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_派团计划_取消))
                    {
                        this.phForCanel.Visible = false;
                    }
                    ListPower = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_修改).ToString().ToLower() + ",";
                    ListPower += CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_派团给计调).ToString().ToLower() + ",";
                    ListPower += "true,";
                    ListPower += CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_新增).ToString().ToLower();
                    break;
                case 3:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_派团计划_栏目))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_派团计划_栏目, true);
                        return;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_派团计划_新增))
                    {
                        this.phForAdd.Visible = false;
                        this.phForCopy.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_派团计划_删除))
                    {
                        this.phForDelete.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_派团计划_修改))
                    {
                        this.phForUpdate.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_派团计划_派团给计调))
                    {
                        this.phForOper.Visible = false;
                    }
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_派团计划_取消))
                    {
                        this.phForCanel.Visible = false;
                    }
                    ListPower = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_修改).ToString().ToLower() + ",";
                    ListPower += CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_派团给计调).ToString().ToLower() + ",";
                    ListPower += "true,";
                    ListPower += CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_派团计划_新增).ToString().ToLower();
                    break;
            }

        }
        private string CanelData(string ids, string canelRemarks)
        {
            string[] id = ids.Split(',');

            if (id == null || id.Length == 0) return UtilsCommons.AjaxReturnJson("0", "操作失败：未选择要取消的计划");

            if (id.Length > 1) return UtilsCommons.AjaxReturnJson("0", "操作失败：一次只能取消一个计划");

            BTour bll = new BTour();
            bool bllRetCode = bll.CancelTour(id, canelRemarks, SiteUserInfo.CompanyId);

            if (bllRetCode) return UtilsCommons.AjaxReturnJson("1", "操作成功!");
            else return UtilsCommons.AjaxReturnJson("0", "操作失败!");
        }

        /// <summary>
        /// 根据变更信息显示
        /// </summary>
        /// <param name="isChange"></param>
        /// <param name="isSure"></param>
        /// <param name="tourId"></param>
        /// <returns></returns>
        protected string GetChangeInfo(bool isChange, bool isSure, string tourId, string tourStatus)
        {
            string str = string.Empty;
            if (isChange)
            {
                str = "<span ><a class='fontred' target='_blank' href='" + PrintPageZt + "?tourid=" + tourId + "'>(变)</a></span>";
            }
            if (isSure)
            {
                str = "<span ><a class='fontgreen' target='_blank' href='" + PrintPageZt + "?tourid=" + tourId + "'>(变)</a></span>";
            }
            if (tourStatus == "已取消")
            {
                str = "";
            }
            return str;
        }

        #endregion

        /// <summary>
        /// 传递订单号给客户名单打印单
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns></returns>
        protected string GetPrintUrl(object orderId)
        {

            if (orderId != null && orderId.ToString().Trim() != "")
            {
                return PrintPageYY + "?orderId=" + orderId.ToString();
            }
            else
            {
                return "javascript:void(0);";
            }
        }


    }
}
