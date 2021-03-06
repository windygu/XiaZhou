﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="ShouldReturnMoney.aspx.cs" Inherits="Web.FinanceManage.ComeAndGoReconciliation.ShouldReturnMoney" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/CaiWuShaiXuan.ascx" TagName="CaiWuShaiXuan" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a href="GatheringList.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>今日收款</span></a></li>
                <li><s class="orderformicon"></s><a href="ReturnMoneyList.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>今日付款</span></a></li>
                <li><s class="orderformicon"></s><a href="ShouldGathering.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>今日应收</span></a></li>
                <li><s class="orderformicon"></s><a href="javascript:void(0);" hidefocus="true" class="ztorderform de-ztorderform">
                    <span>今日应付</span></a></li>
            </ul>
        </div>
        <form id="SelectFrom" action="ShouldReturnMoney.aspx" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    团号：
                    <input type="text" name="tourID" value="<%=Request.QueryString["tourID"] %>" class="inputtext formsize120" />
                    计调项：
                    <select name="item" class="inputselect">
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL( EyouSoft.Common.EnumObj.GetList( typeof(EyouSoft.Model.EnumType.SourceStructure.SourceType)), Request.QueryString["item"]??"-1",true)%>
                    </select>
                    供应商：
                    <uc2:CustomerUnitSelect ID="CustomerUnitSelect1" runat="server" IsUniqueness="false" SelectFrist="false"/>
                    应付金额：
                    <uc1:CaiWuShaiXuan ID="CaiWuShaiXuan1" runat="server" />
                    <br />
                    核算日期：
                    <input value="<%=Request.QueryString["SDate"] %>" name="SDate" type="text" onfocus="WdatePicker();"
                        class="inputtext formsize80" />
                    -
                    <input type="text" value="<%=Request.QueryString["EDate"] %>" name="EDate" onfocus="WdatePicker();"
                        class="inputtext formsize80" />
                    计调员：
                    <uc1:sellsselect id="txt_Seller" runat="server" SelectFrist="false" />
                    <input type="submit" class="search-btn" /></p>
            </span>
        </div>
        <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
        </form>
        <div id="tablehead" class="tablehead">
            <ul class="fixed">
                <li><s class="dayin"></s><a id="a_print" href="javascript:void();" hidefocus="true"
                    class="toolbar_dayin"><span>打印</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a href="javascript:void(0);" hidefocus="true" id="ToXls"
                    class="toolbar_daochu"><span>导出</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th align="center" class="th-line">
                        团号
                    </th>
                    <th align="left" class="th-line">
                        线路名称
                    </th>
                    <th align="center" class="th-line">
                        计调项
                    </th>
                    <th align="left" class="th-line">
                        供应商单位
                    </th>
                    <th align="center" class="th-line">
                        计调员
                    </th>
                    <th align="center" class="th-line">
                        销售员
                    </th>
                    <th align="right" class="th-line">
                        应付金额
                    </th>
                    <th align="center" class="th-line">
                        核算日期
                    </th>
                    <th align="center" class="th-line">
                        财务人
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr <%#Container.ItemIndex%2==0?" class=\"odd\" ":""  %>>
                            <td align="center">
                                <input type="checkbox" />
                            </td>
                            <td align="center">
                                <%#Eval("TourCode")%>
                            </td>
                            <td align="left">
                                <%#Eval("RouteName")%>
                            </td>
                            <td align="center">
                                <%#Eval("PlanItem")%>
                            </td>
                            <td align="left">
                               <a href="javascript:void(0);" data-class="a_bt">
                                    <%#Eval("Crm")%></a> <span style="display: none"><b>
                                        <%#Eval("Crm")%></b><br>
                                        联系人：<%#Eval("Contact")%><br>
                                        联系方式：<%#Eval("Phone")%></span>
                            </td>
                            <td align="center">
                                <%#Eval("Planer")%>
                            </td>
                            <td align="center">
                                <%#Eval("SellerName")%>
                            </td>
                            <td align="right">
                                <strong class="fontred">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Amount"), ProviderToMoney)%></strong>
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("IssueTime"), ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%#Eval("Operator")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server">
                    <tr align="center">
                        <td colspan="10">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pan_sum" runat="server" Visible="false">
                    <tr>
                        <td colspan="7" align="right">
                            <strong>合计：</strong>
                        </td>
                        <td align="right">
                            <strong class="fontred">
                                <asp:Label ID="lbl_sum" runat="server" Text="0"></asp:Label></strong>
                        </td>
                        <td colspan="2" align="center">
                            &nbsp;
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <div class="tablehead" style="border-top: 0 none;">
           <script type="text/javascript">
               document.write(document.getElementById("tablehead").innerHTML);
           </script>
        </div>
    </div>

    <script type="text/javascript">
        var PageJsDataObj = {
            Bt: function() {/*泡泡提示*/
                $(".bt-wrapper").html("");
                $("a[data-class='a_bt']").bt({
                    contentSelector: function() {
                        return $(this).next("span").html();
                    },
                    positions: ['left', 'right', 'bottom'],
                    fill: '#FFF2B5',
                    strokeStyle: '#D59228',
                    noShadowOpts: { strokeStyle: "#D59228" },
                    spikeLength: 10,
                    spikeGirth: 15,
                    width: 200,
                    overlap: 0,
                    centerPointY: 1,
                    cornerRadius: 4,
                    shadow: true,
                    shadowColor: 'rgba(0,0,0,.5)',
                    cssStyles: { color: '#00387E', 'line-height': '180%' }
                });
            },
            PageInit: function() {
                tableToolbar.init({});
                var that = this;
                that.Bt();
                $("#ToXls").click(function() {
                    toXls1();
                    return false;
                })
                $("#a_print").click(function() {
                    PrintPage("#a_print");
                    return false;
                })
                var paidwuc = new wuc.caiWuShaiXuan(window['<%=CaiWuShaiXuan1.ClientUniqueID %>']);
                paidwuc.setOperator('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperator)%>')
                paidwuc.setOperatorNumber('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperatorNumber)%>')
            }
        }
        $(function() {
            PageJsDataObj.PageInit();
        })
    </script>

</asp:Content>
