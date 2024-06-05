<%@ Page Language="C#" AutoEventWireup="true" Codebehind="UploadForm.aspx.cs" Inherits="m2mKoubai.Upload.UploadForm" %>

<%--<%@ Register Src="../CtlTabMain.ascx" TagName="CtlTabMain" TagPrefix="uc3" %>--%>
<%@ Register TagName="CtlMainMenu" Src="~/CtlMainMenu.ascx" TagPrefix="uc3" %>
<%@ Register Src="CtlOrderUpload.ascx" TagName="CtlOrderUpload" TagPrefix="uc2" %>
<%@ Register Src="CtlHinmokuUpload.ascx" TagName="CtlHinmokuUpload" TagPrefix="uc1" %>
<%@ Register Assembly="RadTabStrip.Net2" Namespace="Telerik.WebControls" TagPrefix="radTS" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>アップロード</title>
        <link href="../MainStyle.css" rel="stylesheet" type="text/css" />    
</head>
<body class="bg0">
    <form id="form1" runat="server">
<%--        <uc3:CtlTabMain ID="CtlTabMain1" runat="server" />--%>
        <uc3:CtlMainMenu ID="M" runat="server"></uc3:CtlMainMenu>
        <radTS:RadTabStrip ID="TabUpload" runat="server" AutoPostBack="True" OnTabClick="TabUpload_TabClick" Skin="Office2007">
            <Tabs>
                <radTS:Tab runat="server" Text="品目データ">
                </radTS:Tab>
                <radTS:Tab runat="server" Text="発注データ">
                </radTS:Tab>
            </Tabs>
        </radTS:RadTabStrip>
        <div id="DivHinmokuUpload" runat="server">
            <uc1:CtlHinmokuUpload id="CtlHinmokuUpload1" runat="server">
            </uc1:CtlHinmokuUpload>
        </div>
        <div id="DivOrderUpload" runat="server">
            <uc2:CtlOrderUpload ID="CtlOrderUpload1" runat="server" />
        </div>
    </form>
</body>
</html>
