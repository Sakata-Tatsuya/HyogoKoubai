using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Core.Web;
using KoubaiDAL;

namespace Koubai.Common
{
    public partial class CtlItemEdit : HonyakuUserControl
    {
        //追加
        private const int CELL_HYOJI = 0;
        private const int CELL_HYOUJIJYUN = 1;

        private const int CELL_CAPTION = 2;
        private const int CELL_KETUGOU = 3;
        private const int CELL_FIELD = 4;

        public delegate Core.Error Save(string strUserViewData);
        public Save OnSave = null;

        public delegate void ShowDefault();
        public ShowDefault OnShowDefault = null;

        public delegate void Back();
        public Back OnBack = null;

        private string VsUserID
        {
            get
            {
                return (string)this.ViewState["VsUserID"];
            }
            set
            {
                this.ViewState["VsUserID"] = value;
            }
        }

        private int VsListID
        {
            get
            {
                return (int)this.ViewState["VsListID"];
            }
            set
            {
                this.ViewState["VsListID"] = value;
            }
        }

        public Button BackButton
        {
            get
            {
                return this.BtnBack;
            }
        }

        public bool ShowSortSetting
        {
            get
            {
                object obj = this.ViewState["ShowSortSetting"];
                if (null == obj) return false;
                return Convert.ToBoolean(obj);
            }
            set
            {
                this.ViewState["ShowSortSetting"] = value;
            }
        }

        public Button DefaultButton
        {
            get
            {
                return BtnDefault;
            }
        }

        public Button ResetButton
        {
            get
            {
                return BtnReset;
            }
        }

        protected string S(string str)
        {
            return SessionManager.User.Honyaku(str);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.BtnReset.Text = S("リセット");
            this.BtnCombine.Text = S("選択した列を結合");
            this.LblMsg.Text = "";
            D.Attributes["bordercolor"] = "black";
            //ST.CtlSortList.OnSort = new Common.CtlSortList.Sort(this.OnSort);
        }

        private void OnSort(string strSortExpression)
        {
            //ST.CtlSortList.Create(this.VsListID);
        }

        public bool TextFormatMode
        {
            get
            {
                object obj = this.ViewState["TextFormatMode"];
                if (null == obj) return false;
                return (bool)obj;
            }
            set
            {
                this.ViewState["TextFormatMode"] = value;
                // 結合
                this.D.Columns[CELL_KETUGOU].Visible = !value;
                this.BtnCombine.Visible = !value;
            }
        }

        private void Create(int nListID, string strUserID)
        {
            this.Create(nListID, strUserID, false);
        }

        public void Create(int nListID, string strUserID, bool bShowDefault)
        {
            this.VsListID = nListID;
            this.VsUserID = strUserID;
            if (bShowDefault) strUserID = "";
            UserViewManager.UserView v = UserViewManager.UserView.New(nListID, strUserID, true);
            Create(v);
        }

        public void Create(UserViewManager.UserView v)
        {
            //ST.CtlSortList.Create(v.GetVisibleDataFields().ToArray(), v.SortExpression);

            System.Collections.Generic.List<UserViewManager.GridColumn> lst = v.GridColumns;

            System.Collections.Generic.List<UserViewManager.EditGridColumn> lstEditGridColumn = new System.Collections.Generic.List<UserViewManager.EditGridColumn>();

            for (int i = 0; i < lst.Count; i++)
            {
                UserViewManager.GridColumn gc = lst[i];
                UserViewManager.EditGridColumn d = new UserViewManager.EditGridColumn();
                for (int t = 0; t < gc.DataFields.Count; t++)
                {
                    d.EditDataFields.Add(gc.DataFields[t].EditDataField);
                }
                lstEditGridColumn.Add(d);
            }

            this.Create(lstEditGridColumn);
        }

