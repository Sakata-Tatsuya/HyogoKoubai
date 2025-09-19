<%@ Control Language="C#" AutoEventWireup="true" Codebehind="CtlHinmokuUpload.ascx.cs"
    Inherits="Koubai.Upload.CtlHinmokuUpload" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<table id="TblAll" runat="server" class="def9">
    <tr>
        <td align="right" valign="top">
            <table id="TblMain" runat="server" border="1" style="border-color: #000000;" class="col tl bg1">
                <tr>
                    <td class="bg4">
                        �捞�ݓ�</td>
                    <td>
                        <radCln:RadDatePicker ID="RdpUploadDate" runat="server" SharedCalendarID="SC">
                        </radCln:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td class="bg4">
                        �捞�݃t�@�C��</td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" /></td>
                </tr>
            </table>
            <asp:LinkButton ID="LnkCsvSample" runat="server" OnClick="LnkCsvSample_Click" >�T���v��(CSV)</asp:LinkButton>            
            <asp:LinkButton ID="LnkTabSample" runat="server" OnClick="LnkTabSample_Click" >�T���v��(TAB)</asp:LinkButton>
        </td>
        <td valign="top" style="white-space:nowrap;">
            <asp:DropDownList ID="DdlDataType" runat="server">
                <asp:ListItem Value="CSV">CSV(�J���}��؂�)</asp:ListItem>
                <asp:ListItem Value="TAB">�e�L�X�g(�^�u��؂�)</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="BtnUpload" runat="server" Text="�A�b�v���[�h" OnClick="BtnUpload_Click" />
            <br />
            <asp:CheckBox ID="ChkHeader" runat="server" Checked="True" Text="�w�b�_�[���܂߂�" />
            <div style="margin-top:4px; padding-left:2px;">
                <asp:Label ID="LblMsg" runat="server"></asp:Label>
            </div>
        </td>
    </tr>
</table>
<radCln:RadCalendar ID="SC" runat="server" Skin="Web20">
</radCln:RadCalendar>
