using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core;
using Core.Sql;

namespace m2mKoubaiDAL
{
    public class NouhinClass
    {
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
        public static void getYAE_M_TorihikisakiDataTable(string Year, string strText, int nKubun, int nStartIndex, int nCount,
            SqlConnection sqlConn, out NouhinDataSet.V_NouhinDataTable dt, ref int nTotal)
        {
            Core.Sql.RowNumberInfo info = new Core.Sql.RowNumberInfo();
            info.nStartNumber = nStartIndex + 1;
            info.nEndNumber = nStartIndex + nCount;
            info.strOverText = "HacchuuNo ";

            SqlCommand cmd = new SqlCommand("", sqlConn);
            cmd.CommandText = "SELECT * FROM V_Nouhin ";
            cmd.CommandText += " WHERE Year = @Year AND JigyoushoKubun = @JigyoushoKubun ";
            if (strText.Trim() != "")
            {
                cmd.CommandText += " AND HacchuuNo LIKE @h ";
                cmd.Parameters.AddWithValue("@h", "%" + strText + "%");
            }
            cmd.Parameters.AddWithValue("@Year", Year);
            cmd.Parameters.AddWithValue("@JigyoushoKubun", nKubun);

            dt = new NouhinDataSet.V_NouhinDataTable();
            info.LoadData(cmd, sqlConn, dt, ref nTotal);
        }

        public static m2mKoubaiDataSet.T_NouhinDataTable getT_NouhinDataTable(string Year, string HacchuuNo, int nKubun, SqlConnection sqlConn)
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

        /// <summary>
        /// 注文Noによって、注文データを取得
        /// </summary>
        /// <param name="HacchuuNo"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        /// 修正 09/07/23 (LEFT OUTER JOIN)
        public static NouhinDataSet.V_NouhinRow getV_NouhinRow(string Year, string HacchuuNo, int nKubun, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
              " SELECT dbo.T_Chumon.Year, dbo.T_Chumon.HacchuuNo, dbo.T_Chumon.ShiiresakiCode, "
            + "dbo.M_Shiiresaki.ShiiresakiMei, dbo.M_Buhin.BuhinMei, "
            + "dbo.T_Chumon.BuhinCode, dbo.T_Chumon.BuhinKubun, dbo.T_Chumon.Suuryou,  "
            + "dbo.T_Chumon.Tanka, dbo.M_Buhin.Tani,  "
            + "dbo.M_NounyuuBasho.BashoMei, dbo.T_Chumon.Kingaku, "
            + "dbo.T_Chumon.Zeiritu , "
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
                    + " dbo.T_Chumon.KannouFlg, dbo.T_Chumon.JigyoushoKubun, dbo.T_Chumon.KaritankaFlg "

            + "FROM                     dbo.T_Chumon INNER JOIN "
            + "dbo.M_NounyuuBasho ON dbo.T_Chumon.NounyuuBashoCode = dbo.M_NounyuuBasho.BashoCode INNER JOIN "
            + "dbo.M_Shiiresaki ON dbo.T_Chumon.ShiiresakiCode = dbo.M_Shiiresaki.ShiiresakiCode LEFT OUTER JOIN "
            + "dbo.M_Buhin ON dbo.T_Chumon.BuhinCode = dbo.M_Buhin.BuhinCode "
            + "WHERE                   (dbo.T_Chumon.Year = @Year) AND (dbo.T_Chumon.HacchuuNo = @HacchuuNo) AND "
            + "(dbo.T_Chumon.CancelBi IS NULL) AND (dbo.T_Chumon.JigyoushoKubun = @JigyoushoKubun) ";

            da.SelectCommand.Parameters.AddWithValue("@Year", Year);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", HacchuuNo);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nKubun);
            NouhinDataSet.V_NouhinDataTable dt = new NouhinDataSet.V_NouhinDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (NouhinDataSet.V_NouhinRow)dt.Rows[0];
            else
                return null;
        }
        /// <summary>
        /// 納品テーブルにデータを登録
        /// (数量 = 納品数になった場合か、kannouFlg(変数)がtrue(NouhinFormのBtnKNクリック)の場合T_ChumonのKantouFlgをtrueにする)
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LibError T_Nouhin_Insert_T_Chumon_Update
            (m2mKoubaiDataSet.T_NouhinRow drNouhin, m2mKoubaiDataSet.T_ChumonRow drChumon,
            string Year, string HacchuuNo, int nKubun, int nRowNo, bool kannouFlg, string TourokuUser, SqlConnection sqlConn)
        {
            SqlDataAdapter daN = new SqlDataAdapter("", sqlConn);
            daN.SelectCommand.CommandText = "SELECT * FROM T_Nouhin";
            daN.InsertCommand = (new SqlCommandBuilder(daN)).GetInsertCommand();
            m2mKoubaiDataSet.T_NouhinDataTable dtN = new m2mKoubaiDataSet.T_NouhinDataTable();
            m2mKoubaiDataSet.T_NouhinRow drNew = dtN.NewT_NouhinRow();

            SqlDataAdapter daNs = new SqlDataAdapter("", sqlConn);
            daNs.SelectCommand.CommandText =
                "SELECT SUM(Suuryou) as SuryouSum FROM T_Nouhin "
                + "WHERE Year = @Year AND HacchuuNo = @HacchuuNo AND JigyoushoKubun = @JigyoushoKubun";
            daNs.SelectCommand.Parameters.AddWithValue("@Year", Year);
            daNs.SelectCommand.Parameters.AddWithValue("@HacchuuNo", HacchuuNo);
            daNs.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nKubun);
            NouhinDataSet.V_Nouhin_SuryouSumDataTable dtNs = new NouhinDataSet.V_Nouhin_SuryouSumDataTable();
            daNs.Fill(dtNs);

