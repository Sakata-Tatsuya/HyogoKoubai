<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlNengappiFromTo.ascx.cs" Inherits="m2mKoubai.Common.CtlNengappiFromTo" %>

<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<table id="T" border="0" cellpadding="1" cellspacing="0" >
    <tr>
        <td nowrap >
            <table id="TblFrom" runat="server" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td nowrap >
                       <radCln:RadDatePicker ID="RdpFrom" runat="server">
                           <DateInput Width="80px" Font-Size="10pt" >
                           </DateInput>
                           <Calendar ID="Cal1" runat="server" Skin="Inox"></Calendar>
                        </radCln:RadDatePicker>
                    </td>
                </tr>
            </table>
        </td>
        <td nowrap >
            <asp:DropDownList ID="DdlKikan" runat="server">
                <asp:ListItem Value="0">-----</asp:ListItem>
                <asp:ListItem Value="1">�w���</asp:ListItem>
                <asp:ListItem Value="2">�ȑO</asp:ListItem>
                <asp:ListItem Value="3">�ȍ~</asp:ListItem>
                <asp:ListItem Value="4">����</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            <table id="TblTo" runat="server" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td nowrap>
                        <radCln:RadDatePicker ID="RdpTo" runat="server">
                            <DateInput Width="80px" Font-Size="10pt" >
                            </DateInput>
                            <Calendar ID="Cal2" runat="server" Skin="Inox"></Calendar>
                        </radCln:RadDatePicker>
                        </td>
                </tr>
            </table>
        </td>
        <td nowrap="nowrap">
        </td>
    </tr>
</table>
