<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuhinForm.aspx.cs" Inherits="m2mKoubai.Master.BuhinForm" %>

<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Register Src="~/CtlTabMain.ascx" TagName="CtlTabMain" TagPrefix="uc1" %>
<%@ Register Src="~/Common/CtlMyPager.ascx" TagName="CtlMyPager" TagPrefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>資材</title>
    <link href="../MainStyle.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript">
        function $(id)
        {
            return document.getElementById(id);
        }
        function Update(key)
        {               
            var win = window.open
            ("BuhinUpForm.aspx?key="+key,"_brank","width=600px,height=500px,location=no,resizable=yes,scrollbars=yes");            			        
	        win.focus();	
        }  
    	function Shinki()
		{			    			    	      		    
		    var win = window.open
            ("BuhinUpForm.aspx","_brank","width=600px,height=500px,location=no,resizable=yes,scrollbars=yes");            
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
	        AjaxRequest('Reload', '');
        }   
    	    
    	    
    
     </script> 
</head>
<body class="bg0">
    <form id="form1" runat="server">
       <uc1:CtlTabMain id="Tab" runat="server"></uc1:CtlTabMain>   
        <input id="BtnNew" runat="server" type="button" value="新規登録" class="mt5 bg6" /><br />
        <table border="1" bordercolor="#000000" class="def9 col mt5 tc bg1">
            <tr>
                <td class="bg3" >
                    品目</td>
                <td rowspan="2">
                    <input id="BtnK" runat="server" class="w60 bg6" type="button" value="検索" /></td>
            </tr>
            <tr>
                <td>
                   <asp:DropDownList ID="DdlHinmoku" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table id="TblList" runat="server" width="100%" class="def9">
            <tr>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Label ID="LblMsg" runat="server" CssClass="b"></asp:Label></td>
                            <td width="50%" class="hei20">
                                <img id="Img1" runat="server" src="../Img/Load.gif" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="def" width="100%">
                        <tr>
                            <td>
                               
                                <uc2:CtlMyPager ID="Pt" runat="server" />
                            </td>
                            <td align="right">
                                <table id="TblRow" runat="server" align="right" class="def9" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <input id="BtnS" runat="server" size="20" type="button" value="チェックしたデータを削除する" class="bg6" /></td>
                                        
                                        <td>
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
                                            </asp:DropDownList>行
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="G" runat="server" AutoGenerateColumns="False" OnRowDataBound="G_RowDataBound"
                        Width="100%" CssClass="def9">
                        <PagerSettings Visible="False" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Literal ID="LitS" runat="server" Text="削除"></asp:Literal><br />
                                    <input id="ChkH" runat="server" type="checkbox" />
                                </HeaderTemplate>
                                <ItemStyle CssClass="tc" />
                                <ItemTemplate>
                                    <input id="ChkI" runat="server" type="checkbox" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="更新">
                                <ItemStyle CssClass="tc" />
                                <ItemTemplate>
                                    <input id="BK" runat="server" type="button" value="更新" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="品目グループ">
                                <ItemStyle CssClass="tc" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="品目コード">
                                <ItemStyle CssClass="tc" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="品目名">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="単価">
                                <ItemStyle CssClass="tr" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="単位" >
                                <ItemStyle CssClass="tc" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="ロット">
                                <ItemStyle CssClass="tr" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="リードタイム" >
                                <ItemStyle CssClass="tc" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="仕入先1">
                                <HeaderTemplate>
                                    <table class="tc col" frame="void" width="100%">
                                        <tr>
                                            <td class="s1">
                                                仕入先1</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                仕入先2</td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table class="col" frame="void" width="100%">
                                        <tr>
                                            <td class="s2">
                                                <asp:Literal ID="LitShiire1" runat="server"></asp:Literal></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Literal ID="LitShiire2" runat="server"></asp:Literal></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <table class="tc col" frame="void" width="100%">
                                        <tr>
                                            <td class="s1">
                                                勘定科目コード</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                資産勘定科目名</td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table class="col" frame="void" width="100%">
                                        <tr>
                                            <td class="s2">
                                                <asp:Literal ID="LitKamokuCode" runat="server"></asp:Literal></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Literal ID="LitKamokuMei" runat="server"></asp:Literal></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <table  class="tc col" frame="void" width="100%">
                                        <tr>
                                            <td class="s1">
                                                費用勘定科目コード</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                勘定科目名</td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table class="col" frame="void" width="100%">
                                        <tr>
                                            <td class="s2">
                                                <asp:Literal ID="LitHiyouCode" runat="server"></asp:Literal></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Literal ID="LitHiyouMei" runat="server"></asp:Literal></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <table class="tc col" frame="void" width="100%">
                                        <tr>
                                            <td class="s1">
                                                補助勘定科目No.</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                補助勘定科目名</td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table class="col" frame="void" width="100%">
                                        <tr>
                                            <td class="s2">
                                                <asp:Literal ID="LitHojyoCode" runat="server"></asp:Literal></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Literal ID="LitHojyoMei" runat="server"></asp:Literal></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                     <HeaderStyle CssClass="bg3" />
                         <RowStyle CssClass="bg1" />
                         <AlternatingRowStyle CssClass="bg2" />
                    </asp:GridView>
                   
                    <uc2:CtlMyPager ID="Pb" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <input id="HidChkID" runat="server" type="hidden" />
                    <input id="HidThisID" runat="server" type="hidden" /></td>
            </tr>
        </table>
        <radA:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
        <ClientEvents OnRequestStart= "OnRequestStart" OnResponseEnd="OnResponseEnd" />
        </radA:RadAjaxManager>
         
    </form>
</body>
</html>
