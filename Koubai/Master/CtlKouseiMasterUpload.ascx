<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlKouseiMasterUpload.ascx.cs" Inherits="Koubai.Master.CtlKouseiMasterUpload" %>


<link href="~/MainStyle.css" type="text/css" rel="Stylesheet" />
<link href="~/Style/Grid.ykuri.css" rel="Stylesheet" />
<link href="~/Style/ComboBox.ykuri.css" type="text/css" rel="Stylesheet" />
<telerik:RadScriptBlock ID="RSM" runat="server">

    <script language="javascript" src="~/Core.js"></script>

    <style type="text/css">
        body {
            font-family: "Meiryo UI";
        }

        .fit {
            padding-right: 0px;
            padding-left: 0px;
            padding-bottom: 0px;
            margin: 0px;
            padding-top: 0px;
        }

        .fit2 {
            padding-bottom: 0px;
            padding-left: 0px;
            padding-right: 0px;
            margin: 0px;
            padding-top: 0px;
        }

        .os {
            background-color: #008080;
            color: White;
            width: 100%;
            height: 25px;
            font-weight: bold;
        }

        .pl {
            padding-left: 15px;
            height: 20px;
        }

        .d_h_c {
            background-color: #a3e0f5;
            color: Black;
            height: 20px;
        }

        .bar {
            color: #333333;
            background-color: #F2F4F6;
            border-top: #DFE3E8 1px solid;
            border-bottom: #DFE3E8 1px solid;
            border-left: #DFE3E8 1px solid;
            border-right: #DFE3E8 1px solid;
        }

        .oshirase {
            color: #333333;
            background-color: #F2F4F6;
            border-top: #DFE3E8 1px solid;
            border-bottom: #DFE3E8 1px solid;
            border-left: #DFE3E8 1px solid;
            border-right: #DFE3E8 1px solid;
        }

        .fw {
            font-weight: bold;
        }

        .ch {
            color: #FFFFFF;
            background-color: #0066cc;
        }

        .ch2 {
            color: #000000;
            background-color: #ddd9c3;
            text-align: center;
            width: 60px;
        }

        .th {
            background-color: #edffff;
            padding-left: 5px;
            padding-right: 5px;
        }

        .style1 {
            width: 490px;
        }

        .tbl {
            border: solid;
            border-width: 1px;
            border-spacing: 0px 0px;
            padding: 5px 5px 5px 5px;
            border-collapse: collapse;
        }

        .hdd {
            font-family: "Meiryo UI";
            font-size: 11pt;
            background-color: #fcd5b4;
        }

        .hdb {
            font-family: "Meiryo UI";
            font-size: 11pt;
            background-color: #00ffff;
        }
    </style>
    <script type="text/javascript">

    </script>
</telerik:RadScriptBlock>

<table>
  
    <tr>
        <td>
            <asp:Label ID="LblMsg" runat="server"></asp:Label>

        </td>
    </tr>

    <tr>
        <td>
            <div id="divS" runat="server">
                <table style="margin-bottom: 4px; border-collapse: collapse" bordercolor="#000" cellspacing="0" cellpadding="0" border="1">
                    <tr>
                        <td>
                            <table class="def" id="Table1" style="border-collapse: collapse"
                                bordercolor="#0" cellspacing="0" cellpadding="2" width="100%" border="1" frame="void"
                                runat="server" rules="all">
                                <tr>
                                    <td class="hdd" style="white-space: nowrap;">
                                        <asp:Label ID="Label1" runat="server" Text="取込データ"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap">
                                        <asp:FileUpload ID="FileA" runat="server" Width="490px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            ※部品表ｱｯﾌﾟﾛｰﾄﾞはCSV形式のﾌｧｲﾙで読込すること

        </td>
    </tr>
</table>
