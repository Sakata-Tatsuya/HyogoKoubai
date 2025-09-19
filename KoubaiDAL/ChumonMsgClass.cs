using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace KoubaiDAL
{
    public class ChumonMsgClass
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static KoubaiDataSet.T_ChumonMsgDataTable getT_ChumonMsgDataTable(SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_ChumonMsg";
            KoubaiDataSet.T_ChumonMsgDataTable dt = new KoubaiDataSet.T_ChumonMsgDataTable();
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MsgID"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static KoubaiDataSet.T_ChumonMsgRow getT_ChumonMsgRow(int MsgID, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_ChumonMsg WHERE MsgID = @MsgID";
            da.SelectCommand.Parameters.AddWithValue("@MsgID", MsgID);
            KoubaiDataSet.T_ChumonMsgDataTable dt = new KoubaiDataSet.T_ChumonMsgDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (KoubaiDataSet.T_ChumonMsgRow)dt.Rows[0];
            else
                return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LibError T_ChumonMsg_Insert(KoubaiDataSet.T_ChumonMsgRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_ChumonMsg";
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MsgID"></param>
        /// <param name="dr"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LibError T_ChumonMsg_Update(int MsgID, string msg, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_ChumonMsg WHERE MsgID = @MsgID";
            da.SelectCommand.Parameters.AddWithValue("@MsgID", MsgID);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            KoubaiDataSet.T_ChumonMsgDataTable dt = new KoubaiDataSet.T_ChumonMsgDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new LibError("エラー");
            try
            {
                KoubaiDataSet.T_ChumonMsgRow drThis = (KoubaiDataSet.T_ChumonMsgRow)dt.Rows[0];
                drThis.Message = msg;
                drThis.KoushinBi = DateTime.Now;
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MsgID"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LibError T_ChumonMsg_Delete(int MsgID, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_ChumonMsg WHERE MsgID = @MsgID";
            da.SelectCommand.Parameters.AddWithValue("@MsgID", MsgID);
            da.DeleteCommand = (new SqlCommandBuilder(da)).GetDeleteCommand();
            KoubaiDataSet.T_ChumonMsgDataTable dt = new KoubaiDataSet.T_ChumonMsgDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new LibError("エラー");
            try
            {
                KoubaiDataSet.T_ChumonMsgRow drThis = (KoubaiDataSet.T_ChumonMsgRow)dt.Rows[0];
                drThis.Delete();
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
        }
        public static LibError T_ChumonMsg_Kaifuu(byte p, string p_2, int nMsgID, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "select * from T_ChumonMsg where MsgID = @nMsgID";
            da.SelectCommand.Parameters.AddWithValue("@nMsgID", nMsgID);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            KoubaiDataSet.T_ChumonMsgDataTable dt = new KoubaiDataSet.T_ChumonMsgDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new LibError("エラー");
            KoubaiDataSet.T_ChumonMsgRow drThis = (KoubaiDataSet.T_ChumonMsgRow)dt.Rows[0];
            try
            {
                drThis.OpenedFlg = true;
                drThis.OpenedDate = DateTime.Now;
                drThis.OpenedUserKubun = p;
                drThis.OpenedLoginID = p_2;
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static KoubaiDataSet.T_ChumonMsgRow newT_ChumonMsgRow()
        {
            return new KoubaiDataSet.T_ChumonMsgDataTable().NewT_ChumonMsgRow();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static ChumonMsgDataSet.V_Chumon_MessageDataTable getV_Chumon_MessageDataTable(string strYear, string strHacchuuNo, int nKubun,  SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
             "SELECT M_Login.Name, M_Login_1.Name AS OpenedName, "
            + "T_ChumonMsg.MsgID, T_ChumonMsg.Year, "
            + "T_ChumonMsg.HacchuuNo, T_ChumonMsg.UserKubun, "
            + "T_ChumonMsg.LoginID, T_ChumonMsg.Message, "
            + "T_ChumonMsg.TourokuBi, T_ChumonMsg.KoushinBi, "
            + "T_ChumonMsg.OpenedFlg, T_ChumonMsg.OpenedUserKubun, "
            + "T_ChumonMsg.OpenedLoginID, T_ChumonMsg.OpenedDate, dbo.T_ChumonMsg.JigyoushoKubun "
            + "FROM M_Login "
            + "INNER JOIN T_ChumonMsg ON M_Login.LoginID = T_ChumonMsg.LoginID "
            + "LEFT OUTER JOIN M_Login AS M_Login_1 ON T_ChumonMsg.OpenedLoginID = M_Login_1.LoginID "
            + "WHERE (T_ChumonMsg.HacchuuNo = @HacchuuNo) AND (T_ChumonMsg.Year = @Year) AND (T_ChumonMsg.JigyoushoKubun = @JigyoushoKubun) "
            + "ORDER BY T_ChumonMsg.TourokuBi DESC ";
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", strHacchuuNo);            
            da.SelectCommand.Parameters.AddWithValue("@Year", strYear);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nKubun);
            ChumonMsgDataSet.V_Chumon_MessageDataTable dt = new ChumonMsgDataSet.V_Chumon_MessageDataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
