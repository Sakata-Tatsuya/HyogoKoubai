<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlFilter.ascx.cs" Inherits="Koubai.Common.CtlFilter" %>
<%@ Register Assembly="Core" Namespace="Core.Web" TagPrefix="cc1" %>
<%@ Register Src="CtlNengappiFromTo.ascx" TagName="CtlNengappiFromTo" TagPrefix="uc1" %>
<asp:GridView ID="D" runat="server" AutoGenerateColumns="False" BorderColor="Black"
    BorderWidth="1px" CssClass="def" CellPadding="2">
    <Columns>
        <asp:TemplateField HeaderText="項目">
            <ItemTemplate>
                <asp:DropDownList ID="I" runat="server">
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="値">
            <ItemTemplate>
                <div id="V" runat="server">
                    <cc1:FilterTextBox ID="F" runat="server" TextMode="MultiLine">
                        <cc1:filteritem FilterType="EqualTo" Text="と等しい"></cc1:FilterItem>
                        <cc1:FilterItem FilterType="NotEqualTo" Text="と等しくない"></cc1:FilterItem>
                        <cc1:FilterItem FilterType="Contains" Text="を含む"></cc1:FilterItem>
                        <cc1:FilterItem FilterType="DoesNotContain" Text="を含まない"></cc1:FilterItem>
                        <cc1:FilterItem FilterType="StartsWith" Text="で始まる"></cc1:FilterItem>
                        <cc1:FilterItem FilterType="EndsWith" Text="で終わる"></cc1:FilterItem>
                    </cc1:FilterTextBox>
                    <asp:CheckBox ID="CK" runat="server" />
                    <uc1:CtlNengappiFromTo ID="N" runat="server" />
                    <asp:DropDownList ID="O" runat="server"></asp:DropDownList>
                </div>
            </ItemTemplate>
            <ItemStyle Wrap="False" />
        </asp:TemplateField>
    </Columns>
    <HeaderStyle CssClass="hd" />
    <AlternatingRowStyle CssClass="alt" />
</asp:GridView>
<asp:Button ID="B" runat="server" OnClick="B_Click" />

