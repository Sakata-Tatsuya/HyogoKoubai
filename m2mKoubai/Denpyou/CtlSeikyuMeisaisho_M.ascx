<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlSeikyuMeisaisho_M.ascx.cs" Inherits="m2mKoubai.Denpyou.CtlSeikyuMeisaisho_M" %>
<link href="../MainStyle.css" rel="stylesheet" type="text/css" />
<asp:GridView id="G" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="def10 tc" OnRowDataBound="G_RowDataBound">
                <Columns>
                    <asp:BoundField HeaderText="���sNo" ></asp:BoundField>
                    <asp:BoundField HeaderText="�i�ڃR�[�h" ></asp:BoundField>
                    <asp:BoundField HeaderText="�i�ږ�" >
                        <ItemStyle CssClass="tl" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="����" >
                        <ItemStyle CssClass="tr" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="�P��">
                        <EditItemTemplate>
                          
                        </EditItemTemplate>
                        <ItemTemplate>
                          
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�P��">
                        <EditItemTemplate>
                           
                        </EditItemTemplate>
                        <ItemTemplate>
                           
                        </ItemTemplate>
                        <ItemStyle CssClass="tr" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="���z" >
                        <ItemStyle CssClass="tr" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>