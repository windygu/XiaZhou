﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SouSuo.aspx.cs" Inherits="EyouSoft.YlWeb.CommonPage.SouSuo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater runat="server" ID="rptHQ">
            <ItemTemplate>
                <%#GetHtml(Eval("LeiXing"), Eval("HangQiId"), Eval("FuJians"), Eval("MingCheng"), Eval("YouHuiXinXi"), Eval("RiQis"), Eval("QiShiJiaGe"), Eval("TuJingChengShi"), Eval("ChanPinTeSe"), Eval("XiaoLiang1"), Eval("HaoPing"))%>                                
            </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
$(document).ready(function(){
    if (pConfig.recordCount > 0) { AjaxPageControls.replace("page_change", pConfig); AjaxPageControls.replace("page_change1", pConfig); }
})
</script>
