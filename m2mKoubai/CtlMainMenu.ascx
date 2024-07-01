<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlMainMenu.ascx.cs" Inherits="m2mKoubai.CtlMainMenu" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<%@ Register Assembly="RadMenu.Net2" Namespace="Telerik.WebControls" TagPrefix="radM" %>--%>
<style>
    .top_bar_ub {
        background-position: bottom;
        background-attachment: scroll;
        background-image: url(../img/logo_m2m.png);
        background-repeat: repeat-x;
    }
</style>

<telerik:RadScriptManager ID="SM" runat="server" AsyncPostBackTimeout="600">
    <Scripts>
        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
    </Scripts>
</telerik:RadScriptManager>
<telerik:RadScriptBlock ID="RSM" runat="server">
    <script type="text/javascript">
        window.$ = $telerik.$;

        function hexString(buffer) {
            const byteArray = new Uint8Array(buffer);
            const hexCodes = [...byteArray].map(value => {
                const hexCode = value.toString(16);
                const paddedHexCode = hexCode.padStart(2, '0');
                return paddedHexCode;
            });
            return hexCodes.join('');
        }

    </script>
<%--    <script type="text/javascript" src="../Core.js"></script>--%>
</telerik:RadScriptBlock>

<table id="T0" runat="server" border="0" cellpadding="0" cellspacing="0" style="margin-top: 2px; margin-bottom: 1px;">
    <tr>
        <td nowrap="nowrap">
            <asp:Label ID="LblSiteMei" runat="server" CssClass="def" Font-Bold="True" Font-Names="メイリオ" Font-Size="16pt" ForeColor="Green" Text="m2m Web-EDI">
            </asp:Label>
        </td>
        <td width="20px">
            <p>　</p>
        </td>
        <td>
            <telerik:RadMenu ID="N" Font-Size="9pt" CollapseDelay="200" runat="server" Font-Bold="False" Skin="MetroTouch" style="top: 0px; left: 0px">
                <Items>
                    <telerik:RadMenuItem ID="RadMenuItemO1" runat="server" Text="発注情報" NavigateUrl="~/Order/OrderInfoForm.aspx"></telerik:RadMenuItem>
                    <telerik:RadMenuItem ID="RadMenuItemO2" runat="server" Text="発注入力">
                        <Items>
                            <telerik:RadMenuItem ID="RadMenuItemO21" runat="server" Text="個別発注" NavigateUrl="~/Order/OrderInputForm.aspx"></telerik:RadMenuItem>
                            <telerik:RadMenuItem ID="RadMenuItemO22" runat="server" Text="複数発注" NavigateUrl="~/Order/MultiOrderInputForm.aspx"></telerik:RadMenuItem>
                        </Items>
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem ID="RadMenuItemO3" runat="server" Text="納品" NavigateUrl="~/NouhinForm.aspx"></telerik:RadMenuItem>
                    <telerik:RadMenuItem ID="RadMenuItemO4" runat="server" Text="パスワード変更" NavigateUrl="~/PassChangeForm.aspx"></telerik:RadMenuItem>
                    <telerik:RadMenuItem ID="RadMenuItemO5" runat="server" Text="アップロード" NavigateUrl="~/Upload/UploadForm.aspx"></telerik:RadMenuItem>
                    <telerik:RadMenuItem ID="RadMenuItemO6" runat="server" Text="ダウンロード" NavigateUrl="~/Download/DownloadForm.aspx"></telerik:RadMenuItem>
                    <telerik:RadMenuItem ID="RadMenuItemOM" Text="マスタ管理">
                        <Items>
                            <telerik:RadMenuItem ID="RadMenuItemOM1" runat="server" Text="仕入先" NavigateUrl="~/Master/ShiiresakiForm.aspx"></telerik:RadMenuItem>
                            <telerik:RadMenuItem ID="RadMenuItemOM2" runat="server" Text="担当者(社内)" NavigateUrl="~/Master/TantoushaAccountForm.aspx"></telerik:RadMenuItem>
                            <telerik:RadMenuItem ID="RadMenuItemOM3" runat="server" Text="担当者(仕入先)" NavigateUrl="~/Master/ShiiresakiAccountForm.aspx"></telerik:RadMenuItem>
                            <telerik:RadMenuItem ID="RadMenuItemOM4" runat="server" Text="部品・材料" NavigateUrl="~/Master/BuhinForm.aspx"></telerik:RadMenuItem>
                            <telerik:RadMenuItem ID="RadMenuItemOM5" runat="server" Text="メッセージ登録" NavigateUrl="~/Master/LoginMsgForm.aspx"></telerik:RadMenuItem>
                            <telerik:RadMenuItem ID="RadMenuItemOM6" runat="server" Text="納入場所" NavigateUrl="~/Master/NounyuBashoForm.aspx"></telerik:RadMenuItem>
                            <telerik:RadMenuItem ID="RadMenuItemOM7" runat="server" Text="事業所情報" NavigateUrl="~/Master/KaishaInfoForm.aspx"></telerik:RadMenuItem>
                        </Items>
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem ID="RadMenuItemS1" runat="server" Text="受注情報" NavigateUrl="~/Shiiresaki/OrderInfoForm.aspx"></telerik:RadMenuItem>
                    <telerik:RadMenuItem ID="RadMenuItemS2" runat="server" Text="検収情報" NavigateUrl="~/Shiiresaki/KenshuInfoForm.aspx"></telerik:RadMenuItem>
                    <telerik:RadMenuItem ID="RadMenuItemSM1" runat="server" Text="パスワード変更" NavigateUrl="~/Shiiresaki/PassChangeForm.aspx"></telerik:RadMenuItem>
                    <telerik:RadMenuItem ID="RadMenuItemSM2" runat="server" Text="会社情報" NavigateUrl="~/Shiiresaki/KaishaInfoForm.aspx"></telerik:RadMenuItem>
                    <telerik:RadMenuItem  ID="RadMenuItemC1" runat="server" NavigateUrl="~/ChoboListForm.aspx" Text="帳票管理"></telerik:RadMenuItem>
                    <telerik:RadMenuItem  ID="RadMenuItemC2" runat="server" NavigateUrl="~/LoginForm.aspx" Text="ログアウト"></telerik:RadMenuItem>
                </Items>
            </telerik:RadMenu>
        </td>
    </tr>
</table>
<table id="TblColor" runat="server" bgcolor="#040581" cellpadding="1" cellspacing="0"
    style="margin-bottom: 2px; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #688CAF; border-top-color: #688CAF; border-top-width: 1px; border-top-style: solid;">
    <tr>
        <td width="100%">
            <asp:Label ID="LblMenu" runat="server" CssClass="def" Font-Bold="True" Font-Size="12pt" ForeColor="White"></asp:Label>
        </td>
        <td nowrap="nowrap">
            <asp:Label ID="LblLoginUser" runat="server" CssClass="def" Font-Size="10pt" ForeColor="White"></asp:Label>
        </td>
    </tr>
</table>
