<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuhinSimForm.aspx.cs" Inherits="Koubai.Jyuchu.BuhinSimForm" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagName="CtlMainMenu" Src="~/CtlMainMenu.ascx" TagPrefix="uc1" %>
<%@ Register Src="~/Common/CtlNengappiFromTo.ascx" TagName="CtlNengappiFromTo" TagPrefix="uc3" %>
<%@ Register Src="~/Common/CtlMyPager.ascx" TagName="CtlMyPager" TagPrefix="uc2" %>
<%@ Register Assembly="Core" namespace="Core.Web" tagprefix="cc1" %>
<!DOCTYPE html>

<html lang="ja" xmlns="http://www.w3.org/1999/xhtml">
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
        function CntRow(cnt) {
            document.forms[0].count.value = cnt;
            return;
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
        <table id="TblK" runat="server" border="1" bordercolor="#708090" cellpadding="2" class="col def9 mt5 bg1">
            <tr>
               <td class="bg3" nowrap="nowrap" style="background-color:#FFCCCC;color:Black">
                    <asp:Literal ID="Literal2" runat="server" Text="基準年月"></asp:Literal>
                </td>
                <td  class="bg3" nowrap="nowrap">
                    仕入先</td>
                <td rowspan="2">
                    <asp:Button ID="BtnKensaku" runat="server" CssClass="hand def" Height="30px" OnClick="BtnKensaku_Click" Text="検 索" Width="70px" />
                </td>

            </tr>
            <tr>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="DdlKijyunYM" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="DdlKijyunYM_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="DdlShiiresaki" runat="server" AutoPostBack="True" Width="252px" 
                        onselectedindexchanged="DdlShiiresaki_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <asp:Label runat="server" ID="LblMsg" CssClass="def9"></asp:Label>
    <div id="L" runat="server">
        <table cellpadding="0" cellspacing="0" id="TblList" runat="server">
            <tr>
                <td>
                  <table width="100%" class="def9">
                        <tr>
                            <td class="nw" valign="bottom">
                                <asp:Button ID="BtnOD" runat="server" Text="チェックした部材を発注する" OnClick="BtnOD_Click" class="bg6 w200"/>
                            </td>
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

                    <div id="DivD" width="98%" height="600px">
                        <asp:GridView id="D" runat="server" AutoGenerateColumns="False" Class="def9 defFix" width="100%"
                            OnRowDataBound="D_RowDataBound">
                            <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Literal ID="LitChk_H" runat="server" Text="発注"></asp:Literal><br />
                                            <input id="ChkH" runat="server" type="checkbox" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <input id="ChkI" runat="server" type="checkbox" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="tc" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Literal ID="H_Nengetu" runat="server" Text="年月"></asp:Literal>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Literal ID="LitNengetu" runat="server"></asp:Literal>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="tc" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table class="tc col" width="360px" frame="void">
                                                <tr>
                                                    <td class="s1">
                                                        品目コード
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        仕入先
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table class="tl col" width="360px" frame="void">
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Literal ID="LitBuhinCode" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Literal ID="LitShiiresakiCode" runat="server"></asp:Literal>
                                                        ：
                                                        <asp:Literal ID="LitShiiresakiMei" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Literal ID="H_LeadTime" runat="server" Text="リードタイム"></asp:Literal>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Literal ID="LitLeadTime" runat="server"></asp:Literal>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="tc" width="100px"/>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Literal ID="H_Lot" runat="server" Text="発注ロット"></asp:Literal>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Literal ID="LitLot" runat="server"></asp:Literal>
                                            <asp:Literal ID="LitTani" runat="server"></asp:Literal>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="tc" width="100px"/>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Literal ID="H_RowItem" runat="server" Text="内容"></asp:Literal>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table class="tc col" width="80px" frame="void">
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Literal ID="RowItem1" runat="server" Text="入荷"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Literal ID="RowItem2" runat="server" Text="消費"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Literal ID="RowItem3" runat="server" Text="発注"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Literal ID="RowItem4" runat="server" Text="月末在庫"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="H_PlanN_1" runat="server" Text="前月"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table class="tr col" width="60px" frame="void">
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblNyukaSuN_1" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblShoyouSuN_1" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblHacchuSuN_1" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblZaikoSuN_1" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="H_PlanN0" runat="server" Text="N0"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table class="tr col" width="60px" frame="void">
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblNyukaSuN0" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblShoyouSuN0" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="s2">
<%--                                                        <asp:Label ID="LblHacchuSuN0" runat="server"></asp:Label>--%>
                                                        <asp:TextBox ID="TbxHacchuSuN0" runat="server" MaxLength="8" style="width: 58px;border: none; text-align: right; margin: 0px; white-space: nowrap;font-size:9pt;font-weight: normal;color:red;"
                                                            AutoPostBack="true" OnTextChanged="Tbx_TextChanged"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblZaikoSuN0" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="H_PlanN1" runat="server" Text="N1"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table class="tr col" width="60px" frame="void">
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblNyukaSuN1" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblShoyouSuN1" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblHacchuSuN1" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblZaikoSuN1" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="H_PlanN2" runat="server" Text="N2"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table class="tr col" width="60px" frame="void">
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblNyukaSuN2" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblShoyouSuN2" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblHacchuSuN2" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblZaikoSuN2" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="H_PlanN3" runat="server" Text="N3"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table class="tr col" width="60px" frame="void">
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblNyukaSuN3" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblShoyouSuN3" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblHacchuSuN3" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblZaikoSuN3" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="H_PlanN4" runat="server" Text="N4"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table class="tr col" width="60px" frame="void">
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblNyukaSuN4" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblShoyouSuN4" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblHacchuSuN4" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblZaikoSuN4" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="H_PlanN5" runat="server" Text="N5"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table class="tr col" width="60px" frame="void">
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblNyukaSuN5" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblShoyouSuN5" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblHacchuSuN5" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblZaikoSuN5" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="H_PlanTT" runat="server" Text="合計"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table class="tr col" width="60px" frame="void">
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblNyukaSuTT" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblShoyouSuTT" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="s2">
                                                        <asp:Label ID="LblHacchuSuTT" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblZaikoSuTT" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
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
    <input id="HidVal" type="hidden" name="HidVal" runat="server" />
<%--    <asp:button id="BtnPostback" runat="server" onclick="BtnPostback_Click"></asp:button>--%>
<%--    <input id="HidArg" type="hidden" name="HidArg" runat="server"/>--%>
<%--    <asp:button id="BtnDelPostback" runat="server" onclick="BtnDelPostback_Click" ></asp:button>--%>

<%--    <a id="LnkDownload" runat="server"></a>--%>

    <telerik:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest" UpdatePanelsRenderMode="Inline">
        <ClientEvents OnRequestStart="OnRequestStart" OnResponseEnd="OnResponseEnd" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="DivD">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DivD" LoadingPanelID="LP"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="LP" runat="server" Skin="Web20">
    </telerik:RadAjaxLoadingPanel>

    <input type="hidden" id="count" runat="server" />

    </form>
</body>
</html>


