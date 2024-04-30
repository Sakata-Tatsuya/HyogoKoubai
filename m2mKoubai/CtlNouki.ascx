<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlNouki.ascx.cs" Inherits="m2mKoubai.CtlNouki" %>

<table runat="server" id="T" border="0" cellpadding="2" cellspacing="0" bordercolor="#000000" class="col" frame="void" >
    <tr>
        <td nowrap="nowrap" >
            <asp:GridView  ID="G" runat="server" AutoGenerateColumns="False" BackColor="White" 
            CssClass="def tl@"  ShowFooter="True" OnRowDataBound="G_RowDataBound" Width="180px">
                  <Columns>
                    <asp:TemplateField HeaderText="”[Šú">
                        <ItemTemplate>
                            <asp:TextBox ID="TbxN" runat="server" MaxLength="6" Width="60px" ></asp:TextBox><br />
                            <asp:TextBox ID="TbxKTNo" CssClass="tr" runat="server" MaxLength="14" Width="60px" >
                            </asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="”—Ê">
                        <FooterTemplate>
                            <asp:TextBox ID="TbxSum" runat="server" Width="60px"  MaxLength="14" BorderStyle="none"
                            ReadOnly="True"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate >
                            <asp:TextBox ID="TbxS" runat="server" Width="60px" Style="ime-mode: inactive" MaxLength="14" CssClass="tr">
                            </asp:TextBox>
                        </ItemTemplate>
                       
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="íœ">
                    <ItemTemplate>
                            <asp:HyperLink ID="L" runat="server" ImageUrl="~/img/Close.gif"
                                ToolTip="íœ"></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Wrap="False" />
                       
                    </asp:TemplateField>
                </Columns>
                 <HeaderStyle  BackColor="#ffff99" />
                
            </asp:GridView>
        
        </td>
    </tr>
    <tr  >
        <td  nowrap="nowrap">
            <asp:Button ID="BtnTouroku" runat="server" Text="“o˜^" Font-Size="8pt" />
            <asp:Button ID="BtnClose" runat="server" Text="•Â‚¶‚é" Font-Size="8pt" />
            <asp:Button ID="BtnRowAdd" runat="server" Text="s‚ð’Ç‰Á" Font-Size="8pt" />
            <input id="KTNO" runat="server" type="hidden" />
            <input id="HS" runat="server" type="hidden" />
            <input id="HN" runat="server" type="hidden" /></td>
    </tr>
</table>
