using System;
using System.Data.SqlClient;

namespace KoubaiDAL
{
    [Serializable]
    public class HonyakuClass
    {
        public class Honyaku
        {
            HonyakuManager _manager = null;
            public string LanguageCode
            {
                get;
                set;
            }

            internal Honyaku(HonyakuManager manager, string strLanguageCode)
            {
                this._manager = manager;
                LanguageCode = strLanguageCode;
            }

            public string GetString(string strOriginal)
            {
                return this._manager.GetString(strOriginal, this.LanguageCode);
            }
        }

        /// <summary>
        /// 翻訳
        /// </summary>
        public class HonyakuManager
        {
            private SqlConnection _sqlConn = null;
            private string _strOriginalLanguageCode = "ja";
            private string[] _strLanguageCodes = null;
            SqlDataAdapter _da = null;
            KoubaiDataSet.M_HonyakuDataTable _dt = null;

            /// <summary>
            /// 指定の言語に翻訳するTranslatorクラスのオブジェクト取得
            /// </summary>
            /// <param name="strLanguageCode"></param>
            /// <returns></returns>
            public Honyaku GetHonyaku(string strLanguageCode)
            {
                return new Honyaku(this, strLanguageCode);
            }

            public HonyakuManager(string[] strLanguageCodes, SqlConnection sqlConn)
            {
                this._strLanguageCodes = strLanguageCodes;
                this._sqlConn = sqlConn;
                _da = new SqlDataAdapter("select * from M_Honyaku", this._sqlConn);
                _da.InsertCommand = new SqlCommandBuilder(_da).GetInsertCommand();
                this.Load();
            }

            public void Update(string strOriginal, SqlConnection sqlConn)
            {
                if (null == this._dt) return;
                KoubaiDataSet.M_HonyakuRow dr = this._dt.FindByOriginal(strOriginal);
                KoubaiDataSet.M_HonyakuRow d = HonyakuClass.getM_HonyakuRow(strOriginal, sqlConn);
                if (null != dr && null != d)
                {
                    dr.ItemArray = d.ItemArray;
                }
                this._dt.AcceptChanges();
            }

            private void Load()
            {
                lock (this)
                {
                    if (null != this._dt)
                        this._dt.Clear();
                    this._dt = HonyakuClass.getM_HonyakuDataTable(this._sqlConn);
                }
            }

            /// <summary>
            /// 翻訳を取得
            /// </summary>
            /// <param name="strOriginal"></param>
            /// <param name="strLanguageCode"></param>
            /// <returns></returns>
            public string GetString(string strOriginal, string strLanguageCode)
            {
                if (strLanguageCode.Equals(this._strOriginalLanguageCode)) return strOriginal;
                if (string.IsNullOrEmpty(strOriginal)) return strOriginal;

                // $  ^  {  }  [  ]  (  )  |  *  +  ?  \  は\でエスケープすること
                if (System.Text.RegularExpressions.Regex.Match(strOriginal, @"^[ #\$\?%&';:@\+\-\*/=\[\]a-zA-Z0-9]+$").Success)
                {
                    return strOriginal;
                }

                KoubaiDataSet.M_HonyakuRow d = this._dt.FindByOriginal(strOriginal);
                if (null != d)
                {
                    string str = Convert.ToString(d[strLanguageCode]);
                    if (str.Equals(""))
                    {
                        return strOriginal;
                        //return (this._strOriginalLanguageCode == strLanguageCode) ? strOriginal : string.Format("{0}[not translated]", strOriginal);
                    }
                    else
                        return str;
                }
                else
                {
                    // なければ新規登録
                    KoubaiDataSet.M_HonyakuRow dr = this._dt.NewM_HonyakuRow();
                    dr.Original = strOriginal;

                    for (int i = 0; i < this._strLanguageCodes.Length; i++)
                    {
                        dr[this._strLanguageCodes[i]] = "";
                    }
                    dr[this._strOriginalLanguageCode] = strOriginal;

                    dr.TourokuBi = DateTime.Now;

                    if (null != System.Web.HttpContext.Current && null != System.Web.HttpContext.Current.Request)
                        dr.page = System.Web.HttpContext.Current.Request.Path;
                    else
                        dr.page = "";

                    try
                    {
                        this._dt.Rows.Add(dr);
                        _da.Update(_dt);
                    }
                    catch
                    {
                    }

                    return (this._strOriginalLanguageCode == strLanguageCode) ? strOriginal : strOriginal;/*string.Format("{0}[not translated]", strOriginal);*/
                }
            }
        }


        public static KoubaiDataSet.M_HonyakuDataTable
            getM_HonyakuDataTable(SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Honyaku";
            KoubaiDataSet.M_HonyakuDataTable dt = new KoubaiDataSet.M_HonyakuDataTable();
            da.Fill(dt);
            return dt;
        }

