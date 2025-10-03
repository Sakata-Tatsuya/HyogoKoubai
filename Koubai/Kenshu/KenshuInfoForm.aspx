<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KenshuInfoForm.aspx.cs" Inherits="Koubai.Kenshu.KenshuInfoForm" %>
<%@ Register Src="~/CtlTabMain.ascx" TagName="CtlTabMain" TagPrefix="uc1" %>
<%@ Register Src="~/Common/CtlNengappiFromTo.ascx" TagName="CtlNengappiFromTo" TagPrefix="uc2" %>
<%@ Register Src="~/Common/CtlMyPager.ascx" TagName="CtlMyPager" TagPrefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>検収情報</title>
    <link href="../MainStyle.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript">
    function $(id)
    {
        return document.getElementById(id);
    }
        function AjaxRequest(command_name, arg) {
           <%= Ram.ClientID %>.ajaxRequest(command_name + ':' + arg);
        }
	 function Reload()
    {        
        AjaxRequest('reload', '');
    }
    function Kensaku()
    {
        if(!Check())
            return;
	    AjaxRequest('kensaku', '');
    }
    function RowChange()
    {
	    AjaxRequest('row', '');
    }
    function PageChange(pageIndex)
    {
	    AjaxRequest('page', pageIndex);
    }
    function OnBuhin()
    {
        AjaxRequest('ddlBuhin', '');
    }
	function HacchuuNo(key, hacchuuNo)
    {
        document.getElementById('HidKey').value = key +'\t'+ '';
        NewForm.action = "../Order/OrderShousaiForm";
        NewForm.target = "_hacchuu";
	    OpenWinPost2("_hacchuu",500,500);
        NewForm.submit();
    }
    function Print()
    {
        var chkIdAry = document.getElementById('HidChkID').value.split(',');
        var hidPrintKey = ''
        for(var i = 0; i < chkIdAry.length; i++)
        {
            var chk = document.getElementById(chkIdAry[i]);
           
            if(chk.checked)
            {
                if(hidPrintKey != "") hidPrintKey += "_";
                hidPrintKey += chk.value;
            }
        }
        if(hidPrintKey == "")
        {
            alert("チェックを入れてください");
            return false;
        }   
    
        document.getElementById('HidKey').value = hidPrintKey;
        NewForm.action = "../Denpyou/JyuryousyoForm";
        NewForm.target = "_hacchuusho";
	    OpenWinPost2("_hacchuusho",800,600);
        NewForm.submit();
    }
  
    var win = null;
    function OpenWinPost(target,w,h)
    {
        win = window.open
            ("",target,"width="+w+"px,height="+h+"px,location=no,resizable=yes,scrollbars=yes");            
	    win.focus();
    }
     
    function OpenWinPost2(target,w,h,etc)
    {
        win = window.open
            ("",target,"width="+w+"px,height="+h+"px,menubar=yes,location=no,resizable=yes,scrollbars=yes");            
	    win.focus();
    }
	 function ChkAll(bool)
    {
        var idAry = document.getElementById('HidChkID').value.split(',');
      
        for(var i = 0; i < idAry.length; i++)
        {
            var chk = document.getElementById(idAry[i]);
            chk.checked = bool;
        }
    }
	
    function OnRequestStart(sender, args)
	{
        document.getElementById("Img1").style.display = '';		
	}
	function OnResponseEnd(sender, args)
    {
        document.getElementById('Img1').style.display = 'none';
    }
   
    function KeyCodeCheck()
    {
        var kc = event.keyCode;
        if((kc >= 37 && kc <= 40) || (kc >= 48 && kc <= 57) || (kc >= 96 && kc <= 105) || 
        kc == 46 || kc == 8 || kc == 13 || kc == 9)
            return true; 
        else
            return false;
    }
    
    function Check()
    {
        var TbxKanjyouKamokuCode = document.getElementById('TbxKanjyouKamokuCode');
        if (!CheckNumber(TbxKanjyouKamokuCode,'勘定科目コード'))
        {
            return false;
        }
        var TbxHiyouKamokuCode = document.getElementById('TbxHiyouKamokuCode');
        if (!CheckNumber(TbxHiyouKamokuCode,'費用科目コード'))
        {
            return false;
        }
        var TbxHojyoKamokuNo = document.getElementById('TbxHojyoKamokuNo');
        if (!CheckNumber(TbxHojyoKamokuNo,'補助科目No'))
        {
            return false;
        }
        
        return true;
    }

         function CheckNumber(tbx, str)
    { 
        if (tbx.value.match(/[^0-9]/) != null)
        {
            alert(str+"は半角数字のみで入力して下さい");
            tbx.focus();
            return false;
        }
        return true;
    }
   
     </script>
   
