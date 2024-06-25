<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlNonyuZanDownload.ascx.cs" Inherits="m2mKoubai.Download.CtlNonyuZanDownload" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<table id="TblAll" runat="server" class="def9">
    <tr>
        <td class="tc" align="center">
            <asp:DropDownList ID="DdlDataType" runat="server">
                <asp:ListItem Value="CSV">CSV(カンマ区切り)</asp:ListItem>
                <asp:ListItem Value="TAB">テキスト(タブ区切り)</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="BtnDownload" runat="server" Text="ダウンロード" OnClick="BtnDownload_Click" /></td>
    </tr>
</table>
