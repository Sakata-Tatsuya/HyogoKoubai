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
using KoubaiDAL;
using System.Drawing;
namespace Koubai.Shiiresaki
{
    public partial class MessegeForm : System.Web.UI.Page
    {
        private const int G_CELL_DATE = 0;
        private const int G_CELL_MSG = 1;
        private const int G_CELL_KAIFUU_DATE = 2;
        private const int G_CELL_BTN = 3;

        protected int loadFlg = 0;  // �\����window_reload�p

        // MsgID
        private int VsMsgID
        {
            get { return Convert.ToInt32(this.ViewState["VsMsgID"]); }
            set { this.ViewState["VsMsgID"] = value; }
        }
        /// <summary>
        /// ��L�[1
        /// </summary>
        private string VsYear
        {
            get { return Convert.ToString(this.ViewState["VsYear"]); }
            set { this.ViewState["VsYear"] = value; }
        }
        /// <summary>
        /// ��L�[2
        /// </summary>
        private string VsHacchuuNo
        {
            get { return Convert.ToString(this.ViewState["VsHacchuuNo"]); }
            set { this.ViewState["VsHacchuuNo"] = value; }
        }
        /// <summary>
        /// ��L�[3
        /// </summary>
        private int VsKubun
        {
            get { return Convert.ToInt32(this.ViewState["VsKubun"]); }
            set { this.ViewState["VsKubun"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Request.HttpMethod != "POST")
                {
                    HttpContext.Current.Response.Redirect(Global.LoginPageURL, false);
                    return;
                }

                try
                {

                    string[] strAry = HttpContext.Current.Request.Form["HidKey"].Split('\t');
                    if (strAry == null)
                    {
                        ShowMsg(AppCommon.NO_DATA, true);
                        // this.ShowTblList(false);
                        return;
                    }
                    // �����L�[���
                    ChumonClass.ChumonKey key =
                        new ChumonClass.ChumonKey(strAry[0]);

                    // ��L�[1
                    VsYear = key.Year;
                    // ��L�[2
                    VsHacchuuNo = key.HacchuuNo;
                    // ��L�[3
                    VsKubun = key.JigyoushoKubun;     

                }
                catch
                {
                    this.ShowMsg(AppCommon.NO_DATA, true);
                    return;
                }

                this.Create();
            }
            loadFlg = 0;
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
            // ����
            BtnC.Attributes["onclick"] = "Close(); return false;";
            // �o�^
            BtnT.Attributes["onclick"] = "Touroku(); return false;";
            BtnReg.Style.Add("display", "none");
            // �N���A
            BtnClear.Attributes["onclick"] = "Clear(); return false;";

        }

        private void Create()
        {
            //this.VsMsgID = 0;
            ChumonMsgDataSet.V_Chumon_MessageDataTable dt =
                ChumonMsgClass.getV_Chumon_MessageDataTable(VsYear, VsHacchuuNo, VsKubun, Global.GetConnection());
            if (dt.Rows.Count == 0)
            {
                G.Visible = false;
                return;
            }

            DataView dv = dt.DefaultView;
            G.DataSource = dt;
            G.DataBind();
            G.Visible = true;
        }

        // MsgRow�쐬
        private KoubaiDataSet.T_ChumonMsgRow CreateRow()
        {
            KoubaiDataSet.T_ChumonMsgRow dr = ChumonMsgClass.newT_ChumonMsgRow();
            // ��L�[
            dr.Year = VsYear;
            dr.HacchuuNo = VsHacchuuNo;
             dr.JigyoushoKubun = VsKubun;
            // ���[�U�[�敪
            dr.UserKubun = SessionManager.UserKubun;
            //
            dr.LoginID = SessionManager.LoginID;
            // ���b�Z�[�W
            dr.Message = this.TbxMsg.Text;

            // �J���t���O
            dr.OpenedFlg = false;
            // ���b�Z�[�W�o�^��
            dr.TourokuBi = DateTime.Now;


            // �X�V��(�ݒ�Ȃ�)
            // �J����(�ݒ�Ȃ�)  

            return dr;
        }

