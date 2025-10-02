using Core;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace KoubaiDAL
{
    public class MasterClass
    {
        public static MasterDataSet.M_BuhinRow getM_Buhin1Row(string p, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM M_Buhin WHERE BuhinCode = @SCode";
            da.SelectCommand.Parameters.AddWithValue("@SCode", p);
           

            MasterDataSet.M_BuhinDataTable dt = new MasterDataSet.M_BuhinDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return dt[0];
            else
                return null;
        }

        public static MasterDataSet.M_KouseiRow getM_KouseiRow(string sSeihinCode, string sBuhinCode, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM M_Kousei WHERE SeihinCode = @s AND BuhinCode = @b ";
            da.SelectCommand.Parameters.AddWithValue("@s", sSeihinCode);
            da.SelectCommand.Parameters.AddWithValue("@b", sBuhinCode);

            MasterDataSet.M_KouseiDataTable dt = new MasterDataSet.M_KouseiDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return dt[0];
            else
                return null;
        }

        public static MasterDataSet.M_KouseiDataTable getM_KouseiDataTable(string sSeihinCode, string ORNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM M_Kousei WHERE SeihinCode = @s AND ORNo = @o";
            da.SelectCommand.Parameters.AddWithValue("@s", sSeihinCode);
            da.SelectCommand.Parameters.AddWithValue("@o", ORNo);

            MasterDataSet.M_KouseiDataTable dt = new MasterDataSet.M_KouseiDataTable();
            da.Fill(dt);
            return dt;
        }

        public static Error ORHenkou(string seihinCode, string buhinCode, KouseiData data, SqlTransaction sqlTransaction, SqlDataAdapter dataAdapter)
        {
            SqlDataAdapter da = dataAdapter;
            da.SelectCommand.CommandText = "select * from M_Kousei WHERE SeihinCode = @s AND ORNo = @o AND BuhinCode <> @b";
            da.SelectCommand.Parameters.AddWithValue("@o", data.ORNo);
            da.SelectCommand.Parameters.AddWithValue("@s", seihinCode);
            da.SelectCommand.Parameters.AddWithValue("@b", buhinCode);
            da.UpdateCommand = new SqlCommandBuilder(da).GetUpdateCommand();

            MasterDataSet.M_KouseiDataTable dt = new MasterDataSet.M_KouseiDataTable();

            SqlTransaction sqlTran = sqlTransaction;
            try
            {
                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqlTran;
                da.Fill(dt);

                for (int cnt = 0; data.ORBuhin.Count > cnt; cnt++)
                {
                    for (int i = 0; dt.Count > i; i++)
                    {
                        if (dt[i].BuhinCode == data.ORBuhin[cnt])
                        {
                            dt[i].SelectOR = data.ORBuhinBool[cnt];
                        }
                    }
                }

                da.Update(dt);

                return null;
            }
            catch (Exception e)
            {
                if (null != sqlTran)
                    sqlTran.Rollback();
                return new Core.Error(e);
            }
            finally
            {
            }
        }

        //public static Error AddKouseiRireki(KouseiData data, SqlTransaction sqlTransaction, SqlDataAdapter dataAdapter)
        //{
        //    return AddKouseiRireki(null, null, null, null, null, null, data, sqlTransaction, dataAdapter);
        //}

        //public static Error AddKouseiRireki(string seihinCode, string buhinCode, string kouteiCode, string insu, string hinmokuBunruiCode, KouseiData data, SqlTransaction sqlTransaction, SqlDataAdapter dataAdapter)
        //{
        //    return AddKouseiRireki(seihinCode, buhinCode, kouteiCode, insu, hinmokuBunruiCode, null, data, sqlTransaction, dataAdapter);
        //}

        //public static Error AddKouseiRireki(string seihinCode, string buhinCode, string kouteiCode, string insu, string hinmokuBunruiCode, string updateMessage, KouseiData data, SqlTransaction sqlTransaction, SqlDataAdapter dataAdapter)
        //{
        //    SqlDataAdapter da = dataAdapter;
        //    da.SelectCommand.CommandText = "select * from T_KouseiRireki";
        //    da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
        //    MasterDataSet.T_KouseiRirekiDataTable dt = new MasterDataSet.T_KouseiRirekiDataTable();

        //    SqlTransaction sqlTran = sqlTransaction;
        //    try
        //    {
        //        da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqlTran;

        //        DateTime dtNow = DateTime.Now;
        //        MasterDataSet.T_KouseiRirekiRow dr = dt.NewT_KouseiRirekiRow();

        //        string tmpUpdateValue = "";
        //        string tmpBeforeValue = "";
        //        string tmpAfterValue = "";

        //        if (seihinCode == null)
        //        {
        //            tmpUpdateValue = "新規追加";
        //        }
        //        else if (updateMessage != null)
        //        {
        //            tmpUpdateValue = updateMessage;
        //            //                    dr.ItemArray = data.dr.ItemArray;
        //        }
        //        else
        //        {
        //            if (!data.strSeihinCode.Equals(seihinCode))
        //            {
        //                tmpUpdateValue += "製品コード,";
        //                tmpBeforeValue += seihinCode + ",";
        //                tmpAfterValue += data.strSeihinCode + ",";
        //            }
        //            if (!data.strBuhinCode.Equals(buhinCode))
        //            {
        //                tmpUpdateValue += "部品コード,";
        //                tmpBeforeValue += buhinCode + ",";
        //                tmpAfterValue += data.strBuhinCode + ",";
        //            }
        //            if (!data.strKouteiCode.Equals(kouteiCode))
        //            {
        //                tmpUpdateValue += "工程コード,";
        //                tmpBeforeValue += kouteiCode + ",";
        //                tmpAfterValue += data.strKouteiCode + ",";
        //            }
        //            if (!data.strHinmokuBunruiCode.Equals(hinmokuBunruiCode))
        //            {
        //                tmpUpdateValue += "品目分類コード,";
        //                tmpBeforeValue += hinmokuBunruiCode + ",";
        //                tmpAfterValue += data.strHinmokuBunruiCode + ",";
        //            }
        //            if (data.nInsu != decimal.ToInt32(decimal.Parse(insu)))
        //            {
        //                tmpUpdateValue += "員数,";
        //                tmpBeforeValue += insu + ",";
        //                tmpAfterValue += data.nInsu + ",";
        //            }
        //            if (!tmpUpdateValue.Equals(""))
        //            {
        //                tmpUpdateValue = tmpUpdateValue.Substring(0, tmpUpdateValue.Length - 1);
        //                tmpBeforeValue = tmpBeforeValue.Substring(0, tmpBeforeValue.Length - 1);
        //                tmpAfterValue = tmpAfterValue.Substring(0, tmpAfterValue.Length - 1);
        //            }

        //            //                    dr.ItemArray = data.dr.ItemArray;
        //        }

        //        dr.SeihinCode = data.strSeihinCode;
        //        dr.BuhinCode = data.strBuhinCode;
        //        dr.No = 1;
        //        dr.UpdateDate = DateTime.Now;
        //        dr.UpdateValue = tmpUpdateValue;
        //        dr.BeforeValue = tmpBeforeValue;
        //        dr.AfterValue = tmpAfterValue;
        //        dr.Tantou = data.strTantou;
        //        dr.Syounin = data.strSyounin;

        //        dt.Rows.Add(dr);
        //        da.Update(dt);

        //        return null;
        //    }
        //    catch (Exception e)
        //    {
        //        if (null != sqlTran)
        //            sqlTran.Rollback();
        //        return new Core.Error(e);
        //    }
        //    finally
        //    {
        //    }
        //}

        public class SeihinData
        {
            public string strSeihinCode = "";
            public string strKa = "";
            public string strKakari = "";
            public string strSeihinMei = "";
            public string strKokyaku = "";
            public string strSeihinSeries = "";
            public string strZaikoAlarm = "";
            public int nHikiateJuni = 0;
            public string strBuhinZaikoLocation = "";
            public string strKanseiZaikoLocation = "";
            public string strNoukiOffSet = "";
            public bool bGenpinHyo = false;
            public int nIriSu = 0;
            public decimal nTanka = 0;
            public decimal nGaichuTanka = 0;
            public bool bTankaKubun = true;
            public bool bYoteiBunkatsu = false;
            public bool bBumonBunkatsu = false;
            public int nSagyoJun = 0;
            public string bBuhinShukko = "0";
            public string strSeizouPatternCode = "";
            public string strRyouMenA = "";
            public int TensuA = 0;
            public string strRyouMenB = "";
            public int TensuB = 0;
            public int KadouRitsu = 0;
            public int nPriority = 0;
            public string strSeihinBunrui = "";
            public string strBikou = "";
            public string strGaichusakiCode = "";
            public string strKokyakuSeiban = "";
            public string strBunkatuWariTuke = "";
            public int MinLotSuryou = 0;
            public int MinSuryou = 0;
            public string strSagyouKu = "";
            public int OffSet = 0;
            public string strHikitoriID = "";
            public int HikitoriMarumeSu = 0;
            public int GaichuOffsetnisu = 0;
            public string strRyakushou = "";
            public string strBuzaishukomotoLocation = "";
            public string strBuzaishukosakiLocation = "";
            public string strNounyusakiLocation = "";
            public string strShukkoFormat = "";
        }

        public static SeisanDataSet.VIEW_KouseiListDataTable getVIEW_KouseiListDataTable(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM VIEW_KouseiList";

            SeisanDataSet.VIEW_KouseiListDataTable dt = new SeisanDataSet.VIEW_KouseiListDataTable();
            da.Fill(dt);

            return dt;
        }

        //public static MasterDataSet.VIEW_SeihinKouseiCheckDataTable getVIEW_SeihinKouseiCheckDataTable(string str, SqlConnection sqlConnection)
        //{
        //    SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
        //    da.SelectCommand.CommandText = "SELECT * FROM VIEW_SeihinKouseiCheck ";
        //    if (str != "")
        //    {
        //        da.SelectCommand.CommandText += string.Format(" WHERE (SeihinCode + ' : ' + SeihinMei) LIKE '%{0}%'", str);
        //    }

        //    MasterDataSet.VIEW_SeihinKouseiCheckDataTable dt = new MasterDataSet.VIEW_SeihinKouseiCheckDataTable();
        //    da.Fill(dt);

        //    return dt;
        //}

        public static SeisanDataSet.VIEW_KouseiTreeVDataTable getVIEW_KouseiTreeDataTable(SqlConnection sqlConnection, string seihinCode)
        {
            if (seihinCode.Equals("") || seihinCode == null)
            {
                return null;
            }
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM VIEW_KouseiTreeV WHERE SeihinCode = @s";
            da.SelectCommand.Parameters.AddWithValue("@s", seihinCode);

            SeisanDataSet.VIEW_KouseiTreeVDataTable dt = new SeisanDataSet.VIEW_KouseiTreeVDataTable();
            da.Fill(dt);

            return dt;
        }

        public static MasterDataSet.M_BuhinRow getM_BuhinRow(string p, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM M_Buhin WHERE BuhinCode = @SCode ";
            da.SelectCommand.Parameters.AddWithValue("@SCode", p);

            MasterDataSet.M_BuhinDataTable dt = new MasterDataSet.M_BuhinDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return dt[0];
            else
                return null;
        }

        //public static void GetShiyouSeihinDataTable(string strText, bool bFirstMatch, int nStartIndex, int nCount,
        //    SqlConnection sqlConn, out DataTable dt, ref int nTotal)
        //{
        //    Core.Sql.RowNumberInfo info = new Core.Sql.RowNumberInfo();
        //    info.nStartNumber = nStartIndex + 1;
        //    info.nEndNumber = nStartIndex + nCount;
        //    info.strOverText = "ORDER BY ShiyouSeihin ";

        //    SqlCommand cmd = new SqlCommand("", sqlConn);
        //    cmd.CommandText = "SELECT DISTINCT ShiyouSeihin FROM M_Buhin ";
        //    if (strText.Trim() != "")
        //    {
        //        cmd.CommandText += " WHERE ShiyouSeihin LIKE @h ";
        //    }

        //    if (bFirstMatch)
        //        cmd.Parameters.AddWithValue("@h", strText + "%");
        //    else
        //        cmd.Parameters.AddWithValue("@h", "%" + strText + "%");

        //    dt = new DataTable();
        //    info.LoadData(cmd, sqlConn, dt, ref nTotal);
        //}

        public static MasterDataSet.M_BuhinRow getBuhinCode(string p, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM M_Buhin WHERE BuhinCode = @SCode ";
            da.SelectCommand.Parameters.AddWithValue("@SCode", p);

            MasterDataSet.M_BuhinDataTable dt = new MasterDataSet.M_BuhinDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return dt[0];
            else
                return null;
        }

        public class BuhinData
        {
            public MasterDataSet.M_BuhinRow drBuhin =
                new MasterDataSet.M_BuhinDataTable().NewM_BuhinRow();

            public string strBuhinCode = "";
            public string strBuhinKubun = "";
            public string strBuhinMei = "";
            public string strTani = "";
            public int nLT_Suuji = 0;
            public byte nLT_Tani = 0;
            public decimal nTanka = 0;
            public int nLot = 0;
            public string strShiiresakiCode1 = "";
            public string strShiiresakiCode2 = "";
            public int nKanjyouKamokuCode = 0;
            public int nHiyouKamokuCode = 0;
            public int nHojyoKamokuNo = 0;
        }

        public static Error AddBuhin(BuhinData data, SqlConnection sqlConnection)
        {
            SqlDataAdapter daBuhin = new SqlDataAdapter("", sqlConnection);
            daBuhin.SelectCommand.CommandText = "SELECT * FROM M_Buhin";
            daBuhin.InsertCommand = (new SqlCommandBuilder(daBuhin)).GetInsertCommand();
            MasterDataSet.M_BuhinDataTable dtBuhin = new MasterDataSet.M_BuhinDataTable();

            SqlTransaction sqlTran = null;
            try
            {
                sqlConnection.Open();
                sqlTran = sqlConnection.BeginTransaction();

                daBuhin.SelectCommand.Transaction = daBuhin.InsertCommand.Transaction = sqlTran;

                DateTime dtNow = DateTime.Now;

                data.drBuhin.BuhinCode = data.strBuhinCode;
                data.drBuhin.BuhinKubun = data.strBuhinKubun;
                data.drBuhin.BuhinMei = data.strBuhinMei;
                data.drBuhin.Tani = data.strTani;
                data.drBuhin.LT_Suuji = data.nLT_Suuji;
                data.drBuhin.LT_Tani = data.nLT_Tani;
                data.drBuhin.Tanka = data.nTanka;
                data.drBuhin.Lot = data.nLot;
                data.drBuhin.ShiiresakiCode1 = data.strShiiresakiCode1;
                data.drBuhin.ShiiresakiCode2 = data.strShiiresakiCode2;
                data.drBuhin.KanjyouKamokuCode = data.nKanjyouKamokuCode;
                data.drBuhin.HiyouKamokuCode = data.nHiyouKamokuCode;
                data.drBuhin.HojyoKamokuNo = data.nHojyoKamokuNo;

                MasterDataSet.M_BuhinRow drBuhin = dtBuhin.NewM_BuhinRow();
                drBuhin.ItemArray = data.drBuhin.ItemArray;

                dtBuhin.Rows.Add(drBuhin);
                daBuhin.Update(dtBuhin);

                sqlTran.Commit();

                return null;
            }
            catch (Exception e)
            {
                if (null != sqlTran)
                    sqlTran.Rollback();
                return new Core.Error(e);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public static Error AddBuhincsv(MasterDataSet.M_BuhinRow dr, SqlConnection sqlConnection)
        {
            SqlDataAdapter daBuhin = new SqlDataAdapter("", sqlConnection);
            daBuhin.SelectCommand.CommandText = "select * from M_Buhin";
            daBuhin.InsertCommand = (new SqlCommandBuilder(daBuhin)).GetInsertCommand();
            MasterDataSet.M_BuhinDataTable dtBuhin = new MasterDataSet.M_BuhinDataTable();

            SqlTransaction sqlTran = null;
            try
            {
                sqlConnection.Open();
                sqlTran = sqlConnection.BeginTransaction();

                daBuhin.SelectCommand.Transaction = daBuhin.InsertCommand.Transaction = sqlTran;

                DateTime dtNow = DateTime.Now;

                MasterDataSet.M_BuhinRow drBuhin = dtBuhin.NewM_BuhinRow();

                drBuhin.ItemArray = dr.ItemArray;
                dtBuhin.Rows.Add(drBuhin);
                daBuhin.Update(dtBuhin);

                sqlTran.Commit();

                return null;
            }
            catch (Exception e)
            {
                if (null != sqlTran)
                    sqlTran.Rollback();
                return new Core.Error(e);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public static Error UpdateBuhin(string strBuhinCode, BuhinData data, SqlConnection sqlConnection)
        {
            //string strKoushinSha = data.strSakuseiSha;

            SqlDataAdapter daBuhin = new SqlDataAdapter("", sqlConnection);
            daBuhin.SelectCommand.CommandText = "select * from M_Buhin where BuhinCode = @bc";
            daBuhin.SelectCommand.Parameters.AddWithValue("@bc", strBuhinCode);
            daBuhin.UpdateCommand = (new SqlCommandBuilder(daBuhin)).GetUpdateCommand();
            MasterDataSet.M_BuhinDataTable dtBuhin = new MasterDataSet.M_BuhinDataTable();
            SqlTransaction sqlTran = null;

            try
            {
                sqlConnection.Open();
                sqlTran = sqlConnection.BeginTransaction();

                daBuhin.SelectCommand.Transaction = daBuhin.UpdateCommand.Transaction = sqlTran;

                daBuhin.Fill(dtBuhin);

                MasterDataSet.M_BuhinRow drBuhin = dtBuhin[0];

                DateTime dtNow = DateTime.Now;

                //情報更新
                data.drBuhin.BuhinCode = data.strBuhinCode;
                data.drBuhin.BuhinKubun = data.strBuhinKubun;
                data.drBuhin.BuhinMei = data.strBuhinMei;
                data.drBuhin.Tani = data.strTani;
                data.drBuhin.LT_Suuji = data.nLT_Suuji;
                data.drBuhin.LT_Tani = data.nLT_Tani;
                data.drBuhin.Tanka = data.nTanka;
                data.drBuhin.Lot = data.nLot;
                data.drBuhin.ShiiresakiCode1 = data.strShiiresakiCode1;
                data.drBuhin.ShiiresakiCode2 = data.strShiiresakiCode2;
                data.drBuhin.KanjyouKamokuCode = data.nKanjyouKamokuCode;
                data.drBuhin.HiyouKamokuCode = data.nHiyouKamokuCode;
                data.drBuhin.HojyoKamokuNo = data.nHojyoKamokuNo;

                daBuhin.Update(dtBuhin);

                daBuhin.Fill(dtBuhin);
                sqlTran.Commit();
                sqlTran.Dispose();
                sqlTran = null;

                return null;
            }
            catch (Exception e)
            {
                if (null != sqlTran)
                    sqlTran.Rollback();
                return new Core.Error(e);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static MasterDataSet.M_KouseiDataTable getM_KouseiDataTable(string sSeihinCode, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);

            da.SelectCommand.CommandText = "SELECT * FROM M_Kousei WHERE SeihinCode = @s";

            da.SelectCommand.Parameters.AddWithValue("@s", sSeihinCode);

            var dt = new MasterDataSet.M_KouseiDataTable();

            da.Fill(dt);

            return dt;
        }

        public static MasterDataSet.M_KouseiDataTable getM_KouseiDataTable_Buhin(string sBuhinCode, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM M_Kousei WHERE BuhinCode = @b ORDER BY SeihinCode ";
            da.SelectCommand.Parameters.AddWithValue("@b", sBuhinCode);

            MasterDataSet.M_KouseiDataTable dt = new MasterDataSet.M_KouseiDataTable();
            da.Fill(dt);

            return dt;
        }

        public class ParamKouseiTree
        {
            public string topPartsCode = "";
            public string oyaPartsCode = null;
            public string partsCode = null;
            public bool isOyaKensaku = false;
            public bool isKeyPartsOnly = false;
            public string userId = null;

            public string GetWhereText(SqlCommand cmd, ParamKouseiTree p)
            {
                Core.Sql.WhereGenerator w = new Core.Sql.WhereGenerator();

                if (p.topPartsCode != null)
                {
                    w.Add(string.Format("SeihinCode = '{0}'", p.topPartsCode));
                    //cmd.Parameters.AddWithValue("@topPartsCode", p.topPartsCode);
                }

                return w.WhereText;
            }
        }
        public static SeisanDataSet.VIEW_KouseiTreeVDataTable GetVIEW_KouseiTreeVDataTable(ParamKouseiTree p, SqlConnection sqlCon)
        {
            string strSeiban = string.Empty;
            string strItems = string.Empty;
            if (p.topPartsCode != null)
            {
                strSeiban = p.topPartsCode;
            }
            else
            {
                return null;
            }

            MasterDataSet.M_KouseiDataTable dtK = MasterClass.getM_KouseiDataTable(strSeiban, sqlCon);

            strItems = "'" + strSeiban + "'";
            for (int i = 0; i < dtK.Count; i++)
            {
                MasterDataSet.M_KouseiRow drK = dtK[i];
                strItems += ",'" + drK.BuhinCode + "'";
            }

            string sql = string.Format("SELECT * FROM VIEW_KouseiTreeV");

            SqlDataAdapter da = new SqlDataAdapter(sql, sqlCon);
            string whereText = p.GetWhereText(da.SelectCommand, p);
            if (whereText != "")
            {
                da.SelectCommand.CommandText += string.Format(" WHERE {0} ", whereText);
            }
            SeisanDataSet.VIEW_KouseiTreeVDataTable dt = new SeisanDataSet.VIEW_KouseiTreeVDataTable();

            da.SelectCommand.CommandTimeout = 150;
            da.Fill(dt);
            return dt;
        }

        public static SeisanDataSet.VIEW_KouseiTreeVDataTable GetVIEW_KouseiTreeDataTable(ParamKouseiTree p, SqlConnection sqlCon)
        {
            string strSeiban = string.Empty;
            string strItems = string.Empty;
            if (p.topPartsCode != null)
            {
                strSeiban = p.topPartsCode;
            }
            else 
            {
                return null;
            }

            MasterDataSet.M_KouseiDataTable dtK = MasterClass.getM_KouseiDataTable(strSeiban, sqlCon);

            strItems = "'" + strSeiban + "'";
            for (int i = 0; i < dtK.Count; i++)
            {
                MasterDataSet.M_KouseiRow drK = dtK[i];
                strItems += ",'" + drK.BuhinCode + "'";
            }

            string sql = string.Format(
            @"WITH KouseiTree(SeihinCode, BuhinCode, KouteiCode, KouteiMei, HinmokuBunruiCode, HinmokuBunrui, HinmokuCode, Hinmei, Insu, UpdateValue, UpdateDate, BeforeValue, AfterValue, Tantou, Syounin,SelectOR,No,ORNo,MakerCode,TooshiNo,Bikou1,Bikou2,HyoujiSeihinCode)
            AS(
                SELECT DISTINCT
                    SeihinCode,
                    SeihinCode AS Expr1,
                    CAST('' as nvarchar(50)) AS Expr9,
                    CAST('' as nvarchar(200)) AS Expr14,
                    CAST('' as nvarchar(50)) AS Expr13,
                    CAST('' as nvarchar(50)) AS Expr10,
                    CAST('' as nvarchar(50)) AS Expr11,
                    CAST('' as nvarchar(50)) AS Expr12,
                    CAST('1' as decimal(18, 4)) AS Expr2,
                    CAST('' as nvarchar(50)) AS Expr3,
                    CAST(0 as datetime) AS Expr4,
                    CAST('' as nvarchar(50)) AS Expr5,
                    CAST('' as nvarchar(50)) AS Expr6,
                    CAST('' as nvarchar(50)) AS Expr7,
                    CAST('' as nvarchar(50)) AS Expr8,
                    CONVERT(nvarchar, '') AS SelectOR,
                    0,
                    CONVERT(nvarchar, ''),
                    CAST('' as nvarchar(50)),
                    CAST('' as nvarchar(3)),
                    CAST('' as nvarchar(50)),
                    CAST('' as nvarchar(50)),
                    SeihinCode as HyoujiSeihinCode
                FROM
                    dbo.M_Seihin
                WHERE SeihinCode IN(" + strItems + @")
                UNION ALL
                SELECT
                    kousei.SeihinCode,
                    kousei.BuhinCode,
                    kousei.KouteiCode,
                    kousei.KouteiMei,
                    kousei.HinmokuBunruiCode,
                    kousei.HinmokuBunrui,
                    kousei.HinmokuCode,
                    kousei.Hinmei,
                    kousei.Insu,
                    kousei.UpdateValue,
                    CAST(kousei.TourokuDateTime as datetime),
                    kousei.BeforeValue,
                    kousei.AfterValue,
                    kousei.Tantou,
                    kousei.Syounin,
                    CONVERT(nvarchar, CASE kousei.SelectOR WHEN 1 THEN '選択' ELSE '' END) AS SelectOR,
                    kousei.No,
                    CONVERT(nvarchar, Kousei.ORNo),
                    Kousei.MakerCode,
                    kousei.TooshiNo,
                    kousei.Bikou1,
                    kousei.Bikou2,
                    kousei.HyoujiSeihinCode
                FROM
                    dbo.VIEW_KouseiRireki as kousei
                    INNER JOIN
                        KouseiTree AS KouseiTree_2
                    ON  kousei.SeihinCode = KouseiTree_2.BuhinCode
                WHERE
                    (
                        kousei.SeihinCode = @topPartsCode
                    )
            )
            SELECT
                KouseiTree_1.SeihinCode,
                KouseiTree_1.BuhinCode,
                KouseiTree_1.KouteiCode,
                KouseiTree_1.KouteiMei,
                KouseiTree_1.HinmokuBunruiCode,
                KouseiTree_1.HinmokuBunrui,
                KouseiTree_1.HinmokuCode,
                KouseiTree_1.Hinmei,
                KouseiTree_1.Insu,
                dbo.M_Buhin.TorihikisakiCode,
                dbo.M_Buhin.TorihikisakiName,
                dbo.M_Buhin.MarumeSu,
                dbo.M_Buhin.BuhinName,
                KouseiTree_1.UpdateValue,
                KouseiTree_1.UpdateDate,
                KouseiTree_1.BeforeValue,
                KouseiTree_1.AfterValue,
                KouseiTree_1.Tantou,
                KouseiTree_1.Syounin,
                KouseiTree_1.SelectOR,
                KouseiTree_1.No,
                KouseiTree_1.ORNo,
                KouseiTree_1.MakerCode,
                KouseiTree_1.TooshiNo,
                KouseiTree_1.Bikou1,
                KouseiTree_1.Bikou2,
                KouseiTree_1.HyoujiSeihinCode
            FROM
                KouseiTree AS KouseiTree_1
                LEFT OUTER JOIN
                    dbo.M_Buhin
                ON  KouseiTree_1.BuhinCode = dbo.M_Buhin.BuhinCode
            WHERE
                KouseiTree_1.BuhinCode != @topPartsCode
            ORDER BY
                KouseiTree_1.No
             ");

            SqlDataAdapter da = new SqlDataAdapter(sql, sqlCon);
            string whereText = p.GetWhereText(da.SelectCommand, p);
            if (whereText != "")
            {
                da.SelectCommand.CommandText += string.Format(" WHERE {0} ", whereText);
            }
            SeisanDataSet.VIEW_KouseiTreeVDataTable dt = new SeisanDataSet.VIEW_KouseiTreeVDataTable();

            da.SelectCommand.CommandTimeout = 150;
            da.Fill(dt);
            return dt;
        }

        public static SeisanDataSet.VIEW_KouseiTreeVDataTable GetVIEW_KouseiTreeDataTableOrg(ParamKouseiTree p, SqlConnection sqlCon)
        {
            string sql = string.Format(
                            @"
WITH KouseiTree(SeihinCode, BuhinCode, KouteiCode, KouteiMei, HinmokuBunruiCode, HinmokuBunrui, HinmokuCode, Hinmei, Insu, UpdateValue, UpdateDate, BeforeValue, AfterValue, Tantou, Syounin,SelectOR,No,ORNo,MakerCode,TooshiNo,Bikou1,Bikou2,HyoujiSeihinCode) AS
                  (SELECT
                  SeihinCode,
                  SeihinCode AS Expr1,
                  CAST('' as nvarchar(50)) AS Expr9,
                   CAST('' as nvarchar(200)) AS Expr14,
                    CAST('' as nvarchar(50)) AS Expr13,
                    CAST('' as nvarchar(50)) AS Expr10,
                     CAST('' as nvarchar(50)) AS Expr11,
                      CAST('' as nvarchar(50)) AS Expr12,
                       CAST('1' as decimal(18,4)) AS Expr2,    
                        CAST('' as nvarchar(50)) AS Expr3,
                         CAST(0 as datetime) AS Expr4,
                          CAST('' as nvarchar(50)) AS Expr5,
                           CAST('' as nvarchar(50)) AS Expr6,
                            CAST('' as nvarchar(50)) AS Expr7,
                             CAST('' as nvarchar(50)) AS Expr8,
                             CONVERT(nvarchar,'') AS SelectOR,
                             0,CONVERT(nvarchar,''),CAST('' as nvarchar(50)),CAST('' as nvarchar(3)),CAST('' as nvarchar(50)),CAST('' as nvarchar(50)),SeihinCode as HyoujiSeihinCode
                    FROM	dbo.M_Seihin
                    WHERE SeihinCode = @topPartsCode
                    UNION ALL
                    SELECT	kousei.SeihinCode, kousei.BuhinCode, kousei.KouteiCode, kousei.KouteiMei, kousei.HinmokuBunruiCode, kousei.HinmokuBunrui, kousei.HinmokuCode, kousei.Hinmei, kousei.Insu, kousei.UpdateValue, CAST(kousei.TourokuDateTime as datetime), kousei.BeforeValue, kousei.AfterValue, kousei.Tantou, kousei.Syounin,CONVERT(nvarchar,CASE kousei.SelectOR WHEN 1 THEN '選択' ELSE '' END) AS SelectOR,kousei.No,CONVERT(nvarchar,Kousei.ORNo),Kousei.MakerCode
                            , kousei.TooshiNo, kousei.Bikou1,kousei.Bikou2,kousei.HyoujiSeihinCode 
                    FROM	dbo.VIEW_KouseiRireki as kousei INNER JOIN
                        KouseiTree AS KouseiTree_2 ON kousei.SeihinCode = KouseiTree_2.BuhinCode WHERE (kousei.SeihinCode = @topPartsCode))
                SELECT	KouseiTree_1.SeihinCode, KouseiTree_1.BuhinCode, KouseiTree_1.KouteiCode, KouseiTree_1.KouteiMei, KouseiTree_1.HinmokuBunruiCode, KouseiTree_1.HinmokuBunrui, KouseiTree_1.HinmokuCode, KouseiTree_1.Hinmei, KouseiTree_1.Insu, dbo.M_Buhin.TorihikisakiCode, dbo.M_Buhin.TorihikisakiName,
                    dbo.M_Buhin.MarumeSu,dbo.M_Buhin.BuhinName, KouseiTree_1.UpdateValue, KouseiTree_1.UpdateDate, KouseiTree_1.BeforeValue, KouseiTree_1.AfterValue, KouseiTree_1.Tantou, KouseiTree_1.Syounin,KouseiTree_1.SelectOR,KouseiTree_1.No,KouseiTree_1.ORNo,KouseiTree_1.MakerCode,KouseiTree_1.TooshiNo,KouseiTree_1.Bikou1,KouseiTree_1.Bikou2,KouseiTree_1.HyoujiSeihinCode
                FROM	KouseiTree AS KouseiTree_1 LEFT OUTER JOIN
                    dbo.M_Buhin ON KouseiTree_1.BuhinCode = dbo.M_Buhin.BuhinCode
                WHERE KouseiTree_1.BuhinCode != @topPartsCode
ORDER BY KouseiTree_1.No

             ");

            SqlDataAdapter da = new SqlDataAdapter(sql, sqlCon);
            string whereText = p.GetWhereText(da.SelectCommand, p);
            if (whereText != "")
            {
                da.SelectCommand.CommandText += string.Format(" WHERE {0} ", whereText);
            }
            SeisanDataSet.VIEW_KouseiTreeVDataTable dt = new SeisanDataSet.VIEW_KouseiTreeVDataTable();

            da.SelectCommand.CommandTimeout = 150;
            da.Fill(dt);
            return dt;
        }

        public static void getVIEW_KouseiListDataTable(string strText, bool bFirstMatch, int nStartIndex, int nCount,
            SqlConnection sqlConn, out SeisanDataSet.VIEW_KouseiListDataTable dt, ref int nTotal)
        {
            Core.Sql.RowNumberInfo info = new Core.Sql.RowNumberInfo();
            info.nStartNumber = nStartIndex + 1;
            info.nEndNumber = nStartIndex + nCount;
            info.strOverText = "ORDER BY SeihinCode ";

            SqlCommand cmd = new SqlCommand("", sqlConn);
            cmd.CommandText = "select * from VIEW_KouseiList ";
            if (strText.Trim() != "")
            {
                cmd.CommandText += " where SeihinCode like @h OR SeihinMei like @h ";
            }

            if (bFirstMatch)
                cmd.Parameters.AddWithValue("@h", strText + "%");
            else
                cmd.Parameters.AddWithValue("@h", "%" + strText + "%");

            dt = new SeisanDataSet.VIEW_KouseiListDataTable();
            info.LoadData(cmd, sqlConn, dt, ref nTotal);
        }
        public class KouseiData
        {
            public MasterDataSet.M_KouseiRow dr = new MasterDataSet.M_KouseiDataTable().NewM_KouseiRow();

            public string strSeihinCode = "";
            public string strBuhinCode = "";
            public string strKouteiCode = "";
            public string strHinmokuBunruiCode = "";
            public int nInsu = 0;
            public string strSyounin = "";
            public string strTantou = "";
            public DateTime? dTekiyouKaishiBi = null;
            public DateTime? dTekiyouShuryoBi = null;

            public string ORNo = "";
            public bool bOR = false;

            public List<string> ORBuhin = new List<string>();
            public List<bool> ORBuhinBool = new List<bool>();

            public string strBikou1 = "";
            public string strBikou2 = "";
        }

        public static Error AddKousei(KouseiData data, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_Kousei";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            MasterDataSet.M_KouseiDataTable dt = new MasterDataSet.M_KouseiDataTable();

            SqlTransaction sqlTran = null;
            try
            {
                sqlConnection.Open();
                sqlTran = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqlTran;

                DateTime dtNow = DateTime.Now;

                data.dr.SeihinCode = data.strSeihinCode;
                data.dr.BuhinCode = data.strBuhinCode;
                data.dr.KouteiCode = data.strKouteiCode;
                data.dr.HinmokuBunruiCode = data.strHinmokuBunruiCode;
                data.dr.Insu = data.nInsu;
                data.dr.DeleteFlag = false;
                MasterDataSet.M_KouseiRow dr = dt.NewM_KouseiRow();
                dr.ItemArray = data.dr.ItemArray;
                dr.TourokuDateTime = dtNow;

                dt.Rows.Add(dr);
                da.Update(dt);

                //MasterClass.AddKouseiRireki(data, sqlTran, da);

                sqlTran.Commit();

                return null;
            }
            catch (Exception e)
            {
                if (null != sqlTran)
                    sqlTran.Rollback();
                return new Core.Error(e);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static Error Del_and_AddKousei(MasterDataSet.M_KouseiDataTable dtMain, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_Kousei";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();

            SqlCommand cmdDelKouseiData = new SqlCommand("", sqlConnection);
            cmdDelKouseiData.CommandText = @"DELETE FROM M_Kousei where SeihinCode = @s ";
            cmdDelKouseiData.Parameters.AddWithValue("@s", dtMain[0].SeihinCode);

            MasterDataSet.M_KouseiDataTable dt = new MasterDataSet.M_KouseiDataTable();

            SqlTransaction sqlTran = null;
            try
            {
                sqlConnection.Open();
                sqlTran = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqlTran;
                cmdDelKouseiData.Transaction = sqlTran;

                DateTime dtNow = DateTime.Now;

                for (int i = 0; dtMain.Rows.Count > i; i++)
                {
                    MasterDataSet.M_KouseiRow dr = dt.NewM_KouseiRow();
                    dr.ItemArray = dtMain[i].ItemArray;
                    dt.Rows.Add(dr);
                }

                cmdDelKouseiData.ExecuteNonQuery();
                da.Update(dt);

                sqlTran.Commit();

                return null;
            }
            catch (Exception e)
            {
                if (null != sqlTran)
                    sqlTran.Rollback();
                return new Core.Error(e);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static Error UpdateKousei(string seihinCode, string buhinCode, string kouteiCode, string insu, string hinmokuBunruiCode, KouseiData data, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_Kousei where SeihinCode = @s and BuhinCode = @b";
            da.SelectCommand.Parameters.AddWithValue("@s", seihinCode);
            da.SelectCommand.Parameters.AddWithValue("@b", buhinCode);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            MasterDataSet.M_KouseiDataTable dt = new MasterDataSet.M_KouseiDataTable();
            SqlTransaction sqlTran = null;

            try
            {
                sqlConnection.Open();
                sqlTran = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqlTran;

                da.Fill(dt);

                MasterDataSet.M_KouseiRow dr = dt[0];

                DateTime dtNow = DateTime.Now;

                //情報更新

                dr.SeihinCode = data.strSeihinCode;
                dr.BuhinCode = data.strBuhinCode;
                dr.KouteiCode = data.strKouteiCode;
                dr.Insu = data.nInsu;
                dr.HinmokuBunruiCode = data.strHinmokuBunruiCode;
                dr.SelectOR = data.bOR;
                dr.Bikou1 = data.strBikou1;
                dr.Bikou2 = data.strBikou2;
                dr.TourokuDateTime = DateTime.Now;
                da.Update(dt);

                da.Fill(dt);

               
                sqlTran.Commit();
                sqlTran.Dispose();
                sqlTran = null;

                return null;
            }
            catch (Exception e)
            {
                if (null != sqlTran)
                    sqlTran.Rollback();
                return new Core.Error(e);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static Error LogicalDeleteKousei(string seihinCode, string buhinCode, string insu, string hinmokuBunruiCode, KouseiData data, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_Kousei where SeihinCode = @s and BuhinCode = @b";
            da.SelectCommand.Parameters.AddWithValue("@s", seihinCode);
            da.SelectCommand.Parameters.AddWithValue("@b", buhinCode);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            MasterDataSet.M_KouseiDataTable dt = new MasterDataSet.M_KouseiDataTable();
            SqlTransaction sqlTran = null;

            try
            {
                sqlConnection.Open();
                sqlTran = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqlTran;

                da.Fill(dt);

                MasterDataSet.M_KouseiRow dr = dt[0];

                DateTime dtNow = DateTime.Now;

                //情報更新

                dr.SeihinCode = data.strSeihinCode;
                dr.BuhinCode = data.strBuhinCode;
                dr.KouteiCode = data.strKouteiCode;
                dr.Insu = data.nInsu;
                dr.HinmokuBunruiCode = data.strHinmokuBunruiCode;
                dr.DeleteFlag = !dr.DeleteFlag;
                string updateMessage = dr.DeleteFlag ? "レコード削除" : "レコード復帰";

                da.Update(dt);

                da.Fill(dt);

                // 履歴保持
                ////MasterClass.AddKouseiRireki(seihinCode, buhinCode, null, insu, hinmokuBunruiCode, updateMessage, data, sqlTran, da);

                sqlTran.Commit();
                sqlTran.Dispose();
                sqlTran = null;

                return null;
            }
            catch (Exception e)
            {
                if (null != sqlTran)
                    sqlTran.Rollback();
                return new Core.Error(e);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static Core.Error DeleteKousei(string sSeihinCode, string sBuhinCode, SqlConnection sqlConn)
        {
            //製品
            SqlCommand cmdGaichu = new SqlCommand("", sqlConn);
            cmdGaichu.CommandText = "DELETE M_Kousei WHERE SeihinCode = @sGaichusakiCode AND BuhinCode = @sBuhinCode ";
            cmdGaichu.Parameters.AddWithValue("@sSeihinCode", sSeihinCode);
            cmdGaichu.Parameters.AddWithValue("@sBuhinCode", sBuhinCode);

            SqlTransaction t = null;
            try
            {
                sqlConn.Open();
                t = sqlConn.BeginTransaction();
                cmdGaichu.Transaction = t;

                cmdGaichu.ExecuteNonQuery();

                t.Commit();

                return null;
            }
            catch (Exception e)
            {
                return new Core.Error(e);
            }
            finally
            {
                sqlConn.Close();
            }
        }

        public static MasterDataSet.VIEW_TantoushaRow getVIEW_TantoushaRow(string code, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM VIEW_Tantousha WHERE UserID = @c ";
            da.SelectCommand.Parameters.AddWithValue("@c", code);

            MasterDataSet.VIEW_TantoushaDataTable dt = new MasterDataSet.VIEW_TantoushaDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return dt[0];
            else
                return null;
        }

        public class TokuisakiData
        {
            public MasterDataSet.M_ShiiresakiRow dr = new MasterDataSet.M_ShiiresakiDataTable().NewM_ShiiresakiRow();

            public string strShiiresakiCode = string.Empty;
            public string strShiiresakiMei = string.Empty;
            public string strYubinBangou = string.Empty;
            public string strAddress = string.Empty;
            public string strTel = string.Empty;
            public string strFax = string.Empty;
            public int intShiharaiShimebi = 0;
            public int intShiharaiYoteibi = 0;
            public bool bKensyukoukaiFlg = false;
            public bool bSaisokuMailFlg = true;
            public bool bKousinKyokaFlg = true;
            public string strKouzaMeigi = string.Empty;
            public string strKinyuuKikanMei = string.Empty;
            public string strKouzaBangou = string.Empty;
            public bool bInvoiceRegFlg = true;
            public string strInvoiceRegNo = string.Empty;
        }

        public static Error AddTokuisaki(TokuisakiData data, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_Shiiresaki";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            MasterDataSet.M_ShiiresakiDataTable dt = new MasterDataSet.M_ShiiresakiDataTable();

            SqlTransaction sqlTran = null;
            try
            {
                sqlConnection.Open();
                sqlTran = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqlTran;

                DateTime dtNow = DateTime.Now;
                data.dr.ShiiresakiCode = data.strShiiresakiCode;
                data.dr.ShiiresakiMei = data.strShiiresakiMei;
                data.dr.YubinBangou = data.strYubinBangou;
                data.dr.Address = data.strAddress;
                data.dr.Tel = data.strTel;
                data.dr.Fax = data.strFax;
                data.dr.ShiharaiShimebi = data.intShiharaiShimebi;
                data.dr.ShiharaiYoteibi = data.intShiharaiYoteibi;
                data.dr.KensyukoukaiFlg = data.bKensyukoukaiFlg;
                data.dr.SaisokuMailFlg = data.bSaisokuMailFlg;
                data.dr.KousinKyokaFlg = data.bKousinKyokaFlg;
                data.dr.KouzaMeigi = data.strKouzaMeigi;
                data.dr.KinyuuKikanMei = data.strKinyuuKikanMei;
                data.dr.KouzaBangou = data.strKouzaBangou;
                data.dr.InvoiceRegFlg = data.bInvoiceRegFlg;
                data.dr.InvoiceRegNo = data.strInvoiceRegNo;

                MasterDataSet.M_ShiiresakiRow dr = dt.NewM_ShiiresakiRow();
                dr.ItemArray = data.dr.ItemArray;

                dt.Rows.Add(dr);
                da.Update(dt);

                sqlTran.Commit();

                return null;
            }
            catch (Exception e)
            {
                if (null != sqlTran)
                    sqlTran.Rollback();
                return new Core.Error(e);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static Error UpdateTokuisaki(string code, TokuisakiData data, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_Shiiresaki where TorihikisakiCode = @c";

            da.SelectCommand.Parameters.AddWithValue("@c", code);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            MasterDataSet.M_ShiiresakiDataTable dt = new MasterDataSet.M_ShiiresakiDataTable();
            SqlTransaction sqlTran = null;

            try
            {
                sqlConnection.Open();
                sqlTran = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqlTran;

                da.Fill(dt);

                MasterDataSet.M_ShiiresakiRow dr = dt[0];

                dr.ShiiresakiMei = data.strShiiresakiMei;
                dr.YubinBangou = data.strYubinBangou;
                dr.Address = data.strAddress;
                dr.Tel = data.strTel;
                dr.Fax = data.strFax;
                dr.ShiharaiShimebi = data.intShiharaiShimebi;
                dr.ShiharaiYoteibi = data.intShiharaiYoteibi;
                dr.KensyukoukaiFlg = data.bKensyukoukaiFlg;
                dr.SaisokuMailFlg = data.bSaisokuMailFlg;
                dr.KousinKyokaFlg = data.bKousinKyokaFlg;
                dr.KouzaMeigi = data.strKouzaMeigi;
                dr.KinyuuKikanMei = data.strKinyuuKikanMei;
                dr.KouzaBangou = data.strKouzaBangou;
                dr.InvoiceRegFlg = data.bInvoiceRegFlg;
                dr.InvoiceRegNo = data.strInvoiceRegNo;

                da.Update(dt);

                da.Fill(dt);

                // 履歴保持
                //SetBuhinHenkouRireki(dtNow, data.strSakuseiSha, strBuhinCode, strBuhinCode2, sqlTran);

                sqlTran.Commit();
                sqlTran.Dispose();
                sqlTran = null;

                return null;
            }
            catch (Exception e)
            {
                if (null != sqlTran)
                    sqlTran.Rollback();
                return new Core.Error(e);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static MasterDataSet.M_ShiiresakiRow getM_ShiiresakiRow(string v, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM M_Shiiresaki WHERE ShiiresakiCode = @s1";
            da.SelectCommand.Parameters.AddWithValue("@s1", v);

            MasterDataSet.M_ShiiresakiDataTable dt = new MasterDataSet.M_ShiiresakiDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return dt[0];
            else
                return null;
        }

        public static Core.Error DeleteUser(string sLoginID, SqlConnection sqlConn)
        {
            //製品
            SqlCommand cmdUser = new SqlCommand("", sqlConn);
            cmdUser.CommandText = "DELETE M_User WHERE LoginID = @LoginID";
            cmdUser.Parameters.AddWithValue("@LoginID", sLoginID);

            //製品工程
            SqlCommand cmdTantousha = new SqlCommand("", sqlConn);
            cmdTantousha.CommandText = "DELETE M_Tantousha WHERE UserID = @UserID";
            cmdTantousha.Parameters.AddWithValue("@UserID", sLoginID);

            SqlTransaction t = null;
            try
            {
                sqlConn.Open();
                t = sqlConn.BeginTransaction();
                cmdUser.Transaction =
                cmdTantousha.Transaction
                = t;

                cmdUser.ExecuteNonQuery();
                cmdTantousha.ExecuteNonQuery();

                t.Commit();

                return null;
            }
            catch (Exception e)
            {
                return new Core.Error(e);
            }
            finally
            {
                sqlConn.Close();
            }
        }

        public static Core.Error DeleteShiiresaki(string sShiiresakiCode, SqlConnection sqlConn)
        {
            //製品
            SqlCommand cmdTorihikisaki = new SqlCommand("", sqlConn);
            cmdTorihikisaki.CommandText = "DELETE M_Shiiresaki WHERE ShiiresakiCode = @sShiiresakiCode ";
            cmdTorihikisaki.Parameters.AddWithValue("@sShiiresakiCodeCode", sShiiresakiCode);

            SqlTransaction t = null;
            try
            {
                sqlConn.Open();
                t = sqlConn.BeginTransaction();
                cmdTorihikisaki.Transaction = t;

                cmdTorihikisaki.ExecuteNonQuery();

                t.Commit();

                return null;
            }
            catch (Exception e)
            {
                return new Core.Error(e);
            }
            finally
            {
                sqlConn.Close();
            }
        }

        public static Core.Error DeleteBuhin(string sBuhinCode, SqlConnection sqlConn)
        {
            //製品
            SqlCommand cmdBuhin = new SqlCommand("", sqlConn);
            cmdBuhin.CommandText = "DELETE M_Buhin WHERE BuhinCode = @sBuhinCode ";
            cmdBuhin.Parameters.AddWithValue("@sBuhinCode", sBuhinCode);

            SqlTransaction t = null;
            try
            {
                sqlConn.Open();
                t = sqlConn.BeginTransaction();
                cmdBuhin.Transaction = t;

                cmdBuhin.ExecuteNonQuery();

                t.Commit();

                return null;
            }
            catch (Exception e)
            {
                return new Core.Error(e);
            }
            finally
            {
                sqlConn.Close();
            }
        }

        public static Core.Error DeleteKoutei(string sKouteiCode, SqlConnection sqlConn)
        {
            //製品
            SqlCommand cmdKoutei = new SqlCommand("", sqlConn);
            cmdKoutei.CommandText = "DELETE M_Koutei WHERE KouteiCode = @sKouteiCode ";
            cmdKoutei.Parameters.AddWithValue("@sKouteiCode", sKouteiCode);

            SqlTransaction t = null;
            try
            {
                sqlConn.Open();
                t = sqlConn.BeginTransaction();
                cmdKoutei.Transaction = t;

                cmdKoutei.ExecuteNonQuery();

                t.Commit();

                return null;
            }
            catch (Exception e)
            {
                return new Core.Error(e);
            }
            finally
            {
                sqlConn.Close();
            }
        }

        public static Core.Error DeleteShift(string value, SqlConnection sqlConn)
        {
            string[] str = value.Split(',');
            string sSeisanShiftCode = str[0];
            string nSeisanShiftNo = str[1];
            //製品
            SqlCommand cmdShift = new SqlCommand("", sqlConn);
            cmdShift.CommandText = "DELETE M_Shift WHERE SeisanShiftCode = @sSeisanShiftCode AND SeisanShiftNo = @nSeisanShiftNo";
            cmdShift.Parameters.AddWithValue("@sSeisanShiftCode", sSeisanShiftCode);
            cmdShift.Parameters.AddWithValue("@nSeisanShiftNo", nSeisanShiftNo);

            SqlTransaction t = null;
            try
            {
                sqlConn.Open();
                t = sqlConn.BeginTransaction();
                cmdShift.Transaction = t;

                cmdShift.ExecuteNonQuery();

                t.Commit();

                return null;
            }
            catch (Exception e)
            {
                return new Core.Error(e);
            }
            finally
            {
                sqlConn.Close();
            }
        }

        public static Core.Error DeleteZaikosakiLocation(string LocationCode, string vsHokanCode,  SqlConnection sqlConn)
        {
            //製品
            SqlCommand cmdZaikosakiLocation = new SqlCommand("", sqlConn);
            cmdZaikosakiLocation.CommandText = "DELETE M_Location WHERE LocationCode =@LocationCode";
            cmdZaikosakiLocation.Parameters.AddWithValue("@LocationCode", LocationCode);
           

            SqlTransaction t = null;
            try
            {
                sqlConn.Open();
                t = sqlConn.BeginTransaction();
                cmdZaikosakiLocation.Transaction = t;

                cmdZaikosakiLocation.ExecuteNonQuery();

                t.Commit();

                return null;
            }
            catch (Exception e)
            {
                return new Core.Error(e);
            }
            finally
            {
                sqlConn.Close();
            }
        }

        public static Core.Error DeleteM_Kousei(string sHinmokuCode, SqlConnection sqlConn)
        {
            //製品
            SqlCommand cmdHinmokuBunrui = new SqlCommand("", sqlConn);
            cmdHinmokuBunrui.CommandText = "DELETE M_Kousei WHERE SeihinCode = @SeihinCode";
            cmdHinmokuBunrui.Parameters.AddWithValue("@SeihinCode", sHinmokuCode);

            SqlTransaction t = null;
            try
            {
                sqlConn.Open();
                t = sqlConn.BeginTransaction();
                cmdHinmokuBunrui.Transaction = t;

                cmdHinmokuBunrui.ExecuteNonQuery();

                t.Commit();

                return null;
            }
            catch (Exception e)
            {
                return new Core.Error(e);
            }
            finally
            {
                sqlConn.Close();
            }
        }









    }


}