<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlTabShiire.ascx.cs" Inherits="m2mKoubai.Shiiresaki.CtlTabShiire" %>
<%@ Register Assembly="RadTabStrip.Net2" Namespace="Telerik.WebControls" TagPrefix="radTS" %>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td valign="top">
<radTS:RadTabStrip id="Tab" runat="server" RadControlsDir="~/MyRadControls/"  Skin="ClassicBlue" SkinsPath="~/MyRadControls/TabStrip/Skins"><Tabs>
<radTS:Tab runat="server" Text="受注情報" NavigateUrl="~/Shiiresaki/OrderInfoForm.aspx"></radTS:Tab>
<radTS:Tab runat="server" Text="検収情報" NavigateUrl="~/Shiiresaki/KenshuInfoForm.aspx"></radTS:Tab>
<radTS:Tab runat="server" Text="パスワード変更" NavigateUrl="~/Shiiresaki/PassChangeForm.aspx"></radTS:Tab>
    <radTS:Tab runat="server" NavigateUrl="~/Shiiresaki/KaishaInfoForm.aspx" Text="会社情報">
    </radTS:Tab>
</Tabs>
</radts:radtabstrip>
</td>
        <td valign="top">      
            <radts:radtabstrip id="Radtabstrip1" runat="server"  RadControlsDir="~/MyRadControls/" Skin="ClassicBlue" SkinsPath="~/MyRadControls/TabStrip/Skins">
                <Tabs>           
                    <radTS:Tab ID="TabLO" SelectedCssClass="" ForeColor=""  runat="server" Text="ログアウト" ></radTS:Tab>
                </Tabs>
            </radts:radtabstrip>
        </td>
    </tr>
</table>