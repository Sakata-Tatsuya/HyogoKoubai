<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChoboListForm.aspx.cs" Inherits="m2mKoubai.ChoboListForm" ValidateRequest="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagName="CtlMainMenu" Src="~/CtlMainMenu.ascx" TagPrefix="uc1" %>
<%@ Register Src="~/Common/CtlNengappiFromTo.ascx" TagName="CtlNengappiFromTo" TagPrefix="uc3" %>
<%@ Register Src="~/Common/CtlMyPager.ascx" TagName="CtlMyPager" TagPrefix="uc2" %>
<%--<%@ Register Assembly="Core" namespace="Core.Web" tagprefix="cc1" %>--%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>帳票管理</title>
    <link href="MainStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function $(id) {
            return document.getElementById(id);
        }
        function AjaxRequest(command_name, arg) {
            <%= Ram.ClientID %>.ajaxRequest(command_name + ':' + arg);
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

        //var win = null;
        //function OpenWinPost(target, w, h, etc) {
        //    win = window.open
        //        ("", target, "width=" + w + "px,height=" + h + "px,location=no,resizable=yes,scrollbars=yes" + etc);
        //    win.focus();
        //}
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
    <div id="divList" runat="server">
    <table border="1" bordercolor="#000000" class="col def9 mt5 bg1">
        <tr>
            <td class="tl">
                <table border="1" bordercolor="#000000" class="col tc" frame="below" width="100%">
                    <tr class="bg3">
                        <td>帳票種別</td>
                        <td>取引先</td>
                        <td>計上日</td>
                        <td>発行日</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DdlDataType" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlKaisha" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <uc3:CtlNengappiFromTo ID="CtlKeijoBi" runat="server" />
                        </td>
                        <td>
                            <uc3:CtlNengappiFromTo ID="CtlTourokuBi" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
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
                <table class="def9">
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
            <div id="DivD" runat="server" width="80vw" height="70vh">
                <telerik:RadGrid ID="D" runat="server" CssClass="def9" PageSize="30" width="750px" height="700px" 
                    AllowPaging="False" EnableAJAX="True" EnableAJAXLoadingTemplate="True"
                    AllowCustomPaging="True" EnableEmbeddedSkins="False" Skin="Web20"
                    GridLines="None" OnPageIndexChanged="D_PageIndexChanged" 
                    OnPageSizeChanged="D_PageSizeChanged" CellPadding="0" 
                    EnableEmbeddedBaseStylesheet="False" OnPreRender="D_PreRender"
                    OnItemDataBound="D_ItemDataBound">
                    <MasterTableView CellPadding="2" GridLines="Both" border="0" CellSpacing="0" AutoGenerateColumns="False" >
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="ColDataType" HeaderText="帳票種別">
                                <ItemTemplate>
                                    <asp:Label ID="LblDataType" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn UniqueName="ColKeijoBi" HeaderText="計上日">
                                <ItemTemplate>
                                    <asp:Label ID="LblKeijoBi" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn UniqueName="ColSlipID" HeaderText="帳票番号">
                                <ItemTemplate>
                                    <asp:Label ID="LblSlipID" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn UniqueName="ColDisp" HeaderText="　">
                                <ItemTemplate>
                                    <asp:ImageButton ID="BtnDisp" runat="server" ImageUrl="/img/pdf.gif" Width="24" Height="24" OnClick="BtnDisp_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn UniqueName="ColKaisha" HeaderText="発行社">
                                <ItemTemplate>
                                    <asp:Label ID="LblKaisha" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn UniqueName="ColTourokuBi" HeaderText="発行日">
                                <ItemTemplate>
                                    <asp:Label ID="LblTourokuBi" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridTemplateColumn>

                        </Columns>
                        <EditFormSettings>
                            <EditColumn InsertImageUrl="Update.gif" UpdateImageUrl="Update.gif" EditImageUrl="Edit.gif" CancelImageUrl="Cancel.gif">
                            </EditColumn>
                        </EditFormSettings>
                        <HeaderStyle  CssClass="bg3" HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle Wrap="False"></ItemStyle>
                        <PagerStyle Position="Top" AlwaysVisible="False"
                            PagerTextFormat="ページ移動: {4} &amp;nbsp;ページ : &lt;strong&gt;{0:N0}&lt;/strong&gt; / &lt;strong&gt;{1:N0}&lt;/strong&gt; | 件数: &lt;strong&gt;{2:N0}&lt;/strong&gt; - &lt;strong&gt;{3:N0}件&lt;/strong&gt; / &lt;strong&gt;{5:N0}&lt;/strong&gt;件中" 
                            PageSizeLabelText="ページサイズ:" FirstPageToolTip="最初のページに移動" 
                            LastPageToolTip="最後のページに移動" NextPageToolTip="次のページに移動" 
                            PrevPageToolTip="前のページに移動"/>
                    </MasterTableView>
                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>

                    <ClientSettings>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
                <uc2:CtlMyPager ID="Pb" runat="server" />
                <asp:HiddenField ID="HidFileID" runat="server" />
            </td>
        </tr>
    </table>
    </div>

    <div id="divDtl" runat="server">
        <table style="border-collapse: collapse; margin-top: 20px; white-space: nowrap;">
            <tr>
                <td style="width: 100px;">
                    <asp:Button ID="BtnBack" runat="server"  Text="一覧に戻る" CssClass="btn" OnClick="BtnBack_Click"/>
                </td>
                <td style="width: 40px;">
                </td>
                <td id="btn_save" runat="server" style="padding:5px;">
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btnAqua" OnClick="BtnSave_Click"/>
                </td>
<%--                <td id="btn_print" runat="server" style="padding:5px;"><asp:Button ID="btnPrint" runat="server" Text="印刷" CssClass="btn" OnClick="BtnPrint_Click"/></td>--%>
<%--                <td id="Td1" runat="server" style="padding:5px;"><asp:Button ID="Button1" runat="server" Text="送信" CssClass="btn" OnClick="btnSend_Click"/></td>--%>
            </tr>
        </table>
        <div class="ReportsPdf">
            <asp:Label ID="LblPdf" runat="server" Text=""></asp:Label>
        </div>

    </div>


    <telerik:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
         <ClientEvents OnRequestStart="OnRequestStart" OnResponseEnd="OnResponseEnd" />
    </telerik:RadAjaxManager>

    </form>
    <form id="NewForm" method="post" name="NewForm">
        <input id="HidKey" runat="server" type="hidden" />
    </form>
</body>
</html>
