<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginMsgUpForm.aspx.cs" Inherits="m2mKoubai.Master.LoginMsgUpForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ログイン画面メッセージ登録</title>
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
        if (!MsgCheck())
            return; 
                       
        if (confirm("登録しますか？"))
        {            
            document.getElementById('BtnTS').click();
        }                                           
    }  
    function MsgCheck()
   {              
        var msg = document.getElementById("TbxMsg").value;
        if (msg == "")
        {
            alert("メッセージを入力して下さい");
            return false;
        }
        if (msg.length > 500)
        {
            alert("メッセージは500文字以内で入力して下さい");
            return false;
        }
        return true;
    }        
    function Koushin()
    {       
        if (!MsgCheck())
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
    </script>
</head>
<body class="bg0" onload="OnLoad(<%=loadFlg%>)">
    <form id="form1" runat="server">
    <div>
        <table id="TblAll" runat="server" align="center" class="def9">
            <tr>
                <td class="tc hei20">
                    <asp:Label ID="LblMsg" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td align="right">
                                <input id="BtnC" runat="server" class="w80 bg6"  type="button" value="閉じる" /></td>
            </tr>
            <tr>
                <td>
                    <table id="TblMain" runat="server" align="center" border="1" bordercolor="#000000"
                        class="col bg1">
                        <tr>
                            <td class="bg4" nowrap="nowrap">
                                有効/無効</td>
                            <td>
                                <asp:RadioButton ID="RbtnYukou" runat="server" Checked="True" GroupName="Flag" Text="有効" />
                                <asp:RadioButton ID="RbtnMukou" runat="server" GroupName="Flag" Text="無効" /></td>
                        </tr>
                        <tr>
                            <td class="bg4" >
                                メッセージ<br />
                                </td>
                            <td>
                                <asp:TextBox ID="TbxMsg" runat="server" CssClass="def10" Rows="11" TextMode="MultiLine"
                                    Width="350px"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
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
        <asp:Button ID="BtnTS" runat="server" OnClick="BtnTS_Click" />
        <asp:Button ID="BtnKS" runat="server" OnClick="BtnKS_Click" /></div>
    </form>
</body>
</html>
