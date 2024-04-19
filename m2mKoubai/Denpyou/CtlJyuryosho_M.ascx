<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlJyuryosho_M.ascx.cs" Inherits="m2mKoubai.Denpyou.CtlJyuryosho_M" %>
<link href="../MainStyle.css" rel="stylesheet" type="text/css" />
<asp:GridView ID="G" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                CssClass="def9" OnRowDataBound="G_RowDataBound" Width="100%">
                <Columns>
                    <asp:BoundField HeaderText="発注No">
                    <ItemStyle CssClass="tc" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="発注日" >
                        <ItemStyle CssClass="tc" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="品目&lt;br&gt;グループ">                         
                        <ItemStyle CssClass="tc" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="品目コード">
                    <ItemStyle CssClass="tc" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="品目名">                                      
                        <HeaderStyle Wrap="False" />
                        <ItemStyle CssClass="tl" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="数量">
                        <ItemStyle CssClass="tr" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="単位">
                        <HeaderStyle Wrap="False" />
                        <ItemStyle CssClass="tc" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="単価">
                        <ItemStyle CssClass="tr" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="金額">
                        <ItemStyle CssClass="tr" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                  
                </Columns>
            </asp:GridView>