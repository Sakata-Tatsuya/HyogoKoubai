<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DownloadForm.aspx.cs" Inherits="Koubai.Download.DownloadForm" %>

<%@ Register Assembly="RadTabStrip.Net2" Namespace="Telerik.WebControls" TagPrefix="radTS" %>
<%@ Register Src="CtlNonyuZanDownload.ascx" TagName="CtlNonyuZanDownload" TagPrefix="uc2" %>
<%@ Register Src="CtlKenshuDownload.ascx" TagName="CtlKenshuDownload" TagPrefix="uc1" %>
<%@ Register TagName="CtlMainMenu" Src="~/CtlMainMenu.ascx" TagPrefix="uc3" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ダウンロード</title>
    <link href="../MainStyle.css" rel="stylesheet" type="text/css" />
</head>
<body class="bg0">
    <form id="form1" runat="server">
<%--        <uc3:CtlTabMain ID="CtlTabMain1" runat="server" />--%>
        <uc3:CtlMainMenu ID="M" runat="server"></uc3:CtlMainMenu>
        <radTS:RadTabStrip ID="TabUpload" runat="server" AutoPostBack="True" OnTabClick="TabUpload_TabClick" Skin="Office2007">
            <Tabs>
                <radTS:Tab ID="Tab1" runat="server" Text="納入残情報">
                </radTS:Tab>
                <radTS:Tab ID="Tab2" runat="server" Text="検収情報">
                </radTS:Tab>
            </Tabs>
        </radTS:RadTabStrip>
        <div id="DivNonyuZan" runat="server">
            <uc2:CtlNonyuZanDownload ID="CtlNonyuZanDownload1" runat="server" />
        </div>
        <div id="DivKenshu" runat="server">
            <uc1:CtlKenshuDownload ID="CtlKenshuDownload1" runat="server" />
        </div>
    </form>
</body>
</html>
