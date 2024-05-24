using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace m2mKoubaiDAL
{
    public class KaishaInfoClass
    {

        public class KensakuParam
        {
            // コード
            public string _Code = "";
        }
        // 検索条件
        private static string WhereText(KensakuParam k, SqlCommand cmd)
        {
            Core.Sql.WhereGenerator w = new Core.Sql.WhereGenerator();
            //string str = "";
            // 事業所コード
            if (k._Code != "")
                w.Add(string.Format("T_KaishaInfo.KaishaID = '{0}'", k._Code));

            return w.WhereText;
        }
        /// <summary>
        /// 事業所取得
        /// </summary>
        /// <param name="JigyoushoKubun"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static m2mKoubaiDataSet.T_KaishaInfoDataTable getT_KaishaInfoDataTable( SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_KaishaInfo ";
           　
            m2mKoubaiDataSet.T_KaishaInfoDataTable dt = new m2mKoubaiDataSet.T_KaishaInfoDataTable();
            da.Fill(dt);
            return dt;

        }
        /// <summary>
        /// ヨドコウ興産情報を取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static m2mKoubaiDataSet.T_KaishaInfoRow
            getT_KaishaInfoRow(SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_KaishaInfo WHERE KaishaID = 1";

            m2mKoubaiDataSet.T_KaishaInfoDataTable dt = new m2mKoubaiDataSet.T_KaishaInfoDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (m2mKoubaiDataSet.T_KaishaInfoRow)dt.Rows[0];
            else
                return null;
        }
        /// <summary>
        /// 会社情報取得
        /// </summary>
        /// <param name="JigyoushoKubun"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static m2mKoubaiDataSet.T_KaishaInfoDataTable
            getT_KaishaInfoDataTable(int JigyoushoKubun, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_KaishaInfo WHERE KaishaID = @KaishaID";
            da.SelectCommand.Parameters.AddWithValue("@KaishaID", JigyoushoKubun);
            m2mKoubaiDataSet.T_KaishaInfoDataTable dt = new m2mKoubaiDataSet.T_KaishaInfoDataTable();
            da.Fill(dt);
            return dt;
            
        }
        /// <summary>
        /// 検索条件で会社情報取得
        /// </summary>
        /// <param name="JigyoushoKubun"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static m2mKoubaiDataSet.T_KaishaInfoDataTable getT_KaishaInfoDataTable(KensakuParam k, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            //da.SelectCommand.CommandText = "SELECT * FROM T_KaishaInfo WHERE KaishaID = @KaishaID";
            da.SelectCommand.CommandText = "SELECT * FROM T_KaishaInfo ";
            string strWhere = WhereText(k, da.SelectCommand);
            if (strWhere != "")
            {
                da.SelectCommand.CommandText += "WHERE " + strWhere;
            }
            m2mKoubaiDataSet.T_KaishaInfoDataTable dt = new m2mKoubaiDataSet.T_KaishaInfoDataTable();
            da.Fill(dt);
            return dt;
        }

        public static m2mKoubaiDataSet.T_KaishaInfoRow 
            getT_KaishaInfoRow(int KaishaID, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_KaishaInfo WHERE KaishaID = @KaishaID";
            da.SelectCommand.Parameters.AddWithValue("@KaishaID", KaishaID);
            m2mKoubaiDataSet.T_KaishaInfoDataTable dt = new m2mKoubaiDataSet.T_KaishaInfoDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (m2mKoubaiDataSet.T_KaishaInfoRow)dt.Rows[0];
            else
                return null;
        }

        // 更新
        public static LibError
            T_KaishaInfo_Insert(m2mKoubaiDataSet.T_KaishaInfoRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_KaishaInfo";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            da.InsertCommand.CommandText += ";SELECT SCOPE_IDENTITY();";
            for (int i = 0; i < da.InsertCommand.Parameters.Count; i++)
                da.InsertCommand.Parameters[i].Value = dr[da.InsertCommand.Parameters[i].SourceColumn];
            try
            {
                sqlConn.Open();
                object objRet = da.InsertCommand.ExecuteScalar();
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
            finally { sqlConn.Close(); }
        }

        public static LibError
            T_KaishaInfo_Update(int KaishaID, m2mKoubaiDataSet.T_KaishaInfoRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_KaishaInfo WHERE KaishaID = @KaishaID";
            da.SelectCommand.Parameters.AddWithValue("@KaishaID", KaishaID);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            m2mKoubaiDataSet.T_KaishaInfoDataTable dt = new m2mKoubaiDataSet.T_KaishaInfoDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)            
                return new LibError("エラー");         
            try
            {
                m2mKoubaiDataSet.T_KaishaInfoRow drThis = (m2mKoubaiDataSet.T_KaishaInfoRow)dt.Rows[0];
               // drThis.KaishaMei = dr.KaishaMei;
                drThis.EigyouSho = dr.EigyouSho;
                drThis.Yuubin = dr.Yuubin;
                drThis.Address = dr.Address;
                drThis.Tel = dr.Tel;
                drThis.Fax = dr.Fax;
                drThis.Mail = dr.Mail;
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
        }
        // 削除
        public static LibError
            T_KaishaInfo_Delete(string KaishaID, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_KaishaInfo WHERE KaishaID = @KaishaID ";
            da.SelectCommand.Parameters.AddWithValue("@KaishaID", KaishaID);
            da.DeleteCommand = (new SqlCommandBuilder(da)).GetDeleteCommand();
            m2mKoubaiDataSet.T_KaishaInfoDataTable dt = new m2mKoubaiDataSet.T_KaishaInfoDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new LibError("エラー");
            try
            {
                m2mKoubaiDataSet.T_KaishaInfoRow drThis = (m2mKoubaiDataSet.T_KaishaInfoRow)dt.Rows[0];
                drThis.Delete();
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
        }
               
        public static m2mKoubaiDataSet.T_KaishaInfoRow newT_KaishaInfoRow()
        {
            return new m2mKoubaiDataSet.T_KaishaInfoDataTable().NewT_KaishaInfoRow();
        }
        /// <summary>
        /// 担当者カウント
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LoginDataSet.V_Jigyousho_CountDataTable getV_Jigyousho_CountDataTable(KensakuParam k, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT DISTINCT  "
            + "dbo.T_KaishaInfo.KaishaID, COUNT(*) AS Count, dbo.T_KaishaInfo.KaishaMei,  "
            + "dbo.T_KaishaInfo.EigyouSho, dbo.T_KaishaInfo.Address, dbo.T_KaishaInfo.Yuubin,  "
            + "dbo.T_KaishaInfo.Tel, dbo.T_KaishaInfo.Fax, dbo.T_KaishaInfo.Mail, " 
            + "dbo.M_Login.JigyoushoKubun "
            + "FROM            dbo.M_Login RIGHT OUTER JOIN "
            + "dbo.T_KaishaInfo ON  "
            + "dbo.M_Login.JigyoushoKubun = dbo.T_KaishaInfo.KaishaID ";

            string strWhere = WhereText(k, da.SelectCommand);
            if (strWhere != "")
            {
                da.SelectCommand.CommandText += "WHERE " + strWhere;
            }

            da.SelectCommand.CommandText += "GROUP BY     dbo.T_KaishaInfo.KaishaID, dbo.T_KaishaInfo.KaishaMei, "
                                        + "dbo.T_KaishaInfo.EigyouSho, dbo.T_KaishaInfo.Address, dbo.T_KaishaInfo.Yuubin, "
                                        + "dbo.T_KaishaInfo.Tel, dbo.T_KaishaInfo.Fax, dbo.T_KaishaInfo.Mail,  "
                                        + "dbo.M_Login.JigyoushoKubun ";
            da.SelectCommand.CommandText += "ORDER BY           T_KaishaInfo.KaishaID ";
            LoginDataSet.V_Jigyousho_CountDataTable dt = new LoginDataSet.V_Jigyousho_CountDataTable();
            da.Fill(dt);
            return dt;
        }



    }
}
