<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlGenpinhyou.ascx.cs" Inherits="m2mKoubai.Denpyou.CtlGenpinhyou" %>
<link href="../MainStyle.css" rel="stylesheet" type="text/css" />
<table border="1" bordercolor="#000000" class="col def12" width="50%">
    <tr>
        <td colspan="2" class="def14 b tc" height="30" width="300" nowrap="noWrap">
            ���i�[</td>
    </tr>
    <tr>
        <td height="25" class="tl" nowrap="noWrap">
            ���͂���</td>
        <td nowrap="noWrap" class="tl">
            <asp:Literal ID="LitTodokesaki" runat="server"></asp:Literal>�䒆</td>
    </tr>
    <tr>
        <td height="25" class="tl" nowrap="noWrap">
            ����No</td>
        <td class="tl" nowrap="noWrap">
            <asp:Literal ID="LitHacchuuNo" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <td height="25" class="tl" nowrap="noWrap">
            �i�ڃR�[�h</td>
        <td class="tl" nowrap="noWrap">
            <asp:Literal ID="LitBuhinCode" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <td height="25" class="tl">
            �i�ږ�</td>
        <td class="tl">
            <asp:Literal ID="LitHinmei" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <td colspan="2" class="tc">
            <asp:Image ID="Img1" runat="server" Width="120px" CssClass="hei30" /></td>
        
    </tr>
    <tr>
        <td colspan="2" class="tc" height="25">
            <asp:Literal ID="LitShiiresaki" runat="server"></asp:Literal></td>
        
    </tr>
</table>
