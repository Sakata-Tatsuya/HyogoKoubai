using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;

namespace m2mKoubaiDAL
{
    public class NoukiHenkouClass
    {
        public static m2mKoubaiDataSet.T_NoukiHenkouDataTable getT_NoukiHenkouDataTable(SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_NoukiHenkou";
            m2mKoubaiDataSet.T_NoukiHenkouDataTable dt = new m2mKoubaiDataSet.T_NoukiHenkouDataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="HacchuuNo"></param>
        /// <param name="ShiiresakiCode"></param>
        /// <param name="HenkouNo"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static m2mKoubaiDataSet.T_NoukiHenkouDataTable getT_NoukiHenkouDataTable(string Year, string HacchuuNo, int nKubun, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_NoukiHenkou WHERE Year = @Year AND HacchuuNo = @HacchuuNo AND JigyoushoKubun = @JigyoushoKubun ";
            da.SelectCommand.Parameters.AddWithValue("@Year", Year);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", HacchuuNo);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nKubun);
            m2mKoubaiDataSet.T_NoukiHenkouDataTable dt = new m2mKoubaiDataSet.T_NoukiHenkouDataTable();
            da.Fill(dt);
            return dt;
        }

        public static m2mKoubaiDataSet.T_NoukiHenkouRow newT_NoukiHenkouRow()
        {
            return new m2mKoubaiDataSet.T_NoukiHenkouDataTable().NewT_NoukiHenkouRow();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strYear"></param>
        /// <param name="strNo"></param>
        /// <param name="nNo"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static m2mKoubaiDataSet.T_NoukiHenkouDataTable getT_NoukiHenkouDataTable(string strYear, string strNo, int nKubun, int nNo, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT T_NoukiHenkou.* "
            + "FROM T_NoukiHenkou "
            + "WHERE (HenkouNo = @HenkouNo) AND (Year = @Year) AND (HacchuuNo = @HacchuuNo) AND (JigyoushoKubun = @JigyoushoKubun) ";
            da.SelectCommand.Parameters.AddWithValue("@Year", strYear);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", strNo);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nKubun);
            da.SelectCommand.Parameters.AddWithValue("@HenkouNo", nNo);
            m2mKoubaiDataSet.T_NoukiHenkouDataTable dt = new m2mKoubaiDataSet.T_NoukiHenkouDataTable();
            da.Fill(dt);
            return dt;
        }

