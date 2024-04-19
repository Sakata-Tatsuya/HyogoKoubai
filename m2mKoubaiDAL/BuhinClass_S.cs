using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace m2mKoubaiDAL
{
    public class BuhinClass_S
    {
        /// <summary>
        /// 部品の単位を取得する
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static BuhinDataSet_S.V_Buhin_TaniDataTable 
            getV_Buhin_TaniDataTable(SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT DISTINCT TOP (100) PERCENT Tani "
            + "FROM                     M_Buhin "
            + "ORDER BY           Tani ";
            BuhinDataSet_S.V_Buhin_TaniDataTable dt = new BuhinDataSet_S.V_Buhin_TaniDataTable();
            da.Fill(dt);
            return dt;
        }
        
        /// <summary>
        /// 部品データを取得する
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static BuhinDataSet_S.V_BuhinInfoRow
            getV_BuhinInfoRow(string strCode, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT          Tani, LT_Suuji, LT_Tani, Tanka, Lot "
            + "FROM            M_Buhin "
            + "WHERE           (BuhinCode = @BuhinCode) ";
            da.SelectCommand.Parameters.AddWithValue("@BuhinCode", strCode);
            BuhinDataSet_S.V_BuhinInfoDataTable dt = new BuhinDataSet_S.V_BuhinInfoDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as BuhinDataSet_S.V_BuhinInfoRow;
            else
                return null;
        }

        /*
        /// <summary>
        /// 部品区分を取得する
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static BuhinDataSet_S.V_BuhinKubunDataTable 
            getV_BuhinKubunDataTable(SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT DISTINCT TOP (100) PERCENT BuhinKubun "
            + "FROM            M_Buhin "
            + "ORDER BY     BuhinKubun ";
            BuhinDataSet_S.V_BuhinKubunDataTable dt = new BuhinDataSet_S.V_BuhinKubunDataTable();
            da.Fill(dt);
            return dt;
        }
        */

        /// <summary>
        /// 部品区分を取得する
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static BuhinDataSet_S.V_BuhinKubunDataTable
            getV_BuhinKubunDataTable(string strShiireCode, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT DISTINCT TOP (100) PERCENT BuhinKubun "
            + "FROM            M_Buhin "
            + "WHERE           (ShiiresakiCode1 = @ShiiresakiCode) OR "
            + "(ShiiresakiCode2 = @ShiiresakiCode) "
            + "ORDER BY     BuhinKubun ";
            da.SelectCommand.Parameters.AddWithValue("@ShiiresakiCode", strShiireCode);
            BuhinDataSet_S.V_BuhinKubunDataTable dt = new BuhinDataSet_S.V_BuhinKubunDataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 部品コードと部品目名を取得する
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static BuhinDataSet_S.V_BuhinCodeMeiDataTable
            getV_BuhinCodeMeiDataTable(string strShiiresakiCode, string strKubun, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT          BuhinCode, BuhinMei "
            + "FROM            M_Buhin "
            + "WHERE           (BuhinKubun = @BuhinKubun) AND (ShiiresakiCode1 = @ShiiresakiCode) OR "
            + "(BuhinKubun = @BuhinKubun) AND (ShiiresakiCode2 = @ShiiresakiCode)";            
            da.SelectCommand.Parameters.AddWithValue("@BuhinKubun", strKubun);
            da.SelectCommand.Parameters.AddWithValue("@ShiiresakiCode", strShiiresakiCode);            
            BuhinDataSet_S.V_BuhinCodeMeiDataTable dt = new BuhinDataSet_S.V_BuhinCodeMeiDataTable();
            da.Fill(dt);
            return dt;
        }

        
        

    }
}
