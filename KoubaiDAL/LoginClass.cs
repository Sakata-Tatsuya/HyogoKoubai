using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using KoubaiDAL;
using System.Data;

namespace KoubaiDAL
{
    public class LoginClass
    {
        public class KensakuParam
        {
            public string _Code = "";       // コード
            public int _JigyoushoKubun = 0; // 事業所区分
        }

        // 検索条件
        private static string WhereText(KensakuParam k)
        {
            Core.Sql.WhereGenerator w = new Core.Sql.WhereGenerator();
            // コード
            if (k._Code != "")
                w.Add(string.Format("M_Login.TantoushaCode = '{0}'", k._Code));
            if(k._JigyoushoKubun > 0)
                w.Add(string.Format("M_Login.JigyoushoKubun = '{0}'", k._JigyoushoKubun));

            return w.WhereText;
        }

        /// <summary>
        /// ログインIDとパスワードでログインデータ取得
        /// </summary>
        /// <param name="LoginID"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static KoubaiDataSet.M_LoginRow getM_LoginRow(string LoginID, string Pass, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Login WHERE LoginID = @LoginID AND Password =@Pass";
            da.SelectCommand.Parameters.AddWithValue("@LoginID", LoginID);
            da.SelectCommand.Parameters.AddWithValue("@Pass", Pass);
            KoubaiDataSet.M_LoginDataTable dt = new KoubaiDataSet.M_LoginDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (KoubaiDataSet.M_LoginRow)dt.Rows[0];
            else
                return null;
        }