        private void Create(System.Collections.Generic.List<UserViewManager.EditGridColumn> lst)
        {
            this.D.DataSource = new int[lst.Count];

            this.D.DataBind();

            System.Collections.Generic.List<UserViewManager.EditDataField> lstSort = new System.Collections.Generic.List<UserViewManager.EditDataField>();

            int nVisibleCount = 0;

            for (int i = 0; i < lst.Count; i++)
            {
                UserViewManager.EditGridColumn gc = lst[i];
                DataGridItem item = D.Items[i];
                item.Cells[CELL_CAPTION].CssClass = "";

                // 表示チェック
                System.Web.UI.HtmlControls.HtmlInputCheckBox chk =
                    item.FindControl("V") as System.Web.UI.HtmlControls.HtmlInputCheckBox;
                chk.Disabled = false;

                Button btnKaijo = item.FindControl("BK") as Button;
                btnKaijo.Text = S("結合解除");
                System.Web.UI.HtmlControls.HtmlInputCheckBox chkKetugou =
                    item.FindControl("K") as System.Web.UI.HtmlControls.HtmlInputCheckBox;
                btnKaijo.Visible = false;
                chkKetugou.Visible = false;

                DataGrid c = item.FindControl("C") as DataGrid;
                Literal litCaption = item.FindControl("L") as Literal;
                c.Visible = false;

                if (1 == gc.EditDataFields.Count)
                {
                    UserViewManager.EditDataField df = gc.EditDataFields[0];
                    if (df.Visible) lstSort.Add(df);
                    chk.Checked = df.Visible;
                    item.Cells[CELL_FIELD].Text = df.FieldName;
                    c.Visible = false;
                    litCaption.Text = S(df.Caption);
                    item.Attributes["def_caption"] = df.Caption;
                    item.Cells[CELL_FIELD].Text = df.FieldName;
                    chkKetugou.Visible = true;
                    chkKetugou.Disabled = !(df.ColumnType == Core.Sql.EnumColumnType.DBField ||
                        df.ColumnType == Core.Sql.EnumColumnType.UserField);   // DBﾌｨｰﾙﾄﾞ or ユーザﾌｨｰﾙﾄﾞの場合は結合可能
                    if (chkKetugou.Disabled)
                    {
                        chkKetugou.Attributes["title"] = S("この項目を結合することはできません。");
                    }

                    item.Attributes["ColumnType"] = ((int)df.ColumnType).ToString();

                    if (df.ColumnType == Core.Sql.EnumColumnType.RadGridColumn || df.ForceVisible)
                    {
                        chk.Disabled = true;    // 規定の列は非表示にできない
                        chk.Attributes["title"] = S("この項目は非表示にできません。");
                    }

                    chk.Checked = df.Visible;
                    if (df.Visible) nVisibleCount++;
                    if (!df.Visible) item.BackColor = Color.LightGray;
                }
                else
                {
                    // 結合の列があるとき(この時は常に表示)
                    chk.Checked = true;
                    nVisibleCount++;
                    item.Cells[CELL_CAPTION].CssClass = "fit";
                    c.Visible = true;
                    string[] strValues = new string[gc.EditDataFields.Count];
                    c.DataSource = new int[gc.EditDataFields.Count];
                    c.DataBind();
                    c.Attributes["bordercolor"] = "black";
                    for (int t = 0; t < gc.EditDataFields.Count; t++)
                    {
                        UserViewManager.EditDataField col = gc.EditDataFields[t];
                        lstSort.Add(col);
                        c.Items[t].Attributes["def_caption"] = col.Caption;
                        c.Items[t].Attributes["ColumnType"] = ((int)col.ColumnType).ToString();
                        c.Items[t].Cells[1].Text = S(col.Caption);
                        c.Items[t].Cells[2].Text = col.FieldName;

                        // 表示順
                        DropDownList ddl = c.Items[t].FindControl("B") as DropDownList;
                        ddl.Items.Clear();
                        for (int h = 0; h < gc.EditDataFields.Count; h++)
                        {
                            ddl.Items.Add((1 + h).ToString());
                        }
                        ddl.SelectedIndex = t;
                        ddl.Attributes["onchange"] = string.Format("javascript:__doPostBack('{0}', 'order_child:{1},{2}');", this.BtnCommand.UniqueID, i, t);
                    }
                    btnKaijo.Visible = true;
                }
            }

            // 表示順の設定
            int nVisibleOrderIndex = 0;
            for (int i = 0; i < lst.Count; i++)
            {
                DataGridItem item = D.Items[i];
                // 表示チェック
                System.Web.UI.HtmlControls.HtmlInputCheckBox chk =
                    item.FindControl("V") as System.Web.UI.HtmlControls.HtmlInputCheckBox;
                // 表示順
                DropDownList ddlOrder = item.FindControl("A") as DropDownList;
                ddlOrder.Items.Clear();

                ddlOrder.Visible = chk.Checked;

                if (!chk.Checked) continue;

                for (int t = 0; t < nVisibleCount; t++)
                {
                    ddlOrder.Items.Add((1 + t).ToString());
                }
                ddlOrder.SelectedIndex = nVisibleOrderIndex++;
                ddlOrder.Attributes["onchange"] = string.Format("javascript:__doPostBack('{0}', 'order:{1}');", this.BtnCommand.UniqueID, i);
            }

            // ソート
            //ST.CtlSortList.Create(lstSort.ToArray(), ST.CtlSortList.GetSort());
        }

