<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlNengappiFromTo.ascx.cs" Inherits="m2mKoubai.Common.CtlNengappiFromTo" %>

<table id="T" cellspacing="0" cellpadding="1" border="0">
    <tr>
        <td nowrap>
            <table id="TblFrom" cellspacing="0" cellpadding="0" border="0" runat="server">
                <tr>
                    <td nowrap>
                        <telerik:RadDatePicker ID="RdpFrom" MinDate="1950-01-01" runat="server" Skin="Web20" Width="120px" Culture="Japanese (Japan)">
                            <Calendar ShowRowHeaders="False" Skin="Web20"></Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="カレンダーを表示します。"></DatePopupButton>
                            <DateInput DisplayDateFormat="yyyy/MM/dd" Width="76px" DateFormat="yyyy/MM/dd" Font-Size="10pt" RangeValidation="Immediate"></DateInput>
                        </telerik:RadDatePicker>
                    </td>
                </tr>
            </table>
        </td>
        <td nowrap>
            <asp:DropDownList ID="DdlKikan" runat="server">
                <asp:ListItem Value="0">指定なし</asp:ListItem>
                <asp:ListItem Value="1">指定日</asp:ListItem>
                <asp:ListItem Value="2">以前</asp:ListItem>
                <asp:ListItem Value="3">以降</asp:ListItem>
                <asp:ListItem Value="4">から</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td nowrap>
            <table id="TblTo" cellspacing="0" cellpadding="0" border="0" runat="server">
                <tr>
                    <td nowrap>
                        <telerik:RadDatePicker ID="RdpTo" runat="server" MinDate="1950-01-01"
                            Skin="Web20" Width="120px" Culture="Japanese (Japan)">
                            <Calendar ShowRowHeaders="False" Skin="Web20"></Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="カレンダーを表示します。"></DatePopupButton>
                            <DateInput DisplayDateFormat="yyyy/MM/dd" Width="76px"
                                DateFormat="yyyy/MM/dd" Font-Size="10pt" RangeValidation="Immediate">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </td>
                </tr>
            </table>
        </td>
        <td nowrap></td>
    </tr>
</table>
