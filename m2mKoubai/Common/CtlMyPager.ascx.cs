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

namespace m2mKoubai.Common
{
    public partial class CtlMyPager : System.Web.UI.UserControl
    {
        public delegate void CtlMyPagerEventHandler(int nNewPageIndex);

        public event CtlMyPagerEventHandler OnPageIndexChanged = null;

        public int PageCount
        {
            get
            {
                return DdlPage.Items.Count;
            }
        }

        public int CurrentPageIndex
        {
            get
            {
                return Convert.ToInt32(this.ViewState["VsCurrentPageIndex"]);
            }
            set
            {
                if (0 <= value && value < DdlPage.Items.Count)
                {
                    this.ViewState["VsCurrentPageIndex"] = value;
                    DdlPage.SelectedIndex = value;
                }
            }
        }
        public string ClientEvent
        {
            get
            {
                return Convert.ToString(this.ViewState["ClientEvent"]);
            }
            set
            {
                this.ViewState["ClientEvent"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: この呼び出しは、ASP.NET Web フォーム デザイナで必要です
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		デザイナ サポートに必要なメソッドです- このメソッドの内容を
        ///		コード エディタで変更しないでください
        /// </summary>
        private void InitializeComponent()
        {
            this.PreRender += new System.EventHandler(this.CtlPager_PreRender);
        }

        public void Create(int nPageCount)
        {
            LitPageCount.Text = string.Format("{0:N0}", nPageCount);
            DdlPage.Items.Clear();
            for (int i = 1; i <= nPageCount; i++)
            {
                DdlPage.Items.Add(i.ToString());
            }
        }

        // Ddlクリア
        public void DdlClear()
        {
            this.DdlPage.Items.Clear();
        }

        public void SetItemCounter(int nStartCount, int nEndCount)
        {
            /*
            LitCounter.Text = "";
            this.BtnNext.Text ="次のページ";
            this.BtnPrev.Text ="前のページ";
            */

            if (nStartCount <= 0 || nEndCount <= 0 || nStartCount > nEndCount) return;

            if (nStartCount == nEndCount)
                LitCounter.Text = string.Format("（{0:N0}）", nEndCount);
            else
                LitCounter.Text = string.Format("（{0:N0}～{1:N0}）", nStartCount, nEndCount);
        }


        protected void BtnNext_Click(object sender, EventArgs e)
        {
            if (CurrentPageIndex < this.PageCount - 1)
            {
                CurrentPageIndex++;
                if (null != OnPageIndexChanged)
                    OnPageIndexChanged(CurrentPageIndex);
            }
        }

        protected void BtnPrev_Click(object sender, EventArgs e)
        {
            if (0 < CurrentPageIndex)
                this.CurrentPageIndex--;
            if (null != OnPageIndexChanged)
                OnPageIndexChanged(CurrentPageIndex);
        }

        protected void DdlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentPageIndex = this.DdlPage.SelectedIndex;
            if (null != OnPageIndexChanged)
                OnPageIndexChanged(CurrentPageIndex);
        }

        protected void CtlPager_PreRender(object sender, System.EventArgs e)
        {
            if (PageCount <= 1)
            {
                this.T.Visible = false;
            }
            else
            {
                this.T.Visible = true;
                BtnPrev.Enabled = (0 < this.CurrentPageIndex);
                BtnNext.Enabled = (CurrentPageIndex < this.PageCount - 1);
            }
            if (null != ClientEvent && "" != ClientEvent)
            {
                DdlPage.AutoPostBack = false;

                this.DdlPage.Attributes["onchange"] =
                    string.Format("{0}(this.selectedIndex);", this.ClientEvent);

                this.BtnPrev.Attributes["onclick"] = string.Format("{0}({1}); return false;",
                    this.ClientEvent, this.CurrentPageIndex - 1);

                this.BtnNext.Attributes["onclick"] = string.Format("{0}({1}); return false;",
                    this.ClientEvent, this.CurrentPageIndex + 1);
            }
            else
            {
                DdlPage.AutoPostBack = true;
            }
        }
    }
}