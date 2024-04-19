<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShiiresakiForm.aspx.cs" Inherits="m2mKoubai.Master.ShiiresakiForm" %>

<%@ Register Src="../CtlTabMain.ascx" TagName="CtlTabMain" TagPrefix="uc1" %>

<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>

<%@ Register Src="../Common/CtlMyPager.ascx" TagName="CtlMyPager" TagPrefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>仕入先</title>
    <link href="../MainStyle.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
        
        
        function $(id)
        {
            return document.getElementById(id);
        }
        function Update(key)
        {               
            var win = window.open
            ("ShiiresakiUpForm.aspx?key="+key,"_brank","width=550px,height=500px,location=no,resizable=yes,scrollbars=yes");            			        
	        win.focus();	
        }  
    	function Shinki()
		{			    			    	      		    
		    var win = window.open
            ("ShiiresakiUpForm.aspx","_brank","width=550px,height=500px,location=no,resizable=yes,scrollbars=yes");            
		    win.focus();	
		}   			  
		function Delete()
        {          
            var chkIDAry = $('HidChkID').value.split(',');
            var thisIDAry = $('HidThisID').value.split(',');                            
            var hidDelKey = '';                 
            for (var i = 0; i < chkIDAry.length; i++)
            {                
                var chk = $(chkIDAry[i]);                
                if (chk.checked)
                {                    
                    if (hidDelKey != "") hidDelKey += ",";                 
                    hidDelKey += thisIDAry[i];                    
                }
            }            
            if (hidDelKey == "")
            {
                alert("チェックを入れて下さい");
                return false;
            } 
            if (confirm("削除しますか？"))
            {
                if (confirm("本当に削除しますか？"))
                {
                    AjaxRequest('delete', hidDelKey);                        
                }
            }                       
        }
        function DelChk(bool)
        {
            var idAry = $('HidChkID').value.split(',');
            for (var i = 0; i < idAry.length; i++)
            {
                var chk = $(idAry[i]);
                chk.checked = bool;
            }
        }
	    function OnRequestStart()
        {            
            $('Img1').style.display = '';
        }           
        function OnResponseEnd()
        {            
            $('Img1').style.display = 'none';
        }   
        function PageChange(pageIndex)
        {
            AjaxRequest('page', pageIndex);
        }    
        function AjaxRequest(command_name, arg)
	    {
		    <%=Ram.ClientID%>.AjaxRequest(command_name + ':' + arg);		
	    }     
	    function Kensaku()
        {       
	        AjaxRequest('kensaku', '');
        }  
        function Row()
        {
	        AjaxRequest('row', '');
        }
        function Reload()
        {
	        AjaxRequest('row', '');
	    }  
    	    
    	    
    
        
</script>

</head>
<body class="bg0">
    <form id="form1" runat="server">
        <uc1:CtlTabMain ID="Tab" runat="server" />
                    <input id="BtnNew" runat="server" type="button" value="新規登録" class="mt5 bg6" /><br />
       
        <table border="1" bordercolor="#000000" class="def9 bg1 col mt5 tc">
            <tr>
                <td class="bg3">
                    仕入先</td>
                <td rowspan="2">
                    <input id="BtnKensaku" runat="server" class="w60 bg6" type="button" value="検索" /></td>
            </tr>
            <tr>
                <td align="left">
                    <asp:DropDownList ID="DdlCode" runat="server">
                    </asp:DropDownList></td>
            </tr>
        </table>
      
        <table id="T_Main" runat="server" style="width: 100%" class="def9">
            <tr>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
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
        <table class="def10" width="100%" id="T_Gv" runat="server">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td width="50%">
                                <uc2:CtlMyPager ID="Pt" runat="server" />
                            </td>
                            <td class="tr">
                                <table class="def9">
                                    <tr>
                                        <td>
                    <input id="BtnS" runat="server" size="20" type="button" value="チェックしたデータを削除する" class="bg6" /></td>
                                        <td style="height: 26px">
                                            &nbsp;<asp:DropDownList ID="DdlRow" runat="server">
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
                                            </asp:DropDownList>行
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="G" runat="server" AutoGenerateColumns="False" Width="100%" 
                         OnRowDataBound="G_RowDataBound" CssClass="def9">
                        <PagerSettings Visible="False" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    削除<br />
                                    <input id="ChkH" runat="server" type="checkbox" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input id="ChkI" runat="server" type="checkbox" />
                                </ItemTemplate>
                                <ItemStyle CssClass="tc" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    更新
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input id="BtnK" runat="server" type="button" value="更新" language="javascript" />
                                </ItemTemplate>
                                <ItemStyle CssClass="tc" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="仕入先コード" />
                            <asp:BoundField HeaderText="仕入先名" />
                            <asp:BoundField HeaderText="郵便番号" />
                            <asp:BoundField HeaderText="住所" />
                            <asp:BoundField HeaderText="TEL" />
                            <asp:BoundField HeaderText="FAX" />
                            <asp:BoundField HeaderText="口座名義" />
                            <asp:BoundField HeaderText="金融機関名" />
                            <asp:BoundField HeaderText="口座番号" />
                            <asp:BoundField HeaderText="支払締日" />
                            <asp:BoundField HeaderText="支払予定日" />
                            <asp:BoundField HeaderText="検収情報公開" >
                                <ItemStyle CssClass="tc" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="納期回答催促メール" >
                                <ItemStyle CssClass="tc" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="仕入先情報更新許可" >
                                <ItemStyle CssClass="tc" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="bg3" />
                         <RowStyle CssClass="bg1" />
                         <AlternatingRowStyle CssClass="bg2" />
                    </asp:GridView>
                    <uc2:CtlMyPager ID="Pb" runat="server" />
                </td>
            </tr>
            <tr>
            </tr>
        </table><input id="HidChkID" runat="server" type="hidden" />
                    <input id="HidThisID" runat="server" type="hidden" />
                </td>
            </tr>
        </table>
        <rada:radajaxmanager id="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
            <ClientEvents OnRequestStart="OnRequestStart" OnResponseEnd="OnResponseEnd" />
        </rada:radajaxmanager>
    </form>
</body>
</html>