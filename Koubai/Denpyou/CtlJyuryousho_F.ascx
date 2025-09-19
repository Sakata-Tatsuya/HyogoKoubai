<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlJyuryousho_F.ascx.cs" Inherits="Koubai.Denpyou.CtlJyuryousho_F" %>
<link href="../MainStyle.css" rel="stylesheet" type="text/css" />
<table align="right" width="100%">
    <tr>
        <td  style="border:0" align="right" valign="bottom">
            * åyå∏ê≈ó¶ëŒè€
        </td>
        <td rowspan="2" valign="bottom" align="right">
            <table border="1" bordercolor="#000000" class="col">
                <tr>                    
                    <td height="80" width="80" class="nw">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="right" valign="bottom">
            <table border="1" bordercolor="#000000" class="col tc def12" >
                <tr>
                    <td nowrap="noWrap" width="60">
                        çáåv</td>
                    <td class="tr nw" nowrap="noWrap" width="80">
                        <asp:Literal ID="LitGoukei" runat="server"></asp:Literal></td>
                    <td nowrap="noWrap" width="60">
                        è¡îÔê≈</td>
                    <td class="tr nw" nowrap="noWrap" width="80">
                         <asp:Literal ID="LitShohizei" runat="server"></asp:Literal></td>
                    <td nowrap="noWrap" width="60">
                        ëççáåv</td>
                    <td class="tr nw" width="80">
                         <asp:Literal ID="LitSouGoukei" runat="server"></asp:Literal></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
