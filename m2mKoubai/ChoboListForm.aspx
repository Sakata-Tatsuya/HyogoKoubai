<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChoboListForm.aspx.cs" Inherits="m2mKoubai.ChoboListForm" ValidateRequest="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagName="CtlMainMenu" Src="~/CtlMainMenu.ascx" TagPrefix="uc1" %>
<%@ Register Src="~/Common/CtlNengappiFromTo.ascx" TagName="CtlNengappiFromTo" TagPrefix="uc3" %>
<%@ Register Src="~/Common/CtlMyPager.ascx" TagName="CtlMyPager" TagPrefix="uc2" %>
<%@ Register Assembly="Core" namespace="Core.Web" tagprefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>帳票管理</title>
    <link href="../MainStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function $(id) {
            return document.getElementById(id);
        }
        function AjaxRequest(command_name, arg) {
            <%= Ram.ClientID %>.ajaxRequest(command_name + ':' + arg);
        }
        function pageLoad() {
            console.log("pageload");
            showpdf()
        }
        function showpdf() {
            var fileid = document.getElementById('HidFileID').value;
            console.log("fileid");
            console.log(fileid);
            if (0 < fileid.length) {
                document.getElementById('HidFileID').value = '';
                var url = "/Common/FileView.aspx?FileKey=" + fileid;
                var win = window.open(url, "_brank", "width=1200px,height=768px,location=no,resizable=yes,scrollbars=yes");
                win.focus();
            }
        }

        var win = null;
        function OpenWinPost(target, w, h, etc) {
            win = window.open
                ("", target, "width=" + w + "px,height=" + h + "px,location=no,resizable=yes,scrollbars=yes" + etc);
            win.focus();
        }
        function RowChange() {
            AjaxRequest('row', '');
        }
        function PageChange(pageIndex) {
            AjaxRequest('page', pageIndex);
        }

        function OnRequestStart(sender, args) {
            document.getElementById("Img1").style.display = '';
        }
        function OnResponseEnd(sender, args) {
            document.getElementById('Img1').style.display = 'none';
        }

    </script>

</head>
<body bottommargin="0" leftmargin="4" topmargin="0" rightmargin="4">
    <form id="form1" runat="server">
    <uc1:CtlMainMenu ID="M" runat="server"></uc1:CtlMainMenu>
    <table border="1" bordercolor="#000000" class="col def9 mt5 bg1">
        <tr>
            <td class="tl">
                <table border="1" bordercolor="#000000" class="col tc" frame="below" width="100%">
                    <tr class="bg3">
                        <td>帳票種別</td>
                        <td>取引先</td>
                        <td>発行日</td>
                        <td>計上日</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DdlDataType" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlKaisha" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <uc3:CtlNengappiFromTo ID="CtlTourokuBi" runat="server" />
                        </td>
                        <td>
                            <uc3:CtlNengappiFromTo ID="CtlKeijoBi" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
<%--                <input id="BtnK" runat="server" type="button" value="検索" class="w60 bg6" />--%>
                <asp:Button ID="BtnKensaku" runat="server" Text="検索" Width="50px" OnClick="BtnKensaku_Click" Height="30px" />
            </td>
        </tr>
    </table>
    <table id="TblList" runat="server" width="100%" class="def9">
        <tr>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%">
                            <asp:Label ID="LblMsg" runat="server" CssClass="b"></asp:Label>
                        </td>
                        <td class="hei20">
                            <img id="Img1" runat="server" src="../Img/Load.gif" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" class="def9">
                    <tr>
                        <td class="nw" valign="bottom">
                            <uc2:CtlMyPager ID="Pt" runat="server" />
                        </td>
                        <td valign="bottom" nowrap="noWrap">
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
                            </asp:DropDownList>
                             行
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="G" runat="server" AutoGenerateColumns="False" OnRowDataBound="G_RowDataBound" CssClass="def9" OnRowCommand="G_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="LblDataTypeH" runat="server" Text="帳票種別"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LblDataType" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="LblKeijoBiH" runat="server" Text="計上日"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LblKeijoBi" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="LblSlipIDH" runat="server" Text="帳票番号"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LblSlipID" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="BtnDispH" runat="server" Text="　"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="BtnDisp" runat="server" ImageUrl="/img/pdf.gif" Width="24" Height="24" CommandName="disp"/>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="LblKaishaH" runat="server" Text="取引先"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LblKaisha" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="LblTourokuBiH" runat="server" Text="発行日"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LblTourokuBi" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <HeaderStyle CssClass="bg3" HorizontalAlign = "center" VerticalAlign = "middle"/>
                    <RowStyle CssClass="bg1" HorizontalAlign = "center" VerticalAlign = "middle"/>
                    <AlternatingRowStyle CssClass="bg2" />
<%--                    <ClientSettings AllowColumnsReorder="false" AllowDragToGroup="false" ReorderColumnsOnClient="false">
                        <Scrolling AllowScroll="True" UseStaticHeaders="true"></Scrolling>
                        <Resizing AllowColumnResize="false" EnableRealTimeResize="false" />
                    </ClientSettings>--%>
                </asp:GridView>
                <uc2:CtlMyPager ID="Pb" runat="server" />
                <asp:HiddenField ID="HidFileID" runat="server" />
            </td>
        </tr>
    </table>

    <telerik:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
         <ClientEvents OnRequestStart="OnRequestStart" OnResponseEnd="OnResponseEnd" />
    </telerik:RadAjaxManager>

    </form>
    <form id="NewForm" method="post" name="NewForm">
        <input id="HidKey" runat="server" type="hidden" />
    </form>
</body>
</html>
