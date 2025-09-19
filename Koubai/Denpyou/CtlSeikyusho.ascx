<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlSeikyusho.ascx.cs" Inherits="Koubai.Denpyou.CtlSeikyusho" %>
<link href="../MainStyle.css" rel="stylesheet" type="text/css" />
<table width="100%">
    <tr>
        <td>
            <asp:Literal ID="LitOwner" runat="server"></asp:Literal>�䒆</td>
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
                        �o�^�ԍ��@<asp:Literal ID="LitInvoiceRegNo" runat="server"></asp:Literal></td>
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
                    <td class="hei50" valign="top">
                        <table class="def14">
                            <tr>
                                <td nowrap="noWrap">
                                    TEL�F<asp:Literal ID="LitTel" runat="server"></asp:Literal></td>
                                <td nowrap="noWrap">
                                    FAX�F<asp:Literal ID="LitFax" runat="server"></asp:Literal></td>
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
                        �q�[ �M�Љv�X�����h�̂��ƂƂ��c�ѐ\���グ�܂��B</td>
                </tr>
                <tr>
                    <td>
                        ���āA<asp:Literal ID="LitMonth1" runat="server"></asp:Literal>�̐�������Y�t�������܂���낵���������̂قǋX�������肢���܂��B</td>
                </tr>
                <tr>
                    <td>
                        �Ȃ��A���U���݂͉��L�����܂ł��肢���܂��B</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="hei50" valign="bottom">
            �L</td>
    </tr>
    <tr>
        <td align="center" class="hei50" valign="bottom">
        </td>
    </tr>
    <tr>
        <td align="center" class="hei150" valign="bottom">
            <table class="def14" border="1" style="border-color:Black">
                <tr><td style="border:0"><br /></td></tr>
                <tr>
                    <td style="border:0"></td>
                    <td class="tl nw" height="40" style="border:0">
                        ���������z�i<asp:Literal ID="LitMonth2" runat="server"></asp:Literal>�j</td>
                    <td class="tr nw" nowrap="noWrap" width="200" colspan="2" style="border:0">
                        <asp:Literal ID="LitKingaku" runat="server"></asp:Literal></td><td style="border:0"></td>
                </tr>
                <tr>
                    <td style="border:0"></td>
                    <td colspan="2" style="border:0">
                        <asp:GridView id="GZ" runat="server" Width="100%" AutoGenerateColumns="False"  CssClass="f10" OnRowDataBound="GZ_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Literal ID="TtlShubetu" runat="server" Text="�@"></asp:Literal>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Literal ID="LitShubetu" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="tl" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Literal ID="TtlKingaku" runat="server" Text="�@���v���z"></asp:Literal>
                                    </HeaderTemplate>
                                    <HeaderStyle CssClass="tr" />
                                    <ItemTemplate>
                                        <asp:Literal ID="LitKingaku" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="tr" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Literal ID="TtlZeigaku" runat="server" Text="�@����Ŋz"></asp:Literal>
                                    </HeaderTemplate>
                                    <HeaderStyle CssClass="tr" />
                                    <ItemTemplate>
                                        <asp:Literal ID="LitZeigaku" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="tr" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
<%--                    <td style="border:0">�i</td>
                    <td height="40" style="border:0">
                        ���v���z�F<span lang="ja">�@</span><asp:Literal ID="LitGoukei" runat="server"></asp:Literal></td>
                    <td style="width:30px; border:0"><span lang="ja">�@</span>�{<span lang="ja">�@</span></td>
                    <td height="40" style="border:0">
                        ����ŁF<span lang="ja">�@</span><asp:Literal ID="LitSyouhizei" runat="server"></asp:Literal></td><td style="border:0">�j</td>--%>
                </tr>
                <tr><td style="border:0"><br /></td></tr>
            </table>
            <table class="def14">
                <tr>
                    <td class="tc" colspan="2" height="60">
                        <u>�U�@���@��</u></td>
                </tr>
                <tr>
                    <td class="tl" height="40" width="150">
                        �������`</td>
                    <td class="nw tl">
                        <asp:Literal ID="LitKouzamei" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="tl" height="40">
                        ���Z�@�֖�</td>
                    <td class="nw tl" >
                       <asp:Literal ID="LitKinyuuKikanmei" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="tl" height="40">
                        �����ԍ�</td>
                    <td class="nw tl">
                        <asp:Literal ID="LitKouzaBanggo" runat="server"></asp:Literal></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
