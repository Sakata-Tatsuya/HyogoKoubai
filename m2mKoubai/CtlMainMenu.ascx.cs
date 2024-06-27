using System;
using System.Drawing;
using m2mKoubaiDAL;
using System.IO;
using System.Security.Cryptography;

namespace m2mKoubai
{
    public partial class CtlMainMenu : System.Web.UI.UserControl
    {
        protected string passWord = "12345678";
        protected string passWord_encryption = "12345678";

        [Serializable]
        public class OptionMenu
        {
            public string strName = "";
            public string strValue = "";

            public string NavigateURL
            {
                get;
                set;
            }

            public System.Collections.Generic.List<OptionMenu> SubMenu = new System.Collections.Generic.List<OptionMenu>();

            public void AddSubMenu(string strName, string strValue)
            {
                SubMenu.Add(new OptionMenu(strName, strValue));
            }
            public void AddSubMenu(string strName, string strValue, string strNavigateURL)
            {
                SubMenu.Add(new OptionMenu(strName, strValue, strNavigateURL));
            }
            public OptionMenu()
            {

            }

            public OptionMenu SelectedOptionMenu
            {
                get
                {
                    return SubMenu[SelectedIndex];
                }
            }
            public int SelectedIndex
            {
                get;
                set;
            }

            public OptionMenu this[string strValue]
            {
                get
                {
                    for (int i = 0; i < SubMenu.Count; i++)
                    {
                        if (SubMenu[i].strValue == strValue)
                            return SubMenu[i];
                    }
                    return null;
                }
            }
            public OptionMenu(string strName, string strValue)
            {
                this.strName = strName;
                this.strValue = strValue;
            }

            public OptionMenu(string strName, string strValue, string strNavigateURL)
            {
                this.strName = strName;
                this.strValue = strValue;
                this.NavigateURL = strNavigateURL;
            }
        }
        [Serializable]
        public class OptionMenuCollection : System.Collections.Generic.List<OptionMenu>
        {
            public int SelectedIndex
            {
                get;
                set;
            }

            public OptionMenu this[string strValue]
            {
                get
                {
                    for (int i = 0; i < this.Count; i++)
                    {
                        if (this[i].strValue == strValue)
                            return this[i];
                    }
                    return null;
                }
            }
        }

        public string MenuName
        {
            get
            {
                return this.LblMenu.Text;
            }
            set
            {
                this.LblMenu.Text = value;
            }
        }

        public OptionMenuCollection OptionMenus
        {
            get
            {
                object obj = this.ViewState["VsOptionMenu"];
                if (null == obj)
                {
                    obj = new OptionMenuCollection();
                    this.ViewState["VsOptionMenu"] = obj;
                }
                return obj as OptionMenuCollection;
            }
            set
            {
                this.ViewState["VsOptionMenu"] = value;
            }
        }

        private Telerik.WebControls.RadMenuItem
            FindItemByValue(Telerik.WebControls.RadMenu m, string strValue)
        {
            for (int i = 0; i < m.Items.Count; i++)
            {
                if (m.Items[i].Value.Equals(strValue))
                    return m.Items[i];
                if (0 < m.Items[i].Items.Count)
                {
                    Telerik.WebControls.RadMenuItem f = FindItemByValue(m.Items[i], strValue);
                    if (null != f) return f;
                }
            }
            return null;
        }

        private Telerik.WebControls.RadMenuItem
            FindItemByValue(Telerik.WebControls.RadMenuItem parent, string strValue)
        {
            for (int i = 0; i < parent.Items.Count; i++)
            {
                if (parent.Items[i].Value.Equals(strValue))
                    return parent.Items[i];
                if (0 < parent.Items[i].Items.Count)
                {
                    Telerik.WebControls.RadMenuItem f = FindItemByValue(parent.Items[i], strValue);
                    if (null != f) return f;
                }
            }
            return null;
        }
        protected string GetResource(string str)
        {
            return SessionManager.User.Honyaku(str);
        }

        private void SetMenuResource(Telerik.WebControls.RadMenuItem item)
        {
            item.Text = GetResource(item.Text);
            for (int i = 0; i < item.Items.Count; i++)
            {
                item.Items[i].Text = GetResource(item.Items[i].Text);
                for (int c = 0; c < item.Items[i].Items.Count; c++)
                {
                    SetMenuResource(item.Items[i].Items[c]);
                }
            }
        }

        private void SetMenuResource(Telerik.WebControls.RadMenu m)
        {
            for (int i = 0; i < m.Items.Count; i++)
            {
                m.Items[i].Text = GetResource(m.Items[i].Text);
                for (int c = 0; c < m.Items[i].Items.Count; c++)
                {
                    SetMenuResource(m.Items[i].Items[c]);
                }
            }
        }

        public void ClearOptionMenu()
        {
            this.OptionMenus = null;
        }

        public delegate void EventHandler(OptionMenu m);

