<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlOrderUpload.ascx.cs" Inherits="Koubai.Upload.CtlOrderUpload" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<table id="TblAll" runat="server" class="def9">
    <tr>
        <td align="right" valign="top">
            <table id="TblMain" runat="server" border="1" style="border-color: #000000;" class="col tl bg1">
                <tr>
                    <td class="bg4">
                        発注担当者
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlHachuTantousha" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="bg4">
                        発注日
                    </td>
                    <td>
                        <radCln:RadDatePicker ID="RdpHachuBi" runat="server" SharedCalendarID="SC">
                        </radCln:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td class="bg4">
                        税率
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlTax" runat="server">
                            <asp:ListItem Value="10">10%</asp:ListItem>
                            <asp:ListItem Value="8">8%</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="bg4">
                        取込みファイル
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                </tr>
            </table>
            <asp:LinkButton ID="LnkCsvSample" runat="server" OnClick="LnkCsvSample_Click">サンプル(CSV)</asp:LinkButton>
            <asp:LinkButton ID="LnkTabSample" runat="server" OnClick="LnkTabSample_Click">サンプル(TAB)</asp:LinkButton>
        </td>
        <td valign="top" style="white-space: nowrap;">
            <asp:DropDownList ID="DdlDataType" runat="server">
                <asp:ListItem Value="CSV">CSV(カンマ区切り)</asp:ListItem>
                <asp:ListItem Value="TAB">テキスト(タブ区切り)</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="BtnUpload" runat="server" Text="アップロード" OnClick="BtnUpload_Click" />
            <br />
            <asp:CheckBox ID="ChkHeader" runat="server" Checked="True" Text="ヘッダーを含める" />
            <div style="margin-top: 4px; padding-left: 2px;">
                <asp:Label ID="LblMsg" runat="server"></asp:Label>
            </div>
        </td>
    </tr>
</table>
<radCln:RadCalendar ID="SC" runat="server" Skin="Web20">
</radCln:RadCalendar>
