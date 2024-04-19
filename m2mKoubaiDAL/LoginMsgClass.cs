using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace m2mKoubaiDAL
{
    public class LoginMsgClass
    {
        public class KensakuParam
        {
            public int _Flag = -1;     // 有効/無効
        }
        // 検索条件
        private static string WhereText(KensakuParam k)
        {
            Core.Sql.WhereGenerator w = new Core.Sql.WhereGenerator();
            string str = "";
           
            // 削除フラグ
            if (k._Flag > -1)
            {
                str = string.Format("M_LoginMsg.DelFlg = {0} ", k._Flag);
                w.Add(str);
            }
            return w.WhereText;
        }
        /// <summary>
        /// 有効のデータのみ取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static m2mKoubaiDataSet.M_LoginMsgDataTable
            getM_LoginMsgDataTable(SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_LoginMsg WHERE  DelFlg = 0 ";
            m2mKoubaiDataSet.M_LoginMsgDataTable dt = new m2mKoubaiDataSet.M_LoginMsgDataTable();
            da.Fill(dt);
            return dt;
        }

        public static m2mKoubaiDataSet.M_LoginMsgDataTable
            getM_LoginMsgDataTable(KensakuParam k, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_LoginMsg ";
            string strWhere = WhereText(k);
            if (strWhere != "")
            {
                da.SelectCommand.CommandText += "WHERE " + strWhere;
            }
            da.SelectCommand.CommandText += "ORDER BY TourokuBi DESC ";
            m2mKoubaiDataSet.M_LoginMsgDataTable dt = new m2mKoubaiDataSet.M_LoginMsgDataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 全てのデータを取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static m2mKoubaiDataSet.M_LoginMsgDataTable
           getM_LoginMsg2DataTable(SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_LoginMsg ";
            m2mKoubaiDataSet.M_LoginMsgDataTable dt = new m2mKoubaiDataSet.M_LoginMsgDataTable();
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        /// 主キーによって、データを取得
        /// </summary>
        /// <param name="MsgID"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static m2mKoubaiDataSet.M_LoginMsgRow
            getM_LoginMsgRow(int MsgID, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_LoginMsg WHERE MsgID = @MsgID";
            da.SelectCommand.Parameters.Add("@MsgID", MsgID);
            m2mKoubaiDataSet.M_LoginMsgDataTable dt = new m2mKoubaiDataSet.M_LoginMsgDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (m2mKoubaiDataSet.M_LoginMsgRow)dt.Rows[0];
            else
                return null;
        }
        /// <summary>
        /// ログインメッセージを登録する
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LibError
            M_LoginMsg_Insert(m2mKoubaiDataSet.M_LoginMsgRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_LoginMsg";
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
            finally
            {
                sqlConn.Close();
            }
        }
         /// <summary>
         /// ログインメッセージを更新する
         /// </summary>
         /// <param name="MsgID"></param>
         /// <param name="dr"></param>
         /// <param name="sqlConn"></param>
         /// <returns></returns>
  
        public static LibError
            M_LoginMsg_Update(int MsgID, m2mKoubaiDataSet.M_LoginMsgRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_LoginMsg WHERE MsgID = @MsgID";
            da.SelectCommand.Parameters.Add("@MsgID", MsgID);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            m2mKoubaiDataSet.M_LoginMsgDataTable dt = new m2mKoubaiDataSet.M_LoginMsgDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new LibError("エラー");
            try
            {
                m2mKoubaiDataSet.M_LoginMsgRow drThis = (m2mKoubaiDataSet.M_LoginMsgRow)dt.Rows[0];
                drThis.Msg = dr.Msg;              
                drThis.KoushinBi = dr.KoushinBi;
                drThis.DelFlg = dr.DelFlg;
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
        }
        /// <summary>
        /// ログインメッセージを削除する
        /// </summary>
        /// <param name="MsgID"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LibError
            M_LoginMsg_Delete(int MsgID, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_LoginMsg WHERE MsgID = @MsgID";
            da.SelectCommand.Parameters.Add("@MsgID", MsgID);
            da.DeleteCommand = (new SqlCommandBuilder(da)).GetDeleteCommand();
            m2mKoubaiDataSet.M_LoginMsgDataTable dt = new m2mKoubaiDataSet.M_LoginMsgDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new LibError("エラー");
            try
            {
                m2mKoubaiDataSet.M_LoginMsgRow drThis = (m2mKoubaiDataSet.M_LoginMsgRow)dt.Rows[0];
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
        /// NewTable
        /// </summary>
        /// <returns></returns>
        public static m2mKoubaiDataSet.M_LoginMsgRow
            newM_LoginMsgRow()
        {
            return new m2mKoubaiDataSet.M_LoginMsgDataTable().NewM_LoginMsgRow();
        }
    }
}
