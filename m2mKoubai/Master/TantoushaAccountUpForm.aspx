<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TantoushaAccountUpForm.aspx.cs" Inherits="m2mKoubai.Master.TantoushaAccountUpForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>担当者</title>
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
            var tbxID = document.getElementById('TbxID');
            if(tbxID.value == "")
            {
                alert("ログインIDを入力してください");
                tbxID.focus();
                return;
            }
            if(!HankakuChk('TbxID', 'ログインID'))
            {
                tbxID.focus();
                return;
            }
             var tbxPass = document.getElementById('TbxPass');
            if(tbxPass.value == "")
            {
                alert("パスワードを入力してください");
                tbxPass.focus();
                return;
            }

            if(tbxPass.value.length < 8)
            {
                alert("パスワードは8文字以上で入力して下さい");
                tbxPass.focus();
                return false;
            }
            if(!HankakuChk('TbxPass', 'パスワード'))
            {
                tbxPass.focus();
                return;
            }

       }
       else
       {
            var tbxPass = document.getElementById('TbxPass'); 
            if(tbxPass.value != "")
            {
                if(tbxPass.value.length < 8)
                {
                    alert("パスワードは8文字以上で入力して下さい");
                    tbxPass.focus();
                    return false;
                }
                if(!HankakuChk('TbxPass', 'パスワード'))
            {
                tbxPass.focus();
                return;
            }
           
            }
       }

        var tbxCode = document.getElementById('TbxTCode');
        if(tbxCode.value == "")
        {
            alert("担当者コードを入力してください");
            tbxCode.focus();
            return;
        } 
        if(tbxCode.value.length != 6)
        {
             alert("担当者コードを6桁で入力してください");
            tbxCode.focus();
            return;
        }
        var tbxName = document.getElementById('TbxTName');
        if(tbxName.value == "")
        {
            alert("担当者名を入力してください");
            tbxName.focus();
            return;
        }
        /*
        var tbxBusho = document.getElementById('TbxBusho');
        if(tbxBusho.value == "")
        {
            alert(" 所属部署を入力してください");
            tbxBusho.focus();
            return;
        }
         var tbxYakushoku = document.getElementById('TbxYakushoku');
        if(tbxYakushoku.value == "")
        {
            alert("役職を入力してください");
            tbxYakushoku.focus();
            return;
        }
        */
        
        var tbxMail = document.getElementById('TbxMail');
        /*if(tbxMail.value == "")
        {
            alert("メールアドレスを入力してください");
            tbxMail.focus();
            return;
        }*/
        if (tbxMail.value != '')
        {
            if(!MailCheck(tbxMail.value))
            {
                tbxMail.focus();
                return;
            }
            if(!HankakuChk('TbxMail', 'E-Mail'))
            {
                tbxMail.focus();
                return;
            }
        }
         var ddl = document.getElementById('DdlJigyoushoKubun');
         if(ddl.selectedIndex == 0)
         {
         
            alert("事業所区分を選択してください");
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
        var val = document.getElementById(tbxId).value;

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
    function MailCheck(Mail)
    {       
        var flag = 0;
        if(!Mail.match(/.+@.+\..+/)) 
        {
            flag = 1;
        }
        if(flag)
        {
            alert("メールアドレスを正しく入力してください");
            return false;
        }
        else
        {
            return true;
        }
    }
    </script>
</head>
<body class="bg0" onload="OnLoad(<%=loadFlg%>)">
    <form id="form1" runat="server">
    <div>
        <table id="TblAll" runat="server" align="center" class="def9">
            <tr>
                <td class="tc hei20">
                    <asp:Label ID="LblMsg" runat="server" CssClass="b"></asp:Label></td>
            </tr>
            <tr>
                <td align="left">
                    <input id="BtnC" runat="server" class="w80 bg6" onclick="Close()" type="button" value="閉じる" /></td>
            </tr>
            <tr>
                <td>
                    <table id="TblMain" runat="server" border="1" bordercolor="#000000" class="col bg1" align="center">
                        <tr>
                            <td class="bg4">
                                担当者コード</td>
                            <td >
                                <asp:TextBox ID="TbxTCode" runat="server" MaxLength="6" Width="70px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                担当者名</td>
                            <td >
                                <asp:TextBox ID="TbxTName" runat="server" MaxLength="20" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                所属部署</td>
                            <td >
                               <asp:TextBox ID="TbxBusho" runat="server" MaxLength="15" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                役職</td>
                            <td >
                                <asp:TextBox ID="TbxYakushoku" runat="server" MaxLength="15" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bg4" >
                                ログインID</td>
                            <td class="def12"  >
                                <asp:TextBox ID="TbxID" runat="server" MaxLength="20" Width="200px"></asp:TextBox><asp:Literal ID="LitLoginID" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                パスワード</td>
                            <td>
                                <asp:TextBox ID="TbxPass" runat="server" MaxLength="20" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="bg4" >
                                E-Mail</td>
                            <td colspan="1" >
                                <asp:TextBox ID="TbxMail" runat="server" Width="300px" MaxLength="50"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                管理者権限</td>
                            <td colspan="1">
                            <asp:RadioButton ID="RbtnKanrisha" runat="server" Checked="true" Text="権限あり" GroupName="Kanrisha" />
                                <asp:RadioButton ID="RbtnKanrishaNashi" runat="server" Text="権限なし" GroupName="Kanrisha" />
                            </td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                事業所</td>
                            <td colspan="1">
                                <asp:DropDownList ID="DdlJigyoushoKubun" runat="server">
                                </asp:DropDownList></td>
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
                                <input id="BtnK" runat="server" class="w80 bg6" type="button" value="更新" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
        <asp:Button ID="BtnTS" runat="server" OnClick="BtnTS_Click" />
        <asp:Button ID="BtnKS" runat="server" OnClick="BtnKS_Click" />
    </form>
</body>
</html>
