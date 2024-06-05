<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderShousaiForm.aspx.cs" Inherits="m2mKoubai.Shiiresaki.OrderShousaiForm" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>発注詳細画面</title>
    <link href="../MainStyle.css" rel="stylesheet" type="text/css" />
</head>
<body class="bg99">
    <form id="form1" runat="server">
    <div>
        <table id="TblAll" runat="server" align="center" class="def9" cellpadding="0" cellspacing="0">
            <tr>
               
                <td align="center" colspan="2" class="hei20">
                    <asp:Label ID="LblErrMsg" runat="server" CssClass="b"></asp:Label></td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="LblMsg" runat="server" CssClass="b" ForeColor="Red"></asp:Label></td>
                <td align="left" class="hei30">
                    <input id="BtnC" runat="server" class="w80 bg98" type="button" value="閉じる" onclick="window.close();" /></td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <table id="TblMain" runat="server" border="1" bordercolor="#000000" class="col tl bg1 def12">
                        <tr>
                            <td class="bg3" >
                                発注No</td>
                            <td nowrap="nowrap">
                                <asp:Literal ID="LitHacchuNo" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td bgcolor="#66cc99" class="bg3">
                                発注日</td>
                            <td nowrap="nowrap">
                                <asp:Literal ID="LitHacchuubi" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td bgcolor="#66cc99" class="bg3">
                                仕入先コード</td>
                            <td >
                                <asp:Literal ID="LitShiireCode" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td class="bg3">
                                仕入先名</td>
                            <td nowrap="nowrap">
                                <asp:Literal ID="LitShiireName" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td class="bg3">
                                品目グループ</td>
                            <td nowrap="nowrap">
                                <asp:Literal ID="LitKubun" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td class="bg3" >
                                品目コード</td>
                            <td nowrap="nowrap">
                                <asp:Literal ID="LitBuhinCode" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td class="bg3" >
                                品目名</td>
                            <td nowrap="nowrap">
                                <asp:Literal ID="LitBuhinName" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td class="bg3" >
                                単価</td>
                            <td >
                                <asp:Label ID="LblTanka" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="bg3">
                                数量</td>
                            <td nowrap="nowrap">
                                <asp:Literal ID="LitSuuryou" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td class="bg3">
                                単位</td>
                            <td nowrap="nowrap">
                                <asp:Literal ID="LitTani" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td class="bg3">
                                注文金額</td>
                            <td nowrap="nowrap">
                                <asp:Label ID="LblKingaku" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="bg3">
                                納入場所</td>
                            <td nowrap="nowrap">
                                <asp:Literal ID="LitNBashoMei" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td class="bg3">
                                発注担当者コード</td>
                            <td nowrap="nowrap">
                                <asp:Literal ID="LitTantoushaID" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td class="bg3">
                                発注担当者名</td>
                            <td nowrap="nowrap">
                                <asp:Literal ID="LitTantoushaMei" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td class="bg3">
                                備考</td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TbxBikou" runat="server" Height="70px" MaxLength="200" ReadOnly="True"
                                    TextMode="MultiLine" Width="300px"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
