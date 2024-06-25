<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderShousaiForm.aspx.cs" Inherits="m2mKoubai.Order.OrderShousaiForm" %>
<%--<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>--%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>発注詳細画面</title>
    <link href="../MainStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function $(id)
        {
            return document.getElementById(id);
        }

        function Cancel()
        {
            if (confirm('キャンセルしますか？'))
            {
                if (confirm('本当にキャンセルしますか？')) 
                {
                    document.getElementById('HidProcess').value = 'cancel';
                    document.getElementById('BtnProcess').click();
                }
            }
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
        function Koushin(suu,tanka)
        {
            var tbxTanka = document.getElementById('TbxT');
            var tbxSuuryou = document.getElementById('TbxS');
            if (parseInt(tbxTanka.value) == 0)
            {
                alert('単価は0以上で入力して下さい');
                tbxTanka.focus();
                return ;
            }
            else if(parseFloat(tbxTanka.value) == parseFloat(tanka))
            {
                alert("単価に変更がありません");
                tbxTanka.focus();  
                return;
            }
            else if (!CheckDecimal(tbxTanka.value, tanka))
            {
                tbxTanka.focus();
                return ;
            }
            if (tbxSuuryou.value == '0')
            {
                alert('数量は0以上で入力して下さい');
                 tbxSuuryou.focus();
                return;
            } 
            else if(parseInt(tbxSuuryou.value.replace(",","")) == parseInt(suu.replace(",","")))
            {
                alert('数量に変更がありません');
                 tbxSuuryou.focus();
                return;
            }
            else if (!CheckSuu(tbxSuuryou, '数量'))
            {
                tbxSuuryou.focus();
                return;
            }
            if(tbxTanka.value == '' && tbxSuuryou.value == '')
            {
                alert("更新するデータを入力してください");
                return;
            }
            else
            {
                if (confirm('更新しますか？'))
                {
                    document.getElementById('HidProcess').value = 'koushin';
                    document.getElementById('BtnProcess').click();
                }
            }
        }
         // 数値チェック
        function CheckSuu(tbx,str)
        {
            if (tbx.value.match(/[^0-9,]/) != null)
            {
                alert(str+"は半角数字のみで入力して下さい");
                tbx.focus();
                return false;
            }
            return true;
        }
        function CheckDecimal(deci,tanka)
        {
            if (deci == '')
            {
                return true;
            }
            else
            {
                if (deci.match( /[^0-9.,]/ ) != null)
                {
                    alert('単価の入力値が正しくありません');
                    return false;
                }
                var valAry = tanka.split('.');
                var deciAry = deci.split('.');
                if (deciAry.length > 2)
                {
                    alert('小数点は1つだけ入力可能です');
                    return false;
                }
                if (deciAry.length == 2 && (deciAry[0].length == 0 || deciAry[1].length == 0))
                {
                    alert('単価の入力値が正しくありません');
                    return false;
                }
                if (deciAry.length < 3 && deciAry[0].length > 8)
                {
                    alert('単価の整数部は8桁以内で入力して下さい');
                    return false;
                }
                if (deciAry.length == 2 && deciAry[1].length > 2)
                {
                    alert('単価の小数部は2桁以内で入力して下さい');
                    return false;
                }
                if (deciAry.length == 2 && parseInt(deciAry[0]) == 0 && parseInt(deciAry[1]) == 0)
                {
                    alert('単価は0以上で入力して下さい');
                    return false;
                }
                return true;
            }
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
    </script>
</head>
<body class="bg0" onload="OnLoad(<%=loadFlg%>)">
    <form id="form1" runat="server">
    <div>
       <table id="TblAll" runat="server" align="center" class="def9" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" colspan="2" width="50%" class="hei20">
                    <asp:Label ID="LblErrMsg" runat="server" CssClass="b"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" width="50%">
                    <asp:Label ID="LblMsg" runat="server" CssClass="b" ForeColor="Red"></asp:Label></td>
                <td align="left" class="hei30">
                    <input id="BtnCancel" runat="server" type="button" value="キャンセル" class="bg6" />
                    <input id="BtnC" runat="server" class="w80 bg6" type="button" value="閉じる" onclick="window.close();" />
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <table id="TblMain" runat="server" border="1" bordercolor="#000000" class="col tl bg1 def12">
                        <tr>
                            <td  class="bg3" >
                                発注No</td>
                            <td >
                                <asp:Literal ID="LitHacchuNo" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td  bgcolor="#66cc99" class="bg3">
                                発注日</td>
                            <td >
                                <asp:Literal ID="LitHacchuubi" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td  bgcolor="#66cc99" class="bg3">
                                仕入先コード</td>
                            <td  >
                                <asp:Literal ID="LitShiireCode" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td  class="bg3">
                                仕入先名</td>
                            <td >
                                <asp:Literal ID="LitShiireName" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td  class="bg3">
                                品目グループ</td>
                            <td >
                                <asp:Literal ID="LitKubun" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td  class="bg3" >
                                品目コード</td>
                            <td >
                                <asp:Literal ID="LitBuhinCode" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td  class="bg3" >
                                品目名</td>
                            <td >
                                <asp:Literal ID="LitBuhinName" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td class="bg3"  >
                                単価</td>
                            <td>
                                <table cellpadding="0" cellspacing="0" class="hei15">
                                    <tr>
                                        <td nowrap="noWrap">
                                            <asp:Label ID="LblT" runat="server"></asp:Label><asp:Label ID="LblTanka" runat="server"></asp:Label></td>
                                        <td nowrap="nowrap">
                                            <table id="TblT" runat="server" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td >
                                            &nbsp;→ \<asp:TextBox ID="TbxT" runat="server" Width="60px" CssClass="tr"></asp:TextBox></td>
                                    </tr>
                                </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td  class="bg3">
                                数量</td>
                            <td>
                                <table cellpadding="0" cellspacing="0" class="hei15">
                                    <tr>
                                        <td nowrap="noWrap">
                                            <asp:Literal ID="LitSuuryou" runat="server"></asp:Literal>
                                        </td>
                                        <td nowrap="nowrap" >
                                            <table id="TblS" runat="server" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                &nbsp;→
                                                <asp:TextBox ID="TbxS" runat="server" Width="50px" CssClass="tr"></asp:TextBox>
                                            </td>
                                        </tr>
                                        </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td  class="bg3">
                                単位</td>
                            <td >
                                <asp:Literal ID="LitTani" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td  class="bg3">
                                注文金額</td>
                            <td >
                                <asp:Label ID="LblKingaku" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td  class="bg3">
                                納入場所</td>
                            <td >
                                <asp:Literal ID="LitNBashoMei" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td  class="bg3">
                                発注担当者コード</td>
                            <td >
                                <asp:Literal ID="LitTantoushaID" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td  class="bg3">
                                発注担当者名</td>
                            <td >
                                <asp:Literal ID="LitTantoushaMei" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td  class="bg3">
                                備考</td>
                            <td >
                                <asp:TextBox ID="TbxBikou" runat="server" Height="70px" MaxLength="200" ReadOnly="True"
                                    TextMode="MultiLine" Width="300px"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" class="hei30">
                    <input id="BtnK" runat="server" type="button" value="単価、数量変更" class="bg6 " /></td>
            </tr>
        </table>

    </div>
        <asp:Button ID="BtnProcess" runat="server" OnClick="BtnProcess_Click" />
        <input id="HidProcess" runat="server" type="hidden" />
    </form>
</body>
</html>
