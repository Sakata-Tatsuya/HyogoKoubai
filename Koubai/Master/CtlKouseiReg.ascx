<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlKouseiReg.ascx.cs" Inherits="Koubai.Master.CtlKouseiReg" %>
<%@ Register TagPrefix="cc2" Namespace="Core.Web" Assembly="Core" %>
<%@ Register TagPrefix="cc1" Namespace="Core.Web.DataBindControls" Assembly="Core" %>
<link href="../MainStyles.css" type="text/css" rel="STYLESHEET">

<table id="G">
    <tr>
        <td>
            <table class="def" style="margin-bottom: 8px; border-collapse: collapse" cellspacing="0" cellpadding="2">
                <tr>
                    <td height="22px" width="100px">
                        <asp:Literal ID="Literal7" runat="server" Text=""></asp:Literal>
                    </td>
                    <td height="22px" width="170px" align="center">
                        <asp:Literal ID="Literal9" runat="server" Text="修正前"></asp:Literal>
                    </td>
                    <td></td>
                    <td height="22px" width="275px" align="center">
                        <asp:Literal ID="Literal8" runat="server" Text="修正後"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="hd" height="22px" style="border:1px solid;" >
                        <asp:Literal ID="Literal18" runat="server" Text="登録日"></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlTourokuDate" runat="server" Text=""></asp:Literal>
                    </td>
                    <td rowspan="18" style="vertical-align: middle">⇒
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlTourokuDateT" runat="server" Text=""></asp:Literal>

                    </td>
                </tr>
                <tr>
                    <td class="hd" height="22px" style="border:1px solid;">
                        <asp:Literal ID="Literal1" runat="server" Text="製品番号"></asp:Literal>
                    </td>
                    <td height="22px" width="150px" style="border:1px solid;">
                        <asp:Literal ID="ltlSeihinCode" runat="server" Text=""></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:TextBox ID="tbxSeihinCode" runat="server" Text=""></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td class="hd" height="22px" style="border:1px solid;">
                        <asp:Literal ID="Literal2" runat="server" Text="部品番号"></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlBuhinCode" runat="server" Text=""></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:TextBox ID="tbxBuhinCode" runat="server" Text=""></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td class="hd" height="22px" style="border:1px solid;">
                        <asp:Literal ID="Literal19" runat="server" Text="ﾒｰｶｰ品番"></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlMakerCode" runat="server" Text=""></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="Literal20" runat="server" Text="ﾒｰｶｰ品番は部品マスタで修正"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="hd" height="22px" style="border:1px solid;">
                        <asp:Literal ID="Literal10" runat="server" Text="部品名"></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlBuhinMei" runat="server" Text=""></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlBuhinName" runat="server" Text="部品名は部品マスタで修正"></asp:Literal>
                    </td>

                </tr>
                <tr>
                    <td class="hd" height="22px" style="border:1px solid;">
                        <asp:Literal ID="Literal17" runat="server" Text="OR品"></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlOR" runat="server" Text=""></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:CheckBoxList ID="ChkbORNo" runat="server"></asp:CheckBoxList>
                        <input type="hidden" id="HidORNo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="hd" height="22px" style="border:1px solid;">
                        <asp:Literal ID="Literal3" runat="server" Text="工程コード"></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlKouteiCode" runat="server" Text=""></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlKouteiCodeN" runat="server" Text=""></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="hd" height="22px" style="border:1px solid;">
                        <asp:Literal ID="Literal12" runat="server" Text="工程名"></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlkouteiMei" runat="server" Text=""></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlkouteiMeiN" runat="server" Text=""></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="hd" height="22px" style="border:1px solid;">
                        <asp:Literal ID="Literal13" runat="server" Text="品目分類コード"></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlHinmokuBunruiCode" runat="server" Text=""></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlHinmokuBunruiCodeN" runat="server" Text=""></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="hd" height="22px" style="border:1px solid;">
                        <asp:Literal ID="Literal14" runat="server" Text="品目分類"></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlHinmokuBunrui" runat="server" Text=""></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlHinmokuBunruiN" runat="server" Text=""></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="hd" height="22px" style="border:1px solid;">
                        <asp:Literal ID="Literal15" runat="server" Text="品目コード"></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlHinmokuCode" runat="server" Text=""></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlHinmokuCodeN" runat="server" Text=""></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="hd" height="22px" style="border:1px solid;">
                        <asp:Literal ID="Literal16" runat="server" Text="品名"></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlHinmei" runat="server" Text=""></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="Literal21" runat="server" Text="品目コードは品目分類マスタで修正"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="hd" height="22px" style="border:1px solid;">
                        <asp:Literal ID="Literal11" runat="server" Text="員数"></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlInsu" runat="server" Text=""></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:TextBox ID="tbxInsu" runat="server" Text=""></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="hd" height="22px" style="border:1px solid;">
                        <asp:Literal ID="Literal4" runat="server" Text="承認"></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlSyounin" runat="server" Text=""></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:TextBox ID="TbxSyounin" runat="server" Text=""></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="hd" height="22px" style="border:1px solid;">
                        <asp:Literal ID="Literal5" runat="server" Text="担当"></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltltantou" runat="server" Text=""></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:TextBox ID="TbxTantou" runat="server" Text=""></asp:TextBox>
                    </td>
                </tr>
                
                 <tr>
                    <td class="hd" height="22px" style="border:1px solid;">
                        <asp:Literal ID="Literal22" runat="server" Text="備考1"></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlBikou1" runat="server" Text=""></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:TextBox ID="TbxBikou1" runat="server" Text=""></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td class="hd" height="22px" style="border:1px solid;">
                        <asp:Literal ID="Literal25" runat="server" Text="備考2"></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="ltlBikou2" runat="server" Text=""></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:TextBox ID="TbxBikou2" runat="server" Text=""></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="hd" height="22px" style="border:1px solid;">
                        <asp:Literal ID="Literal6" runat="server" Text="状態"></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="hogeDelete" runat="server" Text=""></asp:Literal>
                    </td>
                    <td height="22px" style="border:1px solid;">
                        <asp:Literal ID="LtlDeleteFlag" runat="server" Text="有効"></asp:Literal>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<cc1:DataBindControlManager ID="U" runat="server" Color="Purple"></cc1:DataBindControlManager>
<cc1:DataBindControlManager ID="S" runat="server" Color="Blue"></cc1:DataBindControlManager>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RblLibrary">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RblLibrary" />
                <telerik:AjaxUpdatedControl ControlID="CblLibrary" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
