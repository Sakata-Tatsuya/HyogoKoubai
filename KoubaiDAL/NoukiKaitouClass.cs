using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
namespace KoubaiDAL
{
    public class NoukiKaitouClass
    {
        public static KoubaiDataSet.T_NoukiKaitouDataTable getT_NoukiKaitouDataTable(SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_NoukiKaitou";
            KoubaiDataSet.T_NoukiKaitouDataTable dt = new KoubaiDataSet.T_NoukiKaitouDataTable();
            da.Fill(dt);
            return dt;
        }

        public static KoubaiDataSet.T_NoukiKaitouRow getT_NoukiKaitouRow(string Year, string HacchuuNo, int nKubun, int KaitouNo, int RowNo, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_NoukiKaitou WHERE Year = @Year AND HacchuuNo = @HacchuuNo AND KaitouNo = @KaitouNo AND RowNo = @RowNo AND JigyoushoKubun = @JigyoushoKubun";
            da.SelectCommand.Parameters.AddWithValue("@Year", Year);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", HacchuuNo);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nKubun);
            da.SelectCommand.Parameters.AddWithValue("@KaitouNo", KaitouNo);
            da.SelectCommand.Parameters.AddWithValue("@RowNo", RowNo);
            KoubaiDataSet.T_NoukiKaitouDataTable dt = new KoubaiDataSet.T_NoukiKaitouDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (KoubaiDataSet.T_NoukiKaitouRow)dt.Rows[0];
            else
                return null;
        }

