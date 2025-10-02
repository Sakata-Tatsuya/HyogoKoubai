<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderInfoForm.aspx.cs" Inherits="Koubai.Order.OrderInfoForm" ValidateRequest="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagName="CtlMainMenu" Src="~/CtlMainMenu.ascx" TagPrefix="uc1" %>
<%@ Register Src="~/Common/CtlNengappiFromTo.ascx" TagName="CtlNengappiFromTo" TagPrefix="uc3" %>
<%@ Register Src="~/Common/CtlMyPager.ascx" TagName="CtlMyPager" TagPrefix="uc2" %>
<%@ Register Assembly="Core" namespace="Core.Web" tagprefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>発注情報</title>
    <link href="../MainStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function $(id) {
            return document.getElementById(id);
        }
        function AjaxRequest(command_name, arg) {
            <%= Ram.ClientID %>.ajaxRequest(command_name + ':' + arg);
        }
        function pageLoad() {
            showpdf()
        }
        function showpdf() {
            var fileid = document.getElementById('HidFileID').value;
            if (0 < fileid.length) {
                document.getElementById('HidFileID').value = '';
                var url = "/Common/FileView.aspx?FileKey=" + fileid;
                var win = window.open(url, "_brank", "width=1200px,height=768px,location=no,resizable=yes,scrollbars=yes");
                win.focus();
            }
        }
        function Reload() {
            AjaxRequest('reload', '');
        }
        function Kensaku() {
            AjaxRequest('kensaku', '');
        }
        function RowChange() {
            AjaxRequest('row', '');
        }
        function PageChange(pageIndex) {
            AjaxRequest('page', pageIndex);
        }
        function OnBuhin() {
            AjaxRequest('ddlBuhin', '');
        }
        function HacchuuNo(key) {
            document.getElementById('HidKey').value = key + '\t' + 'hacchu';
            NewForm.action = "OrderShousaiForm";
            NewForm.target = "_hacchuu";
            OpenWinPost("_hacchuu", 500, 500, '');
            NewForm.submit();
        }
        function Print() {
        //    var chkIdAry = document.getElementById('HidChkID').value.split(',');
        //    var hidPrintKey = ''
        //    for (var i = 0; i < chkIdAry.length; i++) {
        //        var chk = document.getElementById(chkIdAry[i]);

        //        if (chk.checked) {
        //            if (hidPrintKey != "") hidPrintKey += "_";
        //            hidPrintKey += chk.value;
        //        }
        //    }
        //    if (hidPrintKey == "") {
        //        alert("チェックを入れてください");
        //        return false;
        //    }
        //    document.getElementById('HidKey').value = hidPrintKey;
        //    NewForm.action = "../Denpyou/HacchuushoForm";
        //    NewForm.target = "_hacchuusho";
        //    OpenWinPost("_hacchuusho", 800, 600, ',menubar=yes');
        //    NewForm.submit();
        }
        function Msg(key) {
            document.getElementById('HidKey').value = key;
            NewForm.action = "../MessegeForm";
            NewForm.target = "_msg";
            OpenWinPost("_msg", 700, 500, '');
            NewForm.submit();
        }
        var win = null;
        function OpenWinPost(target, w, h, etc) {
            win = window.open
                ("", target, "width=" + w + "px,height=" + h + "px,location=no,resizable=yes,scrollbars=yes" + etc);
            win.focus();
        }

        function Shounin(strKey) {
            if (confirm('承認しますか？')) {
                AjaxRequest('shounin', strKey);
            }
        }
        function ChkAll(bool) {
            var idAry = document.getElementById('HidChkID').value.split(',');
            for (var i = 0; i < idAry.length; i++) {
                var chk = document.getElementById(idAry[i]);
                chk.checked = bool;
            }
        }

        function OnRequestStart(sender, args) {
            document.getElementById("Img1").style.display = '';
        }
        function OnResponseEnd(sender, args) {
            document.getElementById('Img1').style.display = 'none';
            var cmd = args.EventArgument.substring(0, args.EventArgument.indexOf(":"));
            var param = args.EventArgument.substring(args.EventArgument.indexOf(":") + 1);

            switch (cmd) {
                case "nyuryoku_open":
                    var g = document.getElementById('G');
                    var cn_array = param.split('\t');
                    var div = document.getElementById('DivNoukiHenkou');
                    if (div.children.length != cn_array.length) {
                        alert('error');
                        return;
                    }
                    for (var i = 0; i < cn_array.length; i++) {
                        var index = GetIndex(cn_array[i]);
                        g.rows[index + 1].cells[<%=cell_index %>].innerHTML = div.children[i].innerHTML;
                    }
                    div.innerHTML = '';
                    break;
                case "nyuryoku_close":
                    var g = document.getElementById('G');
                    var cell_index = <%=cell_index %>;
                    var cn_array = param.split("\t");
                    var hid_current_array = document.getElementById('TbxHenkouNouki').value.split("\t");
                    if(hid_current_array.length != cn_array.length)
                    {
                        alert('error');
                        return;
                    }
                    for(var i = 0; i < cn_array.length; i++)
                    {
                        var index = GetIndex(cn_array[i]);
                        g.rows[index + 1].cells[<%=cell_index %>].innerHTML = hid_current_array[i];
                    }
                    break;
                case "nyuryoku_add_row":
                case "nyuryoku_del_row":
                    var g = document.getElementById('G');
                    var div = document.getElementById('DivNoukiHenkou');
                    var index = GetIndex(param);
                    g.rows[index + 1].cells[<%=cell_index %>].innerHTML = div.innerHTML;
                    div.innerHTML = '';
                    break;
                case "nouki_kaitou_reg":
                    break;
            }
        }

        function YN(cn_jk) {
            AjaxRequest('nyuryoku_open', cn_jk);
        }
        function GetIndex(cn) {
            var a = GetDataArray('C');  // key 'C'に紐づくデータは「注文番号_情報区分コード」のCSV形式データ

            for (var i = 0; i < a.length; i++) {
                if (cn == a[i])
                    return i;
            }
            return -1;
        }
        function GetDataArray(key) {
            return Core_GetDataArray(key, document.getElementById('HidDataKey'), document.getElementById('HidData'), ',', ':', true);
        }
        function GetDataArray2(key) {
            return Core_GetDataArray(key, document.getElementById('HidDataKey'), document.getElementById('HidData'), ',', ':', false);
        }

        // Keyに対応するデータを配列として返す
        function Core_GetDataArray(key, hidDataKey, hidData, separatorKey, separatorData, bErrorNotFoundKey) {
            if (null == hidData) return [];
            if (hidData.value == "") return [];

            var arrayKey = hidDataKey.value.split(separatorKey);
            var arrayData = hidData.value.split(separatorData);
            var index = -1;
            for (var i = 0; i < arrayKey.length; i++) {
                if (key == arrayKey[i]) {
                    index = i;
                    break;
                }
            }

            if (-1 == index) {
                if (bErrorNotFoundKey)
                    alert('JCore Error!! Not Found Key : ' + key);
                return [];
            }
            return arrayData[index].split(",");
        }

        function KeyCodeCheck() {
            var kc = event.keyCode;
            if ((kc >= 37 && kc <= 40) || (kc >= 48 && kc <= 57) || (kc >= 96 && kc <= 105) ||
                kc == 46 || kc == 8 || kc == 13 || kc == 9)
                return true;
            else
                return false;
        }
        function NKM_Close(cn) {
            AjaxRequest('nyuryoku_close', cn);
        }
        function NKM_Touroku(btn, cn_jk, _nk, _su, _kn) {
            NKC_REG(cn_jk);
        }
        function NKC_REG(cnjk_arg) {
            var nkd_array = [];

            if (null == cnjk_arg) {
                nkd_array = GetDataArray2('HenkouNoukiData');
            }
            else {
                var a = GetDataArray('HenkouNoukiData');
                for (var i = 0; i < a.length; i++) {
                    if (cnjk_arg == a[i].split('\t')[0]) {
                        nkd_array[0] = a[i];
                        break;
                    }
                }
                if (0 == nkd_array.length) {
                    alert('error');
                    return;
                }
            }

            if (0 == nkd_array.length)
                return;

            var data_array = [];
            var cn_tab_array = [];

            for (var i = 0; i < nkd_array.length; i++) {
                var kaitou_nouki_data = nkd_array[i].split('\t');
                var cn_jk = kaitou_nouki_data[0];
                var nk = document.getElementById(kaitou_nouki_data[1]);
                var suu = document.getElementById(kaitou_nouki_data[2]);
                var ktno = document.getElementById(kaitou_nouki_data[3]);

                if (null == nk || null == suu) continue;

                cn_tab_array.push(cn_jk);

                var nk_vals = nk.value.split(",");
                var suu_vals = suu.value.split(",");
                var ktno_vals = ktno.value.split(",");

                var nk_suu = '';

                for (var t = 0; t < nk_vals.length; t++) {
                    var n = document.getElementById(nk_vals[t]).value;
                    var s = document.getElementById(suu_vals[t]).value;
                    var no = document.getElementById(ktno_vals[t]).value;

                    if ('' == n && '' == s) continue;

                    if ('' == n) {
                        alert("納期を入力してください");
                        return;
                    }
                    if ('' == s) {
                        alert("数量を入力してください");
                        return;
                    }

                    if ('' != nk_suu) nk_suu += '\t';
                    nk_suu += (n + '\t' + s + '\t' + no);
                }
                nk_suu = nk_suu.replace('|', '');
                data_array.push(nk_suu);
            }

            if (0 == cn_tab_array.length) {
                alert("登録するデータがありません");
                return;
            }

            if (null == cnjk_arg) {
                if (!confirm('登録しますか？'))
                    return false;
            }

            var hid_arg = document.getElementById('HidHenkouNoukiArg');
            hid_arg.value = data_array.join("|");

            AjaxRequest('nouki_kaitou_reg', cn_tab_array.join("\t"));
        }

        function NKM_Sum(_suu, tbx_sum) {
            var dSum = 0;
            for (var i = 0; i < _suu.length; i++) {
                var t = document.getElementById(_suu[i]);
                if (t == null)
                    continue;
                if (t.value == "")
                    continue;
                if (isNaN(t.value))
                    continue;
                if (parseFloat(t.value) <= 0) {
                    t.value = "";
                    continue;
                }
                dSum += parseFloat(t.value);
            }

            document.getElementById(tbx_sum).value = (0 >= dSum) ? "" : Shousuu2keta(dSum);
        }
        function Shousuu2keta(num) {
            var n = num;
            n = n + ".";
            n = addComma(n.split(".")[0]) + (n.split(".")[1]).substring(0, 2);
            return n;
        }
        function addComma(value) {
            var i;
            for (i = 0; i < value.length / 3; i++) {
                value = value.replace(/^([+-]?\d+)(\d\d\d)/, "$1,$2");
            }
            return value;
        }

        function NKM_AddRow(cn, _nk, _suu, _ncno) {
            var str = '';
            for (var i = 0; i < _nk.length; i++) {
                var nk = document.getElementById(_nk[i]);
                var suu = document.getElementById(_suu[i]);
                var ncno = document.getElementById(_ncno[i]);
                if ('' != str) str += '\t';
                str = str + nk.value;
                str += '\t';
                str = str + suu.value;
                str += '\t';
                str = str + ncno.value;
            }
            document.getElementById('HidHenkouNoukiArg').value = str;
            AjaxRequest('nyuryoku_add_row', cn);
        }

        function NKM_DeleteRow(cn, index, _nk, _suu, _ncno) {
            var str = '';
            for (var i = 0; i < _nk.length; i++) {
                var nk = document.getElementById(_nk[i]);
                var suu = document.getElementById(_suu[i]);
                var ncno = document.getElementById(_ncno[i]);
                if (i == index) continue;
                if ('' != str) str += '\t';
                str = str + nk.value;
                str += '\t';
                str = str + suu.value;
                str += '\t';
                str = str + ncno.value;
            }
            document.getElementById('HidHenkouNoukiArg').value = str;
            AjaxRequest('nyuryoku_del_row', cn);
        }

        function NKC_OPEN() {
            var a = GetDataArray2('HenkouNoukiData');
            if (0 == a.length) {
                alert("表示する項目はありません");
                return;
            }
            var cn_array_not_open = [];
            for (var i = 0; i < a.length; i++) {
                var data_array = a[i].split('\t');
                var nk = document.getElementById(data_array[1]);
                if (null == nk)
                    cn_array_not_open.push(data_array[0]);
            }
            if (0 == cn_array_not_open.length) {
                alert("全て表示しています");
                return;
            }
            AjaxRequest('nyuryoku_open', cn_array_not_open.join("\t"));
        }

        function NKC_CLOSE() {
            var cn_array = GetDataArray('C');
            var a = GetDataArray2('HenkouNoukiData');
            if (0 == a.length)
                return;
            var cn_array_open = [];
            for (var i = 0; i < a.length; i++) {
                var data_array = a[i].split('\t');
                var nk = document.getElementById(data_array[1]);
                if (null != nk)
                    cn_array_open.push(data_array[0]);
            }
            if (0 == cn_array_open.length) {
                alert("全て非表示です");
                return;
            }
            AjaxRequest('nyuryoku_close', cn_array_open.join('\t'));
        }

    </script>