        //public event EventHandler OnOptionMenuSelected = null;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //// メインカラー変更
            //string sfactory = "";
            //if (System.Configuration.ConfigurationManager.AppSettings["factory"] != null)
            //{
            //    sfactory = System.Configuration.ConfigurationManager.AppSettings["factory"];
            //}

            //if (sfactory == "E1")
            //{
            //    TblColor.Style["background-color"] = "#040581";
            //    TblColor.Style["border-color"] = "#040581";
            //}
            //else if (sfactory == "E2")
            //{
            //    TblColor.Style["background-color"] = "#048105";
            //    TblColor.Style["border-color"] = "#048105";
            //}
            //else if (sfactory == "UP")
            //{
            //    TblColor.Style["background-color"] = "#050481";
            //    TblColor.Style["border-color"] = "#050481";
            //}

            if (!this.IsPostBack)
            {
                switch (SessionManager.UserKubun)
                {
                    case (byte)UserKubun.Owner:
                        this.RadMenuItemS1.Visible = false;
                        this.RadMenuItemS2.Visible = false;
                        this.RadMenuItemSM1.Visible = false;
                        this.RadMenuItemSM2.Visible = false;
                        break;
                    case (byte)UserKubun.Shiiresaki:
                        this.RadMenuItemO1.Visible = false;
                        this.RadMenuItemO2.Visible = false;
                        this.RadMenuItemO3.Visible = false;
                        this.RadMenuItemO4.Visible = false;
                        this.RadMenuItemO5.Visible = false;
                        this.RadMenuItemO6.Visible = false;
                        this.RadMenuItemOM.Visible = false;
                        break;
                }
                this.Create();
            }
        }

        private Telerik.Web.UI.RadMenuItem
            FindItemByValue(Telerik.Web.UI.RadMenu m, string strValue)
        {
            for (int i = 0; i < m.Items.Count; i++)
            {
                if (m.Items[i].Value.Equals(strValue))
                    return m.Items[i];
                if (0 < m.Items[i].Items.Count)
                {
                    Telerik.Web.UI.RadMenuItem f = FindItemByValue(m.Items[i], strValue);
                    if (null != f) return f;
                }
            }
            return null;
        }

        private Telerik.Web.UI.RadMenuItem
            FindItemByValue(Telerik.Web.UI.RadMenuItem parent, string strValue)
        {
            for (int i = 0; i < parent.Items.Count; i++)
            {
                if (parent.Items[i].Value.Equals(strValue))
                    return parent.Items[i];
                if (0 < parent.Items[i].Items.Count)
                {
                    Telerik.Web.UI.RadMenuItem f = FindItemByValue(parent.Items[i], strValue);
                    if (null != f) return f;
                }
            }
            return null;
        }

        private void SetMenuResource(Telerik.Web.UI.RadMenuItem item)
        {
            item.Text = item.Text;
            for (int i = 0; i < item.Items.Count; i++)
            {
                item.Items[i].Text = item.Items[i].Text;
                for (int c = 0; c < item.Items[i].Items.Count; c++)
                {
                    SetMenuResource(item.Items[i].Items[c]);
                }
            }
        }

        private void SetMenuResource(Telerik.Web.UI.RadMenu m)
        {
            for (int i = 0; i < m.Items.Count; i++)
            {
                m.Items[i].Text = m.Items[i].Text;
                for (int c = 0; c < m.Items[i].Items.Count; c++)
                {
                    SetMenuResource(m.Items[i].Items[c]);
                }
            }
        }

        private void InitializeResource()
        {
        }

        public void Create()
        {
            //MasterDataSet.M_UserRow drUser = SessionManager.User.M_User;
            m2mKoubaiDataSet.M_LoginRow drUser = LoginClass.getM_LoginRow(SessionManager.LoginID, Global.GetConnection());
            this.LblLoginUser.Text = drUser.Name;

            // 現在表示しているページを探す
            //Telerik.Web.UI.RadMenuItem found = null;
            string strPage = "~" + this.Request.Url.PathAndQuery.Substring(this.Request.ApplicationPath.Length);
            System.Collections.ArrayList lstMenuTree = new System.Collections.ArrayList();
            // lstMenuTreeはメニューが見つかったと時のリンクしているメニューを保持する。(順番は逆に入っている)
        }

        private bool FindMenu(Telerik.Web.UI.RadMenuItem parent, string strUrl, System.Collections.ArrayList lstMenuTree, ref Telerik.Web.UI.RadMenuItem found)
        {
            if (parent.NavigateUrl == strUrl)
            {
                found = parent;
                lstMenuTree.Add(parent);
                return true;
            }

            for (int i = 0; i < parent.Items.Count; i++)
            {
                bool bFound = FindMenu(parent.Items[i], strUrl, lstMenuTree, ref found);
                if (bFound)
                {
                    lstMenuTree.Add(parent);
                    return true;
                }
            }

            return false;
        }

        protected void N_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
        {

        }
    }
}