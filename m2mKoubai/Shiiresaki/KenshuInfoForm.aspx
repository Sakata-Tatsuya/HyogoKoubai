<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KenshuInfoForm.aspx.cs" Inherits="m2mKoubai.Shiiresaki.KenshuInfoForm" %>

<%@ Register Src="CtlTabShiire.ascx" TagName="CtlTabShiire" TagPrefix="uc1" %>
<%@ Register Src="../Common/CtlMyPager.ascx" TagName="CtlMyPager" TagPrefix="uc2" %>
<%@ Register Src="~/Common/CtlNengappiFromTo.ascx" TagName="CtlNengappiFromTo" TagPrefix="uc3" %>
<%--<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>--%>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>検収情報</title>
    <link href="../MainStyle.css" rel="stylesheet" type="text/css" />   
     <script type="text/javascript">
     function $(id) {
         return document.getElementById(id);
     }
     function AjaxRequest(command_name, arg) {
        <%= Ram.ClientID %>.ajaxRequest(command_name + ':' + arg);
     }
	 function Reload()
    {
        AjaxRequest('reload', '');
    }
    function Kensaku()
    {
        if (document.getElementById('DdlMonth').selectedIndex == 0)
        {
            alert('検収月を選択して下さい');
            return;
        }
	    AjaxRequest('kensaku', '');
    }
    function RowChange()
    {
	    AjaxRequest('row', '');
    }
    function PageChange(pageIndex)
    {
	    AjaxRequest('page', pageIndex);
    }
    function OnBuhin()
    {
        AjaxRequest('ddlBuhin', '');
    }
	function HacchuuNo(key, hacchuuNo)
    {
        document.getElementById('HidKey').value = key +'\t'+ hacchuuNo;
        NewForm.action = "OrderShousaiForm";
        NewForm.target = "_hacchuu"; 
	    OpenWinPost("_hacchuu",500,500);
        NewForm.submit();
    }
    function Print()
    {
        var chkIdAry = document.getElementById('HidChkID').value.split(',');
        var hidPrintKey = ''
        for(var i = 0; i < chkIdAry.length; i++)
        {
            var chk = document.getElementById(chkIdAry[i]);
           
            if(chk.checked)
            {
                if(hidPrintKey != "") hidPrintKey += "_";
                hidPrintKey += chk.value;
            }
        }
        if(hidPrintKey == "")
        {
            alert("チェックを入れてください");
            return false;
        }
    
        document.getElementById('HidKey').value = hidPrintKey;
        NewForm.action = "../Denpyou/JyuryousyoForm";
        NewForm.target = "_hacchuusho";
	    OpenWinPost2("_hacchuusho",800,600);
        NewForm.submit();
    }
    function Kenshu(key)
    {
        document.getElementById('HidKey').value = key;
        NewForm.action = "../Denpyou/KenshuMeisaihyoForm";
        NewForm.target = "_hacchuusho";
	    OpenWinPost2("_hacchuusho",800,600);
        NewForm.submit();
    }
    function Seikyu(key)
    {
        document.getElementById('HidKey').value = key;
        NewForm.action = "../Denpyou/SeikyusyoForm";
        NewForm.target = "_hacchuusho";
	    OpenWinPost2("_hacchuusho",800,600);
        NewForm.submit();
    }
    function Jyuryou()
    {
        var chkIdAry = document.getElementById('HidChkID').value.split(',');
        var hidPrintKey = ''
        for(var i = 0; i < chkIdAry.length; i++)
        {
            var chk = document.getElementById(chkIdAry[i]);
           
            if(chk.checked)
            {
                if(hidPrintKey != "") hidPrintKey += "_";
                hidPrintKey += chk.value;
            }
        }
        if(hidPrintKey == "")
        {
            alert("チェックを入れてください");
            return false;
        }
    
        document.getElementById('HidKey').value = hidPrintKey;
        NewForm.action = "../Denpyou/JyuryousyoForm";
        NewForm.target = "_hacchuusho";
	    OpenWinPost2("_hacchuusho",800,600);
        NewForm.submit();
    }
    var win = null;
    function OpenWinPost(target,w,h)
    {
        win = window.open
            ("",target,"width="+w+"px,height="+h+"px,location=no,resizable=yes,scrollbars=yes");
	    win.focus();
    }
     
    function OpenWinPost2(target,w,h,etc)
    {
        win = window.open
            ("",target,"width="+w+"px,height="+h+"px,menubar=yes,location=no,resizable=yes,scrollbars=yes");
	    win.focus();
    } 
	function ChkAll(bool)
    {
        var idAry = document.getElementById('HidChkID').value.split(',');
      
        for(var i = 0; i < idAry.length; i++)
        {
            var chk = document.getElementById(idAry[i]);
            chk.checked = bool;
        }
    }
	
    function OnRequestStart(sender, args)
	{
        document.getElementById("Img1").style.display = '';		
	}
	function OnResponseEnd(sender, args)
    {
        document.getElementById('Img1').style.display = 'none';
    }
   
    function KeyCodeCheck()
    {
        var kc = event.keyCode;
        if((kc >= 37 && kc <= 40) || (kc >= 48 && kc <= 57) || (kc >= 96 && kc <= 105) || 
        kc == 46 || kc == 8 || kc == 13 || kc == 9)
            return true; 
        else 
            return false;
    }
   
     </script>
