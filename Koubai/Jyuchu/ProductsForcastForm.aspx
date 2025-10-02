<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductsForcastForm.aspx.cs" Inherits="Koubai.Jyuchu.ProductsForcastForm" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagName="CtlMainMenu" Src="~/CtlMainMenu.ascx" TagPrefix="uc1" %>
<%@ Register Src="~/Common/CtlNengappiFromTo.ascx" TagName="CtlNengappiFromTo" TagPrefix="uc3" %>
<%@ Register Src="~/Common/CtlMyPager.ascx" TagName="CtlMyPager" TagPrefix="uc2" %>
<%@ Register Assembly="Core" namespace="Core.Web" tagprefix="cc1" %>
<!DOCTYPE html>

<html lang="ja" xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
  <title>製品製造計画</title>
    <link href="../MainStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function $(id) {
            return document.getElementById(id);
        }
        function AjaxRequest(command_name, arg) {
<%--            <%= Ram.ClientID %>.ajaxRequest(command_name + ':' + arg);--%>
            $(Ram).ajaxRequest(command_name + ':' + arg);
        }
        //function pageLoad() {
        //    showpdf()
        //}
        //function showpdf() {
        //    var fileid = document.getElementById('HidFileID').value;
        //    if (0 < fileid.length) {
        //        document.getElementById('HidFileID').value = '';
        //        var url = "/Common/FileView.aspx?FileKey=" + fileid;
        //        var win = window.open(url, "_brank", "width=1200px,height=768px,location=no,resizable=yes,scrollbars=yes");
        //        win.focus();
        //    }
        //}
        function Reload() {
            AjaxRequest('reload', '');
        }
        function Kensaku() {
            AjaxRequest('kensaku', '');
        }
        function RowChange() {
            AjaxRequest('row', '');
        }
        function PageChange(pageIndex) {
            AjaxRequest('page', pageIndex);
        }
        function Msg(key) {
            document.getElementById('HidKey').value = key;
            NewForm.action = "../MessegeForm";
            NewForm.target = "_msg";
            OpenWinPost("_msg", 700, 500, '');
            NewForm.submit();
        }
        var win = null;
        function OpenWinPost(target, w, h, etc) {
            win = window.open
                ("", target, "width=" + w + "px,height=" + h + "px,location=no,resizable=yes,scrollbars=yes" + etc);
            win.focus();
        }

        function ChkAll(bool) {
            var idAry = document.getElementById('HidChkID').value.split(',');
            for (var i = 0; i < idAry.length; i++) {
                var chk = document.getElementById(idAry[i]);
                chk.checked = bool;
            }
        }

        function OnRequestStart(sender, args) {
            document.getElementById("Img1").style.display = '';
        }
        function OnResponseEnd(sender, args) {
            document.getElementById('Img1').style.display = 'none';
            var cmd = args.EventArgument.substring(0, args.EventArgument.indexOf(":"));
            var param = args.EventArgument.substring(args.EventArgument.indexOf(":") + 1);
        }

        function addComma(value) {
            var i;
            for (i = 0; i < value.length / 3; i++) {
                value = value.replace(/^([+-]?\d+)(\d\d\d)/, "$1,$2");
            }
            return value;
        }

    </script>

</head>
<body bottommargin="0" leftmargin="4" topmargin="0" rightmargin="4">
    <form id="form1" runat="server">
    <uc1:CtlMainMenu ID="CtlMainMenu1" runat="server" />
    <div style="margin-bottom: 1px;">
        <table id="TblK" runat="server" border="1" bordercolor="#708090" cellpadding="2" cellspacing="0" class="def9 col" >
            <tr>
               <td class="khd3" nowrap="nowrap" style="background-color:#FFCCCC;color:Black">
                    <asp:Literal ID="Literal2" runat="server" Text="基準年月"></asp:Literal>
                </td>
                <td  class="bg3" nowrap="nowrap">
                    仕入先</td>
                <td rowspan="2">
                    <asp:Button ID="BtnKensaku" runat="server" CssClass="hand def" Height="30px" OnClick="BtnKensaku_Click" Text="検 索" Width="70px" />
                    <asp:Button ID="BtnDownload" runat="server" />
                </td>

            </tr>
            <tr>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="DdlKijyunYM" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="DdlKijyunYM_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="DdlShiiresaki" runat="server" Width="252px" 
                        AutoPostBack="True" onselectedindexchanged="DdlShiiresaki_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <asp:Label runat="server" ID="LblMsg" CssClass="def"></asp:Label>
    <div id="L" runat="server">
        <table cellpadding="0" cellspacing="0" id="TblList" runat="server">
            <tr>
                <td>
                <table width="100%" class="def9">
                    <tr>
                        <td class="nw" valign="bottom">
                            <uc2:CtlMyPager ID="Pt" runat="server" />
                        </td>
                        <td class="tr" valign="bottom">
                            <table id="TblRow" runat="server">
                                <tr>
                                    <td valign="bottom" nowrap="nowrap">
                                        <asp:DropDownList ID="DdlRow" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlRow_SelectedIndexChanged">
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
                                        </asp:DropDownList>
                                        行
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

                    <div id="DivD" class ="def100w">
                        <asp:GridView id="D" runat="server" AutoGenerateColumns="False" class="def9 defFix" width="1000px"
                            OnRowDataBound="D_RowDataBound">
                            <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Literal ID="H_Nengetu" runat="server" Text="年月"></asp:Literal><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Literal ID="LitNengetu" runat="server"></asp:Literal>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="tc" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Literal ID="H_Season" runat="server" Text="シーズン"></asp:Literal><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Literal ID="LitSeason" runat="server"></asp:Literal>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="tc" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Literal ID="H_SeihinCode" runat="server" Text="製品"></asp:Literal><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Literal ID="LitSeihinCode" runat="server"></asp:Literal>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="tc" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Literal ID="H_SeisanSu" runat="server" Text="生産数"></asp:Literal><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Literal ID="LitSeisanSu" runat="server"></asp:Literal>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="tr" />
                                    </asp:TemplateField>

                                </Columns>
                            <HeaderStyle CssClass="bg3" />
                            <RowStyle CssClass="bg1" />
                            <AlternatingRowStyle CssClass="bg2" />
                        </asp:GridView>
                        <uc2:CtlMyPager ID="Pb" runat="server" />
                    </div>
                </td>
            </tr>
        </table>
        <input id="HidChkID" runat="server" type="hidden" />
    </div>

    <telerik:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
         <ClientEvents OnRequestStart="OnRequestStart" OnResponseEnd="OnResponseEnd" />
    </telerik:RadAjaxManager>

    </form>
</body>
</html>


