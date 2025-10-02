<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlItemEdit.ascx.cs" Inherits="Koubai.Common.CtlItemEdit" %>
<%--<%@ Register Src="CtlSort2.ascx" TagName="CtlSort2" TagPrefix="uc1" %>--%>
<table cellspacing="0">
    <tr>
        <td>
            <asp:Button ID="BtnDefault" runat="server" Text="標準設定を表示する" OnClick="BtnDefault_Click"></asp:Button>
        </td>
        <td>
            <asp:Button ID="BtnReg" runat="server" Width="80px" Text="登録" OnClick="BtnReg_Click"></asp:Button>
        </td>
        <td>
            <asp:Button ID="BtnBack" runat="server" Width="80px" Text="戻る"
                OnClick="BtnBack_Click" Visible="False"></asp:Button>
        </td>
    </tr>
</table>
<table class="def" id="TWSDR" cellspacing="0" cellpadding="2" border="0" style="margin-bottom: 4px">
    <tr>
        <td nowrap="nowrap">
            <asp:Button ID="BtnReset" runat="server" Text="リセット" OnClick="BtnReset_Click"></asp:Button>
            <asp:Button ID="BtnCombine" Text="選択した列を結合" runat="server"
                OnClick="BtnCombine_Click"></asp:Button></td>
        <td nowrap="nowrap">
<%--            <uc1:CtlSort2 ID="ST" runat="server" Visible="False" />--%>
        </td>
    </tr>
</table>
<asp:Label ID="LblMsg" runat="server" CssClass="def"></asp:Label>
<asp:DataGrid ID="D" runat="server" CssClass="def" CellPadding="2"
    BorderWidth="1px" BorderColor="Black"
    AutoGenerateColumns="False" OnItemDataBound="D_ItemDataBound">
    <AlternatingItemStyle CssClass="alt"></AlternatingItemStyle>
    <HeaderStyle CssClass="dgd_hd"></HeaderStyle>
    <Columns>
        <asp:TemplateColumn HeaderText="選択">
            <ItemTemplate>
                <input id="V" type="checkbox" size="1" runat="server">
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="列順">
            <ItemStyle HorizontalAlign="Center"></ItemStyle>
            <ItemTemplate>
                <asp:DropDownList ID="A" runat="server"></asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="項目名">
            <HeaderStyle Wrap="False"></HeaderStyle>
            <ItemStyle Wrap="False" CssClass="fit"></ItemStyle>
            <ItemTemplate>
                <asp:Literal ID="L" runat="server"></asp:Literal>
                <asp:DataGrid ID="C" runat="server" AutoGenerateColumns="False" BorderColor="Black" CellPadding="2"
                    CssClass="def" ShowHeader="False" Frame="void" Border="1" Width="100%">
                    <Columns>
                        <asp:TemplateColumn>
                            <HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:DropDownList ID="B" runat="server"></asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn>
                            <HeaderStyle Wrap="False" Width="100%"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn Visible="False" HeaderText="Field"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="列の結合">
            <ItemStyle HorizontalAlign="Center"></ItemStyle>
            <ItemTemplate>
                <asp:Button ID="BK" runat="server" Text="結合解除"></asp:Button>
                <input id="K" type="checkbox" size="1" runat="server">
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:BoundColumn Visible="False" HeaderText="Field"></asp:BoundColumn>
    </Columns>
</asp:DataGrid>
<telerik:RadAjaxManagerProxy ID="R" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="BtnCombine">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="LblMsg" UpdatePanelRenderMode="Inline" />
                <telerik:AjaxUpdatedControl ControlID="D" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="BtnCommand">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="LblMsg" UpdatePanelRenderMode="Inline" />
                <telerik:AjaxUpdatedControl ControlID="D" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<asp:Button ID="BtnCommand" runat="server" OnClick="BtnCommand_Click"></asp:Button>