        private UserViewManager.EditGridColumn GetEditGridColumn(int nIndex)
        {
            DataGridItem item = D.Items[nIndex];
            DataGrid c = item.FindControl("C") as DataGrid;
            Literal litCaption = item.FindControl("L") as Literal;

            UserViewManager.EditGridColumn g = new UserViewManager.EditGridColumn();

            System.Web.UI.HtmlControls.HtmlInputCheckBox chk =
                item.FindControl("V") as System.Web.UI.HtmlControls.HtmlInputCheckBox;

            if (c.Visible && 0 < c.Items.Count)
            {
                // 結合列の場合
                for (int t = 0; t < c.Items.Count; t++)
                {
                    UserViewManager.EditDataField df = new UserViewManager.EditDataField();
                    df.Caption = c.Items[t].Attributes["def_caption"];
                    df.FieldName = c.Items[t].Cells[2].Text;
                    df.ColumnType = (Core.Sql.EnumColumnType)int.Parse(c.Items[t].Attributes["ColumnType"]);
                    df.Visible = chk.Checked;
                    g.EditDataFields.Add(df);
                }
            }
            else
            {
                UserViewManager.EditDataField df = new UserViewManager.EditDataField();
                df.FieldName = item.Cells[CELL_FIELD].Text;
                df.Caption = item.Attributes["def_caption"];
                df.ColumnType = (Core.Sql.EnumColumnType)int.Parse(item.Attributes["ColumnType"]);
                df.Visible = chk.Checked;
                g.EditDataFields.Add(df);
            }

            return g;
        }

        private System.Collections.Generic.List<UserViewManager.EditGridColumn> GetData()
        {
            System.Collections.Generic.List<UserViewManager.EditGridColumn> lst = new System.Collections.Generic.List<UserViewManager.EditGridColumn>();

            System.Collections.Generic.List<UserViewManager.EditGridColumn> lstNotSelected = new System.Collections.Generic.List<UserViewManager.EditGridColumn>();

            for (int i = 0; i < D.Items.Count; i++)
            {
                UserViewManager.EditGridColumn m = this.GetEditGridColumn(i);
                if (m.EditDataFields[0].Visible)
                {
                    lst.Add(m);
                }
                else
                {
                    // 非選択の場合、結合していれば解除する。
                    for (int t = 0; t < m.EditDataFields.Count; t++)
                    {
                        UserViewManager.EditGridColumn g = new UserViewManager.EditGridColumn();
                        g.EditDataFields.Add(m.EditDataFields[t]);
                        lstNotSelected.Add(g);
                    }
                }
            }

            // 非選択分は最後に追加する。
            for (int i = 0; i < lstNotSelected.Count; i++)
                lst.Add(lstNotSelected[i]);

            return lst;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            for (int i = 0; i < D.Items.Count; i++)
            {
                System.Web.UI.HtmlControls.HtmlInputCheckBox chk =
                    D.Items[i].FindControl("V") as System.Web.UI.HtmlControls.HtmlInputCheckBox;
                chk.Attributes["onclick"] = string.Format("javascript:__doPostBack('{0}', 'view:{1}');", this.BtnCommand.UniqueID, i);

                Button btnKaijo = D.Items[i].FindControl("BK") as Button;
                btnKaijo.Attributes["onclick"] = string.Format("javascript:__doPostBack('{0}', 'free_combine:{1}'); return false;", this.BtnCommand.UniqueID, i);
            }

            this.BtnReg.Attributes["onclick"] = "return confirm('" + S("登録しますか。") + "');";

            BtnCommand.Style["display"] = "none";

            //ST.Visible = this.ShowSortSetting;
        }

