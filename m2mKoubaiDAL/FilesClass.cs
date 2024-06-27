using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m2mKoubaiDAL
{
    public class FilesClass
    {
        public class OptionData
        {
            public string FileName = "";
            public string ContentType = "";
            public int FileSize = 0;
            public string DataType = "";
            public string SlipID = "";
        }
        public class KensakuParam
        {
            // ユーザー区分
            public byte _userKubun = 0;
            // 取引先
            public string _KaishaCode = "";
            // 帳票種別
            public string _DataType = "";
            // 発行日
            public Core.Type.NengappiKikan _TourokuBi = null;
            // 計上日
            public Core.Type.NengappiKikan _KeijoBi = null;
        }

        /// <summary>
        /// T_Document用Where文を作成
        /// </summary>
        /// <param name="k"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private static string WhereDocument(KensakuParam k, SqlCommand cmd)
        {
            Core.Sql.WhereGenerator w = new Core.Sql.WhereGenerator();
            // 取引先コード
            if (k._KaishaCode != "")
            {
                w.Add(string.Format("(KaishaCode = '{0}')", k._KaishaCode));
            }
            // 帳票種別
            if (k._DataType != "")
            {
                w.Add(string.Format("DataType = '{0}'", k._DataType));
            }
            // 発行日
            if (k._TourokuBi != null && k._TourokuBi.KikanTypeIsNotNone)
            {
                w.Add(Core.Type.NengappiKikan.GenerateSQL(k._TourokuBi, false, "(convert(varchar,TourokuBi,112))"));
            }
            // 計上日
            if (k._KeijoBi != null && k._KeijoBi.KikanTypeIsNotNone)
            {
                w.Add(Core.Type.NengappiKikan.GenerateSQL(k._KeijoBi, false, "(convert(varchar,KeijoBi,112))"));
            }
            return w.WhereText;
        }


        public static ShareDataSet.T_DocumentRow getT_DocumentRow(string FileID, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = @"SELECT * FROM T_Document
                WHERE FileID = @FileID";
            da.SelectCommand.Parameters.AddWithValue("@FileID", FileID);
            ShareDataSet.T_DocumentDataTable dt = new ShareDataSet.T_DocumentDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return dt[0];
            else
                return null;
        }
        public static ShareDataSet.T_DocumentRow getLastT_DocumentRow(string SlipID, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = @"SELECT * FROM T_Document
                WHERE SlipID = @SlipID ORDER BY FileID DESC ";
            da.SelectCommand.Parameters.AddWithValue("@SlipID", SlipID);
            ShareDataSet.T_DocumentDataTable dt = new ShareDataSet.T_DocumentDataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                return dt[0];
            else
                return null;
        }
        public static int SaveDocument(ShareDataSet.T_DocumentRow drS, SqlConnection sqlConn)
        {
            int ReturnID = 0;
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * from T_Document ";
            da.UpdateCommand = new SqlCommandBuilder(da).GetUpdateCommand();
            da.InsertCommand = new SqlCommandBuilder(da).GetInsertCommand();
            da.InsertCommand.CommandText += ";SELECT SCOPE_IDENTITY();";
            string sql = @"SELECT MAX(FileID) FROM T_Document WHERE SlipID='" + drS.SlipID + "' ";
            SqlCommand cmd = new SqlCommand(sql, sqlConn);

            ShareDataSet.T_DocumentDataTable dt = new ShareDataSet.T_DocumentDataTable();
            SqlTransaction sqlTran = null;

            try
            {
                sqlConn.Open();
                sqlTran = sqlConn.BeginTransaction();
                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = da.InsertCommand.Transaction = cmd.Transaction = sqlTran;
                da.Fill(dt);

                ShareDataSet.T_DocumentRow drnew = dt.NewT_DocumentRow();
                for (int column = 0; column < dt.Columns.Count; column++)
                {
                    drnew[column] = drS[column];
                }
                dt.AddT_DocumentRow(drnew);
                da.Update(dt);
                sqlTran.Commit();
                object id = cmd.ExecuteScalar();
                if (!DBNull.Value.Equals(id))
                {
                    string insertId = id.ToString();
                    int.TryParse(insertId, out ReturnID);
                }
                return ReturnID;
            }
            catch (Exception ex)
            {
                if (null != sqlTran)
                    sqlTran.Rollback();
                return ReturnID;
            }
            finally
            {
                sqlConn.Close();
            }
        }
        public static int Update_T_Invoice_FileID(string strInvoiceID, int intFileID,DateTime DtInsatuBi,DateTime DtSoshinBi, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT TOP(1) InvoiceID FROM T_Invoice WHERE InvoiceID = @InvoiceID ";
            da.SelectCommand.Parameters.AddWithValue("@InvoiceID ", strInvoiceID);
            DataTable dt = new DataTable();
            da.Fill(dt);
            string strSql = "UPDATE T_Invoice SET FileID = " + intFileID;
            if(DtInsatuBi > DateTime.MinValue) 
            {
                strSql += " , InsatuBi = '" + DtInsatuBi.ToString("yyyy-MM-dd hh:mm:ss") + "' ";
            }
            if (DtSoshinBi > DateTime.MinValue)
            {
                strSql += " , SoshinBi = '" + DtSoshinBi.ToString("yyyy-MM-dd hh:mm:ss") + "' ";
            }
            strSql += " WHERE InvoiceID  = '" + strInvoiceID + "' ";
            SqlTransaction sqlTran = null;
            try
            {
                sqlConn.Open();
                sqlTran = sqlConn.BeginTransaction();

                using (SqlCommand cmd = new SqlCommand(strSql, sqlConn, sqlTran))
                    cmd.ExecuteNonQuery();
                sqlTran.Commit();
            }
            catch (Exception e)
            {
                sqlTran.Rollback();
                return -1;
            }
            return 0;

        }
        public static ShareDataSet.V_DocumentDataTable getV_DocumentDataTable(KensakuParam k, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM V_Document ";
            // WHERE 
            string strW = WhereDocument(k, da.SelectCommand);
            if (strW != "")
            {
                da.SelectCommand.CommandText += "WHERE " + strW;
            }
            da.SelectCommand.CommandText += " ORDER BY KeijoBi DESC,FileID DESC ";
            ShareDataSet.V_DocumentDataTable dt = new ShareDataSet.V_DocumentDataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 検索用の値取得
        /// </summary>
        /// <param name="strKoumoku"></param>
        /// <param name="strCode"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static DataTable getV_DocunemtColumnLoadDataTable(string strKoumoku, string strKaishaCode, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);

            da.SelectCommand.CommandText = string.Format(@"SELECT {0} FROM V_Document ", strKoumoku);

            if (strKaishaCode != "") // 受注側ログインの場合
            {
                da.SelectCommand.CommandText += string.Format(@"
                WHERE KaishaCode =  {0}
                GROUP BY {1}
                ORDER BY {1}
                ", strKaishaCode, strKoumoku);
            }
            else
            {
                da.SelectCommand.CommandText += string.Format(@"
                GROUP BY {0}
                ORDER BY {0}
                ", strKoumoku);
            }

            DataTable dt = new DataTable();

            da.Fill(dt);
            return dt;
        }












    }
}
