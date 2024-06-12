using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using m2mKoubaiDAL;

namespace m2mKoubai.Common
{
    public partial class FileView : System.Web.UI.Page
    {
        //public static string GetQueryString(string strHinmokuBangou, string strShubetu, int nDataNo, bool bMitumori)
        //{
        //    ZumenHaifuClass.HinmokuBangouShubtuDataNo z = new ZumenHaifuClass.HinmokuBangouShubtuDataNo();
        //    z.strHinmokuBangou = strHinmokuBangou;
        //    z.strShubetu = strShubetu;
        //    z.nDataNo = nDataNo;
        //    return GetQueryString(new ZumenHaifuClass.HinmokuBangouShubtuDataNo[1] { z }, bMitumori);
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();

            try
            {
                string strArg = "";
                strArg = this.Request.Params["FileKey"];

                if (null == strArg)
                    throw new Exception("指定されたファイルが存在しません");

                ShareDataSet.T_DocumentRow dr = FilesClass.getT_DocumentRow(strArg, Global.GetConnection());

                if (dr != null)
                {
                    string strDispo = "inline;filename=\""+dr.FileName+"\"";
                    Response.Buffer = true;
                    Response.ContentType = dr.ContentType;
                    Response.AppendHeader("Content-Disposition", strDispo);
                    Response.AppendHeader("Content-Transfer-Encoding", "base64");
                    Response.OutputStream.Write(dr.Data, 0, dr.Data.Length);
                }

            }
            catch (Exception ex)
            {
                this.Response.Clear();
                this.Response.Write(ex.Message);
            }
            finally
            {
                Response.End();
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
        }




    }
}
