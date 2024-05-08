<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderInputForm.aspx.cs" Inherits="m2mKoubai.Order.OrderInputForm" %>

<%@ Register Assembly="RadInput.Net2" Namespace="Telerik.WebControls" TagPrefix="radI" %>
<%--<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>--%>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Src="~/CtlTabMain.ascx" TagName="CtlTabMain" TagPrefix="uc1" %>
<%@ Register Src="~/Common/CtlMyPager.ascx" TagName="CtlMyPager" TagPrefix="uc2" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>発注入力</title>
    <link href="../MainStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function $(id)
        {
            return document.getElementById(id);
        }
        function OnRequestStart() { document.getElementById('Img1').style.display = ''; }
        function OnResponseEnd() { document.getElementById('Img1').style.display = 'none'; }
        function AjaxRequest(command_name, arg)
        {
		    <%=Ram.ClientID %>.AjaxRequest(command_name + ':' + arg);
        }
        function RowClear()
        {         
            var chkIDAry = document.getElementById('HidChkID').value.split(',');
            var bChecked = false;
            var nDelCnt = 0;
            for (var i = 0; i < chkIDAry.length; i++)
            {
                var chk = document.getElementById(chkIDAry[i]);
                if (chk.checked)
                {
                    bChecked = true;
                    //break;
                    nDelCnt++;
                }
            }
            if (nDelCnt == 0)
            {
                alert('チェックを入れて下さい');
                return;
            }
            document.getElementById('HidArgs').value = nDelCnt;
            var meisai = CreateMeisai(true, false);
            if (confirm("入力内容を削除しますか？"))
            {
                for (var i = 0; i < chkIDAry.length; i++)
                {
                    var chk = document.getElementById(chkIDAry[i]);
                    if (chk.checked)
                    {
                        chk.checked = false;
                    }
                }
                AjaxRequest('RowClear', meisai);
            }
        }
        function AllClear()
        {
            if (confirm("入力した内容を全て削除しますか？"))
            {
                AjaxRequest('AllClear', '');
            }
        }
        function CreateMeisai(bSakujo, bTouroku)
        {
            var meisai = '';
            //var bErr = false;
            var noukiAry = document.getElementById('HidNoukiID').value.split(',');
            
            var grid = document.getElementById('G');
            for (var i = 1; i < grid.rows.length; i++)
            {       
                var chkKan = grid.rows.item(i).cells.item(0).firstChild;  
                if (bSakujo && chkKan.checked)
                {
                    continue;
                }
                
                var tbl3 = grid.rows.item(i).cells.item(1).firstChild;
	            var ddlShiire = tbl3.rows.item(1).cells.item(0).firstChild;
                if (ddlShiire.length == 0 || ddlShiire.selectedIndex == 0)
                {
                    continue;
                }
                var shiireCode = ddlShiire.options[ddlShiire.selectedIndex].value;
                var tbl0 = grid.rows.item(i).cells.item(2).firstChild;
	            var ddlKubun = tbl0.rows.item(0).cells.item(0).firstChild;
	            var buhinKubun = '';
                if (ddlKubun.length > 0 && ddlKubun.selectedIndex > 0)
                {
                    buhinKubun = ddlKubun.options[ddlKubun.selectedIndex].value;
                }
	            var ddlBuhin = tbl0.rows.item(1).cells.item(0).firstChild;
                var buhinCode = '';
                if (ddlBuhin.length > 0 && ddlBuhin.selectedIndex > 0)
                {
                    buhinCode = ddlBuhin.options[ddlBuhin.selectedIndex].value;
                }
                
                var tbl1 = grid.rows.item(i).cells.item(3).firstChild;
	            var lblLot = tbl1.rows.item(0).cells.item(0).firstChild;
	            var lot = lblLot.innerText;
	            
	            var chkKariTanka = tbl1.rows.item(1).cells.item(0).childNodes.item(1);
                var kariTankaFlg;
                if (chkKariTanka.checked) { kariTankaFlg = "0"; }
                else { kariTankaFlg = "1"; }
                
	            var tbxTanka = tbl1.rows.item(1).cells.item(0).childNodes.item(3);
	            var tanka = tbxTanka.value;

                var tbxSuu = grid.rows.item(i).cells.item(4).firstChild;
                var suu = tbxSuu.value;
                
                var lblTani = grid.rows.item(i).cells.item(5).firstChild;
                var tani = lblTani.innerText;
                
                var lblLT = grid.rows.item(i).cells.item(6).firstChild;
                var leadTime = lblLT.innerText;
                
                var tbl2 = grid.rows.item(i).cells.item(7).firstChild;
	            var rdpNouki = noukiAry[i-1];
	            var nouki = document.getElementById(rdpNouki).value;
	            var ddlBasho = tbl2.rows.item(1).cells.item(0).firstChild;
                var bashoCode = '';
                if (ddlBasho.length > 0 && ddlBasho.selectedIndex > 0)
                {
                    bashoCode = ddlBasho.options[ddlBasho.selectedIndex].value;
                }
                var tbxBikou = grid.rows.item(i).cells.item(8).firstChild;
                var bikou = tbxBikou.value;
                if (bTouroku)
                {
                    if (buhinKubun == '')
                    {
                        alert('品目グループを選択して下さい');
                        return '';
                    }
                    if (buhinCode == '')
                    {
                        alert('品目を選択して下さい');
                        return '';
                    }
                    
                    if (tbxTanka.value == '')
                    {
                        alert('単価を入力して下さい');
                        tbxTanka.focus();
                        return '';
                    }
//                    else if (tbxTanka.value == '0')
//                    {
//                        alert('単価は0以上で入力して下さい');
//                        tbxTanka.focus();
//                        return '';
//                    }
                    else if (!CheckDecimal(tbxTanka.value))
                    {
                        tbxTanka.focus();
                        return '';
                    }
                    if (suu == '')
                    {
                        alert('数量を入力して下さい');
                        return '';
                    }
                    else if(suu == '0')
                    {
                        alert('数量を0以上で入力して下さい');
                        return '';
                    }
                    else if (!CheckSuu(tbxSuu, '数量'))
                    {
                        tbxSuu.focus();
                        return '';
                    }
                    if (nouki == '')
                    {
                        alert('納期を入力して下さい');
                        return '';
                    }
                    else
                    {
                        if (NengappiCheck(nouki) == -1)
                        {
                            return '';
                        }
                    }
                    if (bashoCode == '')
                    {
                        alert('納品場所を選択して下さい');
                        return '';
                    } 
                    if (bikou.length > 200)
                    {
                        alert('備考は200文字以内で入力して下さい');
                        tbxBikou.focus();
                        return '';
                    }
                }
                if (meisai != '') meisai += '\t';
                meisai += shiireCode +'|'+ buhinKubun +'|'+ buhinCode +'|'+ lot +'|'+ tanka +'|'+ suu +'|'+ tani +'|'+ leadTime +'|'+ nouki +'|'+ bashoCode +'|'+ bikou + '|' + kariTankaFlg;
            }
            if (bTouroku && meisai == '')
            {
                alert('発注内容を入力して下さい');
                return '';
            }
            return meisai; 
        }

        function CheckDecimal(deci)
	    {
	        if (deci == '')
	        {
	            return true;
	        }
	        else
	        {
	            if (deci.match( /[^0-9.,]/ ) != null)
	            {
	                alert('単価の入力値が正しくありません');
	                return false;
	            }
	            var deciAry = deci.split('.');
	            if (deciAry.length > 2)
	            {
	                alert('小数点は1つだけ入力可能です');
	                return false;
	            }
	            if (deciAry.length == 2 && (deciAry[0].length == 0 || deciAry[1].length == 0))
	            {
	                alert('単価の入力値が正しくありません');
	                return false;
	            }
	            if (deciAry.length < 3 && deciAry[0].length > 8)
	            {
	                alert('単価の整数部は8桁以内で入力して下さい');
	                return false;
	            }
	            if (deciAry.length == 2 && deciAry[1].length > 2)
	            {
	                alert('単価の小数部は2桁以内で入力して下さい');
	                return false;
	            }
//	            if (deciAry.length == 2 && parseInt(deciAry[0]) == 0 && parseInt(deciAry[1]) == 0)
//	            {
//	                alert('単価は0以上で入力して下さい');
//	                return false;
//	            }
		        return true;
		    }
	    }
        // 年月日チェック(true = retunr日付, false = return -1)
	    function NengappiCheck(nengappi)
	    {
	        if (nengappi.length == 0)
	        {
	            alert("日付を入力して下さい");
                return -1;     
            }
            
            var nen = nengappi.match(/(\d{1,4})\/(\d{1,2})\/(\d{1,2})/);
            if (nen == null)
            {
                alert("日付を正しく入力して下さい");
                return -1;
            }

            var dt = new Date();
            var year = nen[1];
            var month = nen[2];
            var day = nen[3];
            // 年の判定
            if (year < dt.getFullYear() - 1)
            {
                alert("西暦を正しく入力して下さい"); 
                return -1;
            }

            month = FormatMonthDay(month);     
            // 月の判定
            if (!(month >= 1 && month <= 12))
            {
                alert("月を正しく入力して下さい");
                return -1;
            }
            var yy = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31, 29);
            var monthChk = month;
            // 閏年の判定
            if (((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0)) && month == 2)
                monthChk = 12;
            else
                monthChk--;
                        
            day = FormatMonthDay(day);
            // 日の判定
            if (!(day >= 1 && day <= yy[monthChk])) {
                alert("日付を正しく入力して下さい");
                return -1;
            }
            // 今日より前の日付はエラー
            if (dt.getFullYear() + FormatMonthDay(dt.getMonth()+1) + FormatMonthDay(dt.getDate()) > year + month + day)
            {
                alert('本日以降の日付を入力して下さい');
                return -1;
            }
            return (year + "/" + month + "/" + day);
        }
        function FormatMonthDay(monthDay)
        {   
            var str = monthDay.toString();
            if (str.length == 1)
            {
                return "0" + str;
            }
            else
            {
                return str;
            }
        }
        // 数値チェック
        function CheckSuu(tbx,str)
	    {
	        if (tbx.value.match(/[^0-9,]/) != null)
	        {
	            alert(str+"は半角数字のみで入力して下さい");
	            tbx.focus();
	            return false;
	        }
	        return true;
	    }	
        function DelChk(bool)
        {
            var idAry = document.getElementById('HidChkID').value.split(',');
            for (var i = 0; i < idAry.length; i++)
            {
                var chk = document.getElementById(idAry[i]);
                chk.checked = bool;
            }
        }
        function ShiireChange()
        {
            var meisai = CreateMeisai(false, false);
            AjaxRequest('ShiireChange', meisai);
        }
        function BuhinChange(rowNo)
        {
            document.getElementById('HidArgs').value = rowNo;
            var meisai = CreateMeisai(false, false);
            AjaxRequest('BuhinChange', meisai);
        }
        function Touroku()
        { 
            var meisai = CreateMeisai(false, true);
            /*
            if (meisai == '')
            {
                alert('発注内容を入力して下さい');
                return;
            } 
            */      
            if (meisai != '' && confirm('上記の内容で発注しますか？'))
            {
                AjaxRequest('Touroku', meisai);
            }
        }
        function AddRow()
        {
            var meisai = CreateMeisai(false, false);
            AjaxRequest('AddRow', meisai);
        }
        /*
        function Reload()
        {
	        AjaxRequest('Reload', meisai);
        }
        */
        function KubunChange()
        {
            var meisai = CreateMeisai(false, false);
            AjaxRequest('KubunChange', meisai);
        }

    </script>

