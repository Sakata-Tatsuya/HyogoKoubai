using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Collections;

using Telerik.Web.UI;
using KoubaiDAL;

namespace Koubai.Master
{
    public partial class KouseiMasterForm : HonyakuPage
    {
        private enum EnumEditMode
        {
            InitialState = 0,       //初期
            PreRegistratison = 1,   //仮登録
        }

        private ArrayList VsCheck
        {

            set { this.ViewState["VsCheck"] = value; }
            get { return this.ViewState["VsCheck"] as ArrayList; }
        }

        private int VsCurrentPageIndex
        {
            get
            {
                object obj = this.ViewState["page_index"];
                if (null == obj) return 0;
                return Convert.ToInt32(obj);
            }
            set
            {
                this.ViewState["page_index"] = value;
            }
        }

        private bool VsCancel
        {
            set
            {
                this.ViewState["VsCancel"] = value;
            }
            get
            {
                return Convert.ToBoolean(this.ViewState["VsCancel"]);
            }
        }

        private byte VsEditMode
        {
            set
            {
                this.ViewState["VsEditMode"] = value;
            }
            get
            {
                object o = this.ViewState["VsEditMode"];
                return byte.Parse(o.ToString());
            }
        }

        private enum EnumListMode
        {
            HinmokuList = 0,
            TreeList = 1,
        }
        private byte VsListMode
        {
            set
            {
                this.ViewState["VsListMode"] = value;
            }
            get
            {
                object o = this.ViewState["VsListMode"];
                return byte.Parse(o.ToString());
            }
        }

        private bool VsIsNoGridData
        {
            set
            {
                this.ViewState["VsIsNoGridData"] = value;
            }
            get
            {
                return Convert.ToBoolean(this.ViewState["VsIsNoGridData"]);
            }
        }

        protected string H(string str)
        {
            return SessionManager.User.Honyaku(str);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LblMsg.Text = "";

            if (!this.IsPostBack)
            {
                VsCheck = new ArrayList();

                divMain.Visible = true;
                divUpload.Visible = false;
                this.VsEditMode = (byte)EnumEditMode.InitialState;
                this.VsListMode = (byte)EnumListMode.HinmokuList;
                this.VsIsNoGridData = true;

                this.Rtl.Visible = false;
                this.DivTree.Visible = false;
                divKensaku.Visible = true;

                CreateList();
            }
            UserKanri();
        }

        private const int COL_LV = 0;
        private const int COL_HINMOKU_CODE = 1;
        private const int COL_HINMOKU_NAME = 2;
        private const int COL_KOUSEI_SUU = 3;
        private const int COL_KOUSEI_SUU_BUNBO = 4;
        private const int COL_KOUSEI_SUU_TANI = 5;
        private const int COL_SEQ = 6;
        private const int COL_START_DAY = 7;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            bool isPreRegistratison = (this.VsEditMode == (byte)EnumEditMode.PreRegistratison);
        }

        protected void BtnKensaku_Click(object sender, EventArgs e)
        {
            CreateList();
            //this.CreateTree(true);

            //this.DivTree.Visible = true;
            //divKensaku.Visible = false;
        }

        DataView dv = null;

        private void CreateTree(bool isFirst)
        {
            if (VsListMode != (byte)EnumListMode.HinmokuList)
            {
                btnDL.Enabled = true;
                BtnDelCheck.Enabled = false;
            }
            else
            {
                btnDL.Enabled = false;
                BtnDelCheck.Enabled = true;
            }

            string hinban = this.HM.SelectedValue.Trim();
            string hinmei = this.HidHinmei.Value;

            
            //if (!ChkAssist.Checked)
            //    hinban = this.HM.Text.Trim();

            MasterClass.ParamKouseiTree p = new MasterClass.ParamKouseiTree();

            p.userId = SessionManager.User.UserID;
            p.topPartsCode = hinban;
            SeisanDataSet.VIEW_KouseiTreeVDataTable dtBind = MasterClass.GetVIEW_KouseiTreeVDataTable(p, Global.GetConnection());

            if (dtBind.Count == 0)
            {
                this.Rtl.Visible = false;
                this.DivTree.Visible = false;
                ShowMsg(LblMsg, "製品情報、又は部品表が登録されていません。", true);
                return;
            }
            else
            {
                this.Rtl.Visible = true;
                this.DivTree.Visible = true;
                divKensaku.Visible = false;
            }

            HM.Text = hinban + " : " + hinmei;

            SeisanDataSet.VIEW_KouseiTreeVRow drBind = dtBind.NewVIEW_KouseiTreeVRow();
            drBind.SeihinCode = hinban;
            drBind.BuhinCode = hinban;
            drBind.KouteiCode = string.Empty;
            drBind.KouteiMei = string.Empty;
            drBind.HinmokuBunruiCode = string.Empty;
            drBind.HinmokuBunrui = string.Empty;
            drBind.HinmokuCode = string.Empty;
            drBind.Hinmei = string.Empty;
            drBind.Insu = 1;
            drBind.UpdateValue = string.Empty;
            drBind.BeforeValue = string.Empty;
            drBind.AfterValue = string.Empty;
            drBind.Tantou = string.Empty;
            drBind.Syounin = string.Empty;
            drBind.SelectOR = string.Empty;
            drBind.No = 0;
            drBind.ORNo = string.Empty;
            drBind.MakerCode = string.Empty;
            drBind.TooshiNo = string.Empty;
            drBind.Bikou1 = string.Empty;
            drBind.Bikou2 = string.Empty;
            drBind.HyoujiSeihinCode = hinban;
          
           dtBind.AddVIEW_KouseiTreeVRow(drBind);

            this.dv = dtBind.DefaultView;

            this.Rtl.DataSource = dtBind;

            if (isFirst)
            {
                this.Rtl.DataBind();
                this.Rtl.ExpandAllItems();
            }
        }

