using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace m2mKoubaiDAL
{
    public class MailClass
    {
        private const string _SystemURL = "http://m2mkoubai.m2m-asp.com/";
        /// <summary>
        /// メール送信時に必要な項目
        /// (メール送信時、このインスタンスを作成し、全ての項目に値をセットする)
        /// </summary>
        public class MailParam
        {
            public string _MailTo = "";   // メール送信先
            public string _MailFrom = ""; // メール送信元
            public string _ToCC = "";     // CC
            public string _Subject = "";  // 件名
            public string _Body = "";     // 本文
            public string _SMTP_Server = ""; // SMTP
        }


        public static void SendMail(MailParam p, string[] aryTocc)
        {
            try
            {
                if (p._MailTo != "" && p._MailFrom != "" && p._Subject != "" &&
                    p._Body != "" && p._SMTP_Server != "")
                {
                    System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();

                    // 送信元と送信元表示名
                    mm.From = new System.Net.Mail.MailAddress(p._MailFrom);

                    mm.To.Add(p._MailTo);
                    if (aryTocc != null)
                    {
                        for (int i = 0; i < aryTocc.Length; i++)
                        {
                            if (aryTocc[i].ToString() != "")
                                mm.CC.Add(aryTocc[i].ToString());
                        }
                    }

                    mm.Subject = p._Subject;
                    mm.Body = p._Body;

                    // html形式にする
                    mm.IsBodyHtml = false;

                    System.Net.Mail.SmtpClient sc = new System.Net.Mail.SmtpClient(p._SMTP_Server);

                    sc.Send(mm);

                    // 成功した時も自分にメール送信
                    System.Net.Mail.MailMessage mm2 = new System.Net.Mail.MailMessage();
                    mm2.From = new System.Net.Mail.MailAddress("dev@m2m-asp.com");
                    mm2.To.Add("sakata@m2m-asp.com");
                    mm2.Subject = "送信成功";
                    mm2.Body = p._Body;

                    // html形式にする
                    mm2.IsBodyHtml = false;

                    System.Net.Mail.SmtpClient sc2 = new System.Net.Mail.SmtpClient(p._SMTP_Server);
                    sc2.Send(mm2);
                }
                /*
                else
                {
                    System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();
                    // 送信元と送信元表示名
                    mm.From = new System.Net.Mail.MailAddress("dev@m2m-asp.com");
                    mm.To.Add("sakata@m2m-asp.com");
                    mm.Subject = "送信失敗";
                    mm.Body = "送信元: " + p._MailFrom + "\r\n\r\n"
                                + "送信先: " + p._MailTo + "\r\n\r\n"
                                + "件名: " + p._Subject + "\r\n\r\n"
                                + "内容: " + p._Body + "\r\n\r\n"
                                + "SMTP: " + p._SMTP_Server + "\r\n\r\n";
                    //+ "発注者ID: " + strLoginID + "\r\n\r\n"
                    //+ "仕入先コード: " + strShiire;

                    // html形式にする
                    mm.IsBodyHtml = false;

                    System.Net.Mail.SmtpClient sc = new System.Net.Mail.SmtpClient(p._SMTP_Server);
                    sc.Send(mm);
                }
                */
            }
            catch (Exception e)
            {
                System.Net.Mail.MailMessage mm1 = new System.Net.Mail.MailMessage();
                // 送信元と送信元表示名
                mm1.From = new System.Net.Mail.MailAddress("dev@m2m-asp.com");
                mm1.To.Add("sakata@m2m-asp.com");
                mm1.Subject = "送信失敗";
                mm1.Body = "送信元: " + p._MailFrom + "\r\n\r\n"
                            + "送信先: " + p._MailTo + "\r\n\r\n"
                            + "件名: " + p._Subject + "\r\n\r\n"
                            + "内容: " + p._Body + "\r\n\r\n"
                            + "SMTP: " + p._SMTP_Server + "\r\n\r\n"
                            + "例外の理由：" + e.ToString();
                // + "発注者ID: " + strLoginID + "\r\n\r\n"
                //+ "仕入先コード: " + strShiire;

                // html形式にする
                mm1.IsBodyHtml = false;

                System.Net.Mail.SmtpClient sc1 = new System.Net.Mail.SmtpClient(p._SMTP_Server);
                sc1.Send(mm1);
            }
        }

        /// <summary>
        /// メール本文（発注の取消）
        /// </summary>
        /// <returns></returns>
        public static string GetBody_Cancel(ChumonDataSet.V_MailInfoRow dr)
        {
            string strBody =
                "いつもお世話になっています。\r\n"
                + "御社あて、発注品に対し発注の取消を行いました。\r\n"
                + "下記URLにログインいただき、内容のご確認をお願い致します。\r\n"
                + _SystemURL + "\r\n"
                + "**************************************************************\r\n"
                + "★このメールに関するお問い合わせは、下記担当者までご連絡下さい。\r\n"
                + dr.KaishaMei + " " + dr.EigyouSho + "\r\n"
                + dr.Name + "\r\n"
                + "TEL：" + dr.Tel + "\r\n"
                + "FAX：" + dr.Fax + "\r\n"
                + "E-mail：" + dr.Mail_Y;

            return strBody;

        }

        /// <summary>
        /// メール本文(新規注文発生時メール)
        /// </summary>
        /// <returns></returns>
        public static string GetBody_ShinkiChumon(ChumonDataSet.V_MailInfoRow dr)
        {
            string strBody =
                "いつもお世話になっています。\r\n"
                + "御社あて、発注情報をご案内致します。\r\n"
                + "下記URLにログインいただき、内容のご確認をお願い致します。\r\n"
                + "また、本発注に対し納期回答の登録をお願いします。\r\n"
                + _SystemURL + "\r\n"
                + "納期回答が1週間内に登録されていない場合\r\n"
                + "自動的に催促メールが配信されます。\r\n"
                + "**************************************************************\r\n"
                + "★このメールに関するお問い合わせは、下記担当者までご連絡下さい。\r\n"
                + dr.KaishaMei + " " + dr.EigyouSho + "\r\n"
                + dr.Name + "\r\n"
                + "TEL：" + dr.Tel + "\r\n"
                + "FAX：" + dr.Fax + "\r\n"
                + "E-mail：" + dr.Mail_Y;

            return strBody;

        }

        /// <summary>
        /// メール本文(発注数量の変更)
        /// </summary>
        /// <returns></returns>
        public static string GetBody_Suuryou(ChumonDataSet.V_MailInfoRow dr)
        {
            string strBody =
                "いつもお世話になっています。\r\n"
                + "御社あて、発注品に対し発注数量の変更を行いました。\r\n"
                + "下記URLにログインいただき、内容のご確認をお願い致します。\r\n"
                + _SystemURL + "\r\n"
                + "**************************************************************\r\n"
                + "★このメールに関するお問い合わせは、下記担当者までご連絡下さい。\r\n"
                + dr.KaishaMei + " " + dr.EigyouSho + "\r\n"
                + dr.Name + "\r\n"
                + "TEL： " + dr.Tel + "\r\n"
                + "FAX ：" + dr.Fax + "\r\n"
                + "E-mail：" + dr.Mail_Y;

            return strBody;

        }

        /// <summary>
        /// メール本文(指定納期変更時のメール)
        /// </summary>
        /// <returns></returns>
        public static string GetBody_NoukiHenkou(ChumonDataSet.V_MailInfoRow dr)
        {
            string strBody =
              "いつもお世話になっています。\r\n"
              + "御社あて、発注品に対し指定納期の変更を登録しています。\r\n"
              + "下記URLにログインいただき、内容のご確認をお願い致します。\r\n"
              + "また、変更納期に対し回答の登録をお願いします。\r\n"
              + _SystemURL + "\r\n"
              + "納期回答が1週間内に登録されていない場合\r\n"
              + "自動的に催促メールが配信されます。\r\n"
              + "**************************************************************\r\n"
              + "★このメールに関するお問い合わせは、下記担当者までご連絡下さい。\r\n"
              + dr.KaishaMei + " " + dr.EigyouSho + "\r\n"
              + dr.Name + "\r\n"
              + "TEL：" + dr.Tel + "\r\n"
              + "FAX：" + dr.Fax + "\r\n"
              + "E-mail：" + dr.Mail_Y;

            return strBody;
        }

        /// <summary>
        /// メール本文(【会社情報更新メール】)
        /// </summary>
        /// <returns></returns>
        public static string GetBody_KaishaInfo_Koushin(ChumonDataSet.V_Chumon_MailRow dr, m2mKoubaiDataSet.M_ShiiresakiRow drShiire)
        {
            string strBody =
             "会社情報が更新されました。\r\n"
             + "下記URLにログインいただき、内容のご確認をお願い致します。\r\n"
             + "仕入先コード：" + drShiire.ShiiresakiCode + "\r\n"
             + "仕入先名：" + drShiire.ShiiresakiMei + "\r\n"
             + _SystemURL + "\r\n";

            return strBody;
        }

        /// <summary>
        /// メール本文(納期回答登録時のメール)
        /// </summary>
        /// <returns></returns>
        public static string GetBody_NoukiKaitou_Shinki(m2mKoubaiDataSet.M_ShiiresakiRow dr)
        {
            string strBody =
              "納期回答が登録されました。\r\n"
              + "下記URLにログインいただき、内容のご確認をお願い致します。\r\n"
              + "仕入先コード：" + dr.ShiiresakiCode + "\r\n"
              + "仕入先名：" + dr.ShiiresakiMei + "\r\n"
              + _SystemURL + "\r\n";
            return strBody;
        }

        /// <summary>
        /// メール本文(納期回答変更時のメール)
        /// </summary>
        /// <returns></returns>
        public static string GetBody_NoukiKaitou_Koushin(m2mKoubaiDataSet.M_ShiiresakiRow dr)
        {
            string strBody =
              "納期回答が変更されました。\r\n"
              + "下記URLにログインいただき、内容のご確認をお願い致します。\r\n"
              + "仕入先コード：" + dr.ShiiresakiCode + "\r\n"
              + "仕入先名：" + dr.ShiiresakiMei + "\r\n"
              + _SystemURL + "\r\n";
            return strBody;
        }
    }
}

