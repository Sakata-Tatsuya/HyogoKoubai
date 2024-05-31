using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace m2mKoubaiDAL
{
    public class Utility
    {
        /// <summary>
        /// テーブルのキーとバリューを取得するクラス
        /// </summary>
        public class TableKeyValue
        {
            public string keyColumn = null;
            public string valueColumn = null;
            public string table = null;
            public Dictionary<string, string> dicWhereKeyValue = new Dictionary<string, string>();
            public int? nStartIndex = null; // RadComboのサジェスト機能を使用するときに指定する
            public int? nCount = null;  // RadComboのサジェスト機能を使用するときに指定する

            private string sql;
            private SqlCommand cmd;

            public TableKeyValue(string keyColumn, string valueColumn, string table, SqlConnection sqlConn)
            {
                this.keyColumn = keyColumn;
                this.valueColumn = valueColumn;
                this.table = table;
                this.sql = string.Format(" SELECT DISTINCT {0}, {1} FROM {2} ", keyColumn, valueColumn, table);
                this.cmd = new SqlCommand(sql, sqlConn);
            }

            public DataTable DtKeyValue
            {
                get
                {
                    string whereText = "";
                    bool isFirst = true;
                    foreach (KeyValuePair<string, string> pair in this.dicWhereKeyValue)
                    {
                        if (!isFirst)
                        {
                            whereText += " AND ";
                        }
                        else
                        {
                            isFirst = false;
                        }
                        whereText += string.Format(" {0} = @{0} ", pair.Key);
                        this.cmd.Parameters.AddWithValue("@" + pair.Key, pair.Value);
                    }

                    if (whereText != "")
                    {
                        this.cmd.CommandText += " WHERE " + whereText;
                    }

                    if (nStartIndex != null && nCount != null)
                    {
                        Core.Sql.RowNumberInfo info = new Core.Sql.RowNumberInfo();
                        info.nStartNumber = (int)nStartIndex + 1;
                        info.nEndNumber = (int)nStartIndex + (int)nCount;
                        this.cmd.CommandText = info.Generate(cmd.CommandText);
                    }

                    SqlDataReader reader = null;

                    try
                    {
                        this.cmd.Connection.Open();

                        reader = cmd.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Columns.Add(keyColumn);
                        if (keyColumn != valueColumn)
                        {
                            dt.Columns.Add(valueColumn);
                        }

                        while (reader.Read())
                        {
                            DataRow dr = dt.NewRow();
                            dr[keyColumn] = reader[keyColumn];
                            dr[valueColumn] = reader[valueColumn];
                            dt.Rows.Add(dr);
                        }

                        return dt;
                    }
                    finally
                    {
                        if (this.cmd.Connection != null) { this.cmd.Connection.Close(); }
                        if (reader != null) { reader.Close(); }
                    }
                }
            }
        }

        public struct DenpyouSyu
        {
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "A":
                        return "領料単";
                    case "B":
                        return "退料単";
                    case "C":
                        return "成型入庫単";
                    case "D":
                        return "出荷単";
                    case "E":
                        return "倉入れ伝票";
                    case "F":
                        return "送貨単";
                    case "G":
                        return "退貨単";
                    case "H":
                        return "入庫単";
                    case "I":
                        return "廃棄単";
                    case "J":
                        return "外发原料保管单";
                    default:
                        return "";
                }
            }
        }

        public struct DenpyouUketori
        {
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "1":
                        return "生産部門";
                    case "2":
                        return "部品倉庫";
                    case "3":
                        return "部品倉庫";
                    case "4":
                        return "取引先（外注先加工）";
                    case "5":
                        return "製品倉庫";
                    case "6":
                        return "部品倉庫";
                    case "7":
                        return "取引先（外注先加工）";
                    case "8":
                        return "取引先（海外）";
                    case "9":
                        return "なし（または再生工場）";
                    default:
                        return "";
                }
            }
        }

        public struct UserYukou
        {
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "True":
                        return "有効";
                    case "False":
                        return "<span style=\"color:Red;font-weight:bold\">無効</span>";
                    default:
                        return "";
                }
            }
        }

        public struct SeihinYukou
        {
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "True":
                        return "表示";
                    case "False":
                        return "<span style=\"color:Red;font-weight:bold\">非表示</span>";
                    default:
                        return "";
                }
            }
        }

        public struct UserAdmin
        {
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "True":
                        return "有";
                    case "False":
                        return "無";
                    default:
                        return "";
                }
            }
        }

        public struct StatusFlag
        {
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "True":
                        return "<span style=\"color:#0000FF;font-weight:bold\">済</span>";
                    case "False":
                        return "<span style=\"color:#ff7256;font-weight:bold\">未</span>"; 
                    default:
                        return "";
                }
            }
        }

        public struct HyouKubun
        {
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "1":
                        return "○";
                    case "2":
                        return "<span style=\"color:Red;font-weight:bold\">×</span>";
                    case "3":
                        return "<span style=\"color:Blue;font-weight:bold\">×</span>⇒○";
                    default:
                        return "";
                }
            }
        }

        public struct FutekiaiUmu
        {
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "True":
                        return "<span style=\"color:Red;font-weight:bold\">有</span>";
                    case "False":
                        return "無";
                    default:
                        return "";
                }
            }
        }

        public struct UkebaraiKubun
        {
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "1":
                        return "入庫";
                    case "2":
                        return "出庫";
                    case "3":
                        return "仕掛";
                    default:
                        return "";
                }
            }
        }

        public struct KouteiKubun
        {
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "J":
                        return "自社";
                    case "G":
                        return "外注";
                    default:
                        return "";
                }
            }
        }

        public struct MarumeKikan
        {
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "MD":
                        return "毎日";
                    case "MW":
                        return "毎週";
                    case "2W":
                        return "２週間分";
                    case "MM":
                        return "１月分";
                    default:
                        return "";
                }
            }
        }

        public struct RohsKubun
        {
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "1":
                        return "未対応品";
                    case "0":
                        return "Rohs対応";
                    default:
                        return "";
                }
            }
        }

        public struct HachuKahiKubun
        {
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "1":
                        return "所要計算対象外";
                    case "0":
                        return "通常発注";
                    default:
                        return "";
                }
            }
        }

        public struct HozeiKubun
        {
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "1":
                        return "保税対象";
                    case "0":
                        return "対象外";
                    default:
                        return "";
                }
            }
        }
        
        public struct KensaKubun
        {
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "2":
                        return "安規";
                    case "1":
                        return "検査対象";
                    case "0":
                        return "対象外";
                    default:
                        return "";
                }
            }
        }

        public struct MaterialKubun
        {
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "A":
                        return "組立品";
                    case "P":
                        return "部品";
                    case "R":
                        return "素材";
                    default:
                        return "";
                }
            }
        }

        public struct RenbanJidouSeteiKubun
        {
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "2":
                        return "対象外";
                    case "1":
                        return "対象";
                    default:
                        return "";
                }
            }
        }

        public struct ShihonKeitai
        {
            public const byte NONE = 0;
            public const byte DO = 1;
            public const byte GO = 2;
            public const byte OT = 3;
            public const byte KO = 4;
            public const byte SH = 5;
            
            public static string GetText(byte b)
            {
                switch (b)
                {
                    case DO:
                        return "独資";
                    case GO:
                        return "合弁";
                    case OT:
                        return "郷鎮";
                    case KO:
                        return "国営";
                    case SH:
                        return "私企業";
                    default:
                        return "";
                }
            }
        }

        public struct ShiharaiJyouken
        {
            public const byte NONE = 0;
            public const byte ZE = 1;
            public const byte KY = 2;
            public const byte SN = 3;
            public const byte YO = 4;
            public const byte RO = 5;

            public static string GetText(byte b)
            {
                switch (b)
                {
                    case ZE:
                        return "前金";
                    case KY:
                        return "キャシュ＆デリバリー";
                    case SN:
                        return "30日";
                    case YO:
                        return "45日";
                    case RO:
                        return "60日";
                    default:
                        return "";
                }
            }
        }
        
        public static bool IsNotEmpty(string str)
        {
            return !IsEmpty(str);
        }

        public static bool IsEmpty(string str)
        {
            return (null == str || "" == str.Trim());
        }

        /// <summary>
        /// 空白除去
        /// </summary>
        /// <param name="tbx"></param>
        //public static void Trim(TextBox tbx)
        //{
        //    tbx.Text = tbx.Text.Trim();
        //}

        /// <summary>
        /// int型→datetime型に変換
        /// </summary>
        /// <param name="nNengappi"></param>
        /// <returns></returns>
        public static DateTime? getDateTime(int nNengappi)
        {
            if (nNengappi.ToString().Length == 8)
            {
                string str = nNengappi.ToString();
                return new DateTime
                    (int.Parse(str.Substring(0, 4)), int.Parse(str.Substring(4, 2)), int.Parse(str.Substring(6, 2)));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// int型チェック
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(string str)
        {
            try
            {
                int.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// decimal型チェック
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDecimal(string str)
        {
            try
            {
                decimal.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Byte型チェック
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsByte(string str)
        {
            try
            {
                byte.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// メールチェック
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsMail(string str)
        {
            Regex reg = new Regex
                (@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            
            if (reg.IsMatch(str))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// yyyyMMddをyyyy/MM/ddに変換
        /// </summary>
        /// <param name="yyyyMMdd"></param>
        /// <returns></returns>
        public static string FormatFromyyyyMMdd(string yyyyMMdd)
        {
            if (yyyyMMdd.Length != 8)
                return yyyyMMdd;

            return string.Format("{0}/{1}/{2}", yyyyMMdd.Substring(0, 4), yyyyMMdd.Substring(4, 2), yyyyMMdd.Substring(6, 2));
        }

        /// <summary>
        /// yyyyMMddをyyyy/MMに変換
        /// </summary>
        /// <param name="yyyyMMdd"></param>
        /// <returns></returns>
        public static string FormatFromyyyyMM(string yyyyMM)
        {
            if (yyyyMM.Length != 6)
                return yyyyMM;

            return string.Format("{0}/{1}", yyyyMM.Substring(0, 4), yyyyMM.Substring(4, 2));
        }

        /// <summary>
        /// 半角のみか？
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsHankaku(string str)
        {
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            int num = sjisEnc.GetByteCount(str);
            return num == str.Length;
        }

        /// <summary>
        /// float型チェック
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsFloat(string str)
        {
            try
            {
                float.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 発注Noのリンクボタン
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="strArray"></param>
        /// <returns></returns>
        public static string LinkToHacchuuNo(string strKey, string strHacchuuNo)
        {
            return string.Format("<a href=Javascript:void(0); onclick=\"HacchuuNo('{0}')\"; style=color:Blue>{1}</a>",
                strKey, strHacchuuNo);
        }
        
        public static string LinkToSeisankeikaku(string strKey, string strHacchuuNo)
        {
            return string.Format("<a href=Javascript:void(0); onclick=\"SeihinKeikaku('{0}')\"; style=color:Red>{1}</a>",
                strKey, strHacchuuNo);
        }

        public static string LinkToMitumoriNo(string strKey, string strMitumoriNo)
        {
            return string.Format("<a href=Javascript:void(0); onclick=\"MitumoriNo('{0}')\"; style=color:Blue>{1}</a>",
                strKey, strMitumoriNo);
        }

        /// <summary>
        /// 数量のリンクボタン
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="strHacchuuNo"></param>
        /// <returns></returns>
        public static string LinkToSuRyouNo(string strKey, string strSuryou)
        {
            return string.Format("<a href=Javascript:void(0); onclick=\"Suryou('{0}')\"; style=color:White>{1}</a>",
                strKey, strSuryou);
        }

        public static string ToInnerRowsHTML_NoLine(params string[] strArray)
        {
            string str = "";
            for (int i = 0; i < strArray.Length; i++)
            {
                string s = ("" == strArray[i]) ? "&nbsp;" : strArray[i];
                if (0 == i)
                    str += string.Format("<div noWrap>{0}</div>", s);
                else
                    str += string.Format("<div noWrap>{0}</div>", s);
            }
            return str;
        }

        public static string FormatToyyMMdd(string yyyyMMdd)
        {
            if (yyyyMMdd.Length != 8)
                return yyyyMMdd;

            return string.Format("{0}/{1}/{2}", yyyyMMdd.Substring(2, 2), yyyyMMdd.Substring(4, 2), yyyyMMdd.Substring(6, 2));
        }

        public static string FormatToyyMMddHHmm(string yyyyMMdd)
        {
            return (DateTime.Parse(yyyyMMdd)).ToString("yy/MM/dd HH:mm");
        }

        public static string FormatToyyyyMMdd(string yyMMdd)
        {
            if (yyMMdd.Length != 6)
            {
                return yyMMdd;
            }
            else
            {
                try
                {
                    DateTime date = DateTime.ParseExact(yyMMdd, "yyMMdd", null);
                    return date.ToString("yyyyMMdd");
                }
                catch
                {
                    return "";
                }

            }
        }

        // 郵便番号変換
        public static string FormatYuubin(string strBanggou)
        {
            if (strBanggou.Length != 7)
                return strBanggou;
            else
                return strBanggou.Substring(0, 3) + "-" + strBanggou.Substring(3, 4);
        }
        // 
        public static string FormatBanggo(string strBanggou)
        {
            if (strBanggou.Length != 10)
                return strBanggou;
            else
                return strBanggou.Substring(0, 2) + "-" + strBanggou.Substring(2, 4) + "-" + strBanggou.Substring(6, 4);
        }

        // 日曜日始まりの場合
        public static DateTime FirstDayOfThisWeek(DateTime d)
        {
            return d.AddDays(DayOfWeek.Sunday - d.DayOfWeek);
        }

        // 月曜日始まりの場合
        public static DateTime FirstDayOfThisWeek2(DateTime d)
        {
            int diff = DayOfWeek.Monday - d.DayOfWeek;
            if (diff > 0)
                diff -= 7;
            return d.AddDays(diff);
        }

        // 基準日（直近の奇数週の日曜日）を取得
        public static DateTime GetKijunBi2W(DateTime today)
        {
            int num = (today.DayOfYear - 1) / 7;

            // 奇数週
            if (num % 2 == 0)
            {
                return FirstDayOfThisWeek(today);
            }
            // 偶数週
            else
            {
                return FirstDayOfThisWeek(today.AddDays(-7));
            }
        }

        public struct ZaikoKubun
        {
            public const string RYOUHIN = "1";
            public const string SHIKAKARI = "2";
            public const string FURYOU = "3";
            
            public static string GetText(string str)
            {
                switch (str)
                {
                    case RYOUHIN:
                        return "良品";
                    case SHIKAKARI:
                        return "仕掛品";
                    case FURYOU:
                        return "不良品";
                    default:
                        return "";
                }
            }
        }
        
        public struct ZaikoKubunValue
        {
            public const string _RYOUHIN = "良品";
            public const string _SHIKAKARI = "仕掛品";
            public const string _FURYOU = "不良品";
            
            public static string GetText(string str)
            {
                switch (str)
                {
                    case _RYOUHIN:
                        return "1";
                    case _SHIKAKARI:
                        return "2";
                    case _FURYOU:
                        return "3";
                    default:
                        return "";
                }
            }
        }

        public struct ShisonGenin
        {
            public const string BULIANG = "1";
            public const string GONGYI = "2";
            public const string SHENGCHAN = "3";
            public const string QITA = "4";
            
            public static string GetText(string str)
            {
                switch (str)
                {
                    case BULIANG:
                        return "不良制品";
                    case GONGYI:
                        return "工艺变更";
                    case SHENGCHAN:
                        return "生产终了";
                    case QITA:
                        return "其它";
                    default:
                        return "";
                }
            }
        }

        public struct ShisonGeninValue
        {
            public const string _BULIANG = "不良制品";
            public const string _GONGYI = "工艺变更";
            public const string _SHENGCHAN = "生产终了";
            public const string _QITA = "其它";

            public static string GetText(string str)
            {
                switch (str)
                {
                    case _BULIANG:
                        return "1";
                    case _GONGYI:
                        return "2";
                    case _SHENGCHAN:
                        return "3";
                    case _QITA:
                        return "4";
                    default:
                        return "";
                }
            }
        }

        public struct TeiseiRiyu
        {
            public const string A = "1";
            public const string B = "2";
            public const string C = "3";
            public const string D = "4";

            public static string GetText(string str)
            {
                switch (str)
                {
                    case A:
                        return "数量入力誤";
                    case B:
                        return "単価入力誤り";
                    case C:
                        return "数量、単価入力誤り";
                    case D:
                        return "その他";
                    default:
                        return "";
                }
            }
        }

        /// <summary>
        /// 修正伝票用
        /// </summary>
        public struct DenSyu
        {
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "A":
                        return "領料単";
                    case "B":
                        return "退料単";
                    case "C":
                        return "送貨単";
                    case "D":
                        return "退貨単";
                    case "E":
                        return "出荷単（支給）";
                    case "F":
                        return "入庫単（完成品）";
                    case "G":
                        return "入庫単（成型）";
                    case "H":
                        return "入庫単";
                    default:
                        return "";
                }
            }
        }
        
        /// <summary>
        /// メニュー設定　⇒　変更すると必ずT_Menuに登録、更新してください！★★★★★
        /// </summary>
        public struct MenuSetting
        {
            //HTK = "発注管理";
            //SKI = "生産計画閲覧";
            //XKI = "所要計画閲覧";
            //KKY = "加工予定情報";
            //TOS = "棚卸詳細";
            //ZDT = "在庫データ";
            //DHK = "伝票発行";
            //TDK = "訂正伝票";

            //THS	取引先マスタ	
            //THB	取引先分類マスタ	
            //KKK	顧客マスタ	
            //SYI	社員マスタ	
            //GSK	原産国マスタ	
            //TUK	通貨マスタ	
            //BHS	部品種マスタ	
            //KSM	構成マスタ	
            //CRD	カレンダー	
            //GHY	言語翻訳	
            //STS	システム設定	
            //MNS	メニュー設定	
            //SHM	製品マスタ	
            //BHM	部品マスタ	
            //KAK	加工区マスタ	
            //TKM	発注単価マスタ	
            
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "HTK":
                        return "HTK";
                    case "SKI":
                        return "SKI";
                    case "XKI":
                        return "XKI";
                    case "KKY":
                        return "KKY";
                    case "TOS":
                        return "TOS";
                    case "ZDT":
                        return "ZDT";
                    case "DHK":
                        return "DHK";
                    case "TDK":
                        return "TDK";
                    case "THS":
                        return "THS";
                    case "THB":
                        return "THB";
                    case "KKK":
                        return "KKK";
                    case "SYI":
                        return "SYI";
                    case "GSK":
                        return "GSK";
                    case "TUK":
                        return "TUK";
                    case "BHS":
                        return "BHS";
                    case "KSM":
                        return "KSM";
                    case "CRD":
                        return "CRD";
                    case "GHY":
                        return "GHY";
                    case "STS":
                        return "STS";
                    case "MNS":
                        return "MNS";
                    case "SHM":
                        return "SHM";
                    case "BHM":
                        return "BHM";
                    case "KAK":
                        return "KAK";
                    case "TKM":
                        return "TKM";
                        
                    default:
                        return "";
                }
            }
        }

        /// <summary>
        /// 権限設定
        /// </summary>
        public struct Kengen
        {
            public const int boos1 = 100;
            public const int boos2 = 200;
            public const int Keikaku = 300;
            public const int Gyoumu = 400;
            public const int Tana = 500;
            public const int Souko1 = 600;
            public const int Souko2 = 700;
            public const int Souko3 = 800;
            public const int Souko4 = 900;
            public const int admin = 401;
            
            public static string GetText(int b)
            {
                switch (b)
                {
                    case boos1:
                    case boos2:
                        return "BOOS";
                    case Keikaku:
                        return "KEIKAKU";
                    case Gyoumu:
                        return "GYOUMU";
                    case Tana:
                        return "TANA";
                    case Souko1:
                    case Souko2:
                    case Souko3:
                    case Souko4:
                        return "SOUKO";
                    case admin:
                        return "KANRI";
                    default:
                        return "";
                }
            }
        }


        public struct NaijiKubun
        {
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "1":
                        return "する";
                    case "0":
                        return "しない";
                    default:
                        return "";
                }
            }
        }


        public struct SyukaHouhouKubun
        {
            public static string GetText(string str)
            {
                switch (str)
                {
                    case "1":
                        return "在庫";
                    case "0":
                        return "直送";
                    default:
                        return "";
                }
            }
        }

        public static string GetYM(int nM, string strOLDYM)
        {
            string strYM = "";
            DateTime dtBirth = DateTime.Parse(Utility.FormatFromyyyyMMdd(strOLDYM + "01"));
            dtBirth = dtBirth.AddMonths(nM);
            strYM = (dtBirth.ToString().Substring(0, 5) + int.Parse(dtBirth.ToString().Substring(5, 2))).Replace("/","年") + "月";
            return strYM;
        }

        public struct Kubun
        {
            public const string _komoku1 = "1";
            public const string _komoku2 = "2";
                
            public static string GetText(string str)
            {
                switch (str)
                {
                    case _komoku1:
                        return "出荷見込";
                    case _komoku2:
                        return "仕入未承認";
                    default:
                        return "";
                }
            }
        }

        public static string SplitShimei(string strJusho, int nMaxMojiSuInLine)
        {
            System.Text.Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            string str = GetSubstringByByte(strJusho, encoding, nMaxMojiSuInLine);

            return str;
        }

        public static string GetSubstringByByte(string value, System.Text.Encoding encoding, int size)
        {
            int i = encoding.GetByteCount(value);

            if (i <= size)
            {
                // 指定サイズより文字列が短い場合そのまま返す
                return value;
            }
            byte[] b = encoding.GetBytes(value);
            if (!CheckJustSplitMultiByte(value, size, encoding))
            {
                size = size - 1;
            }

            string str1 = encoding.GetString(b, 0, size);
            string str2 = encoding.GetString(b, size, i - size);

            return str1 + "<br />" + str2;
        }

        public static bool CheckJustSplitMultiByte(string stTarget, int iSplitPosB, Encoding oEncoding)
        {
            byte[] nByteAry = oEncoding.GetBytes(stTarget);

            // 分割位置が文字列範囲外の場合、ぴったり切れている
            if (iSplitPosB < 0 || nByteAry.Length <= iSplitPosB) { return true; }

            // 分割して合計文字数を得る。マルチバイト区切りで発生したゴミ文字があれば文字数は増える
            string stSplitLeft = oEncoding.GetString(nByteAry, 0, iSplitPosB);
            string stSplitRight = oEncoding.GetString(nByteAry, iSplitPosB, nByteAry.Length - iSplitPosB);
            int iSplitCharSum = stSplitLeft.Length + stSplitRight.Length;

            return (stTarget.Length == iSplitCharSum);
        }
        public static bool GetKeigenZeirituFlg(DateTime chkDate, string strZeiritu)
        {
            if (chkDate >= new DateTime(2019, 10, 1))
            {
                if (strZeiritu == "8")
                {
                    return true;
                }
            }
            //else if (chkDate >= new DateTime(
            return false;
        }






    }
}