using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace m2mKoubaiDAL
{
    public class DownloadClass
    {
        public enum EnumDataKubun
        {
            Kenshu = 1,
            NounyuZan = 2,
        }

        public static string GetTextData(EnumDataKubun dataKubun, DataTable dtSrc, bool bTab, SqlConnection sqlConn)
        {
            if (dtSrc == null)
            {
                throw new Exception("データソース(DataTable)を指定してください。");
            }

            string _strColumnDelimiter = bTab ? "\t" : ",";                  // 列区切り文字
            string _strColumnDelimiterReplacement = bTab ? "" : "，";        // 列区切り文字が文字列中に現れた場合の置換文字

            System.Text.StringBuilder data = new System.Text.StringBuilder();

            // ■ヘッダ
            DownloadDataSet.T_DownloadHeaderDataTable dtHeader = GetT_DownloadHeaderDataTable((byte)dataKubun, sqlConn);

            for (int i = 0; i < dtHeader.Rows.Count; i++)
            {
                if (0 < i) data.Append(_strColumnDelimiter);
                string titleName = dtHeader[i].TitleMei;
                if (0 < titleName.IndexOf(_strColumnDelimiter))
                {
                    throw new Exception(string.Format("{0}でエラー。見出し文字に{1}は使用できません。", titleName, _strColumnDelimiter));
                }
                data.Append(titleName);
            }

            data.Append(System.Environment.NewLine);

            // ■データ
            DataView dv = new DataView(dtSrc);

            string str = "";

            for (int i = 0; i < dv.Count; i++)
            {
                DataRow dr = dv[i].Row;

                for (int j = 0; j < dtHeader.Rows.Count; j++)
                {
                    if (0 < j)
                    {
                        data.Append(_strColumnDelimiter);
                    }

                    string colName = dtHeader[j].ColumnName;
                    str = Convert.ToString(dr[colName]);
                    // 文字列の場合は改行を取り除く
                    if (dtSrc.Columns[colName].DataType == typeof(string))
                    {
                        str = str.Replace(System.Environment.NewLine, "");
                        str = str.Replace(_strColumnDelimiter, _strColumnDelimiterReplacement);
                    }

                    data.Append(str);
                }
                data.Append(System.Environment.NewLine);
            }
            return data.ToString();
        }

        public static DownloadDataSet.T_DownloadHeaderDataTable
            GetT_DownloadHeaderDataTable(byte dataKubun, SqlConnection sqlConn)
        {
            string sql = "SELECT * FROM T_DownloadHeader WHERE DataKubun = @DataKubun ORDER BY FieldIndex";
            SqlDataAdapter da = new SqlDataAdapter(sql, sqlConn);
            da.SelectCommand.Parameters.AddWithValue("@DataKubun", dataKubun);

            DownloadDataSet.T_DownloadHeaderDataTable dt = new DownloadDataSet.T_DownloadHeaderDataTable();
            da.Fill(dt);

            return dt;
        }
    }
}
