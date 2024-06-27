using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
namespace m2mKoubaiDAL
{
    public class NounyuuBashoClass
    {
        /// <summary>
        /// 検索条件
        /// </summary>
        public class KensakuParam
        {
            public string _BashoCode = "";
            public string _BashoName = "";
        }

        private static string WhereText(KensakuParam k, SqlCommand cmd)
        {
            Core.Sql.WhereGenerator w = new Core.Sql.WhereGenerator();

            if (k._BashoCode != "")
            {               

                w.Add(string.Format("M_NounyuuBasho.BashoCode LIKE @BashoCode "));
                cmd.Parameters.AddWithValue("@BashoCode", k._BashoCode + "%");
            }

            if (k._BashoName != "")
            {
                w.Add(string.Format("M_NounyuuBasho.BashoMei LIKE @BashoMei "));
                cmd.Parameters.AddWithValue("@BashoMei", "%" + k._BashoName + "%");
            }

            return w.WhereText;
        }

        /// <summary>
        /// 検索条件によって、全てデータを取得
        /// </summary>
        /// <param name="k"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static m2mKoubaiDataSet.M_NounyuuBashoDataTable getM_NounyuuBashoDataTable(KensakuParam k, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_NounyuuBasho ";
            string strWhere = WhereText(k, da.SelectCommand);
            if (strWhere != "")
            {
                da.SelectCommand.CommandText += "WHERE " + strWhere;
            }
            m2mKoubaiDataSet.M_NounyuuBashoDataTable dt = new m2mKoubaiDataSet.M_NounyuuBashoDataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        ///全てデータを取得
        /// </summary>
        /// <param name="k"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static m2mKoubaiDataSet.M_NounyuuBashoDataTable getM_NounyuuBashoDataTable( SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_NounyuuBasho ";
           
            m2mKoubaiDataSet.M_NounyuuBashoDataTable dt = new m2mKoubaiDataSet.M_NounyuuBashoDataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 主キーによって、でーたを取得
        /// </summary>
        /// <param name="BashoCode"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static m2mKoubaiDataSet.M_NounyuuBashoRow getM_NounyuuBashoRow(string BashoCode, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_NounyuuBasho WHERE BashoCode = @BashoCode";
            da.SelectCommand.Parameters.AddWithValue("@BashoCode", BashoCode);
            m2mKoubaiDataSet.M_NounyuuBashoDataTable dt = new m2mKoubaiDataSet.M_NounyuuBashoDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (m2mKoubaiDataSet.M_NounyuuBashoRow)dt.Rows[0];
            else
                return null;
        }
 
        /// <summary>
        /// 納入場所の登録
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LibError M_NounyuuBasho_Insert(m2mKoubaiDataSet.M_NounyuuBashoRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_NounyuuBasho";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            m2mKoubaiDataSet.M_NounyuuBashoDataTable dt = new m2mKoubaiDataSet.M_NounyuuBashoDataTable();
            m2mKoubaiDataSet.M_NounyuuBashoRow drNew = dt.NewM_NounyuuBashoRow();
            try
            {
                drNew.BashoCode = dr.BashoCode;
                drNew.BashoMei = dr.BashoMei;
                dt.Rows.Add(drNew);
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
        }

        /// <summary>
        /// 納入場所の更新
        /// </summary>
        /// <param name="BashoCode"></param>
        /// <param name="dr"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LibError M_NounyuuBasho_Update(string BashoCode, m2mKoubaiDataSet.M_NounyuuBashoRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_NounyuuBasho WHERE BashoCode = @BashoCode";
            da.SelectCommand.Parameters.AddWithValue("@BashoCode", BashoCode);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            m2mKoubaiDataSet.M_NounyuuBashoDataTable dt = new m2mKoubaiDataSet.M_NounyuuBashoDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new LibError("エラー");
            try
            {
                m2mKoubaiDataSet.M_NounyuuBashoRow drThis = (m2mKoubaiDataSet.M_NounyuuBashoRow)dt.Rows[0];
                drThis.BashoMei = dr.BashoMei;
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
        }

        /// <summary>
        /// 納入場所の削除
        /// </summary>
        /// <param name="BashoCode"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LibError M_NounyuuBasho_Delete(string BashoCode, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_NounyuuBasho WHERE BashoCode = @BashoCode";
            da.SelectCommand.Parameters.AddWithValue("@BashoCode", BashoCode);
            da.DeleteCommand = (new SqlCommandBuilder(da)).GetDeleteCommand();
            m2mKoubaiDataSet.M_NounyuuBashoDataTable dt = new m2mKoubaiDataSet.M_NounyuuBashoDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new LibError("エラー");
            try
            {
                m2mKoubaiDataSet.M_NounyuuBashoRow drThis = (m2mKoubaiDataSet.M_NounyuuBashoRow)dt.Rows[0];
                drThis.Delete();
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
        }

        /// <summary>
        ///　NewRow
        /// </summary>
        /// <returns></returns>
        public static m2mKoubaiDataSet.M_NounyuuBashoRow newM_NounyuuBashoRow()
        {
            return new m2mKoubaiDataSet.M_NounyuuBashoDataTable().NewM_NounyuuBashoRow();
        }
    }
}
