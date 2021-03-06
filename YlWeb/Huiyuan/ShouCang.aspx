﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShouCang.aspx.cs" Inherits="EyouSoft.YlWeb.Huiyuan.ShouCang" MasterPageFile="~/MasterPage/HuiYuan.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HuiYuanHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HuiYuanBody" runat="server">
    <form id="form1" runat="server">
        <div class="user_headbox">
          <div class="user_head"><img src="<%=TuXiang %>" width="104px" height="100px" /></div>
          <div class="user_msg">
	          <p>欢迎您，<font class="font_blue"><%=this.HuiYuanInfo.XingMing%></font></p>
		        <ul>
		          <li>当前积分 <em class="user_jf"><%=KeYongJiFen.ToString("F2")%></em></li>
		          <li><a href="DingDan.aspx">待付款订单 <em class="user_dindan_num"><%=DaiFuKuanDingDanShu%></em></a></li>
        		  
		          <li><a href="YouKe.aspx">常用游客信息管理</a></li>
		          <li style="text-align:right;"><a href="ShouCang.aspx">查看我的收藏<em class="fontgreen"><%=ShouCangShu%></em></a></li>
		        </ul>
          </div>
        </div>

        <div class="menu_T"><h3>我的收藏</h3></div>

        <div class="user_table">
           <table width="100%" border="0" class="tablelist" id="liststyle">
	          <tr>
	            <th width="30"><input type="checkbox" name="checkbox" /></th>
		        <th align="left">名称</th>
		        <th align="center">金额</th>
		        <th align="center">状态</th>
		        <th width="140" align="center">操作</th>
	          </tr>
           <asp:Repeater ID="rpt" runat="server">
               <ItemTemplate>
	          <tr i_shoucangid="<%#Eval("ShouCangId") %>">
	            <td><input type="checkbox" name="checkbox" /></td>
		        <td><%#Eval("CPName")%><a href="<%#GetLianJie(Eval("ChanPinId"),Eval("LeiXing")) %>" class="green" target="_blank">【详情】</a></td>
		        <td align="center"><%#GetJinE(Eval("JinE"),Eval("JiFen"),Eval("LeiXing")) %></td>
		        <td align="center"><b class="price_fontred font14"><%#GetShuLiang(Eval("IsYouXiao"),Eval("LeiXing")) %></b></td>
		        <td align="center"><a href="javascript:void(0);" class="color_7">【删除】</a></td>
	          </tr>
                </ItemTemplate>
           </asp:Repeater>
           <asp:PlaceHolder ID="phdNoDat" runat="server" Visible="false">
                   <tr>
                     <td align="center" colspan="5">暂无收藏</td>
                   </tr>
           </asp:PlaceHolder>
         </table>
           <table width="100%" border="0" class="margin_T16">
	          <tr>
	            <td>
        <a href="javascript:void(0);" class="color_5">【批量删除】</a></td>
		        <td>
        <div>
            <div id="page_change" style="width: 100%; text-align: right; margin: 0px auto 0px;
                margin:0; clear: both">
            </div>    
        </div>
		        </td>
	          </tr>
           </table>
        </div>
    </form>
    <script type="text/javascript">
        var ShouCang={
            Ids:[],
            Delete:function(){
                $.newAjax({
                    url: "ShouCang.aspx?del=1&ids=" + ShouCang.Ids.join(","),
                    dataType: "json",
                    type: "post",
                    success: function(ret) {
                        ShouCang.Ids=[];
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() { window.location.href = window.location.href; });
                        }
                        else {
                            tableToolbar._showMsg(ret.msg);
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            }
        };
        $(function(){
            tableToolbar.init({ tableContainerSelector: "#liststyle"});
            $(".color_7").unbind("click").bind("click",function(){
                ShouCang.Ids.push($(this).closest("tr").attr("i_shoucangid"));
                ShouCang.Delete();
            });
            $(".color_5").unbind("click").bind("click",function(){
                $("input[name='checkbox']:checked").each(function(){
                    ShouCang.Ids.push($(this).closest("tr").attr("i_shoucangid"));
                });
                if(ShouCang.Ids.length<=0){
                    tableToolbar._showMsg("请选择要删除的收藏！");
                }
                else{
                    ShouCang.Delete();
                }
            });
        })
    </script>    
</asp:Content>