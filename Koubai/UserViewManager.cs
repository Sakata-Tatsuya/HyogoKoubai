using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using KoubaiDAL;

namespace Koubai
{
    public class UserViewManager
    {/// <summary>
	/// UserViewManagerClass の概要の説明です。
    /// ★既存RadColumn列の表示/非表示
	/// </summary>
        private const string UniqueNamePrefix = "__UserView@";

        /// <summary>
        /// 画面での操作用のクラス
        /// </summary>
        public class EditDataField
        {
            public string FieldName
            {
                get;
                set;
            }
            public string Caption
            {
                get;
                set;
            }
            public bool Visible
            {
                get;
                set;
            }
            public bool ForceVisible
            {
                get;
                set;
            }
            public Core.Sql.EnumColumnType ColumnType
            {
                get;
                set;
            }
        }
        public class EditGridColumn
        {
            public System.Collections.Generic.List<EditDataField> EditDataFields
            {
                get;
                set;
            }
            public EditGridColumn()
            {
                EditDataFields = new List<EditDataField>();
            }
        }

        private static string S(string str)
        {
            return SessionManager.User.Honyaku(str);
        }

        public static string GetGroupText(Core.Sql.EnumGroupType t)
        {
            switch (t)
            {
                case Core.Sql.EnumGroupType.AVG: return "平均";
                case Core.Sql.EnumGroupType.COUNT: return "カウント";
                case Core.Sql.EnumGroupType.COUNT_DISTINCT: return "個別のカウント";
                case Core.Sql.EnumGroupType.GROUP: return "グループ化";
                case Core.Sql.EnumGroupType.MIN: return "最小";
                case Core.Sql.EnumGroupType.MAX: return "最大";
                case Core.Sql.EnumGroupType.SUM: return "合計";

            }
            return null;
        }

        public enum EnumItemType
        { 
            Header, DataRow, Footer
        }

        public class UserViewEventArgs
        {
            public class DisplayTableCells : System.Collections.Generic.Dictionary<string, TableCell>
            {
                new public TableCell this[string strFieldName]
                {
                    get
                    {
                        if (base.ContainsKey(strFieldName))
                            return base[strFieldName];
                        else
                            return new TableCell();         // インデクサのアクセスで対象フィールドが無いことによるエラーが発生しないようにダミーを返す
                    }
                }
            }

            private DisplayTableCells _tblTableCell = new DisplayTableCells();

            public DisplayTableCells TableCells
            {
                get {
                    return _tblTableCell;
                }
            }

            public UserView UserView
            {
                get; set;
            }
            public int DataRowIndex
            {
                get;
                internal set;
            }
            public EnumItemType ItemType
            {
                get;
                set;
            }

            public Telerik.Web.UI.GridItem GridItem
            {
                get;
                set;
            }

            public DataRow DataRow
            {
                get;
                set;
            }

            public DataView DataView
            {
                get;
                set;
            }
        }

        public class UserView
        {
            public int ListID
            {
                get;
                internal set;
            }
            public string UserID
            {
                get;
                internal set;
            }

            public string SelectCommandText
            {
                get;
                set;
            }

            public bool CachedSqlDataFactory
            {
                get;
                set;
            }

            public Core.Sql.SqlDataFactory SqlDataFactory
            {
                get;
                set;
            }

            public delegate void DataBoundEventHandler(UserViewEventArgs e);

            private event DataBoundEventHandler _DataBoundEventHandler = null;

            System.Text.StringBuilder _sb = new System.Text.StringBuilder();
            System.Web.UI.HtmlTextWriter _writer = null;

            public string SortText_ASC
            {
                get;
                set;
            }
            public string SortText_DESC
            {
                get;
                set;
            }


            public string HeaderCss
            {
                get;
                set;
            }

            /// <summary>
            /// データソース（データテーブル）に表示カラムが無ければエラーとするかどうか
            /// </summary>
            public bool ErrorIfNotFoundColumnInDataSource
            {
                get;
                set;
            }


            public List<DataField> GetVisibleDataFields()
            {
                System.Collections.Generic.List<DataField> lst = new System.Collections.Generic.List<DataField>();

                for (int i = 0; i < this.GridColumns.Count; i++) {
                    List<DataField> l = this.GridColumns[i].VisibleDataFields;
                    for (int t = 0; t < l.Count; t++)
                        lst.Add(l[t]);
                }
                
                return lst;
            }



            /// <summary>
            /// 表示対象の列を設定する。
            /// </summary>
            /// <returns></returns>
            private List<GridColumn> GetBindGridColumns()
            {

                System.Collections.Generic.List<GridColumn> lst = new System.Collections.Generic.List<GridColumn>();

                if (null != this.GroupBy)
                {
                    // グループの設定を採用する。
                    for (int i = 0; i < this.GroupBy.Count; i++) { 
                        GridColumn gc = new GridColumn();
                        gc.DataFields.Add(new DataField(gc, this.GroupBy[i]));
                        lst.Add(gc);
                    }
                }
                else {
                    for (int i = 0; i < this.GridColumns.Count; i++) { 
                        if (0 == this.GridColumns[i].VisibleDataFields.Count) continue;
                        lst.Add(this.GridColumns[i]);
                    }
                }



                return lst;
            }

            private System.Collections.Generic.List<GridColumn> BindGridColumn
            {
                get;
                set;
            }


            public System.Collections.Generic.List<GridColumn> GridColumns
            {
                get;
                private set;
            }

            
            public DataField GetDataField(string strFieldName)
            {
                // 本来DataFieldはコレクションに入れて管理したいが、動的にGridColumnsに追加されるなど、
                // DataFieldの内容は変化するので、都度検索することにした
                for (int i = 0; i < this.GridColumns.Count; i++) {
                    for (int k = 0; k < this.GridColumns[i].DataFields.Count; k++) {
                        if (strFieldName.Equals(this.GridColumns[i].DataFields[k].ColumnInfo.FieldName))
                            return this.GridColumns[i].DataFields[k];
                    }
                }
                return null;
            }



            private Core.Sql.GroupCollection _GroupCollection = null;
            public Core.Sql.GroupCollection GroupBy
            {
                get {
                    return _GroupCollection;
                }
                set
                {
                    _GroupCollection = value;
                    if (null == value || 0 == value.Count)
                    {
                        _GroupCollection = null;
                    }
                }
            }


            public string SortExpression
            {
                get {
                    if (null != this.GroupBy)
                        return this._GroupCollection.GetSortExpression();
                    else {
                        List<DataField> lstDf = GetSortDataFields();
                        if (0 == lstDf.Count) return "";
                        string[] str = new string[lstDf.Count];
                        for (int i = 0; i < lstDf.Count; i++)
                        {
                            str[i] = lstDf[i].ColumnInfo.FieldName + ((lstDf[i].SortOrder == System.Data.SqlClient.SortOrder.Ascending) ? " ASC" : " DESC");
                        }

                        return string.Join(",", str);
                    }
                }
                set {
                    this.SetSort(value);
                }
            }

            public List<DataField> GetSortDataFields()
            {
                List<DataField> lstDf = new List<DataField>();

                System.Collections.Generic.List<UserViewManager.DataField> array = this.GetVisibleDataFields();
                System.Collections.Generic.SortedList<int, DataField> lst = new SortedList<int, DataField>();
                for (int i = 0; i < array.Count; i++)
                {
                    if (0 < array[i].SortNo && array[i].SortOrder != System.Data.SqlClient.SortOrder.Unspecified)
                        lst.Add(array[i].SortNo, array[i]);
                }

                if (0 == lst.Count) return lstDf;
                int[] nSortNo = new int[lst.Count];
                string[] str = new string[lst.Count];
                lst.Keys.CopyTo(nSortNo, 0);
                for (int i = 0; i < nSortNo.Length; i++)
                {
                    lstDf.Add(lst[nSortNo[i]]);
                }

                return lstDf;
            }


            public Core.Error SaveSort(UserViewClass.EnumType type, string strTypeMei)
            {
                return UserViewClass.SaveSort(this.ListID, this.UserID, type, strTypeMei, this.SortExpression, Global.GetConnection());
            }

            protected UserView()
            {
                System.IO.TextWriter tr = new System.IO.StringWriter(_sb);
                _writer = new System.Web.UI.HtmlTextWriter(tr);
            }

