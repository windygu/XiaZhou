﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transactions.aspx.cs" Inherits="Web.ResourceManage.Shopping.Transactions" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源管理-购物-交易情况</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/bt.min.js" type="text/javascript"></script>

    <!--paopao star-->

    <script type="text/javascript">
        $(function() {
            $("#alertbox").find("a[Data-planer='planer']").bt({
                contentSelector: function() {
                    return $(this).prev("span").html();
                },
                positions: ['left', 'right', 'bottom'],
                fill: '#FFF2B5',
                strokeStyle: '#D59228',
                noShadowOpts: { strokeStyle: "#D59228" },
                spikeLength: 10,
                spikeGirth: 10,
                width: 120,
                overlap: 0,
                centerPointY: 1,
                cornerRadius: 4,
                shadow: true,
                shadowColor: 'rgba(0,0,0,.5)',
                cssStyles: { color: '#00387E', 'line-height': '140%' }
            });

            $("#alertbox").find("a[Data-guide='guide']").bt({
                contentSelector: function() {
                    return $(this).prev("span").html();
                },
                positions: ['left', 'right', 'bottom'],
                fill: '#FFF2B5',
                strokeStyle: '#D59228',
                noShadowOpts: { strokeStyle: "#D59228" },
                spikeLength: 10,
                spikeGirth: 10,
                width: 400,
                overlap: 0,
                centerPointY: 1,
                cornerRadius: 4,
                shadow: true,
                shadowColor: 'rgba(0,0,0,.5)',
                cssStyles: { color: '#00387E', 'line-height': '140%' }
            });
        });
    </script>

    <!--paopao end-->
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox" id="alertbox">
        <div class="tanchuT" style="text-align: left">
            <a href="javascript:void(0)" class="toolbar_dayin" onclick="PrintPage('liststyle')">
                <img src="/images/dayin1-cy.gif" border="0"></a>&nbsp;<a href="javascript:void(0)"
                    class="toolbar_daochu" onclick="toXls1();return false;"><img src="/images/daochu-cy.gif" border="0"></a></div>
        <div class="tanchuT">
            【与 <b class="fontred">
                <%= unitname %></b> 的交易情况】</div>
        <table width="98%" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF" align="center"
            id="liststyle" style="margin: 0 auto">
            <tbody>
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;" class="odd">
                    <td height="23" bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        &nbsp;
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        团号
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        线路名称
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        人数
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        销售员
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        计调员
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        带团导游
                    </td>
                </tr>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <tr bgcolor="F3F3F3">
                            <td align="center">
                                <span class="alertboxTableT">
                                    <%#Container.ItemIndex + 1 %>
                                </span>&nbsp;
                            </td>
                            <td height="28" align="center">
                                <a>
                                    <%#Eval("TourCode")%></a>
                            </td>
                            <td width="22%" height="28" align="center">
                                <%#Eval("RouteName")%>
                            </td>
                            <td height="28" align="center">
                                <%#Eval("PeopleCount")%>
                            </td>
                            <td align="center">
                                <%#Eval("Seller")%>
                            </td>
                            <td align="center">
                                <span style="display: none;">
                                    <%#GetContactinfo(Eval("PlanerModel"))%>
                                </span><a data-planer="planer" href="javascript:void(0)" title="">
                                    <%#((EyouSoft.Model.ComStructure.MComUser)Eval("PlanerModel")).ContactName%></a>
                            </td>
                            <td height="28" align="center">
                                <span style="display: none">
                                    <%#GetGuideList(Eval("GuideList"))%></span> <a data-guide="guide" href="javascript:void(0);">
                                        <%#GetGuide(Eval("GuideList"))%></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Literal ID="literMsg" runat="server" Text=""></asp:Literal>
                <tr style="background: url(../images/y-formykinfo.gif) repeat-x center top;">
                    <td height="23" bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        &nbsp;
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        团号
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        线路名称
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        人数
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        销售员
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        计调
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        带团导游
                    </td>
                </tr>
                <tr class="">
                    <td height="23" align="right" class="alertboxTableT" colspan="7">
                        <div style="position: relative; height: 32px;">
                            <div class="pages">
                                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                            </div>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</body>
</html>