</head>
<body class="bg0">
    <form id="form1" runat="server">
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
    <uc1:CtlTabMain ID="Tab" runat="server" />
    <table id="TblMain" runat="server" width="100%" class="def9">
        <tr>
            <td>
                <table id="Table1" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" height="20">
                            <asp:Label ID="LblMsg" runat="server" CssClass="b"></asp:Label>
                        </td>
                        <td class="hei20">
                            <img id="Img1" runat="server" src="../Img/Load.gif" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <table style="border-collapse:collapse;">
                                <tr>
                                    <td style="padding:0 0 0 0">
                                        <input id="BtnS" runat="server" type="button" value="チェックした行を削除する" class="bg6" />
                                    </td>
                                    <td style="padding:0 0 0 0">
                                        <input id="BtnClear" runat="server" type="button" value="入力した内容を全て削除する" class="bg6" />
                                    </td>
                                    <td style="padding:0 0 0 0">
                                        <table>
                                            <tr>
                                                <td>
                                                    &nbsp;税率：
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DdlTax" runat="server">
                                                        <asp:ListItem Value="8">8%</asp:ListItem>
                                                        <asp:ListItem Value="10">10%</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="G" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="def9" OnRowDataBound="G_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <br />
                            </EditItemTemplate>
                            <HeaderTemplate>
                                削<br />
                                除<br />
                                <input id="ChkH" runat="server" type="checkbox" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input id="ChkI" runat="server" type="checkbox" />
                            </ItemTemplate>
                            <ItemStyle CssClass="tc" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <table class="col" frame="void" width="100%">
                                    <tr>
                                        <td class="tc nw s1">発注No</td>
                                    </tr>
                                    <tr>
                                        <td class="tc nw">仕入先</td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table class="col" frame="void" width="100%">
                                    <tr>
                                        <td class="tl s3 hei25">
                                            <asp:Label ID="LblOrderNo" runat="server" CssClass="def12"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tl hei25">
                                            <asp:DropDownList ID="DdlShiire" runat="server"></asp:DropDownList>
