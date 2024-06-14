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
            // CODEGEN: ���̌Ăяo���́AASP.NET Web �t�H�[�� �f�U�C�i�ŕK�v�ł�
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Designer �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��R�[�h �G�f�B�^��
        /// ���̃��\�b�h�̃R���e���c��ύX���Ȃ��ł�������
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

                // ���\�[�X�̉���ibm�j
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
                        Global.SendMail("BarCodeClass�̃C���X�^���X������O����" + ex.Message);
                        return;
                    }
                }
				

                if (null == bc) {
                    Global.SendMail("BarCodeClass�̃C���X�^���X�������s");
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
                            // �������[�v�̊댯���
                            Global.SendMail("BarCodeClass�̖������[�v���");
                            break;
                        }
                        System.Diagnostics.Debug.Write("*");
                    }
                }
                catch 
                {
                }

            }

            // �g�����U�N�V�������l�����Ȃ��Ƃ����Ȃ��H
            this.Cache["BarCodeClassUsing"] = true;	// ����Ńo�[�R�[�h�I�u�W�F�N�g�̃v���p�e�B�[���X���b�h�Z�[�t�ɂȂ�̂��H�H�H

            bc.Value = strValue;
            Bitmap bm = bc.GetBitmap();
            Bitmap bmt = new Bitmap(bm.Width, bm.Height - 8);
            Graphics g = Graphics.FromImage(bmt);
            Rectangle rect = new Rectangle(0, 8, bm.Width, bm.Height);
            g.DrawImage(bm, 0, 0, rect, GraphicsUnit.Pixel);
				
            Response.ContentType = "image/gif";
            bmt.Save(Response.OutputStream, ImageFormat.Gif);

            // ���\�[�X�̉���ibm�j
            bm.Dispose();
            bm = null;

            this.Cache["BarCodeClassUsing"] = false;
        }

        */

    }
}