        public static KoubaiDataSet.M_HonyakuRow
            getM_HonyakuRow(string strOriginal, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Honyaku WHERE Original = @r";
            da.SelectCommand.Parameters.AddWithValue("@r", strOriginal);
            KoubaiDataSet.M_HonyakuDataTable dt = new KoubaiDataSet.M_HonyakuDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return dt[0];
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="sqlConn"></param>
        public static Core.Error
            Insert(KoubaiDataSet.M_HonyakuRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Honyaku where Original=@c";

            da.SelectCommand.Parameters.AddWithValue("@c", dr.Original);

            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();

            KoubaiDataSet.M_HonyakuDataTable dt = new KoubaiDataSet.M_HonyakuDataTable();
            da.Fill(dt);
            if (0 != dt.Count) return null;

            dr.TourokuBi = DateTime.Now;
            dt.LoadDataRow(dr.ItemArray, false);
            da.Update(dt);
            return null;
        }

        public class KensakuParam
        {
            public string strLanguageCode = null;   // ja,zh,en,・・・
            public Core.Sql.FilterItem objText = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="info"></param>
        /// <param name="sqlConn"></param>
        /// <param name="nRecCount"></param>
        /// <returns></returns>
        public static KoubaiDataSet.M_HonyakuDataTable
            getM_HonyakuDataTable(KensakuParam p, Core.Sql.RowNumberInfo info, SqlConnection sqlConn, ref int nRecCount)
        {
            nRecCount = 0;

            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Honyaku ";

            Core.Sql.WhereGenerator w = new Core.Sql.WhereGenerator();

            if (null != p)
            {

                if (!string.IsNullOrEmpty(p.strLanguageCode))
                {
                    string strField = p.strLanguageCode;
                    if (p.strLanguageCode == "ja")
                        strField = "Original";
                    if (null != p.objText)
                    {
                        string str = p.objText.GetFilterText(strField, "@text", da.SelectCommand);
                        w.Add(str);
                    }
                }

                if (!string.IsNullOrEmpty(w.WhereText))
                {
                    da.SelectCommand.CommandText += " where " + w.WhereText;
                }
            }

            KoubaiDataSet.M_HonyakuDataTable dt = new KoubaiDataSet.M_HonyakuDataTable();

            if (null != info)
            {
                info.LoadData(da.SelectCommand, sqlConn, dt, ref nRecCount);
            }
            else
            {
                da.Fill(dt);
                nRecCount = dt.Count;
            }
            return dt;
        }

        public static Core.Error Update(string strOriginal, string strChina, string strEn, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Honyaku WHERE Original = @Original";
            da.SelectCommand.Parameters.AddWithValue("@Original", strOriginal);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            KoubaiDataSet.M_HonyakuDataTable dt = new KoubaiDataSet.M_HonyakuDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new Core.Error("Error");
            KoubaiDataSet.M_HonyakuRow drThis = (KoubaiDataSet.M_HonyakuRow)dt.Rows[0];
            try
            {
                //drThis.ja = dr.ja;
                drThis.zh = strChina;
                drThis.en = strEn;
                drThis.TourokuBi = DateTime.Now;
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new Core.Error(e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class HonyakuKey
        {
            public string Original
            {
                get;
                set;
            }
            public string zh
            {
                get;
                set;
            }
            public string en
            {
                get;
                set;
            }
        }

        public static Core.Error SetM_Honyaku(HonyakuKey[] array, string strTourokuUserID, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Honyaku WHERE Original = @Original";
            da.SelectCommand.Parameters.AddWithValue("@Original", "");

            da.InsertCommand = new SqlCommandBuilder(da).GetInsertCommand();
            da.UpdateCommand = new SqlCommandBuilder(da).GetUpdateCommand();

            KoubaiDataSet.M_HonyakuDataTable dt = new KoubaiDataSet.M_HonyakuDataTable();
            SqlTransaction t = null;
            try
            {
                sqlConn.Open();
                t = sqlConn.BeginTransaction();
                da.SelectCommand.Transaction = da.InsertCommand.Transaction = da.UpdateCommand.Transaction = t;

                for (int i = 0; i < array.Length; i++)
                {
                    da.SelectCommand.Parameters["@Original"].Value = array[i].Original;
                    dt.Clear();
                    da.Fill(dt);
                    KoubaiDataSet.M_HonyakuRow dr = null;

                    if (1 == dt.Count)
                    {
                        dt[0].zh = array[i].zh;
                        dt[0].en = array[i].en;
                        dt[0].TourokuBi = DateTime.Now;
                    }
                    else
                    {
                        dr = dt.NewM_HonyakuRow();
                        dr.Original = array[i].Original;
                        dr.zh = array[i].zh;
                        dr.en = array[i].en;
                        dr.TourokuBi = DateTime.Now;
                        dt.Rows.Add(dr);
                    }
                    da.Update(dt);
                }

                t.Commit();
                return null;
            }
            catch (Exception e)
            {
                return new Core.Error(e);
            }
            finally
            {
                sqlConn.Close();
            }
        }
    }
}
