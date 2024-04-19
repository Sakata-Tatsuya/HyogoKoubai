using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace m2mKoubaiDAL
{
    public class HacchuClass
    {
        private static string WhereText(string key, SqlCommand cmd)
        {
            Core.Sql.WhereGenerator w = new Core.Sql.WhereGenerator();

            //Key•ª‰ð 09,0000001_09,0000002_09,0000003
            string[] strKeyAry = key.Split('_');

            if (strKeyAry != null)
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < strKeyAry.Length; i++)
                {
                    string[] keyAry = strKeyAry[i].Split(',');
                    string year = keyAry[0];
                    string hacchuuNo = keyAry[1];
                    int nKubun = int.Parse(keyAry[2]);

                    if (sb.Length > 0) sb.Append(" OR ");
                    sb.Append("T_Chumon.Year = @Year" + i);
                    sb.Append(" AND ");
                    sb.Append("T_Chumon.HacchuuNo = @HacchuuNo" + i);
                    sb.Append(" AND ");
                    sb.Append("T_Chumon.JigyoushoKubun = @JigyoushoKubun" + i);
                    cmd.Parameters.AddWithValue("@Year" + i, year);
                    cmd.Parameters.AddWithValue("@HacchuuNo" + i, hacchuuNo);
                    cmd.Parameters.AddWithValue("@JigyoushoKubun" + i, nKubun);
                }
                if (sb.Length > 0)
                    w.Add(sb.ToString());  
            }
            return w.WhereText;
        }

        public static HacchuDataSet_M.V_HacchuDataTable
            getV_HacchuDataTable(string key, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "SELECT                  TOP (100) PERCENT T_Chumon.Year, T_Chumon.HacchuuNo, T_Chumon.ShiiresakiCode, T_Chumon.BuhinKubun, "
            + "T_Chumon.BuhinCode, T_Chumon.Tanka, T_Chumon.Suuryou, T_Chumon.Nouki, T_Chumon.NounyuuBashoCode, "
            + "T_Chumon.Bikou, T_Chumon.HacchuuBi, T_Chumon.HacchushaID, M_Buhin.Tani, M_Buhin.BuhinMei, "
            + "M_Shiiresaki.ShiiresakiMei, M_Login.Name, M_NounyuuBasho.BashoMei, T_Chumon.Kingaku, "
            + "M_Shiiresaki.YubinBangou, M_Shiiresaki.Tel, M_Shiiresaki.Fax, M_Shiiresaki.Address, T_Chumon.JigyoushoKubun, "
            + "T_KaishaInfo.KaishaMei, T_KaishaInfo.EigyouSho, T_KaishaInfo.Yuubin AS YuubinY, T_KaishaInfo.Address AS AddressY, "
            + "T_KaishaInfo.Tel AS TelY, T_KaishaInfo.Fax AS FaxY "
            + "FROM                     T_Chumon INNER JOIN "
            + "M_Buhin ON T_Chumon.BuhinKubun = M_Buhin.BuhinKubun AND "
            + "T_Chumon.BuhinCode = M_Buhin.BuhinCode INNER JOIN "
            + "M_Shiiresaki ON T_Chumon.ShiiresakiCode = M_Shiiresaki.ShiiresakiCode INNER JOIN "
            + "M_NounyuuBasho ON T_Chumon.NounyuuBashoCode = M_NounyuuBasho.BashoCode INNER JOIN "
            + "T_KaishaInfo ON T_Chumon.JigyoushoKubun = T_KaishaInfo.KaishaID LEFT OUTER JOIN "
            + "M_Login ON T_Chumon.HacchushaID = M_Login.LoginID ";

            // WHERE            
            string strW = WhereText(key, da.SelectCommand);
            if (strW != "")
            {
                da.SelectCommand.CommandText += " WHERE " + strW;
            }           
            // GROUP BY
            // ORDER BY
            da.SelectCommand.CommandText += "  ORDER BY dbo.T_Chumon.ShiiresakiCode, T_Chumon.JigyoushoKubun, dbo.T_Chumon.HacchuuNo ";

            HacchuDataSet_M.V_HacchuDataTable dt = new HacchuDataSet_M.V_HacchuDataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
