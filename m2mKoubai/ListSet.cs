using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using m2mKoubaiDAL;
using System.Data.SqlClient;

namespace m2mKoubai
{
    public class ListSet
    {
        /// <summary>
        /// Ddl削除フラグをセット
        /// </summary>
        /// <param name="ddl"></param>
        public static void SetDdlSakujoFlag(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "-1"));
            ddl.Items.Add(new ListItem("有効", "0"));
            ddl.Items.Add(new ListItem("無効", "1"));
        }
        /// <summary>
        /// マスタ管理部品の単位をセット
        /// </summary>
        /// <param name="ddl"></param>
        public static void SetDdlTani(DropDownList ddl)
        {
            ddl.Items.Clear();
            /*
            ddl.Items.Add(new ListItem("---", "0"));
            ddl.Items.Add(new ListItem("kg", "kg"));
            ddl.Items.Add(new ListItem("m", "m"));
            ddl.Items.Add(new ListItem("本", "本"));
            ddl.Items.Add(new ListItem("枚", "枚"));
            */
            BuhinDataSet_S.V_Buhin_TaniDataTable dt =
                BuhinClass_S.getV_Buhin_TaniDataTable(Global.GetConnection());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(dt[i].Tani);
            }
        }
        /// <summary>
        /// マスタ管理部品のリードタイムをセット
        /// </summary>
        /// <param name="ddl"></param>
        public static void SetDdlLeadTime(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));
            ddl.Items.Add(new ListItem("day", "1"));
            ddl.Items.Add(new ListItem("week", "2"));
            ddl.Items.Add(new ListItem("month", "3"));
            ddl.Items.Add(new ListItem("year", "4"));
        }
        /// <summary>
        /// 事業所区分をセットする
        /// </summary>
        /// <param name="ddl"></param>
        public static void SetDdlJigyoushoKubun(byte bKubun, DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));
            m2mKoubaiDataSet.T_KaishaInfoDataTable dt =
                KaishaInfoClass.getT_KaishaInfoDataTable(Global.GetConnection());
            if (bKubun == (byte)UserKubun.Shiiresaki)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddl.Items.Add(new ListItem(dt[i].KaishaID.ToString() + "：" + dt[i].EigyouSho, 
                                                dt[i].KaishaID.ToString()));
                }
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddl.Items.Add(new ListItem(dt[i].EigyouSho,dt[i].KaishaID.ToString()));
                }
            }
               
        }
        
        /// <summary>
        /// マスタ管理部品のコードをセット
        /// </summary>
        /// <param name="ddl"></param>
        public static void SetDdlBuhin(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));

            m2mKoubaiDataSet.M_BuhinDataTable dt =
                BuhinClass.getM_BuhinDataTable(Global.GetConnection());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //ddl.Items.Add(new ListItem
                //    (dt[i].BuhinKubun + dt[i].BuhinCode + ":" + dt[i].BuhinMei, dt[i].BuhinKubun + "_" + dt[i].BuhinCode));
                ddl.Items.Add(new ListItem
                    (dt[i].BuhinKubun + ":" + dt[i].BuhinCode + ":" + dt[i].BuhinMei, dt[i].BuhinCode));

            }
        }

        /// <summary>
        /// アカウントのDdl仕入先担当者をセット
        /// </summary>
        /// <param name="bKubun"></param>
        /// <param name="ddl"></param>
        public static void SetDdlShiireTantousha(byte bKubun, DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));
            LoginDataSet.V_ShiiresakiAccountDataTable dt =
                LoginClass.getV_ShiiresakiAccountDataTable(bKubun, Global.GetConnection());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt[i].TantoushaCode + ":" + dt[i].Name, dt[i].TantoushaCode));
            }
        }
        /// <summary>
        /// Ddl担当者をセットする
        /// </summary>
        /// <param name="bKubun"></param>
        /// <param name="ddl"></param>
        public static void SetDdlTantousha(byte bKubun, DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));
            m2mKoubaiDataSet.M_LoginDataTable dt =
                LoginClass.getM_LoginDataTable(bKubun, Global.GetConnection());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt[i].TantoushaCode + ":" + dt[i].Name, dt[i].TantoushaCode));
            }
        }
        /// <summary>
        /// Ddl担当者をセットする
        /// </summary>
        /// <param name="bKubun"></param>
        /// <param name="ddl"></param>
        public static void SetDdlTantoushaValueIsId(byte bKubun, DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));
            m2mKoubaiDataSet.M_LoginDataTable dt =
                LoginClass.getM_LoginDataTable(bKubun, Global.GetConnection());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt[i].TantoushaCode + ":" + dt[i].Name, dt[i].LoginID));
            }
        }
        /// <summary>
        /// Ddl仕入先をセットする
        /// </summary>
        /// <param name="ddl"></param>
        public static void SetDdlShiiresaki(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));
            m2mKoubaiDataSet.M_ShiiresakiDataTable dt =
                ShiiresakiClass.getM_ShiiresakiDataTable(Global.GetConnection());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt[i].ShiiresakiCode + ":" + dt[i].ShiiresakiMei, dt[i].ShiiresakiCode));
            }
        }
        /// <summary>
        /// 納入場所を取得
        /// </summary>
        /// <param name="ddl"></param>
        public static void SetDdlNounyuuBasho(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));

            m2mKoubaiDataSet.M_NounyuuBashoDataTable dt =
                NounyuuBashoClass.getM_NounyuuBashoDataTable(Global.GetConnection());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt[i].BashoCode + ":" + dt[i].BashoMei, dt[i].BashoCode));
            }
        }

        /// <summary>
        /// 部品目名プルダウンを作成
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="nBuhinKubun"></param>
        public static void SetddlBuhin_KubunBetsu(DropDownList ddl, string strShiiresakiCode, string strKubun)
        {
            BuhinDataSet_S.V_BuhinCodeMeiDataTable dt = BuhinClass_S.getV_BuhinCodeMeiDataTable(strShiiresakiCode, strKubun, Global.GetConnection());
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", ""));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt[i].BuhinCode + ":" + dt[i].BuhinMei, dt[i].BuhinCode));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddl"></param>
        public static void SetDdlMsg(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "-1"));
            ddl.Items.Add(new ListItem("受信", "0"));
            ddl.Items.Add(new ListItem("送信", "1"));
            ddl.Items.Add(new ListItem("履歴", "2"));
           
        }
        
        /*
        /// <summary>
        /// 部品区分
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="nBuhinKubun"></param>
        public static void SetDdlBuhinKubun(DropDownList ddl)
        {
            BuhinDataSet_S.V_BuhinKubunDataTable dt =
               BuhinClass_S.getV_BuhinKubunDataTable(Global.GetConnection());
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", ""));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(dt[i].BuhinKubun);
            }          
        }
        */


        public static void SetDdlNounyuuBasho_C(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));

            ChumonDataSet.V_ChumonNounyuuBashoDataTable dt =
                ChumonClass.getV_ChumonNounyuuBashoDataTable(Global.GetConnection());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt[i].NounyuuBashoCode + ":" + dt[i].BashoMei, dt[i].NounyuuBashoCode));
            }
        }


        /// <summary>
        ///発注情報で仕入れ先をセットする
        /// </summary>
        /// <param name="ddl"></param>
        public static void SetDdlShiiresaki_C(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));
            ChumonDataSet.V_ChumonShiiresakiDataTable dt =
                ChumonClass.getV_ChumonShiiresakiDataTablee(Global.GetConnection());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt[i].ShiiresakiCode + ":" + dt[i].ShiiresakiMei, dt[i].ShiiresakiCode));
            }
        }
        /// <summary>
        /// 発注情報でDdl部品区分をセット
        /// </summary>
        /// <param name="ddl"></param>
        public static void SetDdlBuhinKubun_C(DropDownList ddl,string strKaisha)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));

            ChumonDataSet.V_ChumonBuhinKubunDataTable dt =
                ChumonClass.getV_ChumonBuhinKubunDataTable(strKaisha, Global.GetConnection());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt[i].BuhinKubun, dt[i].BuhinKubun));
            }
        }
       
        /// <summary>
        /// 発注情報でDdl部品をセット
        /// </summary>
        /// <param name="strKubun"></param>
        /// <param name="ddl"></param>
        public static void SetDdlBuhin_C(string strKubun, DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));
            ChumonDataSet.V_ChumonBuhinDataTable dt =
                ChumonClass.getV_ChumonBuhinDataTable(strKubun, Global.GetConnection());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt[i].BuhinCode + ":" + dt[i].BuhinMei, dt[i].BuhinCode));
            }
        }
        /// <summary>
        /// 発注情報でDdl発注担当者をセット
        /// </summary>
        /// <param name="ddl"></param>
        public static void SetDdlHacchuTantousha_C(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));

            ChumonDataSet.V_ChumonTantoushaDataTable dt =
                ChumonClass.getV_ChumonTantoushaDataTable(Global.GetConnection());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt[i].TantoushaCode + ":" + dt[i].Name, dt[i].TantoushaCode));
            }
        }
        /// <summary>
        /// 発注情報でDdl納期回答状況
        /// </summary>
        /// <param name="ddl"></param>
        public static void SetDdlNoukiKaitouJyoukyou(DropDownList ddl, byte nkubun)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));
            ddl.Items.Add(new ListItem("納期回答あり", "1"));
            ddl.Items.Add(new ListItem("納期回答なし", "2"));
            if(nkubun == (byte)UserKubun.Owner)
                ddl.Items.Add(new ListItem("未承認", "3"));
        }
        /// <summary>
        /// 発注情報でDdl納期状況
        /// </summary>
        /// <param name="ddl"></param>
        public static void SetDdlNoukiJyoukyou(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));
            ddl.Items.Add(new ListItem("未承認", "1"));
            
        }
        /// <summary>
        /// 発注情報でDdl納品状況
        /// </summary>
        /// <param name="ddl"></param>
        public static void SetDdlNouhinJyoukyou(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));

            ddl.Items.Add(new ListItem("未完納", "1"));
            ddl.Items.Add(new ListItem("完納", "2"));

            ddl.SelectedValue = "1";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddl"></param>
        public static void SetYear(DropDownList ddl)
        {
            ddl.Items.Clear();
            for (int i = 0; i < 3; i++)
            {
                ddl.Items.Add(new ListItem((DateTime.Today.AddYears(-i)).ToString("yy"), i.ToString()));

            }
            ddl.SelectedValue = "0";
        }


        /// <summary>
        /// 品目の勘定科目名
        /// </summary>
        /// <param name="nKamokuCode"></param>
        /// <returns></returns>
        public static void SetKamokuMei(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));
            ddl.Items.Add(new ListItem("原材料", "174"));
            ddl.Items.Add(new ListItem("貯蔵品", "176"));


        }

        /// <summary>
        /// 品目の費用勘定科目名
        /// </summary>
        /// <param name="nHiyouCode"></param>
        /// <returns></returns>
        public static void SetHiyouMei(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));
            ddl.Items.Add(new ListItem("補助材料費", "722"));
            ddl.Items.Add(new ListItem("原料費", "723"));      
            ddl.Items.Add(new ListItem("原燃料費", "740"));
            ddl.Items.Add(new ListItem("消耗品費", "752"));


        }

        /// <summary>
        /// 品目の補助勘定科目名
        /// </summary>
        /// <param name="nHojyoNo"></param>
        /// <returns></returns>
        public static void SetHojyoMei(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));
            ddl.Items.Add(new ListItem("主原料費", "1"));
            ddl.Items.Add(new ListItem("副原料費", "2"));
            ddl.Items.Add(new ListItem("原燃料費", "3"));
            ddl.Items.Add(new ListItem("梱包材料費", "4"));
            ddl.Items.Add(new ListItem("SP付属品", "5"));
            ddl.Items.Add(new ListItem("直接消耗品費", "6"));         
        }

        /// <summary>
        /// 検収情報でDdl部品区分をセット
        /// </summary>
        /// <param name="ddl"></param>
        public static void SetDdlBuhinKubun_K(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));

            KenshuDataSet.V_Kenshu_BuhinKubunDataTable dt =
                KenshuClass.V_Kenshu_BuhinKubunDataTable(Global.GetConnection());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt[i].BuhinKubun, dt[i].BuhinKubun));
            }
        }
        /// <summary>
        /// 検収情報でDdl部品をセット
        /// </summary>
        /// <param name="strKubun"></param>
        /// <param name="ddl"></param>
        public static void SetDdlBuhin_K(string strKubun, DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));
            KenshuDataSet.V_Kenshu_BuhinDataTable dt =
               KenshuClass.getV_Kenshu_BuhinDataTable(strKubun, Global.GetConnection());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt[i].BuhinCode + ":" + dt[i].BuhinMei, dt[i].BuhinCode));
            }
        }

        /// <summary>
        ///検収情報で仕入れ先をセットする
        /// </summary>
        /// <param name="ddl"></param>
        public static void SetDdlShiiresaki_K(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));
            KenshuDataSet.V_Kenshu_ShiireDataTable dt =
                KenshuClass.getV_Kenshu_ShiireDataTable(Global.GetConnection());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt[i].ShiiresakiCode + ":" + dt[i].ShiiresakiMei, dt[i].ShiiresakiCode));
            }
        }
        /// <summary>
        /// 注文情報画面でDdlCancelをセット
        /// </summary>
        /// <param name="ddl"></param>
        public static void SetDdlCancel(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));
            ddl.Items.Add(new ListItem("キャンセル", "1"));
          
        }
        /// <summary>
        /// Ddl仕入先をセットする
        /// </summary>
        /// <param name="ddl"></param>
        /// 09/07/24 追加
        public static void SetDdlShiiresakiMulti(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));
            ShiiresakiDataSet_S.V_ShiiresakiDataTable dt =
                ShiiresakiClass.getV_ShiiresakiDataTable(Global.GetConnection());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt[i].ShiiresakiCode + ":" + dt[i].ShiiresakiMei, dt[i].ShiiresakiCode));
            }
        }
    }
}