</head>
<body bottommargin="0" leftmargin="4" topmargin="0" rightmargin="4">
    <form id="form1" runat="server">
    <uc1:CtlMainMenu ID="M" runat="server"></uc1:CtlMainMenu>
    <table border="1" bordercolor="#000000" class="col def9 mt5 bg1">
        <tr>
            <td class="tl">
                <table border="1" bordercolor="#000000" class="col tc" frame="below" width="100%">
                    <tr class="bg3">
                        <td>発注No</td>
                        <td>仕入先</td>
                        <td>納入場所</td>
                        <td>納期回答状況</td>
                        <td>納品状況</td>
                        <td>納期</td>
                        <td>回答納期</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TbxHacchuNo" runat="server" Width="100px" MaxLength="7"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlShiire" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlNBasho" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlNKJyoukyou" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlNJyoukyou" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <uc3:CtlNengappiFromTo ID="CtlNouki" runat="server" />
                        </td>
                        <td>
                            <uc3:CtlNengappiFromTo ID="CtlKNouki" runat="server" />
                        </td>
                    </tr>
                </table>
                <table border="1" bordercolor="#000000" class="col tc" frame="void" width="100%">
                    <tr class="bg3">
                        <td>品目グループ</td>
                        <td>品目名</td>
                        <td>発注担当者</td>
                        <td>発注日</td>
                        <td>納品日</td>
                        <td>メッセージ</td>
                        <td>キャンセル注文</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DdlBuhinKubun" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlBuhin" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlHacchuTantousha" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <uc3:CtlNengappiFromTo ID="CtlHacchuubi" runat="server" />
                        </td>
                        <td>
                            <uc3:CtlNengappiFromTo ID="CtlNouhinbi" runat="server" />
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlMsg" runat="server"></asp:DropDownList>
                            &nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlCancel" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <input id="BtnK" runat="server" type="button" value="検索" class="w60 bg6" />
            </td>
        </tr>
    </table>
    <table id="TblList" runat="server" width="100%" class="def9">
        <tr>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%">
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
                <table width="100%" class="def9">
                    <tr>
                        <td class="nw" valign="bottom">
                            <uc2:CtlMyPager ID="Pt" runat="server" />
                        </td>
                        <td class="nw" valign="bottom">