        private void ShowMsg(string strMsg, bool bErrorMsg)
        {
            this.LblMsg.ForeColor = (bErrorMsg) ? Color.Red : Color.Blue;
            this.LblMsg.Text = strMsg;
        }

        private void SaveData()
        {
            string strCols = "";
            Core.Error ret = this.GetData(ref strCols);
            if (null != ret)
            {
                this.ShowMsg(ret.Message, true);
                return;
            }

            bool bSaveSort = false;
            //bool bSaveSort = (SessionManager.User.IsPurchaser && SessionManager.User.VIEW_TantoushaRow.Admin);
            string strSort = "";
            //if (SessionManager.User.IsPurchaser && SessionManager.User.VIEW_TantoushaRow.Admin)
            //    strSort = ST.CtlSortList.GetSort();

            ret = UserViewClass.Save(
                this.VsListID, this.VsUserID, UserViewClass.EnumType.VIEW, "", strCols, bSaveSort, strSort, Global.GetConnection());

            if (null != ret)
                this.ShowMsg(ret.Message, true);
            else
            {
                this.ShowMsg(S("登録しました。"), false);
            }
        }

        public Core.Error GetData(ref string strCols)
        {
            System.Collections.Generic.List<UserViewManager.EditGridColumn> lst = this.GetData();

            if (0 == lst.Count)
            {
                return new Core.Error(S("項目を選択してください。"));
            }

            System.Collections.Generic.List<string> lstCol = new System.Collections.Generic.List<string>();
            for (int i = 0; i < lst.Count; i++)
            {
                UserViewManager.EditGridColumn m = lst[i];
                if (1 == m.EditDataFields.Count)
                {
                    // 結合無しの場合
                    if (!m.EditDataFields[0].Visible) continue;
                    lstCol.Add(m.EditDataFields[0].FieldName);
                }
                else
                {
                    string[] s = new string[m.EditDataFields.Count];
                    for (int t = 0; t < m.EditDataFields.Count; t++)
                    {
                        s[t] = m.EditDataFields[t].FieldName;
                    }
                    lstCol.Add(string.Join(",", s));
                }
            }

            strCols = string.Join("\t", lstCol.ToArray());

            return null;
        }

        protected void BtnReset_Click(object sender, System.EventArgs e)
        {
            this.Create(this.VsListID, this.VsUserID);
        }

        protected void D_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                //追加******************
                e.Item.Cells[CELL_HYOJI].Text = S("選択");
                e.Item.Cells[CELL_HYOUJIJYUN].Text = S("列順");
                e.Item.Cells[CELL_CAPTION].Text = S("項目名");
                e.Item.Cells[CELL_KETUGOU].Text = S("列の結合");

