<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NounyuBashoUpForm.aspx.cs" Inherits="m2mKoubai.Master.NounyuBashoUpForm" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>納入場所登録</title>
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
        if(bool)
        {
            var tbxCode = document.getElementById('TbxCode');
            if(tbxCode.value == "")
            {
                alert("納入場所コードを入力してください");
                tbxCode.focus();
                return;
            } 
            if(tbxCode.value.length != 2)
            {
                 alert("コードを2桁で入力してください");
                tbxCode.focus();
                return;
            }
      }
        var tbxName = document.getElementById('TbxName');
        if(tbxName.value == "")
        {
            alert("納入場所を入力してください");
            tbxName.focus();
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
                <td align="left">
                    <input id="BtnC" runat="server" class="w80 bg6" type="button" value="閉じる" /></td>
            </tr>
            <tr>
                <td align="right">
                    <table id="TblMain" runat="server" border="1" bordercolor="#000000" class="col tl bg1">
                        <tr>
                            <td class="bg4">
                                納入場所コード</td>
                            <td class="def12">
                                <asp:TextBox ID="TbxCode" runat="server" Width="70px"></asp:TextBox><asp:Literal ID="LitCode" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td class="bg4" >
                                納入場所名</td>
                            <td >
                                <asp:TextBox ID="TbxName" runat="server" CssClass="w250"></asp:TextBox></td>
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
                                <input id="BtnK" runat="server" class="w80 bg6" type="button" value="更新" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Button ID="BtnTS" runat="server" OnClick="BtnTS_Click" />
        <asp:Button ID="BtnKS" runat="server" OnClick="BtnKS_Click" />

    </div>
    </form>
</body>
</html>
