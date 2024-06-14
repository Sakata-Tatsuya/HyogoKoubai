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
using AdvBarCodeCtrl;
using System.Drawing.Imaging;
using System.Drawing;
namespace m2mKoubai.BarCode
{
    public partial class BarCodeForm : System.Web.UI.Page
    {
        private static BarCodeClass bc = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (1 == this.Request.QueryString.Count && "BarCode" == this.Request.QueryString.Keys[0])
            {
                string strValue = this.Request.QueryString[0];
                SetBarCode(strValue);
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: この呼び出しは、ASP.NET Web フォーム デザイナで必要です
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Designer サポートに必要なメソッドですコード エディタで
        /// このメソッドのコンテンツを変更しないでください
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion



        private void SetBarCode(string strValue)
        {
            if (null == bc)
            {
                try
                {
                    bc = new AdvBarCodeCtrl.BarCodeClass();
                }
                catch
                {
                    System.Threading.Thread.Sleep(200);
                    try
                    {
                        bc = new AdvBarCodeCtrl.BarCodeClass();
                    }
                    catch
                    {

                        return;
                    }
                }
                if (null == bc)
                {

                    return;
                }

                bc.Unit = AdvBarCodeCtrl.Units.Pixel;
                bc.Type = AdvBarCodeCtrl.Types.Code39;
                bc.CheckCharMode = false;
                bc.BarHeight = 30;
                bc.BarWidth = 120;
                bc.DrawMessage = false;
            }

            try
            {
                if (!System.Threading.Monitor.TryEnter(bc, 5000))
                {
                    throw new Exception("Monitor.TryEnter(bc, 5000)");
                }

                bc.Value = strValue;
                Bitmap bm = bc.GetBitmap();
                Bitmap bmt = new Bitmap(bm.Width, bm.Height - 8);
                Graphics g = Graphics.FromImage(bmt);
                Rectangle rect = new Rectangle(0, 8, bm.Width, bm.Height);
                g.DrawImage(bm, 0, 0, rect, GraphicsUnit.Pixel);

                Response.ContentType = "image/gif";
                bmt.Save(Response.OutputStream, ImageFormat.Gif);

                // リソースの解放（bm）
                bm.Dispose();
                bm = null;
            }
            catch
            {

                this.Response.Clear();
                return;
            }
            finally
            {
                System.Threading.Monitor.Exit(bc);
            }


        }




        /*
	
        private void SetBarCode(string strValue)
        {
            BarCodeClass bc = (BarCodeClass)this.Cache["BarCodeClass"];
			
            if (null == bc) {
                try 
                {
                    bc = new AdvBarCodeCtrl.BarCodeClass();
                }
                catch {
                    System.Threading.Thread.Sleep(500);
                    try 
                    {
                        bc = new AdvBarCodeCtrl.BarCodeClass();
                    }
                    catch (Exception ex){
                        Global.SendMail("BarCodeClassのインスタンス生成例外発生" + ex.Message);
                        return;
                    }
                }
				

                if (null == bc) {
                    Global.SendMail("BarCodeClassのインスタンス生成失敗");
                    return;
                }

                bc.Unit = AdvBarCodeCtrl.Units.Pixel;			
                bc.Type = AdvBarCodeCtrl.Types.Code39;
                bc.CheckCharMode = false;
                bc.BarHeight = 30;
                bc.BarWidth = 120;
                bc.DrawMessage = false;
                this.Cache["BarCodeClass"] = bc;
            }


            int nCount = 0;

            while (true) 
            {
                try 
                {
                    object obj = this.Cache["BarCodeClassUsing"];
                    if (null == obj) break;
                    bool bInUse = Convert.ToBoolean(this.Cache["BarCodeClassUsing"]);
                    if (!bInUse) 
                    {
                        break;
                    }
                    else 
                    {
                        nCount++;
                        if (nCount > 1000) 
                        {
                            // 無限ループの危険回避
                            Global.SendMail("BarCodeClassの無限ループ回避");
                            break;
                        }
                        System.Diagnostics.Debug.Write("*");
                    }
                }
                catch 
                {
                }

            }

            // トランザクションを考慮しないといけない？
            this.Cache["BarCodeClassUsing"] = true;	// これでバーコードオブジェクトのプロパティーがスレッドセーフになるのか？？？

            bc.Value = strValue;
            Bitmap bm = bc.GetBitmap();
            Bitmap bmt = new Bitmap(bm.Width, bm.Height - 8);
            Graphics g = Graphics.FromImage(bmt);
            Rectangle rect = new Rectangle(0, 8, bm.Width, bm.Height);
            g.DrawImage(bm, 0, 0, rect, GraphicsUnit.Pixel);
				
            Response.ContentType = "image/gif";
            bmt.Save(Response.OutputStream, ImageFormat.Gif);

            // リソースの解放（bm）
            bm.Dispose();
            bm = null;

            this.Cache["BarCodeClassUsing"] = false;
        }

        */

    }
}
