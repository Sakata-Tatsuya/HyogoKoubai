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

namespace m2mKoubai.Denpyou
{
    public partial class GenpinhyouForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (SessionManager.UserKubun != (byte)UserKubun.Shiiresaki)
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return;
                }
                try
                {
                    // 検索条件をセットする
                    string strKey = HttpContext.Current.Request.Form["HidKey"];

                    // 納品書明細
                    this.Create(strKey);


                }
                catch
                {
                    ShowMsg(AppCommon.NO_DATA, true);
                    return;
                }
            }
        }
        private void Create(string strkey)
        {
            // 主キーによって、注文明細を取得
            ChumonDataSet.V_Chumon_MeisaiDataTable dt =
                ChumonClass.getV_Chumon_MeisaiDataTable(strkey, Global.GetConnection());
            if (dt.Rows.Count == 0)
            {
                this.ShowMsg(AppCommon.NO_DATA, true);
                return;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ChumonDataSet.V_Chumon_MeisaiRow dr =
                    dt.NewV_Chumon_MeisaiRow();
                dr.ItemArray = dt[i].ItemArray;
                
                CtlGenpinhyou c = LoadControl("CtlGenpinhyou.ascx") as CtlGenpinhyou;
                c.Create(dr);

                CtlA4 ctlA4 = LoadControl("CtlA4.ascx") as CtlA4;

                this.T.Rows[0].Cells[0].Controls.Add(ctlA4);

                ctlA4.Table.Rows[0].Cells[0].Controls.Add(c);
                if (i < dt.Rows.Count - 1)
                    ctlA4.Table.Style.Add("page-break-after", "always");
            }
        }             

        // メッセージ
        private void ShowMsg(string strMsg, bool bError)
        {
            this.LblMsg.Text = strMsg;
            this.LblMsg.ForeColor = (bError) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
        }
    }
}
