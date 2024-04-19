<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlJyuryousho_H.ascx.cs" Inherits="m2mKoubai.Denpyou.CtlJyuryousho_H" %>
<link href="../MainStyle.css" rel="stylesheet" type="text/css" />
<table width="100%" class="def10">
    <tr>
        <td class="tc def14"><b>
            <table border="1" bordercolor="#000000" cellpadding="0" cellspacing="0" class="tc col"
                frame="below" rules="none">
                <tr>
                    <td class="def14 b w50">
                        物</td>
                    <td class="def14 b w50">
                        品</td>
                    <td class="def14 b w50">
                        受
                    </td>
                    <td class="def14 b w50">
                        領</td>
                    <td class="def14 b w50">
                        書</td>
                </tr>
            </table>
        </b></td>
    </tr>
    <tr>
        <td>
            <table width="100%">
                <tr>
                    <td width="50%">
                        <table class="def13">
                            <tr>
                                <td >
                                    〒<asp:Literal ID="LitYuubin" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td nowrap="noWrap" >
                                    <asp:Literal ID="LitJyusho" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap">
                                    <asp:Literal ID="LitShiiresakiMei" runat="server"></asp:Literal>
                                    御中</td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap">
                                                TEL：<asp:Literal ID="LitTel" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" style="height: 19px">
                                                FAX：<asp:Literal ID="LitFax" runat="server"></asp:Literal></td>
                            </tr>
                        </table>
                    </td>
                    <td align="right" >
                        <table class="def11 tr">
                            <tr>
                                <td>
                                    <asp:Literal ID="LitDate" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td>
                                    〒<asp:Literal ID="LitYuubinY" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td nowrap="noWrap">
                                    <asp:Literal ID="LitJyushoY" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap">
                                                <asp:Literal ID="LitKaishaMeiY" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap">
                                                TEL：<asp:Literal ID="LitTelY" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap">
                                                FAX：<asp:Literal ID="LitFaxY" runat="server"></asp:Literal></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="tr f10 hei30" valign="middle">
                        下記の通り受領致しました。</td>
                    
                </tr>
            </table>
        </td>
    </tr>
</table>
