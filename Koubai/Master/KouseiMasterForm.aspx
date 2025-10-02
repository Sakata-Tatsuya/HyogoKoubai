<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KouseiMasterForm.aspx.cs" Inherits="Koubai.Master.KouseiMasterForm" %>

<%@ Register TagPrefix="uc1" TagName="CtlMainMenu" Src="~/CtlMainMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="CtlKouseiMasterUpload" Src="~/Master/CtlKouseiMasterUpload.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<%@ Register Src="../Common/CtlFilter.ascx" TagName="CtlFilter" TagPrefix="uc2" %>
<%@ Register Src="../Common/CtlItemEdit.ascx" TagName="CtlItemEdit" TagPrefix="uc4" %>--%>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head runat="server">
    <title>構成マスタ</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="~/Style/Grid.ykuri.css" rel="stylesheet" />
    <link href="~/Style/ComboBox.ykuri.css" type="text/css" rel="stylesheet" />
    <link href="~/MainStyles.css" type="text/css" rel="stylesheet" />
    <link href="~/Style/TreeList.Office2007.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/TreeList.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .khd3 {
            background-color: #20b2aa;
            color: White;
        }

        .td {
            border: solid 1px #708090;
            white-space: nowrap;
        }

        .c {
            cursor: pointer;
        }

        .rcbReadOnly {
            display: none;
        }

        .rtlPagerLabel {
            display: none;
        }

        .none {
            display: none;
        }

        .b {
            font-size: 10px;
            height: 19px;
            width: 70px;
        }
    </style>

<%--    <script type="text/javascript" src="../../Core.js"></script>--%>

    <telerik:RadScriptBlock ID="RSM" runat="server">
        <script type="text/javascript">
            function DeleteCheck() {

                if (!confirm('削除しますか？')) {
                    return false;
                }
                __doPostBack("<%=BtnDel.UniqueID%>");
                return false;
            }
        </script>
    </telerik:RadScriptBlock>
