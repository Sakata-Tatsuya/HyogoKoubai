using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using KoubaiDAL;

namespace Koubai.Common
{
    public partial class CtlFilter : System.Web.UI.UserControl
    {
        private int VsListID
        {
            get
            {
                return Convert.ToInt32(this.ViewState["VsListID"]);
            }
            set
            {
                this.ViewState["VsListID"] = value;
            }
        }

        public Telerik.Web.UI.RadAjaxManagerProxy RadAjaxManagerProxy
        {
            get;
            set;
        }
        public Telerik.Web.UI.RadAjaxManager RadAjaxManager
        {
            get;
            set;
        }

        public Telerik.Web.UI.RadAjaxLoadingPanel RadAjaxLoadingPanel
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            D.Attributes["bordercolor"] = "black";
            B.Style["display"] = "none";
        }
        
        public void Create(int nListID, int nRowCount)
        {
            this.VsListID = nListID;
            //Core.Sql.SqlDataFactory sqlData = SessionManager.User.GetUserView(nListID).SqlDataFactory;

            this.D.DataSource = new int[nRowCount];
            this.D.DataBind();

            for (int i = 0; i < this.D.Rows.Count; i++)
            {
                DropDownList ddl = D.Rows[i].FindControl("I") as DropDownList;
                ddl.Items.Add(new ListItem(SessionManager.User.Honyaku("選択なし"), ""));
                ddl.Attributes["onchange"] = string.Format("javascript:__doPostBack('{0}','{1}');", this.B.UniqueID, i);
                
                //for (int c = 0; c < sqlData.Columns.Count; c++)
                //{
                //    Core.Sql.ColumnInfo ci = sqlData.Columns[c];
                //    if (!ci.Hide && ci.ColumnType == Core.Sql.EnumColumnType.DBField)
                //    {
                //        if (ci.FieldName == "KaCode")
                //        {
                //            ddl.Items.Add(new ListItem("課/係", ci.FieldName));
                //        }
                //        else if (ci.FieldName == "KakariCode")
                //        {
                //            // 課にまとめる為表示しない(一覧上にのみ表示)
                //        }
                //        else
                //            ddl.Items.Add(new ListItem(ci.Caption.Replace("<br />", ""), ci.FieldName));
                //    }   
                //}

                Core.Web.FilterTextBox f = D.Rows[i].FindControl("F") as Core.Web.FilterTextBox;
                AppCommon.Honyaku(f);

                System.Web.UI.HtmlControls.HtmlGenericControl div = D.Rows[i].FindControl("V") as System.Web.UI.HtmlControls.HtmlGenericControl;
                div.Style["display"] = "none";
            }
        }

