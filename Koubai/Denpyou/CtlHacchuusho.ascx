<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlHacchuusho.ascx.cs" Inherits="Koubai.Denpyou.CtlHacchuusho" %>
<link href="../MainStyle.css" rel="stylesheet" type="text/css" />
<table id="T" width="100%" runat="server">
    <tr>
        <td align="center" >
            <table class="tc col" border="1" bordercolor="#000000" cellpadding="0" cellspacing="0" frame="below" rules="none">
                <tr>
                    <td class="def14 b w50">
                        ��</td>
                    <td class="def14 b w50">
                        ��
                    </td>
                    <td class="def14 b w50" style="width: 50px">
                        ��</td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td>
                        <table align="left" class="tl def13 nw">
                            <tr>
                                <td class="f10">
                                    ��<asp:Literal ID="LitYubin" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td class="f10">
                                    <asp:Literal ID="LitJyusyo" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td class="f12">
                                    <asp:Literal ID="LitKaisha" runat="server"></asp:Literal>&nbsp;�䒆</td>
                            </tr>
                            <tr>
                                <td class="f10" style="height: 15px">
                                    TEL:<asp:Literal ID="LitShiireTEL" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td class="f10">
                                    FAX:<asp:Literal ID="LitShiireFAX" runat="server"></asp:Literal></td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table align="right" class="tr def11 col nw" border="1" bordercolor="#000000" cellpadding="0" cellspacing="0" frame="below" rules="none" width="50%">
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Literal ID="LitDate" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td class="f11">
                                </td>
                                <td class="f11" nowrap="nowrap">
                                    <asp:Literal ID="LitKaishaMeiH" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td class="f10" style="height: 13px">
                                </td>
                                <td class="f10" style="height: 13px">
                                    TEL:<asp:Literal ID="LitTelH" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td class="f10">
                                </td>
                                <td class="f10">
                                    FAX:<asp:Literal ID="LitFaxH" runat="server"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td class="f11 hei25 tl " valign="bottom" nowrap="nowrap">
                                    �����S����:</td>
                                <td class="f11 tc" valign="bottom" nowrap="nowrap">
                                    <asp:Literal ID="LitTantousha" runat="server"></asp:Literal></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </td>
    </tr>
    <tr>
        <td align="left">
            <table>
                <tr>
                    <td class="def11">
                        ������ς����b�ɂȂ��Ă���܂��B<br />
                        ���L�����v���܂��̂ŁA����z�̂قǋX�������肢�v���܂��B</td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td>
            <asp:GridView ID="G" runat="server" AutoGenerateColumns="False" CssClass="def9"
                OnRowDataBound="G_RowDataBound" Width="100%">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <table border="1" frame="below" width="100%" class="def9 col ">
                                <tr>
                                    <td class="tc b" width="10%">
                                        ���sNo</td>
                                    <td class="tc b" width="25%">
                                        �i�ڃR�[�h</td>
                                    <td class="tc b" width="10%">
                                        ����</td>
                                    <td class="tc b" rowspan="2" width="10%">
                                        ���v</td>
                                    <td class="tc b" rowspan="2" width="5%">
                                        �P��</td>
                                    <td class="tc b" width="25%">
                                        �[��</td>
                                </tr>
                                <tr>
                                    <td class="tc b">
                                        ������</td>
                                    <td class="tc b">
                                        �i��</td>
                                    <td class="tc b">
                                        �P��</td>
                                    <td class="tc b">
                                        �[���ꏊ</td>
                                    
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table border="1" width="100%" class="f9 col" frame="below">
                                <tr>
                                    <td class="tc hei15" width="10%">
                                        <asp:Literal ID="LitNo" runat="server"></asp:Literal></td>
                                    <td class="tl" width="25%">
                                       <asp:Literal ID="LitCode" runat="server"></asp:Literal></td>
                                    <td class="tr" width="10%">
                                        <asp:Literal ID="LitSuu" runat="server"></asp:Literal></td>
                                    <td class="tr hei30" rowspan="2" width="10%">
                                         <asp:Literal ID="LitKei" runat="server"></asp:Literal></td>
                                    <td class="tc" rowspan="2" width="5%">
                                         <asp:Literal ID="LitTani" runat="server"></asp:Literal></td>
                                    <td class="tc" width="25%">
                                         <asp:Literal ID="LitNouki" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td class="tc hei15">
                                        <asp:Literal ID="LitDate" runat="server"></asp:Literal></td>
                                    <td class="tl">
                                        <asp:Literal ID="LitHinmei" runat="server"></asp:Literal></td>
                                    <td class="tr">
                                        <asp:Literal ID="LitTanka" runat="server"></asp:Literal></td>
                                    <td class="tc">
                                        <asp:Literal ID="LitBasyo" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td colspan="6" height="45" valign="top" class="tl">
                                        <asp:Label ID="Label1" runat="server" Text="���l" BorderColor="Black" BorderStyle="Double" BorderWidth="1px" CssClass="vt b"></asp:Label>
                                        <asp:Literal ID="LitBikou" runat="server"></asp:Literal></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <ItemStyle CssClass="col" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>