</head>
<body bottommargin="0" leftmargin="4" topmargin="0" rightmargin="4" style="overflow: auto">
    <form id="MasterForm" method="post" runat="server">
        <uc1:CtlMainMenu ID="M" runat="server"></uc1:CtlMainMenu>
        <div id="divMain" runat="server">
            <div>
                <table style="border-collapse: collapse; display: none;">
                    <tr>
                        <td>
                            <table style="border-collapse: collapse;">
                                <tr>
                                    <td class="khd3 td">
                                        <asp:Literal ID="Literal3" runat="server" Text="品目番号"></asp:Literal>
                                    </td>
                                    <td class="khd3 td" style="display: none">
                                        <asp:Literal ID="Literal2" runat="server" Text="品目名"></asp:Literal>
                                    </td>
                                    <td rowspan="2" class="td" style="background-color: Teal"></td>
                                </tr>
                                <tr>
                                    <td class="td">
                                        <asp:TextBox ID="TbxHinban" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="td" style="display: none">
                                        <asp:TextBox ID="TbxHinmei" runat="server"></asp:TextBox>
                                        <asp:HiddenField ID="HidHinmei" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table class="def" id="t000" style="margin-bottom: 4px; border-collapse: collapse"
                    bordercolor="#0" cellspacing="0" cellpadding="2" border="1">
                    <tr>
                        <td class="hd" nowrap="nowrap">
                            <asp:Literal ID="LitHinmokuBan" runat="server" Text="製品番号"></asp:Literal>
                            <br />
                    </td>
                        <td nowrap="nowrap">
                            <table cellpadding="0" class="def" style="margin-bottom: 3px">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="HM" runat="server" AllowCustomText="True" EnableLoadOnDemand="True"
                                            Height="180px" NoWrap="True" OnItemsRequested="HM_ItemsRequested"
                                            ShowMoreResultsBox="True" ShowToggleImage="False" Skin="Simple" Width="320px"
                                            AutoPostBack="True" EnableVirtualScrolling="True">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td nowrap="nowrap">
                                       <%-- <asp:CheckBox ID="ChkAssist" runat="server" Text="入力補助" AutoPostBack="True" Checked="True"
                                            OnCheckedChanged="ChkAssist_CheckedChanged"></asp:CheckBox>--%>
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="0" class="def" runat="server" id="TblRemodel" style="display: none">
                                <tr>
                                    <td nowrap="nowrap">
                                        <asp:Literal ID="LitRemodelTitle" runat="server" Text="リモデル"></asp:Literal>
                                        :
                                </td>
                                    <td>
                                        <telerik:RadComboBox ID="RM" runat="server" Height="180px" NoWrap="True" Skin="Web20"
                                            Width="210px" ShowDropDownOnTextboxClick="False" AllowCustomText="True">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td nowrap="nowrap">
                            <asp:Button ID="BtnKensaku" runat="server" Text="検索" Style="width: 80px; height: 30px;"
                                OnClick="BtnKensaku_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnList" runat="server" Text="登録一覧" Style="width: 80px; height: 30px;" OnClick="btnList_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnUpdate" runat="server" Text="修正/新規登録" Style="height: 30px;" OnClick="btnUpdate_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnDL" runat="server" Text="BOMﾀﾞｳﾝﾛｰﾄﾞ" Style="height: 30px;" OnClick="btnDL_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnUP" runat="server" Text="BOMｱｯﾌﾟﾛｰﾄﾞ" Style="height: 30px;" OnClick="btnUP_Click" />
                        </td>
                        <td>
                            <asp:Button ID="BtnDel" runat="server" OnClick="BtnDel_Click" Visible="false" />
                            <asp:Button ID="BtnDelCheck" runat="server" Text="BOM削除" Height="30px" Width="80px" OnClientClick="DeleteCheck();return false;" />
                        </td>
                    </tr>
                </table>


            </div>
            <asp:Label ID="LblMsg" runat="server"></asp:Label>
            <div id="divKensaku" runat="server">
                <telerik:RadGrid ID="DG" runat="server" CssClass="def" PageSize="50" AllowPaging="True"
                    EnableAJAX="True" EnableAJAXLoadingTemplate="True" Skin="ykuri" AllowCustomPaging="True"
                    EnableEmbeddedSkins="False" GridLines="None" OnPageIndexChanged="DG_PageIndexChanged"
                    OnPageSizeChanged="DG_PageSizeChanged" CellPadding="0" EnableEmbeddedBaseStylesheet="False"
                    OnPreRender="DG_PreRender" OnItemDataBound="DG_ItemDataBound" OnItemCreated="DG_ItemCreated" OnItemCommand="DG_ItemCommand" Width="630px">
                    <HeaderContextMenu EnableAutoScroll="True">
                    </HeaderContextMenu>
                    <%--<AlternatingItemStyle CssClass="alt"></AlternatingItemStyle>--%>
                    <HeaderStyle CssClass="hd tc" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </HeaderStyle>
                    <MasterTableView CellPadding="2" AutoGenerateColumns="False" GridLines="Both" Border="0"
                        CellSpacing="0">
                        <Columns>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkSelect" runat="server" OnCheckedChanged="ChkSelect_CheckedChanged"/>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" Width="10px" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="btnS" runat="server" Text="参照" />
                                </ItemTemplate> 
                                <ItemStyle Wrap="False" Width="10px" HorizontalAlign="Center"/>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="SeihinCode" HeaderText="製品番号">
                                <ItemStyle Wrap="False" Width="130px" HorizontalAlign="Left"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="SeihinMei" HeaderText="製品名称">
                                <ItemStyle Wrap="False" Width="350px" HorizontalAlign="Left"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="SeihinM" HeaderText="製品ﾏｽﾀ">
                                <ItemStyle Wrap="False" Width="55px" HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="KouseiM" HeaderText="部品表">
                                <ItemStyle Wrap="False" Width="55px" HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="UpdateTime" HeaderText="登録/更新日">
                                <ItemStyle Wrap="False" Width="150px" HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <EditFormSettings>
                            <EditColumn InsertImageUrl="Update.gif" UpdateImageUrl="Update.gif" EditImageUrl="Edit.gif"
                                CancelImageUrl="Cancel.gif">
                            </EditColumn>
                        </EditFormSettings>
                        <ItemStyle Wrap="False"></ItemStyle>
                        <HeaderStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="radgrid_header_def" HorizontalAlign="Center"></HeaderStyle>
                        <AlternatingItemStyle Wrap="False"></AlternatingItemStyle>
                        <PagerStyle Position="Top" PagerTextFormat="ページ移動: {4} &amp;nbsp;ページ : &lt;strong&gt;{0:N0}&lt;/strong&gt; / &lt;strong&gt;{1:N0}&lt;/strong&gt; | 件数: &lt;strong&gt;{2:N0}&lt;/strong&gt; - &lt;strong&gt;{3:N0}件&lt;/strong&gt; / &lt;strong&gt;{5:N0}&lt;/strong&gt;件中"
                            PageSizeLabelText="ページサイズ:" FirstPageToolTip="最初のページに移動" LastPageToolTip="最後のページに移動"
                            NextPageToolTip="次のページに移動" PrevPageToolTip="前のページに移動" />
                    </MasterTableView>
                    <ClientSettings>
                        <%--<ClientEvents OnGridCreated="Core.ResizeRadGrid" />--%>
                        <Scrolling AllowScroll="false" UseStaticHeaders="True" />
                    </ClientSettings>
                    <FilterMenu EnableEmbeddedSkins="False">
                    </FilterMenu>
                </telerik:RadGrid>
            </div>
        </div>
        
        <div id="divUpload" runat="server">
            <asp:Button ID="BtnUpReg" runat="server" Text="登録" OnClick="BtnUpReg_Click" />
            <asp:Button ID="BtnBack" runat="server" Text="戻る" OnClick="BtnBack_Click" />
          <uc2:CtlKouseiMasterUpload ID="CtlKouseiMasterUpload" runat="server"></uc2:CtlKouseiMasterUpload>
        </div>

        <div id="DivListMain" runat="server">
            <div id="DivD" runat="server">
            </div>
            <div id="DivTree" runat="server" style="overflow-y: scroll; border: solid 1px silver;">
                <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                    <telerik:RadTreeList ID="Rtl" runat="server" AllowPaging="True" PageSize="1000" DataKeyNames="BuhinCode" Skin="Office2007"
                        ParentDataKeyNames="SeihinCode" AutoGenerateColumns="False" OnNeedDataSource="Rtl_NeedDataSource" EnableEmbeddedBaseStylesheet="False" OnPreRender="Rtl_PreRender" CommandItemDisplay="None" Culture="ja-JP" IsExporting="False" OnItemDataBound="Rtl_ItemDataBound">
                        <Columns>
                            <telerik:TreeListBoundColumn UniqueName="No" DataField="No" HeaderText="">
                            </telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn UniqueName="Lv" HeaderText="LV">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:TreeListBoundColumn>
                            
                            <telerik:TreeListBoundColumn UniqueName="SeihinCode" DataField="HyoujiSeihinCode" HeaderText="製品番号">
                            </telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn UniqueName="BuhinCode" DataField="BuhinCode" HeaderText="部品番号">
                            </telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn UniqueName="MakerCode" DataField="MakerCode" HeaderText="メーカー品番">
                            </telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn UniqueName="BuhinName" DataField="BuhinName" HeaderText="部品名">
                            </telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn UniqueName="ORNo" DataField="ORNo" HeaderText="OR品" HeaderStyle-HorizontalAlign="Center">
                            </telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn UniqueName="SelectOR" DataField="SelectOR" HeaderText="" HeaderStyle-Width="40px">
                            </telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn UniqueName="Insu" DataField="Insu" HeaderText="員数" ItemStyle-HorizontalAlign="Right">
                            </telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn UniqueName="KouteiCode" DataField="KouteiCode" HeaderText="工程コード">
                            </telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn UniqueName="KouteiMei" DataField="KouteiMei" HeaderText="工程名">
                            </telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn UniqueName="HinmokuBunruiCode" DataField="HinmokuBunruiCode" HeaderText="品目分類コード">
                            </telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn UniqueName="HinmokuBunrui" DataField="HinmokuBunrui" HeaderText="品目分類">
                            </telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn UniqueName="HinmokuCode" DataField="HinmokuCode" HeaderText="品目コード">
                            </telerik:TreeListBoundColumn>
                          <telerik:TreeListBoundColumn UniqueName="Hinmei" DataField="Hinmei" HeaderText="品名">
                            </telerik:TreeListBoundColumn>
                            
                            <%--<telerik:TreeListBoundColumn UniqueName="tc" DataField="TorihikisakiCode" HeaderText="取引先コード">
                                </telerik:TreeListBoundColumn>--%>
                            <%--<telerik:TreeListBoundColumn UniqueName="tn" DataField="TorihikisakiName" HeaderText="取引先名">
                                </telerik:TreeListBoundColumn>--%>
                            <telerik:TreeListBoundColumn UniqueName="MarumeSu" DataField="MarumeSu" HeaderText="まるめ数" ItemStyle-HorizontalAlign="Right">
                            </telerik:TreeListBoundColumn>
                            
                            <telerik:TreeListBoundColumn UniqueName="UpdateValue" DataField="UpdateValue" HeaderText="変更項目" ItemStyle-HorizontalAlign="Right">
                            </telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn UniqueName="BeforeValue" DataField="BeforeValue" HeaderText="変更前" ItemStyle-HorizontalAlign="Right">
                            </telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn UniqueName="AfterValue" DataField="AfterValue" HeaderText="変更後" ItemStyle-HorizontalAlign="Right">
                            </telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn UniqueName="Syounin" DataField="Syounin" HeaderText="承認" ItemStyle-HorizontalAlign="Right">
                            </telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn UniqueName="Tantou" DataField="Tantou" HeaderText="担当" ItemStyle-HorizontalAlign="Right">
                            </telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn UniqueName="Bikou1" DataField="Bikou1" HeaderText="備考1" ItemStyle-HorizontalAlign="Left">
                            </telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn UniqueName="Bikou2" DataField="Bikou2" HeaderText="備考2" ItemStyle-HorizontalAlign="Left">
                            </telerik:TreeListBoundColumn>
                            <telerik:TreeListBoundColumn UniqueName="UpdateDate" DataField="UpdateDate" HeaderText="最終変更日時" ItemStyle-HorizontalAlign="Right">
                            </telerik:TreeListBoundColumn>
                        </Columns>
                        <PagerStyle Position="TopAndBottom" />
                    </telerik:RadTreeList>
                </telerik:RadAjaxPanel>
            </div>
        </div>

        <telerik:RadAjaxLoadingPanel ID="LP" runat="server" Skin="Web20">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxManager ID="Ram" runat="server">
        </telerik:RadAjaxManager>
        <telerik:RadWindow ID="RW" runat="server" Behaviors="Resize, Close, Move" AutoSize="True"
            ReloadOnShow="True" Skin="Web20" ShowContentDuringLoad="false" VisibleStatusbar="false"
            AutoSizeBehaviors="WidthProportional" Height="400">
        </telerik:RadWindow>
        <telerik:RadWindow ID="RW_UP" runat="server" Behaviors="Resize, Close, Move" AutoSize="True"
            ReloadOnShow="false" Skin="Web20" ShowContentDuringLoad="false" VisibleStatusbar="false"
            AutoSizeBehaviors="WidthProportional">
        </telerik:RadWindow>
        <telerik:RadWindow ID="RW_DW" runat="server" Behaviors="Resize, Close, Move" AutoSize="True"
            ReloadOnShow="false" Skin="Web20" ShowContentDuringLoad="false" VisibleStatusbar="false"
            AutoSizeBehaviors="WidthProportional">
        </telerik:RadWindow>
        <input type="hidden" id="HidReloadCmd" runat="server" />
        <input type="hidden" id="HidReloadArg" runat="server" />
    </form>
</body>
</html>
