using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
namespace m2mKoubaiDAL
{
    public class NouhinClass_N
    {
        public static m2mKoubaiDataSet.T_NouhinDataTable
            getT_NouhinDataTable(string Year, string HacchuuNo, int nKubun, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_Nouhin WHERE Year = @Year AND HacchuuNo = @HacchuuNo  AND JigyoushoKubun = @JigyoushoKubun ";
            da.SelectCommand.Parameters.AddWithValue("@Year", Year);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", HacchuuNo);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nKubun);
            m2mKoubaiDataSet.T_NouhinDataTable dt = new m2mKoubaiDataSet.T_NouhinDataTable();
            da.Fill(dt);
            return dt;
        }


        public class KensakuParam
        {
            public string _HacchuNo = "";
        }
        /// <summary>
        /// Where文を作成
        /// </summary>
        /// <param name="k"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private static string WhereText(KensakuParam k, SqlCommand cmd)
        {
            Core.Sql.WhereGenerator w = new Core.Sql.WhereGenerator();
            if (k._HacchuNo != "")
            {
                w.Add(string.Format("T_Chumon.HacchuuNo = '{0}'", k._HacchuNo));
            }
            return w.WhereText;
        }

        /// <summary>
        /// 注文Noによって、注文データを取得
        /// </summary>
        /// <param name="HacchuuNo"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static NouhinDataSet_N.V_NouhinRow
            getV_NouhinRow(string Year, string HacchuuNo, int nKubun, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            " SELECT dbo.T_Chumon.Year, dbo.T_Chumon.HacchuuNo, dbo.T_Chumon.ShiiresakiCode, "
            + "dbo.M_Shiiresaki.ShiiresakiMei, dbo.M_Buhin.BuhinMei, "
            + "dbo.T_Chumon.BuhinCode, dbo.T_Chumon.BuhinKubun, dbo.T_Chumon.Suuryou,  "
            + "dbo.T_Chumon.Tanka, dbo.M_Buhin.Tani,  "
            + "dbo.M_NounyuuBasho.BashoMei, dbo.T_Chumon.Kingaku, "

                    + "(SELECT TOP (1) HenkouNo "
                    + "FROM dbo.T_NoukiHenkou "
                    + "WHERE (Year = dbo.T_Chumon.Year) AND (HacchuuNo = dbo.T_Chumon.HacchuuNo) AND (JigyoushoKubun = T_Chumon.JigyoushoKubun) "
                    + "ORDER BY HenkouNo DESC) AS HenkouNo, "

                    + "(SELECT TOP (1) KaitouNo "
                    + "FROM dbo.T_NoukiKaitou "
                    + "WHERE (Year = dbo.T_Chumon.Year) AND (HacchuuNo = dbo.T_Chumon.HacchuuNo) AND (JigyoushoKubun = T_Chumon.JigyoushoKubun) "
                    + "ORDER BY KaitouNo DESC) AS KaitouNo, "

                    + "(SELECT TOP (1) NouhinNo "
                    + "FROM dbo.T_Nouhin "
                    + "WHERE (Year = dbo.T_Chumon.Year) AND (HacchuuNo = dbo.T_Chumon.HacchuuNo) AND (JigyoushoKubun = T_Chumon.JigyoushoKubun) "
                    + "ORDER BY NouhinNo DESC) AS NouhinNo, dbo.T_Chumon.Nouki, dbo.T_Chumon.HacchuuBi, "

                    + "(SELECT SUM(Suuryou) AS NouhinSuuryou "
                    + "FROM dbo.T_Nouhin AS T_Nouhin_1 "
                    + "WHERE (Year = dbo.T_Chumon.Year) AND (HacchuuNo = dbo.T_Chumon.HacchuuNo) AND (JigyoushoKubun = T_Chumon.JigyoushoKubun)) AS NouhinSuuryou,  "
                    + " dbo.T_Chumon.KannouFlg, dbo.T_Chumon.JigyoushoKubun "

            + "FROM                     dbo.T_Chumon INNER JOIN "
            + "dbo.M_NounyuuBasho ON dbo.T_Chumon.NounyuuBashoCode = dbo.M_NounyuuBasho.BashoCode INNER JOIN "
            + "dbo.M_Shiiresaki ON dbo.T_Chumon.ShiiresakiCode = dbo.M_Shiiresaki.ShiiresakiCode LEFT OUTER JOIN "
            + "dbo.M_Buhin ON dbo.T_Chumon.BuhinCode = dbo.M_Buhin.BuhinCode "
            + "WHERE                   (dbo.T_Chumon.Year = @Year) AND (dbo.T_Chumon.HacchuuNo = @HacchuuNo) AND "
            + "(dbo.T_Chumon.CancelBi IS NULL) AND (dbo.T_Chumon.JigyoushoKubun = @JigyoushoKubun) ";



            da.SelectCommand.Parameters.AddWithValue("@Year", Year);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", HacchuuNo);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nKubun);
            NouhinDataSet_N.V_NouhinDataTable dt = new NouhinDataSet_N.V_NouhinDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (NouhinDataSet_N.V_NouhinRow)dt.Rows[0];
            else
                return null;
        }
        /// <summary>
        /// 納品テーブルにデータを登録
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LibError
            T_Nouhin_Insert(m2mKoubaiDataSet.T_NouhinRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_Nouhin";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            m2mKoubaiDataSet.T_NouhinDataTable dt = new m2mKoubaiDataSet.T_NouhinDataTable();
            m2mKoubaiDataSet.T_NouhinRow drNew = dt.NewT_NouhinRow();
            try
            {
                //drNew.ItemArray = dr.ItemArray;
                drNew.Year = dr.Year;
                drNew.HacchuuNo = dr.HacchuuNo;
                drNew.NouhinNo = dr.NouhinNo;
                drNew.NouhinBi = dr.NouhinBi;
                drNew.Suuryou = dr.Suuryou;
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
        /// NewRow
        /// </summary>
        /// <returns></returns>
        public static m2mKoubaiDataSet.T_NouhinRow
            newT_NouhinRow()
        {
            return new m2mKoubaiDataSet.T_NouhinDataTable().NewT_NouhinRow();
        }
    }
}
