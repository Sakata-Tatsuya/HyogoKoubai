<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlNouhinsho_M.ascx.cs" Inherits="Koubai.Denpyou.CtlNouhinsho_M" %>
<link href="../MainStyle.css" rel="stylesheet" type="text/css" />
<asp:GridView ID="G" runat="server" AutoGenerateColumns="False" BorderColor="Black"
    CssClass="def10 tc" Width="100%" OnRowDataBound="G_RowDataBound" >
    <Columns>
        <asp:BoundField HeaderText="発注No" >
            <HeaderStyle Wrap="False" />
        </asp:BoundField>
        <asp:BoundField HeaderText="発注日">
            <ItemStyle CssClass="tc" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="品目&lt;br&gt;グループ"></asp:TemplateField>
        <asp:BoundField HeaderText="品目コード">
        <HeaderStyle Wrap="False" />
        </asp:BoundField>
        <asp:BoundField HeaderText="品目名" >
        <HeaderStyle Wrap="False" />
            <ItemStyle CssClass="tl" />
        </asp:BoundField>
        <asp:BoundField HeaderText="数量" >
        <HeaderStyle Wrap="False" />
            <ItemStyle CssClass="tr" />
        </asp:BoundField>
        <asp:BoundField HeaderText="単位" >   
        <HeaderStyle Wrap="False" />
        </asp:BoundField>     
        <asp:BoundField HeaderText="単価" >
        <HeaderStyle Wrap="False" />
            <ItemStyle CssClass="tr" />
        </asp:BoundField>
        <asp:BoundField HeaderText="金額" >
        <HeaderStyle Wrap="False" />
            <ItemStyle CssClass="tr" />
        </asp:BoundField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Image ID="Img" runat="server" Height="15px" Width="120px" />
            </ItemTemplate>
            <ItemStyle CssClass="tc" />
        </asp:TemplateField>
        
    </Columns>
</asp:GridView>
