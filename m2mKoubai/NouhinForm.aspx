<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NouhinForm.aspx.cs" Inherits="m2mKoubai.NouhinForm" %>

<%@ Register Src="CtlTabMain.ascx" TagName="CtlTabMain" TagPrefix="uc1" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>納品</title>
    <link href="MainStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    function $(id)
    {
        return document.getElementById(id);
    }    
    function Load()
    {
       var tbx = $("TbxHacchuNo");
        if(tbx != null)
        {
            tbx.focus();
        }
    }
    function AjaxRequest(command_name, arg)
	{
		<%=Ram.ClientID%>.AjaxRequest(command_name + ':' + arg);
	}
    function Check()
    {
        var tbx = $('TbxHacchuNo');
        if (tbx == "")
        {
            alert("発注Noを入力してください");
            tbx.focus();
            return;
        }
        if(!CheckSuu(tbx, '発注No'))
        {
            return;
        }
        
        AjaxRequest('Check', '');
    }
    
    function Clear(tbl)
    {
        var tbx = $("TbxHacchuNo");
        tbx.value = "";
        $(tbl).style.display = "none";
        tbx.focus();
        //AjaxRequest('Clear', '');           
        
    }
    
    function Nouhin()
    {
        var tbx = $("TbxNouhinsuu");
        if (tbx.value == "")
        {
            alert("数量を入力して下さい");
            tbx.focus();
            return;
        }
        if(!CheckSuu(tbx,"納品数量"))
        {
            return;
        }
        if(!confirm("入力した数量で納品を確定しますか？"))
            return false;
            
        AjaxRequest('Nouhin', '');
        
    }
    function Kannou()
    {   
        var tbx = $("TbxNouhinsuu");
        if (tbx.value == "")
        {
            alert("数量を入力して下さい");
            tbx.focus();
            return;
        }
        if(!CheckSuu(tbx,"納品数量"))
        {
            return;
        }

        if(!confirm("入力した数量で納品を納品を確定し、注文を完納にしますか?"))
            return false;    
        AjaxRequest('Kannou', '');
    }
    function OnRequestStart(sender, args)
	{
        $("Img1").style.display = '';
	}
	function OnResponseEnd(sender, args)
    {
        $('Img1').style.display = 'none';
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
    function SuuryouChk(nSuuryou1, nSuuryou2, nSuuryou3)
    {
        var suu1 = parseInt(nSuuryou1,10);
        var suu2 = parseInt(nSuuryou2,10);
        var tbx = $(nSuuryou3);
        var suu3 = parseInt(tbx.value, 10);
        if(suu1 - suu2 < suu3)
        {
            alert("納品数量が納入残数を越えています");
            tbx.focus();
            return;
        }
       
    }
    function KenChk()
    {
        if (event.keyCode == 13)
        {
            if(!Check())
            {
                return;
            }
        
           // AjaxRequest('Check', '');  
        }          
    }   
     // 数値チェック
        function CheckSuu(tbx,str)
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
<body onload="Load();" class="bg0">
    <form id="form1" runat="server">
    <div>
        <uc1:CtlTabMain ID="Tab" runat="server" />
        <table class="col def9 mt5 bg1" border="1" bordercolor="#000000">
            <tr>
                <td class="bg3 tc">
                    発注年
                </td>
                <td class="bg3 tc">
                    発注No
                </td>
                <td rowspan="2">
                    <input id="BtnCK" runat="server" type="button" value="注文内容確認" class="w100 bg6" />
                    <input id="BtnC" runat="server" type="button" value="クリア" class="w80 bg6" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="DdlYear" runat="server">
                    </asp:DropDownList>
                    年
                </td>
                <td>
                    <input id="TbxHacchuNo" runat="server" type="text" maxlength="7" class="w100 tr" />&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <table id="TblList" runat="server" width="100%">
        <tr>
            <td>
                <table width="100%" id="TABLE1" runat="server" class="def9" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%">
                            <asp:Label ID="LblMsg" runat="server" CssClass="b"></asp:Label>
                        </td>
                        <td class="hei20">
                            <img id="Img1" runat="server" src="Img/Load.gif" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tc">
                <table id="TblMain" runat="server" enableviewstate="true" width="100%">
                    <tr>
                        <td>
                            <table border="1" bordercolor="#000000" class="col def11 tc" width="100%">
                                <tr>
                                    <td class="bg3">
                                        発注No
                                    </td>
                                    <td class="bg3 tc">
                                        仕入先コード
                                    </td>
                                    <td class="bg3 tc">
                                        仕入先名
                                    </td>
                                    <td class="bg3 tc">
                                        納期
                                    </td>
                                    <td class="bg3 tc">
                                        回答納期
                                    </td>
                                    <td class="bg3 tc">
                                        納入場所
                                    </td>
                                </tr>
                                <tr>
                                    <td class="bg1">
                                        <asp:Label ID="LblHacchuuNo" runat="server"></asp:Label>
                                    </td>
                                    <td class="bg1">
                                        <asp:Label ID="LblShiireCode" runat="server"></asp:Label>
                                    </td>
                                    <td class="bg1 col ">
                                        <asp:Label ID="LblShiireMei" runat="server"></asp:Label>
                                    </td>
                                    <td class="bg1 tc">
                                        <asp:Label ID="LblNouki" runat="server"></asp:Label>
                                    </td>
                                    <td class="bg1 tc">
                                        <asp:Label ID="LblKaitouNouki" runat="server"></asp:Label>
                                    </td>
                                    <td class="bg1">
                                        <asp:Label ID="LblBasho" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table border="1" bordercolor="#000000" class="col def11 tc" width="100%">
                                <tr>
                                    <td class="bg3 tc">
                                        品目グループ
                                    </td>
                                    <td class="bg3 tc">
                                        品目コード
                                    </td>
                                    <td class="bg3 tc">
                                        品名
                                    </td>
                                    <td class="bg3 tc">
                                        単価
                                    </td>
                                    <td class="bg3 tc">
                                        注文金額
                                    </td>
                                    <td class="bg3 tc">
                                        税額
                                    </td>
                                    <td class="bg3 tc">
                                        単位
                                    </td>
                                </tr>
                                <tr>
                                    <td class="bg1 tc">
                                        <asp:Label ID="LblBuhinGroup" runat="server"></asp:Label>
                                    </td>
                                    <td class="bg1 tc">
                                        <asp:Label ID="LblBuhinCode" runat="server"></asp:Label>
                                    </td>
                                    <td class="bg1 tc">
                                        <asp:Label ID="LblBuhinMei" runat="server"></asp:Label>
                                    </td>
                                    <td class="bg1 tr">
                                        <asp:Label ID="LblTanka" runat="server"></asp:Label>
                                    </td>
                                    <td class="bg1 tr" nowrap="noWrap">
                                        <asp:Label ID="LblChumonKingaku" runat="server"></asp:Label>
                                    </td>
                                    <td class="bg1 tr" nowrap="noWrap">
                                        <asp:Label ID="LblZeigaku" runat="server"></asp:Label>
                                    </td>
                                    <td class="bg1 tc">
                                        <asp:Label ID="LblTani" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <%--                                <table  border="1" bordercolor="#000000" class="col def11 tc" width="100%">
                                    <tr>
                                        <td class="bg3">
                                            発注No</td>
                                        <td class="bg3 tc">
                                            仕入先コード</td>
                                        <td class="bg3 tc">
                                            仕入先名</td>
                                        <td class="bg3 tc">
                                            納期</td>
                                        <td class="bg3 tc">
                                            回答納期</td>
                                        <td class="bg3 tc">
                                            納入場所</td>
                                    </tr>
                                    <tr>
                                        <td class="bg1">
                                            <asp:Label ID="LblHacchuuNo" runat="server"></asp:Label></td>
                                        <td class="bg1">
                                            <asp:Label ID="LblShiireCode" runat="server"></asp:Label></td>
                                        <td class="bg1 col ">
                                            <asp:Label ID="LblShiireMei" runat="server"></asp:Label></td>
                                        <td class="bg1 tc">
                                            <asp:Label ID="LblNouki" runat="server"></asp:Label></td>
                                        <td class="bg1 tc">
                                            <asp:Label ID="LblKaitouNouki" runat="server"></asp:Label></td>
                                        <td class="bg1">
                                            <asp:Label ID="LblBasho" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="bg3 tc">
                                            品目グループ</td>
                                        <td class="bg3 tc">
                                            品目コード</td>
                                        <td class="bg3 tc">
                                            品名</td>
                                        <td class="bg3 tc">
                                            単価</td>
                                        <td class="bg3 tc">
                                            注文金額</td>
                                        <td class="bg3 tc">
                                            単位</td>
                                        
                                    </tr>
                                    <tr>
                                        <td class="bg1 tc">
                                            <asp:Label ID="LblBuhinGroup" runat="server"></asp:Label></td>
                                        <td class="bg1 tc">
                                            <asp:Label ID="LblBuhinCode" runat="server"></asp:Label></td>
                                        <td class="bg1 tc">
                                            <asp:Label ID="LblBuhinMei" runat="server"></asp:Label></td>
                                        <td class="bg1 tr">
                                            <asp:Label ID="LblTanka" runat="server"></asp:Label></td>
                                        <td class="bg1 tr" nowrap="noWrap">
                                            <asp:Label ID="LblChumonKingaku" runat="server"></asp:Label></td>
                                        <td class="bg1 tc">
                                            <asp:Label ID="LblTani" runat="server"></asp:Label></td>
                                    </tr>
                                </table>--%>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tr">
                <table border="1" bordercolor="#000000" class="col def11" width="420px">
                    <tr>
                        <%--2013/05/13 納品日追加--%>
                        <td class="bg3 tc" width="50px">
                            税率
                        </td>
                        <td class="bg3 tc" width="100px">
                            納期
                        </td>
                        <td class="bg3 tc" width="80px">
                            発注数量
                        </td>
                        <td class="bg3 tc" width="100px">
                            納品済数量
                        </td>
                        <td class="bg3 tc" width="100px">
                            納品数量
                        </td>
                    </tr>
                    <tr>
                        <td class="bg1 nw">
                            <table>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="DdlTax" runat="server" AutoPostBack="True" onselectedindexchanged="DdlTax_SelectedIndexChanged">
                                            <asp:ListItem Value="10">10%</asp:ListItem>
                                            <asp:ListItem Value="8">8%</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="bg1 nw">
                            <radCln:RadDatePicker ID="RdpDay" runat="server">
                                <DateInput Width="80px" Font-Size="10pt">
                                </DateInput>
                                <Calendar ID="Cal1" runat="server">
                                </Calendar>
                            </radCln:RadDatePicker>
                        </td>
                        <td class="bg1 tr">
                            <asp:Label ID="LblSuuryou" runat="server" Height="19px"></asp:Label>
                        </td>
                        <td class="bg1 tr">
                            <asp:Label ID="LblNouhinSumiSuu" runat="server"></asp:Label>
                        </td>
                        <td class="bg1 tc">
                            <asp:TextBox ID="TbxNouhinsuu" runat="server" CssClass="tr" Width="80px"></asp:TextBox>
                            <asp:Literal ID="LitMsg" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tr">
                <input id="BtnKN" runat="server" type="button" value="注文を完納にする" class="bg6" />
                <input id="BtnNK" runat="server" type="button" value="納品確定" class="bg6" />
            </td>
        </tr>
    </table>
    </td> </tr> </table>
    <radA:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
        <ClientEvents OnResponseEnd="OnResponseEnd" OnRequestStart="OnRequestStart" />
    </radA:RadAjaxManager>
    <asp:TextBox ID="TextBox1" runat="server" CssClass="none" />
    </form>
</body>
</html>