        public string GetFilter(SqlCommand cmd)
        {
            //Core.Sql.SqlDataFactory sqlData = SessionManager.User.GetUserView(this.VsListID).SqlDataFactory;
            Core.Sql.WhereGenerator w = new Core.Sql.WhereGenerator();
            for (int i = 0; i < D.Rows.Count; i++)
            {
                DropDownList ddl = D.Rows[i].FindControl("I") as DropDownList;
                if (0 == ddl.SelectedIndex) continue;

            //    Core.Sql.ColumnInfo ci = sqlData.Columns[ddl.SelectedValue];

            //    TypeCode tp = Type.GetTypeCode(sqlData.Columns[ddl.SelectedValue].DataColumn.DataType);

                string strWhere = "";

                //switch (tp)
                //{
                //    case TypeCode.Boolean:
                //        CheckBox chk = D.Rows[i].FindControl("CK") as CheckBox;
                //        strWhere = string.Format("{0}=@{0}", ci.FieldName);
                //        cmd.Parameters.AddWithValue("@" + ci.FieldName, chk.Checked);
                //        break;
                //    case TypeCode.DateTime:
                //        CtlNengappiFromTo n = D.Rows[i].FindControl("N") as CtlNengappiFromTo;
                //        if (null == n.From)
                //            throw new Exception(SessionManager.User.Honyaku("値を入力してください。") + ":" + ddl.SelectedItem.Text);
                //        if (n.KikanType == Core.Type.NengappiKikan.EnumKikanType.FROM)
                //        {
                //            if (null == n.To)
                //                throw new Exception(SessionManager.User.Honyaku("値を入力してください。") + ":" + ddl.SelectedItem.Text);
                //        }
                //        strWhere = n.GetNengappiKikan().GenerateSQLAsDateTime(ci.FieldName);
                //        break;
                //    default:
                //        {
                //            if (ci.FieldName == "KokyakuCode")
                //            {
                //                DropDownList ddlO = D.Rows[i].FindControl("O") as DropDownList;
                //                if (ddlO.SelectedValue == "")
                //                    throw new Exception(SessionManager.User.Honyaku("選択した値が正しくありません。") + ":" + ddl.SelectedItem.Text);

                //                strWhere = string.Format("(KokyakuCode = {0})", ddlO.SelectedValue);
                //            }
                //            // 課/係の項目追加
                //            else if (ci.FieldName == "KaCode")
                //            {
                //                DropDownList ddlO = D.Rows[i].FindControl("O") as DropDownList;
                //                if(ddlO.SelectedValue == "")
                //                    throw new Exception(SessionManager.User.Honyaku("選択した値が正しくありません。") + ":" + ddl.SelectedItem.Text);
                //                string[] sAry = ddlO.SelectedValue.Split(':');
                //                if(sAry.Length != 2)
                //                    throw new Exception(SessionManager.User.Honyaku("選択した値が正しくありません。") + ":" + ddl.SelectedItem.Text);

                //                strWhere = string.Format("(KaCode = {0} AND KakariCode = {1})", sAry[0], sAry[1]);
                //            }
                //            else if (ci.FieldName == "SeihinSeries")
                //            {
                //                DropDownList ddlO = D.Rows[i].FindControl("O") as DropDownList;
                //                if (ddlO.SelectedValue == "")
                //                    throw new Exception(SessionManager.User.Honyaku("選択した値が正しくありません。") + ":" + ddl.SelectedItem.Text);

                //                strWhere = string.Format("(SeihinSeries = '{0}')", ddlO.SelectedValue);
                //            }
                //            else
                //            {
                //                Core.Web.FilterTextBox t = D.Rows[i].FindControl("F") as Core.Web.FilterTextBox;
                //                t.TypeCode = Type.GetTypeCode(ci.DataColumn.DataType);
                //                t.Caption = ci.Caption;
                //                Core.Sql.FilterItem f = t.GetFilterItem();
                //                if (null == f)
                //                {
                //                    throw new Exception(SessionManager.User.Honyaku("値を入力してください。") + ":" + ddl.SelectedItem.Text);
                //                }
                //                strWhere = t.GetFilterItem().GetFilterText(ddl.SelectedValue, "@" + i.ToString(), cmd);
                //            }
                //            break;
                //        }
                //}

                w.Add(strWhere);
            }

            return w.WhereText;
        }

        // 図面の受付番号の検索に対応
        public string GetFilterZumen(SqlCommand cmd)
        {
            //Core.Sql.SqlDataFactory sqlData = SessionManager.User.GetUserView(this.VsListID).SqlDataFactory;
            Core.Sql.WhereGenerator w = new Core.Sql.WhereGenerator();
            for (int i = 0; i < D.Rows.Count; i++)
            {
                DropDownList ddl = D.Rows[i].FindControl("I") as DropDownList;
                if (0 == ddl.SelectedIndex) continue;

            //    Core.Sql.ColumnInfo ci = sqlData.Columns[ddl.SelectedValue];

            //    TypeCode tp = Type.GetTypeCode(sqlData.Columns[ddl.SelectedValue].DataColumn.DataType);

                string strWhere = "";

                //switch (tp)
                //{
                //    case TypeCode.Boolean:
                //        CheckBox chk = D.Rows[i].FindControl("CK") as CheckBox;
                //        strWhere = string.Format("{0}=@{0}", ci.FieldName);
                //        cmd.Parameters.AddWithValue("@" + ci.FieldName, chk.Checked);
                //        break;
                //    case TypeCode.DateTime:
                //        CtlNengappiFromTo n = D.Rows[i].FindControl("N") as CtlNengappiFromTo;
                //        if (null == n.From)
                //            throw new Exception(SessionManager.User.Honyaku("値を入力してください。") + ":" + ddl.SelectedItem.Text);
                //        if (n.KikanType == Core.Type.NengappiKikan.EnumKikanType.FROM)
                //        {
                //            if (null == n.To)
                //                throw new Exception(SessionManager.User.Honyaku("値を入力してください。") + ":" + ddl.SelectedItem.Text);
                //        }
                //        strWhere = n.GetNengappiKikan().GenerateSQLAsDateTime(ci.FieldName);
                //        break;
                //    default:
                //        Core.Web.FilterTextBox t = D.Rows[i].FindControl("F") as Core.Web.FilterTextBox;
                //        t.TypeCode = Type.GetTypeCode(ci.DataColumn.DataType);
                //        t.Caption = ci.Caption;
                //        if (ddl.SelectedValue == "UketukeBangou")
                //        {
                //            if (0 <= t.Text.IndexOf("V"))
                //            {
                //                t.Text = t.Text.Replace("V", "");
                //                w.Add("UketukeKubun = 'V'");
                //            }
                //            else
                //            {
                //                w.Add("UketukeKubun = ''");
                //            }
                //        }

                //        Core.Sql.FilterItem f = t.GetFilterItem();
                //        if (null == f)
                //        {
                //            throw new Exception(SessionManager.User.Honyaku("値を入力してください。") + ":" + ddl.SelectedItem.Text);
                //        }
                //        strWhere = t.GetFilterItem().GetFilterText(ddl.SelectedValue, "@" + i.ToString(), cmd);
                //        break;
                //}

                w.Add(strWhere);
            }

            return w.WhereText;
        }