</head>
<body class="bg99">
    <form id="form1" runat="server">
    <div>
        <uc1:CtlTabShiire ID="Tab" runat="server" />
                    <table border="1" bordercolor="#000000" class="tc col bg1 def9 mt5">
                        <tr class="bg3">
                            <td >
                                検収年月</td>
                            <td style="width: 96px">
                                発注元事業所</td>
                            <td rowspan="2">
                                <input id="BtnK" runat="server" class="w60 bg98" type="button" value="検索" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="DdlYear" runat="server">
                                </asp:DropDownList>年<asp:DropDownList ID="DdlMonth" runat="server">
                                    <asp:ListItem>---</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                </asp:DropDownList>月</td>
                            <td >
                                <asp:DropDownList ID="DdlJigyoshoKubun" runat="server">
                                </asp:DropDownList></td>
                            
                        </tr>
                    </table>
        <table id="TblList" runat="server" width="100%" class="def9">
            <tr>
                <td>
                    <table id="TABLE1" runat="server" width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="50%">
                                <asp:Label ID="LblMsg" runat="server" CssClass="b"></asp:Label></td>
                            <td class="hei20">
                                <img id="Img1" runat="server" src="../Img/Load.gif" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td>
                               <uc2:CtlMyPager ID="Pt" runat="server" />
                            </td>
                            <td nowrap="noWrap">
                                &nbsp;<input id="BtnKI" runat="server" type="button" value="検収明細表の&#13;&#10;印刷画面を表示する" class="bg98 w150" />
                    <input id="BtnSI" runat="server" type="button" value="請求書の&#13;&#10;印刷画面を表示する" class="bg98 w150" /></td>
                            <td align="right">
                                <table class="def9" id="TblRow" runat="server">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="DdlRow" runat="server">
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem Selected="True">30</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                                <asp:ListItem>--</asp:ListItem>
                                            </asp:DropDownList>行</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="G" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="def9" OnRowDataBound="G_RowDataBound">
                        <Columns>                          
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <table class="tc col" frame="void" width="100%">
                                        <tr>
                                            <td class="s1">
                                                発注No</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                発注日</td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table class="col" frame="void" width="100%">
                                        <tr>
                                            <td class="s2">
                                                <asp:Literal ID="LitHacchuuNo" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Literal ID="LitHacchuuBi" runat="server"></asp:Literal></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="品目グループ" >
                                <ItemStyle CssClass="tc" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="品目コード" >
                                <ItemStyle CssClass="tc" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="品目名" />
                            <asp:BoundField HeaderText="注文数量">
                                <ItemStyle CssClass="tr" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="単位">
                                <ItemStyle CssClass="tc" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="単価" >
                                <ItemStyle CssClass="tr" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="注文金額" >
                                <ItemStyle CssClass="tr" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="納入場所" />
                            <asp:BoundField HeaderText="受入日" >
                                <ItemStyle CssClass="tc" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="入荷数量">
                                <ItemStyle CssClass="tr" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle CssClass="bg1" />
                         <HeaderStyle BorderStyle="None" CssClass="bg3" />
                        <AlternatingRowStyle CssClass="bg2" />
                    </asp:GridView>
                    <uc2:CtlMyPager ID="Pb" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <input id="HidChkID" runat="server" type="hidden" />
                    <input id="HidKeyKen" runat="server" type="hidden" /></td>
            </tr>
        </table>
    
    </div>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js">
                </asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js">
                </asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js">
                </asp:ScriptReference>
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
            <ClientEvents OnRequestStart="OnRequestStart" OnResponseEnd="OnResponseEnd" />
        </telerik:RadAjaxManager>    
    </form>   
    <form id="NewForm" method="post" name="NewForm" >
            <input id="HidKey" runat="server" type="hidden" /></form>
</body>
</html>
