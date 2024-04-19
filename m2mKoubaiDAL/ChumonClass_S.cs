using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace m2mKoubaiDAL
{
    public class ChumonClass_S
    {
        /// <summary>
        /// 今年の注文Noの最大値を取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns> 
        public static int GetMaxHacchuuNo(SqlConnection sqlConn)
        {            
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "SELECT MAX(HacchuuNo) as HacchuuNo FROM T_Chumon WHERE Year = @Year ";     
            da.SelectCommand.Parameters.AddWithValue("@Year", DateTime.Now.ToString("yy"));
            //da.SelectCommand.Parameters.AddWithValue("@Kubun", nKubun);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];            
            if (dt.Rows[0].IsNull("HacchuuNo"))
            {
                return 0;
            }
            else
            {                
                return (int.Parse(dt.Rows[0]["HacchuuNo"].ToString()));
            }

        }


        /// <summary>
        /// 発注登録
        /// </summary>
        /// <param name="dtOrder"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LibError
            T_Chumon_Insert(ChumonDataSet_S.V_OrderInputDataTable dtOrder, string strLoginID, int nKubun, int Zeiritu, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_Chumon";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            m2mKoubaiDataSet.T_ChumonDataTable dt = new m2mKoubaiDataSet.T_ChumonDataTable();

            DateTime date = DateTime.Now;
            string strYear = date.ToString("yy");
            int nOrderNo = GetMaxHacchuuNo(sqlConn);

            
            SqlTransaction sqlTran = null;
            try
            {
                sqlConn.Open();
                sqlTran = sqlConn.BeginTransaction();
                da.InsertCommand.Transaction = sqlTran;

                for (int i = 0; i < dtOrder.Rows.Count; i++)
                {
                    m2mKoubaiDataSet.T_ChumonRow drNew = dt.NewT_ChumonRow();

                    nOrderNo++;

                    drNew.Year = strYear;
                    drNew.HacchuuNo = nOrderNo.ToString("0000000");
                    drNew.JigyoushoKubun = nKubun;
                    drNew.ShiiresakiCode = dtOrder[i].ShiiresakiCode;
                    drNew.BuhinKubun = dtOrder[i].BuhinKubun;
                    drNew.BuhinCode = dtOrder[i].BuhinCode;
                    drNew.Tanka = decimal.Parse(dtOrder[i].Tanka);
                    drNew.Suuryou = int.Parse(dtOrder[i].Suuryou.Replace(",",""));
                    // 増税対応（注文金額＝「注文単価×注文数量」　※DB上は小数2桁まで保持。画面上は小数部は四捨五入）
                    drNew.Kingaku = Math.Round(drNew.Tanka * drNew.Suuryou, 2);
                    drNew.Zeiritu = Zeiritu;
                    drNew.Nouki = dtOrder[i].Nouki.Replace("/","");
                    drNew.NounyuuBashoCode = dtOrder[i].NounyuuBashoCode;
                    drNew.Bikou = dtOrder[i].Bikou;
                    drNew.HacchuuBi = date;
                    drNew.HacchushaID = strLoginID;
                    drNew.KannouFlg = false;
                    if (dtOrder[i].KariTankaFlg == "0")
                    {
                        drNew.KaritankaFlg = true;
                    }
                    else
                    {
                        drNew.KaritankaFlg = false;
                    }
                    
                   // drNew.DataDLFlg = false;
                   // drNew.HacchushoInsatsuFlg = false;

                    dt.Rows.Add(drNew);                   
                }
                da.Update(dt);
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
  
    }
}