        protected void B_Click(object sender, EventArgs e)
        {
            //Core.Sql.SqlDataFactory sqlData = SessionManager.User.GetUserView(this.VsListID).SqlDataFactory;
            int nIndex = int.Parse(this.Request.Params["__EVENTARGUMENT"]);
            DropDownList ddl = D.Rows[nIndex].FindControl("I") as DropDownList;
            
            System.Web.UI.HtmlControls.HtmlGenericControl div = D.Rows[nIndex].FindControl("V") as System.Web.UI.HtmlControls.HtmlGenericControl;
            div.Style["display"] = (0 < ddl.SelectedIndex) ? "" : "none";

            //if (0 < ddl.SelectedIndex)
            //{
            //    Core.Web.FilterTextBox t = D.Rows[nIndex].FindControl("F") as Core.Web.FilterTextBox;
            //    t.Visible = false;

            //    CheckBox chk = D.Rows[nIndex].FindControl("CK") as CheckBox;
            //    chk.Visible = false;

            //    CtlNengappiFromTo n = D.Rows[nIndex].FindControl("N") as CtlNengappiFromTo;
            //    n.Visible = false;

            //    DropDownList o = D.Rows[nIndex].FindControl("O") as DropDownList;
            //    o.Items.Clear();
            //    o.Visible = false;

            //    TypeCode tp = Type.GetTypeCode(sqlData.Columns[ddl.SelectedValue].DataColumn.DataType);

            //    switch (tp)
            //    {
            //        case TypeCode.Boolean:
            //            chk.Visible = true;
            //            break;
            //        case TypeCode.DateTime:
            //            n.KikanType = Core.Type.NengappiKikan.EnumKikanType.ONEDAY;
            //            n.Visible = true;
            //            break;
            //        case TypeCode.String:
            //            {
            //                if (sqlData.Columns[ddl.SelectedValue].FieldName == "KokyakuCode")
            //                {
            //                    MasterDataSet.M_TorihikisakiDataTable dtT = TorihikisakiClass.getM_TorihikisakiDataTable(Global.GetConnection());
            //                    for (int i = 0; i < dtT.Rows.Count; i++)
            //                    {
            //                        ListItem item = new ListItem();
            //                        item.Text = dtT[i].TorihikisakiCode + ": " + dtT[i].TorihikisakiName;
            //                        item.Value = dtT[i].TorihikisakiCode;

            //                        o.Items.Add(item);
            //                        o.Visible = true;
            //                    }
            //                }
            //                else if (sqlData.Columns[ddl.SelectedValue].FieldName == "KaCode")
            //                {
            //                    YAEMasterDataSet.YAE_M_KouteiDataTable dtO = ZaikoClass.getYAE_M_KouteiDataTable(Global.GetConnection());
            //                    DataView dvKa = dtO.DefaultView;
            //                    DataTable dtKa = dvKa.ToTable("OriginalKa", true, new string[] { "KaCode", "KakariCode" });
            //                    for (int i = 0; i < dtKa.Rows.Count; i++)
            //                    {
            //                        ListItem item = new ListItem();
            //                        item.Text = string.Format("製造{0:#0}課{1:#0}係", int.Parse(dtKa.Rows[i]["KaCode"].ToString()), int.Parse(dtKa.Rows[i]["KakariCode"].ToString()));
            //                        if (int.Parse(dtKa.Rows[i]["KakariCode"].ToString()) == 0)
            //                            item.Text = string.Format("製造{0:#0}課", int.Parse(dtKa.Rows[i]["KaCode"].ToString()));
            //                        item.Value = string.Format("{0}:{1}", dtKa.Rows[i]["KaCode"].ToString(), dtKa.Rows[i]["KakariCode"].ToString());
            //                        o.Items.Add(item);
            //                        o.Visible = true;
            //                    }
            //                }
            //                else if (sqlData.Columns[ddl.SelectedValue].FieldName == "SeihinSeries")
            //                {
            //                    DataTable dtS = MasterClass.getYAE_M_SeihinDataTable_SeihinSeries(Global.GetConnection());
            //                    for (int i = 0; i < dtS.Rows.Count; i++)
            //                    {
            //                        ListItem item = new ListItem();
            //                        item.Text = dtS.Rows[i]["SeihinSeries"].ToString();
            //                        item.Value = dtS.Rows[i]["SeihinSeries"].ToString();

            //                        o.Items.Add(item);
            //                        o.Visible = true;
            //                    }
            //                }
            //                else
            //                {
            //                    t.TypeCode = tp;
            //                    t.FilterItems.Clear();
            //                    t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.EqualTo, "と等しい"));
            //                    t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.NotEqualTo, "と等しくない"));
            //                    t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.Contains, "を含む"));
            //                    t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.DoesNotContain, "を含まない"));
            //                    t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.StartsWith, "で始まる"));
            //                    t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.EndsWith, "で終わる"));
            //                    t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.IsEmpty, "空白"));
            //                    t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.IsNotEmpty, "空白でない"));
            //                    t.Visible = true;
            //                }
            //            }
            //            break;
            //        default:
            //            {
            //                if(sqlData.Columns[ddl.SelectedValue].FieldName == "KaCode")
            //                {
            //                    YAEMasterDataSet.YAE_M_KouteiDataTable dtO = ZaikoClass.getYAE_M_KouteiDataTable(Global.GetConnection());
            //                    DataView dvKa = dtO.DefaultView;
            //                    DataTable dtKa = dvKa.ToTable("OriginalKa", true, new string[] { "KaCode", "KakariCode" });
            //                    for (int i = 0; i < dtKa.Rows.Count; i++)
            //                    {
            //                        ListItem item = new ListItem();
            //                        item.Text = string.Format("製造{0:#0}課{1:#0}係", int.Parse(dtKa.Rows[i]["KaCode"].ToString()), int.Parse(dtKa.Rows[i]["KakariCode"].ToString()));
            //                        if(int.Parse(dtKa.Rows[i]["KakariCode"].ToString()) == 0)
            //                            item.Text = string.Format("製造{0:#0}課", int.Parse(dtKa.Rows[i]["KaCode"].ToString()));
            //                        item.Value = string.Format("{0}:{1}", dtKa.Rows[i]["KaCode"].ToString(), dtKa.Rows[i]["KakariCode"].ToString());
            //                        o.Items.Add(item);
            //                        o.Visible = true;
            //                    }
            //                }
            //                else
            //                {
            //                    t.TypeCode = tp;
            //                    t.FilterItems.Clear();
            //                    t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.EqualTo, "と等しい"));
            //                    t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.NotEqualTo, "と等しくない"));
            //                    t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.GreaterThanOrEqualTo, "以上"));
            //                    t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.LessThanOrEqualTo, "以下"));
            //                    t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.GreaterThan, "より大きい"));
            //                    t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.LessThan, "より小さい"));
            //                    t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.IsNull, "NULL"));
            //                    t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.NotIsNull, "NULLでない"));
            //                    t.Visible = true;
            //                }
            //            }
            //            break;
            //    }
            //}

            if (null != this.RadAjaxManagerProxy)
                this.RadAjaxManagerProxy.AjaxSettings.AddAjaxSetting(B, div);
            else if (null != this.RadAjaxManager)
                this.RadAjaxManager.AjaxSettings.AddAjaxSetting(B, div);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (null != this.RadAjaxManagerProxy)
            {
                this.RadAjaxManagerProxy.AjaxSettings.AddAjaxSetting(B, B);
            }
            else if (null != this.RadAjaxManager)
            {
                this.RadAjaxManager.AjaxSettings.AddAjaxSetting(B, B);
            }
        }
    }
}