<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlTabShiire.ascx.cs" Inherits="m2mKoubai.Shiiresaki.CtlTabShiire" %>
<%@ Register Assembly="RadTabStrip.Net2" Namespace="Telerik.WebControls" TagPrefix="radTS" %>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td valign="top">
<radTS:RadTabStrip id="Tab" runat="server" RadControlsDir="~/MyRadControls/"  Skin="ClassicBlue" SkinsPath="~/MyRadControls/TabStrip/Skins"><Tabs>
<radTS:Tab runat="server" Text="�󒍏��" NavigateUrl="~/Shiiresaki/OrderInfoForm.aspx"></radTS:Tab>
<radTS:Tab runat="server" Text="�������" NavigateUrl="~/Shiiresaki/KenshuInfoForm.aspx"></radTS:Tab>
<radTS:Tab runat="server" Text="�p�X���[�h�ύX" NavigateUrl="~/Shiiresaki/PassChangeForm.aspx"></radTS:Tab>
    <radTS:Tab runat="server" NavigateUrl="~/Shiiresaki/KaishaInfoForm.aspx" Text="��Џ��">
    </radTS:Tab>
</Tabs>
</radts:radtabstrip>
</td>
        <td valign="top">      
            <radts:radtabstrip id="Radtabstrip1" runat="server"  RadControlsDir="~/MyRadControls/" Skin="ClassicBlue" SkinsPath="~/MyRadControls/TabStrip/Skins">
                <Tabs>           
                    <radTS:Tab ID="TabLO" SelectedCssClass="" ForeColor=""  runat="server" Text="���O�A�E�g" ></radTS:Tab>
                </Tabs>
            </radts:radtabstrip>
        </td>
    </tr>
</table>