                if (this.TextFormatMode)
                {
                    e.Item.Cells[0].Text = S("選択");
                    e.Item.Cells[1].Text = S("列順");
                }
                else
                {
                }
            }
        }

        protected void BtnDefault_Click(object sender, EventArgs e)
        {
            if (null != OnShowDefault)
                OnShowDefault();
            else
                Create(VsListID, this.VsUserID, true);   // 再ロード
        }

        protected void BtnReg_Click(object sender, System.EventArgs e)
        {
            if (null != OnSave)
            {
                string strCols = "";
                Core.Error ret = this.GetData(ref strCols);
                if (null != ret)
                {
                    this.ShowMsg(ret.Message, true);
                    return;
                }

                Core.Error r = OnSave(strCols);
                if (null != r)
                {
                    this.ShowMsg(r.Message, false);
                    return;
                }

                this.ShowMsg(S("登録しました。"), false);
            }
            else
            {
                SaveData();
                if (!string.IsNullOrEmpty(this.VsUserID))
                    //SessionManager.User.SetUserView(this.VsListID);
                Create(VsListID, this.VsUserID, false);
            }
        }

        protected void BtnCombine_Click(object sender, EventArgs e)
        {
            System.Collections.Generic.List<UserViewManager.EditGridColumn> lst = new System.Collections.Generic.List<UserViewManager.EditGridColumn>();

            int nInsert = -1;

            UserViewManager.EditGridColumn combine = new UserViewManager.EditGridColumn();

            for (int i = 0; i < this.D.Items.Count; i++)
            {
                UserViewManager.EditGridColumn c = this.GetEditGridColumn(i);

                if (!c.EditDataFields[0].Visible)
                {
                    // 非選択の結合は解除する。
                    for (int t = 0; t < c.EditDataFields.Count; t++)
                    {
                        UserViewManager.EditGridColumn ed = new UserViewManager.EditGridColumn();
                        c.EditDataFields[t].Visible = false;
                        ed.EditDataFields.Add(c.EditDataFields[t]);
                        lst.Add(ed);
                    }
                    continue;
                }

                // 以下表示列
                System.Web.UI.HtmlControls.HtmlInputCheckBox chkKetugou =
                    D.Items[i].FindControl("K") as System.Web.UI.HtmlControls.HtmlInputCheckBox;
                if (chkKetugou.Visible && chkKetugou.Checked)
                {
                    if (-1 == nInsert)
                        nInsert = i;
                    for (int t = 0; t < c.EditDataFields.Count; t++)
                        combine.EditDataFields.Add(c.EditDataFields[t]);
                }
                else
                {
                    lst.Add(c);
                }
            }

            if (1 >= combine.EditDataFields.Count)
            {
                Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert(S("2つ以上選択してください。"));
                return;
            }

            lst.Insert(nInsert, combine);

            this.Create(lst);
        }

        protected void BtnCommand_Click(object sender, EventArgs e)
        {
            string[] strArgs = this.Request.Params["__EVENTARGUMENT"].Split(new char[] { ':' }, 2);

            string strCmd = strArgs[0];
            string strArg = strArgs[1];

            switch (strCmd)
            {
                case "view":
                    {
                        this.Create(this.GetData());
                    }
                    break;
                case "order":
                    {
                        System.Collections.Generic.List<UserViewManager.EditGridColumn> lst = this.GetData();

                        int nIndex = int.Parse(strArg);
                        DropDownList ddlOrder = D.Items[nIndex].FindControl("A") as DropDownList;
                        int nMoveIndex = ddlOrder.SelectedIndex;

                        UserViewManager.EditGridColumn c = lst[nIndex];
                        lst.RemoveAt(nIndex);
                        if (nMoveIndex > lst.Count)
                            nMoveIndex = lst.Count;
                        lst.Insert(nMoveIndex, c);
                        this.Create(lst);
                    }
                    break;
                case "order_child":
                    {
                        // 結合した列内の表示順変更
                        string[] str = strArg.Split(',');
                        int nIndexParent = int.Parse(str[0]);
                        int nIndexChild = int.Parse(str[1]);

                        DataGrid dgdChild = D.Items[nIndexParent].FindControl("C") as DataGrid;
                        DropDownList ddlOrder = dgdChild.Items[nIndexChild].FindControl("B") as DropDownList;

                        int nMoveIndex = ddlOrder.SelectedIndex;

                        System.Collections.Generic.List<UserViewManager.EditGridColumn> lst = this.GetData();

                        UserViewManager.EditDataField df = lst[nIndexParent].EditDataFields[nIndexChild];
                        lst[nIndexParent].EditDataFields.RemoveAt(nIndexChild);
                        if (nMoveIndex > lst.Count)
                            nMoveIndex = lst.Count;

                        lst[nIndexParent].EditDataFields.Insert(nMoveIndex, df);

                        this.Create(lst);
                    }
                    break;
                case "free_combine":
                    {
                        // コンバイン解除
                        int nIndex = int.Parse(strArg);

                        System.Collections.Generic.List<UserViewManager.EditGridColumn> lst = this.GetData();

                        UserViewManager.EditGridColumn c = lst[nIndex];

                        lst.RemoveAt(nIndex);

                        for (int i = 0; i < c.EditDataFields.Count; i++)
                        {
                            UserViewManager.EditGridColumn n = new UserViewManager.EditGridColumn();
                            n.EditDataFields.Add(c.EditDataFields[i]);
                            lst.Insert(nIndex + i, n);
                        }
                        this.Create(lst);
                    }
                    break;
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            OnBack();
        }
    }
}