        public static LibError T_NoukiKaitou_Insert(KoubaiDataSet.T_NoukiKaitouRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_NoukiKaitou";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            KoubaiDataSet.T_NoukiKaitouDataTable dt = new KoubaiDataSet.T_NoukiKaitouDataTable();
            try
            {
                KoubaiDataSet.T_NoukiKaitouRow drNew = dt.NewT_NoukiKaitouRow();
                //drNew.ItemArray = dr.ItemArray;
                drNew.Year = dr.Year;
                drNew.HacchuuNo = dr.HacchuuNo;
                drNew.JigyoushoKubun = dr.JigyoushoKubun;
                drNew.KaitouNo = dr.KaitouNo;
                drNew.RowNo = dr.RowNo;
                drNew.Nouki = dr.Nouki;
                drNew.Suuryou = dr.Suuryou;
                drNew.Tourokubi = dr.Tourokubi;
                drNew.ShouninFlg = dr.ShouninFlg;
                drNew.ShouninshaID = dr.ShouninshaID;
                dt.Rows.Add(drNew);
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
        }

        public static LibError T_NoukiKaitou_Update(string Year, string HacchuuNo, int nKubun, string ShiiresakiCode, int KaitouNo, 
            int RowNo, KoubaiDataSet.T_NoukiKaitouRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_NoukiKaitou WHERE Year = @Year AND HacchuuNo = @HacchuuNo AND  KaitouNo = @KaitouNo AND RowNo = @RowNo AND JigyoushoKubun = @JigyoushoKubun ";
            da.SelectCommand.Parameters.AddWithValue("@Year", Year);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", HacchuuNo);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nKubun);     
            da.SelectCommand.Parameters.AddWithValue("@KaitouNo", KaitouNo);
            da.SelectCommand.Parameters.AddWithValue("@RowNo", RowNo);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            KoubaiDataSet.T_NoukiKaitouDataTable dt = new KoubaiDataSet.T_NoukiKaitouDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new LibError();
            try
            {
                KoubaiDataSet.T_NoukiKaitouRow drThis = (KoubaiDataSet.T_NoukiKaitouRow)dt.Rows[0];
                //drNew.ItemArray = dr.ItemArray;
                drThis.Nouki = dr.Nouki;
                drThis.Suuryou = dr.Suuryou;
                drThis.Tourokubi = dr.Tourokubi;
                drThis.ShouninFlg = dr.ShouninFlg;
                drThis.ShouninshaID = dr.ShouninshaID;
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
        }

        public static LibError T_NoukiKaitou_Delete(string Year, string HacchuuNo, int nKubun, int KaitouNo, int RowNo, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_NoukiKaitou WHERE Year = @Year AND HacchuuNo = @HacchuuNo AND  KaitouNo = @KaitouNo AND RowNo = @RowNo AND JigyoushoKubun = @JigyoushoKubun ";
            da.SelectCommand.Parameters.AddWithValue("@Year", Year);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", HacchuuNo);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nKubun);     
            da.SelectCommand.Parameters.AddWithValue("@KaitouNo", KaitouNo);
            da.SelectCommand.Parameters.AddWithValue("@RowNo", RowNo);
            da.DeleteCommand = (new SqlCommandBuilder(da)).GetDeleteCommand();
            KoubaiDataSet.T_NoukiKaitouDataTable dt = new KoubaiDataSet.T_NoukiKaitouDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new LibError();
            try
            {
                KoubaiDataSet.T_NoukiKaitouRow drThis = (KoubaiDataSet.T_NoukiKaitouRow)dt.Rows[0];
                drThis.Delete();
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
        }

        public static KoubaiDataSet.T_NoukiKaitouRow newT_NoukiKaitouRow()
        {
            return new KoubaiDataSet.T_NoukiKaitouDataTable().NewT_NoukiKaitouRow();
        }

        //Å@î[ä˙âÒìöçXêV
        public static LibError
            T_NoukiKaitou_Update(string year, string no, int nKubun, string code, int nkaitouNo,
                                string LoginID, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_NoukiKaitou "
                                         + "WHERE  Year = @Year AND HacchuuNo = @HacchuuNo AND "
                                         + "KaitouNo = @KaitouNo AND JigyoushoKubun = @JigyoushoKubun ";

            da.SelectCommand.Parameters.AddWithValue("@Year", year);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", no);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nKubun);     
            da.SelectCommand.Parameters.AddWithValue("@KaitouNo", nkaitouNo);
            da.UpdateCommand = (new SqlCommandBuilder(da).GetUpdateCommand());
            KoubaiDataSet.T_NoukiKaitouDataTable dtNew = new KoubaiDataSet.T_NoukiKaitouDataTable();
            da.Fill(dtNew);
            try
            {

                for (int i = 0; i < dtNew.Rows.Count; i++)
                {
                    KoubaiDataSet.T_NoukiKaitouRow dr = dtNew.Rows[i] as KoubaiDataSet.T_NoukiKaitouRow;

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

        public static LibError T_NoukiKaitou_Insert(string strYear, string strHacchuuNo, int nKubun, 
            int nKaitouNo, string Suuryou, string strNouki, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_NoukiKaitou WHERE Year = @Year AND HacchuuNo = @HacchuuNo AND JigyoushoKubun = @JigyoushoKubun";

            da.SelectCommand.Parameters.AddWithValue("@Year", strYear);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", strHacchuuNo);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nKubun);     
            da.InsertCommand = (new SqlCommandBuilder(da).GetInsertCommand());
            KoubaiDataSet.T_NoukiKaitouDataTable dt = new KoubaiDataSet.T_NoukiKaitouDataTable();
           
            try
            {
                string[] strSuuryouAry = Suuryou.Split('_');
                string[] strNoukiAry = strNouki.Split('_');

                for (int i = 0; i < strNoukiAry.Length; i++)
                {
                    KoubaiDataSet.T_NoukiKaitouRow drNew = dt.NewT_NoukiKaitouRow();

                    drNew.Year = strYear;
                    drNew.HacchuuNo = strHacchuuNo;
                     drNew.JigyoushoKubun = nKubun;
                    drNew.KaitouNo = nKaitouNo + 1;
                    drNew.RowNo = i + 1;
                    drNew.Nouki = int.Parse(strNoukiAry[i]);
                    drNew.Suuryou = int.Parse(strSuuryouAry[i]);
                    drNew.Tourokubi = DateTime.Now;
                    drNew.ShouninFlg = false;
                    drNew.ShouninshaID = "";
                    dt.AddT_NoukiKaitouRow(drNew);
                }
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
        }

        //Å@î[ä˙âÒìöìoò^ÅEçXêV
        public static LibError T_NoukiKaitou_Update(string strYear, string strHacchuuNo,int nKubun,
                               Hashtable KaitouNoukiDataTbl, int nKaitouNo, SqlConnection sqlConn)
        {
            ArrayList aryNouki = (ArrayList)KaitouNoukiDataTbl["Nouki"];
            ArrayList arySuuryou = (ArrayList)KaitouNoukiDataTbl["Suuryou"];
            ArrayList aryKaitouNo = (ArrayList)KaitouNoukiDataTbl["KaitouNo"];
            int nRowNo = 1;
            SqlTransaction sqlTran = null;
            try
            {
                SqlDataAdapter daKaitouNoukiIns = new SqlDataAdapter("", sqlConn);
                daKaitouNoukiIns.SelectCommand.CommandText =
                    "SELECT * FROM T_NoukiKaitou";
                daKaitouNoukiIns.InsertCommand = (new SqlCommandBuilder(daKaitouNoukiIns).GetInsertCommand());

                sqlConn.Open();
                sqlTran = sqlConn.BeginTransaction();

                daKaitouNoukiIns.SelectCommand.Transaction = daKaitouNoukiIns.InsertCommand.Transaction = sqlTran;

                KoubaiDataSet.T_NoukiKaitouDataTable dtKaitouNoukiIns = new KoubaiDataSet.T_NoukiKaitouDataTable();

                daKaitouNoukiIns.Fill(dtKaitouNoukiIns);
                for (int i = 0; i < aryKaitouNo.Count; i++)
                {
                    KoubaiDataSet.T_NoukiKaitouRow drKaitouNoukiIns = dtKaitouNoukiIns.NewT_NoukiKaitouRow();

                    drKaitouNoukiIns.Year= strYear;
                    drKaitouNoukiIns.HacchuuNo = strHacchuuNo;
                    drKaitouNoukiIns.JigyoushoKubun = nKubun;
                    drKaitouNoukiIns.RowNo = nRowNo;
                    drKaitouNoukiIns.KaitouNo = nKaitouNo + 1;
                    drKaitouNoukiIns.Nouki = Convert.ToInt32(aryNouki[i]);
                    drKaitouNoukiIns.Suuryou = Convert.ToInt32(arySuuryou[i]);
                    drKaitouNoukiIns.Tourokubi = DateTime.Now;
                    drKaitouNoukiIns.ShouninFlg = false;
                    drKaitouNoukiIns.ShouninshaID = "";
                    
                    dtKaitouNoukiIns.AddT_NoukiKaitouRow(drKaitouNoukiIns);
                    nRowNo++;
                }
                daKaitouNoukiIns.Update(dtKaitouNoukiIns);

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

        public static KoubaiDataSet.T_NoukiKaitouDataTable getT_NoukiKaitouDataTable(string strYear, string strNo, int nKubun, int nNo, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT  T_NoukiKaitou.* "
            + "FROM  T_NoukiKaitou "
            + "WHERE (Year = @Year) AND (HacchuuNo = @HacchuuNo) AND (KaitouNo = @KaitouNo) AND (JigyoushoKubun = @JigyoushoKubun) ";

            da.SelectCommand.Parameters.AddWithValue("@Year", strYear);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", strNo);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nKubun);     
            da.SelectCommand.Parameters.AddWithValue("@KaitouNo", nNo);
            KoubaiDataSet.T_NoukiKaitouDataTable dt = new KoubaiDataSet.T_NoukiKaitouDataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