            public static UserView New(int nListID, string strUserID, bool bLoadFromCache)
			{
				Core.Sql.Dataset.T_UserListRow dr = 
					Core.Sql.SqlDataFactory.getT_UserListRow(nListID, Global.GetConnection());
				if (null == dr) return null;

				UserView obj = new UserView();
                obj.CachedSqlDataFactory = bLoadFromCache;
				obj.ListID = nListID;
				obj.UserID = strUserID;
                obj.SelectCommandText = dr.SelectCommand;
                obj.ErrorIfNotFoundColumnInDataSource = false;
                obj.InnerTableRenderByText = true;
                if (bLoadFromCache)
                { 
                    // キャッシュ使用
                    string strKey = "SqlDataFactory_ListID=" + nListID.ToString();
                    if (null != System.Web.HttpContext.Current.Application[strKey])
                    {
                        // アプリケーションキャッシュに格納して本オブジェクトでは持たない 
                        // UserViewオブジェクトは、各ユーザーのセッションに可能される。
                        // SqlDataFactoryのオブジェクトは全ユーザーで共通なので、各ユーザー単位で持つとリソースの無駄になるのでこのような方法にした
                        obj.SqlDataFactory = System.Web.HttpContext.Current.Application[strKey] as Core.Sql.SqlDataFactory;
                    }
                    else{
                        obj.SqlDataFactory = new Core.Sql.SqlDataFactory(nListID, Global.GetConnection());
                        System.Web.HttpContext.Current.Application[strKey] = obj.SqlDataFactory;
                    }
                }
                else
                    obj.SqlDataFactory = new Core.Sql.SqlDataFactory(nListID, Global.GetConnection());


                // 標準設定を読み込む
                KoubaiDAL.ShareDataSet.T_UserViewRow drDefault =
                    UserViewClass.getT_UserViewRow(nListID, "", UserViewClass.EnumType.VIEW, "", Global.GetConnection());


                KoubaiDAL.ShareDataSet.T_UserViewRow drT_UserViewRow = null;

                if ("" != strUserID)
                {
                    drT_UserViewRow = UserViewClass.getT_UserViewRow(nListID, strUserID, UserViewClass.EnumType.VIEW, "", Global.GetConnection());
                    if (null == dr)
                        drT_UserViewRow = drDefault;	// デフォルトの設定を使用する。
                }
                else
                    drT_UserViewRow = drDefault;

                if (null == drT_UserViewRow)
                {
                    drT_UserViewRow = new KoubaiDAL.ShareDataSet.T_UserViewDataTable().NewT_UserViewRow();
                    drT_UserViewRow.ListID = nListID;
                    drT_UserViewRow.Sort = "";
                    drT_UserViewRow.Columns = "";
                    drT_UserViewRow.UserID = strUserID;
                }
                if (null != drDefault)
                {
                    if ("" == drT_UserViewRow.Columns) drT_UserViewRow.Columns = drDefault.Columns;
                    if ("" == drT_UserViewRow.Sort) drT_UserViewRow.Sort = drDefault.Sort;
                }

                obj.LoadUserViewColumnData(drT_UserViewRow.Columns);
                
                obj.SortExpression = drT_UserViewRow.Sort;
				
                return obj;
			}


 
            public void LoadUserViewColumnData(string strUserViewData)
            {
                if (null == this.GridColumns)
                    this.GridColumns = new List<GridColumn>();

                this.GridColumns.Clear();

                // 表示するフィールド取得(もともと非表示の項目は無視する)
                System.Collections.Generic.Dictionary<string, Core.Sql.ColumnInfo> tblActiveColumns =
                    new System.Collections.Generic.Dictionary<string, Core.Sql.ColumnInfo>();
                for (int i = 0; i < this.SqlDataFactory.Columns.Count; i++)
                {
                    if (!this.SqlDataFactory.Columns[i].Hide)
                        tblActiveColumns.Add(this.SqlDataFactory.Columns[i].FieldName, this.SqlDataFactory.Columns[i]);
                }

                System.Collections.Generic.List<string> lstSelectedColName = new System.Collections.Generic.List<string>(); // 実際に表示されるフィールド名

                if (!string.IsNullOrEmpty(strUserViewData))
                {
                    System.Collections.Generic.List<string> lstSelectedColumnName = new System.Collections.Generic.List<string>(strUserViewData.Split('\t'));
                    for (int i = 0; i < lstSelectedColumnName.Count; i++)
                    {
                        string strColName = lstSelectedColumnName[i];
                        if ("" == strColName) continue;
                        string[] str = strColName.Split(',');
                        GridColumn gc = new GridColumn();
                        for (int c = 0; c < str.Length; c++)
                        {
                            string strName = str[c];
                            if (!tblActiveColumns.ContainsKey(strName)) continue;
                            DataField df = new DataField(gc, tblActiveColumns[strName]);
                            df.UserViewSettingVisible = true;
                            gc.DataFields.Add(df);
                            lstSelectedColName.Add(str[c]);
                        }
                        if (0 == gc.DataFields.Count) continue;
                        this.GridColumns.Add(gc);
                    }

                    // 非選択カラム
                    string[] strActiveColumnName = new string[tblActiveColumns.Count];
                    tblActiveColumns.Keys.CopyTo(strActiveColumnName, 0);
                    for (int i = 0; i < strActiveColumnName.Length; i++)
                    {
                        string strName = strActiveColumnName[i];
                        if (!lstSelectedColName.Contains(strName))
                        {
                            GridColumn gc = new GridColumn();
                            DataField df = new DataField(gc, tblActiveColumns[strName]);
                            gc.DataFields.Add(df);
                            if (tblActiveColumns[strName].ColumnType == Core.Sql.EnumColumnType.RadGridColumn)
                                df.UserViewSettingVisible = true;// ★★★★★★★★★★規定のカラムは必ず表示にする。(現状敢えて非表示にしているかどうか識別できない)
                            else
                                df.UserViewSettingVisible = false;
                            this.GridColumns.Add(gc);
                        }
                    }
                }
                else
                {
                    string[] strActiveColumnName = new string[tblActiveColumns.Count];
                    tblActiveColumns.Keys.CopyTo(strActiveColumnName, 0);
                    for (int i = 0; i < strActiveColumnName.Length; i++)
                    {
                        string strName = strActiveColumnName[i];
                        GridColumn gc = new GridColumn();
                        DataField df = new DataField(gc, tblActiveColumns[strName]);

                        gc.DataFields.Add(df);
                        this.GridColumns.Add(gc);
                    }
                }


                // DataFieldsに全て格納
                /* 廃止
                this.DataFields = new System.Collections.Generic.Dictionary<string, DataField>();
                for (int i = 0; i < GridColumns.Count; i++)
                {
                    System.Collections.Generic.List<DataField> a = this.GridColumns[i].VisibleDataFields;
                    for (int t = 0; t < a.Count; t++)
                    {
                        this.DataFields.Add(a[t].ColumnInfo.FieldName, a[t]);
                    }
                }
                */

            }


            public string CreateTextData_N(DataTable dt, Core.Data.DataTable2Text.EnumDataFormat f, Core.Data.DataTable2Text.RowBoundCallback callback, string strYM)
            {
                Core.Data.DataTable2Text d = CreateDataTable2Text_N(strYM);
                DataView dv = new DataView(dt);
                dv.Sort = this.SortExpression;
                d.DataSource = dt;
                d.SortExpression = this.SortExpression;
                d.Format = f;
                d.OnRowBoundCallback = callback;
                return d.GetTextData();
            }

            public Core.Data.DataTable2Text CreateDataTable2Text_N(string strYM)
            {
                Core.Data.DataTable2Text d = new Core.Data.DataTable2Text();

                if (null != this.GroupBy)
                {
                    for (int i = 0; i < this.GroupBy.Count; i++)
                    {
                        Core.Data.DataTable2Text.ColumnInfo c = d.Columns.Add(GroupBy[i].GroupFieldName,
                            GroupBy[i].ColumnInfo.Caption,
                            GroupBy[i].ColumnInfo.Format, GroupBy[i].ColumnInfo.TrueString, GroupBy[i].ColumnInfo.FalseString);
                    }
                }
                else
                {
                    System.Collections.Generic.List<UserViewManager.DataField> array = this.GetVisibleDataFields();
                    for (int i = 0; i < array.Count; i++)
                    {
                        if (array[i].ColumnInfo.ColumnType == Core.Sql.EnumColumnType.DBField ||
                            array[i].ColumnInfo.ColumnType == Core.Sql.EnumColumnType.UserField)
                        {
                            string[] strKomoku = { "N-6", "N-5", "N-4", "N-3", "N-2", "N-1", "N", "N+1", "N+2", "N+3", "N+4", "N+5" };
                            bool bMode = false;
                            
                            for (int y = 0; y < strKomoku.Length; y++)
                            {
                                if (array[i].ColumnInfo.Caption == strKomoku[y])
                                {
                                    Core.Data.DataTable2Text.ColumnInfo c = d.Columns.Add(array[i].ColumnInfo.FieldName, Utility.GetYM(y-6, strYM).ToString(),
                                        array[i].ColumnInfo.Format, array[i].ColumnInfo.TrueString, array[i].ColumnInfo.FalseString);
                                    bMode = true;
                                    break;
                                }
                            }
                            
                            if (bMode == false)
                            {
                                Core.Data.DataTable2Text.ColumnInfo c = d.Columns.Add(array[i].ColumnInfo.FieldName, array[i].ColumnInfo.Caption,
                                    array[i].ColumnInfo.Format, array[i].ColumnInfo.TrueString, array[i].ColumnInfo.FalseString);
                            }
                        }
                    }
                }
                
                d.SortExpression = this.SortExpression;
                return d;
            }


            public string CreateTextData(DataTable dt, Core.Data.DataTable2Text.EnumDataFormat f, Core.Data.DataTable2Text.RowBoundCallback callback)
            {
                Core.Data.DataTable2Text d = CreateDataTable2Text();
                DataView dv = new DataView(dt);
                dv.Sort = this.SortExpression;
                d.DataSource = dt;
                d.SortExpression = this.SortExpression;
                d.Format = f;
                d.OnRowBoundCallback = callback;
                return d.GetTextData();
            }