        /// <summary>
        /// パスワードを更新する
        /// </summary>      
        /// <param name="LoginID"></param>
        /// <param name="strNewPass"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LibError M_Login_Update_Password(string strID, string strNewPass, SqlConnection sqlConn)
        {
            SqlCommand cmdUp = new SqlCommand("", sqlConn);
            cmdUp.CommandText =
                "UPDATE M_Login SET Password = @Password  WHERE LoginID = @strID";
            cmdUp.Parameters.AddWithValue("@Password", strNewPass);

            cmdUp.Parameters.AddWithValue("@strID", strID);
            try
            {
                sqlConn.Open();
                int nCnt = cmdUp.ExecuteNonQuery();
                if (nCnt != 1)
                    return new LibError("更新するデータが見つかりませんでした");
                else
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
        /// ユーザー区分が1のとき
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static KoubaiDataSet.M_LoginDataTable getM_LoginDataTable(SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Login ";

            KoubaiDataSet.M_LoginDataTable dt = new KoubaiDataSet.M_LoginDataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// ユーザー区分が1のとき
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static KoubaiDataSet.M_LoginDataTable getM_Login_ShanaiDataTable(KensakuParam k, byte bkubun, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Login WHERE UserKubun = @kubun ";

            string strWhere = WhereText(k);
            if (strWhere != "")
            {
                da.SelectCommand.CommandText += "AND " + strWhere;
            }
            da.SelectCommand.Parameters.AddWithValue("@kubun", bkubun);

            KoubaiDataSet.M_LoginDataTable dt = new KoubaiDataSet.M_LoginDataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// ユーザー区分が２のとき
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static KoubaiDataSet.M_LoginDataTable getM_LoginDataTable(byte bKubun, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Login WHERE UserKubun = @UserKubun ";
            da.SelectCommand.Parameters.AddWithValue("@UserKubun", bKubun);
            KoubaiDataSet.M_LoginDataTable dt = new KoubaiDataSet.M_LoginDataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 担当者データの削除
        /// </summary>
        /// <param name="LoginID"></param>
        /// <param name="sqlConn"></param>
        public static LibError M_Login_Delete(string LoginID, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Login WHERE LoginID = @LoginID";
            da.SelectCommand.Parameters.AddWithValue("@LoginID", LoginID);
            da.DeleteCommand = (new SqlCommandBuilder(da)).GetDeleteCommand();
            KoubaiDataSet.M_LoginDataTable dt = new KoubaiDataSet.M_LoginDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new LibError("エラー");
            try
            {
                KoubaiDataSet.M_LoginRow drThis = (KoubaiDataSet.M_LoginRow)dt.Rows[0];
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
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LibError M_Login_Insert(KoubaiDataSet.M_LoginRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Login";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            KoubaiDataSet.M_LoginDataTable dt = new KoubaiDataSet.M_LoginDataTable();
            KoubaiDataSet.M_LoginRow drNew = dt.NewM_LoginRow();
            try
            {
                //drNew.ItemArray = dr.ItemArray;
                drNew.LoginID = dr.LoginID;
                drNew.UserKubun = dr.UserKubun;
                drNew.KaishaCode = dr.KaishaCode;
                drNew.JigyoushoKubun = dr.JigyoushoKubun;
                drNew.Busho = dr.Busho;
                drNew.Yakushoku = dr.Yakushoku;
                drNew.Password = dr.Password;
                drNew.KanrishaFlg = dr.KanrishaFlg;
                drNew.TantoushaCode = dr.TantoushaCode;
                drNew.Name = dr.Name;
                drNew.Mail = dr.Mail;
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
        /// 
        /// </summary>
        /// <param name="LoginID"></param>
        /// <param name="dr"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LibError M_Login_Update(string LoginID, KoubaiDataSet.M_LoginRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Login WHERE LoginID = @LoginID";
            da.SelectCommand.Parameters.AddWithValue("@LoginID", LoginID);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            KoubaiDataSet.M_LoginDataTable dt = new KoubaiDataSet.M_LoginDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new LibError("エラー");
            KoubaiDataSet.M_LoginRow drThis = (KoubaiDataSet.M_LoginRow)dt.Rows[0];
            try
            {
                drThis.KaishaCode = dr.KaishaCode;
                drThis.Busho = dr.Busho;
                drThis.Yakushoku = dr.Yakushoku;
                if (dr.Password != "")
                {
                    drThis.Password = dr.Password;
                }
                drThis.KanrishaFlg = dr.KanrishaFlg;
                drThis.TantoushaCode = dr.TantoushaCode;
                drThis.Name = dr.Name;
                drThis.Mail = dr.Mail;
                drThis.JigyoushoKubun = dr.JigyoushoKubun;
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
        public static KoubaiDataSet.M_LoginRow newM_LoginRow()
        {
            return new KoubaiDataSet.M_LoginDataTable().NewM_LoginRow();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LoginDataSet.V_ShiiresakiAccountDataTable getV_ShiiresakiAccountDataTable(KensakuParam k, byte bUserKubun, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT M_Login.LoginID, M_Login.UserKubun, "
            + "M_Login.KaishaCode AS ShiiresakiCode, M_Login.Busho, "
            + "M_Login.Yakushoku, M_Login.Password, M_Login.KanrishaFlg, "
            + "M_Login.TantoushaCode, M_Login.Name, M_Login.Mail, "
            + "M_Shiiresaki.ShiiresakiMei "
            + "FROM dbo.M_Login "
            + "INNER JOIN M_Shiiresaki ON M_Login.KaishaCode = M_Shiiresaki.ShiiresakiCode "
            + "WHERE (M_Login.UserKubun = @UserKubun) ";
            string strWhere = WhereText(k);
            if (strWhere != "")
            {
                da.SelectCommand.CommandText += "AND " + strWhere;
            }
            da.SelectCommand.CommandText += "ORDER BY M_Shiiresaki.ShiiresakiCode ";
            da.SelectCommand.Parameters.AddWithValue("@UserKubun", bUserKubun);
            LoginDataSet.V_ShiiresakiAccountDataTable dt = new LoginDataSet.V_ShiiresakiAccountDataTable();
            da.Fill(dt);
            return dt;
        }

        public static LoginDataSet.V_ShiiresakiAccountDataTable getV_ShiiresakiAccountDataTable(byte bUserKubun, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT  M_Login.LoginID, M_Login.UserKubun, "
            + "M_Login.KaishaCode AS ShiiresakiCode, M_Login.Busho, "
            + "M_Login.Yakushoku, M_Login.Password, M_Login.KanrishaFlg, "
            + "M_Login.TantoushaCode, M_Login.Name, M_Login.Mail, "
            + "M_Shiiresaki.ShiiresakiMei "
            + "FROM dbo.M_Login "
            + "INNER JOIN M_Shiiresaki ON M_Login.KaishaCode = M_Shiiresaki.ShiiresakiCode "
            + "WHERE (M_Login.UserKubun = @UserKubun) ";

            da.SelectCommand.Parameters.AddWithValue("@UserKubun", bUserKubun);
            LoginDataSet.V_ShiiresakiAccountDataTable dt = new LoginDataSet.V_ShiiresakiAccountDataTable();
            da.Fill(dt);
            return dt;
        }
        public static KoubaiDataSet.M_LoginRow getM_LoginRow(string LoginID, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Login WHERE LoginID = @LoginID";
            da.SelectCommand.Parameters.AddWithValue("@LoginID", LoginID);
            KoubaiDataSet.M_LoginDataTable dt = new KoubaiDataSet.M_LoginDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (KoubaiDataSet.M_LoginRow)dt.Rows[0];
            else
                return null;
        }

        public static int GetMaxTantoushaCode(int iUserKubun, SqlConnection sqlConn)
        {
            int MaxCode = 0;
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "SELECT MAX(TantoushaCode) as TantoushaCode FROM M_Login WHERE UserKubun = @UserKubun ";
            da.SelectCommand.Parameters.AddWithValue("@UserKubun", iUserKubun);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows[0].IsNull("TantoushaCode"))
            {
                return MaxCode;
            }
            else
            {
                int.TryParse(dt.Rows[0]["TantoushaCode"].ToString(), out MaxCode);
                return MaxCode;
            }
        }

        /// <summary>
        /// 仕入先の設定を取得
        /// </summary>
        /// <param name="LoginID"></param>
        /// <param name="Pass"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LoginDataSet.V_Shiiresaki_FlgRow getV_Shiiresaki_FlgRow(string LoginID, string Pass, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "SELECT dbo.M_Shiiresaki.KensyukoukaiFlg, M_Shiiresaki.KousinKyokaFlg FROM dbo.M_Shiiresaki "
              + "INNER JOIN dbo.M_Login ON dbo.M_Shiiresaki.ShiiresakiCode = dbo.M_Login.KaishaCode "
              + "WHERE (LoginID = @LoginID) AND (Password = @Pass)";
            da.SelectCommand.Parameters.AddWithValue("@LoginID", LoginID);
            da.SelectCommand.Parameters.AddWithValue("@Pass", Pass);
            LoginDataSet.V_Shiiresaki_FlgDataTable dt = new LoginDataSet.V_Shiiresaki_FlgDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (LoginDataSet.V_Shiiresaki_FlgRow)dt.Rows[0];
            else
                return null;
        }

        /// <summary>
        /// 全て管理者のメール情報取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LoginDataSet.V_MailInfoDataTable getV_MailInfoDataTable(SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
             "SELECT T_KaishaInfo.KaishaMei, T_KaishaInfo.EigyouSho, M_Login.Busho, M_Login.Name, T_KaishaInfo.Tel, T_KaishaInfo.Fax, M_Login.Mail "
            + "FROM M_Login "
            + "INNER JOIN T_KaishaInfo ON M_Login.JigyoushoKubun = T_KaishaInfo.KaishaID "
            + "WHERE      (M_Login.KaishaCode = N'0') AND (M_Login.KanrishaFlg = 1) ";
            LoginDataSet.V_MailInfoDataTable dt = new LoginDataSet.V_MailInfoDataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// ログインIDによって、管理者のメール情報取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LoginDataSet.V_MailInfoRow getV_MailInfoRow(string strLoginID, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
             "SELECT T_KaishaInfo.KaishaMei, T_KaishaInfo.EigyouSho, M_Login.Busho, M_Login.Name, T_KaishaInfo.Tel, T_KaishaInfo.Fax, M_Login.Mail "
            + "FROM  M_Login "
            + "INNER JOIN T_KaishaInfo ON M_Login.JigyoushoKubun = T_KaishaInfo.KaishaID "
            + "WHERE      (M_Login.LoginID = @LoginID) ";
            da.SelectCommand.Parameters.AddWithValue("@LoginID", strLoginID);
            LoginDataSet.V_MailInfoDataTable dt = new LoginDataSet.V_MailInfoDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (LoginDataSet.V_MailInfoRow)dt.Rows[0];
            else
                return null;
        }

        /// <summary>
        /// 会社コードによって、仕入先の担当者メール情報取得
        /// </summary>
        /// <param name="strCode"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static KoubaiDataSet.M_LoginDataTable getM_LoginDataTable(string strCode, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Login WHERE KaishaCode = @Code ";
            da.SelectCommand.Parameters.AddWithValue("@Code", strCode);
            KoubaiDataSet.M_LoginDataTable dt = new KoubaiDataSet.M_LoginDataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 管理側担当者情報を取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LoginDataSet.V_TantoushaAccountDataTable getV_TantoushaAccountDataTable(KensakuParam k, byte bkubun, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT DISTINCT "
            + "M_Login.LoginID, M_Login.UserKubun, M_Login.KaishaCode, M_Login.JigyoushoKubun, M_Login.TantoushaCode, "
            + "M_Login.Busho, M_Login.Yakushoku, M_Login.Password, M_Login.KanrishaFlg, M_Login.Name, M_Login.Mail, "
            + "T_KaishaInfo.EigyouSho "
            + "FROM  M_Login "
            + "INNER JOIN T_KaishaInfo ON M_Login.JigyoushoKubun = T_KaishaInfo.KaishaID "
            + "WHERE UserKubun = @kubun ";
            string strWhere = WhereText(k);
            if (strWhere != "")
            {
                da.SelectCommand.CommandText += "AND " + strWhere;
            }
            da.SelectCommand.CommandText += "ORDER BY M_Login.JigyoushoKubun, M_Login.TantoushaCode ";
            da.SelectCommand.Parameters.AddWithValue("@kubun", bkubun);

            LoginDataSet.V_TantoushaAccountDataTable dt = new LoginDataSet.V_TantoushaAccountDataTable();
            da.Fill(dt);
            return dt;
        }
    }
}