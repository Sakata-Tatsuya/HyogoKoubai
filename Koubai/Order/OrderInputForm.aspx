<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderInputForm.aspx.cs" Inherits="Koubai.Order.OrderInputForm" %>

<%@ Register TagPrefix="uc1" TagName="CtlMainMenu" Src="~/CtlMainMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<%@ Register Src="~/CtlTabMain.ascx" TagName="CtlTabMain" TagPrefix="uc1" %>--%>
<%@ Register Src="~/Common/CtlMyPager.ascx" TagName="CtlMyPager" TagPrefix="uc2" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>発注入力</title>
    <link href="../MainStyle.css" rel="stylesheet" type="text/css" />

    <telerik:RadScriptBlock ID="RSM" runat="server">
    <script type="text/javascript">
        function $(id)
        {
            return document.getElementById(id);
        }
        function OnRequestStart()
        {
            //document.getElementById("Img1").style.display = '';
        }
        function OnResponseEnd()
        {
            //document.getElementById("Img1").style.display = 'none';
        }
        function AjaxRequest(command_name, arg)
        {
            <%= Ram.ClientID %>.ajaxRequest(command_name + ':' + arg);
        }
        function CheckDecimal(deci)
	    {
	        if (deci == '')
	        {
	            return true;
	        }
	        else
	        {
	            if (deci.match( /[^0-9.,]/ ) != null)
	            {
	                alert('単価の入力値が正しくありません');
	                return false;
	            }
	            var deciAry = deci.split('.');
	            if (deciAry.length > 2)
	            {
	                alert('小数点は1つだけ入力可能です');
	                return false;
	            }
	            if (deciAry.length == 2 && (deciAry[0].length == 0 || deciAry[1].length == 0))
	            {
	                alert('単価の入力値が正しくありません');
	                return false;
	            }
	            if (deciAry.length < 3 && deciAry[0].length > 8)
	            {
	                alert('単価の整数部は8桁以内で入力して下さい');
	                return false;
	            }
	            if (deciAry.length == 2 && deciAry[1].length > 2)
	            {
	                alert('単価の小数部は2桁以内で入力して下さい');
	                return false;
	            }
	            return true;
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
        /*
        function Reload()
        {
            AjaxRequest('Reload', meisai);
        }
        */

    </script>
    </telerik:RadScriptBlock>

</head>
<body class="bg0">
    <form id="form1" runat="server">
<%--    <uc1:CtlTabMain ID="Tab" runat="server" />--%>
    <uc1:CtlMainMenu ID="M" runat="server"></uc1:CtlMainMenu>
    <table id="TblMain" runat="server" width="100%" class="def9">
        <tr>
            <td>
                <table id="Table1" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" height="20">
                            <asp:Label ID="LblMsg" runat="server" CssClass="b"></asp:Label>
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
                            <table style="border-collapse:collapse;">
                                <tr>
                                    <td style="padding:0 0 0 0">
                                        <asp:Button ID="BtnDel" runat="server" Text="チェックした行を削除する" OnClick="BtnDel_Click" />
                                    </td>
                                    <td style="padding:0 0 0 0">
                                        <asp:Button ID="BtnClear" runat="server" Text="入力内容を全てクリアする" OnClick="BtnClear_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="G" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="def9" OnRowDataBound="G_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <br />
                            </EditItemTemplate>
                            <HeaderTemplate>
                                削<br />
                                除<br />
                                <input id="ChkH" runat="server" type="checkbox" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input id="ChkI" runat="server" type="checkbox" />
                            </ItemTemplate>
                            <ItemStyle CssClass="tc" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <table class="col" frame="void" width="100%">
                                    <tr>
                                        <td class="tc nw s1">発注No</td>
                                    </tr>
                                    <tr>
                                        <td class="tc nw">仕入先</td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table class="col" frame="void" width="100%">
                                    <tr>
                                        <td class="tl s3 hei25">
                                            <asp:Label ID="LblHacchuuNo" runat="server" CssClass="def12"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tl hei25">
                                            <telerik:RadComboBox ID="RcbShiiresaki" runat="server" AllowCustomText="True" EnableLoadOnDemand="True"
                                                Height="180px" MarkFirstMatch="True" NoWrap="True" ShowMoreResultsBox="True" ShowToggleImage="False"
                                                AutoPostBack="True" EnableVirtualScrolling="false" Skin="Simple" Width="200px"
                                                OnItemsRequested="RcbShiiresaki_ItemsRequested">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <ItemStyle CssClass="tc" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="nw" />
                            <HeaderTemplate>
                                <table class="col" frame="void" width="100%">
                                    <tr>
                                        <td class="tc nw s1">
                                            品目グループ
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tc nw">
                                            品目コード/品目名
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table align="center" class="col" frame="void" width="100%">
                                    <tr>
                                        <td class="nw s3 hei25">
<%--                                            <asp:DropDownList ID="DdlBuhinKubun" runat="server">
                                            </asp:DropDownList>--%>
                                            <telerik:RadComboBox ID="RcbBuhinKubun" runat="server" AllowCustomText="True" EnableLoadOnDemand="True"
                                                Height="180px" MarkFirstMatch="True" NoWrap="True" ShowMoreResultsBox="True" ShowToggleImage="False"
                                                AutoPostBack="True" EnableVirtualScrolling="false" Skin="Simple" Width="60px"
                                                OnItemsRequested="RcbBuhinKubun_ItemsRequested">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="nw hei25">
                                            <telerik:RadComboBox ID="RcbBuhin" runat="server" AllowCustomText="True" EnableLoadOnDemand="True"
                                                Height="180px" MarkFirstMatch="True" NoWrap="True" ShowMoreResultsBox="True" ShowToggleImage="False"
                                                AutoPostBack="True" EnableVirtualScrolling="false" Skin="Simple" Width="200px"
                                                OnItemsRequested="RcbBuhin_ItemsRequested">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <table class="col" frame="void" width="100%">
                                    <tr>
                                        <td class="tc nw s1">
                                            ロット数
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tc nw">
                                            単価
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table align="center" class="col" frame="void" width="100%">
                                    <tr>
                                        <td class="tr nw s3 hei25">
                                            <asp:Label ID="LblLot" runat="server" CssClass="def12"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tr nw hei25">
                                            仮<input id="ChkKariTanka" runat="server" type="checkbox" />
                                            \<asp:TextBox ID="TbxTanka" runat="server" Width="60px" MaxLength="11" CssClass="tr"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <ItemStyle CssClass="nw" />
                            <HeaderStyle CssClass="nw" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="税率">
                            <ItemTemplate>
                                <asp:DropDownList ID="DdlZeiritu" runat="server">
                                    <asp:ListItem Value="8">8%</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="10">10%</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle CssClass="tc" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="数量">
                            <ItemTemplate>
                                <asp:TextBox ID="TbxSuryo" runat="server" CssClass="tr" MaxLength="8" Width="60px"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle CssClass="tc" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="単位">
                            <ItemTemplate>
                                <asp:Label ID="LblTani" runat="server" CssClass="def12"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="tc nw" />
                            <HeaderStyle CssClass="nw" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="リードタイム">
                            <ItemStyle CssClass="nw tc" />
                            <HeaderStyle CssClass="nw" />
                            <ItemTemplate>
                                <asp:Label ID="LblLT" runat="server" CssClass="def12"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <table class="col" frame="void" width="100%">
                                    <tr>
                                        <td class="tc s1">
                                            納期
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tc">
                                            納入場所
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table align="center" class="col" frame="void" width="100%">
                                    <tr>
                                        <td class="tc s3 hei25">
                                            <telerik:RadDatePicker id="RdpNouki" ToolTip="" MinDate="1950-01-01" Runat="server" Width="100px">
                                                <Calendar Runat="server" ShowRowHeaders="False"></Calendar>
                                                <DateInput Runat="server" DisplayDateFormat="yyyy/MM/dd" LabelCssClass="radLabelCss_Default" Width="80px"
                                                   DateFormat="yyyy/MM/dd" Font-Size="9pt" RangeValidation="Immediate" DisplayPromptChar="_" PromptChar=" ">
                                                </DateInput>
                                             </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tc hei25">
                                            <asp:DropDownList ID="DdlBasho" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <ItemStyle CssClass="tc" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="備考（200文字以内）">
                            <ItemTemplate>
                                <asp:TextBox ID="TbxBikou" runat="server" TextMode="MultiLine" MaxLength="200" Height="70px" Width="300px"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle CssClass="tc" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="bg1" />
                    <HeaderStyle BorderStyle="None" CssClass="bg3" />
                    <AlternatingRowStyle CssClass="bg2" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="tl">
                <asp:Button ID="BtnAdd" runat="server" Text="行を追加する" OnClick="BtnAdd_Click" />
            </td>
        </tr>
        <tr>
            <td class="tc">
                <asp:Label ID="LblOK" runat="server" CssClass="b"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tc">
                <asp:Button ID="BtnT" runat="server" BackColor="aqua" Text="上記の内容で発注する" OnClick="BtnT_Click" />
            </td>
        </tr>
        <tr>
            <td>
<%--                <radCln:RadCalendar ID="SC" runat="server" Skin="Web20">
                </radCln:RadCalendar>--%>
                <input id="HidChkID" runat="server" type="hidden" />
                <input id="HidArgs" runat="server" type="hidden" />
                <input id="HidNoukiID" runat="server" type="hidden" />
            </td>
        </tr>
    </table>
    <telerik:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
        <ClientEvents OnRequestStart="OnRequestStart" OnResponseEnd="OnResponseEnd" />
    </telerik:RadAjaxManager>
    </form>
</body>
</html>
