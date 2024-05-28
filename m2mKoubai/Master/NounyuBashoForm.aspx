<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NounyuBashoForm.aspx.cs" Inherits="m2mKoubai.Master.NounyuBashoForm" %>

<%@ Register Src="../CtlTabMain.ascx" TagName="CtlTabMain" TagPrefix="uc1" %>
<%--<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>--%>
<%@ Register Src="../Common/CtlMyPager.ascx" TagName="CtlMyPager" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>納入場所</title>
    <link href="../MainStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function $(id) {
            return document.getElementById(id);
        }
        function AjaxRequest(command_name, arg) {
            <%= Ram.ClientID %>.ajaxRequest(command_name + ':' + arg);
        }
        function Update(key)
        {
            var win = window.open
            ("NounyuBashoUpForm?key="+key,"_brank","width=500px,height=200px,location=no,resizable=yes,scrollbars=yes");
	        win.focus();
        }
    	function Shinki()
		{
		    var win = window.open
            ("NounyuBashoUpForm","_brank","width=500px,height=200px,location=no,resizable=yes,scrollbars=yes");
		    win.focus();
		}
		function Delete()
        {
            var chkIDAry = document.getElementById('HidChkID').value.split(',');
            var thisIDAry = document.getElementById('HidThisID').value.split(',');
            var hidDelKey = '';
            for (var i = 0; i < chkIDAry.length; i++)
            {
                var chk = document.getElementById(chkIDAry[i]);
                if (chk.checked)
                {
                    if (hidDelKey != "") hidDelKey += ",";
                    hidDelKey += thisIDAry[i];
                }
            }
            if (hidDelKey == "")
            {
                alert("チェックを入れて下さい");
                return false;
            } 
            if (confirm("削除しますか？"))
            {
                if (confirm("本当に削除しますか？"))
                {
                    AjaxRequest('delete', hidDelKey);
                }
            }
        }
        function DelChk(bool)
        {
            var idAry = document.getElementById('HidChkID').value.split(',');
            for (var i = 0; i < idAry.length; i++)
            {
                var chk = document.getElementById(idAry[i]);
                chk.checked = bool;
            }
        }
	    function OnRequestStart()
        {
            document.getElementById('Img1').style.display = '';
        }
        function OnResponseEnd()
        {
            document.getElementById('Img1').style.display = 'none';
        }
        function PageChange(pageIndex)
        {
            AjaxRequest('page', pageIndex);
        }    
	    function Kensaku()
        {
	        AjaxRequest('kensaku', '');
        }
        function Row()
        {
	        AjaxRequest('row', '');
        }
        function Reload()
        {
	        AjaxRequest('row', '');
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
<body class="bg0">
    <form id="form1" runat="server">    
        <uc1:CtlTabMain ID="Tab" runat="server" />
            <input id="BtnNew" runat="server" type="button" value="新規登録" class="mt5 bg6" />       
        <table border="1" bordercolor="#000000" class="def9 bg1 col mt5 tc">
            <tr>
                <td class="bg3" style="width: 83px">
                    納入場所コード</td>
                <td class="bg3">
                    納入場所名</td>
                <td rowspan="2">
                    <input id="BtnK" runat="server" class="w60 bg6" type="button" value="検索" /></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TbxNCode" runat="server" Width="70px"></asp:TextBox></td>
                <td>
                    <asp:TextBox ID="TbxNMei" runat="server" Width="100px"></asp:TextBox></td>
            </tr>
        </table>
        <table width="100%" id="TblList" runat="server" class="def9">
            <tr>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
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
                            <td width="50%">
                                <uc2:ctlmypager id="Pt" runat="server"></uc2:ctlmypager>
                            </td>
                            <td class="tr">
                                <table id="TblRow" runat="server" class="def9">
                                    <tr>
                                        <td>
                                            <input id="BtnS" runat="server" type="button" value="チェックしたデータを削除する" class="bg6" /></td>
                                      
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
                    <asp:GridView ID="G" runat="server" AutoGenerateColumns="False" CssClass="def9" Width="100%" OnRowDataBound="G_RowDataBound">
                        <PagerSettings Visible="False" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    削除<br />
                                    <input id="ChkH" runat="server" type="checkbox" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input id="ChkI" runat="server" type="checkbox" />
                                </ItemTemplate>
                                <ItemStyle CssClass="tc" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    更新
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input id="BK" runat="server" type="button" value="更新" />
                                </ItemTemplate>
                                <ItemStyle CssClass="tc" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="納入場所コード" >
                                <ItemStyle CssClass="tc" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="納入場所名">
                            </asp:BoundField>
                        </Columns>
                       <HeaderStyle CssClass="bg3" />
                         <RowStyle CssClass="bg1" />
                         <AlternatingRowStyle CssClass="bg2" />
                    </asp:GridView>
                    <uc2:ctlmypager id="Pb" runat="server"></uc2:ctlmypager>
                </td>
            </tr>
            <tr>
                <td> <input id="HidChkID" runat="server" type="hidden" />
                    <input id="HidThisID" runat="server" type="hidden" />
                </td>
            </tr>
        </table>
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
            <ClientEvents OnRequestStart= "OnRequestStart" OnResponseEnd="OnResponseEnd" />
        </telerik:RadAjaxManager>
    </form>
</body>
</html>
