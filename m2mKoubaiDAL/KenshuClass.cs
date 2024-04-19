using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace m2mKoubaiDAL
{
    public class KenshuClass
    {
        /// <summary>
        /// ��������
        /// </summary>
        public class KensakuParam
        {

            // ����No
            public string _HacchuNo = "";
            // �d����
            public string _SCode = "";
            // �[���ꏊ
            public string _NBasho = "";
            // �[���񓚏�
            //public int _NkJyoukyou = -1;
            // �[�i��
            //public int _NHJyoukyou = -1;
            // ���i�敪
            public string _Kubun = "";
            // ���i
            public string _BuhinCode = "";
            // �����S����
            public string _TantoushaCode = "";
            // �[��
            //public Core.Type.NengappiKikan _Nouki = null;
            // �[�i��
            //public Core.Type.NengappiKikan _NouhinBi = null;

            // �[�i�N��
            public string _NouhinYearMonth = "";
            // �J�n�N����   
            public string _FromDate = "";
            // �I���N����
            public string _ToDate = "";
            // �[�i��
            //public string _NouhinMonth = "";

            // ������
            //public Core.Type.NengappiKikan _Hacchuubi = null;
            // �񓚔[��
            //public Core.Type.NengappiKikan _KaitouNouki = null;

            // �����
            public Core.Type.NengappiKikan _UkeireBi = null;
            // �x������
            public int _Shimebi = -1;


            // ����ȖڃR�[�h 09/07/22�ǉ�
            public int _KanjyouKamokuCode = -1;

            // ��p�ȖڃR�[�h 09/07/22�ǉ�
            public int _HiyouKamokuCode = -1;

            // �⏕�Ȗ�No 09/07/22�ǉ�
            public int _HojyoKamokuNo = -1;
            // ���Ə��敪
            public int _JigyoushoKubun = 0;

        }


        /// <summary>
        /// Where�����쐬
        /// </summary>
        /// <param name="k"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private static string WhereText(KensakuParam k, SqlCommand cmd)
        {
            Core.Sql.WhereGenerator w = new Core.Sql.WhereGenerator();

            // �[�i�N
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
            // �[�i�N
            if (k._HacchuNo != "")
            {
                w.Add(string.Format("T_Chumon.HacchuuNo LIKE '{0}%'", k._HacchuNo));
            }

            // ����No
            if (k._HacchuNo != "")
            {
                w.Add(string.Format("T_Chumon.HacchuuNo LIKE '{0}%'", k._HacchuNo));
            }
            // �d����R�[�h
            if (k._SCode != "")
            {
                w.Add(string.Format("T_Chumon.ShiiresakiCode = '{0}'", k._SCode));
            }
            // �[���ꏊ
            if (k._NBasho != "")
            {
                w.Add(string.Format("T_Chumon.NounyuuBashoCode = '{0}'", k._NBasho));
            }
         

            // ���i�敪
            if (k._Kubun != "")
            {
                w.Add(string.Format("T_Chumon.BuhinKubun = '{0}'", k._Kubun));
            }
            // ���i
            if (k._BuhinCode != "")
            {
                w.Add(string.Format("T_Chumon.BuhinCode = '{0}'", k._BuhinCode));
            }
            // �����S����
            if (k._TantoushaCode != "")
            {
                w.Add(string.Format("T_Chumon.HacchushaID = '{0}'", k._TantoushaCode));
            }
           

            // �����
            if (k._UkeireBi != null)
            {
                
                w.Add(Core.Type.NengappiKikan.GenerateSQL(k._UkeireBi, false, "(convert(varchar,T_Nouhin.NouhinBi,112))"));
            }
            // �ȉ�3���ڒǉ� 09/07/22
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
            // ���Ə��敪
            if (k._JigyoushoKubun != 0)
            {
                w.Add(string.Format("T_Chumon.JigyoushoKubun = '{0}'", k._JigyoushoKubun));
            }
            return w.WhereText;
        }






        /// <summary>
        /// �����f�[�^���擾// �C�� 09/07/22
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
        /// �����f�[�^���擾// �C�� 09/07/22
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
            + ", T_Nouhin.Zeiritu "  // ���őΉ�
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
        /// �����f�[�^���擾
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
        /// �����f�[�^���擾
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
        /// �L�[�̌J��Ԃ��̏��������쐬
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
        /// �L�[�̌J��Ԃ��̏��������쐬
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
        /// �������̎d���斈�̍��v
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
        /// �������ɕR���Ă邷�ׂĂ̕��i�敪���擾
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
        ///���i�敪�ɂ���āA�������ɕR���Ă邷�ׂĂ̕��i�R�[�h�A���i�����擾
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
        /// �������ꗗ��Ddl�d������Z�b�g
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
        /// �������� �f�[�^���擾
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
        /// �������� �f�[�^���擾
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

