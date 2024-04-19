<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="m2mKoubai.LoginForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ログイン</title>
    <link href="MainStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
    function $(id)
    {
        return document.getElementById(id);
    }
    function Load()
    {
        var tbx = $("TbxID");
        if(tbx != null)
        {
            tbx.focus();
        }
    }
    </script>
</head>
<body onload="Load();" class="bg0">
    <form id="form1" runat="server">
        <table align="center" class="mt60" cellpadding="0" cellspacing="0">
            <tr>
                <td class="f35" >
                    <font color="#ff3333" style="font-family: 'Times New Roman'"><b>m2m電子発注 Web-EDI</b></font></td>
            </tr>
            <tr>
                <td class="tc">
                    <table align="center" class="mt10 def10 " id="TblLogin" runat="server">
                        <tr>
                            <td>
                                <font color="#000000">ログインID</font></td>
                            <td>
                                <asp:TextBox ID="TbxID" runat="server" Width="120px"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <font color="#000000">パスワード</font></td>
                            <td>
                                <asp:TextBox ID="TbxPass" runat="server" Width="120px" TextMode="Password"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="BtnTouroku" runat="server" Text="ログイン" CssClass="w100 bg6 " OnClick="BtnTouroku_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tc" height="40">
                                <asp:Label ID="LblErrMsg" runat="server" CssClass="def11 b" ForeColor="Red"></asp:Label></td>
            </tr>
        </table>
        <table align="center">
            <tr>
                <td bgcolor="#ffffff">
                    <asp:GridView ID="G" runat="server" AutoGenerateColumns="False" Width="400px" CssClass="f10" OnRowDataBound="G_RowDataBound">
                        <Columns>
                            <asp:BoundField  HeaderText="インフォメーション" >
                                <ItemStyle CssClass="tl" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="bg3" />
                        <RowStyle CssClass="bg1" />
                        <AlternatingRowStyle CssClass="bg2" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
