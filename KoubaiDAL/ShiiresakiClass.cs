using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace KoubaiDAL
{
    public class ShiiresakiClass
    {
        public enum ShimeBi
        {
            MATUJITU = 0,
            GO = 1,
            JYU = 2,
            JYUGO = 3,
            NIJYU = 4,
            NIJYUGO = 5,
        }

        public class KensakuParam
        {
            public string _Code = "";       // コード
            // public string _Name = "";    //仕入先名
        }

        public class ShiiresakiKey
        {
            private string _Code;

            public ShiiresakiKey(string Code)
            {
                this._Code = Code;
            }

            public override string ToString()
            {
                return this._Code;
            }
        }

        // 検索条件
        private static string WhereText(KensakuParam k)
        {
            Core.Sql.WhereGenerator w = new Core.Sql.WhereGenerator();
            // コード
            if (k._Code != "")
                w.Add(string.Format("M_Shiiresaki.ShiiresakiCode = '{0}'", k._Code));
            // 仕入先名
            /* if (k._Name != "")
             {
                 w.Add(string.Format("M_Shiiresaki.ShiiresakiMei = '{0}'", k._Name));
             }*/
            return w.WhereText;
        }


        public static KoubaiDataSet.M_ShiiresakiDataTable getM_ShiiresakiDataTable(SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Shiiresaki";
            KoubaiDataSet.M_ShiiresakiDataTable dt = new KoubaiDataSet.M_ShiiresakiDataTable();
            da.Fill(dt);
            return dt;
        }

        public static KoubaiDataSet.M_ShiiresakiDataTable getM_ShiiresakiDataTable(KensakuParam k, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Shiiresaki ";
            string strWhere = WhereText(k);
            if (strWhere != "")
            {
                da.SelectCommand.CommandText += "WHERE " + strWhere;
            }

            KoubaiDataSet.M_ShiiresakiDataTable dt = new KoubaiDataSet.M_ShiiresakiDataTable();
            da.Fill(dt);
            return dt;
        }

        public static KoubaiDataSet.M_ShiiresakiDataTable getM_ShiiresakiDataTable(string nShiiresakiCode, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Shiiresaki";

            StringBuilder sb = new StringBuilder();
            if (nShiiresakiCode != "")
            {
                if (sb.Length == 0)
                    sb.Append(" WHERE ");
                sb.Append("M_Shiiresaki.ShiiresakiCode = " + nShiiresakiCode);
            }
            da.SelectCommand.CommandText += sb.ToString();

            KoubaiDataSet.M_ShiiresakiDataTable dt = new KoubaiDataSet.M_ShiiresakiDataTable();
            da.Fill(dt);
            return dt;
        }

        public static void getM_ShiiresakiDataTable(string strText, bool bFirstMatch, int nStartIndex, int nCount,
            SqlConnection sqlConn, out ShiiresakiDataSet.M_ShiiresakiDataTable dt, ref int nTotal)
        {
            Core.Sql.RowNumberInfo info = new Core.Sql.RowNumberInfo();
            info.nStartNumber = nStartIndex + 1;
            info.nEndNumber = nStartIndex + nCount;
            info.strOverText = "ORDER BY BuhinCode ";

            SqlCommand cmd = new SqlCommand("", sqlConn);
            cmd.CommandText = "SELECT * FROM M_Shiiresaki ";
            if (strText.Trim() != "")
            {
                cmd.CommandText += "WHERE ShiiresakiCode LIKE @ShiiresakiCode ";
                if (bFirstMatch){
                    cmd.Parameters.AddWithValue("@ShiiresakiCode", strText + "%");
                }
                else { 
                    cmd.Parameters.AddWithValue("@ShiiresakiCode", "%" + strText + "%");
                }
            }

            dt = new ShiiresakiDataSet.M_ShiiresakiDataTable();
            info.LoadData(cmd, sqlConn, dt, ref nTotal);
        }

        public static KoubaiDataSet.M_ShiiresakiRow GetV_SHiiresakiRow(string ShiiresakiCode, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "SELECT  *  FROM M_Shiiresaki "
                + "WHERE   (ShiiresakiCode = @ShiiresakiCode) ";
            da.SelectCommand.Parameters.AddWithValue("@ShiiresakiCode", ShiiresakiCode);
            KoubaiDataSet.M_ShiiresakiDataTable dt = new KoubaiDataSet.M_ShiiresakiDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt.Rows[0] as KoubaiDataSet.M_ShiiresakiRow;
            else
                return null;
        }

        public static KoubaiDataSet.M_ShiiresakiRow newM_ShiiresakiRow()
        {
            return new KoubaiDataSet.M_ShiiresakiDataTable().NewM_ShiiresakiRow();
        }

        public static LibError M_Shiiresaki_Delete(string ShiiresakiCode, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Shiiresaki WHERE ShiiresakiCode = @ShiiresakiCode";
            da.SelectCommand.Parameters.AddWithValue("@ShiiresakiCode", ShiiresakiCode);
            da.DeleteCommand = (new SqlCommandBuilder(da)).GetDeleteCommand();
            KoubaiDataSet.M_ShiiresakiDataTable dt = new KoubaiDataSet.M_ShiiresakiDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new LibError("エラー");
            try
            {
                KoubaiDataSet.M_ShiiresakiRow drThis = (KoubaiDataSet.M_ShiiresakiRow)dt.Rows[0];
                drThis.Delete();
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
        }

        public static KoubaiDataSet.M_ShiiresakiRow getM_ShiiresakiRow(string ShiiresakiCode, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Shiiresaki WHERE ShiiresakiCode = @ShiiresakiCode";
            da.SelectCommand.Parameters.AddWithValue("@ShiiresakiCode", ShiiresakiCode);
            KoubaiDataSet.M_ShiiresakiDataTable dt = new KoubaiDataSet.M_ShiiresakiDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (KoubaiDataSet.M_ShiiresakiRow)dt.Rows[0];
            else
                return null;
        }

        public static LibError M_Shiiresaki_Insert(KoubaiDataSet.M_ShiiresakiRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Shiiresaki";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            KoubaiDataSet.M_ShiiresakiDataTable dt = new KoubaiDataSet.M_ShiiresakiDataTable();
            KoubaiDataSet.M_ShiiresakiRow drNew = dt.NewM_ShiiresakiRow();
            try
            {
                drNew.ItemArray = dr.ItemArray;
                //drNew.ShiiresakiCode = dr.ShiiresakiCode;
                //drNew.ShiiresakiMei = dr.ShiiresakiMei;
                //drNew.YubinBangou = dr.YubinBangou;
                //drNew.Address = dr.Address;
                //drNew.Tel = dr.Tel;
                //drNew.Fax = dr.Fax;
                //drNew.KouzaMeigi = dr.KouzaMeigi;
                //drNew.KinyuuKikanMei = dr.KinyuuKikanMei;
                //drNew.KouzaBangou = dr.KouzaBangou;
                //drNew.ShiharaiShimebi = dr.ShiharaiShimebi;
                //drNew.ShiharaiYoteibi = dr.ShiharaiYoteibi;
                //drNew.KensyukoukaiFlg = dr.KensyukoukaiFlg;
                //drNew.SaisokuMailFlg = dr.SaisokuMailFlg;
                //drNew.KousinKyokaFlg = dr.KousinKyokaFlg;
                //drNew.InvoiceRegFlg = dr.InvoiceRegFlg;
                //drNew.InvoiceRegNo = dr.InvoiceRegNo;
                dt.Rows.Add(drNew);
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError("エラー");
            }
        }

        public static LibError M_Shiiresaki_Update(string ShiiresakiCode, KoubaiDataSet.M_ShiiresakiRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Shiiresaki WHERE ShiiresakiCode = @ShiiresakiCode";
            da.SelectCommand.Parameters.AddWithValue("@ShiiresakiCode", ShiiresakiCode);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            KoubaiDataSet.M_ShiiresakiDataTable dt = new KoubaiDataSet.M_ShiiresakiDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new LibError("エラー");
            try
            {
                KoubaiDataSet.M_ShiiresakiRow drThis = (KoubaiDataSet.M_ShiiresakiRow)dt.Rows[0];
                drThis.ItemArray = dr.ItemArray;
                //drThis.ShiiresakiMei = dr.ShiiresakiMei;
                //drThis.YubinBangou = dr.YubinBangou;
                //drThis.Address = dr.Address;
                //drThis.Tel = dr.Tel;
                //drThis.Fax = dr.Fax;
                //drThis.KouzaMeigi = dr.KouzaMeigi;
                //drThis.KinyuuKikanMei = dr.KinyuuKikanMei;
                //drThis.KouzaBangou = dr.KouzaBangou;
                //drThis.ShiharaiShimebi = dr.ShiharaiShimebi;
                //drThis.ShiharaiYoteibi = dr.ShiharaiYoteibi;
                //drThis.KensyukoukaiFlg = dr.KensyukoukaiFlg;
                //drThis.SaisokuMailFlg = dr.SaisokuMailFlg;
                //drThis.KousinKyokaFlg = dr.KousinKyokaFlg;
                //drThis.InvoiceRegFlg = dr.InvoiceRegFlg;
                //drThis.InvoiceRegNo = dr.InvoiceRegNo;
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
        }

        /// <summary>
        /// 仕入先を取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static ShiiresakiDataSet.V_ShiiresakiDataTable getV_ShiiresakiDataTable(SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);

            da.SelectCommand.CommandText =
                "SELECT ShiiresakiCode, ShiiresakiMei "
                + "FROM dbo.M_Buhin "
                + "INNER JOIN dbo.M_Shiiresaki ON dbo.M_Buhin.ShiiresakiCode1 = M_Shiiresaki.ShiiresakiCode "
                + "UNION "
                + "SELECT ShiiresakiCode, ShiiresakiMei "
                + "FROM dbo.M_Buhin "
                + "INNER JOIN dbo.M_Shiiresaki AS M_Shiiresaki_1 ON  dbo.M_Buhin.ShiiresakiCode1 = M_Shiiresaki_1.ShiiresakiCode ";
            
            ShiiresakiDataSet.V_ShiiresakiDataTable dt = new ShiiresakiDataSet.V_ShiiresakiDataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// ログインIDによって、会社情報を取得
        /// </summary>
        /// <param name="strLoginID"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static ShiiresakiDataSet.V_Nouhinsho_HeaderRow getV_Nouhinsho_HeaderRow(string strLoginID, int nKubun, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM V_Nouhinsho_Header "
            + "WHERE (LoginID = @LoginID) ";
            da.SelectCommand.Parameters.AddWithValue("@LoginID", strLoginID);
            //da.SelectCommand.Parameters.AddWithValue("@Kubun", nKubun);
            ShiiresakiDataSet.V_Nouhinsho_HeaderDataTable dt = new ShiiresakiDataSet.V_Nouhinsho_HeaderDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (ShiiresakiDataSet.V_Nouhinsho_HeaderRow)dt.Rows[0];
            else
                return null;
        }



        public static int GetShimeBiDay(ShimeBi ShimeiBi)
        {
           
            if (ShimeiBi == ShiiresakiClass.ShimeBi.GO)
                return 5;
            else if (ShimeiBi == ShiiresakiClass.ShimeBi.JYU)
                return 10;
            else if (ShimeiBi == ShiiresakiClass.ShimeBi.JYUGO)
                return 15;
            else if (ShimeiBi == ShiiresakiClass.ShimeBi.NIJYU)
                return 20;
            else if (ShimeiBi == ShiiresakiClass.ShimeBi.NIJYUGO)
                return 25;
            else 
                return 31;
        }

        // 会社情報更新
        public static LibError M_ShiiresakiInfo_Update(string ShiiresakiCode, KoubaiDataSet.M_ShiiresakiRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Shiiresaki WHERE ShiiresakiCode = @ShiiresakiCode";
            da.SelectCommand.Parameters.AddWithValue("@ShiiresakiCode", ShiiresakiCode);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            KoubaiDataSet.M_ShiiresakiDataTable dt = new KoubaiDataSet.M_ShiiresakiDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new LibError("エラー");
            try
            {
                KoubaiDataSet.M_ShiiresakiRow drThis = (KoubaiDataSet.M_ShiiresakiRow)dt.Rows[0];
                //drNew.ItemArray = dr.ItemArray;
                drThis.ShiiresakiMei = dr.ShiiresakiMei;
                drThis.YubinBangou = dr.YubinBangou;
                drThis.Address = dr.Address;
                drThis.Tel = dr.Tel;
                drThis.Fax = dr.Fax;
                drThis.KouzaMeigi = dr.KouzaMeigi;
                drThis.KinyuuKikanMei = dr.KinyuuKikanMei;
                drThis.KouzaBangou = dr.KouzaBangou;
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
        }
    }
}
