<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlTabMain.ascx.cs" Inherits="m2mKoubai.CtlTabMain" %>
<%@ Register Assembly="RadTabStrip.Net2" Namespace="Telerik.WebControls" TagPrefix="radTS" %>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td valign="top">
<radts:radtabstrip id="Tab" RadControlsDir="~/MyRadControls/" Skin="ClassicBlue" runat="server" SkinsPath="~/MyRadControls/TabStrip/Skins">
<Tabs>
<radTS:Tab ID="Tab1" runat="server" Text="�������" Font-Bold="True" NavigateUrl="~/Order/OrderInfoForm.aspx"></radTS:Tab>
<radTS:Tab ID="Tab2" runat="server" NavigateUrl="~/Order/OrderInputForm.aspx" Text="��������" Font-Bold="True">
    <Tabs>
        <radTS:Tab ID="Tab3" runat="server" NavigateUrl="~/Order/OrderInputForm.aspx" Text="�ʔ���">
        </radTS:Tab>
        <radTS:Tab ID="Tab4" runat="server" NavigateUrl="~/Order/MultiOrderInputForm.aspx" Text="��������">
        </radTS:Tab>
    </Tabs>
</radTS:Tab>
<radTS:Tab ID="Tab5" runat="server" Text="�������" Font-Bold="True" NavigateUrl="~/Kenshu/KenshuInfoForm.aspx">
    <Tabs>
        <radTS:Tab ID="Tab6" runat="server" NavigateUrl="~/Kenshu/KenshuInfoForm.aspx" Text="�ꗗ">
        </radTS:Tab>
        <radTS:Tab ID="Tab7" runat="server" NavigateUrl="~/Kenshu/KenshuInfoShukeiForm.aspx" Text="�W�v">
        </radTS:Tab>
    </Tabs>
</radTS:Tab>
<radTS:Tab ID="Tab8" runat="server" Text="�[�i" Font-Bold="True" NavigateUrl="~/NouhinForm.aspx"></radTS:Tab>
<radTS:Tab ID="Tab9" runat="server" Text="�p�X���[�h�ύX" NavigateUrl="~/PassChangeForm.aspx" Font-Bold="True"></radTS:Tab>
<radTS:Tab ID="Tab19" runat="server" Text="�A�b�v���[�h" NavigateUrl="~/Upload/UploadForm.aspx" Font-Bold="True"></radTS:Tab>
<radTS:Tab ID="Tab20" runat="server" Text="�_�E�����[�h" NavigateUrl="~/Download/DownloadForm.aspx" Font-Bold="True"></radTS:Tab>
<radTS:Tab ID="Tab10" runat="server" Text="�}�X�^�Ǘ�" NavigateUrl="~/Master/ShiiresakiForm.aspx" Font-Bold="True">
    <Tabs>
        <radTS:Tab ID="Tab11" runat="server" Text="�d����" NavigateUrl="~/Master/ShiiresakiForm.aspx" Font-Bold="True"></radTS:Tab>
        <radTS:Tab ID="Tab12" runat="server" Text="�S����" NavigateUrl="~/Master/TantoushaAccountForm.aspx" Font-Bold="True">
            <Tabs>
                <radTS:Tab ID="Tab13" runat="server" NavigateUrl="~/Master/TantoushaAccountForm.aspx" Text="�Г�" Font-Bold="True">
                </radTS:Tab>
                <radTS:Tab ID="Tab14" runat="server" Text="�d����" NavigateUrl="~/Master/ShiiresakiAccountForm.aspx" Font-Bold="True">
                </radTS:Tab>
            </Tabs>
        </radTS:Tab>
        <radTS:Tab ID="Tab15" runat="server" Text="����" NavigateUrl="~/Master/BuhinForm.aspx" Font-Bold="True"></radTS:Tab>
        <radTS:Tab ID="Tab16" runat="server" Text="���b�Z�[�W�o�^"  NavigateUrl="~/Master/LoginMsgForm.aspx" Font-Bold="True"></radTS:Tab>
        <radTS:Tab ID="Tab17" runat="server" NavigateUrl="~/Master/NounyuBashoForm.aspx" Text="�[���ꏊ" Font-Bold="True">
        </radTS:Tab>
        <radTS:Tab ID="Tab18" runat="server" Text="���Ə����" NavigateUrl="~/Master/KaishaInfoForm.aspx" Font-Bold="true">
        </radTS:Tab>
    </Tabs>
</radTS:Tab>
</Tabs>
</radts:radtabstrip>
</td>
        <td valign="top">      
            <radts:radtabstrip id="Radtabstrip1" runat="server"  RadControlsDir="~/MyRadControls/" Skin="ClassicBlue" SkinsPath="~/MyRadControls/TabStrip/Skins">
                <Tabs>           
                    <radTS:Tab ID="TabLO" SelectedCssClass=""  runat="server" Text="���O�A�E�g" Font-Bold="True"></radTS:Tab>
                </Tabs>
            </radts:radtabstrip>
        </td>
    </tr>
</table>