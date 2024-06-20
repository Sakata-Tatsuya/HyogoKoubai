using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace m2mKoubaiDAL
{
    public class BuhinClass
    {

        public class KensakuParam
        {
            public string _Code = "";     // コード            
            //public string _Hinmei = "";     // 品目名   
            //public string _Kubun = "";    //　部品区分
        }
        public class BuhinKey
        {
            //private string _Kubun;
            private string _Code;            

            public BuhinKey(string Code)
            {
                //this._Kubun = Kubun;
                this._Code = Code;                
            }
            //
            public override string ToString()
            {
                //return this._Kubun + "_" + this._Code;
                return this._Code;
            }
        }
        // 検索条件
        private static string WhereText(KensakuParam k)
        {
            Core.Sql.WhereGenerator w = new Core.Sql.WhereGenerator();
            //string str = "";

            // コード
            if(k._Code != "")
                w.Add(string.Format("M_Buhin.BuhinCode = '{0}'", k._Code));
          /*
            //　部品区分
            if (k._Kubun != "")
            {
                w.Add(string.Format("M_Buhin.BuhinKubun = '{0}'", k._Kubun));
            }*/
            return w.WhereText;
        }




        /// <summary>
        /// 全ての部品データを取得
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static m2mKoubaiDataSet.M_BuhinDataTable
            getM_BuhinDataTable(SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Buhin";
            m2mKoubaiDataSet.M_BuhinDataTable dt = new m2mKoubaiDataSet.M_BuhinDataTable();
            da.Fill(dt);
            return dt;
        }

        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="k"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static m2mKoubaiDataSet.M_BuhinDataTable
           getM_BuhinDataTable(KensakuParam k, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Buhin ";
            string strWhere = WhereText(k);
            if (strWhere != "")
            {
                da.SelectCommand.CommandText += "WHERE " + strWhere;
            }
            
            m2mKoubaiDataSet.M_BuhinDataTable dt = new m2mKoubaiDataSet.M_BuhinDataTable();
            da.Fill(dt);
            return dt;
        }
        */

        /// <summary>
        /// 部品データを取得(マスター画面表示用)
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static BuhinDataSet.V_Buhin_MasterDataTable 
            getV_Buhin_MasterDataTable(KensakuParam k, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
            "SELECT          M_Buhin.BuhinCode, M_Buhin.BuhinKubun, M_Buhin.BuhinMei, "
            + "M_Buhin.Tani, M_Buhin.LT_Suuji, M_Buhin.LT_Tani, "
            + "M_Buhin.Tanka, M_Buhin.Lot, M_Buhin.ShiiresakiCode1, "
            + "M_Buhin.ShiiresakiCode2, M_Shiiresaki.ShiiresakiMei AS ShiiresakiMei1, "
            + "M_Shiiresaki_1.ShiiresakiMei AS ShiiresakiMei2, M_Buhin.KanjyouKamokuCode, M_Buhin.HiyouKamokuCode, "
            + "M_Buhin.HojyoKamokuNo "
            
            + "FROM            M_Shiiresaki AS M_Shiiresaki_1 RIGHT OUTER JOIN "
            + "M_Buhin ON "
            + "M_Shiiresaki_1.ShiiresakiCode = M_Buhin.ShiiresakiCode2 LEFT OUTER JOIN "
            + "M_Shiiresaki ON "
            + "M_Buhin.ShiiresakiCode1 = M_Shiiresaki.ShiiresakiCode ";

            string strWhere = WhereText(k);
            if (strWhere != "")
            {
                da.SelectCommand.CommandText += "WHERE " + strWhere;
            }

            da.SelectCommand.CommandText += " ORDER BY  M_Buhin.BuhinCode, M_Buhin.ShiiresakiCode1, M_Buhin.ShiiresakiCode2 ";

            BuhinDataSet.V_Buhin_MasterDataTable dt = new BuhinDataSet.V_Buhin_MasterDataTable();
            da.Fill(dt);
            return dt;
        }


        /// <summary>
        ///主キーによって、部品データを取得
        /// </summary>
        /// <param name="BuhinCode"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static m2mKoubaiDataSet.M_BuhinRow getM_BuhinRow(string BuhinCode, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Buhin WHERE BuhinCode = @BuhinCode";
            //da.SelectCommand.Parameters.AddWithValue("@BuhinKubun", BuhinKubun);
            da.SelectCommand.Parameters.AddWithValue("@BuhinCode", BuhinCode);
            m2mKoubaiDataSet.M_BuhinDataTable dt = new m2mKoubaiDataSet.M_BuhinDataTable();
            da.Fill(dt);
            if (1 == dt.Rows.Count)
                return (m2mKoubaiDataSet.M_BuhinRow)dt.Rows[0];
            else
                return null;
        }

        /// <summary>
        /// 部品データを登録する
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LibError M_Buhin_Insert(m2mKoubaiDataSet.M_BuhinRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Buhin";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            m2mKoubaiDataSet.M_BuhinDataTable dt = new m2mKoubaiDataSet.M_BuhinDataTable();
            m2mKoubaiDataSet.M_BuhinRow drNew = dt.NewM_BuhinRow();
            try
            {
                drNew.BuhinKubun = dr.BuhinKubun;
                drNew.BuhinCode = dr.BuhinCode;
                drNew.BuhinMei = dr.BuhinMei;
                drNew.Tanka = dr.Tanka;
                drNew.Tani = dr.Tani;
                drNew.Lot = dr.Lot;
                drNew.LT_Suuji = dr.LT_Suuji;
                drNew.LT_Tani = dr.LT_Tani;
                drNew.ShiiresakiCode1 = dr.ShiiresakiCode1;
                drNew.ShiiresakiCode2 = dr.ShiiresakiCode2;
                drNew.KanjyouKamokuCode = dr.KanjyouKamokuCode;
                drNew.HiyouKamokuCode = dr.HiyouKamokuCode;
                drNew.HojyoKamokuNo = dr.HojyoKamokuNo;

                dt.Rows.Add(drNew);
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }

        }
        /// <summary>
        /// 部品データを更新
        /// </summary>
        /// <param name="BuhinCode"></param>
        /// <param name="dr"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LibError M_Buhin_Update(string BuhinCode, m2mKoubaiDataSet.M_BuhinRow dr, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Buhin WHERE BuhinCode = @BuhinCode";
            //da.SelectCommand.Parameters.AddWithValue("@BuhinKubun", BuhinKubun);
            da.SelectCommand.Parameters.AddWithValue("@BuhinCode", BuhinCode);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            m2mKoubaiDataSet.M_BuhinDataTable dt = new m2mKoubaiDataSet.M_BuhinDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new LibError("エラー");
            try
            {
                m2mKoubaiDataSet.M_BuhinRow drThis = (m2mKoubaiDataSet.M_BuhinRow)dt.Rows[0];
                drThis.BuhinMei = dr.BuhinMei;
                drThis.BuhinKubun = dr.BuhinKubun;
                drThis.Tani = dr.Tani;
                drThis.LT_Suuji = dr.LT_Suuji;
                drThis.LT_Tani = dr.LT_Tani;
                drThis.Tanka = dr.Tanka;
                drThis.Lot = dr.Lot;
                // 仕入先1
                drThis.ShiiresakiCode1 = dr.ShiiresakiCode1;
                drThis.ShiiresakiCode2 = dr.ShiiresakiCode2;
                drThis.KanjyouKamokuCode = dr.KanjyouKamokuCode;
                drThis.HiyouKamokuCode = dr.HiyouKamokuCode;
                drThis.HojyoKamokuNo = dr.HojyoKamokuNo;
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
        }
        /// <summary>
        /// 部品を削除
        /// </summary>
        /// <param name="BuhinCode"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public static LibError
            M_Buhin_Delete(string BuhinCode, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Buhin WHERE BuhinCode = @BuhinCode ";
            //da.SelectCommand.Parameters.AddWithValue("@BuhinKubun", BuhinKubun);
            da.SelectCommand.Parameters.AddWithValue("@BuhinCode", BuhinCode);
            da.DeleteCommand = (new SqlCommandBuilder(da)).GetDeleteCommand();
            m2mKoubaiDataSet.M_BuhinDataTable dt = new m2mKoubaiDataSet.M_BuhinDataTable();
            da.Fill(dt);
            if (1 != dt.Rows.Count)
                return new LibError("エラー");
            try
            {
                m2mKoubaiDataSet.M_BuhinRow drThis = (m2mKoubaiDataSet.M_BuhinRow)dt.Rows[0];
                drThis.Delete();
                da.Update(dt);
                return null;
            }
            catch (Exception e)
            {
                return new LibError(e);
            }
        }
        /// <summary>
        /// NewRow
        /// </summary>
        /// <returns></returns>
        public static m2mKoubaiDataSet.M_BuhinRow newM_BuhinRow()
        {
            return new m2mKoubaiDataSet.M_BuhinDataTable().NewM_BuhinRow();
        }
        public static void getM_BuhinDataTable(string strText, string strShiiresakiCode, string strBuhinKubun, bool bFirstMatch, int nStartIndex, int nCount,
            SqlConnection sqlConn, out m2mKoubaiDataSet.M_BuhinDataTable dt, ref int nTotal)
        {
            Core.Sql.RowNumberInfo info = new Core.Sql.RowNumberInfo();
            info.nStartNumber = nStartIndex + 1;
            info.nEndNumber = nStartIndex + nCount;
            info.strOverText = "ORDER BY BuhinCode ";

            SqlCommand cmd = new SqlCommand("", sqlConn);
            cmd.CommandText = "SELECT * FROM M_Buhin WHERE (ShiiresakiCode1 LIKE @ShiiresakiCode OR ShiiresakiCode2 LIKE @ShiiresakiCode) AND BuhinKubun LIKE @BuhinKubun ";
            if (strText.Trim() != "")
            {
                cmd.CommandText += " AND BuhinCode LIKE @h OR BuhinName LIKE @h ";
            }
            cmd.Parameters.AddWithValue("@ShiiresakiCode", strShiiresakiCode + "%");
            cmd.Parameters.AddWithValue("@BuhinKubun", strBuhinKubun + "%");
            if (bFirstMatch)
                cmd.Parameters.AddWithValue("@h", strText + "%");
            else
                cmd.Parameters.AddWithValue("@h", "%" + strText + "%");

            dt = new m2mKoubaiDataSet.M_BuhinDataTable();
            info.LoadData(cmd, sqlConn, dt, ref nTotal);
        }


    }
}
