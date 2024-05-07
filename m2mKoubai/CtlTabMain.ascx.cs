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

namespace m2mKoubai
{
    public partial class CtlTabMain : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // ���O�A�E�g                
                this.TabLO.Attributes["onclick"] =
                    string.Format("if(confirm('���O�A�E�g���܂����H'))location.href = '{0}';", Global.LoginPageURL);

                if (!SessionManager.KanrishaFlag)
                {
                    // �}�X�^�Ǘ�
                    this.Tab.Tabs[(int)MainMenu.Master].Visible = false;
                }
            }
        }

        // ���C�����j���[
        public enum MainMenu
        {
            // �������
            Hacchu_Jyouhou = 0,
            // ��������
            Hacchu_Nyuuryoku = 1,
            // �������
            Kenshu_Jyouhou = 2,
            // �[�i
            Nouhin = 3,
            // �p�X���[�h�ύX
            PassChange = 4,
            // �A�b�v���[�h
            Upload = 5,
            // �_�E�����[�h
            Download = 6,
            // �}�X�^�Ǘ�
            Master = 7,
            // ���O�A�E�g
            LogOut = 8,
        }
        // �}�X�^�Ǘ����j���[
        public enum Master
        {

            // �d����
            Shiiresaki = 0,
            // �S����
            Account = 1,
            // ���i
            Buhin = 2,
            // ���b�Z�[�W�o�^
            Message = 3,
            // �[���ꏊ
            NounyuBasho = 4,
            // ��Џ��
            KaishaJyouhou = 5,
        }
        //
        public enum Account
        {
            Shanai = 0,
            Shiiresaki = 1,
        }
        // ������񃁃j���[
        public enum Kenshu_Jyouhou
        {
            // �������ꗗ
            Yichiran = 0,
            // �������W�v
            Shukei = 1,
        }
        // �������̓��j���[ �ǉ� 09/07/24
        public enum Hacchu_Nyuuryoku
        {
            Single = 0,
            Multi = 1,
        }
        // ���C�����j���[
        public MainMenu Menu
        {
            get
            {
                return (MainMenu)this.Tab.SelectedIndex;
            }
            set
            {
                Tab.SelectedIndex = (int)value;
            }
        }
        // �}�X�^�Ǘ����j���[
        public Master MasterMenu
        {
            get
            {
                return (Master)this.Tab.Tabs[(int)MainMenu.Master].SelectedIndex;
            }
            set
            {
                Tab.Tabs[(int)MainMenu.Master].SelectedIndex = (int)value;
            }
        }
        // �A�J�E���g�̃T�u���j���[
        public Account AccoutMenu
        {
            get
            {
                return (Account)this.Tab.Tabs[(int)MainMenu.Master].Tabs[(int)Master.Account].SelectedIndex;
            }
            set
            {
                Tab.Tabs[(int)MainMenu.Master].Tabs[(int)Master.Account].SelectedIndex = (int)value;
            }
        }
        // �������̃T�u���j���[
        public Kenshu_Jyouhou KenshuMenu
        {
            get
            {
                return (Kenshu_Jyouhou)this.Tab.Tabs[(int)MainMenu.Kenshu_Jyouhou].SelectedIndex;
            }
            set
            {
                Tab.Tabs[(int)MainMenu.Kenshu_Jyouhou].SelectedIndex = (int)value;
            }
        }
        // �������͂̃T�u���j���[
        public Hacchu_Nyuuryoku Hacchu_NyuuryokuMenu
        {
            get
            {
                return (Hacchu_Nyuuryoku)this.Tab.Tabs[(int)MainMenu.Hacchu_Nyuuryoku].SelectedIndex;
            }
            set
            {
                Tab.Tabs[(int)MainMenu.Hacchu_Nyuuryoku].SelectedIndex = (int)value;
            }
        }

    }
}