            public Core.Data.DataTable2Text CreateDataTable2Text()
            {
                Core.Data.DataTable2Text d = new Core.Data.DataTable2Text();

                if (null != this.GroupBy)
                {
                    for (int i = 0; i < this.GroupBy.Count; i++)
                    {
                        Core.Data.DataTable2Text.ColumnInfo c = d.Columns.Add(GroupBy[i].GroupFieldName, 
                            GroupBy[i].ColumnInfo.Caption,
                            GroupBy[i].ColumnInfo.Format, GroupBy[i].ColumnInfo.TrueString, GroupBy[i].ColumnInfo.FalseString);
                    }
                }
                else
                {
                    System.Collections.Generic.List<UserViewManager.DataField> array = this.GetVisibleDataFields();
                    for (int i = 0; i < array.Count; i++)
                    {
                        if (array[i].ColumnInfo.ColumnType == Core.Sql.EnumColumnType.DBField ||
                            array[i].ColumnInfo.ColumnType == Core.Sql.EnumColumnType.UserField)
                        {
                            Core.Data.DataTable2Text.ColumnInfo c = d.Columns.Add(array[i].ColumnInfo.FieldName, array[i].ColumnInfo.Caption,
                                array[i].ColumnInfo.Format, array[i].ColumnInfo.TrueString, array[i].ColumnInfo.FalseString);
                        }
                    }
                }

                d.SortExpression = this.SortExpression;

                return d;

            }
            

            private static string GetCss(Core.Sql.ColumnInfo.EnumTextAlign TextAlign)
			{
                switch (TextAlign) 
				{
					case Core.Sql.ColumnInfo.EnumTextAlign.Left:
						return "";
                    case Core.Sql.ColumnInfo.EnumTextAlign.Center:
						return "tc";
                    case Core.Sql.ColumnInfo.EnumTextAlign.Right:
						 return "tr";
				}
				return "";
			}

			private string GetText(UserViewManager.DataField m, DataRow dr)
			{
                Core.Sql.ColumnInfo c = (null != m.ColumnInfo) ? m.ColumnInfo : m.GroupInfo.ColumnInfo;
                string strFieldName = (null != m.ColumnInfo) ? m.ColumnInfo.FieldName : m.GroupInfo.GroupFieldName;


                if (!dr.Table.Columns.Contains(strFieldName)) 
                {
                    if (ErrorIfNotFoundColumnInDataSource)
                        throw new Exception(string.Format("データソースにカラム{0}が見つかりません。", strFieldName));
                    else
                        return "";  
                }

                if (dr.IsNull(strFieldName)) 
				{
                    return "";
				}
				else 
				{
                    if (dr[strFieldName] is bool)
                    {
                        return (Convert.ToBoolean(dr[strFieldName])) ? c.TrueString : c.FalseString;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(c.Format))
                            return string.Format(c.Format, dr[strFieldName]);
                        else
                            return Convert.ToString(dr[strFieldName]);
                    }
				}
			}

            public string GetSortText()
            {
                if (string.IsNullOrEmpty(this.SortExpression)) return "";

                ArrayList lst = new ArrayList();
                System.Collections.Generic.List<string> lstText = new System.Collections.Generic.List<string>();

                string[] strSorts = this.SortExpression.Split(',');

                string strASC = SessionManager.User.Honyaku("昇順");
                string strDESC = SessionManager.User.Honyaku("降順");

                for (int i = 0; i < strSorts.Length; i++)
                {
                    strSorts[i] = strSorts[i].Trim();
                    string[] s = strSorts[i].Split(' ');

                    string strCol;
                    System.Data.SqlClient.SortOrder sort = System.Data.SqlClient.SortOrder.Ascending;

                    if (1 == s.Length)
                    {
                        strCol = s[0];
                        sort = System.Data.SqlClient.SortOrder.Ascending;
                    }
                    else
                    {
                        strCol = s[0];
                        if (s[1].ToUpper() == "ASC")
                            sort = System.Data.SqlClient.SortOrder.Ascending;
                        else if (s[1].ToUpper() == "DESC")
                            sort = System.Data.SqlClient.SortOrder.Descending;
                        else throw new Exception("pg error");
                    }

                    if (lst.Contains(strCol)) continue;

                    lst.Add(strCol);

                    DataField c = this.GetDataField(strCol);
                    string strCaption = (null == c) ? "???" : SessionManager.User.Honyaku(c.ColumnInfo.Caption);

                    lstText.Add(string.Format("{0} ({1})", strCaption, 
                        (sort == System.Data.SqlClient.SortOrder.Ascending)? strASC : strDESC));
                }

                return string.Join(" , ", lstText.ToArray());
            }


			private void SetSort(string strSort)
			{
                // 同じ項目が出現する可能性がある(order by hinban asc, binban, asc)ので、再設定する。
                System.Collections.Generic.List<UserViewManager.DataField> array = this.GetVisibleDataFields();
                for (int i = 0; i < array.Count; i++) {
                    array[i].SortOrder = System.Data.SqlClient.SortOrder.Unspecified;
                    array[i].SortNo = 0;
                }
                
                if (string.IsNullOrEmpty(strSort)) return;

                System.Collections.Generic.List<string> lstSortCol= new System.Collections.Generic.List<string>();
				string [] strSorts = strSort.Split(',');
                int nCount = 1;
                for (int i = 0; i < strSorts.Length; i++) 
				{
					strSorts[i] = strSorts[i].Trim();
					string [] s = strSorts[i].Split(' ');

					string strCol;
                    System.Data.SqlClient.SortOrder sort;

					if (1 == s.Length) 
					{
						strCol = s[0].Trim();
                        sort = System.Data.SqlClient.SortOrder.Ascending;
					}
					else 
					{
						strCol = s[0].Trim();
						if (s[1].ToUpper() == "ASC")
                            sort = System.Data.SqlClient.SortOrder.Ascending;
						else if (s[1].ToUpper() == "DESC")
                            sort = System.Data.SqlClient.SortOrder.Descending;
						else throw new Exception("pg error");
					}

                    if (lstSortCol.Contains(strCol)) continue;

                    DataField df = this.GetDataField(strCol);
                    if (null == df) continue;

                    if (!df.Visible) 
                    {
                        // このソート列が表示列でない場合はソートに加えない
                        continue;
                    }

                    df.SortOrder = sort;
                    df.SortNo = nCount++;

                    lstSortCol.Add(strCol);
				}

			}


            /// <summary>
            /// カラム内に複数行あるケースでHTMLで書き込むかどうか。
            /// </summary>
            public bool InnerTableRenderByText
            {
                get;
                set;
            }

            