        private void ShowMsg(Label lblMsg, string strMsg, bool bError)
        {
            lblMsg.Text = strMsg;
            lblMsg.ForeColor = (bError) ? System.Drawing.Color.Red : System.Drawing.Color.Blue;
        }

        protected void Rtl_NeedDataSource(object sender, TreeListNeedDataSourceEventArgs e)
        {
            this.CreateTree(false);
        }

        protected void Rtl_PreRender(object sender, EventArgs e)
        {


            for (int i = 0; i < this.Rtl.Items.Count; i++)
            {

               
                string oyaHinban = this.Rtl.Items[i]["SeihinCode"].Text;
                string hinban = this.Rtl.Items[i]["BuhinCode"].Text;
             
                string strMitouroku = H("マスタ未登録");

                this.Rtl.Items[i]["Lv"].Text = this.Rtl.Items[i].HierarchyIndex.NestedLevel.ToString();
                double dInsu = 0;
                if (double.TryParse(this.Rtl.Items[i]["Insu"].Text, out dInsu))
                    this.Rtl.Items[i]["Insu"].Text = (dInsu).ToString("0.#####");

                double dMarumeSu = 0;
                if (double.TryParse(this.Rtl.Items[i]["MarumeSu"].Text, out dMarumeSu))
                    this.Rtl.Items[i]["MarumeSu"].Text = (dMarumeSu).ToString("#,0");



                if (hinban.Equals("&nbsp;"))
                {
                    hinban = "";
                }
                if (oyaHinban.Equals("&nbsp;"))
                {
                    oyaHinban = "";
                }
                if (this.Rtl.Items[i]["Lv"].Text =="0")
                {
                    this.Rtl.Items[i]["SeihinCode"].Text = hinban;
                    this.Rtl.Items[i]["BuhinCode"].Text = "";
                }
            }
        }

        private void CallParent(DataView dvOyaKo, string hinban, string oyaHinban, ref int lv)
        {
            dvOyaKo.RowFilter = string.Format("BuhinCode = '{0}' AND SeihinCode = '{1}'", hinban, oyaHinban);

            if (dvOyaKo.Count != 1)
            {
                return;
            }

            object objHinban = dvOyaKo[0]["SeihinCode"];
            if (objHinban == null)
            {
                return;
            }

            string oyaHinbanNew = objHinban.ToString();

            lv++;

            this.CallParent(dvOyaKo, oyaHinban, oyaHinbanNew, ref lv);
        }

        protected void HM_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            Telerik.Web.UI.RadComboBox rcb = o as Telerik.Web.UI.RadComboBox;

            rcb.Items.Clear();

            string strText = e.Text.Trim();

            if ("" == strText)
            {
                rcb.Height = Unit.Pixel(0);
                e.Message = H("入力してください。");
                return;
            }
            rcb.Height = Unit.Pixel(180);

            int itemOffset = e.NumberOfItems;
            int endOffset = itemOffset + 20;

            int nTotal = 0;
            //YAEViewDataSet.VIEW_BuhinDataTable dt = null;
            //MasterDataSet.M_SeihinDataTable dtS = null;

            SeisanDataSet.VIEW_KouseiListDataTable dt = null;

