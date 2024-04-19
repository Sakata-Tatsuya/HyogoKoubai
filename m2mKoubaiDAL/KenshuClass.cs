using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace m2mKoubaiDAL
{
    public class KenshuClass
    {
        /// <summary>
        /// 検索条件
        /// </summary>
        public class KensakuParam
        {

            // 発注No
            public string _HacchuNo = "";
            // 仕入先
            public string _SCode = "";
            // 納入場所
            public string _NBasho = "";
            // 納期回答状況
            //public int _NkJyoukyou = -1;
            // 納品状況
            //public int _NHJyoukyou = -1;
            // 部品区分
            public string _Kubun = "";
            // 部品
            public string _BuhinCode = "";
            // 発注担当者
            public string _TantoushaCode = "";
            // 納期
            //public Core.Type.NengappiKikan _Nouki = null;
            // 納品日
            //public Core.Type.NengappiKikan _NouhinBi = null;

            // 納品年月
            public string _NouhinYearMonth = "";
            // 開始年月日   
            public string _FromDate = "";
            // 終了年月日
            public string _ToDate = "";
            // 納品月
            //public string _NouhinMonth = "";

            // 発注日
            //public Core.Type.NengappiKikan _Hacchuubi = null;
            // 回答納期
            //public Core.Type.NengappiKikan _KaitouNouki = null;

            // 受入日
            public Core.Type.NengappiKikan _UkeireBi = null;
            // 支払締日
            public int _Shimebi = -1;


            // 勘定科目コード 09/07/22追加
            public int _KanjyouKamokuCode = -1;

            // 費用科目コード 09/07/22追加
            public int _HiyouKamokuCode = -1;

            // 補助科目No 09/07/22追加
            public int _HojyoKamokuNo = -1;
            // 事業所区分
            public int _JigyoushoKubun = 0;

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

            // 納品年
            if (k._FromDate != "" && k._ToDate != "")
            {
                w.Add(string.Format
                       ("(convert(varchar,T_Nouhin.NouhinBi,112) >= {0} AND convert(varchar,T_Nouhin.NouhinBi,112) <= {1}) ",
                       k._FromDate, k._ToDate));
            }

            if (k._HacchuNo != "")
            {
                w.Add(string.Format("T_Chumon.HacchuuNo LIKE '{0}%'", k._HacchuNo));
            }
            // 納品年
            if (k._HacchuNo != "")
            {
                w.Add(string.Format("T_Chumon.HacchuuNo LIKE '{0}%'", k._HacchuNo));
            }

            // 発注No
            if (k._HacchuNo != "")
            {
                w.Add(string.Format("T_Chumon.HacchuuNo LIKE '{0}%'", k._HacchuNo));
            }
            // 仕入先コード
            if (k._SCode != "")
            {
                w.Add(string.Format("T_Chumon.ShiiresakiCode = '{0}'", k._SCode));
            }
            // 納入場所
            if (k._NBasho != "")
            {
                w.Add(string.Format("T_Chumon.NounyuuBashoCode = '{0}'", k._NBasho));
            }
         

            // 部品区分
            if (k._Kubun != "")
            {
                w.Add(string.Format("T_Chumon.BuhinKubun = '{0}'", k._Kubun));
            }
            // 部品
            if (k._BuhinCode != "")
            {
                w.Add(string.Format("T_Chumon.BuhinCode = '{0}'", k._BuhinCode));
            }
            // 発注担当者
            if (k._TantoushaCode != "")
            {
                w.Add(string.Format("T_Chumon.HacchushaID = '{0}'", k._TantoushaCode));
            }
           

            // 受入日
            if (k._UkeireBi != null)
            {
                
                w.Add(Core.Type.NengappiKikan.GenerateSQL(k._UkeireBi, false, "(convert(varchar,T_Nouhin.NouhinBi,112))"));
            }
            // 以下3項目追加 09/07/22
            if (k._KanjyouKamokuCode != -1)
            {
                w.Add(string.Format("M_Buhin.KanjyouKamokuCode = {0}", k._KanjyouKamokuCode));
            }

            if (k._HiyouKamokuCode != -1)
            {
                w.Add(string.Format("M_Buhin.HiyouKamokuCode = {0}", k._HiyouKamokuCode));
            }

            if (k._HojyoKamokuNo != -1)
            {
                w.Add(string.Format("M_Buhin.HojyoKamokuNo = {0}", k._HojyoKamokuNo));
            }
            // 事業所区分
            if (k._JigyoushoKubun != 0)
            {
                w.Add(string.Format("T_Chumon.JigyoushoKubun = '{0}'", k._JigyoushoKubun));
            }
            return w.WhereText;
        }






        /// <summary>
        /// 検収データを取得// 修正 09/07/22
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static KenshuDataSet.V_KenshuDataTable
            getV_KenshuDataTable(KensakuParam k, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
              "SELECT                  T_Nouhin.Year, T_Nouhin.HacchuuNo, T_Nouhin.NouhinNo, T_Nouhin.Suuryou AS NouhinSuuryou, "
            + "T_Chumon.ShiiresakiCode, T_Chumon.BuhinKubun, T_Chumon.BuhinCode, T_Chumon.Tanka, "
            + "T_Chumon.Suuryou AS ChumonSuuryou, M_NounyuuBasho.BashoMei, M_Buhin.BuhinMei, M_Buhin.Tani, "
            + "T_Nouhin.NouhinBi, T_Chumon.HacchuuBi, M_Shiiresaki.ShiiresakiMei, T_Chumon.Kingaku, "
            + "M_Buhin.KanjyouKamokuCode, M_Buhin.HiyouKamokuCode, M_Buhin.HojyoKamokuNo, T_Nouhin.JigyoushoKubun "
            + "FROM                     T_Nouhin INNER JOIN "
            + "T_Chumon ON T_Nouhin.Year = T_Chumon.Year AND T_Nouhin.HacchuuNo = T_Chumon.HacchuuNo AND "
            + "T_Nouhin.JigyoushoKubun = T_Chumon.JigyoushoKubun INNER JOIN "
            + "M_NounyuuBasho ON T_Chumon.NounyuuBashoCode = M_NounyuuBasho.BashoCode INNER JOIN "
            + "M_Buhin ON T_Chumon.BuhinCode = M_Buhin.BuhinCode INNER JOIN "
            + "M_Shiiresaki ON T_Chumon.ShiiresakiCode = M_Shiiresaki.ShiiresakiCode "
            + "WHERE                   (T_Chumon.CancelBi IS NULL) ";

            // WHERE
            string strW = WhereText(k, da.SelectCommand);
            if (strW != "")
            {
                da.SelectCommand.CommandText += "AND " + strW;
            }

            da.SelectCommand.CommandText += " ORDER BY        T_Chumon.Year  DESC,  T_Chumon.HacchuuNo DESC ";
            KenshuDataSet.V_KenshuDataTable dt = new KenshuDataSet.V_KenshuDataTable();
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        /// 検収データを取得// 修正 09/07/22
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static KenshuDataSet.V_KenshuDataTable
            getV_Kenshu2DataTable(KensakuParam k, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
              "SELECT                  T_Nouhin.Year, T_Nouhin.HacchuuNo, T_Nouhin.NouhinNo, T_Nouhin.Suuryou AS NouhinSuuryou, "
            + "T_Chumon.ShiiresakiCode, T_Chumon.BuhinKubun, T_Chumon.BuhinCode, T_Chumon.Tanka, "
            + "T_Chumon.Suuryou AS ChumonSuuryou, M_NounyuuBasho.BashoMei, M_Buhin.BuhinMei, M_Buhin.Tani, "
            + "T_Nouhin.NouhinBi, T_Chumon.HacchuuBi, M_Shiiresaki.ShiiresakiMei, T_Chumon.Kingaku, "
            + "M_Buhin.KanjyouKamokuCode, M_Buhin.HiyouKamokuCode, M_Buhin.HojyoKamokuNo, T_Nouhin.JigyoushoKubun "
            + ", T_Nouhin.Zeiritu "  // 増税対応
            + "FROM                     T_Nouhin INNER JOIN "
            + "T_Chumon ON T_Nouhin.Year = T_Chumon.Year AND T_Nouhin.HacchuuNo = T_Chumon.HacchuuNo AND "
            + "T_Nouhin.JigyoushoKubun = T_Chumon.JigyoushoKubun INNER JOIN "
            + "M_NounyuuBasho ON T_Chumon.NounyuuBashoCode = M_NounyuuBasho.BashoCode INNER JOIN "
            + "M_Buhin ON T_Chumon.BuhinCode = M_Buhin.BuhinCode INNER JOIN "
            + "M_Shiiresaki ON T_Chumon.ShiiresakiCode = M_Shiiresaki.ShiiresakiCode "
            + "WHERE                   (T_Chumon.CancelBi IS NULL) ";

            // WHERE
            string strW = WhereText(k, da.SelectCommand);
            if (strW != "")
            {
                da.SelectCommand.CommandText += "AND " + strW;
            }

            da.SelectCommand.CommandText += " ORDER BY         T_Nouhin.JigyoushoKubun ";
            KenshuDataSet.V_KenshuDataTable dt = new KenshuDataSet.V_KenshuDataTable();
            da.Fill(dt);
            return dt;
        }
       
        /// <summary>
        /// 検収データを取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static KenshuDataSet.V_KenshuDataTable
            getV_KenshuDataTable(string strKeyAry, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
             "SELECT                  T_Nouhin.Year, T_Nouhin.HacchuuNo, T_Nouhin.NouhinNo, T_Nouhin.Suuryou AS NouhinSuuryou, "
            + "T_Chumon.ShiiresakiCode, T_Chumon.BuhinKubun, T_Chumon.BuhinCode, T_Chumon.Tanka, "
            + "T_Chumon.Suuryou AS ChumonSuuryou, M_NounyuuBasho.BashoMei, M_Buhin.BuhinMei, M_Buhin.Tani, "
            + "T_Nouhin.NouhinBi, T_Chumon.HacchuuBi, M_Shiiresaki.ShiiresakiMei, T_Chumon.Kingaku, "
            + "M_Buhin.KanjyouKamokuCode, M_Buhin.HiyouKamokuCode, M_Buhin.HojyoKamokuNo, T_Nouhin.JigyoushoKubun "
            + "FROM                     T_Nouhin INNER JOIN "
            + "T_Chumon ON T_Nouhin.Year = T_Chumon.Year AND T_Nouhin.HacchuuNo = T_Chumon.HacchuuNo AND "
            + "T_Nouhin.JigyoushoKubun = T_Chumon.JigyoushoKubun INNER JOIN "
            + "M_NounyuuBasho ON T_Chumon.NounyuuBashoCode = M_NounyuuBasho.BashoCode INNER JOIN "
            + "M_Buhin ON T_Chumon.BuhinCode = M_Buhin.BuhinCode INNER JOIN "
            + "M_Shiiresaki ON T_Chumon.ShiiresakiCode = M_Shiiresaki.ShiiresakiCode "
            + "WHERE                   (T_Chumon.CancelBi IS NULL) ";

            string strWhere = WhereKey(strKeyAry);
            if (strWhere != "")
                da.SelectCommand.CommandText += "AND " + strWhere;

            da.SelectCommand.CommandText += " ORDER BY T_Chumon.ShiiresakiCode";
         
            KenshuDataSet.V_KenshuDataTable dt = new KenshuDataSet.V_KenshuDataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 検収データを取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static KenshuDataSet.V_KenshuDataTable
            getV_Kenshu_MeisaiDataTable(string strKeyAry, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
              "SELECT          T_Nouhin.Year, T_Nouhin.HacchuuNo, T_Nouhin.NouhinNo, "
            + "T_Nouhin.Suuryou AS NouhinSuuryou, T_Chumon.ShiiresakiCode, "
            + "T_Chumon.BuhinKubun, T_Chumon.BuhinCode, T_Chumon.Tanka, "
            + "T_Chumon.Suuryou AS ChumonSuuryou, M_NounyuuBasho.BashoMei, "
            + "M_Buhin.BuhinMei, M_Buhin.Tani, T_Nouhin.NouhinBi, "
            + "T_Chumon.HacchuuBi, M_Shiiresaki.ShiiresakiMei, "
            + "T_Chumon.Kingaku, M_Buhin.KanjyouKamokuCode, "
            + "M_Buhin.HiyouKamokuCode, M_Buhin.HojyoKamokuNo, "
            + "T_Nouhin.JigyoushoKubun "
            + "FROM            M_NounyuuBasho INNER JOIN "
            + "T_Chumon ON "
            + "M_NounyuuBasho.BashoCode = T_Chumon.NounyuuBashoCode INNER JOIN "
            + "M_Buhin ON "
            + "T_Chumon.BuhinCode = M_Buhin.BuhinCode INNER JOIN "
            + "M_Shiiresaki ON "
            + "T_Chumon.ShiiresakiCode = M_Shiiresaki.ShiiresakiCode RIGHT OUTER JOIN "
            + "T_Nouhin ON T_Chumon.Year = T_Nouhin.Year AND "
            + "T_Chumon.HacchuuNo = T_Nouhin.HacchuuNo AND "
            + "T_Chumon.JigyoushoKubun = T_Nouhin.JigyoushoKubun "
            + "WHERE                   (T_Chumon.CancelBi IS NULL) ";
            string strWhere = WhereKey2(strKeyAry);
            if (strWhere != "")
                da.SelectCommand.CommandText += "AND " + strWhere;

            da.SelectCommand.CommandText += " ORDER BY T_Nouhin.JigyoushoKubun ";

            KenshuDataSet.V_KenshuDataTable dt = new KenshuDataSet.V_KenshuDataTable();
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        /// キーの繰り返しの条件分を作成
        /// </summary>
        /// <param name="strAry"></param>
        /// <returns></returns>
        private static string WhereKey2(string strAry)
        {
            string[] strKeyAry = strAry.Split('_');
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strKeyAry.Length; i++)
            {
                string[] strKey = strKeyAry[i].Split(',');
                string strYear = strKey[0];
                string strHacchuNo = strKey[1];
                int nJigyoushoKubun = int.Parse(strKey[2]);
                if (sb.Length > 0) sb.Append(" OR ");
                sb.Append("(T_Nouhin.Year = '" + strYear + "' AND T_Nouhin.HacchuuNo = '" + strHacchuNo + "' AND T_Nouhin.JigyoushoKubun = '" + nJigyoushoKubun + "')");
                      
            }
            return "(" + sb.ToString() + ")";
        }
        /// <summary>
        /// キーの繰り返しの条件分を作成
        /// </summary>
        /// <param name="strAry"></param>
        /// <returns></returns>
        private static string WhereKey(string strAry)
        {
            string[] strKeyAry = strAry.Split('_');
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strKeyAry.Length; i++)
            {
                string[] strKey = strKeyAry[i].Split(',');
                string strYear = strKey[0];
                string strHacchuNo = strKey[1];
                int nJigyoushoKubun = int.Parse(strKey[2]);
                int nNouhinNo = int.Parse(strKey[3]);
                if (sb.Length > 0) sb.Append(" OR ");
                sb.Append("(T_Nouhin.Year = '" + strYear + "' AND T_Nouhin.HacchuuNo = '" + strHacchuNo + "' AND T_Nouhin.JigyoushoKubun = '" + nJigyoushoKubun + "' AND T_Nouhin.NouhinNo = '" + nNouhinNo + "')");
               
            }
            return "(" + sb.ToString() + ")";
        }
        /// <summary>
        /// 検収情報の仕入先毎の合計
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static KenshuDataSet.V_Kenshu_ShukeiDataTable
            getV_Kenshu_ShukeiDataTable(KensakuParam k, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT                  TOP (100) PERCENT T_Nouhin.NouhinNo, T_Nouhin.Suuryou, T_Chumon.ShiiresakiCode, T_Nouhin.NouhinBi, "
            + "M_Shiiresaki.ShiiresakiMei, T_Chumon.Tanka "
            + ",T_Nouhin.Zeiritu "
            + "FROM     dbo.T_Nouhin INNER JOIN "
            + "dbo.T_Chumon ON dbo.T_Nouhin.Year = dbo.T_Chumon.Year "
            + "AND dbo.T_Nouhin.HacchuuNo = dbo.T_Chumon.HacchuuNo INNER JOIN "
            + "dbo.M_Shiiresaki ON dbo.T_Chumon.ShiiresakiCode = dbo.M_Shiiresaki.ShiiresakiCode INNER JOIN "
            + "dbo.M_Buhin ON dbo.T_Chumon.BuhinCode = dbo.M_Buhin.BuhinCode "

             + "WHERE T_Chumon.CancelBi IS NULL ";

            // WHERE
            string strW = WhereText(k, da.SelectCommand);
            if (strW != "")
            {
                da.SelectCommand.CommandText += "AND " + strW;
            }
            da.SelectCommand.CommandText += "ORDER BY           T_Chumon.ShiiresakiCode ";
            KenshuDataSet.V_Kenshu_ShukeiDataTable dt = new KenshuDataSet.V_Kenshu_ShukeiDataTable();
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        /// 検収情報に紐ついてるすべての部品区分を取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static KenshuDataSet.V_Kenshu_BuhinKubunDataTable
            V_Kenshu_BuhinKubunDataTable(SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT DISTINCT TOP (100) PERCENT T_Chumon.BuhinKubun "
            + "FROM                     T_Chumon INNER JOIN "
            + "M_Buhin ON T_Chumon.BuhinCode = M_Buhin.BuhinCode INNER JOIN "
            + "T_Nouhin ON T_Chumon.Year = T_Nouhin.Year AND T_Chumon.HacchuuNo = T_Nouhin.HacchuuNo AND "
            + "T_Chumon.JigyoushoKubun = dbo.T_Nouhin.JigyoushoKubun "
            + "WHERE T_Chumon.CancelBi IS NULL "
            + "ORDER BY           T_Chumon.BuhinKubun ";
            KenshuDataSet.V_Kenshu_BuhinKubunDataTable dt = new KenshuDataSet.V_Kenshu_BuhinKubunDataTable();
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        ///部品区分によって、検収情報に紐ついてるすべての部品コード、部品名を取得
        /// </summary>
        /// <param name="strKubun"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static KenshuDataSet.V_Kenshu_BuhinDataTable
            getV_Kenshu_BuhinDataTable(string strKubun, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT DISTINCT TOP (100) PERCENT T_Chumon.BuhinCode, M_Buhin.BuhinMei, T_Chumon.BuhinKubun "
            + "FROM                     T_Chumon INNER JOIN "
            + "M_Buhin ON T_Chumon.BuhinCode = M_Buhin.BuhinCode AND "
            + "T_Chumon.BuhinKubun = M_Buhin.BuhinKubun INNER JOIN "
            + "T_Nouhin ON T_Chumon.Year = T_Nouhin.Year AND T_Chumon.HacchuuNo = T_Nouhin.HacchuuNo AND "
            + "T_Chumon.JigyoushoKubun = dbo.T_Nouhin.JigyoushoKubun "
            + "WHERE T_Chumon.BuhinKubun = @Kubun AND T_Chumon.CancelBi IS NULL ";
            da.SelectCommand.Parameters.AddWithValue("@Kubun", strKubun);

            da.SelectCommand.CommandText += "ORDER BY           T_Chumon.BuhinKubun, T_Chumon.BuhinCode ";
            KenshuDataSet.V_Kenshu_BuhinDataTable dt = new KenshuDataSet.V_Kenshu_BuhinDataTable();
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        /// 検収情報一覧でDdl仕入先をセット
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static KenshuDataSet.V_Kenshu_ShiireDataTable
            getV_Kenshu_ShiireDataTable(SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT DISTINCT TOP (100) PERCENT T_Chumon.ShiiresakiCode, M_Shiiresaki.ShiiresakiMei "
            + "FROM                     M_Shiiresaki INNER JOIN "
            + "T_Chumon ON M_Shiiresaki.ShiiresakiCode = T_Chumon.ShiiresakiCode INNER JOIN "
            + "T_Nouhin ON T_Chumon.Year = T_Nouhin.Year AND T_Chumon.HacchuuNo = T_Nouhin.HacchuuNo AND "
            + "T_Chumon.JigyoushoKubun = dbo.T_Nouhin.JigyoushoKubun "
            + "WHERE T_Chumon.CancelBi IS NULL "
            + "ORDER BY           T_Chumon.ShiiresakiCode ";
            KenshuDataSet.V_Kenshu_ShiireDataTable dt = new KenshuDataSet.V_Kenshu_ShiireDataTable();
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        /// 検収明細 データを取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>        
        public static KenshuDataSet.V_KenshuBindDataTable
            getV_KenshuBindDataTable(KensakuParam k, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
              "SELECT                  T_Nouhin.Year, T_Nouhin.HacchuuNo, T_Nouhin.NouhinNo, T_Nouhin.Suuryou, T_Chumon.ShiiresakiCode, "
            + "T_Chumon.BuhinKubun, T_Chumon.BuhinCode, T_Chumon.Tanka, T_Chumon.Suuryou AS Expr1, "
            + "M_NounyuuBasho.BashoMei, M_Buhin.BuhinMei, M_Buhin.Tani, T_Nouhin.NouhinBi, T_Chumon.HacchuuBi, "
            + "M_Shiiresaki.ShiiresakiMei, T_Nouhin.JigyoushoKubun "
            + "FROM                     T_Nouhin INNER JOIN "
            + "T_Chumon ON T_Nouhin.Year = T_Chumon.Year AND T_Nouhin.HacchuuNo = T_Chumon.HacchuuNo AND "
            + "T_Nouhin.JigyoushoKubun = T_Chumon.JigyoushoKubun INNER JOIN "
            + "M_NounyuuBasho ON T_Chumon.NounyuuBashoCode = M_NounyuuBasho.BashoCode INNER JOIN "
            + "M_Buhin ON T_Chumon.BuhinCode = M_Buhin.BuhinCode INNER JOIN "
            + "M_Shiiresaki ON T_Chumon.ShiiresakiCode = M_Shiiresaki.ShiiresakiCode "
            + "WHERE                   (T_Chumon.CancelBi IS NULL) ";

            // WHERE
            string strW = WhereText(k, da.SelectCommand);
            if (strW != "")
            {
                da.SelectCommand.CommandText += "AND " + strW;
            }
            KenshuDataSet.V_KenshuBindDataTable dt = new KenshuDataSet.V_KenshuBindDataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 検収明細 データを取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>        
        public static KenshuDataSet.V_KenshuBindDataTable
            getV_KenshuBin2dDataTable(KensakuParam k, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
              "SELECT                  T_Nouhin.Year, T_Nouhin.HacchuuNo, T_Nouhin.NouhinNo, T_Nouhin.Suuryou, T_Chumon.ShiiresakiCode, "
            + "T_Chumon.BuhinKubun, T_Chumon.BuhinCode, T_Chumon.Tanka, T_Chumon.Suuryou AS Expr1, "
            + "M_NounyuuBasho.BashoMei, M_Buhin.BuhinMei, M_Buhin.Tani, T_Nouhin.NouhinBi, T_Chumon.HacchuuBi, "
            + "M_Shiiresaki.ShiiresakiMei, T_Nouhin.JigyoushoKubun "
            + "FROM                     T_Nouhin INNER JOIN "
            + "T_Chumon ON T_Nouhin.Year = T_Chumon.Year AND T_Nouhin.HacchuuNo = T_Chumon.HacchuuNo AND "
            + "T_Nouhin.JigyoushoKubun = T_Chumon.JigyoushoKubun INNER JOIN "
            + "M_NounyuuBasho ON T_Chumon.NounyuuBashoCode = M_NounyuuBasho.BashoCode INNER JOIN "
            + "M_Buhin ON T_Chumon.BuhinCode = M_Buhin.BuhinCode INNER JOIN "
            + "M_Shiiresaki ON T_Chumon.ShiiresakiCode = M_Shiiresaki.ShiiresakiCode "
            + "WHERE                   (T_Chumon.CancelBi IS NULL) ";

            // WHERE
            string strW = WhereText(k, da.SelectCommand);
            if (strW != "")
            {
                da.SelectCommand.CommandText += "AND " + strW;
            }
            da.SelectCommand.CommandText += " ORDER BY T_Nouhin.JigyoushoKubun  ";
            KenshuDataSet.V_KenshuBindDataTable dt = new KenshuDataSet.V_KenshuBindDataTable();
            da.Fill(dt);
            return dt;
        }

        public static System.Data.DataTable GetNouhinYearDataTable(SqlConnection sqlCon)
        {
            string sql =
                @"
                SELECT          
                SUBSTRING(convert(varchar,T_Nouhin.NouhinBi,112), 0, 5) AS Year
                FROM                     T_Nouhin INNER JOIN 
                T_Chumon ON T_Nouhin.Year = T_Chumon.Year AND T_Nouhin.HacchuuNo = T_Chumon.HacchuuNo AND 
                T_Nouhin.JigyoushoKubun = T_Chumon.JigyoushoKubun INNER JOIN 
                M_NounyuuBasho ON T_Chumon.NounyuuBashoCode = M_NounyuuBasho.BashoCode INNER JOIN 
                M_Buhin ON T_Chumon.BuhinCode = M_Buhin.BuhinCode INNER JOIN 
                M_Shiiresaki ON T_Chumon.ShiiresakiCode = M_Shiiresaki.ShiiresakiCode 
                WHERE                   (T_Chumon.CancelBi IS NULL) 
                GROUP BY SUBSTRING(convert(varchar,T_Nouhin.NouhinBi,112), 0, 5)
                ORDER BY SUBSTRING(convert(varchar,T_Nouhin.NouhinBi,112), 0, 5)
                ";

            SqlDataAdapter da = new SqlDataAdapter(sql, sqlCon);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}

