using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

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
                    w.Add(string.Format("ShiiresakiCode1 = '{0}'", k._ShiiresakiCode));
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

        public static SeisanDataSet.V_ShoyouSimDataTable GetV_ShoyouSimDataTable(KensakuParam p, SqlConnection sqlConn)
        {
            string sql = @"SELECT * FROM V_ShoyouSim ";

            SqlDataAdapter da = new SqlDataAdapter(sql, sqlConn);
            string whereText = p.WhereText(da.SelectCommand, p);
            if (whereText != "")
            {
                da.SelectCommand.CommandText += string.Format(" WHERE {0} ", whereText);
            }

            SeisanDataSet.V_ShoyouSimDataTable dt = new SeisanDataSet.V_ShoyouSimDataTable();
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

        public static SeisanDataSet.V_ShoyouSimDataTable ShoyouSimCulc(SeisanDataSet.V_ShoyouSimDataTable dtBase, DateTime dtN0, SqlConnection sqlConn)
        {
            SeisanDataSet.V_ShoyouSimDataTable dt = new SeisanDataSet.V_ShoyouSimDataTable();
            for (int i = 0; i < dtBase.Rows.Count; i++)
            {
                SeisanDataSet.V_ShoyouSimRow drBase = dtBase[i];
                SeisanDataSet.V_ShoyouSimRow drNew = dt.NewV_ShoyouSimRow();
                drNew.ItemArray = SeisanClass.ShoyouSimRowCulc(drBase, dtN0, sqlConn).ItemArray;
                dt.Rows.Add(drNew);
            }

            if (dt != null)
            {
                return dt;
            }
            return null;
        }

        public static SeisanDataSet.V_ShoyouSimRow ShoyouSimRowCulc(SeisanDataSet.V_ShoyouSimRow dr, DateTime dtN0,SqlConnection sqlConn)
        {
            SeisanDataSet.V_ShoyouSimDataTable dt = new SeisanDataSet.V_ShoyouSimDataTable();
            SeisanDataSet.V_ShoyouSimRow drNew = dt.NewV_ShoyouSimRow();

            drNew.ItemArray = dr.ItemArray;

            DateTime dtN_1 = dtN0.AddMonths(-1);
            DateTime dtN1 = dtN0.AddMonths(1);
            DateTime dtN2 = dtN0.AddMonths(2);
            DateTime dtN3 = dtN0.AddMonths(3);
            DateTime dtN4 = dtN0.AddMonths(4);
            DateTime dtN5 = dtN0.AddMonths(5);
            DateTime dtN6 = dtN0.AddMonths(6);

            //発注計画計算
            decimal[] dNyu = new decimal[8];
            decimal[] dUse = new decimal[8];
            decimal[] dOdr = new decimal[8];
            decimal[] dZai = new decimal[8];
            decimal dTemp = 0;
            //パラメータ取得
            decimal dLot = dr.Lot; //最小ロット

            dUse[0] = 0;
            dUse[1] = dr.UseN0;
            dUse[2] = dr.UseN1;
            dUse[3] = dr.UseN2;
            dUse[4] = dr.UseN3;
            dUse[5] = dr.UseN4;
            dUse[6] = dr.UseN5;
            dUse[7] = 0;

            dNyu[0] = ChumonClass.GetSumNyukoYotei(dr.BuhinCode, dtN_1.ToString("yyyyMMdd"), dtN0.ToString("yyyyMMdd"), sqlConn);
            dNyu[1] = ChumonClass.GetSumNyukoYotei(dr.BuhinCode, dtN0.ToString("yyyyMMdd"), dtN1.ToString("yyyyMMdd"), sqlConn);
            dNyu[2] = ChumonClass.GetSumNyukoYotei(dr.BuhinCode, dtN1.ToString("yyyyMMdd"), dtN2.ToString("yyyyMMdd"), sqlConn);
            dNyu[3] = ChumonClass.GetSumNyukoYotei(dr.BuhinCode, dtN2.ToString("yyyyMMdd"), dtN3.ToString("yyyyMMdd"), sqlConn);
            dNyu[4] = ChumonClass.GetSumNyukoYotei(dr.BuhinCode, dtN3.ToString("yyyyMMdd"), dtN4.ToString("yyyyMMdd"), sqlConn);
            dNyu[5] = ChumonClass.GetSumNyukoYotei(dr.BuhinCode, dtN4.ToString("yyyyMMdd"), dtN5.ToString("yyyyMMdd"), sqlConn);
            dNyu[6] = ChumonClass.GetSumNyukoYotei(dr.BuhinCode, dtN5.ToString("yyyyMMdd"), dtN6.ToString("yyyyMMdd"), sqlConn);

            dOdr[0] = ChumonClass.GetSumHacyuuSu(dr.BuhinCode, dtN_1.ToString("yyyy-MM-dd"), dtN0.ToString("yyyy-MM-dd"), sqlConn);
            SeisanDataSet.V_ShoyouSuRow drN_1 = SeisanClass.GetV_ShoyouSuRow(dtN_1.ToString("yyyyMM"), dr.BuhinCode, sqlConn);
            if (drN_1 != null)
            {
                dUse[0] = drN_1.N0;
            }
            //dZai[0] = dNyu[0] - dUse[0];
            dZai[0] = ZaikoClass.GetZensyaZaikoSu(dtN_1.ToString("yyyyMM"), dr.BuhinCode, sqlConn);
            if (dZai[0] == 0)
            {
                dZai[0] = dNyu[0] - dUse[0];
            }

            for (int i = 1; i < 7; i++)
            {
                if (i == 1 && dr.OdrN0 > 0)
                {
                    dNyu[i] = dOdr[i - 1];
                    dZai[i] = dZai[i - 1] + dNyu[i] - dUse[i];
                    dOdr[i] = dr.OdrN0;
                }
                else
                {
                    dNyu[i] = dOdr[i - 1];
                    dZai[i] = dZai[i - 1] + dNyu[i] - dUse[i];
                    if ((dZai[i - 1] + dNyu[i] - dUse[i]) <= dUse[i + 1])
                    {
                        dTemp = Math.Ceiling(Math.Abs(dZai[i] - dUse[i + 1]) / dLot);
                        dOdr[i] = dLot * dTemp;
                    }
                    else
                    {
                        dOdr[i] = 0;
                    }
                }
            }

            drNew.NyuN_1 = dNyu[0];
            drNew.NyuN0 = dNyu[1];
            drNew.NyuN1 = dNyu[2];
            drNew.NyuN2 = dNyu[3];
            drNew.NyuN3 = dNyu[4];
            drNew.NyuN4 = dNyu[5];
            drNew.NyuN5 = dNyu[6];
            drNew.NyuN6 = dNyu[7];
            drNew.UseN_1 = dUse[0];
            drNew.UseN0 = dUse[1];
            drNew.UseN1 = dUse[2];
            drNew.UseN2 = dUse[3];
            drNew.UseN3 = dUse[4];
            drNew.UseN4 = dUse[5];
            drNew.UseN5 = dUse[6];
            drNew.UseN6 = dUse[7];
            drNew.OdrN_1 = dOdr[0];
            drNew.OdrN0 = dOdr[1];
            drNew.OdrN1 = dOdr[2];
            drNew.OdrN2 = dOdr[3];
            drNew.OdrN3 = dOdr[4];
            drNew.OdrN4 = dOdr[5];
            drNew.OdrN5 = dOdr[6];
            drNew.OdrN6 = dOdr[7];
            drNew.ZaiN_1 = dZai[0];
            drNew.ZaiN0 = dZai[1];
            drNew.ZaiN1 = dZai[2];
            drNew.ZaiN2 = dZai[3];
            drNew.ZaiN3 = dZai[4];
            drNew.ZaiN4 = dZai[5];
            drNew.ZaiN5 = dZai[6];
            drNew.ZaiN6 = dZai[7];

            if (drNew != null)
            {
                return drNew;
            }
            return null;
        }
















    }
}
