<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlKenshuMeisaihyo_M.ascx.cs" Inherits="Koubai.Denpyou.CtlKenshuMeisaihyo_M" %>
<link href="../MainStyle.css" rel="stylesheet" type="text/css" />
<asp:GridView ID="G" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="def10 tc" OnRowDataBound="G_RowDataBound">
    <Columns>
        <asp:BoundField HeaderText="����No" />
        <asp:BoundField HeaderText="�i�ڃR�[�h" />
        <asp:BoundField HeaderText="�i�ږ�" >
            <ItemStyle CssClass="tr" />
        </asp:BoundField>
        <asp:BoundField HeaderText="��������" >
            <ItemStyle CssClass="tr" />
        </asp:BoundField>
        <asp:BoundField HeaderText="�P��" >
            <ItemStyle CssClass="tr" />
        </asp:BoundField>
        <asp:BoundField HeaderText="�������z" >
            <ItemStyle CssClass="tr" />
        </asp:BoundField>
        <asp:BoundField HeaderText="�[���ꏊ" />
        <asp:BoundField HeaderText="�����" />
        <asp:BoundField HeaderText="���א���" >
            <ItemStyle CssClass="tr" />
        </asp:BoundField>
    </Columns>
</asp:GridView>