            public void CreateRadGrid(Telerik.Web.UI.RadGrid dgd,
                int nColumnStartIndex, DataBoundEventHandler callback)
            {
                if (0 > nColumnStartIndex) nColumnStartIndex = 0;
                this._DataBoundEventHandler = callback;
                dgd.ItemDataBound += new Telerik.Web.UI.GridItemEventHandler(this.RadGrid_ItemDataBound);

                // カラムの最後から削除していく(デザイン時は既存列の最後に列が追加されている為)
                while (0 < dgd.MasterTableView.Columns.Count) 
                {
                    string strUniqueName = dgd.MasterTableView.Columns[dgd.MasterTableView.Columns.Count - 1].UniqueName;
                    if (strUniqueName.StartsWith(UniqueNamePrefix))
                    {
                        // UniqueNameが数値で有れば動的に追加したカラムなので削除
                        dgd.MasterTableView.Columns.RemoveAt(dgd.MasterTableView.Columns.Count - 1);
                    }
                    else
                        break;
                }

                if (nColumnStartIndex > dgd.MasterTableView.Columns.Count)
                    nColumnStartIndex = dgd.MasterTableView.Columns.Count;

                System.Collections.ArrayList lstColumns = new ArrayList();  // 表示する列の一覧（表示順で挿入）

                this.BindGridColumn = this.GetBindGridColumns();
                List<string> lstFieldName = new List<string>();
                for (int i = 0; i < this.BindGridColumn.Count; i++) {
                    for (int t = 0; t < this.BindGridColumn[i].DataFields.Count; t++) 
                    {
                        DataField df = this.BindGridColumn[i].DataFields[t];
                        if (null != df.GroupInfo)
                            lstFieldName.Add(df.GroupInfo.GroupFieldName);
                        else
                            lstFieldName.Add(df.ColumnInfo.FieldName);
                    }
                }


                // 開始列までを挿入
                for (int i = 0; i < nColumnStartIndex; i++)
                {
                    if (!lstFieldName.Contains(dgd.MasterTableView.Columns[i].UniqueName))
                        lstColumns.Add(dgd.MasterTableView.Columns[i]);
                }


                // 列定義順に挿入
                for (int i = 0; i < this.BindGridColumn.Count; i++)
                {
                    UserViewManager.GridColumn gc = this.BindGridColumn[i];

                    if (1 == gc.DataFields.Count && gc.DataFields[0].ColumnInfo.ColumnType == Core.Sql.EnumColumnType.RadGridColumn)
                    {
                        // 列タイプがRadGridColumnの場合、m.FieldNameにはUniqueNameがセットされている。
                        DataField df = gc.DataFields[0];
                        gc.WebUI_GridColumn = dgd.MasterTableView.Columns.FindByUniqueNameSafe(df.ColumnInfo.FieldName); // これだけは設定できる。 
                        gc.WebUI_GridColumn.HeaderText = S(df.ColumnInfo.Caption);   // 規定列はヘッダのテキストをココで設定する(これは後の処理では変更できない)
                        if (null == gc.WebUI_GridColumn)
                        {
                            throw new Exception(string.Format("UniqueName={0}の列がありません。", df.ColumnInfo.FieldName));
                        }
                        lstColumns.Add(gc.WebUI_GridColumn);
                    }
                    else
                    {
                        Telerik.Web.UI.GridBoundColumn bc = new Telerik.Web.UI.GridBoundColumn();
                        bc.ItemStyle.Wrap = false;
                        bc.HeaderStyle.Wrap = false;
                        bc.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                        bc.HeaderStyle.CssClass = this.HeaderCss;
                        bc.FooterStyle.Wrap = false;

                        bc.UniqueName = gc.WebUI_GridColumnUniqueName;

#if DEBUG
                        string str = "";
                        if (1 == gc.DataFields.Count)
                        {
                            str = gc.DataFields[0].ColumnInfo.Caption;
                        }
                        else
                        {
                            for (int r = 0; r < gc.DataFields.Count; r++)
                            {
                                str += gc.DataFields[r].ColumnInfo.Caption + "/";
                            }
                        }

                        bc.HeaderText = str;
#endif

                        //dgd.MasterTableView.Columns.Insert(this._nColumnStartIndex + i, bc);	インサートするとデザイン時にあったコントロールがViewStateに登録されず、FindControl()出来ない
                        dgd.MasterTableView.Columns.Add(bc);    // 必ず最後に追加
                        int nInsertIndex = nColumnStartIndex + i;
                        gc.WebUI_GridColumn = bc;
                        lstColumns.Add(bc);
                    }
                }
#if DEBUG
                for (int y = 0; y < lstColumns.Count; y++)
                {
                    string str = "";
                    Telerik.Web.UI.GridColumn c = lstColumns[y] as Telerik.Web.UI.GridColumn;
                    if (c is Telerik.Web.UI.GridBoundColumn)
                    {
                        Telerik.Web.UI.GridBoundColumn d = c as Telerik.Web.UI.GridBoundColumn;
                        str = d.HeaderText;

                    }
                    else
                    {
                        str = c.UniqueName;
                    }
                }
#endif

                // 追加されていない列を最後に追加
                for (int i = 0; i < dgd.MasterTableView.Columns.Count; i++)
                {
                    if (!lstColumns.Contains(dgd.MasterTableView.Columns[i]))
                        lstColumns.Add(dgd.MasterTableView.Columns[i]);
                }

                // OrderIndexの再設定
                for (int i = 0; i < lstColumns.Count; i++)
                {
                    Telerik.Web.UI.GridColumn c = lstColumns[i] as Telerik.Web.UI.GridColumn;
                    c.OrderIndex = i + 2; // 実際のセル列のindexと一致させる為2を加算
                }
            }

            public void CreateRadGrid_H(Telerik.Web.UI.RadGrid dgd,
                int nColumnStartIndex, DataBoundEventHandler callback)
            {
                if (0 > nColumnStartIndex) nColumnStartIndex = 0;
                this._DataBoundEventHandler = callback;
                dgd.ItemDataBound += new Telerik.Web.UI.GridItemEventHandler(this.RadGrid_H_ItemDataBound);

                // カラムの最後から削除していく(デザイン時は既存列の最後に列が追加されている為)
                while (0 < dgd.MasterTableView.Columns.Count)
                {
                    string strUniqueName = dgd.MasterTableView.Columns[dgd.MasterTableView.Columns.Count - 1].UniqueName;
                    if (strUniqueName.StartsWith(UniqueNamePrefix))
                    {
                        // UniqueNameが数値で有れば動的に追加したカラムなので削除
                        dgd.MasterTableView.Columns.RemoveAt(dgd.MasterTableView.Columns.Count - 1);
                    }
                    else
                        break;
                }

                if (nColumnStartIndex > dgd.MasterTableView.Columns.Count)
                    nColumnStartIndex = dgd.MasterTableView.Columns.Count;

                System.Collections.ArrayList lstColumns = new ArrayList();  // 表示する列の一覧（表示順で挿入）

                this.BindGridColumn = this.GetBindGridColumns();
                List<string> lstFieldName = new List<string>();
                for (int i = 0; i < this.BindGridColumn.Count; i++)
                {
                    for (int t = 0; t < this.BindGridColumn[i].DataFields.Count; t++)
                    {
                        DataField df = this.BindGridColumn[i].DataFields[t];
                        if (null != df.GroupInfo)
                            lstFieldName.Add(df.GroupInfo.GroupFieldName);
                        else
                            lstFieldName.Add(df.ColumnInfo.FieldName);
                    }
                }


                // 開始列までを挿入
                for (int i = 0; i < nColumnStartIndex; i++)
                {
                    if (!lstFieldName.Contains(dgd.MasterTableView.Columns[i].UniqueName))
                        lstColumns.Add(dgd.MasterTableView.Columns[i]);
                }

                int nGroupCnt = 1;

                // 列定義順に挿入
                for (int i = 0; i < this.BindGridColumn.Count; i++)
                {
                    UserViewManager.GridColumn gc = this.BindGridColumn[i];

                    if (1 == gc.DataFields.Count && gc.DataFields[0].ColumnInfo.ColumnType == Core.Sql.EnumColumnType.RadGridColumn)
                    {
                        // 列タイプがRadGridColumnの場合、m.FieldNameにはUniqueNameがセットされている。
                        DataField df = gc.DataFields[0];
                        gc.WebUI_GridColumn = dgd.MasterTableView.Columns.FindByUniqueNameSafe(df.ColumnInfo.FieldName); // これだけは設定できる。 
                        gc.WebUI_GridColumn.HeaderText = S(df.ColumnInfo.Caption);   // 規定列はヘッダのテキストをココで設定する(これは後の処理では変更できない)
                        if (null == gc.WebUI_GridColumn)
                        {
                            throw new Exception(string.Format("UniqueName={0}の列がありません。", df.ColumnInfo.FieldName));
                        }
                        lstColumns.Add(gc.WebUI_GridColumn);
                    }
                    else
                    {
                        if (gc.DataFields.Count > 1)
                        {
                            Telerik.Web.UI.GridColumnGroup ColG = new Telerik.Web.UI.GridColumnGroup();
                            ColG.Name = string.Format("Group{0}", nGroupCnt);
                            ColG.HeaderText = ColG.Name;
                            dgd.MasterTableView.ColumnGroups.Add(ColG);

                            for (int n = 0; n < gc.DataFields.Count; n++)
                            {
                                Telerik.Web.UI.GridBoundColumn bc = new Telerik.Web.UI.GridBoundColumn();
                                bc.ItemStyle.Wrap = false;
                                bc.HeaderStyle.Wrap = false;
                                bc.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                                bc.HeaderStyle.CssClass = this.HeaderCss;
                                bc.FooterStyle.Wrap = false;
                                bc.ColumnGroupName = ColG.Name;

                                bc.UniqueName = string.Format("{0}{1}", UniqueNamePrefix, gc.DataFields[n].ColumnInfo.FieldName);
                                //bc.UniqueName = gc.WebUI_GridColumnUniqueName;

#if DEBUG
                                string str = "";
                                if (1 == gc.DataFields.Count)
                                {
                                    str = gc.DataFields[0].ColumnInfo.Caption;
                                }
                                else
                                {
                                    for (int r = 0; r < gc.DataFields.Count; r++)
                                    {
                                        str += gc.DataFields[r].ColumnInfo.Caption + "/";
                                    }
                                }

                                bc.HeaderText = str;
#endif

                                //dgd.MasterTableView.Columns.Insert(this._nColumnStartIndex + i, bc);	インサートするとデザイン時にあったコントロールがViewStateに登録されず、FindControl()出来ない
                                dgd.MasterTableView.Columns.Add(bc);    // 必ず最後に追加
                                int nInsertIndex = nColumnStartIndex + i;
                                gc.WebUI_GridColumn = bc;
                                lstColumns.Add(bc);
                            }
                        }
                        else
                        {
                            Telerik.Web.UI.GridBoundColumn bc = new Telerik.Web.UI.GridBoundColumn();
                            bc.ItemStyle.Wrap = false;
                            bc.HeaderStyle.Wrap = false;
                            bc.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                            bc.HeaderStyle.CssClass = this.HeaderCss;
                            bc.FooterStyle.Wrap = false;

                            bc.UniqueName = gc.WebUI_GridColumnUniqueName;

#if DEBUG
                            string str = "";
                            if (1 == gc.DataFields.Count)
                            {
                                str = gc.DataFields[0].ColumnInfo.Caption;
                            }
                            else
                            {
                                for (int r = 0; r < gc.DataFields.Count; r++)
                                {
                                    str += gc.DataFields[r].ColumnInfo.Caption + "/";
                                }
                            }

                            bc.HeaderText = str;
#endif

                            //dgd.MasterTableView.Columns.Insert(this._nColumnStartIndex + i, bc);	インサートするとデザイン時にあったコントロールがViewStateに登録されず、FindControl()出来ない
                            dgd.MasterTableView.Columns.Add(bc);    // 必ず最後に追加
                            int nInsertIndex = nColumnStartIndex + i;
                            gc.WebUI_GridColumn = bc;
                            lstColumns.Add(bc);
                        }
                    }
                }
#if DEBUG
                for (int y = 0; y < lstColumns.Count; y++)
                {
                    string str = "";
                    Telerik.Web.UI.GridColumn c = lstColumns[y] as Telerik.Web.UI.GridColumn;
                    if (c is Telerik.Web.UI.GridBoundColumn)
                    {
                        Telerik.Web.UI.GridBoundColumn d = c as Telerik.Web.UI.GridBoundColumn;
                        str = d.HeaderText;

                    }
                    else
                    {
                        str = c.UniqueName;
                    }
                }
#endif

                // 追加されていない列を最後に追加
                for (int i = 0; i < dgd.MasterTableView.Columns.Count; i++)
                {
                    if (!lstColumns.Contains(dgd.MasterTableView.Columns[i]))
                        lstColumns.Add(dgd.MasterTableView.Columns[i]);
                }

                // OrderIndexの再設定
                for (int i = 0; i < lstColumns.Count; i++)
                {
                    Telerik.Web.UI.GridColumn c = lstColumns[i] as Telerik.Web.UI.GridColumn;
                    c.OrderIndex = i + 2; // 実際のセル列のindexと一致させる為2を加算
                }
            }

