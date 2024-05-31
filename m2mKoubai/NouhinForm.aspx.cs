using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using m2mKoubaiDAL;
using Telerik.Web.UI;

namespace m2mKoubai
{
    public partial class NouhinForm : System.Web.UI.Page
    {
        //前月一日
        DateTime minDate = new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 1);

        // 現在表示されている発注No
        private string VsInfo
        {
            get { return Convert.ToString(this.ViewState["VsInfo"]); }
            set { this.ViewState["VsInfo"] = value; }
        }

        // 完納ボタンを押したのか納品確定ボタンを押したのか判定
        bool judgeFlg = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionManager.UserKubun != (byte)UserKubun.Owner)
                {
                    HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return;
                }
                // タブ
                CtlTabMain tab = FindControl("Tab") as CtlTabMain;
                tab.Menu = CtlTabMain.MainMenu.Nouhin;

                DateTime dtNow = DateTime.Now;
                int nYear = dtNow.Year;
                // 今年
                DdlYear.Items.Add(nYear.ToString());
                // 来年
                nYear--;
                DdlYear.Items.Add(nYear.ToString());
                // 再来年
                nYear--;
                DdlYear.Items.Add(nYear.ToString());
                // 今年を選択する
                DdlYear.SelectedValue = dtNow.Year.ToString();
                //ListSet.SetYear(DdlYear);
                // 最初は非表示
                this.ShowTblMain(false);
                this.RdpDay.SelectedDate = DateTime.Today;
            }

        }

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.PreRender += new System.EventHandler(this.Form_PreRender);
        }
        private void Form_PreRender(object sender, EventArgs e)
        {
            // 確認
            this.BtnCK.Attributes["onclick"] = "Check(); return false;";
            // クリア
            this.BtnC.Attributes["onclick"] = string.Format("Clear('{0}'); return false;", TblList.ClientID);
            // 納品確定
            this.BtnNK.Attributes["onclick"] = "Nouhin(); return false;";
            // 完納
            this.BtnKN.Attributes["onclick"] = "Kannou(); return false;";
            // Img
            this.Img1.Style.Add("display", "none");
            // 
            this.TbxNouhinsuu.Attributes["onfocusout"] =
                string.Format("SuuryouChk('{0}','{1}','{2}'); return false;",
                LblSuuryou.Text.Replace(",", ""), LblNouhinSumiSuu.Text, TbxNouhinsuu.ClientID);

            this.TbxHacchuNo.Attributes["onkeyup"] = "KenChk();";
            // 数字のみ入力可
            this.SetNumOnly();

            /*
            ・カレンダーは1カ月以上前は選択不可。
             （手入力して登録しようとすると、エラーメッセージを表示する。）
            ・カレンダーは未来日での選択は不可。
             （手入力して登録しようとすると、エラーメッセージを表示する。）
            */
            this.RdpDay.MinDate = minDate;
            this.RdpDay.MaxDate = DateTime.Today;

        }
        // 数字のみ入力可
        private void SetNumOnly()
        {
            AppCommon.NumOnly(TbxNouhinsuu);
        }
        private void Create()
        {
            // 発注Noが7桁未満で入力されたら0を埋める
            string strHacchuNoZeroume = "";

            try
            {
                strHacchuNoZeroume = string.Format("{0:0000000}", Convert.ToInt32(TbxHacchuNo.Value));
            }
            catch
            {
                ShowMsg(AppCommon.NO_DATA, true);
                this.ShowTblMain(false);
                return;
            }

            // 注文Noによって、注文データを取得
            NouhinDataSet_N.V_NouhinRow dr = NouhinClass_Y.getV_NouhinRow(DdlYear.SelectedValue.Substring(2, 2), strHacchuNoZeroume, SessionManager.JigyoushoKubun, Global.GetConnection());

            if (dr == null)
            {
                ShowMsg(AppCommon.NO_DATA, true);
                this.ShowTblMain(false);
                return;

            }
            else if (dr.KaritankaFlg)
            {
                ShowMsg("この注文は仮単価で発注されています　単価を確定してください", true);
                this.ShowTblMain(false);
                return;
            }
            else
            {
                // 納品No
                int nNouhinNo = 0;
                if (!dr.IsNouhinNoNull())
                    nNouhinNo = dr.NouhinNo;

                VsInfo = dr.Year + "_" + dr.HacchuuNo + "_" + dr.JigyoushoKubun.ToString() + "_" + nNouhinNo.ToString();


                this.ShowMsg("", false);
                this.ShowTblMain(true);
                this.GetHacchuuInfo(dr);
            }
        }

        private void GetHacchuuInfo(NouhinDataSet_N.V_NouhinRow dr)
        {
            // 発注No
            this.LblHacchuuNo.Text = dr.HacchuuNo;

            // 仕入先コード               
            this.LblShiireCode.Text = dr.ShiiresakiCode;
            // 仕入先名
            this.LblShiireMei.Text = dr.ShiiresakiMei;
            // 品名グループ
            this.LblBuhinGroup.Text = dr.BuhinKubun;
            // コード
            this.LblBuhinCode.Text = dr.BuhinCode;
            // 品名
            if (!dr.IsBuhinMeiNull())
            {
                this.LblBuhinMei.Text = dr.BuhinMei;
            }
            else
            {
                this.LblBuhinMei.Text = "";
            }
            // 数量
            LblSuuryou.Text = dr.Suuryou.ToString("#,##0");
            // 単価            
            LblTanka.Text = "\\" + dr.Tanka.ToString("#,##0.#0");
            // 注文金額
            decimal dKingaku_Round = Math.Round((decimal)dr.Kingaku, 0, MidpointRounding.AwayFromZero);
            LblChumonKingaku.Text = Convert.ToString(string.Format("{0:C}", dKingaku_Round));

            if (!this.bTourokuKanryou)
            {
                this.DdlTax.SelectedValue = dr.Zeiritu.ToString();
                this.Create_Zeigaku(dKingaku_Round);
            }
            // 単位
            if (!dr.IsTaniNull())
            {
                LblTani.Text = dr.Tani;
            }
            else
            {
                this.LblTani.Text = "";
            }
            // 納入場所
            LblBasho.Text = dr.BashoMei;
            // 納期
            string strNouki = "";
            if (!dr.IsHenkouNoNull())
            {
                m2mKoubaiDataSet.T_NoukiHenkouDataTable dtNouki =
                    NoukiHenkouClass.getT_NoukiHenkouDataTable(dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, dr.HenkouNo, Global.GetConnection());
                for (int i = 0; i < dtNouki.Rows.Count; i++)
                {
                    strNouki += Utility.FormatFromyyyyMMdd(dtNouki[i].Nouki.ToString()) + "<br>";
                }
            }
            LblNouki.Text = Utility.FormatFromyyyyMMdd(dr.Nouki);
            // 回答納期
            string strKaitou = "";
            if (!dr.IsKaitouNoNull())
            {
                m2mKoubaiDataSet.T_NoukiKaitouDataTable dt =
                    NoukiKaitouClass.getT_NoukiKaitouDataTable(dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, dr.KaitouNo, Global.GetConnection());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strKaitou += Utility.FormatFromyyyyMMdd(dt[i].Nouki.ToString()) + "<br>";
                }
            }
            LblKaitouNouki.Text = strKaitou;

            // 納品済数量
            int nNouhinSuuryou = 0;
            if (!dr.IsNouhinSuuryouNull())
                nNouhinSuuryou = dr.NouhinSuuryou;

            LblNouhinSumiSuu.Text = nNouhinSuuryou.ToString("#,##0");

            // 納入残数
            int nZanSuu = dr.Suuryou - nNouhinSuuryou;
            TbxNouhinsuu.Text = nZanSuu.ToString();

            if (nZanSuu == 0 && dr.KannouFlg == false)
            {
                dr.KannouFlg = true;
                string[] strkey = VsInfo.Split('_');
                m2mKoubaiDataSet.T_ChumonRow drChumon = this.UpdateCreateRow();
                if (dr == null)
                    return;
                string strYear = strkey[0];
                string strHacchuuNo = strkey[1];
                int nKubun = int.Parse(strkey[2]);
                int nNo = int.Parse(strkey[3]);

                LibError err = ChumonClass.T_Chumon_Update(strYear, strHacchuuNo, nKubun, drChumon, Global.GetConnection());
                if (err != null)
                {
                    this.ShowMsg(err.Message, true);
                }                
            }
            if (nZanSuu != 0 && dr.KannouFlg == false)
            {
                this.TbxNouhinsuu.Visible = true;
                this.LitMsg.Text = "";
            }
            if (dr.KannouFlg == true)
            {
                this.TbxNouhinsuu.Visible = false;
                this.LitMsg.Text = "完納済";
                this.BtnNK.Visible = false;
                this.BtnKN.Visible = false;
            }  
        }

        // GridView表示
        private void ShowTblMain(bool b)
        {
            TblMain.Visible = b;
            BtnNK.Visible = b;
            BtnKN.Visible = b;
        }
        // メッセージ表示
        private void ShowMsg(string strMsg, bool bError)
        {
            LblMsg.Text = strMsg;
            LblMsg.ForeColor = (bError) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
        }

        bool bTourokuKanryou = false;

        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            this.Ram.AjaxSettings.Clear();

            string[] strArgs = e.Argument.Split(':');
            string strCmd = strArgs[0];

            if (strCmd == "Check")
            {
                this.Create();

                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TbxHacchuNo);
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
            }
            else if (strCmd == "Clear")
            {
                this.TbxHacchuNo.Value = "";
                this.ShowTblMain(false);
                this.ShowMsg("", false);
                this.BtnNK.Visible = false;
                this.BtnKN.Visible = false;

                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TbxHacchuNo);
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
            }
            else if (strCmd == "Nouhin")
            {
                /*
                 ・カレンダーは1カ月以上前は選択不可。
　                （手入力して登録しようとすると、エラーメッセージを表示する。）
                ・カレンダーは未来日での選択は不可。
　                （手入力して登録しようとすると、エラーメッセージを表示する。）
                */
                if (this.RdpDay.SelectedDate == null)
                {
                    this.ShowMsg("納品日が指定されていません", true);
                    return;
                }
                
                if (this.RdpDay.SelectedDate.Value < minDate)
                {
                    this.ShowMsg("前月一日以前の日付は指定出来ません", true);
                    return;
                }

                if (DateTime.Today < this.RdpDay.SelectedDate.Value)
                {
                    this.ShowMsg("本日以降の日付は指定出来ません", true);
                    return;
                }

                string[] strkey = VsInfo.Split('_');

                m2mKoubaiDataSet.T_NouhinRow drN = this.CreateRow();
                if (drN == null)
                    return;

                m2mKoubaiDataSet.T_ChumonRow drC = this.UpdateCreateRow();
                if (drC == null)
                    return;
                
                // 登録                
                LibError err = NouhinClass_Y.T_Nouhin_Insert_T_Chumon_Update
                    (drN, drC, strkey[0], strkey[1], int.Parse(strkey[2]), int.Parse(strkey[3]),judgeFlg, SessionManager.LoginID, Global.GetConnection());
                if (err != null)
                {
                    this.ShowMsg("登録に失敗しました" + "<br/>" + err.Message, true);
                }
                else
                {
                    bTourokuKanryou = true;
                    this.Create();
                    this.ShowMsg("登録しました", false);
                }

                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
            }
            else if (strCmd == "Kannou")
            {
                ///
                if (this.RdpDay.SelectedDate == null)
                {
                    this.ShowMsg("納品日が指定されていません", true);
                    return;
                }

                if (this.RdpDay.SelectedDate.Value < minDate)
                {
                    this.ShowMsg("前月一日以前の日付は指定出来ません", true);
                    return;
                }

                if (DateTime.Today < this.RdpDay.SelectedDate.Value)
                {
                    this.ShowMsg("本日以降の日付は指定出来ません", true);
                    return;
                }


                this.judgeFlg = true;
                string[] strkey = VsInfo.Split('_');

                m2mKoubaiDataSet.T_NouhinRow drN = this.CreateRow();
                if (drN == null)
                    return;

                m2mKoubaiDataSet.T_ChumonRow drC = this.UpdateCreateRow();
                if (drC == null)
                    return;

                LibError err = NouhinClass_Y.T_Nouhin_Insert_T_Chumon_Update
                    (drN, drC, strkey[0], strkey[1],int.Parse(strkey[2]),int.Parse(strkey[3]), judgeFlg, SessionManager.LoginID, Global.GetConnection());
                if (err != null)
                {
                    this.ShowMsg("登録に失敗しました" + "<br/>" + err.Message, true);
                    this.TblMain.Style.Add("display", "none");
                }
                else
                {
                    bTourokuKanryou = true;
                    this.Create();
                    this.ShowMsg("登録しました", false);
                    this.TbxNouhinsuu.Visible = false;
                    this.LitMsg.Text = "完納済";
                    this.BtnNK.Visible = false;
                    this.BtnKN.Visible = false;
                }

                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
            }

        }
        // 登録用Row
        private m2mKoubaiDataSet.T_NouhinRow CreateRow()
        {
            string[] strkey = VsInfo.Split('_');
            // NewRow
            m2mKoubaiDataSet.T_NouhinRow dr = NouhinClass_Y.newT_NouhinRow();

            dr.Year = strkey[0];
            dr.HacchuuNo = strkey[1];
            dr.JigyoushoKubun = int.Parse(strkey[2]);
            dr.NouhinNo = int.Parse(strkey[3]) + 1;
            dr.Suuryou = int.Parse(TbxNouhinsuu.Text );
            dr.NouhinBi = this.RdpDay.SelectedDate.Value;
            dr.Zeiritu = Convert.ToInt32(this.DdlTax.SelectedValue);
            dr.KeigenZeirituFlg = Utility.GetKeigenZeirituFlg(dr.NouhinBi,DdlTax.SelectedValue);

            return dr;

        }

        private m2mKoubaiDataSet.T_ChumonRow UpdateCreateRow()
        {
            string[] strkey = VsInfo.Split('_');
            // NewRow
            m2mKoubaiDataSet.T_ChumonRow dr = ChumonClass.getT_ChumonRow(strkey[0], strkey[1], int.Parse(strkey[2]), Global.GetConnection());
            dr.KannouFlg = true;

            return dr;

        }

        protected void DdlTax_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strChumonKingaku = this.LblChumonKingaku.Text.Trim().Replace("\\", "").Replace(",", "");

            int nChumonKingaku = 0;
            if (int.TryParse(strChumonKingaku, out nChumonKingaku))
            {
                this.Create_Zeigaku(nChumonKingaku);
            }
            else
            {
                this.LblZeigaku.Text = "";
            }
        }


        private void Create_Zeigaku(decimal dKingaku_Round)
        {
            int Zeiritu = Convert.ToInt32(this.DdlTax.SelectedValue);

            decimal dZeigaku = Math.Round(dKingaku_Round * Zeiritu / 100, 0, MidpointRounding.AwayFromZero);

            LblZeigaku.Text = Convert.ToString(string.Format("{0:C}", dZeigaku));
        }

        protected void RcbHacchuNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = o as RadComboBox;

            rcb.Items.Clear();
            string strText = e.Text.Trim();

            rcb.Height = Unit.Pixel(180);

            int itemOffset = e.NumberOfItems;
            int endOffset = itemOffset + 20;

            int nTotal = 0;
            NouhinDataSet_N.V_NouhinDataTable dt = null;

            try
            {
                NouhinClass_Y.getYAE_M_TorihikisakiDataTable(DdlYear.SelectedValue.Substring(2, 2), strText, SessionManager.JigyoushoKubun, itemOffset, 20, Global.GetConnection(), out dt, ref nTotal);
            }
            catch (Exception ex)
            {
                e.Message = ex.Message;
                return;
            }
            for (int i = 0; i < dt.Count; i++)
            {
                rcb.Items.Add(new RadComboBoxItem(dt[i].HacchuuNo, dt[i].HacchuuNo));
            }

            e.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>",
                endOffset.ToString(), nTotal.ToString());
        }




    }
}