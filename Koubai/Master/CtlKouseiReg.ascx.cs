using Core;
using KoubaiDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace Koubai.Master
{
    public partial class CtlKouseiReg : System.Web.UI.UserControl
    {
        public bool bKoushin;

        private string vsCode
        {
            get
            {
                return this.ViewState["vsCode"].ToString();
            }
            set
            {
                this.ViewState["vsCode"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUser();
                createDdlList();
            }
        }

        protected string GetResource(string str)
        {
            return SessionManager.User.Honyaku(str);
        }

        private void LoadUser()
        {
            KoubaiDataSet.M_LoginDataTable dt = LoginClass.getM_LoginDataTable(Global.GetConnection());
        }

        internal Error GetData(out MasterClass.KouseiData data)
        {
            data = new MasterClass.KouseiData();
            Core.Error ret = null;

            data.strSeihinCode = tbxSeihinCode.Text.Trim();
            if ("" == data.strSeihinCode)
            { return new Core.Error(GetResource("製品番号を入力してください。")); }
            //if("" == data.strKouteiCode)
            //{
            //    return new Core.Error(GetResource("工程コードを入力してください。"));
            //}

            //1件でも部門コードが同じデータが返ってきた場合登録できない
            //更新FLGがtrueの場合はここはスルーする
            MasterDataSet.M_KouseiRow dr = MasterClass.getM_KouseiRow(tbxSeihinCode.Text.Trim(), tbxBuhinCode.Text.Trim(), Global.GetConnection());
            if (null != dr && bKoushin == false) return new Core.Error(GetResource("製品番号と部品番号の組み合わせが重複している為、登録できません。"));

            data.strSeihinCode = tbxSeihinCode.Text.Trim();

            data.strBuhinCode = tbxBuhinCode.Text.Trim();

            data.strKouteiCode = ltlKouteiCodeN.Text.Trim();

            data.strHinmokuBunruiCode = ltlHinmokuBunruiCodeN.Text.Trim();
            int insu = 0;
           int.TryParse(tbxInsu.Text.Trim(),out insu);
            data.nInsu = insu;
            data.strSyounin = TbxSyounin.Text.Trim();

            data.strTantou = TbxTantou.Text.Trim();

            data.ORNo = HidORNo.Value;

            if (HidORNo.Value != "")
            {
                for (int i = 0; ChkbORNo.Items.Count > i; i++)
                {
                    if (ChkbORNo.Items[i].Text == data.strBuhinCode)
                    { data.bOR = ChkbORNo.Items[i].Selected; }
                    else
                    {
                        data.ORBuhin.Add(ChkbORNo.Items[i].Text);
                        data.ORBuhinBool.Add(ChkbORNo.Items[i].Selected);
                    }
                }
            }

            data.strBikou1 = TbxBikou1.Text;
            data.strBikou2 = TbxBikou2.Text;


            ret = S.GetData();
            if (null != ret) return ret;

            return null;
        }

        internal void setDataClear()
        {
            tbxSeihinCode.Enabled = true;
            tbxSeihinCode.ReadOnly = false;
            tbxSeihinCode.Text = string.Empty;
            tbxBuhinCode.Enabled = true;
            tbxBuhinCode.ReadOnly = false;
            tbxBuhinCode.Text = string.Empty;
            ltlKouteiCodeN.Text = string.Empty;
            ltlKouteiCodeN.Text = string.Empty;
            ltlHinmokuBunruiCodeN.Text = string.Empty;
            ltlHinmokuBunruiCodeN.Text = string.Empty;
            TbxSyounin.Text = string.Empty;
            TbxSyounin.Enabled = true;
            TbxSyounin.ReadOnly = false;
            TbxTantou.Text = string.Empty;
            TbxTantou.Enabled = true;
            TbxTantou.ReadOnly = false;
            tbxInsu.Text = string.Empty;
            LtlDeleteFlag.Text = "新規作成";
            ltlSeihinCode.Text = string.Empty;
            ltlBuhinCode.Text = string.Empty;
            ltlKouteiCode.Text = string.Empty;
            ltlkouteiMei.Text = string.Empty;
            ltlHinmokuBunruiCode.Text = string.Empty;
            ltlHinmokuBunrui.Text = string.Empty;
            ltlHinmokuCode.Text = string.Empty;
            ltlHinmei.Text = string.Empty;
            ltlInsu.Text = string.Empty;
            ltlSyounin.Text = string.Empty;
            ltltantou.Text = string.Empty;
            ChkbORNo.Items.Clear();
            ltlOR.Text = string.Empty;
            ltlTourokuDate.Text = string.Empty;

            ltlBikou1.Text = string.Empty;
            ltlBikou2.Text = string.Empty;
            TbxBikou1.Text = string.Empty;
            TbxBikou2.Text = string.Empty;

            //ltlYajirusi.Text = "";
            //updhoge.Style.Add("display","none");
            //updhead.Style.Add("display", "none");
        }

        public Core.Error Create(string seihinCode, string buhinCode)
        {
            vsCode = seihinCode;

            MasterDataSet.M_KouseiRow dr =
                MasterClass.getM_KouseiRow(seihinCode, buhinCode, Global.GetConnection());
            if (null == dr) return new Core.Error(GetResource("該当のデータはありません。"));

            try
            {
                MasterDataSet.M_BuhinRow dr_b;
                dr_b = MasterClass.getM_BuhinRow(buhinCode, Global.GetConnection());
                if (dr_b!=null)
                {
                    ltlBuhinMei.Text = dr_b.BuhinMei.ToString();
                }
              
            }
            catch (Exception e)
            { ltlBuhinMei.Text = ""; }

            if (!dr.IsTourokuDateTimeNull())
            { ltlTourokuDate.Text = dr.TourokuDateTime.ToString("yyyy/MM/dd"); }

            ltlTourokuDateT.Text = DateTime.Now.ToString("yyyy/MM/dd");

            if (!dr.IsORNoNull() && dr.ORNo != "")
            {
                HidORNo.Value = dr.ORNo;

                MasterDataSet.M_KouseiDataTable dtOR =
                    MasterClass.getM_KouseiDataTable(seihinCode,dr.ORNo, Global.GetConnection());

                string chkMSG = "";

                for (int i = 0; dtOR.Count > i; i++)
                {
                    if(chkMSG != "")
                    { chkMSG += "<br/>"; }

                    ChkbORNo.Items.Add(dtOR[i].BuhinCode);
                    if (!dtOR[i].IsSelectORNull())
                    {
                        ChkbORNo.Items[i].Selected = dtOR[i].SelectOR;

                        if(dtOR[i].SelectOR)
                        { chkMSG += "☑：" + dtOR[i].BuhinCode; }
                        else
                        { chkMSG += "□：" + dtOR[i].BuhinCode; }
                    }
                    else
                    { chkMSG += "□：" + dtOR[i].BuhinCode; }
                }

                ltlOR.Text = chkMSG;
            }

            tbxSeihinCode.Text = dr.SeihinCode;
            tbxSeihinCode.Enabled = true;
            tbxSeihinCode.ReadOnly = true;//dr.DeleteFlag;
            tbxBuhinCode.Text = dr.BuhinCode;
            tbxBuhinCode.Enabled = true;
            tbxBuhinCode.ReadOnly = dr.DeleteFlag;
            ltlKouteiCodeN.Text = dr.KouteiCode;
            ltlHinmokuBunruiCodeN.Text = dr.HinmokuBunruiCode;
            ltlKouteiCodeN.Text += dr.KouteiCode;
            tbxInsu.Text = dr.Insu.ToString();
            tbxInsu.Enabled = true;
            tbxInsu.ReadOnly = dr.DeleteFlag;
            TbxSyounin.Text = "";
            TbxSyounin.Enabled = true;
            TbxSyounin.ReadOnly = false;
            TbxTantou.Text = "";
            TbxTantou.Enabled = true;
            TbxTantou.ReadOnly = false;
            LtlDeleteFlag.Text = dr.DeleteFlag ? "削除済み" : "有効";
            ltlSeihinCode.Text = dr.SeihinCode;
            ltlBuhinCode.Text = dr.BuhinCode;
            ltlKouteiCode.Text = dr.KouteiCode;
            ltlkouteiMei.Text = "";

            if (!dr.IsHinmokuBunruiCodeNull())
            { ltlHinmokuBunruiCode.Text = dr.HinmokuBunruiCode; }
            ltlHinmokuBunrui.Text = "";
            ltlHinmokuCode.Text = "";
            ltlHinmei.Text = "";
            ltlInsu.Text = dr.Insu.ToString();
            ltlSyounin.Text = " ";
            ltltantou.Text = " ";

            if (!dr.IsBikou1Null())
            { TbxBikou1.Text = ltlBikou1.Text = dr.Bikou1; }

            if (!dr.IsBikou2Null())
            { TbxBikou2.Text = ltlBikou2.Text = dr.Bikou2; }
            return null;
        }
        private void createDdlList()
        {
            return;
        }

        protected void GV_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}