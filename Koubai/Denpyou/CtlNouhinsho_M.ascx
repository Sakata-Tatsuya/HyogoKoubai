<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlNouhinsho_M.ascx.cs" Inherits="Koubai.Denpyou.CtlNouhinsho_M" %>
<link href="../MainStyle.css" rel="stylesheet" type="text/css" />
<asp:GridView ID="G" runat="server" AutoGenerateColumns="False" BorderColor="Black"
    CssClass="def10 tc" Width="100%" OnRowDataBound="G_RowDataBound" >
    <Columns>
        <asp:BoundField HeaderText="����No" >
            <HeaderStyle Wrap="False" />
        </asp:BoundField>
        <asp:BoundField HeaderText="������">
            <ItemStyle CssClass="tc" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="�i��&lt;br&gt;�O���[�v"></asp:TemplateField>
        <asp:BoundField HeaderText="�i�ڃR�[�h">
        <HeaderStyle Wrap="False" />
        </asp:BoundField>
        <asp:BoundField HeaderText="�i�ږ�" >
        <HeaderStyle Wrap="False" />
            <ItemStyle CssClass="tl" />
        </asp:BoundField>
        <asp:BoundField HeaderText="����" >
        <HeaderStyle Wrap="False" />
            <ItemStyle CssClass="tr" />
        </asp:BoundField>
        <asp:BoundField HeaderText="�P��" >   
        <HeaderStyle Wrap="False" />
        </asp:BoundField>     
        <asp:BoundField HeaderText="�P��" >
        <HeaderStyle Wrap="False" />
            <ItemStyle CssClass="tr" />
        </asp:BoundField>
        <asp:BoundField HeaderText="���z" >
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
