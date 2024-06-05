<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KaishaInfoForm.aspx.cs" Inherits="m2mKoubai.Shiiresaki.KaishaInfoForm" %>

<%@ Register TagName="CtlMainMenu" Src="~/CtlMainMenu.ascx" TagPrefix="uc1" %>
<%--<%@ Register Src="CtlTabShiire.ascx" TagName="CtlTabShiire" TagPrefix="uc1" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>会社情報</title>
    <link href="../MainStyle.css" rel="stylesheet" type="text/css" />   
    <script type="text/javascript">
         function $(id) {
             return document.getElementById(id);
         }
         function OnRequestStart() {
             document.getElementById('Img1').style.display = '';
         }
         function OnResponseEnd() {
             document.getElementById('Img1').style.display = 'none';
         }

         function Koushin() {
             AjaxRequest('koushin', '');
         }
         function AjaxRequest(command_name, arg) {
               <%= Ram.ClientID %>.ajaxRequest(command_name + ':' + arg);
         }
         // "更新しますか？"メッセージ
         function Koushin() {
             if (confirm("更新しますか？")) {
                 AjaxRequest('koushin', '');
             }
         }
    </script>  
</head>
<body class="bg99">
    <form id="form1" runat="server">    
    <div>
        <uc1:CtlMainMenu ID="M" runat="server"></uc1:CtlMainMenu>
<%--        <uc1:CtlTabShiire ID="Tab" runat="server" />--%>
        <br />
        <table id="TblAll" runat="server" align="left" cellpadding="0" cellspacing="0" >
            <tr>
                <td align="center" class="hei20">
                    <asp:Label ID="LblMsg" runat="server" CssClass="b def9"></asp:Label></td>
            </tr>
            <tr>
                <td align="center" class="hei20">
                    <img id="Img1" runat="server" src="../Img/Load.gif" />&nbsp;</td>
            </tr>
            <tr>
                <td >
                    <table id="TblMain" runat="server" border="1" bordercolor="#000000" class="col tl bg1">
                        <tr>
                            <td nowrap="nowrap" bgcolor="#66cc99" class="bg3">
                                仕入先コード</td>
                              <td nowrap="nowrap">             
                                <asp:Literal ID="LitCode" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" bgcolor="#66cc99" class="bg3">
                                仕入先名</td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TbxShiireName" runat="server" MaxLength="20" Width="200px" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" bgcolor="#66cc99" class="bg3">
                                郵便番号</td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TbxYuubin" runat="server" MaxLength="8" Width="80px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" bgcolor="#66cc99" class="bg3">
                                住所</td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TbxJyusho" runat="server" MaxLength="20" Width="250px" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" bgcolor="#66cc99" class="bg3">
                                TEL</td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TbxTel" runat="server" MaxLength="12" Width="250px" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" bgcolor="#66cc99" class="bg3">
                                FAX</td>
                            <td nowrap="nowrap">
                                <asp:TextBox  ID="TbxFax" runat="server" MaxLength="12" Width="250px" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" bgcolor="#66cc99" class="bg3">
                                口座名義</td>
                            <td nowrap="nowrap">
                                <asp:TextBox  ID="TbxKouzameigi" runat="server" MaxLength="12" Width="250px" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" bgcolor="#66cc99" class="bg3">
                                金融機関名</td>
                            <td nowrap="nowrap">
                                <asp:TextBox  ID="TbxKinyuukikanMei" runat="server" MaxLength="12" Width="250px" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" bgcolor="#66cc99" class="bg3">
                                口座番号</td>
                            <td nowrap="nowrap">
                                <asp:TextBox  ID="TbxKouzaBangou" runat="server" MaxLength="12" Width="250px" ></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" >
                    <table id="TblBtn" runat="server">
                        <tr>
                            <td>
                                &nbsp;<input id="BtnK" runat="server" class="w80 bg98" type="button" value="更新" /></td>
                        </tr>
                    </table>
                    &nbsp;
                </td>
            </tr>
        </table>    
    </div>
        <br />
<%--        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js">
                </asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js">
                </asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js">
                </asp:ScriptReference>
            </Scripts>
        </telerik:RadScriptManager>--%>
        <telerik:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
            <ClientEvents OnRequestStart= "OnRequestStart" OnResponseEnd="OnResponseEnd" />
        </telerik:RadAjaxManager>
    </form>
</body>
</html>
