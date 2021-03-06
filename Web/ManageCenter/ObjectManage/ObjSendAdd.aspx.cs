﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using EyouSoft.BLL.GovStructure;
using EyouSoft.Model.GovStructure;
using System.Text;

namespace Web.ManageCenter.ObjectManage
{
    /// <summary>
    /// 行政中心-物品管理-物品发放
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-04-01
    public partial class ObjSendAdd : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            #region 处理AJAX请求
            //获取ajax请求
            string save = Utils.GetQueryStringValue("save");
            string id = Utils.GetQueryStringValue("id");
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (save == "save")
            {
                PageSave();
            }
            #endregion

            if (!IsPostBack)
            {
                //根据ID初始化页面
                PageInit(id);
            }
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id)
        {
            string iframeId = Utils.GetQueryStringValue("iframeId");
            this.HrSelect1.ParentIframeID = iframeId;
            this.HrSelect1.SetTitle = "签收人";
            this.HrSelect1.SModel = "1";
            this.SelectSection1.ParentIframeID = iframeId;
            this.SelectSection1.SetTitle = "签收部门";
            this.SelectSection1.SModel = "1";
            this.txtTime.Text = string.Format("{0:yyyy-MM-dd HH:mm}", DateTime.Now);
            this.lbName.Text = Utils.GetQueryStringValue("names");
            this.lbHandler.Text = this.SiteUserInfo.Name;
            this.hidGoodId.Value = id;
            this.hidName.Value = Utils.GetQueryStringValue("names");
            this.hidPrice.Value = Utils.GetQueryStringValue("prices");

        }

        private void PageSave()
        {
            #region 表单取值
            string goodid = Utils.GetFormValue(hidGoodId.UniqueID);
            string name = Utils.GetFormValue(hidName.UniqueID);
            string gettime = Utils.GetFormValue(txtTime.UniqueID);
            string price = Utils.GetFormValue(hidPrice.UniqueID);
            string deptid = Utils.GetFormValue(this.SelectSection1.SelectIDClient);
            string deptname = Utils.GetFormValue(this.SelectSection1.SelectNameClient);
            string getpersonid = Utils.GetFormValue(this.HrSelect1.HrSelectIDClient);
            string getpersonname = Utils.GetFormValue(this.HrSelect1.HrSelectNameClient);
            string count = Utils.GetFormValue(txtCount.UniqueID);
            string use = Utils.GetFormValue(this.txtUsed.UniqueID);
            #endregion
            #region 表单验证
            string msg = "";
            int result = -1;
            if (string.IsNullOrEmpty(gettime))
            {
                msg += "-请输入领用时间！";
            }
            if (string.IsNullOrEmpty(deptid) && string.IsNullOrEmpty(deptname))
            {
                msg += "-请选择领用部门！";
            }
            if (string.IsNullOrEmpty(getpersonid) && string.IsNullOrEmpty(getpersonname))
            {
                msg += "-请选择领用人！";
            }
            if (string.IsNullOrEmpty(count))
            {
                msg += "-请输入领用数量！";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson(result.ToString(), msg));
                Response.End();
            }
            #endregion
            #region 实体赋值
            IList<MGovGoodUse> list = new List<MGovGoodUse>();
            string[] id = goodid.Split(',');
            string[] names = name.Split(',');
            string[] prices = price.Split(',');
            for (int i = 0; i < id.Length; i++)
            {
                MGovGoodUse model = new MGovGoodUse();
                model.CompanyId = this.SiteUserInfo.CompanyId;
                model.Dept = deptname;
                model.DeptId = Utils.GetInt(deptid);
                model.GoodId = id[i];
                model.GoodName = names[i];
                model.IssueTime = DateTime.Now;
                model.Number = Utils.GetInt(count);
                model.Operator = this.SiteUserInfo.Name;
                model.OperatorId = this.SiteUserInfo.UserId;
                model.Price = Utils.GetDecimal(prices[i]);
                model.Time = Utils.GetDateTimeNullable(gettime);
                model.Type = EyouSoft.Model.EnumType.GovStructure.GoodUseType.发放;
                model.Use = use;
                model.UserId = getpersonid;
                model.UserName = getpersonname;
                list.Add(model);
            }
            #endregion
            #region 提交保存
            BGood BLL = new BGood();
            result = BLL.AddGovGoodUseList(list);
            msg = result == 0 ? "物品发放成功！" : result == -1 ? "物品发放失败！" : "发放物品数量超出库存数量！";
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(result.ToString(), msg));
            Response.End();
            #endregion
        }

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_栏目, false);
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_发放登记))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_发放登记, false);
            }
        }
        #endregion
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
    }
}