        private void ShowMsg(string strMsg, bool bError)
        {
            LblMsg.Text = strMsg;
            LblMsg.ForeColor = (bError) ? Color.Red : Color.Black;
        }

        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ChumonMsgDataSet.V_Chumon_MessageRow dr =
                   ((DataRowView)e.Row.DataItem).Row as ChumonMsgDataSet.V_Chumon_MessageRow;
                // �쐬��+���M�Җ�
                e.Row.Cells[G_CELL_DATE].Text = dr.TourokuBi.ToString("yy/MM/dd<br>HH:mm") + "<br>" + dr.Name;
                // ���b�Z�[�W
                e.Row.Cells[G_CELL_MSG].Text = dr.Message;
                // �J����+�J���Җ�
                if (!dr.IsOpenedDateNull())
                {
                    e.Row.Cells[G_CELL_KAIFUU_DATE].Text = dr.OpenedDate.ToString("yy/MM/dd<br>HH:mm") + "<br>" + dr.OpenedName;
                }

                Button btnK = e.Row.FindControl("BE") as Button;

                Button btnS = e.Row.FindControl("BD") as Button;

                bool bMyMsg = false;
                if (SessionManager.UserKubun == dr.UserKubun)
                    bMyMsg = true;
                // �ŏ��̃��b�Z�[�W�i���ŐV���b�Z�[�W�j
                btnK.CommandArgument = btnS.CommandArgument = dr.MsgID.ToString();
                if (bMyMsg)
                {
                    // �J����
                    if (dr.OpenedFlg)
                    {
                        btnK.Visible = false;
                        btnS.Visible = false;
                    }
                    else
                    {
                        btnK.Text = "�C��";
                        btnK.CommandName = "Shusei";

                        btnS.Text = "�폜";
                        btnS.CommandName = "Del";
                        btnS.Attributes["onclick"] = string.Format("return confirm('{0}');", "�폜���܂����H");
                    }
                }
                else
                {
                    // �J���ς�
                    if (dr.OpenedFlg)
                    {
                        btnK.Visible = false;
                    }
                    else
                    {
                        btnK.Text = "�J���ςɂ���";
                        btnK.CommandName = "Kaifuu";
                    }
                    btnS.Visible = false;
                }

            }
        }

        protected void BtnReg_Click(object sender, EventArgs e)
        {
            string msg = this.TbxMsg.Text.Trim();
            LibError err = null;
            if (VsMsgID == 0)
            {
                // �V�K�o�^
                KoubaiDataSet.T_ChumonMsgRow drNew = this.CreateRow();
                err = ChumonMsgClass.T_ChumonMsg_Insert(drNew, Global.GetConnection());
            }
            else
            {
                // �X�V
                err = ChumonMsgClass.T_ChumonMsg_Update(VsMsgID, msg, Global.GetConnection());
            }
            BtnT.Value = "�o�^";
            VsMsgID = 0;
            loadFlg = 1;
            if (err != null)
            {
                this.ShowMsg(err.Message, true);
            }
            else
            {
                this.ShowMsg("", true);
                this.Create();
            }
        }

        protected void G_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Shusei")
            {
                // �{�^����value���C���ɂ���

                VsMsgID = Convert.ToInt32(e.CommandArgument);
                KoubaiDataSet.T_ChumonMsgRow dr =
                    ChumonMsgClass.getT_ChumonMsgRow(VsMsgID, Global.GetConnection());
                if (dr == null)
                {
                    this.ShowMsg(AppCommon.NO_DATA, true);
                    return;
                }
                BtnT.Value = "�C��";
                this.TbxMsg.Text = dr.Message;


            }
            else if (e.CommandName == "Kaifuu")
            {
                int nMsgID = Convert.ToInt32(e.CommandArgument);

                LibError err = ChumonMsgClass.T_ChumonMsg_Kaifuu
                (SessionManager.UserKubun, SessionManager.LoginID, nMsgID, Global.GetConnection());
                if (err != null)
                {
                    this.ShowMsg(err.Message, true);
                }
                else
                {
                    this.Create();
                }

                loadFlg = 1;
            }
            else if (e.CommandName == "Del")
            {
                int nMsgID = Convert.ToInt32(e.CommandArgument);
                LibError err =
                    ChumonMsgClass.T_ChumonMsg_Delete(nMsgID, Global.GetConnection());
                if (err != null)
                {
                    this.ShowMsg(err.Message, true);
                }
                else
                {
                    this.Create();
                    // �o�^���邽�߁AVsMsgID��0�ɖ߂�
                    this.VsMsgID = 0;
                    // �{�^����value��o�^�ɂ���
                    BtnT.Value = "�o�^";

                }

                loadFlg = 1;
            }
        }
    }
}