            private System.Web.UI.WebControls.Table CreateInnerTable(int nRows)
            {
                Table tbl = new Table();

                tbl.BorderWidth = Unit.Pixel(0);
                tbl.CellPadding = 2;
                tbl.Width = Unit.Percentage(100);
                for (int i = 0; i < nRows; i++)
                {
                    TableRow r = new TableRow();
                    TableCell c = new TableCell();
                    c.Wrap = false;
                    r.Cells.Add(c);

                    if (i != nRows - 1) 
                        c.Style["border-bottom"] = "solid 1px black";

                    c.Text = "&nbsp;";
                    tbl.Rows.Add(r);
                }

                return tbl;
            }


            private void AddCss(TableCell cell, string strCss)
            {
                if (string.IsNullOrEmpty(cell.CssClass))
                    cell.CssClass = strCss;
                else
                    cell.CssClass += " " + strCss;
            }


            private void RadGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
            {
                switch (e.Item.ItemType)
                {
                    case Telerik.Web.UI.GridItemType.Header:
                    case Telerik.Web.UI.GridItemType.Footer:
                    case Telerik.Web.UI.GridItemType.Item:
                    case Telerik.Web.UI.GridItemType.AlternatingItem:
                    case Telerik.Web.UI.GridItemType.SelectedItem:
                        break;
                    default:
                        return;
                }

                Telerik.Web.UI.RadGrid dgd = sender as Telerik.Web.UI.RadGrid;
                UserViewEventArgs arg = new UserViewEventArgs();

                if (dgd.DataSource is DataView)
                    arg.DataView = dgd.DataSource as DataView;
                else if (dgd.DataSource is DataTable)
                    arg.DataView = (dgd.DataSource as DataTable).DefaultView;


                arg.UserView = this;
                arg.GridItem = e.Item;

                DataRow dr = null;
                System.Collections.Generic.Dictionary<int, Table> InnerTables = 
                    new System.Collections.Generic.Dictionary<int,Table>(); // 結合列の内部テーブル

                // ----- 予め表示対象のセルを設定しておく-----
                for (int i = 0; i < this.BindGridColumn.Count; i++)
                {
                    UserViewManager.GridColumn gc = this.BindGridColumn[i];

                    if (1 == gc.DataFields.Count && gc.DataFields[0].ColumnInfo.ColumnType == Core.Sql.EnumColumnType.RadGridColumn) continue;    // ★★★★★既存列の場合はスキップ

                    Telerik.Web.UI.GridColumn col = gc.WebUI_GridColumn;

                    int nColIndex = col.OrderIndex;

                    if (1 == gc.DataFields.Count)
                    {
                        this.AddCss(e.Item.Cells[nColIndex], UserView.GetCss(gc.DataFields[0].ColumnInfo.TextAlign));
                        arg.TableCells.Add(gc.DataFields[0].ColumnInfo.FieldName, e.Item.Cells[nColIndex]);
                    }
                    else
                    {
                        // 結合列の場合
                        Table t = CreateInnerTable(gc.DataFields.Count);
                        t.CssClass = "def";
                        for (int c = 0; c < gc.DataFields.Count; c++)
                        {
                            UserViewManager.DataField df = gc.DataFields[c];

                            TableCell cell = t.Rows[c].Cells[0];
                            this.AddCss(cell, UserView.GetCss(df.ColumnInfo.TextAlign));

                            arg.TableCells.Add(df.ColumnInfo.FieldName, cell);
                        }
                        e.Item.Cells[nColIndex].CssClass = "fit";
                        e.Item.Cells[nColIndex].Text = "";
                        e.Item.Cells[nColIndex].Controls.Add(t);
                        InnerTables.Add(i, t);

                    }
                }

                switch (e.Item.ItemType)
                {
                    case Telerik.Web.UI.GridItemType.Header:

                        arg.ItemType = EnumItemType.Header;
                        e.Item.CssClass = this.HeaderCss;

                        for (int i = 0; i < this.BindGridColumn.Count; i++)
                        {
                            UserViewManager.GridColumn gc = this.BindGridColumn[i];

                            if (1 == gc.DataFields.Count)
                            {
                                DataField df = gc.DataFields[0];
                                string strFieldName = (null != df.GroupInfo) ? df.GroupInfo.GroupFieldName : df.ColumnInfo.FieldName;

                                TableCell cell = arg.TableCells[strFieldName];

                                AddCss(cell, gc.HeaderCSS);

                                string strCaption = S(df.ColumnInfo.Caption);
                                if (null != df.GroupInfo) strCaption += string.Format("({0})", S(GetGroupText(df.GroupInfo.GroupType)));

                                switch (df.SortOrder) { 
                                    case System.Data.SqlClient.SortOrder.Ascending:
                                        strCaption += this.SortText_ASC;
                                        break;
                                    case System.Data.SqlClient.SortOrder.Descending:
                                        strCaption += this.SortText_DESC;
                                        break;
                                }

                                cell.Text = strCaption;
                            }
                            else
                            {
                                // 結合列の場合
                                for (int c = 0; c < gc.DataFields.Count; c++)
                                {
                                    UserViewManager.DataField df = gc.DataFields[c];
                                    string strFieldName = (null != df.GroupInfo) ? df.GroupInfo.GroupFieldName : df.ColumnInfo.FieldName;
                                    TableCell cell = arg.TableCells[strFieldName];
                                    this.AddCss(cell, this.HeaderCss);
                                    this.AddCss(cell, gc.HeaderCSS);

                                    string strCaption = S(df.ColumnInfo.Caption);
                                    if (null != df.GroupInfo)
                                        strCaption += string.Format("({0})", S(GetGroupText(df.GroupInfo.GroupType)));

                                    switch (df.SortOrder) { 
                                        case System.Data.SqlClient.SortOrder.Ascending:
                                            strCaption += this.SortText_ASC;
                                            break;
                                        case System.Data.SqlClient.SortOrder.Descending:
                                            strCaption += this.SortText_DESC;
                                            break;
                                    }

                                    cell.Text = strCaption;
                                }
                            }
                        }

                        break;
                    case Telerik.Web.UI.GridItemType.Item:
                    case Telerik.Web.UI.GridItemType.AlternatingItem:
                    case Telerik.Web.UI.GridItemType.SelectedItem:
                        dr = (e.Item.DataItem as DataRowView).Row;

                        arg.DataRow = dr;
                        arg.DataRowIndex = e.Item.ItemIndex;
                        arg.ItemType = EnumItemType.DataRow;
                        for (int i = 0; i < this.BindGridColumn.Count; i++)
                        {
                            UserViewManager.GridColumn gc = this.BindGridColumn[i];

                            if (1 < gc.DataFields.Count)
                            {
                                // 結合列の場合
                                for (int c = 0; c < gc.DataFields.Count; c++)
                                {
                                    UserViewManager.DataField df = gc.DataFields[c];
                                    string strFieldName = (null != df.GroupInfo) ? df.GroupInfo.GroupFieldName : df.ColumnInfo.FieldName;
                                    TableCell cell = arg.TableCells[strFieldName];
                                    cell.Text = GetText(df, dr);
                                    if ("" == cell.Text.Trim()) cell.Text = "&nbsp;";
                                }
                            }
                            else
                            {
                                DataField df = gc.DataFields[0];
                                string strFieldName = (null != df.GroupInfo) ? df.GroupInfo.GroupFieldName : df.ColumnInfo.FieldName;
                                TableCell cell = arg.TableCells[strFieldName];
                                cell.Text = GetText(df, dr);
                                if ("" == cell.Text.Trim()) cell.Text = "&nbsp;";
                            }
                        }
                        break;
                    case Telerik.Web.UI.GridItemType.Footer:
                        arg.ItemType = EnumItemType.Footer;

                        break;
                }

                // コールバック
                if (null != this._DataBoundEventHandler)
                    this._DataBoundEventHandler(arg);


                // 結合列をViewStateに保存する為、動的コントロール(Table)をHTML化して書き込む
                for (int i = 0; i < this.BindGridColumn.Count; i++)
                {
                    UserViewManager.GridColumn gc = this.BindGridColumn[i];
                    if (1 == gc.DataFields.Count && gc.DataFields[0].ColumnInfo.ColumnType == Core.Sql.EnumColumnType.RadGridColumn) continue;    // ★★★★★既存列の場合はスキップ
                    Telerik.Web.UI.GridColumn col = gc.WebUI_GridColumn;
                    if (1 < gc.DataFields.Count)
                    {
                        if (InnerTableRenderByText)
                        {
                            _sb.Length = 0;
                            Table t = InnerTables[i];

                            t.RenderControl(_writer);
                            _writer.Flush();
                            e.Item.Cells[col.OrderIndex].Text = _sb.ToString();  // この処理で内部テーブルがViewstateに保存される。
                        }
                    }
                    if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Header)
                    {
                        // ★これが無いとAjaxで更新時にヘッダー文字が消える
                        col.HeaderText = e.Item.Cells[col.OrderIndex].Text;
                    }
                    else if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Footer)
                    {
                        col.FooterText = e.Item.Cells[col.OrderIndex].Text;
                    }
                }
            }

            bool bHeader = false;
            private void RadGrid_H_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
            {
                switch (e.Item.ItemType)
                {
                    case Telerik.Web.UI.GridItemType.Header:
                    case Telerik.Web.UI.GridItemType.Footer:
                    case Telerik.Web.UI.GridItemType.Item:
                    case Telerik.Web.UI.GridItemType.AlternatingItem:
                    case Telerik.Web.UI.GridItemType.SelectedItem:
                        break;
                    default:
                        return;
                }

                Telerik.Web.UI.RadGrid dgd = sender as Telerik.Web.UI.RadGrid;
                UserViewEventArgs arg = new UserViewEventArgs();

                if (dgd.DataSource is DataView)
                    arg.DataView = dgd.DataSource as DataView;
                else if (dgd.DataSource is DataTable)
                    arg.DataView = (dgd.DataSource as DataTable).DefaultView;


                arg.UserView = this;
                arg.GridItem = e.Item;

                DataRow dr2 = null;
                System.Collections.Generic.Dictionary<int, Table> InnerTables2 =
                    new System.Collections.Generic.Dictionary<int, Table>(); // 結合列の内部テーブル

                // ----- 予め表示対象のセルを設定しておく-----
                if (bHeader && e.Item.ItemType == Telerik.Web.UI.GridItemType.Header)
                {
                    for (int i = 0; i < this.BindGridColumn.Count; i++)
                    {
                        UserViewManager.GridColumn gc = this.BindGridColumn[i];

                        if (1 == gc.DataFields.Count && gc.DataFields[0].ColumnInfo.ColumnType == Core.Sql.EnumColumnType.RadGridColumn) continue;    // ★★★★★既存列の場合はスキップ

                        if (1 < gc.DataFields.Count)
                        {
                            // 結合列の場合
                            for (int c = 0; c < gc.DataFields.Count; c++)
                            {
                                UserViewManager.DataField df = gc.DataFields[c];

                                if (c < e.Item.Cells.Count)
                                {
                                    this.AddCss(e.Item.Cells[c], UserView.GetCss(gc.DataFields[c].ColumnInfo.TextAlign));
                                    arg.TableCells.Add(gc.DataFields[c].ColumnInfo.FieldName, e.Item.Cells[c]);
                                }
                            }
                        }
                    }

                    #region データ内容
                    switch (e.Item.ItemType)
                    {
                        case Telerik.Web.UI.GridItemType.Header:

                            arg.ItemType = EnumItemType.Header;
                            e.Item.CssClass = this.HeaderCss;

                            for (int i = 0; i < this.BindGridColumn.Count; i++)
                            {
                                UserViewManager.GridColumn gc = this.BindGridColumn[i];

                                if (1 == gc.DataFields.Count)
                                {
                                    DataField df = gc.DataFields[0];
                                    string strFieldName = (null != df.GroupInfo) ? df.GroupInfo.GroupFieldName : df.ColumnInfo.FieldName;

                                    TableCell cell = arg.TableCells[strFieldName];

                                    AddCss(cell, gc.HeaderCSS);

                                    string strCaption = S(df.ColumnInfo.Caption);
                                    if (null != df.GroupInfo) strCaption += string.Format("({0})", S(GetGroupText(df.GroupInfo.GroupType)));

                                    switch (df.SortOrder)
                                    {
                                        case System.Data.SqlClient.SortOrder.Ascending:
                                            strCaption += this.SortText_ASC;
                                            break;
                                        case System.Data.SqlClient.SortOrder.Descending:
                                            strCaption += this.SortText_DESC;
                                            break;
                                    }

                                    cell.Text = strCaption;
                                }
                                else
                                {
                                    // 結合列の場合
                                    for (int c = 0; c < gc.DataFields.Count; c++)
                                    {
                                        UserViewManager.DataField df = gc.DataFields[c];
                                        string strFieldName = (null != df.GroupInfo) ? df.GroupInfo.GroupFieldName : df.ColumnInfo.FieldName;
                                        TableCell cell = arg.TableCells[strFieldName];
                                        this.AddCss(cell, this.HeaderCss);
                                        this.AddCss(cell, gc.HeaderCSS);

                                        string strCaption = S(df.ColumnInfo.Caption);
                                        if (null != df.GroupInfo)
                                            strCaption += string.Format("({0})", S(GetGroupText(df.GroupInfo.GroupType)));

                                        switch (df.SortOrder)
                                        {
                                            case System.Data.SqlClient.SortOrder.Ascending:
                                                strCaption += this.SortText_ASC;
                                                break;
                                            case System.Data.SqlClient.SortOrder.Descending:
                                                strCaption += this.SortText_DESC;
                                                break;
                                        }

                                        cell.Text = strCaption;
                                    }
                                }
                            }

                            break;
                        case Telerik.Web.UI.GridItemType.Item:
                        case Telerik.Web.UI.GridItemType.AlternatingItem:
                        case Telerik.Web.UI.GridItemType.SelectedItem:
                            dr2 = (e.Item.DataItem as DataRowView).Row;

                            arg.DataRow = dr2;
                            arg.DataRowIndex = e.Item.ItemIndex;
                            arg.ItemType = EnumItemType.DataRow;
                            for (int i = 0; i < this.BindGridColumn.Count; i++)
                            {
                                UserViewManager.GridColumn gc = this.BindGridColumn[i];

                                if (1 < gc.DataFields.Count)
                                {
                                    // 結合列の場合
                                    for (int c = 0; c < gc.DataFields.Count; c++)
                                    {
                                        UserViewManager.DataField df = gc.DataFields[c];
                                        string strFieldName = (null != df.GroupInfo) ? df.GroupInfo.GroupFieldName : df.ColumnInfo.FieldName;
                                        TableCell cell = arg.TableCells[strFieldName];
                                        cell.Text = GetText(df, dr2);
                                        if ("" == cell.Text.Trim()) cell.Text = "&nbsp;";
                                    }
                                }
                                else
                                {
                                    DataField df = gc.DataFields[0];
                                    string strFieldName = (null != df.GroupInfo) ? df.GroupInfo.GroupFieldName : df.ColumnInfo.FieldName;
                                    TableCell cell = arg.TableCells[strFieldName];
                                    cell.Text = GetText(df, dr2);
                                    if ("" == cell.Text.Trim()) cell.Text = "&nbsp;";
                                }
                            }
                            break;
                        case Telerik.Web.UI.GridItemType.Footer:
                            arg.ItemType = EnumItemType.Footer;

                            break;

                    }
                    #endregion

                    #region コールバック
                    // コールバック
                    if (null != this._DataBoundEventHandler)
                        this._DataBoundEventHandler(arg);

                    // 結合列をViewStateに保存する為、動的コントロール(Table)をHTML化して書き込む
                    for (int i = 0; i < this.BindGridColumn.Count; i++)
                    {
                        UserViewManager.GridColumn gc = this.BindGridColumn[i];
                        if (1 == gc.DataFields.Count && gc.DataFields[0].ColumnInfo.ColumnType == Core.Sql.EnumColumnType.RadGridColumn) continue;    // ★★★★★既存列の場合はスキップ
                        Telerik.Web.UI.GridColumn col = gc.WebUI_GridColumn;

                        if (1 < gc.DataFields.Count)
                        {
                            // 結合列の場合
                            for (int c = 0; c < gc.DataFields.Count; c++)
                            {
                                UserViewManager.DataField df = gc.DataFields[c];

                                if (c < e.Item.Cells.Count)
                                {
                                    // ★これが無いとAjaxで更新時にヘッダー文字が消える
                                    col.HeaderText = e.Item.Cells[c].Text;
                                }
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    bHeader = true;
                    int nIndex = 0;
                    bool bStart = false;
                    for (int n = 0; n < e.Item.Cells.Count; n++)
                    {
                        TableCell tc = e.Item.Cells[n];

                        // Groupを2つ以上作成する場合はプログラミング適当に変更してください
                        if (tc.Text == "Group1")
                        {
                            this.AddCss(e.Item.Cells[n], UserView.GetCss(Core.Sql.ColumnInfo.EnumTextAlign.Center));
                            arg.TableCells.Add("Group1", e.Item.Cells[n]);

                            for (int i = 0; i < this.BindGridColumn.Count; i++)
                            {
                                UserViewManager.GridColumn gc = this.BindGridColumn[i];

                                if (1 == gc.DataFields.Count && gc.DataFields[0].ColumnInfo.ColumnType == Core.Sql.EnumColumnType.RadGridColumn) continue;    // ★★★★★既存列の場合はスキップ

                                if (1 < gc.DataFields.Count)
                                {
                                    // 結合列の場合
                                    for (int c = 0; c < gc.DataFields.Count; c++)
                                    {
                                        nIndex++;
                                    }
                                }
                            }
                            continue;
                        }

                        bool bAdd = false;

                        for (int i = 0; i < this.BindGridColumn.Count; i++)
                        {
                            UserViewManager.GridColumn gc = this.BindGridColumn[i];

                            if (1 == gc.DataFields.Count && gc.DataFields[0].ColumnInfo.ColumnType == Core.Sql.EnumColumnType.RadGridColumn) continue;    // ★★★★★既存列の場合はスキップ

                            Telerik.Web.UI.GridColumn col = gc.WebUI_GridColumn;

                            int nColIndex = col.OrderIndex;

                            if (nColIndex != nIndex)
                                continue;

                            bStart = true;
                            bAdd = true;
                            if (1 == gc.DataFields.Count)
                            {
                                this.AddCss(e.Item.Cells[n], UserView.GetCss(gc.DataFields[0].ColumnInfo.TextAlign));
                                arg.TableCells.Add(gc.DataFields[0].ColumnInfo.FieldName, e.Item.Cells[n]);
                            }
                            else
                            {
                                if (e.Item.ItemType != Telerik.Web.UI.GridItemType.Header)
                                {
                                    // 結合列の場合
                                    for (int c = 0; c < gc.DataFields.Count; c++)
                                    {
                                        UserViewManager.DataField df = gc.DataFields[c];

                                        this.AddCss(e.Item.Cells[n], UserView.GetCss(gc.DataFields[c].ColumnInfo.TextAlign));
                                        arg.TableCells.Add(gc.DataFields[c].ColumnInfo.FieldName, e.Item.Cells[n]);

                                        n++;
                                        bAdd = false;
                                    }
                                }
                            }
                        }

                        //if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Header)
                        if (bStart && !bAdd)
                        {
                            n--;
                        }

                        nIndex++;
                    }

                    #region データ内容
                    switch (e.Item.ItemType)
                    {
                        case Telerik.Web.UI.GridItemType.Header:

                            arg.ItemType = EnumItemType.Header;
                            e.Item.CssClass = this.HeaderCss;

                            for (int i = 0; i < this.BindGridColumn.Count; i++)
                            {
                                UserViewManager.GridColumn gc = this.BindGridColumn[i];

                                if (1 == gc.DataFields.Count)
                                {
                                    DataField df = gc.DataFields[0];
                                    string strFieldName = (null != df.GroupInfo) ? df.GroupInfo.GroupFieldName : df.ColumnInfo.FieldName;

                                    TableCell cell = arg.TableCells[strFieldName];

                                    AddCss(cell, gc.HeaderCSS);

                                    string strCaption = S(df.ColumnInfo.Caption);
                                    if (null != df.GroupInfo) strCaption += string.Format("({0})", S(GetGroupText(df.GroupInfo.GroupType)));

                                    switch (df.SortOrder)
                                    {
                                        case System.Data.SqlClient.SortOrder.Ascending:
                                            strCaption += this.SortText_ASC;
                                            break;
                                        case System.Data.SqlClient.SortOrder.Descending:
                                            strCaption += this.SortText_DESC;
                                            break;
                                    }

                                    cell.Text = strCaption;
                                }
                                else
                                {
                                    // 結合列の場合
                                    for (int c = 0; c < gc.DataFields.Count; c++)
                                    {
                                        UserViewManager.DataField df = gc.DataFields[c];
                                        string strFieldName = (null != df.GroupInfo) ? df.GroupInfo.GroupFieldName : df.ColumnInfo.FieldName;
                                        TableCell cell = arg.TableCells[strFieldName];
                                        this.AddCss(cell, this.HeaderCss);
                                        this.AddCss(cell, gc.HeaderCSS);

                                        string strCaption = S(df.ColumnInfo.Caption);
                                        if (null != df.GroupInfo)
                                            strCaption += string.Format("({0})", S(GetGroupText(df.GroupInfo.GroupType)));

                                        switch (df.SortOrder)
                                        {
                                            case System.Data.SqlClient.SortOrder.Ascending:
                                                strCaption += this.SortText_ASC;
                                                break;
                                            case System.Data.SqlClient.SortOrder.Descending:
                                                strCaption += this.SortText_DESC;
                                                break;
                                        }

                                        cell.Text = strCaption;
                                    }
                                }
                            }

                            break;
                        case Telerik.Web.UI.GridItemType.Item:
                        case Telerik.Web.UI.GridItemType.AlternatingItem:
                        case Telerik.Web.UI.GridItemType.SelectedItem:
                            dr2 = (e.Item.DataItem as DataRowView).Row;

                            arg.DataRow = dr2;
                            arg.DataRowIndex = e.Item.ItemIndex;
                            arg.ItemType = EnumItemType.DataRow;
                            for (int i = 0; i < this.BindGridColumn.Count; i++)
                            {
                                UserViewManager.GridColumn gc = this.BindGridColumn[i];

                                if (1 < gc.DataFields.Count)
                                {
                                    // 結合列の場合
                                    for (int c = 0; c < gc.DataFields.Count; c++)
                                    {
                                        UserViewManager.DataField df = gc.DataFields[c];
                                        string strFieldName = (null != df.GroupInfo) ? df.GroupInfo.GroupFieldName : df.ColumnInfo.FieldName;
                                        TableCell cell = arg.TableCells[strFieldName];
                                        cell.Text = GetText(df, dr2);
                                        if ("" == cell.Text.Trim()) cell.Text = "&nbsp;";
                                    }
                                }
                                else
                                {
                                    DataField df = gc.DataFields[0];
                                    string strFieldName = (null != df.GroupInfo) ? df.GroupInfo.GroupFieldName : df.ColumnInfo.FieldName;
                                    TableCell cell = arg.TableCells[strFieldName];
                                    cell.Text = GetText(df, dr2);
                                    if ("" == cell.Text.Trim()) cell.Text = "&nbsp;";
                                }
                            }
                            break;
                        case Telerik.Web.UI.GridItemType.Footer:
                            arg.ItemType = EnumItemType.Footer;

                            break;

                    }
                    #endregion

                    #region コールバック
                    // コールバック
                    if (null != this._DataBoundEventHandler)
                        this._DataBoundEventHandler(arg);

                    //// 結合列をViewStateに保存する為、動的コントロール(Table)をHTML化して書き込む
                    //nIndex = 0;
                    //bStart = false;
                    //for (int n = 0; n < e.Item.Cells.Count; n++)
                    //{
                    //    TableCell tc = e.Item.Cells[n];

                    //    bool bAdd = false;

                    //    for (int i = 0; i < this.BindGridColumn.Count; i++)
                    //    {
                    //        UserViewManager.GridColumn gc = this.BindGridColumn[i];
                    //        if (1 == gc.DataFields.Count && gc.DataFields[0].ColumnInfo.ColumnType == Core.Sql.EnumColumnType.RadGridColumn) continue;    // ★★★★★既存列の場合はスキップ
                    //        Telerik.Web.UI.GridColumn col = gc.WebUI_GridColumn;
                    //        if (1 < gc.DataFields.Count)
                    //        {
                    //            if (e.Item.ItemType != Telerik.Web.UI.GridItemType.Header)
                    //            {
                    //                // 結合列の場合
                    //                for (int c = 0; c < gc.DataFields.Count; c++)
                    //                {
                    //                    //if (InnerTableRenderByText)
                    //                    //{
                    //                    //    _sb.Length = 0;
                    //                    //    Table t = InnerTables[i];

                    //                    //    t.RenderControl(_writer);
                    //                    //    _writer.Flush();
                    //                    //    e.Item.Cells[n].Text = _sb.ToString();  // この処理で内部テーブルがViewstateに保存される。
                    //                    //}

                    //                    n++;
                    //                    bAdd = false;
                    //                }

                    //                continue;
                    //            }
                    //        }

                    //        if (col.OrderIndex != nIndex)
                    //            continue;

                    //        bStart = true;
                    //        bAdd = true;
                    //        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Header)
                    //        {
                    //            // ★これが無いとAjaxで更新時にヘッダー文字が消える
                    //            //col.HeaderText = e.Item.Cells[n].Text;
                    //        }
                    //        else if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Footer)
                    //        {
                    //            col.FooterText = e.Item.Cells[n].Text;
                    //        }
                    //    }

                    //    if (bStart && !bAdd)
                    //    {
                    //        n--;
                    //    }

                    //    nIndex++;
                    //}
                    #endregion
                }
            }
		}


        public class GridColumn
        {
            public string HeaderCSS
            {
                get;
                set;
            }

            public Telerik.Web.UI.GridColumn WebUI_GridColumn
            {
                get;
                set;
            }

            /// <summary>
            /// RadGrid生成前にUniqueNameを取得したいケースが有る為
            /// </summary>
            public string WebUI_GridColumnUniqueName
            {
                get {
                    if (null != WebUI_GridColumn) return WebUI_GridColumn.UniqueName;
                    if (0 == DataFields.Count) return null;
                    string [] str = new string[DataFields.Count];
                    for (int i = 0; i < DataFields.Count; i++) {
                        str[i] = DataFields[i].ColumnInfo.FieldName;
                    }
                    return UniqueNamePrefix + string.Join("/", str);
                }
            }

            public System.Collections.Generic.List<DataField> DataFields
            {
                get;
                set;
            }


            public System.Collections.Generic.List<DataField> VisibleDataFields
            {
                get {
                    System.Collections.Generic.List<DataField> lst = new System.Collections.Generic.List<DataField>();
                    for (int i = 0; i < DataFields.Count; i++) {
                        if (DataFields[i].ForceVisible)
                            lst.Add(DataFields[i]);
                        else
                        {
                            if (DataFields[i].UserViewSettingVisible && DataFields[i].Visible)
                                lst.Add(DataFields[i]);
                        }
                    }
                    return lst;
                }
            }

            public GridColumn()
            {
                DataFields = new List<DataField>();
            }
        }


        public class DataField
        {
            public GridColumn GridColumn
            {
                get;
                private set;
            }

            public System.Data.SqlClient.SortOrder SortOrder
            {
                get;
                set;
            }

            public int SortNo
            {
                get;
                set;
            }

            public string SortExpression
            {
                get {
                    switch (this.SortOrder) { 
                        case System.Data.SqlClient.SortOrder.Ascending:
                            return this.ColumnInfo.FieldName + " ASC";
                        case System.Data.SqlClient.SortOrder.Descending:
                            return this.ColumnInfo.FieldName + " DESC";
                    }
                    return "";
                }
            }


            public EditDataField EditDataField
            {
                get {
                    EditDataField e = new EditDataField();
                    e.Caption = this.ColumnInfo.Caption;
                    e.ColumnType = this.ColumnInfo.ColumnType;
                    e.FieldName = this.ColumnInfo.FieldName;
                    e.Visible = this.Visible;
                    e.ForceVisible = this.ForceVisible;
                    return e;
                }
            }


            private Core.Sql.ColumnInfo _ColumnInfo = null;

            /// <summary>
            /// グループ列の場合は、グループのColumnInfoを返す.
            /// </summary>
            public Core.Sql.ColumnInfo ColumnInfo
            {
                get
                {
                    if (null != GroupInfo) return GroupInfo.ColumnInfo;
                    return _ColumnInfo;
                }
                set {
                    _ColumnInfo = value;
                }
            }

            public Core.Sql.GroupInfo GroupInfo
            {
                get;
                private set;
            }


            private bool _bVisible = true;
            public bool Visible
            {
                get {
                    if (ForceVisible) return true;
                    if (!UserViewSettingVisible) return false;
                    return _bVisible;
                }
                set {
                    _bVisible = value;
                }
            }

            /// <summary>
            /// UserViewの設定で表示対象かどうか
            /// </summary>
            public bool UserViewSettingVisible
            {
                get;
                set;
            }

            /// <summary>
            /// 強制的に表示かどうか
            /// </summary>
            public bool ForceVisible
            {
                get;
                set;
            }

            public DataField(GridColumn gc, Core.Sql.ColumnInfo ColumnInfo)
            {
                this.GridColumn = gc;
                this.ColumnInfo = ColumnInfo;
                this.Visible = true;
                this.UserViewSettingVisible = true;
                this.SortOrder = System.Data.SqlClient.SortOrder.Unspecified;
                this.ForceVisible = false;
            }

            public DataField(GridColumn gc, Core.Sql.GroupInfo GroupInfo)
            {
                this.GridColumn = gc;
                this.GroupInfo = GroupInfo;
                this.Visible = true;
                this.UserViewSettingVisible = true;
                this.SortOrder = System.Data.SqlClient.SortOrder.Unspecified;
                this.ForceVisible = false;
            }

        }




        /*不要
        private static System.Collections.Generic.List<GridColumn>
            GetColumnsSetting(Core.Sql.SqlDataFactory f, string strUserID, ref string strSort)
		{
            System.Collections.Generic.List<GridColumn> lst = new System.Collections.Generic.List<GridColumn>();

            int nListID = f.T_UserListRow.ListID;


			// 標準設定を読み込む
			Dataset.T_UserViewRow drDefault = 
				UserViewClass.getT_UserViewRow(nListID, "", UserViewClass.EnumType.VIEW, "", Global.GetConnection());

            
            Dataset.T_UserViewRow dr = null;

			if ("" != strUserID) 
			{
				dr = UserViewClass.getT_UserViewRow(nListID, strUserID, UserViewClass.EnumType.VIEW, "", Global.GetConnection());
				if (null == dr)
					dr = drDefault;	// デフォルトの設定を使用する。
			}
			else
				dr = drDefault;

            if (null == dr) {
                dr = new Dataset.T_UserViewDataTable().NewT_UserViewRow();
                dr.ListID = nListID;
                dr.Sort = "";
                dr.Columns = "";
                dr.UserID = strUserID;
            }
            if (null != drDefault) {
                if ("" == dr.Columns) dr.Columns = drDefault.Columns;
                if ("" == dr.Columns) dr.Sort = drDefault.Sort;                
            }

            strSort = dr.Sort;
            
            // 表示するフィールド取得(もともと非表示の項目は無視する)
            System.Collections.Generic.Dictionary<string, Core.Sql.ColumnInfo> tblActiveColumns = 
                new System.Collections.Generic.Dictionary<string, Core.Sql.ColumnInfo>();
            for (int i = 0; i < f.Columns.Count; i++) {
                if (!f.Columns[i].Hide)
                    tblActiveColumns.Add(f.Columns[i].FieldName, f.Columns[i]);
            }

            System.Collections.Generic.List<string> lstSelectedColName = new System.Collections.Generic.List<string>(); // 実際に表示されるフィールド名

            if ("" != dr.Columns)
            {
                System.Collections.Generic.List<string> lstSelectedColumnName = new System.Collections.Generic.List<string>(dr.Columns.Split('\t'));
                for (int i = 0; i < lstSelectedColumnName.Count; i++)
                {
                    string strColName = lstSelectedColumnName[i];
                    if ("" == strColName) continue;
                    string[] str = strColName.Split(',');
                    GridColumn gc = new GridColumn();
                    for (int c = 0; c < str.Length; c++)
                    {
                        string strName = str[c];
                        if (!tblActiveColumns.ContainsKey(strName)) continue;
                        DataField df = new DataField(gc, tblActiveColumns[strName]);
                        df.UserViewSettingVisible = true;
                        gc.DataFields.Add(df);
                        lstSelectedColName.Add(str[c]);
                    }
                    if (0 == gc.DataFields.Count) continue;
                    lst.Add(gc);
                }

                // 非選択カラム
                string[] strActiveColumnName = new string[tblActiveColumns.Count];
                tblActiveColumns.Keys.CopyTo(strActiveColumnName, 0);
                for (int i = 0; i < strActiveColumnName.Length; i++)
                {
                    string strName = strActiveColumnName[i];
                    if (!lstSelectedColName.Contains(strName))
                    {
                        GridColumn gc = new GridColumn();
                        DataField df = new DataField(gc, tblActiveColumns[strName]);
                        gc.DataFields.Add(df);
                        if (tblActiveColumns[strName].ColumnType == Core.Sql.EnumColumnType.RadGridColumn)
                            df.UserViewSettingVisible = true;// ★★★★★★★★★★規定のカラムは必ず表示にする。(現状敢えて非表示にしているかどうか識別できない)
                        else
                            df.UserViewSettingVisible = false;
                        lst.Add(gc);
                    }
                }
            }
            else
            {
                string[] strActiveColumnName = new string[tblActiveColumns.Count];
                tblActiveColumns.Keys.CopyTo(strActiveColumnName, 0);
                for (int i = 0; i < strActiveColumnName.Length; i++)
                {
                    string strName = strActiveColumnName[i];
                    GridColumn gc = new GridColumn();
                    DataField df = new DataField(gc, tblActiveColumns[strName]);

                    gc.DataFields.Add(df);
                    lst.Add(gc);
                }
            }


            return lst;

		}
         */

	}
}




/*
public class MyGridTemplateColumnTemplate : ITemplate
{
    #region ITemplate Members

    public void InstantiateIn(Control container)
    {
        TextBox textBox = new TextBox();
        textBox.ID = "TextBox1";
        textBox.DataBinding += new EventHandler(textBox_DataBinding);
        container.Controls.Add(textBox);
    }

    void textBox_DataBinding(object sender, EventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        GridDataItem item = (GridDataItem)item.NamingContainer;
        int itemId = (int)DataBinder.Eval(item.DataItem, "ID");
        //In this way, you can access data values and customize
        //the control firing DataBinding event for each cell
    }

    #endregion
}
*/