<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KenshuInfoShukeiForm.aspx.cs" Inherits="m2mKoubai.Kenshu.KenshuInfoShukeiForm" %>

<%@ Register Src="../CtlTabMain.ascx" TagName="CtlTabMain" TagPrefix="uc1" %>
<%@ Register Src="~/Common/CtlNengappiFromTo.ascx" TagName="CtlNengappiFromTo" TagPrefix="uc2" %>
<%@ Register Src="~/Common/CtlMyPager.ascx" TagName="CtlMyPager" TagPrefix="uc3" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>検収情報集計</title>
    <link href="../MainStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    function $(id)
    {
        return document.getElementById(id);
    }
    function AjaxRequest(command_name, arg)
	{
        <%= Ram.ClientID %>.ajaxRequest(command_name + ':' + arg);
	}
	 function Reload()
    {
        AjaxRequest('reload', '');
    }
    function Kensaku()
    {
        if(!Check())
            return;
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
 
    function OnRequestStart(sender, args)
	{
        document.getElementById("Img1").style.display = '';
	}
	function OnResponseEnd(sender, args)
    {
        document.getElementById('Img1').style.display = 'none';
    }
    function Check()
    {
        var TbxKanjyouKamokuCode = document.getElementById('TbxKanjyouKamokuCode');
        if (!CheckNumber(TbxKanjyouKamokuCode,'勘定科目コード'))
        {
            return false;
        }
        var TbxHiyouKamokuCode = document.getElementById('TbxHiyouKamokuCode');
        if (!CheckNumber(TbxHiyouKamokuCode,'費用科目コード'))
        {
            return false;
        }
        var TbxHojyoKamokuNo = document.getElementById('TbxHojyoKamokuNo');
        if (!CheckNumber(TbxHojyoKamokuNo,'補助科目No'))
        {
            return false;
        }
        
        return true;
    }
    function CheckNumber(tbx,str)
    { 
        if (tbx.value.match(/[^0-9]/) != null)
        {
            alert(str+"は半角数字のみで入力して下さい");
            tbx.focus();
            return false;
        }
        return true;
    }
   
    </script>

</head>
<body class="bg0">
    <form id="form1" runat="server">
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
    <div>
        <uc1:CtlTabMain ID="Tab" runat="server" />
    </div>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table class="tc def10 col bg1 mt5" border="1" bordercolor="#000000">
                    <tr class="bg3">
                        <td>
                            期間
                        </td>
                        <td>
                            仕入先
                        </td>
                        <td>
                            勘定科目コード
                        </td>
                        <td>
                            費用科目コード
                        </td>
                        <td>
                            補助科目No
                        </td>
                        <td rowspan="2" class="bg1">
                            <input id="BtnK" runat="server" class="w60 bg6" type="button" value="検索" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc2:CtlNengappiFromTo ID="CtlDate" runat="server" />
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlShiire" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="TbxKanjyouKamokuCode" runat="server" Width="40px" MaxLength="3"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="TbxHiyouKamokuCode" runat="server" Width="40px" MaxLength="3"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="TbxHojyoKamokuNo" runat="server" Width="40px" MaxLength="1"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="hei20">
                <asp:Label ID="LblMsg" runat="server" CssClass="b def9"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <table id="TblList" runat="server" align="left" class="def9">
        <tr>
            <td>
                <table class="def14 col" border="1" bordercolor="#000000" align="center" frame="box"
                    width="50%" id="TblGoukei" runat="server">
                    <tr>
                        <td class="bg3 tc">
                            合計金額
                        </td>
                        <td class="bg1" align="right">
                            <asp:Label ID="LblKingaku" runat="server" CssClass="tr"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="bottom">
                <table id="TABLE1" runat="server" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%">
                        </td>
                        <td class="hei20">
                            <img id="Img1" runat="server" src="../Img/Load.gif" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <uc3:CtlMyPager ID="Pt" runat="server"></uc3:CtlMyPager>
                        </td>
                        <td align="right" valign="bottom">
                            <table id="TblRow" runat="server" cellpadding="0" cellspacing="0" class="def9">
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
                                        </asp:DropDownList>
                                        行
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="G" runat="server" AutoGenerateColumns="False" CssClass="def12"
                    OnRowDataBound="G_RowDataBound" Width="100%">
                    <PagerSettings Visible="False" />
                    <Columns>
                        <asp:TemplateField HeaderText="仕入先">
                            <ItemStyle CssClass="tl w300" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="合計金額">
                            <ItemStyle CssClass="tr w150" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="税額">
                            <ItemStyle CssClass="tr w150" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="総合計">
                            <ItemStyle CssClass="tr w150" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="bg3" />
                    <RowStyle CssClass="bg1" />
                    <AlternatingRowStyle CssClass="bg2" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <uc3:CtlMyPager ID="Pb" runat="server"></uc3:CtlMyPager>
            </td>
        </tr>
    </table>
    <telerik:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
        <ClientEvents OnRequestStart="OnRequestStart" OnResponseEnd="OnResponseEnd" />
    </telerik:RadAjaxManager>
    <telerik:RadCalendar ID="SC" runat="server" Skin="Web20">
    </telerik:RadCalendar>
    </form>
</body>
</html>
