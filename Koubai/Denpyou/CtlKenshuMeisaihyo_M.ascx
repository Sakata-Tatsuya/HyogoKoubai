<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlKenshuMeisaihyo_M.ascx.cs" Inherits="Koubai.Denpyou.CtlKenshuMeisaihyo_M" %>
<link href="../MainStyle.css" rel="stylesheet" type="text/css" />
<asp:GridView ID="G" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="def10 tc" OnRowDataBound="G_RowDataBound">
    <Columns>
        <asp:BoundField HeaderText="発注No" />
        <asp:BoundField HeaderText="品目コード" />
        <asp:BoundField HeaderText="品目名" >
            <ItemStyle CssClass="tr" />
        </asp:BoundField>
        <asp:BoundField HeaderText="注文数量" >
            <ItemStyle CssClass="tr" />
        </asp:BoundField>
        <asp:BoundField HeaderText="単価" >
            <ItemStyle CssClass="tr" />
        </asp:BoundField>
        <asp:BoundField HeaderText="注文金額" >
            <ItemStyle CssClass="tr" />
        </asp:BoundField>
        <asp:BoundField HeaderText="納入場所" />
        <asp:BoundField HeaderText="受入日" />
        <asp:BoundField HeaderText="入荷数量" >
            <ItemStyle CssClass="tr" />
        </asp:BoundField>
    </Columns>
</asp:GridView>
