<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlSeikyusho.ascx.cs" Inherits="m2mKoubai.Denpyou.CtlSeikyusho" %>
<link href="../MainStyle.css" rel="stylesheet" type="text/css" />
<table width="100%">
    <tr>
        <td>
            <asp:Literal ID="LitOwner" runat="server"></asp:Literal>御中</td>
    </tr>
    <tr>
        <td>
            <table align="right" class="def14 tr">
                <tr>
                    <td nowrap="noWrap">
                        <asp:Literal ID="LitDate" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td nowrap="noWrap">
                        <asp:Literal ID="LitShiiresakiMei" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td nowrap="noWrap">
                        登録番号　<asp:Literal ID="LitInvoiceRegNo" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td nowrap="noWrap">
                        〒<asp:Literal ID="LitYubinBangou" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td nowrap="noWrap">
                        <asp:Literal ID="LitAddress" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="hei50" valign="top">
                        <table class="def14">
                            <tr>
                                <td nowrap="noWrap">
                                    TEL：<asp:Literal ID="LitTel" runat="server"></asp:Literal></td>
                                <td nowrap="noWrap">
                                    FAX：<asp:Literal ID="LitFax" runat="server"></asp:Literal></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="hei50" valign="bottom">
            <table class="f12">
                <tr>
                    <td>
                        拝啓 貴社益々ご清栄のこととお慶び申し上げます。</td>
                </tr>
                <tr>
                    <td>
                        さて、<asp:Literal ID="LitMonth1" runat="server"></asp:Literal>の請求書を添付いたしますよろしくご査收のほど宜しくお願いします。</td>
                </tr>
                <tr>
                    <td>
                        なお、お振込みは下記口座までお願いします。</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="hei50" valign="bottom">
            記</td>
    </tr>
    <tr>
        <td align="center" class="hei50" valign="bottom">
        </td>
    </tr>
    <tr>
        <td align="center" class="hei150" valign="bottom">
            <table class="def14" border="2" style="border-color:Black">
                <tr><td style="border:0"><br /></td></tr>
                <tr>
                    <td style="border:0"></td>
                    <td class="tl nw" height="40" style="border:0">
                        ご請求金額（<asp:Literal ID="LitMonth2" runat="server"></asp:Literal>）</td>
                    <td class="tr nw" nowrap="noWrap" width="200" colspan="2" style="border:0">
                        <asp:Literal ID="LitKingaku" runat="server"></asp:Literal></td><td style="border:0"></td>
                </tr>
                <tr>
                    <td style="border:0">（</td>
                    <td height="40" style="border:0">
                        合計金額：<span lang="ja">　</span><asp:Literal ID="LitGoukei" runat="server"></asp:Literal></td>
                    <td style="width:30; border:0"><span lang="ja">　</span>＋<span lang="ja">　</span></td>
                    <td height="40" style="border:0">
                        消費税：<span lang="ja">　</span><asp:Literal ID="LitSyouhizei" runat="server"></asp:Literal></td><td style="border:0">）</td>
                </tr>
                <tr><td style="border:0"><br /></td></tr>
            </table>
            <table class="def14">
                <tr>
                    <td class="tc" colspan="2" height="60">
                        <u>振　込　先</u></td>
                </tr>
                <tr>
                    <td class="tl" height="40" width="150">
                        口座名義</td>
                    <td class="nw tl">
                        <asp:Literal ID="LitKouzamei" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="tl" height="40">
                        金融機関名</td>
                    <td class="nw tl" >
                       <asp:Literal ID="LitKinyuuKikanmei" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="tl" height="40">
                        口座番号</td>
                    <td class="nw tl">
                        <asp:Literal ID="LitKouzaBanggo" runat="server"></asp:Literal></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