        //Å@î[ä˙âÒìöçXêV
        public static LibError T_NoukiHenkou_Update(string year, string no, int nKubun, int nhenkouNo, string LoginID, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                                            "SELECT * FROM T_NoukiHenkou "
                                            + "WHERE  Year = @Year AND HacchuuNo = @HacchuuNo AND "
                                            + "HenkouNo = @HenkouNo AND JigyoushoKubun = @JigyoushoKubun  ";

            da.SelectCommand.Parameters.AddWithValue("@Year", year);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", no);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nKubun);
            da.SelectCommand.Parameters.AddWithValue("@HenkouNo", nhenkouNo);
            da.UpdateCommand = (new SqlCommandBuilder(da).GetUpdateCommand());

            m2mKoubaiDataSet.T_NoukiHenkouDataTable dtNew = new m2mKoubaiDataSet.T_NoukiHenkouDataTable();
            da.Fill(dtNew);
            try
            {
                for (int i = 0; i < dtNew.Rows.Count; i++)
                {
                    m2mKoubaiDataSet.T_NoukiHenkouRow dr = dtNew.Rows[i] as m2mKoubaiDataSet.T_NoukiHenkouRow;
                    dr.ShouninFlg = true;
                    dr.ShouninshaID = LoginID;
                    da.Update(dtNew);
                }
                da.Update(dtNew);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
        }

        //Å@î[ä˙ïœçXìoò^ÅEçXêV
        public static LibError T_NoukiHenkou_Update(string strYear, string strHacchuuNo, int nKubun,
                               Hashtable NoukiHenkouDataTbl, int nHenkouNo, SqlConnection sqlConn)
        {
            ArrayList aryNouki = (ArrayList)NoukiHenkouDataTbl["Nouki"];
            ArrayList arySuuryou = (ArrayList)NoukiHenkouDataTbl["Suuryou"];
            ArrayList aryKaitouNo = (ArrayList)NoukiHenkouDataTbl["HenkouNo"];
            int nRowNo = 1;

                SqlDataAdapter daHenkouNoukiIns = new SqlDataAdapter("", sqlConn);
                daHenkouNoukiIns.SelectCommand.CommandText =
                    "SELECT * FROM T_NoukiHenkou";
                daHenkouNoukiIns.InsertCommand = (new SqlCommandBuilder(daHenkouNoukiIns).GetInsertCommand());

                SqlDataAdapter daC = new SqlDataAdapter("", sqlConn);
                daC.SelectCommand.CommandText =
                "SELECT * FROM T_Chumon "
                + "WHERE Year = @Year AND HacchuuNo = @HacchuuNo AND JigyoushoKubun = @JigyoushoKubun ";
                daC.SelectCommand.Parameters.AddWithValue("@Year", strYear);
                daC.SelectCommand.Parameters.AddWithValue("@HacchuuNo", strHacchuuNo);
                daC.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nKubun);
                daC.UpdateCommand = (new SqlCommandBuilder(daC)).GetUpdateCommand();
                m2mKoubaiDataSet.T_ChumonDataTable dtC = new m2mKoubaiDataSet.T_ChumonDataTable();
                daC.Fill(dtC);
                if (dtC.Rows.Count != 1)
                {
                    return new LibError("error");
                }

                SqlTransaction sqlTran = null;
                try
                {
                    sqlConn.Open();
                    sqlTran = sqlConn.BeginTransaction();

                    daHenkouNoukiIns.SelectCommand.Transaction = daHenkouNoukiIns.InsertCommand.Transaction = sqlTran;
                    daC.UpdateCommand.Transaction = sqlTran;

                    m2mKoubaiDataSet.T_NoukiHenkouDataTable dtHenkouNoukiIns = new m2mKoubaiDataSet.T_NoukiHenkouDataTable();

                    //daHenkouNoukiIns.Fill(dtHenkouNoukiIns);               
                    // àÍî‘íxÇ¢î[ä˙
                    int nMaxNouki = 0;
                    for (int i = 0; i < aryKaitouNo.Count; i++)
                    {
                        m2mKoubaiDataSet.T_NoukiHenkouRow drHenkouNoukiIns =
                            dtHenkouNoukiIns.NewT_NoukiHenkouRow();

                        drHenkouNoukiIns.Year = strYear;
                        drHenkouNoukiIns.HacchuuNo = strHacchuuNo;
                        drHenkouNoukiIns.JigyoushoKubun = nKubun;
                        drHenkouNoukiIns.RowNo = nRowNo;
                        drHenkouNoukiIns.HenkouNo = nHenkouNo + 1;
                        drHenkouNoukiIns.Nouki = Convert.ToInt32(aryNouki[i]);

                        if (nMaxNouki < drHenkouNoukiIns.Nouki)
                        {
                            nMaxNouki = drHenkouNoukiIns.Nouki;
                        }

                        drHenkouNoukiIns.Suuryou = Convert.ToInt32(arySuuryou[i]);
                        drHenkouNoukiIns.Tourokubi = DateTime.Now;
                        drHenkouNoukiIns.ShouninFlg = false;
                        drHenkouNoukiIns.ShouninshaID = "";

                        dtHenkouNoukiIns.AddT_NoukiHenkouRow(drHenkouNoukiIns);
                        nRowNo++;
                    }
                    daHenkouNoukiIns.Update(dtHenkouNoukiIns);

                    // íçï∂ÉfÅ[É^ÇÃî[ä˙ÇçXêV
                    // ï™î[ÇÃèÍçáÇÕÅAàÍî‘íxÇ¢î[ä˙ÇÉZÉbÉgÇ∑ÇÈÅB
                    dtC[0].Nouki = nMaxNouki.ToString();
                    daC.Update(dtC);

                    sqlTran.Commit();
                    return null;
                }
                catch (Exception e)
                {
                    if (sqlTran != null)
                    {
                        sqlTran.Rollback();
                    }
                    return new LibError(e);
                }
                finally
                {
                    sqlConn.Close();
                }
            
        }
    }



}

    
