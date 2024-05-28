<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuhinUpForm.aspx.cs" Inherits="m2mKoubai.Master.BuhinUpForm" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>部材登録</title>
    <link href="../MainStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
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
            document.getElementById('BtnTS').click();
        }
    }
    function TourokuChk(bool)
    {
        var tbxKubun = document.getElementById('TbxKubun');
        if(tbxKubun.value == "")
        {
            alert("品目グループを入力してください");
            tbxKubun.focus();
            return false;
        }
        if(bool)
        {
            var tbxCode = document.getElementById('TbxCode');
            if(tbxCode.value == "")
            {
                alert("品目コードを入力してください");
                tbxCode.focus();
                return false;
            }
        }
        var tbxName = document.getElementById('TbxHinmei');
        if(tbxName.value == "")
        {
            alert("品目名を入力してください");
            tbxName.focus();
            return false;
        }
        // 単価
        var tbxTanka = document.getElementById('TbxTanka');
        if(tbxTanka.value != "" && !CheckDecimal(tbxTanka.value))
        {
            tbxTanka.focus();
            return false;
        }
        
        var tbxTani = document.getElementById('TbxTani');
        if(tbxTani.value == "")
        {
            alert("単位を入力してください");
            tbxTani.focus();
            return false;
        }
        // ロット
        var tbxLot = document.getElementById('TbxLot');
        if(tbxLot.value != "" && !CheckSuu(tbxLot,'ロット'))
        {
            return false;
        }
        var tbxLeadTime = document.getElementById('TbxLeadTime');
        if (document.getElementById('DdlLeadTime').selectedIndex > 0 && tbxLeadTime.value == "")
        {
            alert("リードタイムを入力をしてください");
            tbxLeadTime.focus();
            return false;
        }
        if (tbxLeadTime.value != "" && document.getElementById('DdlLeadTime').selectedIndex == 0)
        {
            alert("リードタイムの単位を選択してしてください");
            return false;
        }
        // 仕入先
        if (document.getElementById('DdlShiire1').selectedIndex == 0)
        {
            alert("仕入先1を選択して下さい");
            return false;
        }
        if (document.getElementById('DdlShiire1').selectedIndex == document.getElementById('DdlShiire2').selectedIndex)
        {
            alert("仕入先1と仕入先2は違う仕入先を選択して下さい");
            return false;
        }
        return true;
    }
    function CheckDecimal(deci)
    {
        if (deci.match( /[^0-9.]/ ) != null)
        {
            alert('単価の入力値が正しくありません');
            return false;
        }
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
        return true;
    }
    // 数値チェック
    function CheckSuu(tbx,str)
    {
        if (tbx.value.match(/[^0-9]/) != null)
        {
            alert(str+"は半角数字のみで入力して下さい");
            tbx.focus();
            return false;
        }
        return true;
    }
    function Koushin()
    {
        if (!TourokuChk(false))
            return;
        if (confirm("更新しますか？"))
        {
            document.getElementById('BtnKS').click();
        }
    }
    function Close() 
    {
        window.close();
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
             if (c < 256 || (c >= 0xff61 && c <= 0xff9f))
             {
             }
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
<body class="bg0" onload="OnLoad(<%=loadFlg%>)">
    <form id="form1" runat="server">
    <div>
        <table id="TblAll" runat="server" align="center" class="def9">
            <tr>
                <td align="center">
                    <asp:Label ID="LblMsg" runat="server" CssClass="b"></asp:Label></td>
            </tr>
            <tr>
                <td align="right">
                    <input id="BtnC" runat="server" class="w80 bg6" type="button" value="閉じる" /></td>
            </tr>
            <tr>
                <td align="right">
                    <table id="TblMain" runat="server" border="1" bordercolor="#000000" class="col tl bg1">
                        <tr>
                            <td class="bg4">
                                品目グループ</td>
                            <td >
                                <asp:TextBox ID="TbxKubun" runat="server" MaxLength="5" Width="50px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                品目コード</td>
                            <td class="def12">
                                <asp:TextBox ID="TbxCode" runat="server" MaxLength="10" Width="100px"></asp:TextBox><asp:Literal ID="LitCode" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td class="bg4" >
                                品目名</td>
                            <td >
                                <asp:TextBox ID="TbxHinmei" runat="server" MaxLength="50" Width="400px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bg4" >
                                単価</td>
                            <td >
                                <asp:TextBox ID="TbxTanka" runat="server" CssClass="tr" MaxLength="11" Width="100px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                単位</td>
                            <td>
                                <asp:TextBox ID="TbxTani" runat="server" MaxLength="5" Width="80px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                ロット</td>
                            <td>
                                <asp:TextBox ID="TbxLot" runat="server" MaxLength="8" Width="80px" CssClass="tr"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                リードタイム</td>
                            <td>
                                <asp:TextBox ID="TbxLeadTime" runat="server" Width="30px" CssClass="tr"></asp:TextBox>
                                <asp:DropDownList ID="DdlLeadTime" runat="server">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                仕入先1</td>
                            <td>
                                <asp:DropDownList ID="DdlShiire1" runat="server">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                仕入先2</td>
                            <td>
                                <asp:DropDownList ID="DdlShiire2" runat="server">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="bg4 nw">
                                資産勘定科目名</td>
                            <td>
                                <asp:DropDownList ID="DdlKamoku" runat="server">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                勘定科目名</td>
                            <td>
                                <asp:DropDownList ID="DdlHiyou" runat="server">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                補助科目名</td>
                            <td>
                                <asp:DropDownList ID="DdlHojyo" runat="server">
                                </asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tc">
                    <table id="TblBtn" runat="server">
                        <tr>
                            <td >
                                <input id="BtnT" runat="server" class="w80 bg6" type="button" value="登録" />
                                <input id="BtnK" runat="server" class="w80 bg6" type="button" value="更新" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Button ID="BtnTS" runat="server" OnClick="BtnTS_Click" />
        <asp:Button ID="BtnKS" runat="server" OnClick="BtnKS_Click" /></div>
    </form>
</body>
</html>