            try
            {
                MasterClass.getVIEW_KouseiListDataTable(strText, true, itemOffset, 20,
                    Global.GetConnection(), out dt, ref nTotal);
            }
            catch (Exception ex)
            {
                e.Message = ex.Message;
                return;
            }

            for (int i = 0; i < dt.Count; i++)
            {
                rcb.Items.Add(new Telerik.Web.UI.RadComboBoxItem(dt[i].SeihinCode + " : " + dt[i].SeihinMei, dt[i].SeihinCode));
            }

            e.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>",
                endOffset.ToString(), nTotal.ToString());

            if (nTotal == 0)
            {
                rcb.Height = Unit.Pixel(0);
                e.Message = H("マスタにデータがありません。");
                return;
            }
        }

        protected void ChkAssist_CheckedChanged(object sender, System.EventArgs e)
        {

        }

        protected void btnDL_Click(object sender, EventArgs e)
        {
            string hinban = this.HM.SelectedValue.Trim();

            if (hinban.Length == 0)
            {
                Ram.Alert("一覧画面からのダウンロードはできません。");
                return;
            }

            if (Rtl.Items.Count == 0)
            {
               // Ram.Alert("製品を選択してください。");
                //return;
            }

            MasterClass.ParamKouseiTree p = new MasterClass.ParamKouseiTree();


            SeisanDataSet.VIEW_KouseiTreeVDataTable dt = new SeisanDataSet.VIEW_KouseiTreeVDataTable();

            dt.Columns.Add("Lv", typeof(string));
            //dt.Columns.Add("KoteiCode", typeof(string));

            for (int i = 0; i < Rtl.Items.Count; i++)
            {
                SeisanDataSet.VIEW_KouseiTreeVRow drBind = dt.NewVIEW_KouseiTreeVRow();

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string colName = dt.Columns[j].ColumnName;

                    //if (colName == "TorihikisakiCode" || colName == "TorihikisakiName" || colName == "KoteiCode" || colName == "MarumeSu")
                    if (colName == "No" || colName == "TooshiNo" || colName == "HyoujiSeihinCode" || colName == "UpdateDate")
                        continue;

                    string val = this.Rtl.Items[i][colName].Text;

                    if (val.Equals("&nbsp;"))
                    {
                        val = "";
                    }

                    drBind[colName] = val;
                }

                string lv = this.Rtl.Items[i].HierarchyIndex.NestedLevel.ToString();
                drBind["Lv"] = lv;

                dt.AddVIEW_KouseiTreeVRow(drBind);
            }

            Core.Data.DataTable2Text d2 = new Core.Data.DataTable2Text();

            d2.Columns.Add("Lv", "Lv", null, null, null);
            d2.Columns.Add("SeihinCode", "製品番号", null, null, null);
            d2.Columns.Add("BuhinCode", "部品番号", null, null, null);
            d2.Columns.Add("BuhinName", "部品名", null, null, null);
            d2.Columns.Add("Insu", "員数", null, null, null);
            d2.Columns.Add("MarumeSu", "まるめ数", null, null, null);
            d2.Columns.Add("KouteiCode", "工程コード", null, null, null);
            d2.Columns.Add("HinmokuBunrui", "品目分類", null, null, null);
            d2.Columns.Add("Hinmei", "品名", null, null, null);
            d2.Columns.Add("HinmokuCode", "品目コード", null, null, null);
            d2.Columns.Add("ORNo", "OR品No", null, null, null);
            d2.Columns.Add("SelectOR", "OR品選択", null, null, null);
            d2.Columns.Add("Bikou1", "備考1", null, null, null);
            d2.Columns.Add("Bikou2", "備考2", null, null, null);

            if (0 == dt.Rows.Count)
            {
                this.Ram.Alert(H("該当のデータはありません。"));
                return;
            }

            bool bTab = false;
            Core.Data.DataTable2Text.EnumDataFormat fmt =
                (bTab) ? Core.Data.DataTable2Text.EnumDataFormat.Tab :
                Core.Data.DataTable2Text.EnumDataFormat.Csv;

            d2.Format = fmt;

            d2.DataSource = dt;

            string strData = d2.GetTextData();

            string strExt = (bTab) ? "txt" : "csv";

            string strFileName = string.Format("{0}_構成表.{1}", hinban, strExt);

            this.Ram.ResponseScripts.Add(string.Format("window.location.href='{0}';",
                this.ResolveUrl("../Common/DownloadDataForm.aspx?" +
                Common.DownloadDataForm.GetQueryString4Text(strFileName, d2.GetTextData())))
                );
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //this.Ram.ResponseScripts.Add(string.Format("window.open('{0}','_blank');", this.ResolveUrl("../Master/UpdateKousei.aspx?seihinCode=" + HM.SelectedValue.Replace("#", "%23"))));

        }

        protected void DG_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (!(Telerik.Web.UI.GridItemType.Item == e.Item.ItemType ||
                Telerik.Web.UI.GridItemType.AlternatingItem == e.Item.ItemType))
                return;

            SeisanDataSet.VIEW_SeihinKouseiCheckRow dr = ((DataRowView)e.Item.DataItem).Row as SeisanDataSet.VIEW_SeihinKouseiCheckRow;
            e.Item.Cells[DG.MasterTableView.Columns.FindByUniqueName("SeihinCode").OrderIndex].Text = dr.SeihinCode;

            if (!dr.IsSeihinMeiNull() && dr.SeihinMei != "")
            { e.Item.Cells[DG.MasterTableView.Columns.FindByUniqueName("SeihinMei").OrderIndex].Text = dr.SeihinMei; }
            else
            { e.Item.Cells[DG.MasterTableView.Columns.FindByUniqueName("SeihinMei").OrderIndex].Text = ""; }

            if (dr.SeihinM != "")
            { e.Item.Cells[DG.MasterTableView.Columns.FindByUniqueName("SeihinM").OrderIndex].Text = "登録済"; }
            else
            {
                e.Item.Cells[DG.MasterTableView.Columns.FindByUniqueName("SeihinM").OrderIndex].Text = "未登録";
                e.Item.Cells[DG.MasterTableView.Columns.FindByUniqueName("SeihinM").OrderIndex].ForeColor = Color.Red;
            }

            if (dr.KouseiM != "")
            { e.Item.Cells[DG.MasterTableView.Columns.FindByUniqueName("KouseiM").OrderIndex].Text = "登録済"; }
            else
            {
                e.Item.Cells[DG.MasterTableView.Columns.FindByUniqueName("KouseiM").OrderIndex].Text = "未登録";
                e.Item.Cells[DG.MasterTableView.Columns.FindByUniqueName("KouseiM").OrderIndex].ForeColor = Color.Red;
            }

            if (!dr.IsUpdateTimeNull())
            { e.Item.Cells[DG.MasterTableView.Columns.FindByUniqueName("UpdateTime").OrderIndex].Text = dr.UpdateTime.ToString("yyyy/MM/dd HH:mm"); }
            else
            { e.Item.Cells[DG.MasterTableView.Columns.FindByUniqueName("UpdateTime").OrderIndex].Text = ""; }

            CheckBox cbx = (CheckBox)e.Item.FindControl("ChkSelect");
            cbx.Attributes["onClick"] = string.Format("__doPostBack('{0}',{1});", cbx.UniqueID, e.Item.ItemIndex);

            if (VsCheck.Contains(dr.SeihinCode.ToString()))
            { cbx.Checked = true; }


            Button btnS = e.Item.FindControl("btnS") as Button;
            btnS.CommandName = "ref";
            btnS.CommandArgument = dr.SeihinCode;
        }

        protected void DG_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            DG.MasterTableView.CurrentPageIndex = e.NewPageIndex;
            CreateList();
        }

        bool _bD_PageSizeChanged = false;
        protected void DG_PageSizeChanged(object source, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
        {
            if (!_bD_PageSizeChanged)
            {
                _bD_PageSizeChanged = true;
                DG.MasterTableView.PageSize = e.NewPageSize;
                CreateList();
            }
        }

        protected void DG_PreRender(object sender, EventArgs e)
        {
            this.DG.MasterTableView.Attributes["bordercolor"] = "black";
            this.DG.MasterTableView.Attributes["border"] = "1";
            this.DG.MasterTableView.HeaderStyle.CssClass = "radgrid_header_def";
        }

        protected void DG_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Pager)
            {
                (e.Item.Cells[0].Controls[0] as Table).Rows[0].Visible = false;
            }
        }

        public void CreateList()
        {
            if (VsListMode != (byte)EnumListMode.HinmokuList)
            {
                btnDL.Enabled = true;
                BtnDelCheck.Enabled = false;
            }
            else
            {
                btnDL.Enabled = false;
                BtnDelCheck.Enabled = true;
            }
            int nDataCount = 0;
            SeisanDataSet.VIEW_SeihinKouseiCheckDataTable dt = SeisanClass.getVIEW_SeihinKouseiCheckDataTable(HM.Text,Global.GetConnection());
            this.DG.VirtualItemCount = nDataCount = dt.Count;
            this.DG.AllowCustomPaging = false;

            int nPageSize = this.DG.PageSize;
            int nPageCount = 0;
            if (nPageSize > 0)
            {
                DG.PageSize = nPageSize;
                DG.AllowPaging = true;
                nPageCount = nDataCount / nPageSize;
                if (0 < nDataCount % nPageSize) nPageCount++;
                if (nPageCount <= VsCurrentPageIndex)
                    VsCurrentPageIndex = 0;

                // 現在の表示行(何行目～何行目)
                int nStartCount = nPageSize * VsCurrentPageIndex + 1;
                int nEndCount = nStartCount + nPageSize - 1;
                if (nEndCount > nDataCount)
                    nEndCount = nDataCount;
            }
            else
            {
                DG.PageSize = nDataCount;
                DG.AllowPaging = false;
                VsCurrentPageIndex = 0;
            }
            this.DG.MasterTableView.CurrentPageIndex = VsCurrentPageIndex;

            DG.DataSource = dt;
            DG.DataBind();
        }

        protected void btnList_Click(object sender, EventArgs e)
        {
            VsListMode = (byte)EnumListMode.HinmokuList;
            this.DivTree.Visible = false;
            divKensaku.Visible = true;
            CreateList();

        }

        protected void DG_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "ref")
            {
                try
                {
                    string str = e.CommandArgument.ToString();

                    HM.SelectedValue = str;
                    VsListMode = (byte)EnumListMode.TreeList;
                    CreateTree(true);


                }
                catch (Exception ex)
                {
                    
                }
            }
        }

        protected void UpdateInsu(object sender, GridCommandEventArgs e)
        {
            divKensaku.Visible = true;

        }

        protected void btnUP_Click(object sender, EventArgs e)
        {
            divMain.Visible = false;
            DivListMain.Visible = false;
            divUpload.Visible = true;
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            divMain.Visible = true;
            DivListMain.Visible = true;
            divUpload.Visible = false;
        }

        protected void BtnUpReg_Click(object sender, EventArgs e)
        {
            CtlKouseiMasterUpload.getRegData();
        }

        protected void ChkSelect_CheckedChanged(object sender, EventArgs e)
        {
            int nIndex = int.Parse(this.Request.Params["__EVENTARGUMENT"]);
            CheckBox chk = DG.Items[nIndex].FindControl("ChkSelect") as CheckBox;
            string SeihinCode = DG.Items[nIndex].Cells[DG.MasterTableView.Columns.FindByUniqueName("SeihinCode").OrderIndex].Text;

            if (chk.Checked)
            {
                if (!VsCheck.Contains(SeihinCode))
                { VsCheck.Add(SeihinCode); }
            }
            else
            {
                if (VsCheck.Contains(SeihinCode))
                { VsCheck.Remove(SeihinCode); }
            }
        }

        protected void BtnDel_Click(object sender, EventArgs e)
        {
            if (VsListMode != (byte)EnumListMode.HinmokuList)
            {
                ShowMsg(LblMsg, "一覧画面から製品番号をチェックして削除してください。", false);
                return; 
            }
            string Key = "";

            for (int i = 0; i < DG.Items.Count; i++)
            {
                string strSeihinCode = DG.Items[i].Cells[DG.MasterTableView.Columns.FindByUniqueName("SeihinCode").OrderIndex].Text;

                CheckBox chkS = DG.Items[i].FindControl("ChkSelect") as CheckBox;

                if (chkS.Checked)
                {
                    if (Key != "")
                    { Key += ","; }
                    Key += strSeihinCode;
                }
            }

            if(Key == "")
            { 
                ShowMsg(LblMsg, "削除対象を未選択です。", false);
                return;
            }

            Core.Error ret = new Core.Error();

            //ret = JuchuClass.DeleteSimulation(Key, Global.GetConnection());
            ret = MasterClass.DeleteM_Kousei(Key, Global.GetConnection());
            if (null != ret)
            {
                this.ShowMsg(LblMsg,ret.Message, true);
                return;
            }

            CreateList();

            ShowMsg(LblMsg,"選択した部品表を削除しました。", false);

        }

        protected void Rtl_ItemDataBound(object sender, TreeListItemDataBoundEventArgs e)
        {
            if(e.Item.ItemType == TreeListItemType.HeaderItem)
            {
                e.Item.Cells[6].ColumnSpan = 2;
                e.Item.Cells[7].Visible = false;

            }
        }
        protected void UserKanri()
        {
            bool Admin = true;
            if (!Admin)
            {
                btnUpdate.Enabled = false;
                btnUP.Enabled = false;
                BtnDel.Enabled = false;
                BtnDelCheck.Enabled = false;
            }
        }
    }
}
