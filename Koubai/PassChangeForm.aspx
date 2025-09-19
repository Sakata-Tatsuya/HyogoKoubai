<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PassChangeForm.aspx.cs" Inherits="Koubai.PassChangeForm" %>
<%@ Register TagName="CtlMainMenu" Src="~/CtlMainMenu.ascx" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>パスワード変更</title>
    <link href="MainStyle.css" rel="stylesheet" type="text/css" />    
    
    <script type="text/javascript">
        function Load()
        {
            document.getElementById("TbxPass").focus();
        }
        function $(id)
        {
            return document.getElementById(id);
        }
        function AjaxRequest(command_name, arg) {
        <%= Ram.ClientID %>.ajaxRequest(command_name + ':' + arg);
        }
        function OnRequestStart(sender, args)
        {
            var img1 = document.getElementById("Img1");
            img1.style.display = "";
        }
        function OnResponseEnd(sender, args)
        {
            var img1 = document.getElementById("Img1");
            img1.style.display = "none";
        }
        function Check()
        {
            var tbxPass = document.getElementById('TbxPass');
            if(tbxPass.value == "")
            {
                alert("パスワードを入力して下さい");
                tbxPass.focus();
                return;
            }
            if(tbxPass.value.length < 8)
            {
                alert("パスワードは8文字以上で入力して下さい");
                tbxPass.focus();
                return;
            }
            if (!HankakuChk('TbxPass', "パスワード")) {
                tbxPass.focus();
                return;
            }
            var tbxPass2 = document.getElementById("TbxPass2");
            if (tbxPass2.value == "") {
                alert("確認用パスワードを入力して下さい");
                tbxPass2.focus();
                return;
            }
            if (tbxPass.value != tbxPass2.value) {
                alert("確認用パスワードが間違っています");
                tbxPass2.focus();
                return;
            }
            if (!HankakuChk('TbxPass2', "パスワード")) {
                tbxPass.focus();
                return;
            }
            if (confirm("変更しますか？")) {
                AjaxRequest("henkou", "");
            }
        }
        function HankakuChk(tbxId, objName) {
            var count = 0;
            var val = document.getElementById(tbxId).value;

            for (var i = 0; i < val.length; i++) {
                var s = val.substring(i, i + 1);
                var c = s.charCodeAt(0);
                if (c < 256 || (c >= 0xff61 && c <= 0xff9f)) {
                }
                else {
                    alert(objName + 'は半角英数字のみで入力して下さい');
                    return false;
                }
            }
            return true;
        }
    </script>

</head>
<body onload="Load();" class="bg0">
    <form id="form1" runat="server">
    <div>
<%--        <uc1:CtlTabMain ID="Tab" runat="server" />--%>
        <uc1:CtlMainMenu ID="M" runat="server"></uc1:CtlMainMenu>
        <br />
       </div>
        <table align="left" id="TblList" runat="server" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tc hei20">
                    <asp:Label ID="LblMsg" runat="server" CssClass="b def9"></asp:Label></td>
            </tr>
            <tr>
                <td class="tc hei20" >
                    <img id="Img1" runat="server" src="Img/Load.gif" /></td>
            </tr>
            <tr>
                <td class="tc">
                    <table class="col tl bg1" border="1" bordercolor="#000000" id="TblMain" runat="server">
                        <tr>
                            <td class="bg3 b">
                                新しいパスワード</td>
                            <td>
                                <asp:TextBox ID="TbxPass" runat="server" TextMode="Password" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bg3 b">
                                確認パスワード（再入力）</td>
                            <td>
                                <asp:TextBox ID="TbxPass2" runat="server" TextMode="Password" Width="200px"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tc" >
                    <input id="BtnT" runat="server" type="button" value="変更登録" class="mt20 bg6 " /></td>
            </tr>
        </table>
        <telerik:radajaxmanager id="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
            <ClientEvents OnRequestStart="OnRequestStart" OnResponseEnd="OnResponseEnd" />
        </telerik:radajaxmanager>
    </form>
</body>
</html>
