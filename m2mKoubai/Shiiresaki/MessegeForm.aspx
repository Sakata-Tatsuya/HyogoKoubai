<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessegeForm.aspx.cs" Inherits="m2mKoubai.Shiiresaki.MessegeForm" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>メッセージ</title>
    <link href="../MainStyle.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript">    
    function $(id)
    {
        return document.getElementById(id);
    }
    function Close()
    {
        window.close();
    }
    function Touroku()
    {
        var msg = document.getElementById("TbxMsg").value;
        if (msg == "")
        {
            alert("メッセージを入力して下さい");
            return;
        }
        if (msg.length > 100)
        {
            alert("メッセージは100文字以内で入力して下さい");
            return;
        }
        if (confirm(document.getElementById('BtnT').value+"しますか？"))
        {
            document.getElementById('BtnReg').click();
        }
    }
    function Clear()
    {
        document.getElementById('TbxMsg').value = "";
        document.getElementById('TbxMsg').focus();
    }
    function OnLoad(loadFlg)
    {
        document.getElementById('TbxMsg').focus();
        if (window.opener != null)
        {
            if (loadFlg == 1)
            {
                window.opener.Reload();
            }
        }
    }
    </script>
</head>
<body onload="OnLoad(<%=loadFlg%>)" class="bg99">
    <form id="form1" runat="server">
    <div>
        <table id="TblAll" runat="server" align="center" border="0" cellpadding="1" cellspacing="1" class="def9">
            <tr>
                <td align="center" class="hei20">
                    <asp:Label ID="LblMsg" runat="server" CssClass="b"></asp:Label></td>
            </tr>
            <tr>
                <td align="center" class="nowrap">
                    <table width="100%">
                        <tr align="center">
                            <td class="tr" width="60%">
                            </td>
                            <td class="tr">
                                <input id="BtnC" runat="server" onclick="Close()" type="button" value="閉じる" class="w80 bg98" /></td>
                        </tr>
                    </table>
                    <table id="TblMsg" runat="server" border="1" bordercolor="#000000" class="col def9" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="bg7 b">
                                メッセージ</td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TbxMsg" runat="server" MaxLength="100" Rows="6" TextMode="MultiLine" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <input id="BtnClear" runat="server" class="w80 bg98" type="button" value="クリア" />
                    <input id="BtnT" runat="server" class="w80 bg98" type="button" value="登録" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="G" runat="server" AutoGenerateColumns="False" OnRowCommand="G_RowCommand"
                        OnRowDataBound="G_RowDataBound" CssClass="def9">
                        <Columns>
                            <asp:BoundField HeaderText="送信日時">
                                <ItemStyle CssClass="tc" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="メッセージ">
                                <ItemStyle HorizontalAlign="Left" Width="350px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="開封日時">
                                <ItemStyle CssClass="tc" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="BE" runat="server" Text="開封" />
                                    <asp:Button ID="BD" runat="server" Text="削除" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="bg7" />
                         <RowStyle CssClass="bg1" />
                         <AlternatingRowStyle CssClass="bg2" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
    </div>
        <asp:Button ID="BtnReg" runat="server" OnClick="BtnReg_Click" />
    </form>
</body>
</html>
