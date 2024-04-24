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
namespace m2mKoubai
{
    public partial class LoginForm : System.Web.UI.Page
    {
        //public const int G_CELL_DATE = 0;
        public const int G_CELL_MESSAGE = 0;

        m2mKoubaiDataSet.M_LoginMsgDataTable _dt = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Session.Clear();
                SessionManager.Logout();
                this.Create();
            }
        }

        // ���b�Z�[�W�\��
        private void ShowErrMsg(string strMsg)
        {
            LblErrMsg.Text = strMsg;
        }
        private void Create()
        {
            _dt = LoginMsgClass.getM_LoginMsgDataTable(Global.GetConnection());      
            if (_dt.Rows.Count > 0)
            {
                G.DataSource = _dt;
                G.DataBind();
                //G.Attributes.Add("bordercolor", "#00A0FF"); // ���F                
            }
            else
            {
                G.DataSource = new int[1];
                G.DataBind();
            }             
        }

        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (_dt.Rows.Count > 0)
                {
                    m2mKoubaiDataSet.M_LoginMsgRow dr =
                        (m2mKoubaiDataSet.M_LoginMsgRow)((DataRowView)e.Row.DataItem).Row;

                    //string date = dr.TourokuBi.ToString("yy/MM/dd<br/>HH:mm");
                    //e.Row.Cells[G_CELL_DATE].Text = date;
                    e.Row.Cells[G_CELL_MESSAGE].Text = dr.Msg.Replace("\r\n", "<br>");
                }
            }
        }

        protected void BtnTouroku_Click(object sender, EventArgs e)
        {
            string strId = TbxID.Text.Trim();
            string strPass = TbxPass.Text.Trim();

            if (strId == "")
            {
                this.ShowErrMsg("���O�C��ID����͂��ĉ�����");
                return;
            }
            if (strPass == "")
            {
                this.ShowErrMsg("�p�X���[�h����͂��ĉ�����");
                return;
            }
            // 
            // �F��
            m2mKoubaiDataSet.M_LoginRow dr = LoginClass.getM_LoginRow(strId, strPass, Global.GetConnection());

            if (dr == null)
            {
                this.ShowErrMsg("���O�C���ł��܂���ł���<br>���O�C��ID���̓p�X���[�h�����m���߉�����");
                return;
            }

            // �d����̏ꍇ
            if (dr.UserKubun == (byte)UserKubun.Shiiresaki)
            {         
                // ��Џ����擾
                LoginDataSet.V_Shiiresaki_FlgRow drShiire =
                    LoginClass.getV_Shiiresaki_FlgRow(strId, strPass, Global.GetConnection());
                // ��Џ�񂪑��݂���ꍇ
                if (drShiire != null)
                {
                    SessionManager.LoginShiiresaki(drShiire);
                }
                else
                {
                    // ���O�C���s��
                    this.ShowErrMsg("���O�C���ł��܂���ł���<br>���O�C��ID���̓p�X���[�h�����m���߉�����");
                    return;
                }
            }

            SessionManager.Login(dr,"ja");

            if (dr.UserKubun == (byte)UserKubun.Owner)
            {
                // ������
                this.Response.Redirect("~/Order/OrderInfoForm.aspx");
            }
            else            
            {
                // �d����                  
                this.Response.Redirect("~/Shiiresaki/OrderInfoForm.aspx");
            }
        }
    }
}
