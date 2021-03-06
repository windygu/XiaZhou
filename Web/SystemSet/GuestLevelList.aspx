﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="GuestLevelList.aspx.cs" Inherits="Web.SystemSet.GuestLevelList" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<%@ Register Src="../UserControl/BasciSetBar.ascx" TagName="BasciSetBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="mainbox">
        <uc1:BasciSetBar ID="BasciSetBar1" runat="server" />
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="addicon"></s><a class="add_xlqy" hidefocus="true" href="javascript:"><span>
                    添加</span></a></li><li class="line"></li>
                <li><a class="toolbar_delete" hidefocus="true" href="javascript:"><s class="delicon">
                </s><span>删除</span></a></li>
            </ul>
            <div class="pages">
                <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="tablelist-box">
            <table width="100%" align="center" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th class="thinputbg" style="width: 35px">
                            <input type="checkbox" id="checkbox" name="checkbox" disabled="disabled"/>
                        </th>
                        <th width="264" align="center" class="th-line">
                            等级名称
                        </th>
                        <th width="236" align="center" class="th-line">
                            操作
                        </th>
                        <th style="width: 35px" align="center">
                            &nbsp;
                        </th>
                        <th width="370" align="center" class="th-line">
                            等级名称
                        </th>
                        <th width="249" align="center" class="th-line">
                            操作
                        </th>
                    </tr>
                    <tr>
                        <cc1:CustomRepeater ID="CustomRepeater2" runat="server">
                            <ItemTemplate>
                                <td align="left">
                                    <input type="checkbox" id="<%#Eval("Id")%>" value="<%#Eval("Id")%>" <%#Convert.ToBoolean(Eval("IsSystem")) ? "disabled=\"disabled\"" : ""%>
                                        name="chk">
                                    <%#Container.ItemIndex+1 %>
                                </td>
                                <td align="center">
                                    <%#Eval("Name")%>
                                </td>
                                <td align="center">
                                    <a class="update_bjbz" target="_blank" href="GuestLevelEdit.aspx?id=<%#Eval("Id")%>&Name=<%#Server.UrlEncode(Eval("Name").ToString())%>">
                                        <%#Convert.ToBoolean(Eval("IsSystem")) ? "" : "修改&nbsp;"%></a> <a class="del_xq"
                                            target="_blank" href="GuestLevelList.aspx?ids=<%#Eval("Id")%>&state=del">
                                            <%#Convert.ToBoolean(Eval("IsSystem")) ? "" : "&nbsp;删除"%></a>
                                        <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex,i,2,3) %>
                            </ItemTemplate>
                        </cc1:CustomRepeater>
                </tbody>
            </table>
        </div>
    </div>
    <div class="tablehead">
        <ul class="fixed">
            <li><s class="addicon"></s><a class="add_xlqy" hidefocus="true" href="javascript:"><span>
                添加</span></a></li><li class="line"></li>
            <li><a class="toolbar_delete" hidefocus="true" href="javascript:"><s class="delicon">
            </s><span>删除</span></a></li>
        </ul>
        <div class="pages">
            <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var GuestLevelList = {

            GetSelectCount: function() {
                var count = 0;
                $("#liststyle").find("input[name='chk']").each(function() {
                    if ($(this).attr("checked") == true) {
                        count++;
                    }
                });
                return count;
            },

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

            Del: function(id) {
                //删除
                var url = "GuestLevelList.aspx?ids=" + id + "&state=del" + "&" + $.param(GuestLevelList.Params);
                window.location.href = url;
                return false;
            },

            openXLwindow: function(url, title, width, height) {
                url = url;
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: title,
                    modal: true,
                    width: width,
                    height: height
                });
            }
        }
        tableToolbar.IsHandleElse = "true";
        tableToolbar.init({
            
            tableContainerSelector: "#liststyle",
            objectName: " 记录",
            //默认按钮
            //修改-删除-取消-复制 为默认按钮，按钮class对应  toolbar_update  toolbar_delete  toolbar_cancel  toolbar_copy即可
            deleteCallBack: function(objsArr) {
                //删除
                var url = "GuestLevelList.aspx?ids=" + GuestLevelList.GetSelectItemValue().toString() + "&state=del" + "&" + $.param(GuestLevelList.Params);
                window.location.href = url;
                return false;
            }
        });
        $(".del_xq").click(function() {
            var url = $(this).attr("href") + "&" + $.param(GuestLevelList.Params);
            window.location.href = url;
            return false;
        });
        $(".add_xlqy").click(function() {
            var url = "GuestLevelEdit.aspx?" + $.param(GuestLevelList.Params);
            GuestLevelList.openXLwindow(url, "添加客户等级", "500px", "99px");
            return false;
        });
        $(".update_bjbz").click(function() {
            var url = $(this).attr("href") + "&" + $.param(GuestLevelList.Params);
            GuestLevelList.openXLwindow(url, "修改客户等级", "500px", "99px");
            return false;
        });
       
    </script>

</asp:Content>
