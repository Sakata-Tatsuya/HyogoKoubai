using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace m2mKoubaiDAL
{
    public class ChumonClass
    {
        /// <summary>
        /// 検索条件
        /// </summary>
        public class KensakuParam
        {
            // ユーザー区分
            public byte _userKubun = 0;
            // 発注No
            public string _HacchuNo = "";
            // 仕入先
            public string _SCode = "";
            // 納入場所
            public string _NBasho = "";
            // 納期回答状況
            public int _NkJyoukyou = 0;
            //　納期変更
            public int _NhJyoukyou = 0;
            // 納品状況
            public int _NHJyoukyou = 0;
            // 部品区分
            public string _Kubun = "";
            // 部品
            public string _BuhinCode = "";
            // 発注担当者
            public string _TantoushaCode = "";
            // 納期
            public Core.Type.NengappiKikan _Nouki = null;
            // 納品日
            public Core.Type.NengappiKikan _NouhinBi = null;
            // 発注日
            public Core.Type.NengappiKikan _Hacchuubi = null;
            // 回答納期
            public Core.Type.NengappiKikan _KaitouNouki = null;
            // 受入日
            public Core.Type.NengappiKikan _UkeireBi = null;
            // キャンセル
            public int _Cancelbi = 0;
            // メッセージ
            public int _Msg = -1;
            // 事業所区分
            public int _JigyoushoKubun = 0;
        }

        public class ChumonMeisai
        {
            public bool ChkI { get; set; }
            public string strYear { get; set; }
            public string strHacchuuNo { get; set; }
            public string strShiiresakiCode { get; set; }
            public string strShiiresakiItem { get; set; }
            public string strBuhinKubun { get; set; }
            public string strBuhinCode { get; set; }
            public string strBuhinItem { get; set; }
            public string strLot { get; set; }
            public bool ChkKaritankaFlg { get; set; }
            public string strTanka { get; set; }
            public string strSuryo { get; set; }
            public string strKingaku { get; set; }
            public bool KeigenZeirituFlg { get; set; }
            public string strZeiritu { get; set; }
            public string strTani { get; set; }
            public string strLT { get; set; }
            public string strNouki { get; set; }
            public string strNounyuuBashoCode { get; set; }
            public string strBikou { get; set; }
        }

        // 発注の主キー
        public class ChumonKey
        {
            private string _Year;// 年
            private string _HacchuuNo;   // 発注No
            private int _JigyoushoKubun;  // 仕入先コード            

            public ChumonKey(string strYear, string strHacchuuNo, int nJigyoushoKubun)
            {
                this._Year = strYear;
                this._HacchuuNo = strHacchuuNo;
                this._JigyoushoKubun = nJigyoushoKubun;
            }
            public ChumonKey(string strKey)
            {
                string[] strKeyAry = strKey.Split(',');
                this._Year = strKeyAry[0];
                this._HacchuuNo = strKeyAry[1];
                this._JigyoushoKubun = int.Parse(strKeyAry[2]);


            }
            /// <summary>
            /// [発注No_データ作成年月日_データ作成時間_種別]の形で結合させた文字列を返す
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return this._Year + "," + this._HacchuuNo + "," + this._JigyoushoKubun.ToString();
            }
            public string Year
            {
                get { return this._Year; }
            }
            public string HacchuuNo
            {
                get { return this._HacchuuNo; }
            }
            public int JigyoushoKubun
            {
                get { return this._JigyoushoKubun; }
            }

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
            // 発注No
            if (k._HacchuNo != "")
            {
                w.Add(string.Format("T_Chumon.HacchuuNo LIKE '{0}%'", k._HacchuNo));
            }
            // 事業所区分
            if (k._JigyoushoKubun != 0)
            {
                w.Add(string.Format("T_Chumon.JigyoushoKubun = '{0}'", k._JigyoushoKubun));
            }
            // 仕入先コード
            if (k._SCode != "")
            {
                w.Add(string.Format("(T_Chumon.ShiiresakiCode = '{0}')", k._SCode));
            }
            // 納入場所
            if (k._NBasho != "")
            {
                w.Add(string.Format("T_Chumon.NounyuuBashoCode = '{0}'", k._NBasho));
            }
            // 納期回答状況
            if (k._NkJyoukyou > 0)
            {
                if (k._NkJyoukyou == 1)
                {
                    w.Add("((SELECT TOP 1 KaitouNo "
                            + "FROM T_NoukiKaitou "
                            + "WHERE (T_Chumon.Year = Year) AND (T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun) "
                            + "ORDER BY KaitouNo DESC) IS NOT NULL)");
                }
                else if (k._NkJyoukyou == 2)
                {
                    w.Add("((SELECT TOP 1 KaitouNo "
                         + "FROM           T_NoukiKaitou "
                         + "WHERE (T_Chumon.Year = Year) AND (T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun) "
                         + "ORDER BY KaitouNo DESC) IS NULL)");
                }
                else
                {/*
                    w.Add("(((SELECT TOP 1 KaitouNo "
                          + "FROM           T_NoukiKaitou "
                          + "WHERE (T_Chumon.Year = Year) AND (T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun) "
                          + "ORDER BY KaitouNo DESC) IS NOT NULL) AND "
                          + "((SELECT                  TOP (1) ShouninFlg "
                          + "FROM                      T_NoukiKaitou AS T_NoukiKaitou_1 "
                          + "WHERE                   ( T_Chumon.Year = Year) AND ( T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun)  "
                          + "ORDER BY           KaitouNo DESC) IS NULL))");

                    */

                    w.Add("  ((SELECT                  TOP (1) ShouninFlg "
                    + " FROM                     dbo.T_NoukiKaitou AS T_NoukiKaitou_1 "
                    + " WHERE                    (dbo.T_Chumon.Year = Year) AND (dbo.T_Chumon.HacchuuNo = HacchuuNo) AND "
                    + " (dbo.T_Chumon.JigyoushoKubun = JigyoushoKubun) "
                    + " ORDER BY            KaitouNo DESC) = 0) AND "
                    + " ((SELECT                  TOP (1) KaitouNo "
                    + " FROM                     dbo.T_NoukiKaitou AS T_NoukiKaitou_1 "
                    + " WHERE                   (dbo.T_Chumon.Year = Year) AND (dbo.T_Chumon.HacchuuNo = HacchuuNo) AND "
                    + " (dbo.T_Chumon.JigyoushoKubun = JigyoushoKubun) "
                    + " ORDER BY           KaitouNo DESC) IS NOT NULL) ");
                }
            }
            // 納期変更
            if (k._NhJyoukyou > 0)
            {
                w.Add("((SELECT TOP (1) HenkouNo "
                + "FROM                     dbo.T_NoukiHenkou AS T_NoukiHenkou_1 "
                + "WHERE                    (dbo.T_Chumon.Year = Year) AND (dbo.T_Chumon.HacchuuNo = HacchuuNo) AND  "
                + "(dbo.T_Chumon.JigyoushoKubun = JigyoushoKubun) "
                + "ORDER BY            HenkouNo DESC) >= 1) AND "
                + "((SELECT                  TOP (1) ShouninFlg "
                + "FROM                     dbo.T_NoukiHenkou AS T_NoukiHenkou_1 "
                + "WHERE                   (dbo.T_Chumon.Year = Year) AND (dbo.T_Chumon.HacchuuNo = HacchuuNo) AND  "
                + "(dbo.T_Chumon.JigyoushoKubun = JigyoushoKubun) "
                + "ORDER BY           HenkouNo DESC) = 0) ");

            }
            // 納品状況
            if (k._NHJyoukyou > 0)
            {
                // 未完納
                if (k._NHJyoukyou == 1)
                {
                    /*
                    w.Add("(((SELECT SUM(Suuryou) AS NouhinSuuryou "
                       + "FROM T_Nouhin AS T_Nouhin "
                       + "WHERE (T_Chumon.Year = Year) AND (T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun)) <> "
                       + "T_Chumon.Suuryou ) OR "
                       + "((SELECT SUM(Suuryou) AS NouhinSuuryou "
                       + "FROM T_Nouhin AS T_Nouhin_1 "
                       + "WHERE (T_Chumon.Year = Year) AND (T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun)) IS NULL ))");
                    */
                    w.Add("T_Chumon.KannouFlg = 0 ");
                }
                else // 完納
                {
                    /*
                    w.Add("((SELECT SUM(Suuryou) AS NouhinSuuryou "
                      + "FROM T_Nouhin AS T_Nouhin_1 "
                      + "WHERE (T_Chumon.Year = Year) AND (T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun) ) =   "
                      + "T_Chumon.Suuryou )");
                    */
                    w.Add("T_Chumon.KannouFlg = 1 ");
                }
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
                w.Add(string.Format("M_Login.TantoushaCode = '{0}'", k._TantoushaCode));
            }
            // 発注日
            if (k._Hacchuubi != null)
            {
                w.Add(Core.Type.NengappiKikan.GenerateSQL(k._Hacchuubi, false, "(convert(varchar,T_Chumon.HacchuuBi,112))"));

            }
            // 納期
            if (k._Nouki != null && k._Nouki.KikanTypeIsNotNone)
            {
                w.Add(
                "(((SELECT              TOP (1) HenkouNo "
                    + "FROM                      T_NoukiHenkou AS T_NoukiHenkou_1 "
                    + "WHERE                   ( T_Chumon.Year = Year) AND ( T_Chumon.HacchuuNo = HacchuuNo)  AND (T_Chumon.JigyoushoKubun = JigyoushoKubun) "
                    + "ORDER BY           HenkouNo DESC) IS NULL AND "
                 + k._Nouki.GenerateSQL("T_Chumon.Nouki", false) + ") OR ("
                 + k._Nouki.GenerateSQL("(SELECT    Top(1)          Nouki "
                    + "FROM                      T_NoukiHenkou AS T_NoukiHenkou_2 "
                    + "WHERE ( T_Chumon.Year = Year) AND ( T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun)  ORDER BY HenkouNo DESC)", false) + "))");
                // + k._Nouki.GenerateSQL("T_NoukiHenkou.Nouki", false) + ")");        

            }

            // 回答納期            
            if (k._KaitouNouki != null && k._KaitouNouki.KikanTypeIsNotNone)
            {
                w.Add(k._KaitouNouki.GenerateSQL("(SELECT    Top(1)          Nouki "
                    + "FROM                      T_NoukiKaitou AS T_NoukiKaitou_2 "
                    + "WHERE ( T_Chumon.Year = Year) AND ( T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun)  ORDER BY KaitouNo DESC)", false));

                //w.Add(k._KaitouNouki.GenerateSQL("T_NoukiKaitou.Nouki ", false));

            }
            // 納品日
            if (k._NouhinBi != null && k._NouhinBi.KikanTypeIsNotNone)
            {
                // w.Add(k._NouhinBi.GenerateSQL("(convert(varchar,T_Nouhin.NouhinBi,112))", false));
                w.Add(Core.Type.NengappiKikan.GenerateSQL(k._NouhinBi, false, "(convert(varchar,T_Nouhin.NouhinBi,112))"));
            }
            // キャンセル
            if (k._Cancelbi == 0)
            {
                w.Add("(T_Chumon.CancelBi IS NULL) ");
            }
            else
            {
                w.Add("(T_Chumon.CancelBi IS NOT NULL) ");
            }
            // メッセージ
            if (k._Msg > -1)
            {
                if (k._Msg == 0)
                {
                    if (k._userKubun == (byte)UserKubun.Owner)
                    {
                        w.Add("  (((SELECT TOP (1) OpenedFlg "
                               + "FROM dbo.T_ChumonMsg AS T_ChumonMsg_1 "
                               + "WHERE (dbo.T_Chumon.Year = Year) AND (dbo.T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun)  "
                               + "ORDER BY TourokuBi DESC) = 0) AND "
                               + "((SELECT TOP (1) UserKubun "
                               + "FROM dbo.T_ChumonMsg AS T_ChumonMsg_2 "
                               + "WHERE (dbo.T_Chumon.Year = Year) AND (dbo.T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun)  "
                               + "ORDER BY TourokuBi DESC) = 2)) ");
                    }
                    else
                    {
                        w.Add("  (((SELECT TOP (1) OpenedFlg "
                             + "FROM dbo.T_ChumonMsg AS T_ChumonMsg_1 "
                             + "WHERE (dbo.T_Chumon.Year = Year) AND (dbo.T_Chumon.HacchuuNo = HacchuuNo)  AND (T_Chumon.JigyoushoKubun = JigyoushoKubun) "
                             + "ORDER BY TourokuBi DESC) = 0) AND "
                             + "((SELECT TOP (1) UserKubun "
                             + "FROM dbo.T_ChumonMsg AS T_ChumonMsg_2 "
                             + "WHERE (dbo.T_Chumon.Year = Year) AND (dbo.T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun)  "
                             + "ORDER BY TourokuBi DESC) = 1)) ");
                    }
                }
                else if (k._Msg == 1)
                {
                    if (k._userKubun == (byte)UserKubun.Owner)
                    {
                        w.Add("  (((SELECT TOP (1) OpenedFlg "
                                + "FROM dbo.T_ChumonMsg AS T_ChumonMsg_1 "
                                + "WHERE (dbo.T_Chumon.Year = Year) AND (dbo.T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun)  "
                                + "ORDER BY TourokuBi DESC) = 0) AND "
                                + "((SELECT TOP (1) UserKubun "
                                + "FROM dbo.T_ChumonMsg AS T_ChumonMsg_2 "
                                + "WHERE (dbo.T_Chumon.Year = Year) AND (dbo.T_Chumon.HacchuuNo = HacchuuNo)  AND (T_Chumon.JigyoushoKubun = JigyoushoKubun) "
                                + "ORDER BY TourokuBi DESC) = 1)) ");
                    }
                    else
                    {
                        w.Add("  (((SELECT TOP (1) OpenedFlg "
                               + "FROM dbo.T_ChumonMsg AS T_ChumonMsg_1 "
                               + "WHERE (dbo.T_Chumon.Year = Year) AND (dbo.T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun)  "
                               + "ORDER BY TourokuBi DESC) = 0) AND "
                               + "((SELECT TOP (1) UserKubun "
                               + "FROM dbo.T_ChumonMsg AS T_ChumonMsg_2 "
                               + "WHERE (dbo.T_Chumon.Year = Year) AND (dbo.T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun)  "
                               + "ORDER BY TourokuBi DESC) = 2)) ");
                    }
                }

                else
                {
                    w.Add("(((SELECT TOP (1) UserKubun "
                           + " FROM dbo.T_ChumonMsg "
                           + " WHERE (dbo.T_Chumon.Year = Year) AND (dbo.T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun)  "
                           + " ORDER BY MsgID DESC) IS NOT NULL) AND "
                           + "((SELECT                  TOP (1) OpenedFlg "
                           + "FROM                      T_ChumonMsg AS T_ChumonMsg_1 "
                           + "WHERE                   ( T_Chumon.Year = Year) AND ( T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun)  "
                           + "ORDER BY           MsgID DESC) = 1)) ");
                }

            }

            return w.WhereText;
        }

        /// <summary>
        /// 発注情報一覧
        /// </summary>
        /// <param name="k"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static ChumonDataSet.V_Chumon_JyouhouDataTable getV_Chumon_JyouhouDataTable(KensakuParam k, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT DISTINCT "
            + "TOP (100) PERCENT dbo.T_Chumon.Year, dbo.T_Chumon.HacchuuNo, dbo.T_Chumon.HacchuuBi, dbo.T_Chumon.HacchushaID, "
            + "dbo.M_Login.Name, dbo.T_Chumon.ShiiresakiCode, dbo.M_Shiiresaki.ShiiresakiMei, dbo.T_Chumon.BuhinKubun, "
            + "dbo.T_Chumon.BuhinCode, dbo.M_Buhin.BuhinMei, dbo.T_Chumon.Suuryou, dbo.T_Chumon.Tanka, dbo.M_Buhin.Tani,"
            + "dbo.T_Chumon.Kingaku, dbo.T_Chumon.NounyuuBashoCode, dbo.M_NounyuuBasho.BashoMei, dbo.T_Chumon.Nouki, "
            + "T_Chumon.Zeiritu,"
            + "dbo.T_Chumon.CancelBi, dbo.T_Chumon.JigyoushoKubun, T_Chumon.KariTankaFlg, dbo.M_Login.TantoushaCode, "
                        + "ISNULL((SELECT TOP (1) UserKubun "
                        + "FROM    T_ChumonMsg "
                        + "WHERE   ( T_Chumon.Year = Year) AND ( T_Chumon.HacchuuNo = HacchuuNo)  AND (T_Chumon.JigyoushoKubun = JigyoushoKubun) "
                        + "ORDER BY MsgID DESC),'') AS UserKubun, "

                        + "ISNULL((SELECT TOP (1) OpenedFlg "
                        + "FROM    T_ChumonMsg AS T_ChumonMsg_1 "
                        + "WHERE   ( T_Chumon.Year = Year) AND ( T_Chumon.HacchuuNo = HacchuuNo)  AND (T_Chumon.JigyoushoKubun = JigyoushoKubun) "
                        + "ORDER BY MsgID DESC),0) AS OpenedFlg, "

                        + "ISNULL((SELECT TOP (1) HenkouNo "
                        + "FROM     T_NoukiHenkou AS T_NoukiHenkou_1 "
                        + "WHERE    ( T_Chumon.Year = Year) AND ( T_Chumon.HacchuuNo = HacchuuNo)  AND (T_Chumon.JigyoushoKubun = JigyoushoKubun) "
                        + "ORDER BY HenkouNo DESC),0) AS HenkouNo, "

                        + "ISNULL((SELECT TOP (1) ShouninFlg "
                        + "FROM     T_NoukiHenkou AS T_NoukiHenkou_1 "
                        + "WHERE    ( T_Chumon.Year = Year) AND ( T_Chumon.HacchuuNo = HacchuuNo)  AND (T_Chumon.JigyoushoKubun = JigyoushoKubun) "
                        + "ORDER BY HenkouNo DESC),0) AS HenkouShouninFlg, "

                        + "ISNULL((SELECT TOP (1) KaitouNo "
                        + "FROM     T_NoukiKaitou AS T_NoukiKaitou_1  "
                        + "WHERE    ( T_Chumon.Year = Year) AND ( T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun)  "
                        + "ORDER BY KaitouNo DESC),0) AS KaitouNo, "

                        + "ISNULL((SELECT TOP (1) ShouninFlg "
                        + "FROM     T_NoukiKaitou AS T_NoukiKaitou_1 "
                        + "WHERE    ( T_Chumon.Year = Year) AND ( T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun)  "
                        + "ORDER BY KaitouNo DESC),0) AS KaitouShouninFlg, "

                        + "ISNULL((SELECT TOP (1) NouhinNo "
                        + "FROM     T_Nouhin  AS T_Nouhin_1 "
                        + "WHERE    ( T_Chumon.Year = Year) AND ( T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun)  "
                        + "ORDER BY NouhinNo DESC),0) AS NouhinNo, "

                        + "(SELECT TOP(1) NouhinBi "
                        + "FROM    T_Nouhin AS T_Nouhin_1 "
                        + "WHERE   ( T_Chumon.Year = Year ) AND( T_Chumon.HacchuuNo = HacchuuNo ) "
                        + "ORDER BY NouhinNo DESC) AS NouhinBi, "

                        + "ISNULL((SELECT SUM(Suuryou) AS NouhinSuuryou "
                        + "FROM    T_Nouhin AS T_Nouhin_1 "
                        + "WHERE   (Year =  T_Chumon.Year) AND (HacchuuNo =  T_Chumon.HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun) ),0) AS NouhinSuuryou, "
            + "dbo.T_Chumon.KeigenZeirituFlg "
        + "FROM dbo.T_Chumon INNER JOIN "
        + "dbo.M_Login ON dbo.T_Chumon.HacchushaID = dbo.M_Login.LoginID INNER JOIN "
        + "dbo.M_Shiiresaki ON dbo.T_Chumon.ShiiresakiCode = dbo.M_Shiiresaki.ShiiresakiCode INNER JOIN "
        + "dbo.M_Buhin ON dbo.T_Chumon.BuhinKubun = dbo.M_Buhin.BuhinKubun AND  "
        + "dbo.T_Chumon.BuhinCode = dbo.M_Buhin.BuhinCode INNER JOIN "
        + "dbo.M_NounyuuBasho ON dbo.T_Chumon.NounyuuBashoCode = dbo.M_NounyuuBasho.BashoCode LEFT OUTER JOIN "
        + "dbo.T_Nouhin ON dbo.T_Chumon.JigyoushoKubun = dbo.T_Nouhin.JigyoushoKubun AND dbo.T_Chumon.Year = dbo.T_Nouhin.Year AND  "
        + "dbo.T_Chumon.HacchuuNo = dbo.T_Nouhin.HacchuuNo LEFT OUTER JOIN "
        + "dbo.T_NoukiHenkou ON dbo.T_Chumon.JigyoushoKubun = dbo.T_NoukiHenkou.JigyoushoKubun AND  "
        + "dbo.T_Chumon.Year = dbo.T_NoukiHenkou.Year AND dbo.T_Chumon.HacchuuNo = dbo.T_NoukiHenkou.HacchuuNo LEFT OUTER JOIN "
        + "dbo.T_NoukiKaitou ON dbo.T_Chumon.JigyoushoKubun = dbo.T_NoukiKaitou.JigyoushoKubun AND  "
        + "dbo.T_Chumon.Year = dbo.T_NoukiKaitou.Year AND dbo.T_Chumon.HacchuuNo = dbo.T_NoukiKaitou.HacchuuNo ";
            // WHERE 
            string strW = WhereText(k, da.SelectCommand);
            if (strW != "")
            {
                da.SelectCommand.CommandText += "WHERE " + strW;
            }

            da.SelectCommand.CommandText += " ORDER BY T_Chumon.Year DESC, T_Chumon.HacchuuNo DESC ";
            ChumonDataSet.V_Chumon_JyouhouDataTable dt = new ChumonDataSet.V_Chumon_JyouhouDataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 発注情報一覧(Row)
        /// </summary>
        /// <param name="k"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static ChumonDataSet.V_Chumon_JyouhouRow getV_Chumon_JyouhouRow(string strYear, string strHacchuuNo, int nJigyoushoKubun, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "SELECT DISTINCT "
                + "TOP (100) PERCENT dbo.T_Chumon.Year, dbo.T_Chumon.HacchuuNo, dbo.T_Chumon.HacchuuBi, dbo.T_Chumon.HacchushaID, "
                + "dbo.M_Login.Name, dbo.T_Chumon.ShiiresakiCode, dbo.M_Shiiresaki.ShiiresakiMei, dbo.T_Chumon.BuhinKubun, "
                + "dbo.T_Chumon.BuhinCode, dbo.M_Buhin.BuhinMei, dbo.T_Chumon.Suuryou, dbo.T_Chumon.Tanka, dbo.M_Buhin.Tani,"
                + "dbo.T_Chumon.Kingaku, dbo.T_Chumon.NounyuuBashoCode, dbo.M_NounyuuBasho.BashoMei, dbo.T_Chumon.Nouki, "
                + "dbo.T_Chumon.CancelBi, dbo.T_Chumon.JigyoushoKubun, T_Chumon.KariTankaFlg, dbo.M_Login.TantoushaCode, "
                        + "(SELECT                  TOP (1) UserKubun "
                        + "FROM                      T_ChumonMsg "
                        + "WHERE                   ( T_Chumon.Year = Year) AND ( T_Chumon.HacchuuNo = HacchuuNo)  AND (T_Chumon.JigyoushoKubun = JigyoushoKubun) "
                        + "ORDER BY           MsgID DESC) AS UserKubun, "

                        + "(SELECT                  TOP (1) OpenedFlg "
                        + "FROM                      T_ChumonMsg AS T_ChumonMsg_1 "
                        + "WHERE                   ( T_Chumon.Year = Year) AND ( T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun) "
                        + "ORDER BY           MsgID DESC) AS OpenedFlg, "

                        + "(SELECT                  TOP (1) HenkouNo "
                        + "FROM                      T_NoukiHenkou AS T_NoukiHenkou_1 "
                        + "WHERE                   ( T_Chumon.Year = Year) AND ( T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun)  "
                        + "ORDER BY           HenkouNo DESC) AS HenkouNo, "

                        + "(SELECT                  TOP (1) ShouninFlg "
                        + "FROM                      T_NoukiHenkou AS T_NoukiHenkou_1 "
                        + "WHERE                   ( T_Chumon.Year = Year) AND ( T_Chumon.HacchuuNo = HacchuuNo)  AND (T_Chumon.JigyoushoKubun = JigyoushoKubun) "
                        + "ORDER BY           HenkouNo DESC) AS HenkouShouninFlg, "

                        + "(SELECT                  TOP (1) KaitouNo "
                        + "FROM                      T_NoukiKaitou AS T_NoukiKaitou_1  "
                        + "WHERE                   ( T_Chumon.Year = Year) AND ( T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun)  "
                        + "ORDER BY           KaitouNo DESC) AS KaitouNo, "

                        + "(SELECT                  TOP (1) ShouninFlg "
                        + "FROM                      T_NoukiKaitou AS T_NoukiKaitou_1 "
                        + "WHERE                   ( T_Chumon.Year = Year) AND ( T_Chumon.HacchuuNo = HacchuuNo)  AND (T_Chumon.JigyoushoKubun = JigyoushoKubun) "
                        + "ORDER BY           KaitouNo DESC) AS KaitouShouninFlg, "

                        + "(SELECT                  TOP (1) NouhinNo "
                        + "FROM                      T_Nouhin  AS T_Nouhin_1 "
                        + "WHERE                   ( T_Chumon.Year = Year) AND ( T_Chumon.HacchuuNo = HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun) "
                        + "ORDER BY           NouhinNo DESC) AS NouhinNo, "

                        + "(SELECT                  SUM(Suuryou) AS NouhinSuuryou "
                        + "FROM                      T_Nouhin AS T_Nouhin_1 "
                        + "WHERE                   (Year =  T_Chumon.Year) AND (HacchuuNo =  T_Chumon.HacchuuNo) AND (T_Chumon.JigyoushoKubun = JigyoushoKubun) ) AS NouhinSuuryou "


            + "FROM dbo.T_Chumon INNER JOIN "
            + "dbo.M_Login ON dbo.T_Chumon.HacchushaID = dbo.M_Login.LoginID INNER JOIN "
            + "dbo.M_Shiiresaki ON dbo.T_Chumon.ShiiresakiCode = dbo.M_Shiiresaki.ShiiresakiCode INNER JOIN "
            + "dbo.M_Buhin ON dbo.T_Chumon.BuhinKubun = dbo.M_Buhin.BuhinKubun AND  "
            + "dbo.T_Chumon.BuhinCode = dbo.M_Buhin.BuhinCode INNER JOIN "
            + "dbo.M_NounyuuBasho ON dbo.T_Chumon.NounyuuBashoCode = dbo.M_NounyuuBasho.BashoCode LEFT OUTER JOIN "
            + "dbo.T_Nouhin ON dbo.T_Chumon.JigyoushoKubun = dbo.T_Nouhin.JigyoushoKubun AND dbo.T_Chumon.Year = dbo.T_Nouhin.Year AND  "
            + "dbo.T_Chumon.HacchuuNo = dbo.T_Nouhin.HacchuuNo LEFT OUTER JOIN "
            + "dbo.T_NoukiHenkou ON dbo.T_Chumon.JigyoushoKubun = dbo.T_NoukiHenkou.JigyoushoKubun AND  "
            + "dbo.T_Chumon.Year = dbo.T_NoukiHenkou.Year AND dbo.T_Chumon.HacchuuNo = dbo.T_NoukiHenkou.HacchuuNo LEFT OUTER JOIN "
            + "dbo.T_NoukiKaitou ON dbo.T_Chumon.JigyoushoKubun = dbo.T_NoukiKaitou.JigyoushoKubun AND  "
            + "dbo.T_Chumon.Year = dbo.T_NoukiKaitou.Year AND dbo.T_Chumon.HacchuuNo = dbo.T_NoukiKaitou.HacchuuNo "
            + "WHERE (T_Chumon.Year = @Year) AND (T_Chumon.HacchuuNo = @HacchuuNo) AND (T_Chumon.JigyoushoKubun = @JigyoushoKubun)";

            da.SelectCommand.Parameters.AddWithValue("@Year", strYear);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", strHacchuuNo);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nJigyoushoKubun);

            da.SelectCommand.CommandText += " ORDER BY          T_Chumon.HacchuuNo ";
            ChumonDataSet.V_Chumon_JyouhouDataTable dt = new ChumonDataSet.V_Chumon_JyouhouDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (ChumonDataSet.V_Chumon_JyouhouRow)dt.Rows[0];
            else
                return null;
        }
        /// <summary>
        /// 主キーによって、明細を取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static ChumonDataSet.V_Chumon_MeisaiRow
            getV_Chumon_MeisaiRow(string strYear, string strHacchuuNo, int nJigyoushoKubun, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
               "SELECT          T_Chumon.Year, T_Chumon.HacchuuNo, T_Chumon.HacchuuBi, "
            + "T_Chumon.ShiiresakiCode, M_Shiiresaki.ShiiresakiMei, "
            + "T_Chumon.BuhinKubun, T_Chumon.BuhinCode, M_Buhin.BuhinMei, "
            + "T_Chumon.Suuryou, T_Chumon.Tanka, M_Buhin.Tani, "
            + "T_Chumon.Kingaku, T_Chumon.NounyuuBashoCode, "
            + "T_Chumon.HacchushaID, M_Login.Name, T_Chumon.Bikou, "
            + "M_NounyuuBasho.BashoMei, T_Chumon.CancelBi, "
            + "T_Chumon.JigyoushoKubun, T_Chumon.KaritankaFlg, dbo.M_Login.TantoushaCode "
            + "FROM            T_Chumon INNER JOIN "
            + "M_Shiiresaki ON "
            + "T_Chumon.ShiiresakiCode = M_Shiiresaki.ShiiresakiCode INNER JOIN "
            + "M_Buhin ON T_Chumon.BuhinKubun = M_Buhin.BuhinKubun AND "
            + "T_Chumon.BuhinCode = M_Buhin.BuhinCode INNER JOIN "
            + "M_NounyuuBasho ON "
            + "T_Chumon.NounyuuBashoCode = M_NounyuuBasho.BashoCode INNER JOIN "
            + "M_Login ON T_Chumon.HacchushaID = M_Login.LoginID "
            + "WHERE (T_Chumon.HacchuuNo = @HacchuuNo)  AND (T_Chumon.Year = @Year) AND (T_Chumon.JigyoushoKubun = @JigyoushoKubun)  ";
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", strHacchuuNo);
            da.SelectCommand.Parameters.AddWithValue("@Year", strYear);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nJigyoushoKubun);

            ChumonDataSet.V_Chumon_MeisaiDataTable dt = new ChumonDataSet.V_Chumon_MeisaiDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (ChumonDataSet.V_Chumon_MeisaiRow)dt.Rows[0];
            else
                return null;
        }

        /// <summary>
        /// 主キーによって、明細を取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static ChumonDataSet.V_Chumon_MeisaiDataTable
            getV_Chumon_MeisaiDataTable(string strKeyAry, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT          T_Chumon.Year, T_Chumon.HacchuuNo, T_Chumon.HacchuuBi, "
            + "T_Chumon.ShiiresakiCode, M_Shiiresaki.ShiiresakiMei, "
            + "T_Chumon.BuhinKubun, T_Chumon.BuhinCode, M_Buhin.BuhinMei, "
            + "T_Chumon.Suuryou, T_Chumon.Tanka, M_Buhin.Tani, "
            + "T_Chumon.Kingaku, T_Chumon.NounyuuBashoCode, "
            + "T_Chumon.HacchushaID, M_Login.Name, T_Chumon.Bikou, "
            + "M_NounyuuBasho.BashoMei, T_Chumon.CancelBi, "
            + "T_Chumon.JigyoushoKubun, T_Chumon.KaritankaFlg, dbo.M_Login.TantoushaCode  "
            + "FROM            T_Chumon INNER JOIN "
            + "M_Shiiresaki ON "
            + "T_Chumon.ShiiresakiCode = M_Shiiresaki.ShiiresakiCode INNER JOIN "
            + "M_Buhin ON T_Chumon.BuhinKubun = M_Buhin.BuhinKubun AND "
            + "T_Chumon.BuhinCode = M_Buhin.BuhinCode INNER JOIN "
            + "M_NounyuuBasho ON "
            + "T_Chumon.NounyuuBashoCode = M_NounyuuBasho.BashoCode INNER JOIN "
            + "M_Login ON T_Chumon.HacchushaID = M_Login.LoginID "
            + "WHERE T_Chumon.CancelBi IS NULL ";
            string strWhere = WhereKey(strKeyAry);
            if (strWhere != "")
                da.SelectCommand.CommandText += "AND " + strWhere;

            da.SelectCommand.CommandText += "ORDER BY T_Chumon.JigyoushoKubun ";
            ChumonDataSet.V_Chumon_MeisaiDataTable dt = new ChumonDataSet.V_Chumon_MeisaiDataTable();
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        /// T_Chumonテーブルからすべて仕入先を取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static ChumonDataSet.V_ChumonShiiresakiDataTable
            getV_ChumonShiiresakiDataTablee(SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT      DISTINCT            TOP (100) PERCENT T_Chumon.ShiiresakiCode, M_Shiiresaki.ShiiresakiMei "
            + "FROM                     T_Chumon INNER JOIN "
            + "M_Shiiresaki ON T_Chumon.ShiiresakiCode = M_Shiiresaki.ShiiresakiCode "
            + "ORDER BY           T_Chumon.ShiiresakiCode ";
            ChumonDataSet.V_ChumonShiiresakiDataTable dt = new ChumonDataSet.V_ChumonShiiresakiDataTable();
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        ///  T_Chumonテーブルからすべて納入場所を取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static ChumonDataSet.V_ChumonNounyuuBashoDataTable
            getV_ChumonNounyuuBashoDataTable(SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT DISTINCT TOP (100) PERCENT T_Chumon.NounyuuBashoCode, M_NounyuuBasho.BashoMei "
            + "FROM                     M_NounyuuBasho INNER JOIN "
            + "T_Chumon ON M_NounyuuBasho.BashoCode = T_Chumon.NounyuuBashoCode "
            + "ORDER BY           T_Chumon.NounyuuBashoCode ";
            ChumonDataSet.V_ChumonNounyuuBashoDataTable dt = new ChumonDataSet.V_ChumonNounyuuBashoDataTable();
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        /// T_Chumonテーブルからすべて部品区分を取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static ChumonDataSet.V_ChumonBuhinKubunDataTable
            getV_ChumonBuhinKubunDataTable(string strKaisha, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT     DISTINCT             TOP (100) PERCENT BuhinKubun "
            + "FROM                     T_Chumon ";
            if (strKaisha != null)
            {
                da.SelectCommand.CommandText += "WHERE " + string.Format("T_Chumon.ShiiresakiCode = '{0}'", strKaisha);
            }
            da.SelectCommand.CommandText += "ORDER BY           BuhinKubun ";
            ChumonDataSet.V_ChumonBuhinKubunDataTable dt = new ChumonDataSet.V_ChumonBuhinKubunDataTable();
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        /// 部品区分によって、T_Chumonデーブルからすべて部品を取得
        /// </summary>
        /// <param name="strKubun"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static ChumonDataSet.V_ChumonBuhinDataTable
            getV_ChumonBuhinDataTable(string strKubun, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT         DISTINCT         TOP (100) PERCENT T_Chumon.BuhinCode, M_Buhin.BuhinMei, T_Chumon.BuhinKubun "
            + "FROM                     T_Chumon INNER JOIN "
            + "M_Buhin ON T_Chumon.BuhinKubun = M_Buhin.BuhinKubun AND T_Chumon.BuhinCode = M_Buhin.BuhinCode "
            + "WHERE                   (T_Chumon.BuhinKubun = @kubun) AND (T_Chumon.CancelBi IS NULL) "
            + "ORDER BY           T_Chumon.BuhinKubun, T_Chumon.BuhinCode ";
            da.SelectCommand.Parameters.AddWithValue("@kubun", strKubun);
            ChumonDataSet.V_ChumonBuhinDataTable dt = new ChumonDataSet.V_ChumonBuhinDataTable();
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        /// T_Chumonテーブルからすべての発注担当者を取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static ChumonDataSet.V_ChumonTantoushaDataTable
            getV_ChumonTantoushaDataTable(SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT       DISTINCT           TOP (100) PERCENT M_Login.TantoushaCode, M_Login.Name "
            + "FROM                     T_Chumon INNER JOIN "
            + "M_Login ON T_Chumon.HacchushaID = M_Login.LoginID "
             + "WHERE T_Chumon.CancelBi IS NULL "
            + "ORDER BY            dbo.M_Login.TantoushaCode ";
            ChumonDataSet.V_ChumonTantoushaDataTable dt = new ChumonDataSet.V_ChumonTantoushaDataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 検収情報
        /// </summary>
        /// <param name="k"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        //public static ChumonDataSet.V_Chumon_KenshuRow
        //getV_Chumon_KenshuRow(string strYear, string strHacchuuNo, SqlConnection sqlConn)
        public static ChumonDataSet.V_Chumon_KenshuDataTable
            getV_Chumon_KenshuDataTable(string strKeyAry, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT                  TOP (100) PERCENT T_Chumon.Year, T_Chumon.HacchuuNo, T_Chumon.HacchuuBi, T_Chumon.ShiiresakiCode, "
            + "M_Shiiresaki.ShiiresakiMei, T_Chumon.BuhinKubun, T_Chumon.BuhinCode, M_Buhin.BuhinMei, T_Chumon.Suuryou, "
            + "T_Chumon.Tanka, M_NounyuuBasho.BashoMei, M_Buhin.Tani "
            + "FROM                     T_Chumon INNER JOIN "
            + "M_Shiiresaki ON T_Chumon.ShiiresakiCode = M_Shiiresaki.ShiiresakiCode INNER JOIN "
            + "M_Buhin ON T_Chumon.BuhinKubun = M_Buhin.BuhinKubun AND "
            + "T_Chumon.BuhinCode = M_Buhin.BuhinCode INNER JOIN "
            + "M_NounyuuBasho ON T_Chumon.NounyuuBashoCode = M_NounyuuBasho.BashoCode "
             + "WHERE T_Chumon.CancelBi IS NULL ";

            string strWhere = WhereKey(strKeyAry);
            if (strWhere != "")
                da.SelectCommand.CommandText += "AND " + strWhere;

            da.SelectCommand.CommandText += " ORDER BY T_Chumon.ShiiresakiCode";

            ChumonDataSet.V_Chumon_KenshuDataTable dt = new ChumonDataSet.V_Chumon_KenshuDataTable();
            da.Fill(dt);
            return dt;

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
                int nKubun = int.Parse(strKey[2]);

                if (sb.Length > 0) sb.Append(" OR ");

                sb.Append("(T_Chumon.Year = '" + strYear + "' AND T_Chumon.HacchuuNo = '" + strHacchuNo + "' AND T_Chumon.JigyoushoKubun = '" + nKubun + "')");

            }

            return "(" + sb.ToString() + ")";
        }


        /// <summary>
        /// キャンセル日の登録
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="HacchuuNo"></param>
        /// <param name="dr"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LibError
            T_Chumon_Update_CancelBi(string Year, string HacchuuNo, int nJigyoushoKubun, m2mKoubaiDataSet.T_ChumonRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_Chumon WHERE Year = @Year AND HacchuuNo = @HacchuuNo AND JigyoushoKubun = @JigyoushoKubun";
            da.SelectCommand.Parameters.AddWithValue("@Year", Year);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", HacchuuNo);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nJigyoushoKubun);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();


            m2mKoubaiDataSet.T_ChumonDataTable dt = new m2mKoubaiDataSet.T_ChumonDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new LibError();
            try
            {
                m2mKoubaiDataSet.T_ChumonRow drThis = (m2mKoubaiDataSet.T_ChumonRow)dt.Rows[0];

                if (!dr.IsCancelBiNull())
                    drThis.CancelBi = dr.CancelBi;
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e.ToString());
            }
        }
        /// <summary>
        /// 数量、単価の更新
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="HacchuuNo"></param>
        /// <param name="dr"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LibError
            T_Chumon_Update(string Year, string HacchuuNo, int nJigyoushoKubun, m2mKoubaiDataSet.T_ChumonRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_Chumon WHERE Year = @Year AND HacchuuNo = @HacchuuNo AND JigyoushoKubun = @JigyoushoKubun ";
            da.SelectCommand.Parameters.AddWithValue("@Year", Year);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", HacchuuNo);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nJigyoushoKubun);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            m2mKoubaiDataSet.T_ChumonDataTable dt = new m2mKoubaiDataSet.T_ChumonDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new LibError();
            try
            {
                m2mKoubaiDataSet.T_ChumonRow drThis = (m2mKoubaiDataSet.T_ChumonRow)dt.Rows[0];

                if (dr.Suuryou > 0)
                    drThis.Suuryou = dr.Suuryou;
                if (dr.Tanka >= 0)
                {
                    drThis.Tanka = dr.Tanka;
                    //drThis.Kingaku = (int)Math.Floor(dr.Tanka * dt[0].Suuryou);   // 2015.03.05 金額は常に再計算するように修正
                    drThis.KaritankaFlg = false;
                }
                drThis.Kingaku = (int)Math.Floor(drThis.Tanka * drThis.Suuryou);    // 2015.03.05 金額は常に再計算するように修正
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e.ToString());
            }
        }
        /// <summary>
        /// メール情報を取得
        /// </summary>
        /// <param name="strYear"></param>
        /// <param name="strHacchuuNo"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static ChumonDataSet.V_Chumon_MailDataTable
            getV_Chumon_MailDataTable(string strYear, string strHacchuuNo, int nJigyoushoKubun, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT M_Login.Mail AS FromMail, M_Login_1.Mail AS ToMail, T_KaishaInfo.KaishaMei, T_KaishaInfo.EigyouSho, "
            + "T_KaishaInfo.Tel, T_KaishaInfo.Fax, M_Login.Busho, "
            + "M_Login.Name, M_Login.JigyoushoKubun, M_Login.LoginID "

            + "FROM T_Chumon INNER JOIN "
            + "M_Login ON T_Chumon.HacchushaID = M_Login.LoginID INNER JOIN "
            + "T_KaishaInfo ON M_Login.JigyoushoKubun = T_KaishaInfo.KaishaID INNER JOIN "
            + "M_Login AS M_Login_1 ON T_Chumon.ShiiresakiCode = M_Login_1.KaishaCode "
            + "WHERE (T_Chumon.Year = @Year) AND (T_Chumon.HacchuuNo = @HacchuuNo) AND (T_Chumon.JigyoushoKubun = @JigyoushoKubun)  ";

            da.SelectCommand.Parameters.AddWithValue("@Year", strYear);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", strHacchuuNo);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nJigyoushoKubun);

            ChumonDataSet.V_Chumon_MailDataTable dt = new ChumonDataSet.V_Chumon_MailDataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strYear"></param>
        /// <param name="strHacchuuNo"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static ChumonDataSet.V_Chumon_MailRow
            getV_Chumon_MailRow(string strYear, string strHacchuuNo, int nJigyoushoKubun, string strLoginID, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT DISTINCT  M_Login.Mail AS ToMail, M_Login_1.Mail AS FromMail, T_KaishaInfo.KaishaMei, T_KaishaInfo.EigyouSho, "
            + "T_KaishaInfo.Tel, T_KaishaInfo.Fax, M_Login.Busho, "
            + "M_Login.Name, M_Login.JigyoushoKubun, M_Login.LoginID "

            + "FROM T_Chumon INNER JOIN "
            + "M_Login ON T_Chumon.HacchushaID = M_Login.LoginID INNER JOIN "
            + "T_KaishaInfo ON M_Login.JigyoushoKubun = T_KaishaInfo.KaishaID INNER JOIN "
            + "M_Login AS M_Login_1 ON T_Chumon.ShiiresakiCode = M_Login_1.KaishaCode "

            + "WHERE (T_Chumon.Year = @Year) AND (T_Chumon.HacchuuNo = @HacchuuNo) AND "
            + "(T_Chumon.JigyoushoKubun = @JigyoushoKubun)  AND (M_Login_1.LoginID = @ID) ";

            da.SelectCommand.Parameters.AddWithValue("@Year", strYear);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", strHacchuuNo);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nJigyoushoKubun);
            da.SelectCommand.Parameters.AddWithValue("@ID", strLoginID);

            ChumonDataSet.V_Chumon_MailDataTable dt = new ChumonDataSet.V_Chumon_MailDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (ChumonDataSet.V_Chumon_MailRow)dt.Rows[0];
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nKubun"></param>
        /// <param name="strID"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static ChumonDataSet.V_MailToCCDataTable
            getV_MailToCCDataTable(int nKubun, string strID, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "SELECT DISTINCT LoginID, JigyoushoKubun, Mail "
                + "FROM M_Login "
                + "WHERE (JigyoushoKubun = @Kubun) AND (LoginID <> @LoginID) ";
            da.SelectCommand.Parameters.AddWithValue("@Kubun", nKubun);
            da.SelectCommand.Parameters.AddWithValue("@LoginID", strID);

            ChumonDataSet.V_MailToCCDataTable dt = new ChumonDataSet.V_MailToCCDataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 全て発注データ取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static m2mKoubaiDataSet.T_ChumonDataTable
            getT_ChumonDataTable(SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Chumon ORDER BY T_Chumon.HacchuuNo DESC ";
            m2mKoubaiDataSet.T_ChumonDataTable dt = new m2mKoubaiDataSet.T_ChumonDataTable();
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        /// NewRow
        /// </summary>
        /// <returns></returns>
        public static m2mKoubaiDataSet.T_ChumonRow newT_ChumonRow()
        {
            return new m2mKoubaiDataSet.T_ChumonDataTable().NewT_ChumonRow();
        }

        /// <summary>
        /// Row取得
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="HacchuuNo"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        /// 追加 09/07/23
        public static m2mKoubaiDataSet.T_ChumonRow
           getT_ChumonRow(string Year, string HacchuuNo, int nJigyoushoKubun, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "SELECT *  FROM dbo.T_Chumon "
                + "WHERE  (dbo.T_Chumon.Year = @Year) AND (dbo.T_Chumon.HacchuuNo = @HacchuuNo) AND (T_Chumon.JigyoushoKubun = @JigyoushoKubun) ";
            da.SelectCommand.Parameters.AddWithValue("@Year", Year);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", HacchuuNo);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nJigyoushoKubun);

            m2mKoubaiDataSet.T_ChumonDataTable dt = new m2mKoubaiDataSet.T_ChumonDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (m2mKoubaiDataSet.T_ChumonRow)dt.Rows[0];
            else
                return null;
        }

        /// <summary>
        /// 完納フラグ変更のためRow更新
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="HacchuuNo"></param>
        /// <param name="dr"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        /// 追加 09/07/23
        public static LibError
            T_Chumon_Update_Kannou(string Year, string HacchuuNo, int nJigyoushoKubun, m2mKoubaiDataSet.T_ChumonRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM T_Chumon WHERE Year = @Year AND HacchuuNo = @HacchuuNo  AND JigyoushoKubun = @JigyoushoKubun ";
            da.SelectCommand.Parameters.AddWithValue("@Year", Year);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", HacchuuNo);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nJigyoushoKubun);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            m2mKoubaiDataSet.T_ChumonDataTable dt = new m2mKoubaiDataSet.T_ChumonDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new LibError("エラー");
            try
            {
                m2mKoubaiDataSet.T_ChumonRow drThis = (m2mKoubaiDataSet.T_ChumonRow)dt.Rows[0];
                drThis.KannouFlg = dr.KannouFlg;
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
        }

        /// <summary>
        /// 新規注文登録時、送信メール情報取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static ChumonDataSet.V_MailInfoDataTable
            getV_MailInfoDataTable(string strLoginID, string strShiireCode, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT DISTINCT "
            + "M_Login.Mail AS Mail_Y, M_Login_1.Mail AS Mail_S, T_KaishaInfo.KaishaMei, T_KaishaInfo.EigyouSho, T_KaishaInfo.Tel, "
            + "T_KaishaInfo.Fax, M_Login.Busho, M_Login.Name, M_Login.JigyoushoKubun, M_Login.LoginID AS LoginID_Y, "
            + "M_Login_1.LoginID AS LoginID_S, M_Login_1.KaishaCode "
            + "FROM                     T_Chumon INNER JOIN "
            + "M_Login ON T_Chumon.HacchushaID = M_Login.LoginID INNER JOIN "
            + "T_KaishaInfo ON M_Login.JigyoushoKubun = T_KaishaInfo.KaishaID INNER JOIN "
            + "M_Login AS M_Login_1 ON T_Chumon.ShiiresakiCode = M_Login_1.KaishaCode "
            + "WHERE                   (M_Login.LoginID = @LoginID) AND (M_Login_1.KaishaCode = @ShiireCode) ";

            da.SelectCommand.Parameters.AddWithValue("@LoginID", strLoginID);
            da.SelectCommand.Parameters.AddWithValue("@ShiireCode", strShiireCode);

            ChumonDataSet.V_MailInfoDataTable dt = new ChumonDataSet.V_MailInfoDataTable();
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        /// 新規注文登録時、送信メール情報取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static ChumonDataSet.V_MailInfoDataTable
            getV_MailInfo2DataTable(string strLoginID, string strShiireCode, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT DISTINCT "
            + "M_Login.Mail AS Mail_Y, M_Login_1.Mail AS Mail_S, T_KaishaInfo.KaishaMei, T_KaishaInfo.EigyouSho, T_KaishaInfo.Tel, "
            + "T_KaishaInfo.Fax, M_Login.Busho, M_Login.Name, M_Login.JigyoushoKubun, M_Login.LoginID AS LoginID_Y, "
            + "M_Login_1.LoginID AS LoginID_S, M_Login_1.KaishaCode "
            + "FROM                     T_Chumon INNER JOIN "
            + "M_Login ON T_Chumon.HacchushaID = M_Login.LoginID INNER JOIN "
            + "T_KaishaInfo ON M_Login.JigyoushoKubun = T_KaishaInfo.KaishaID INNER JOIN "
            + "M_Login AS M_Login_1 ON T_Chumon.ShiiresakiCode = M_Login_1.KaishaCode "
            + "WHERE                   (M_Login_1.LoginID = @LoginID) AND (M_Login_1.KaishaCode = @ShiireCode) ";

            da.SelectCommand.Parameters.AddWithValue("@LoginID", strLoginID);
            da.SelectCommand.Parameters.AddWithValue("@ShiireCode", strShiireCode);

            ChumonDataSet.V_MailInfoDataTable dt = new ChumonDataSet.V_MailInfoDataTable();
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        /// 数量変更、注文キャンセルされたとき、メール送信情報
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static ChumonDataSet.V_MailInfoDataTable
            getV_MailInfoDataTable(string strYear, string strHacchuuNo, int nJigyoushoKubun, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT DISTINCT "
            + "M_Login.Mail AS Mail_Y, M_Login_1.Mail AS Mail_S, T_KaishaInfo.KaishaMei, T_KaishaInfo.EigyouSho, T_KaishaInfo.Tel, "
            + "T_KaishaInfo.Fax, M_Login.Busho, M_Login.Name, M_Login.JigyoushoKubun, M_Login.LoginID AS LoginID_Y, "
            + "M_Login_1.LoginID AS LoginID_S, M_Login_1.KaishaCode "
            + "FROM                     T_Chumon INNER JOIN "
            + "M_Login ON T_Chumon.HacchushaID = M_Login.LoginID INNER JOIN "
            + "T_KaishaInfo ON M_Login.JigyoushoKubun = T_KaishaInfo.KaishaID INNER JOIN "
            + "M_Login AS M_Login_1 ON T_Chumon.ShiiresakiCode = M_Login_1.KaishaCode "

            + "WHERE (T_Chumon.Year = @Year) AND (T_Chumon.HacchuuNo = @HacchuuNo) AND (T_Chumon.JigyoushoKubun = @JigyoushoKubun)  ";

            da.SelectCommand.Parameters.AddWithValue("@Year", strYear);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", strHacchuuNo);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nJigyoushoKubun);

            ChumonDataSet.V_MailInfoDataTable dt = new ChumonDataSet.V_MailInfoDataTable();
            da.Fill(dt);
            return dt;
        }
      
           public static ChumonDataSet.V_MailInfoDataTable
            getV_MailInfoDataTable(string strYear, string strHacchuuNo, int nJigyoushoKubun, string strID, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT DISTINCT "
            + "M_Login.Mail AS Mail_Y, M_Login_1.Mail AS Mail_S, T_KaishaInfo.KaishaMei, T_KaishaInfo.EigyouSho, T_KaishaInfo.Tel, "
            + "T_KaishaInfo.Fax, M_Login.Busho, M_Login.Name, M_Login.JigyoushoKubun, M_Login.LoginID AS LoginID_Y, "
            + "M_Login_1.LoginID AS LoginID_S, M_Login_1.KaishaCode "
            + "FROM                     T_Chumon INNER JOIN "
            + "M_Login ON T_Chumon.HacchushaID = M_Login.LoginID INNER JOIN "
            + "T_KaishaInfo ON M_Login.JigyoushoKubun = T_KaishaInfo.KaishaID INNER JOIN "
            + "M_Login AS M_Login_1 ON T_Chumon.ShiiresakiCode = M_Login_1.KaishaCode "

            + "WHERE (T_Chumon.Year = @Year) AND (T_Chumon.HacchuuNo = @HacchuuNo) AND "
            + "(T_Chumon.JigyoushoKubun = @JigyoushoKubun)   AND (M_Login_1.LoginID = @ID) ";

            da.SelectCommand.Parameters.AddWithValue("@Year", strYear);
            da.SelectCommand.Parameters.AddWithValue("@HacchuuNo", strHacchuuNo);
            da.SelectCommand.Parameters.AddWithValue("@JigyoushoKubun", nJigyoushoKubun);
            da.SelectCommand.Parameters.AddWithValue("@ID", strID);
            ChumonDataSet.V_MailInfoDataTable dt = new ChumonDataSet.V_MailInfoDataTable();
            da.Fill(dt);
            return dt;
        }




        /// <summary>
        /// 回答納期登録、更新とき、メール送信情報
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static ChumonDataSet.V_MailInfoDataTable
            getV_MailInfo_KNDataTable(string strKeyAry, string strID, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT DISTINCT "
            + "M_Login.Mail AS Mail_Y, M_Login_1.Mail AS Mail_S, T_KaishaInfo.KaishaMei, T_KaishaInfo.EigyouSho, T_KaishaInfo.Tel, "
            + "T_KaishaInfo.Fax, M_Login.Busho, M_Login.Name, M_Login.JigyoushoKubun, M_Login.LoginID AS LoginID_Y, "
            + "M_Login_1.LoginID AS LoginID_S, M_Login_1.KaishaCode "
            + "FROM                     T_Chumon INNER JOIN "
            + "M_Login ON T_Chumon.HacchushaID = M_Login.LoginID INNER JOIN "
            + "T_KaishaInfo ON M_Login.JigyoushoKubun = T_KaishaInfo.KaishaID INNER JOIN "
            + "M_Login AS M_Login_1 ON T_Chumon.ShiiresakiCode = M_Login_1.KaishaCode "

            + "WHERE (M_Login_1.LoginID = @ID) ";

            string strWhere = WhereKey2(strKeyAry);
            if (strWhere != "")
                da.SelectCommand.CommandText += "AND " + strWhere;

            da.SelectCommand.Parameters.AddWithValue("@ID", strID);
            da.SelectCommand.CommandText += "ORDER BY           LoginID_Y ";
            ChumonDataSet.V_MailInfoDataTable dt = new ChumonDataSet.V_MailInfoDataTable();
            da.Fill(dt);
            return dt;
        }

        
       /// <summary>
       /// メール情報を取得
       /// </summary>
       /// <param name="strYear"></param>
       /// <param name="strHacchuuNo"></param>
       /// <param name="sqlConn"></param>
       /// <returns></returns>
       public static ChumonDataSet.V_Chumon_MailDataTable
           getV_Chumon_Mail_KaishaInfoDataTable(string strLoginID, string strShiireCode, SqlConnection sqlConn)
       {
           SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
           da.SelectCommand.CommandText =
           "SELECT DISTINCT  M_Login.Mail AS ToMail, M_Login_1.Mail AS FromMail, T_KaishaInfo.KaishaMei, T_KaishaInfo.EigyouSho, "
           + "T_KaishaInfo.Tel, T_KaishaInfo.Fax, M_Login.Busho, "
           + "M_Login.Name, M_Login.JigyoushoKubun, M_Login.LoginID "

           + "FROM T_Chumon INNER JOIN "
           + "M_Login ON T_Chumon.HacchushaID = M_Login.LoginID INNER JOIN "
           + "T_KaishaInfo ON M_Login.JigyoushoKubun = T_KaishaInfo.KaishaID INNER JOIN "
           + "M_Login AS M_Login_1 ON T_Chumon.ShiiresakiCode = M_Login_1.KaishaCode "

           + "WHERE (M_Login_1.LoginID = @LoginID) AND (M_Login_1.KaishaCode = @ShiireCode) ";

           da.SelectCommand.Parameters.AddWithValue("@LoginID", strLoginID);
           da.SelectCommand.Parameters.AddWithValue("@ShiireCode", strShiireCode);
           ChumonDataSet.V_Chumon_MailDataTable dt = new ChumonDataSet.V_Chumon_MailDataTable();
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
           string[] strKeyAry = strAry.Split('\t');



           StringBuilder sb = new StringBuilder();
           for (int i = 0; i < strKeyAry.Length; i++)
           {
               string[] strKey = strKeyAry[i].Split('_');
               string strYear = strKey[0];
               string strHacchuNo = strKey[1];
               int nKubun = int.Parse(strKey[2]);

               if (sb.Length > 0) sb.Append(" OR ");

               sb.Append("(T_Chumon.Year = '" + strYear + "' AND T_Chumon.HacchuuNo = '" + strHacchuNo + "' AND T_Chumon.JigyoushoKubun = '" + nKubun + "')");

           }

           return "(" + sb.ToString() + ")";
       }
        public static ChumonDataSet.V_Chumon_NoukiKaitouDataTable
            getV_Chumon_NoukiKaitouDataTable(string strKeyAry, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT DISTINCT "
             + " TOP (100) PERCENT dbo.T_Chumon.Year, dbo.T_Chumon.HacchuuNo, dbo.M_Login.Name, dbo.T_Chumon.ShiiresakiCode,  "
             + " dbo.M_Shiiresaki.ShiiresakiMei, dbo.T_Chumon.JigyoushoKubun, dbo.M_Login.TantoushaCode, "
             + " (SELECT                  TOP (1) KaitouNo "
             + " FROM                     dbo.T_NoukiKaitou AS T_NoukiKaitou_1 "
             + " WHERE                   (dbo.T_Chumon.Year = Year) AND (dbo.T_Chumon.HacchuuNo = HacchuuNo) AND  "
             + " (dbo.T_Chumon.JigyoushoKubun = JigyoushoKubun) "
             + " ORDER BY           KaitouNo DESC) AS KaitouNo, dbo.T_Chumon.HacchushaID, dbo.M_Login.Mail "
             + "FROM dbo.T_Chumon INNER JOIN "
             + "dbo.M_Login ON dbo.T_Chumon.HacchushaID = dbo.M_Login.LoginID INNER JOIN "
             + "dbo.M_Shiiresaki ON dbo.T_Chumon.ShiiresakiCode = dbo.M_Shiiresaki.ShiiresakiCode LEFT OUTER JOIN "
             + "dbo.T_NoukiKaitou ON dbo.T_Chumon.JigyoushoKubun = dbo.T_NoukiKaitou.JigyoushoKubun AND  "
             + " dbo.T_Chumon.Year = dbo.T_NoukiKaitou.Year AND dbo.T_Chumon.HacchuuNo = dbo.T_NoukiKaitou.HacchuuNo ";
           
            string strWhere = WhereKey2(strKeyAry);
            if (strWhere != "")
                da.SelectCommand.CommandText += "WHERE " + strWhere;
            da.SelectCommand.CommandText += "ORDER BY           dbo.T_Chumon.HacchushaID ";

            ChumonDataSet.V_Chumon_NoukiKaitouDataTable dt = new ChumonDataSet.V_Chumon_NoukiKaitouDataTable();
            da.Fill(dt);
            return dt;


        }
       
    }
}