            SqlDataAdapter daC = new SqlDataAdapter("", sqlConn);
            daC.SelectCommand.CommandText =
                "SELECT * FROM T_Chumon "
                + "WHERE Year = @Year AND HacchuuNo = @HacchuuNo AND JigyoushoKubun = @JigyoushoKubun ";
            daC.SelectCommand.Parameters.AddWithValue("@Year", Year);
            daC.SelectCommand.Parameters.AddWithValue("@HacchuuNo", HacchuuNo);
            daC.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nKubun);
            daC.UpdateCommand = (new SqlCommandBuilder(daC)).GetUpdateCommand();
            m2mKoubaiDataSet.T_ChumonDataTable dtC = new m2mKoubaiDataSet.T_ChumonDataTable();
            daC.Fill(dtC);
            if (1 != dtC.Rows.Count)
                return new LibError("エラー");

            SqlTransaction sqlTran = null;
            try
            {
                sqlConn.Open();
                sqlTran = sqlConn.BeginTransaction();

                daN.InsertCommand.Transaction = sqlTran;
                daC.UpdateCommand.Transaction = sqlTran;

                // 納品テーブル登録
                try
                {
                    //drNew.ItemArray = drNouhin.ItemArray;
                    drNew.Year = drNouhin.Year;
                    drNew.HacchuuNo = drNouhin.HacchuuNo;
                    drNew.JigyoushoKubun = drNouhin.JigyoushoKubun;
                    drNew.NouhinNo = drNouhin.NouhinNo;
                    drNew.NouhinBi = drNouhin.NouhinBi;
                    drNew.Tanka = drChumon.Tanka;
                    drNew.Suuryou = drNouhin.Suuryou;
                    drNew.Zeiritu = drNouhin.Zeiritu;
                    drNew.TourokuBi = DateTime.Now;
                    drNew.TourokuUser = TourokuUser;
                    drNew.KeigenZeirituFlg = drNouhin.KeigenZeirituFlg;

                    dtN.Rows.Add(drNew);
                    daN.Update(dtN);
                }
                catch (Exception e)
                {
                    if (sqlTran != null)
                        sqlTran.Rollback();
                    return new LibError(e);
                }
                // 納品データがまだない場合、dtNs[0].SuryouSumはnullでなく空白になるので、その場合0を代入
                try { int.Parse(dtNs[0].SuryouSum.ToString()); }
                catch { dtNs[0].SuryouSum = 0; }

                if ((dtNs[0].SuryouSum + drNew.Suuryou) == dtC[0].Suuryou || kannouFlg)
                {
                    // 注文テーブル更新
                    try
                    {
                        m2mKoubaiDataSet.T_ChumonRow drThis = (m2mKoubaiDataSet.T_ChumonRow)dtC.Rows[0];
                        drThis.KannouFlg = drChumon.KannouFlg;
                        daC.Update(dtC);
                    }
                    catch (Exception e)
                    {
                        if (sqlTran != null)
                            sqlTran.Rollback();
                        return new LibError(e);
                    }
                }

                sqlTran.Commit();
                return null;
            }
            catch (Exception e)
            {
                if (sqlTran != null)
                    sqlTran.Rollback();
                return new LibError(e);
            }
            finally
            {
                sqlConn.Close();
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