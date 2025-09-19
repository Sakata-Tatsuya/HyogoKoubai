<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginMsgForm.aspx.cs" Inherits="Koubai.Master.LoginMsgForm" %>
<%@ Register TagName="CtlMainMenu" Src="~/CtlMainMenu.ascx" TagPrefix="uc1" %>
<%@ Register Src="~/Common/CtlMyPager.ascx" TagName="CtlMyPager" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>メッセージ登録</title>    
    <link href="../MainStyle.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript">
        function $(id)
        {
            return document.getElementById(id);
        }
         function AjaxRequest(command_name, arg) {
            <%= Ram.ClientID %>.ajaxRequest(command_name + ':' + arg);
         }
        function Update(key)
        {
            var win = window.open
            ("LoginMsgUpForm?MsgID="+key,"_brank","width=550px,height=300px,location=no,resizable=yes,scrollbars=yes");
            win.focus();
        }
        function Shinki()
        {
            var win = window.open
            ("LoginMsgUpForm","_brank","width=550px,height=300px,location=no,resizable=yes,scrollbars=yes");
            win.focus();
        }
        function Delete()
        {
            var chkIDAry = document.getElementById('HidChkID').value.split(',');
            var thisIDAry = document.getElementById('HidThisID').value.split(',');
            var hidDelKey = '';
            for (var i = 0; i < chkIDAry.length; i++)
            {
                var chk = document.getElementById(chkIDAry[i]);
                if (chk.checked)
                {
                    if (hidDelKey != "") hidDelKey += ",";
                    hidDelKey += thisIDAry[i];
                }
            }
            if (hidDelKey == "")
            {
                alert("チェックを入れて下さい");
                return false;
            }
            if (confirm("削除しますか？"))
            {
                if (confirm("本当に削除しますか？"))
                {
                    AjaxRequest('delete', hidDelKey);
                }
            }
        }
        function DelChk(bool)
        {
            var idAry = document.getElementById('HidChkID').value.split(',');
            for (var i = 0; i < idAry.length; i++)
            {
                var chk = document.getElementById(idAry[i]);
                chk.checked = bool;
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
        function PageChange(pageIndex)
        {
            AjaxRequest('page', pageIndex);
        }
        function Kensaku()
        {
            AjaxRequest('kensaku', '');
        }
        function Row()
        {
            AjaxRequest('row', '');
        }
        function Reload()
        {
            AjaxRequest('row', '');
        }
     </script> 
</head>
<body class="bg0">
    <form id="form1" runat="server">
      <div>
        <uc1:CtlMainMenu ID="M" runat="server"></uc1:CtlMainMenu>
<%--        <uc3:CtlTabMain id="Tab" runat="server"></uc3:CtlTabMain>--%>
        <input id="BtnNew" runat="server" type="button" value="新規登録" class="mt5 bg6" />
        <table border="1" bordercolor="#000000" class="def9 bg1 col mt5 tc">
            <tr>
                <td class="bg3">
                    有効/無効</td>
                <td rowspan="2">
                    <input id="BtnKen" runat="server" class="w60 bg6" type="button" value="検索" /></td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="DdlFlag" runat="server">
                    </asp:DropDownList></td>
            </tr>
        </table>
        <table id="TblList" runat="server" width="100%" class="def9">
            <tr>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="50%">
                                <asp:Label ID="LblMsg" runat="server" CssClass="b"></asp:Label></td>
                            <td class="hei20" >
                                <img id="Img1" runat="server" src="../Img/Load.gif" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="def" width="100%">
                        <tr>
                            <td width="20%">
                                <uc2:CtlMyPager ID="Pt" runat="server" />
                            </td>
                            <td align="left">
                                <table id="TblRow" runat="server" align="left" class="def9">
                                    <tr>
                                        <td>
                                            <input id="BtnS" runat="server" size="20" type="button" value="チェックしたデータを削除する" class="bg6" /></td>
                                        <td>
                                            <asp:DropDownList ID="DdlRow" runat="server">
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem Selected="True">30</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                                <asp:ListItem>--</asp:ListItem>
                                            </asp:DropDownList>行
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="G" runat="server" AutoGenerateColumns="False" OnRowDataBound="G_RowDataBound"
                        Width="100%" CssClass="def9">
                        <PagerSettings Visible="False" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Literal ID="LitS" runat="server" Text="削除"></asp:Literal><br />
                                    <input id="ChkH" runat="server" type="checkbox" />
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <input id="ChkI" runat="server" type="checkbox" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="更新">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <input id="BK" runat="server" type="button" value="更新" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="有効/無効">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="メッセージ">
                                <ItemStyle HorizontalAlign="Left" Width="450px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="登録日">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="更新日">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                      <HeaderStyle CssClass="bg3" />
                         <RowStyle CssClass="bg1" />
                         <AlternatingRowStyle CssClass="bg2" />
                    </asp:GridView>
                    <uc2:CtlMyPager ID="Pb" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <input id="HidChkID" runat="server" type="hidden" />
                    <input id="HidThisID" runat="server" type="hidden" />
                </td>
            </tr>
        </table>
        <telerik:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
            <ClientEvents OnRequestStart="OnRequestStart" OnResponseEnd="OnResponseEnd" />
        </telerik:RadAjaxManager>
        </div>
    </form>
</body>
</html>