<%--                            <input id="BtnI" runat="server" type="button" value="チェックした発注書を印刷する" class="bg6 w180 f9" />--%>
                            <asp:Button ID="BtnHP" runat="server" Text="チェックした発注書を印刷する" OnClick="BtnHP_Click" class="bg6 w180 f9"/>
                        </td>
                        <td class="tr" valign="bottom">
                            <table id="TblRow" runat="server">
                                <tr>
                                    <td nowrap="nowrap">
                                        納期回答入力：<input id="BtnAH" runat="server" type="button" class="w100 bg6 f9" value="納期入力欄を&#13;&#10;全て表示させる" />
                                        <input id="BtnAC" runat="server" type="button" class="w100 bg6 f9" value="納期入力欄を&#13;&#10;全て閉じる" />
                                        <input id="BtnAT" runat="server" type="button" class="f9 w150 bg6" value="表示されている入力欄を&#13;&#10;全て登録する" />
                                    </td>
                                    <td valign="bottom" nowrap="nowrap">
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
                                        </asp:DropDownList>
                                        行
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="G" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="def9"
                    OnRowDataBound="G_RowDataBound">
                    <PagerSettings Visible="False" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Literal ID="LitChk_H" runat="server" Text="発注書<br>印刷"></asp:Literal><br />
                                <input id="ChkH" runat="server" type="checkbox" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input id="ChkI" runat="server" type="checkbox" />
                            </ItemTemplate>
                            <ItemStyle CssClass="tc" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="キャン&lt;br&gt;セル">
                            <ItemStyle CssClass="tc" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <table class="tc col" frame="void" width="100%">
                                    <tr>
                                        <td class="s1">
                                            発注No
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            発注日
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table class="tl col" frame="void" width="100%">
                                    <tr>
                                        <td class="s2">
                                            <asp:Literal ID="LitHacchuuNo" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Literal ID="LitHacchuuBi" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <table class="tc col" width="100%" frame="void">
                                    <tr>
                                        <td class="s1">
                                            仕入先コード
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            仕入先名
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table class="tl col" width="100%" frame="void">
                                    <tr>
                                        <td class="s2">
                                            <asp:Literal ID="LitShiireCode" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Literal ID="LitShiireName" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <table class="tc col" width="100%" frame="void">
                                    <tr>
                                        <td class="s1">
                                            品目コード
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            品目名
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table class="col" width="100%" frame="void">
                                    <tr>
                                        <td class="s2">
                                            <asp:Literal ID="LitCode" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Literal ID="LitName" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <table class="tc col" width="100%" frame="void">
                                    <tr>
                                        <td width="50%" class="s1">
                                            数量
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="50%">
                                            単価
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table class="tr col" width="100%" frame="void">
                                    <tr>
                                        <td width="50%" class="s2">
                                            <asp:Literal ID="LitSuuryou" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="50%">
                                            <asp:Literal ID="LitTanka" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <table class="tc col" width="100%" frame="void">
                                    <tr>
                                        <td width="50%" class="s1">
                                            注文金額
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="50%">
                                            税額
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table class="tr col" width="100%" frame="void">
                                    <tr>
                                        <td width="50%" class="s2">
                                            <asp:Literal ID="LitChumonKingaku" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="50%">
                                            <asp:Literal ID="LitZeigaku" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="単位">
                            <ItemStyle CssClass="tc" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="納入&lt;br&gt;場所">
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <table class="tc col" frame="void" width="100%">
                                    <tr>
                                        <td class="s1">
                                            発注担当者コード
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            発注担当者名
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table class=" col" frame="void" width="100%">
                                    <tr>
                                        <td class="s2">
                                            <asp:Literal ID="LitHacchuuTantoushaCode" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Literal ID="LitHacchuuTantoushaMei" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="納期">
                            <ItemTemplate>
                                <asp:Label ID="LblNouki" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="tc" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="回答納期">
                            <ItemTemplate>
                                <asp:Label ID="LblKNouki" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="tc" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="納品日">
                            <ItemStyle CssClass="tc" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="メッセージ">
                            <ItemTemplate>
                                <input id="BM" runat="server" type="button" class="f8" />
                            </ItemTemplate>
                            <ItemStyle CssClass="tc" />
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
                <input id="HidDataKey" runat="server" type="hidden" />
                <input id="HidData" runat="server" type="hidden" />
                <input id="HidHenkouNouki" runat="server" type="hidden" />
                <input id="HidHenkouNoukiArg" runat="server" type="hidden" />
                <asp:TextBox ID="TbxHenkouNouki" runat="server" CssClass="none"></asp:TextBox>
                <asp:HiddenField ID="HidFileID" runat="server" />
                <asp:HiddenField ID="HidKeyPDF" runat="server" />
                <div id="DivNoukiHenkou" runat="server">
                </div>
            </td>
        </tr>
    </table>

    <telerik:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
         <ClientEvents OnRequestStart="OnRequestStart" OnResponseEnd="OnResponseEnd" />
    </telerik:RadAjaxManager>

    <telerik:RadCalendar ID="SC" runat="server" Skin="Web20">
    </telerik:RadCalendar>

    </form>
    <form id="NewForm" method="post" name="NewForm">
        <input id="HidKey" runat="server" type="hidden" />
    </form>
</body>
</html>
