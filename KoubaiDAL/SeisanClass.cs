using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace KoubaiDAL
{
    public class SeisanClass
    {
        /// <summary>
        /// 検索条件
        /// </summary>
        public class KensakuParam
        {
            // 年月
            public string _Nengetu = string.Empty;
            // 基準年月
            public string _KijyunYM = string.Empty;
            // シーズン
            public string _Season = string.Empty;
            // 仕入先コード
            public string _ShiiresakiCode = string.Empty;
            public string WhereText(SqlCommand cmd, KensakuParam k)
            {
                Core.Sql.WhereGenerator w = new Core.Sql.WhereGenerator();
                // 年月
                if (k._Nengetu != string.Empty)
                {
                    w.Add(string.Format("Nengetu >= '{0}'", k._Nengetu));
                }
                // 基準年月
                if (k._KijyunYM != string.Empty)
                {
                    w.Add(string.Format("Nengetu = '{0}'", k._KijyunYM));
                }
                // シーズン
                if (k._Season != string.Empty)
                {
                    w.Add(string.Format("Season = '{0}'", k._Season));
                }
                // 仕入先コード
                if (k._ShiiresakiCode != string.Empty)
                {
                    w.Add(string.Format("ShiiresakiCode = '{0}'", k._ShiiresakiCode));
                }
                return w.WhereText;
            }

        }

        public static SeisanDataSet.T_SeisanKeikakuDataTable GetT_SeisanKeikakuDataTable(KensakuParam p, SqlConnection sqlConn)
        {
            string sql = @"SELECT * FROM T_SeisanKeikaku ";

            SqlDataAdapter da = new SqlDataAdapter(sql, sqlConn);
            string whereText = p.WhereText(da.SelectCommand, p);
            if (whereText != "")
            {
                da.SelectCommand.CommandText += string.Format(" WHERE {0} ", whereText);
            }

            SeisanDataSet.T_SeisanKeikakuDataTable dt = new SeisanDataSet.T_SeisanKeikakuDataTable();
            da.Fill(dt);

            return dt;
        }

        public static SeisanDataSet.V_ShoyouSuDataTable GetV_ShoyouSuDataTable(KensakuParam p, SqlConnection sqlConn)
        {
            string sql = @"SELECT * FROM V_ShoyouSu ";

            SqlDataAdapter da = new SqlDataAdapter(sql, sqlConn);
            string whereText = p.WhereText(da.SelectCommand, p);
            if (whereText != "")
            {
                da.SelectCommand.CommandText += string.Format(" WHERE {0} ", whereText);
            }

            SeisanDataSet.V_ShoyouSuDataTable dt = new SeisanDataSet.V_ShoyouSuDataTable();
            da.Fill(dt);

            return dt;
        }

        public static SeisanDataSet.V_ShoyouSuRow GetV_ShoyouSuRow(string strNengetu, string strBuhinCode, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT DISTINCT * FROM V_ShoyouSu"
            + " WHERE Nengetu = @Nengetu AND BuhinCode = @BuhinCode";

            da.SelectCommand.Parameters.AddWithValue("@Nengetu", strNengetu);
            da.SelectCommand.Parameters.AddWithValue("@BuhinCode", strBuhinCode);
            SeisanDataSet.V_ShoyouSuDataTable dt = new SeisanDataSet.V_ShoyouSuDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (SeisanDataSet.V_ShoyouSuRow)dt.Rows[0];
            else
                return null;
        }

        //public static HacchuDataSet.V_HacchuDataTable getV_HacchuDataTable(string key, SqlConnection sqlConn)
        //{
        //    SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
        //    da.SelectCommand.CommandText =
        //        "SELECT  TOP (100) PERCENT T_Chumon.Year, T_Chumon.HacchuuNo, T_Chumon.ShiiresakiCode, T_Chumon.BuhinKubun, "
        //    + "T_Chumon.BuhinCode, T_Chumon.Tanka, T_Chumon.Suuryou, T_Chumon.Nouki, T_Chumon.NounyuuBashoCode, "
        //    + "T_Chumon.Bikou, T_Chumon.HacchuuBi, T_Chumon.HacchushaID, M_Buhin.Tani, M_Buhin.BuhinMei, "
        //    + "M_Shiiresaki.ShiiresakiMei, M_Login.Name, M_NounyuuBasho.BashoMei, T_Chumon.Kingaku, "
        //    + "M_Shiiresaki.YubinBangou, M_Shiiresaki.Tel, M_Shiiresaki.Fax, M_Shiiresaki.Address, T_Chumon.JigyoushoKubun, "
        //    + "T_KaishaInfo.KaishaMei, T_KaishaInfo.EigyouSho, T_KaishaInfo.Yuubin AS YuubinY, T_KaishaInfo.Address AS AddressY, "
        //    + "T_KaishaInfo.Tel AS TelY, T_KaishaInfo.Fax AS FaxY "
        //    + "FROM T_Chumon "
        //    + "INNER JOIN M_Buhin ON T_Chumon.BuhinKubun = M_Buhin.BuhinKubun AND "
        //    + "T_Chumon.BuhinCode = M_Buhin.BuhinCode "
        //    + "INNER JOIN M_Shiiresaki ON T_Chumon.ShiiresakiCode = M_Shiiresaki.ShiiresakiCode "
        //    + "INNER JOIN M_NounyuuBasho ON T_Chumon.NounyuuBashoCode = M_NounyuuBasho.BashoCode "
        //    + "INNER JOIN T_KaishaInfo ON T_Chumon.JigyoushoKubun = T_KaishaInfo.KaishaID "
        //    + "LEFT OUTER JOIN M_Login ON T_Chumon.HacchushaID = M_Login.LoginID ";

        //    // WHERE
        //    string strW = WhereText(key, da.SelectCommand);
        //    if (strW != "")
        //    {
        //        da.SelectCommand.CommandText += " WHERE " + strW;
        //    }
        //    // GROUP BY
        //    // ORDER BY
        //    da.SelectCommand.CommandText += "  ORDER BY dbo.T_Chumon.ShiiresakiCode, T_Chumon.JigyoushoKubun, dbo.T_Chumon.HacchuuNo ";

        //    HacchuDataSet.V_HacchuDataTable dt = new HacchuDataSet.V_HacchuDataTable();
        //    da.Fill(dt);
        //    return dt;
        //}

        public static SeisanDataSet.VIEW_SeihinKouseiCheckDataTable getVIEW_SeihinKouseiCheckDataTable(string str, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM VIEW_SeihinKouseiCheck ";
            if (str != "")
            {
                da.SelectCommand.CommandText += string.Format(" WHERE (SeihinCode + ' : ' + SeihinMei) LIKE '%{0}%'", str);
            }

            SeisanDataSet.VIEW_SeihinKouseiCheckDataTable dt = new SeisanDataSet.VIEW_SeihinKouseiCheckDataTable();
            da.Fill(dt);

            return dt;
        }

















    }
}
