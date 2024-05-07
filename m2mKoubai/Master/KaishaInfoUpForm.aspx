<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KaishaInfoUpForm.aspx.cs" Inherits="m2mKoubai.Master.KaishaInfoUpForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>会社情報登録</title>
    <link href="../MainStyle.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript">
  function $(id)
  {
      return document.getElementById(id);
  }
  
  function OnLoad(loadFlg)
  {
      if (window.opener != null)
      {
          if (loadFlg == 1)
          {
              window.opener.Reload();
          }
      }
  }
  function Touroku()
  {     
      if (!TourokuChk(true))
          return;         
      
      if (confirm("登録しますか？"))
      {
          document.getElementById('BtnTS').click();
      }
  }  
  function TourokuChk(bool)
  {
      if (bool)
      {     
         /*
          var TbxKaishaMei = document.getElementById('TbxKaishaMei');
          if (TbxKaishaMei.value == "")
          {
              alert("会社名を入力してください");
              TbxKaishaMei.focus();
              return;
          }
          */
           var TbxKaishaID = document.getElementById('TbxKaishaID');
          if (TbxKaishaID.value == "")
          {
              alert("事業所コードを入力してください");
              TbxKaishaID.focus();
              return;
          }
           if (TbxKaishaID.value == "0")
          {
              alert("事業所コードは0以上で入力して下さい");
              TbxKaishaID.focus();
              return;
          }
          var TbxEigyousho = document.getElementById('TbxEigyousho');
          if (TbxEigyousho.value == "")
          {
              alert("事業所名を入力してください")
              TbxEigyousho.focus();
              return;
          }
          var TbxJyusho = document.getElementById('TbxJyusho');
          if (TbxJyusho.value == "")
          {
              alert("住所を入力してください");
              TbxJyusho.focus();
              return;
          }
          var TbxYubin = document.getElementById('TbxYubin');
          if (TbxYubin.value == "")
          {
              alert("郵便番号を入力してください")
              TbxYubin.focus();
              return;
          }
          var TbxTel = document.getElementById('TbxTel');
          if (TbxTel.value == "")
          {
              alert("電話番号を入力してください")
              TbxTel.focus();
              return;
          }                                               
      }
      return true;           
  }
  function Koushin()
  {
      if (!TourokuChk(false))
          return;
      if (confirm("更新しますか？"));
      {
          document.getElementById('BtnKS').click();
      }
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
  function Close()
  {
      window.close();
  }
  
   
      

  
  </script>
</head>
<body class="bg0" onload="OnLoad(<%=loadFlg%>)">
    <form runat="server">
    <div>
        <table id="TblAll" runat="server" align="center" class="def10">
            <tr>
                <td class="tc hei20">
                    <asp:Label ID="LblMsg" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td align="right">
                    <input id="BtnC" runat="server" class="w80 bg6" onclick="Close()" type="button" value="閉じる" /></td>
            </tr>
            <tr>
                <td>
                    <table id="TblMain" runat="server" border="1" bordercolor="#000000" class="col bg1" align="center">
                        <tr>
                            <td class="bg4 hei20" >
                                事業所コード</td>
                            <td class="hei20"  >
                                <asp:TextBox ID="TbxKaishaID" runat="server" MaxLength="10" Width="150px" Height="15px"></asp:TextBox>
                                <asp:Literal ID="LitCode" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                事業所名</td>
                            <td class="hei20" >
                                <asp:TextBox ID="TbxEigyousho" runat="server" MaxLength="30" Width="250px" Height="15px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                住所</td>
                            <td >
                               <asp:TextBox ID="TbxJyusho" runat="server" MaxLength="50" Width="350px" CssClass="hei20" Height="15px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                郵便番号</td>
                            <td class="hei20" >
                                <asp:TextBox ID="TbxYubin" runat="server" MaxLength="8" Width="100px" Height="15px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bg4" >
                                TEL</td>
                            <td class="hei20"  >
                                <asp:TextBox ID="TbxTel" runat="server" MaxLength="15" Width="200px" Height="15px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bg4">
                                Fax</td>
                            <td class="hei20" >
                                <asp:TextBox ID="TbxFax" runat="server" MaxLength="15" Width="200px" Height="15px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bg4" >
                                E-Mail</td>
                            <td colspan="1" class="hei20" >
                                <asp:TextBox ID="TbxMail" runat="server" Width="350px" MaxLength="50" Height="15px"></asp:TextBox></td>
                           
                        </tr>                        
                        
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table id="TblBtn" runat="server">
                        <tr>
                            <td>
                                <input id="BtnT" runat="server" class="w80 bg6" type="button" value="登録" />
                                <input id="BtnK" runat="server" class="w80 bg6" type="button" value="更新" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
        <asp:Button ID="BtnTS" runat="server" OnClick="BtnTS_Click" />
        <asp:Button ID="BtnKS" runat="server" OnClick="BtnKS_Click" />
    </form>
</body>
</html>
