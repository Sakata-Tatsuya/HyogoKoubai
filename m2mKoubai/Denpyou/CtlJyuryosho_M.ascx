<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlJyuryosho_M.ascx.cs" Inherits="m2mKoubai.Denpyou.CtlJyuryosho_M" %>
<link href="../MainStyle.css" rel="stylesheet" type="text/css" />
<asp:GridView ID="G" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                CssClass="def9" OnRowDataBound="G_RowDataBound" Width="100%">
                <Columns>
                    <asp:BoundField HeaderText="����No">
                    <ItemStyle CssClass="tc" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="������" >
                        <ItemStyle CssClass="tc" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="�i��&lt;br&gt;�O���[�v">                         
                        <ItemStyle CssClass="tc" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="�i�ڃR�[�h">
                    <ItemStyle CssClass="tc" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�i�ږ�">                                      
                        <HeaderStyle Wrap="False" />
                        <ItemStyle CssClass="tl" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="����">
                        <ItemStyle CssClass="tr" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�P��">
                        <HeaderStyle Wrap="False" />
                        <ItemStyle CssClass="tc" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="�P��">
                        <ItemStyle CssClass="tr" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="���z">
                        <ItemStyle CssClass="tr" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                  
                </Columns>
            </asp:GridView>