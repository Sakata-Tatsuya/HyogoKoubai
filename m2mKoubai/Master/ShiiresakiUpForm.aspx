<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShiiresakiUpForm.aspx.cs" Inherits="m2mKoubai.Master.ShiiresakiUpForm" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>仕入先登録</title>
    <link href="../MainStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
		    function $(id)
            {
                return document.getElementById(id);
            }
    
            function OnLoad(loadFlg)
            {
                if (window.opener != null) 
                {
                    if (loadFlg == 1) 
                    {
                        window.opener.Reload();
                    }
                }
            }
            function Touroku()
            {
                if (!TourokuChk(true))
                    return;
                if (confirm("登録しますか？"))
                {
                    document.getElementById('BtnTouroku').click();
                }
            }
            function TourokuChk(bool)
            {
                if(bool)
                {
                    var TbxShiireCode = document.getElementById('TbxShiireCode');
                    if(TbxShiireCode.value == "") 
                    {
                        alert("仕入先コードを入力してください");
                        return;
                    }
                }
                
                var TbxShiireName = document.getElementById('TbxShiireName');
                if(TbxShiireName.value == "")
                {
                    alert("仕入先名を入力してください");
                    TbxShiireName.focus();
                    return;
                }
                var TbxYuubin = document.getElementById('TbxYuubin');
                if(TbxYuubin.value == "")
                {
                    alert("郵便番号入力してください");
                    TbxYuubin.focus();
                    return;
                }
                var TbxTel = document.getElementById('TbxTel');
                if(TbxTel.value == "")
                {
                    alert("電話番号を入力してください");
                    TbxTel.focus();
                    return;
                }
                var TbxFax = document.getElementById('TbxFax');
                if(TbxFax.value == "")
                {
                    alert("FAX番号を入力してください");
                    TbxFax.focus();
                    return;
                }
                var DdlShimebi = document.getElementById('DdlShimebi');
                if(DdlShimebi.selectedIndex == 0)
                {
                    alert("支払締日を選択してください");
                    return;
                }
                var DdlYotebi1 = document.getElementById('DdlYotebi1');
                if(DdlYotebi1.selectedIndex == 0)
                {
                    alert("支払予定月を選択してください");
                    return;
                }
                var DdlYotebi2 = document.getElementById('DdlYotebi2');
                if(DdlYotebi2.selectedIndex == 0)
                {
                    alert("支払予定日を選択してください");
                    return;
                }
                return true;
            }
            function Koushin()
            {
                if (!TourokuChk(false))
                    return; 
                if (confirm("更新しますか？"))
                {
                    document.getElementById('BtnKoushin').click();
                }
            }
            function Close() 
            {
                window.close();
                if (window.opener != null) 
                {
                    window.opener.Reload();
                }
            }
            function OnRequestStart()
            {
                document.getElementById('Img1').style.display = '';
            }
            function OnResponseEnd()
            {
                document.getElementById('Img1').style.display = 'none';
            }
            function KeyCodeCheck()
            {
                var kc = event.keyCode;
                if((kc >= 37 && kc <= 40) || (kc >= 48 && kc <= 57) || (kc >= 96 && kc <= 105) || 
                    kc == 46 || kc == 8 || kc == 13 || kc == 9)
                    return true;
                else
                    return false;
            }
            function HankakuChk(tbxId, objName)
            {
                var count = 0;
                var val = tbxId.value;
                for( var i = 0; i < val.length; i++ )
                {
                    var s = val.substring(i, i + 1);
                    var c = s.charCodeAt(0);
                    if (c < 256 || (c >= 0xff61 && c <= 0xff9f)){}
                    else
                    {
                        alert(objName + 'は半角英数字のみで入力して下さい');
                        return false;
                    }
                }
                return true;
            }
		</script>
