<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlSeikyuMeisaisho_H.ascx.cs" Inherits="m2mKoubai.Denpyou.CtlSeikyuMeisaisho_H" %>
<link href="../MainStyle.css" rel="stylesheet" type="text/css" />
<table width="100%">
    <tr>
        <td class="tc def14 b">
            <table border="1" bordercolor="#000000" cellpadding="0" cellspacing="0" class="tc col"
                frame="below" rules="none">
                <tr>
                    <td class="def14 b w50">
                        ��</td>
                    <td class="def14 b w50">
                        ��
                    </td>
                    <td class="def14 b w50">
                        ��</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="def10">
            <table width="100%">
                <tr>
                    <td width="50%">
                        <table class="def13">
                            <tr>
                                <td nowrap="noWrap">
                                    ��<asp:Literal ID="LitYubinH" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="noWrap">
                                    <asp:Literal ID="LitAddressH" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap">
                                    <asp:Literal ID="LitKaishaMei" runat="server"></asp:Literal>
                                    �䒆</td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap">
                                    TEL�F<asp:Literal ID="LitTelH" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap">
                                    FAX�F<asp:Literal ID="LitFaxH" runat="server"></asp:Literal></td>
                            </tr>
                        </table>
                    </td>
                    <td align="right">
                        <table class="def11 tr">
                            <tr>
                                <td nowrap="noWrap">
                                    <asp:Literal ID="LitDate" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="noWrap">
                                    <asp:Literal ID="LitShiiresakiMei" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap">
                                    �o�^�ԍ��F<asp:Literal ID="LitInvoiceRegNo" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td nowrap="noWrap">
                                    ��<asp:Literal ID="LitYubinBangou" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td nowrap="noWrap">
                                    <asp:Literal ID="LitAddress" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap">
                                    TEL�F<asp:Literal ID="LitTel" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td>
                                    FAX�F<asp:Literal ID="LitFax" runat="server"></asp:Literal></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tr f10 hei30" colspan="2">
                        ���x���肪�Ƃ��������܂����L�̒ʂ�䐿���\���グ�܂��B</td>
                </tr>
            </table>
        </td>
    </tr>
</table>
