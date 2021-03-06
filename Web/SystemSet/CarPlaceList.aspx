﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true" CodeBehind="CarPlaceList.aspx.cs" Inherits="EyouSoft.Web.SystemSet.CarPlaceList" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/BasciSetBar.ascx" TagName="BasciSetBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form id="form1" runat="server">
    <div class="mainbox">
        <uc1:BasciSetBar ID="BasciSetBar1" runat="server" />
        <div class="searchbox fixed">
                <span class="searchT">
                    <p>
                        上车地点：
                        <input type="text" name="txtSearch" class="inputtext formsize150" size="28" value="<%=EyouSoft.Common.Utils.GetFormValue("txtSearch")%>" />  
                        <input type="submit" class="search-btn" />
                    </p>
                </span>
            </div>
        <div class="tablehead" id="temptablehead">
           <div id="caozuo" runat="Server">
              <ul class="fixed">
                    <li><s class="addicon"></s><a class="toolbar_add" hidefocus="true" href="javascript:">
                        <span>添加</span></a></li>
                    <li class="line"></li>
                    <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="javascript:">
                        <span>修改</span></a></li>
                    <li class="line"></li>
                    <li><a class="toolbar_delete" hidefocus="true" href="javascript:"><s class="delicon">
                    </s><span>删除</span></a></li>
                </ul>
            </div>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th width="30" class="thinputbg">
                            <input type="checkbox" id="checkbox" name="checkbox"/>
                        </th>
                        <th width="300" align="center" class="th-line">
                            上车地点
                        </th>
                        <th width="200" align="center" class="th-line">
                            接价
                        </th>
                        <th width="200" align="center" class="th-line">
                            送价
                        </th>
                        <th width="228" align="center" class="th-line">
                            描述
                        </th>
                        <th width="100" align="center" class="th-line">
                            状态
                        </th>
                    </tr>
                    <cc2:CustomRepeater ID="repList" runat="server">
                    <ItemTemplate>
                          <tr class="<%#Container.ItemIndex%2==0?"":"#odd" %>">
                             <td align="center">
                                <input type="checkbox" id="<%#Eval("CarLocationId") %>" value="<%#Eval("CarLocationId") %>" name="chk" /><input type="hidden" name="ItemUserID" value="<%#Eval("OperatorId") %>" />
                             </td>
                             <td align="center">
                                 <%#Eval("Location")%>
                             </td>
                             <td align="center">
                                 <b class="fontblue"><%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("OnPrice"), this.ProviderToMoney)%></b> 
                             </td>
                             <td align="center">
                                 <b class="fontblue"><%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("OffPrice"), this.ProviderToMoney)%></b>
                             </td>
                             <td align="center">
                                 <%#Eval("Desc") %>   
                             </td>
                             <td align="center">
                                 <%#GetCarPlaceStatus(Eval("Status").ToString())%>  
                             </td>
                          </tr>
                       </ItemTemplate>
                   </cc2:CustomRepeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border:none;">
             <script type="text/javascript">
                document.write(document.getElementById("temptablehead").innerHTML);
             </script>
         </div>
    </div>
    </form>
    <script type="text/javascript">
        var CarPlaceList = {
            Params: { memuid: querystring(location.href, "memuid"), sl: querystring(location.href, "sl") },
            GetSelectItemValue: function() {
                var arrayList = new Array();
                $("#liststyle").find("input[name='chk']").each(function() {
                    if ($(this).attr("checked") == true) {
                        arrayList.push($(this).attr("id"));
                    }
                });
                return arrayList;
            },
            Init: function() {
                tableToolbar.init({
                    tableContainerSelector: "#liststyle",
                    objectName: " 记录",
                    updateCallBack: function(arr) {
                        var url = "CarPlaceEdit.aspx?id=" + CarPlaceList.GetSelectItemValue().toString() + "&" + $.param(CarPlaceList.Params);
                        CarPlaceList.openXLwindow(url, "修改上车地点", "560px", "235px");
                    }
                });
                $(".toolbar_add").click(function() {
                    var url = "CarPlaceEdit.aspx?" + $.param(CarPlaceList.Params);
                    CarPlaceList.openXLwindow(url, "添加上车地点", "560px", "235px");
                    return false;
                });
                $(".toolbar_delete").click(function() {
                    var selectcols = CarPlaceList.GetSelectItemValue();
                    if (selectcols.length == 0) { tableToolbar._showMsg("未选中任何记录！"); return false; }
                    if (selectcols.length > 1) {
                        tableToolbar._showMsg("只能删除一条记录！");
                    }
                    else {//删除
                        tableToolbar.ShowConfirmMsg("确定删除选中的记录？删除后不可恢复！", function() {
                            CarPlaceList.Del("del");
                        });
                    }
                });
            },
            Del: function(type) {
                $.newAjax({
                    type: "get",
                    cache: false,
                    url: "/SystemSet/CarPlaceList.aspx?state=" + type + "&id=" + CarPlaceList.GetSelectItemValue().toString() + "&" + $.param(CarPlaceList.Params),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = window.location.href;
                            });
                        } else {
                            tableToolbar._showMsg(ret.msg);
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            },
            openXLwindow: function(url, title, width, height) {
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: title,
                    modal: true,
                    width: width,
                    height: height
                });
            }
        }
    $(function() {
      CarPlaceList.Init();
    });
    </script>
</asp:Content>
