using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace m2mKoubaiDAL
{
    public class UploadClass
    {
        public static void SaveM_Buhin(m2mKoubaiDataSet.M_BuhinDataTable dt, DateTime torikomiBi, string fileName, SqlConnection sqlCon)
        {
            DateTime now = DateTime.Now;

            string sql = "SELECT * FROM M_Buhin WHERE BuhinCode = @BuhinCode";
            SqlDataAdapter da = new SqlDataAdapter(sql, sqlCon);
            da.SelectCommand.Parameters.Add("@BuhinCode", System.Data.SqlDbType.NVarChar);
            da.InsertCommand = new SqlCommandBuilder(da).GetInsertCommand();
            da.UpdateCommand = new SqlCommandBuilder(da).GetUpdateCommand();

            string sqlUpload = "SELECT * FROM M_BuhinUpload";
            SqlDataAdapter daUpload = new SqlDataAdapter(sqlUpload, sqlCon);
            daUpload.InsertCommand = new SqlCommandBuilder(daUpload).GetInsertCommand();

            SqlTransaction sqlTran = null;

            try
            {
                sqlCon.Open();
                sqlTran = sqlCon.BeginTransaction();
                da.SelectCommand.Transaction = da.InsertCommand.Transaction = da.UpdateCommand.Transaction = sqlTran;
                daUpload.SelectCommand.Transaction = daUpload.InsertCommand.Transaction = sqlTran;

                m2mKoubaiDataSet.M_BuhinDataTable dtThis = new m2mKoubaiDataSet.M_BuhinDataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    da.SelectCommand.Parameters["@BuhinCode"].Value = dt[i].BuhinCode;

                    da.Fill(dtThis);

                    if (dtThis.Rows.Count == 0)
                    {
                        // ■新規
                        dtThis.ImportRow(dt[i]);
                    }
                    else
                    {
                        // ■更新
                        dtThis[0].ItemArray = dt[i].ItemArray;
                    }

                    da.Update(dtThis);

                    // ■アップロードファイルデータを保存
                    m2mKoubaiDataSet.M_BuhinUploadDataTable dtUpload = new m2mKoubaiDataSet.M_BuhinUploadDataTable();
                    m2mKoubaiDataSet.M_BuhinUploadRow drUpload = dtUpload.NewM_BuhinUploadRow();
                    drUpload.BuhinCode = dt[i].BuhinCode;
                    drUpload.BuhinKubun = dt[i].BuhinKubun;
                    drUpload.BuhinMei = dt[i].BuhinMei;
                    drUpload.Tani = dt[i].Tani;
                    drUpload.LT_Suuji = dt[i].LT_Suuji;
                    drUpload.LT_Tani = dt[i].LT_Tani;
                    drUpload.Tanka = dt[i].Tanka;
                    drUpload.Lot = dt[i].Lot;
                    drUpload.ShiiresakiCode1 = dt[i].ShiiresakiCode1;
                    drUpload.ShiiresakiCode2 = dt[i].ShiiresakiCode2;
                    drUpload.KanjyouKamokuCode = dt[i].KanjyouKamokuCode;
                    drUpload.HiyouKamokuCode = dt[i].HiyouKamokuCode;
                    drUpload.HojyoKamokuNo = dt[i].HojyoKamokuNo;
                    drUpload.TorikomiBi = torikomiBi;
                    drUpload.FileName = fileName;
                    drUpload.TourokuBi = now;
                    dtUpload.AddM_BuhinUploadRow(drUpload);

                    daUpload.Update(dtUpload);

                    dtThis.Clear();
                }


                sqlTran.Commit();
            }
            catch (Exception ex)
            {
                sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public static void SaveT_Chumon(m2mKoubaiDataSet.T_ChumonDataTable dt, string fileName, SqlConnection sqlCon)
        {
            DateTime now = DateTime.Now;

            string sqlMaxHacchuuNo = "SELECT MAX(HacchuuNo) as HacchuuNo FROM T_Chumon WHERE Year = @Year ";
            SqlCommand cmdMaxHacchuuNo = new SqlCommand(sqlMaxHacchuuNo, sqlCon);
            cmdMaxHacchuuNo.Parameters.AddWithValue("@Year", DateTime.Now.ToString("yy"));

            string sqlChumon = "SELECT * FROM T_Chumon WHERE Year = @Year AND HacchuuNo = @HacchuuNo AND JigyoushoKubun = @JigyoushoKubun";
            SqlDataAdapter daChumon = new SqlDataAdapter(sqlChumon, sqlCon);
            daChumon.SelectCommand.Parameters.Add("@Year", System.Data.SqlDbType.NVarChar);
            daChumon.SelectCommand.Parameters.Add("@HacchuuNo", System.Data.SqlDbType.NVarChar);
            daChumon.SelectCommand.Parameters.Add("@JigyoushoKubun", System.Data.SqlDbType.Int);
            daChumon.InsertCommand = new SqlCommandBuilder(daChumon).GetInsertCommand();
            //daChumon.UpdateCommand = new SqlCommandBuilder(daChumon).GetUpdateCommand();

            string sqlBuhinKubun = "SELECT BuhinKubun FROM M_Buhin WHERE BuhinCode = @BuhinCode";
            SqlCommand cmdBuhinKubun = new SqlCommand(sqlBuhinKubun, sqlCon);
            cmdBuhinKubun.Parameters.Add("@BuhinCode", System.Data.SqlDbType.NVarChar);
            SqlDataReader readerBuhinKubun = null;

            string sqlUpload = "SELECT * FROM T_ChumonUpload";
            SqlDataAdapter daUpload = new SqlDataAdapter(sqlUpload, sqlCon);
            daUpload.InsertCommand = new SqlCommandBuilder(daUpload).GetInsertCommand();

            SqlTransaction sqlTran = null;

            try
            {
                sqlCon.Open();
                sqlTran = sqlCon.BeginTransaction();
                cmdMaxHacchuuNo.Transaction = sqlTran;
                daChumon.SelectCommand.Transaction = daChumon.InsertCommand.Transaction = sqlTran;
                cmdBuhinKubun.Transaction = sqlTran;
                daUpload.SelectCommand.Transaction = daUpload.InsertCommand.Transaction = sqlTran;

                // ■発注番号を取得
                object objMaxHacchuuNo = cmdMaxHacchuuNo.ExecuteScalar();
                int maxHacchuuNo = 0;
                if (objMaxHacchuuNo != DBNull.Value)
                {
                    maxHacchuuNo = Convert.ToInt32(objMaxHacchuuNo);
                }

                // ■発注テーブルを更新
                m2mKoubaiDataSet.T_ChumonDataTable dtChumonThis = new m2mKoubaiDataSet.T_ChumonDataTable();

                m2mKoubaiDataSet.T_ChumonUploadDataTable dtUpload = new m2mKoubaiDataSet.T_ChumonUploadDataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // -- 商品レコードの存在確認
                    cmdBuhinKubun.Parameters["@BuhinCode"].Value = dt[i].BuhinCode;
                    readerBuhinKubun = cmdBuhinKubun.ExecuteReader();
                    string buhinKubun = "";

                    try
                    {
                        bool isOk = readerBuhinKubun.Read();
                        if (!isOk)
                        {
                            throw new Exception(string.Format("品番:{0}に紐づく商品が存在しません。", dt[i].BuhinCode));
                        }

                        buhinKubun = readerBuhinKubun["BuhinKubun"].ToString();
                    }
                    finally
                    {
                        if (readerBuhinKubun != null)
                        {
                            readerBuhinKubun.Close();
                        }
                    }

                    m2mKoubaiDataSet.T_ChumonRow drChumonThis = dtChumonThis.NewT_ChumonRow();
                    drChumonThis.Year = dt[i].Year;
                    drChumonThis.HacchuuNo = (++maxHacchuuNo).ToString("0000000");
                    drChumonThis.JigyoushoKubun = dt[i].JigyoushoKubun;
                    drChumonThis.ShiiresakiCode = dt[i].ShiiresakiCode;
                    drChumonThis.BuhinKubun = buhinKubun;
                    drChumonThis.BuhinCode = dt[i].BuhinCode;
                    drChumonThis.Tanka = dt[i].Tanka;
                    drChumonThis.Suuryou = dt[i].Suuryou;
                    // 増税対応 金額は切り捨てではなく四捨五入に変更
                    //drChumonThis.Kingaku = (int)Math.Floor(dt[i].Tanka * dt[i].Suuryou);
                    drChumonThis.Kingaku = Math.Round(dt[i].Tanka * dt[i].Suuryou, 2);
                    drChumonThis.Nouki = dt[i].Nouki;
                    drChumonThis.NounyuuBashoCode = dt[i].NounyuuBashoCode;
                    drChumonThis.Bikou = dt[i].Bikou;
                    drChumonThis.HacchuuBi = dt[i].HacchuuBi;
                    drChumonThis.HacchushaID = dt[i].HacchushaID;
                    drChumonThis.KannouFlg = dt[i].KannouFlg;
                    drChumonThis.KaritankaFlg = dt[i].KaritankaFlg;
                    drChumonThis.Zeiritu = dt[i].Zeiritu;
                    dtChumonThis.AddT_ChumonRow(drChumonThis);

                    // ■ファイルデータをDBに保存
                    m2mKoubaiDataSet.T_ChumonUploadRow drUpload = dtUpload.NewT_ChumonUploadRow();
                    drUpload.Year = drChumonThis.Year;
                    drUpload.HacchuuNo = drChumonThis.HacchuuNo;
                    drUpload.JigyoushoKubun = drChumonThis.JigyoushoKubun;
                    drUpload.ShiiresakiCode = drChumonThis.ShiiresakiCode;
                    drUpload.BuhinCode = drChumonThis.BuhinCode;
                    drUpload.Tanka = drChumonThis.Tanka;
                    drUpload.Suuryou = drChumonThis.Suuryou;
                    drUpload.Nouki = drChumonThis.Nouki;
                    drUpload.NounyuuBashoCode = drChumonThis.NounyuuBashoCode;
                    drUpload.Bikou = drChumonThis.Bikou;
                    drUpload.HacchushaID = drChumonThis.HacchushaID;
                    drUpload.FileName = fileName;
                    drUpload.TourokuBi = now;
                    dtUpload.AddT_ChumonUploadRow(drUpload);
                }

                daChumon.Update(dtChumonThis);

                daUpload.Update(dtUpload);

                sqlTran.Commit();
            }
            catch (Exception ex)
            {
                sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
