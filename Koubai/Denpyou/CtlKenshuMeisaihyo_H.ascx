<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlKenshuMeisaihyo_H.ascx.cs" Inherits="Koubai.Denpyou.CtlKenshuMeisaihyo_H" %>
<link href="../MainStyle.css" rel="stylesheet" type="text/css" />
<table width="100%">
    <tr>
        <td class="tc">
            <table class="def10">
                <tr>
                    <td class="tc def14 b" nowrap="nowrap">
                        �������ו\</td>
                </tr>
                <tr>
                    <td>
                        �����F<asp:Literal ID="LitDate" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table width="100%">
                <tr>
                    <td width="50%">
                        <table class="def13">
                            <tr>
                                <td nowrap="noWrap">
                                    <asp:Literal ID="LitShiiresakiMei" runat="server"></asp:Literal>
                                    �䒆</td>
                            </tr>
                            <tr>
                                <td>
                                    ��<asp:Literal ID="LitYuubin" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td nowrap="noWrap">
                                    <asp:Literal ID="LitJyusho" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap">
                                                TEL�F<asp:Literal ID="LitTel" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap">
                                                FAX�F<asp:Literal ID="LitFax" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="right">
                        <table class="def11 tr">
                            <tr>
                                <td>
                                    ��<asp:Literal ID="LitYuubinY" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td nowrap="noWrap">
                                    <asp:Literal ID="LitJyushoY" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="def11">
                                        <tr>
                                            <td align="right" colspan="1" nowrap="noWrap">
                                                <asp:Literal ID="LitKaishaMeiY" runat="server"></asp:Literal></td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="1" nowrap="nowrap">
                                                TEL�F<asp:Literal ID="LitTelY" runat="server"></asp:Literal></td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="1" nowrap="nowrap">
                                                FAX�F<asp:Literal ID="LitFaxY" runat="server"></asp:Literal></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tl f10 hei30" colspan="2">
                        ���L�̒ʂ茟�����A�����|�����Ƃ��ċM�����Ɍv�ア�����܂����̂ł��A���\���グ�܂��B</td>
                </tr>
            </table>
        </td>
    </tr>
</table>
