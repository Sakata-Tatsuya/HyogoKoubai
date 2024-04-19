<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlSeikyuMeisaisho_M.ascx.cs" Inherits="m2mKoubai.Denpyou.CtlSeikyuMeisaisho_M" %>
<link href="../MainStyle.css" rel="stylesheet" type="text/css" />
<asp:GridView id="G" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="def10 tc" OnRowDataBound="G_RowDataBound">
                <Columns>
                    <asp:BoundField HeaderText="発行No" ></asp:BoundField>
                    <asp:BoundField HeaderText="品目コード" ></asp:BoundField>
                    <asp:BoundField HeaderText="品目名" >
                        <ItemStyle CssClass="tl" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="数量" >
                        <ItemStyle CssClass="tr" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="単位">
                        <EditItemTemplate>
                          
                        </EditItemTemplate>
                        <ItemTemplate>
                          
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="単価">
                        <EditItemTemplate>
                           
                        </EditItemTemplate>
                        <ItemTemplate>
                           
                        </ItemTemplate>
                        <ItemStyle CssClass="tr" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="金額" >
                        <ItemStyle CssClass="tr" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>