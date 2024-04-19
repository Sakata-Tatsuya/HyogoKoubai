<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlNouhinsho_H.ascx.cs" Inherits="m2mKoubai.Denpyou.CtlNouhinsho_H" %>
<link href="../MainStyle.css" rel="stylesheet" type="text/css" />
<table width="100%">
    <tr>
        <td class="tc def14">
            <b>
                <table border="1" bordercolor="#000000" cellpadding="0" cellspacing="0" class="tc col"
                    frame="below" rules="none">
                    <tr>
                        <td class="def14 b w50">
                            納</td>
                        <td class="def14 b w50">
                            品
                        </td>
                        <td class="def14 b w50" >
                            書</td>
                    </tr>
                </table>
            </b></td>
    </tr>
    <tr>
        <td>
            <table width="100%" class="def10">
                <tr>
                    <td width="50%">
                        &nbsp;<table class="def13">
                            <tr>
                                <td nowrap="noWrap">
                                    〒<asp:Literal ID="LitYuubinY" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="noWrap">
                                    <asp:Literal ID="LitJyushoY" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap">
                                                <asp:Literal ID="LitKaishaMeiY" runat="server"></asp:Literal>&nbsp;
                                                御中</td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap">
                                                TEL：<asp:Literal ID="LitTelY" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap">
                                                FAX：<asp:Literal ID="LitFaxY" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="right" >
                      <table class="def11 tr">
                            <tr>
                                <td >
                                    <asp:Literal ID="LitDate" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td >
                                    </td>
                            </tr>
                            <tr>
                                <td nowrap="noWrap">
                                    <asp:Literal ID="LitShiiresakiMei" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td>
                                    〒<asp:Literal ID="LitYuubin" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="LitJyusho" runat="server"></asp:Literal></td>
                            </tr>
                          <tr>
                              <td>
                                                TEL：<asp:Literal ID="LitTel" runat="server"></asp:Literal></td>
                          </tr>
                          <tr>
                              <td style="height: 16px">
                                                FAX：<asp:Literal ID="LitFax" runat="server"></asp:Literal></td>
                          </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="tr hei50" valign="middle">
                        毎度ありがとうございます下記の通り納品致しますのでご査収ください。</td>                    
                </tr>
            </table>
        </td>
    </tr>
</table>
