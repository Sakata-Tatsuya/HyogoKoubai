<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlKenshuDownload.ascx.cs" Inherits="Koubai.Download.CtlKenshuDownload" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<table id="TblAll" runat="server" class="def9">
    <tr>
        <td align="right" valign="top" style="white-space:nowrap">
            <table id="TblMain" runat="server" border="1" style="border-color:#000000;" class="col tl bg1">
                <tr>
                    <td class="bg4">
                        �����N��</td>
                    <td>
                        &nbsp;<asp:DropDownList ID="DdlYear" runat="server">
                        </asp:DropDownList>�N<asp:DropDownList ID="DdlMonth" runat="server">
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
                        </asp:DropDownList>��</td>
                </tr>
            </table>
        </td>
        <td align="right" valign="top" style="white-space:nowrap">
            <asp:DropDownList ID="DdlDataType" runat="server">
                <asp:ListItem Value="CSV">CSV(�J���}��؂�)</asp:ListItem>
                <asp:ListItem Value="TAB">�e�L�X�g(�^�u��؂�)</asp:ListItem>
            </asp:DropDownList><asp:Button ID="BtnDownload" runat="server" Text="�_�E�����[�h" OnClick="BtnDownload_Click" /></td>
    </tr>
</table>