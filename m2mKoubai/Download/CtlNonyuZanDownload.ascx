<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlNonyuZanDownload.ascx.cs" Inherits="m2mKoubai.Download.CtlNonyuZanDownload" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<table id="TblAll" runat="server" class="def9">
    <tr>
        <td class="tc" align="center">
            <asp:DropDownList ID="DdlDataType" runat="server">
                <asp:ListItem Value="CSV">CSV(�J���}��؂�)</asp:ListItem>
                <asp:ListItem Value="TAB">�e�L�X�g(�^�u��؂�)</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="BtnDownload" runat="server" Text="�_�E�����[�h" OnClick="BtnDownload_Click" /></td>
    </tr>
</table>