<%--                                            <asp:DropDownList ID="DdlShiire" runat="server" OnSelectedIndexChanged="DdlShiire_SelectedIndexChanged"></asp:DropDownList>--%>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <ItemStyle CssClass="tc" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <table align="center" class="col" frame="void" width="100%">
                                    <tr>
                                        <td class="nw s3 hei25">
                                            <asp:DropDownList ID="DdlKubun" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="nw hei25">
                                            <asp:DropDownList ID="DdlBuhin" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <HeaderStyle CssClass="nw" />
                            <HeaderTemplate>
                                <table class="col" frame="void" width="100%">
                                    <tr>
                                        <td class="tc nw s1">
                                            品目グループ
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tc nw">
                                            品目コード/品目名
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <table align="center" class="col" frame="void" width="100%">
                                    <tr>
                                        <td class="tr nw s3 hei25">
                                            <asp:Label ID="LblLot" runat="server" CssClass="def12"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tr nw hei25">
                                            仮<input id="ChkKariTanka" runat="server" type="checkbox" />
                                            \<asp:TextBox ID="TbxTanka" runat="server" Width="60px" MaxLength="11" CssClass="tr"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <HeaderTemplate>
                                <table class="col" frame="void" width="100%">
                                    <tr>
                                        <td class="tc nw s1">
                                            ロット数
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tc nw">
                                            単価
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemStyle CssClass="nw" />
                            <HeaderStyle CssClass="nw" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="数量">
                            <ItemTemplate>
                                <asp:TextBox ID="TbxSuu" runat="server" CssClass="tr" MaxLength="8" Width="60px"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle CssClass="tc" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="単位">
                            <ItemTemplate>
                                <asp:Label ID="LblTani" runat="server" CssClass="def12"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="tc nw" />
                            <HeaderStyle CssClass="nw" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="リードタイム">
                            <ItemStyle CssClass="nw tc" />
                            <HeaderStyle CssClass="nw" />
                            <ItemTemplate>
                                <asp:Label ID="LblLT" runat="server" CssClass="def12"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <table class="col" frame="void" width="100%">
                                    <tr>
                                        <td class="tc s1">
                                            納期
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tc">
                                            納入場所
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table align="center" class="col" frame="void" width="100%">
                                    <tr>
                                        <td class="tc s3 hei25">
                                            <telerik:RadDatePicker id="RdpNouki" ToolTip="" MinDate="1950-01-01" Runat="server" Width="100px">
                                                <Calendar Runat="server" ShowRowHeaders="False"></Calendar>
                                                <DateInput Runat="server" DisplayDateFormat="yyyy/MM/dd" LabelCssClass="radLabelCss_Default" Width="80px"
                                                   DateFormat="yyyy/MM/dd" Font-Size="9pt" RangeValidation="Immediate" DisplayPromptChar="_" PromptChar=" ">
                                                </DateInput>
                                             </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tc hei25">
                                            <asp:DropDownList ID="DdlBasho" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <ItemStyle CssClass="tc" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="備考（200文字以内）">
                            <ItemTemplate>
                                <asp:TextBox ID="TbxBikou" runat="server" TextMode="MultiLine" MaxLength="200" Height="70px" Width="300px"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle CssClass="tc" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="bg1" />
                    <HeaderStyle BorderStyle="None" CssClass="bg3" />
                    <AlternatingRowStyle CssClass="bg2" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="tr">
                <input id="BtnAdd" runat="server" type="button" value="行を追加する" class="bg6" />
            </td>
        </tr>
        <tr>
            <td class="tc">
                <asp:Label ID="LblOK" runat="server" CssClass="b"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tc">
                <input id="BtnT" runat="server" type="button" value="上記の内容で発注する" class="b bg6" />
            </td>
        </tr>
        <tr>
            <td>
<%--                <radCln:RadCalendar ID="SC" runat="server" Skin="Web20">
                </radCln:RadCalendar>--%>
                <input id="HidChkID" runat="server" type="hidden" />
                <input id="HidArgs" runat="server" type="hidden" />
                <input id="HidNoukiID" runat="server" type="hidden" />
            </td>
        </tr>
    </table>
    <telerik:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
        <ClientEvents OnRequestStart="OnRequestStart" OnResponseEnd="OnResponseEnd" />
    </telerik:RadAjaxManager>
    </form>
<%--</body>--%>
</html>