</head>
<body class="bg0">
    <form id="form1" runat="server" >
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js">
                </asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js">
                </asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js">
                </asp:ScriptReference>
            </Scripts>
        </telerik:RadScriptManager>
    <div>
        <uc1:CtlTabMain ID="Tab" runat="server" />
      </div>
        <table class="tc def9 col bg1 mt5" border="1" bordercolor="#000000">
            <tr  class="bg3">
                <td >
                    発注No</td>
                <td >
                    品目グループ</td>
                <td >
                    品目名</td>
                <td >
                    仕入先</td>
                <td >
                    受入日</td>
                <td>
                    勘定科目コード</td>
                <td>
                    費用科目コード</td>
                <td>
                    補助科目No</td>
                <td rowspan="2" class="bg1">
                    <input id="BtnK" runat="server" class="w60 bg6" type="button" value="検索" /></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TbxHacchuNo" runat="server" Width="100px" MaxLength="7" ></asp:TextBox></td>
                <td>
                    <asp:DropDownList ID="DdlBuhinKubun" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DdlBuhin" runat="server">
                    </asp:DropDownList></td>
                <td>
                    <asp:DropDownList ID="DdlShiire" runat="server">
                    </asp:DropDownList></td>
                <td>
                    <uc2:CtlNengappiFromTo ID="CtlUkeireBi" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="TbxKanjyouKamokuCode" runat="server" Width="40px" MaxLength="3" ></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TbxHiyouKamokuCode" runat="server" Width="40px" MaxLength="3" ></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TbxHojyoKamokuNo" runat="server" Width="40px" MaxLength="1" ></asp:TextBox>
                </td>
                
            </tr>
        </table>
        <table id="TblList" runat="server" width="100%" class="def9">
            <tr>
                <td>
                    <table id="TABLE1" runat="server" width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="50%">
                                <asp:Label ID="LblMsg" runat="server" CssClass="b"></asp:Label></td>
                            <td class="hei20">
                                <img id="Img1" runat="server" src="../Img/Load.gif" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="nw" valign="bottom">
                                <uc3:CtlMyPager ID="Pt" runat="server" />
                            </td>
                            <td align="right" valign="bottom">
                                <table id="TblRow" runat="server" cellpadding="0" cellspacing="0" class="def9">
                                    <tr>
                                        <td >
                                            <asp:DropDownList ID="DdlRow" runat="server">
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem Selected="True">30</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                                <asp:ListItem>--</asp:ListItem>
                                            </asp:DropDownList>行</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="G" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="def9" OnRowDataBound="G_RowDataBound">
                        <PagerSettings Visible="False" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <table border="1" bordercolor="#ffffff" class="tc col" frame="void" width="100%">
                                        <tr>
                                            <td>
                                                発注No</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                発注日</td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table border="1" bordercolor="silver" class="col" frame="void" width="100%">
                                        <tr>
                                            <td>
                                                <asp:Literal ID="LitHacchuuNo" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Literal ID="LitHacchuuBi" runat="server"></asp:Literal></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="仕入先コード">
                                 <ItemStyle CssClass="tc" />
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="仕入先名"></asp:TemplateField>
                            <asp:BoundField HeaderText="品目グループ">
                                <ItemStyle CssClass="tc" />
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="品目コード">
                                 <ItemStyle CssClass="tc" />
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="品目名">
                             </asp:TemplateField>
                            <asp:TemplateField HeaderText="注文数量">
                                <ItemStyle CssClass="tr" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="単位" >
                                <ItemStyle CssClass="tc" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="単価">
                                <ItemStyle CssClass="tr" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="注文金額">
                                <ItemStyle CssClass="tr" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="納入場所"></asp:TemplateField>
                            <asp:TemplateField HeaderText="受入日">
                                <ItemStyle CssClass="tc" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="入荷数量">
                                <ItemStyle CssClass="tr" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="bg3" />
                         <RowStyle CssClass="bg1" />
                         <AlternatingRowStyle CssClass="bg2" />
                    </asp:GridView>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <uc3:CtlMyPager ID="Pb" runat="server" />
                  </td>
            </tr>
            <tr>
                <td>
                     <input id="HidChkID" runat="server" type="hidden" /></td>
            </tr>
        </table>
        
        <telerik:RadCalendar ID="SC" runat="server" Skin="Web20">
        </telerik:RadCalendar>
        <telerik:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
         <ClientEvents OnRequestStart="OnRequestStart" OnResponseEnd="OnResponseEnd" />
        </telerik:RadAjaxManager>
    </form>
     <form id="NewForm" method="post" name="NewForm" >
            <input id="HidKey" runat="server" type="hidden" /></form>

</body>
</html>
