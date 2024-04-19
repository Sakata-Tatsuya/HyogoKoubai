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

using Core.Type;

namespace m2mKoubai.Common
{
    public partial class CtlNengappiFromTo : System.Web.UI.UserControl
    {
        public Nengappi From
        {
            get
            {
                return new Nengappi((DateTime)RdpFrom.SelectedDate);
            }
            set
            {
                if (null != value)
                {
                    RdpFrom.SelectedDate = value.ToDateTime();
                }
            }
        }

        public Nengappi To
        {
            get
            {
                return new Nengappi((DateTime)RdpTo.SelectedDate);
            }
            set
            {
                if (null != value)
                {
                    RdpTo.SelectedDate = value.ToDateTime();
                }
            }
        }

        /// <summary>
        /// �J�����_�[�̋��L
        /// �y�[�W�T�C�Y�팸�̈�
        /// </summary>
        public Telerik.WebControls.RadCalendar SharedCalendar
        {
            set
            {
                this.RdpFrom.SharedCalendar = this.RdpTo.SharedCalendar = value;
            }
        }


        public Core.Type.NengappiKikan.EnumKikanType KikanType
        {
            get
            {
                return (Core.Type.NengappiKikan.EnumKikanType)this.DdlKikan.SelectedIndex;
            }
            set
            {
                this.DdlKikan.SelectedIndex = (int)value;
            }
        }

        public NengappiKikan.EnumKikanType NengappiKikanKikanType
        {
            get
            {
                switch (this.DdlKikan.SelectedIndex)
                {
                    case 0:
                        return NengappiKikan.EnumKikanType.NONE;
                    case 1:
                        return NengappiKikan.EnumKikanType.ONEDAY;
                    case 2:
                        return NengappiKikan.EnumKikanType.BEFORE;
                    case 3:
                        return NengappiKikan.EnumKikanType.AFTER;
                    case 4:
                        return NengappiKikan.EnumKikanType.FROM;
                }

                return NengappiKikan.EnumKikanType.NONE;

            }
        }

        public bool IsCreated
        {
            get
            {
                return Convert.ToBoolean(this.ViewState["IsCreated"]);
            }
            set
            {
                this.ViewState["IsCreated"] = value;
            }
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.Create();
            }
        }

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
            this.PreRender += new System.EventHandler(this.CtlNengappiFromTo_PreRender);
        }

        public void Create()
        {
            if (IsCreated) return;

            IsCreated = true;

            // ������ݒ肷��
            this.From = this.To = Nengappi.Now;

            DdlKikan.Items[0].Text = "�w��Ȃ�";
            DdlKikan.Items[1].Text = "�w���";
            DdlKikan.Items[2].Text = "�ȑO";
            DdlKikan.Items[3].Text = "�ȍ~";
            DdlKikan.Items[4].Text = "����";

            //this.RdpFrom.Culture =
            //    this.RdpTo.Culture = SessionManager.CurrentCultureInfo;

        }


        /*
        public void Create()
        {
            if (IsCreated) return;

            IsCreated = true;

            DateTime dtNow = DateTime.Now;

            // ������ݒ肷��
            this.From = this.To = Nengappi.Now;
        }
        */

        public void Create(Core.Type.NengappiKikan k)
        {
            this.Create();
            if (null != k)
            {
                if (null != k.From)
                    this.From = k.From;
                if (null != k.To)
                    this.To = k.To;
                this.KikanType = k.KikanType;
            }
        }


        public NengappiKikan GetNengappiKikan()
        {
            NengappiKikan k = new NengappiKikan();
            k.From = this.From;
            k.To = this.To;
            k.KikanType = this.KikanType;
            return k;
        }

        protected void CtlNengappiFromTo_PreRender(object sender, System.EventArgs e)
        {
            // ajax�Ŗ{�R���g���[�����N���C�A���g�Ƀ��[�h���ꂽ���A
            // HTML���ɏ�����javascript�����f����Ȃ���肪����A�܂��{�R���g���[���̕����g�p�ɂ��jvascript�֐��̏d�������Ȃ��悤��
            // javascript�̓^�O�̒��ɋL�q����悤�ɂ���
            string strDdlKikanOnChange =
                @"document.getElementById('{0}').style.display = (0 == this.selectedIndex)? 'none' : '';
                  document.getElementById('{1}').style.display = (4 == this.selectedIndex)? '' : 'none';";

            this.DdlKikan.Attributes["onchange"] =
                string.Format(strDdlKikanOnChange, this.TblFrom.ClientID, this.TblTo.ClientID);

            // ��ʂ̍X�V���A�ǂ����Ă�TblTo���\�������̂ŁA�ŏ��͕\�����Ȃ��悤�ɂ��Ă���
            if (4 != this.DdlKikan.SelectedIndex)
                TblTo.Style.Add("display", "none");
            else
                TblTo.Style.Add("display", "");

            if (0 == this.DdlKikan.SelectedIndex)
                TblFrom.Style.Add("display", "none");
            else
                TblFrom.Style.Add("display", "");

            //// �ǉ� 2007/3/31 by �I��
            //// 2007/1/1�͒�����Culture�ł�2007-1-1�����A
            //// ���񂱂��Őݒ肵�Ă��Ȃ��ƁApostback���ɕW���ݒ�ɖ߂��Ă��܂�
            //this.RdpFrom.Culture =
            //    this.RdpTo.Culture = SessionManager.CurrentCultureInfo;


            /* ���R�[�h
            this.DdlKikan.Attributes["onchange"] =
                string.Format("CtlNengappiFromTo_OnSelectKikan('{0}','{1}','{2}');",
                this.DdlKikan.ClientID, this.TblFrom.ClientID, this.TblTo.ClientID);

            string strScript = string.Format("<script language='javascript'>{0}</script>",
                this.DdlKikan.Attributes["onchange"]);

            this.Page.RegisterStartupScript(strScript, strScript);


            
            if (4 != this.DdlKikan.SelectedIndex)
                TblTo.Style.Add("display", "none");
            else
                TblTo.Style.Add("display", "");

            if (0 == this.DdlKikan.SelectedIndex)
                TblFrom.Style.Add("display", "none");
            else
                TblFrom.Style.Add("display", "");
            */

        }
    }
}