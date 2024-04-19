<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlMyPager.ascx.cs" Inherits="m2mKoubai.Common.CtlMyPager" %>
<table id="T"  border="0" cellpadding="2" cellspacing="0" class="def2" runat="server">
    <tr>
        <td nowrap="nowrap">
            <asp:Button ID="BtnPrev" runat="server" Text="前のページ" OnClick="BtnPrev_Click"/>
        </td>
        <td nowrap="nowrap">
            <asp:DropDownList ID="DdlPage" runat="server" AutoPostBack="true" CssClass="def2" 
            OnSelectedIndexChanged="DdlPage_SelectedIndexChanged">
            </asp:DropDownList></td>
        <td nowrap="nowrap" >
            /</td>
        <td nowrap="nowrap" >
            <asp:Literal ID="LitPageCount" runat="server"></asp:Literal></td>
        <td nowrap="nowrap" >
            <asp:Button ID="BtnNext" runat="server" Text="次のページ" OnClick="BtnNext_Click"/></td>
        <td nowrap="nowrap" >
            <asp:Literal ID="LitCounter" runat="server"></asp:Literal></td>
    </tr>
</table>