</head>
<body onload="OnLoad(<%=loadFlg %>);" class="bg0">
    <form id="form1" runat="server">
    <div>
       <table id="TblAll" runat="server" align="center" class="def9">
            <tr>
                <td align="center" class="hei20">
                    <asp:Label ID="LblMsg" runat="server" CssClass="b"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" class="tl">
                    <input id="BtnC" runat="server" class="w80 bg6" type="button" value="閉じる" onclick="Close()"/></td>
            </tr>
            <tr>
                <td>
                    <table id="TblMain" runat="server" border="1" bordercolor="#000000" class="col bg1">
                        <tr>
                            <td class="bg4">
                                仕入先コード</td>
                            <td class="def12" >                                
                                <asp:TextBox ID="TbxShiireCode" runat="server"  MaxLength="10"></asp:TextBox><asp:Literal ID="LitCode" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td class="bg4" >
                                仕入先名</td>
                            <td >
                                <asp:TextBox ID="TbxShiireName" runat="server" MaxLength="30" Width="250px" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                郵便番号</td>
                            <td colspan="1">
                                <asp:TextBox ID="TbxYuubin" runat="server" MaxLength="8" Width="80px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                住所</td>
                            <td colspan="1">
                                <asp:TextBox ID="TbxJyusho" runat="server" MaxLength="20" Width="250px" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                TEL</td>
                            <td colspan="1">
                                <asp:TextBox ID="TbxTel" runat="server" MaxLength="12" Width="250px" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                FAX</td>
                            <td colspan="1">
                                <asp:TextBox  ID="TbxFax" runat="server" MaxLength="12" Width="250px" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                口座名義</td>
                            <td colspan="1">
                                <asp:TextBox  ID="TbxKouzameigi" runat="server" MaxLength="12" Width="250px" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                金融機関名</td>
                            <td colspan="1">
                                <asp:TextBox  ID="TbxKinyuukikanMei" runat="server" MaxLength="12" Width="250px" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                口座番号</td>
                            <td colspan="1">
                                <asp:TextBox  ID="TbxKouzaBangou" runat="server" MaxLength="12" Width="250px" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="bg4" >
                                支払締日</td>
                            <td>
                                <asp:DropDownList ID="DdlShimebi" runat="server">   
                                <asp:ListItem Selected="True" Value="6">--</asp:ListItem>                                
                                    <asp:ListItem Value="1">5日</asp:ListItem>
                                    <asp:ListItem Value="2">10日</asp:ListItem>
                                    <asp:ListItem Value="3">15日</asp:ListItem>
                                    <asp:ListItem Value="4">20日</asp:ListItem>
                                    <asp:ListItem Value="5">25日</asp:ListItem>
                                    <asp:ListItem Value="0">末日</asp:ListItem>
                                </asp:DropDownList>
                                </td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                支払予定日</td>
                            <td>
                              <asp:DropDownList ID="DdlYotebi1" runat="server">
                               <asp:ListItem Selected="True" Value="0">--</asp:ListItem>
                                <asp:ListItem Value="1">翌月</asp:ListItem>
                                    <asp:ListItem Value="2">翌翌月</asp:ListItem>
                                   
                                </asp:DropDownList>
                                <asp:DropDownList ID="DdlYotebi2" runat="server">    
                                 <asp:ListItem Selected="True" Value="6">--</asp:ListItem>                                
                                    <asp:ListItem Value="1">5日</asp:ListItem>
                                    <asp:ListItem Value="2">10日</asp:ListItem>
                                    <asp:ListItem Value="3">15日</asp:ListItem>
                                    <asp:ListItem Value="4">20日</asp:ListItem>
                                    <asp:ListItem Value="5">25日</asp:ListItem>
                                    <asp:ListItem Value="0">末日</asp:ListItem>                                  
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                検収情報公開</td>
                            <td >
                                <asp:RadioButton ID="RbtnKoukai" runat="server" Checked="True" GroupName="Koukai"
                                    Text="公開する" />
                                <asp:RadioButton ID="RbtnKoukaiNashi" runat="server" GroupName="Koukai" Text="公開しない" /></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                納期回答催促メール</td>
                            <td>
                                <asp:RadioButton ID="RbtnSoushin" runat="server" Checked="True" GroupName="Soushin"
                                    Text="送信する" />
                                <asp:RadioButton ID="RbtnSoushinNashi" runat="server" GroupName="Soushin" Text="送信しない" /></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                仕入先情報更新許可</td>
                            <td colspan="1">
                                <asp:RadioButton ID="RbtnKyoka" runat="server" Checked="True" GroupName="Kyoka" Text="許可する" />
                                <asp:RadioButton ID="RbtnKyokaNashi" runat="server" GroupName="Kyoka" Text="許可しない" /></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                適格請求書発行事業者</td>
                            <td colspan="1">
                                <asp:RadioButton ID="RbtSumi" runat="server" Checked="True" GroupName="Invoice" Text="登録済" />
                                <asp:RadioButton ID="RbtMi" runat="server" GroupName="Invoice" Text="未登録" /></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                事業者番号</td>
                            <td colspan="1">
                                <asp:TextBox  ID="TbxInvoiceNo" runat="server" MaxLength="13" Width="250px" ></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <table id="TblBtn" runat="server">
                        <tr>
                            <td>
                                <input id="BtnT" runat="server" class="w80 bg6" type="button" value="登録" />
                                <input id="BtnK" runat="server" class="w80 bg6" type="button" value="更新" /></td>
                        </tr>
                    </table>
                    &nbsp;
                </td>
            </tr>
        </table>
    
    </div>
                    <asp:Button ID="BtnTouroku" runat="server" CssClass="none" OnClick="BtnTouroku_Click" />
                    <asp:Button ID="BtnKoushin" runat="server" CssClass="none" OnClick="BtnKoushin_Click" />
    </form>
</body>
</html>
