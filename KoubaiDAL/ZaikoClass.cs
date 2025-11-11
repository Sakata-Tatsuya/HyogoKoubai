using Core.Type;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace KoubaiDAL
{
    public class ZaikoClass
    {
        public static ZaikoDataSet.T_ZaikoRow GetT_ZaikoRow(string strNengetu, string strLocationCode, string strItemCode, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_Zaiko"
            + " WHERE Nengetu = @Nengetu AND LocationCode = @LocationCode AND ItemCode = @ItemCode";

            da.SelectCommand.Parameters.AddWithValue("@Nengetu", strNengetu);
            da.SelectCommand.Parameters.AddWithValue("@LocationCode", strLocationCode);
            da.SelectCommand.Parameters.AddWithValue("@ItemCode", strItemCode);
            ZaikoDataSet.T_ZaikoDataTable dt = new ZaikoDataSet.T_ZaikoDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (ZaikoDataSet.T_ZaikoRow)dt.Rows[0];
            else
                return null;
        }
        public static decimal GetZensyaZaikoSu(string strNengetu, string strItemCode, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT SUM(ZaikoSu) as ZaikoSu FROM T_Zaiko  WHERE Nengetu = @Nengetu AND ItemCode = @ItemCode";
            da.SelectCommand.Parameters.AddWithValue("@Nengetu", strNengetu);
            da.SelectCommand.Parameters.AddWithValue("@ItemCode", strItemCode);
            System.Data.DataSet ds = new System.Data.DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows[0].IsNull("ZaikoSu"))
            {
                return 0;
            }
            else
            {
                return (decimal.Parse(dt.Rows[0]["ZaikoSu"].ToString()));
            }

        }


        public static LibError UpdateT_Zaiko(ZaikoDataSet.T_ZaikoDataTable dtD, SqlConnection sqlConn)
        {
            SqlCommand cmd_C = new SqlCommand("", sqlConn);
            SqlCommand cmd_U = new SqlCommand("", sqlConn);
            SqlCommand cmd_I = new SqlCommand("", sqlConn);
            SqlTransaction sqlTran = null;

            try
            {
                sqlConn.Open();
                sqlTran = sqlConn.BeginTransaction();
                cmd_C.Transaction =
                cmd_U.Transaction =
                cmd_I.Transaction = sqlTran;

                for (int i = 0; i < dtD.Count; i++)
                {
                    ZaikoDataSet.T_ZaikoRow drD = dtD[i];
                    cmd_C.CommandText = "SELECT COUNT(*) FROM T_Zaiko WHERE Nengetu = "+drD.Nengetu.ToString() + " AND LocationCode = '" + drD.LocationCode + "' AND ItemCode = '" + drD.ItemCode + "' ";
                    int iCount = (int)cmd_C.ExecuteScalar();
                    if (iCount < 1)
                    {
                        cmd_I.CommandText = "INSERT INTO T_Zaiko(Nengetu,LocationCode,ItemCode,HinmokuKubun,ZaikoSu,Tanka,TourokuBi,KoushinBi,ShikibetsuID) "
                            + "VALUES ("
                            + drD.Nengetu + ",'"
                            + drD.LocationCode + "','"
                            + drD.ItemCode + "',"
                            + drD.HinmokuKubun + ","
                            + drD.ZaikoSu + ","
                            + drD.Tanka + ",'"
                            + drD.TourokuBi.ToString("yyyy-MM-dd") + "','"
                            + drD.KoushinBi.ToString("yyyy-MM-dd") + "','"
                            + drD.ShikibetsuID + "')";
                        cmd_I.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd_U.CommandText = "UPDATE T_Zaiko SET ZaikoSu = " + drD.ZaikoSu + ",KoushinBi = " + drD.KoushinBi.ToString("yyyy-MM-dd") +
                            " WHERE Nengetu = "+drD.Nengetu.ToString() + " AND LocationCode = " + drD.LocationCode + " AND ItemCode = " + drD.ItemCode;
                        cmd_U.ExecuteNonQuery();
                    }
                }
                sqlTran.Commit();
                return null;
            }
            catch (Exception e)
            {
                if (null != sqlTran)
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
