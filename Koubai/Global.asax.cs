using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Management;
using System.Data.SqlClient;
using System.Web.Optimization;
using System.Web.Routing;

using KoubaiDAL;


namespace Koubai
{
    public class Global : System.Web.HttpApplication
    {
        private static HonyakuClass.HonyakuManager _HonyakuManager = new HonyakuClass.HonyakuManager(Global.Languages, Global.GetConnection());
        void Application_Start(object sender, EventArgs e)
        {
            // アプリケーションのスタートアップで実行するコードです
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public static HonyakuClass.HonyakuManager HonyakuManager
        {
            get
            {
                return _HonyakuManager;
            }
        }

        public static string[] Languages
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["Languages"].Split(',');
            }
        }
        protected void Application_End(object sender, EventArgs e)
        {

        }

        [Obsolete]
        protected void Session_End(object sender, EventArgs e)
        {

            System.Collections.IDictionary tbl =
                System.Configuration.ConfigurationManager.GetSection("ServerViewStatePage")
                as System.Collections.IDictionary;
            Core.Web.ServerViewStatePage.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"];
        }


        /// <summary>
        /// DEBUGでは、Application_Errorからメール送信されたが、本番環境では送信されない現象が発生した
        /// 原因は、Global.asaxが本番環境で削除されていた為であった
        /// ★ Global.asaxのファイルを配置しないと、本関数はコールされないので注意！！！
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(Object sender, EventArgs e)
        {
            string strErrMsg = GetErrorInfo();
            try
            {
#if !DEBUG
                // デバッグじゃない場合エラーメール送信
                SendMail(strErrMsg);
                this.Response.Write("Error");
                Server.ClearError();
#endif
            }
            catch (Exception ee)
            {
                this.Response.Write(ee.Message);
            }
        }

        public static void SendMail(string strMsg)
        {
            string strSubject = "m2m Web-EDIエラー";

            System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage
                (System.Configuration.ConfigurationManager.AppSettings["Err_Mail_From"],
                System.Configuration.ConfigurationManager.AppSettings["Err_Mail_To"]);
            mm.Subject = strSubject;
            mm.Body = strMsg;
            System.Net.Mail.SmtpClient sc = new System.Net.Mail.SmtpClient
                (System.Configuration.ConfigurationManager.AppSettings["SMTP_Server"]);
            sc.Send(mm);
        }

        private string GetErrorInfo()
        {
            System.IO.StringWriter w = new System.IO.StringWriter();
            System.Exception ex = Context.Server.GetLastError().GetBaseException();

            // IPアドレス
            w.WriteLine("[IPアドレス]");
            w.WriteLine(GetMyIPAddress());

            // エラー発生ページ
            w.WriteLine("[エラー発生ページ]");
            w.WriteLine(Request.Path);

            // エラーメッセージ
            w.WriteLine("[エラーメッセージ]");
            w.WriteLine(ex.Message);

            // スタックトレース
            w.WriteLine("[スタックトレース]");
            w.WriteLine(ex.StackTrace);

            // セッション値
            // スタックトレース
            w.WriteLine("[セッション値]");
            for (int i = 0; i < Session.Count; i++)
                w.WriteLine("{0}：{1}", Session.Keys[i], Session[Session.Keys[i]]);

            return w.ToString();
        }

        private static string GetMyIPAddress()
        {

            string strIP = "";

            try
            {
                System.Management.ManagementScope scope = new System.Management.ManagementScope("root\\cimv2");
                scope.Connect();
                System.Management.ObjectQuery oq
                    = new System.Management.ObjectQuery("Select IPAddress from Win32_NetworkAdapterConfiguration where IPEnabled=TRUE");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, oq);
                ManagementObjectCollection collection = searcher.Get();
                foreach (ManagementObject mo in collection)
                {
                    string[] ip = (string[])mo["IPAddress"];
                    if (null != ip)
                    {
                        for (int t = 0; t < ip.Length; t++)
                        {
                            if ("" != strIP) strIP += "_";
                            strIP += ip[t];
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                strIP = ex.Message;
            }

            return strIP;

        }



        public static byte[] GetKey()
        {
            return new byte[16] { 45, 98, 102, 240, 52, 13, 89, 77, 10, 19, 95, 145, 38, 209, 102, 162 };
        }



        /// <summary>
        /// DB接続
        /// </summary>
        /// <returns></returns>
        public static System.Data.SqlClient.SqlConnection GetConnection()
        {
            return new System.Data.SqlClient.SqlConnection(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
        }
        /// <summary>
        /// ログイン画面のURL
        /// </summary>
        public static string LoginPageURL
        {
            get
            {
                //return System.Web.HttpContext.Current.Request.ApplicationPath + "/"
                //    + System.Configuration.ConfigurationManager.AppSettings["LoginUrl"];
                return System.Configuration.ConfigurationManager.AppSettings["LoginUrl"];
            }
        }
        public static string AppRootURL
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["AppRtUrl"];
            }
        }

        public static int DataDownloadMaxGyou
        {
            get
            {
                return 50000;
            }
        }

        public static string TempPath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["TempPath"];
            }
        }

        /// <summary>
        /// SMTP_Server
        /// </summary>
        public static string SMTP_Server
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SMTP_Server"];
            }
        }



        /// <summary>
        /// 消費税
        /// </summary>
        public static string ShouhiZei
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ShouhiZei"];
            }
        }


        /// <summary>
        /// 納品書印刷時の表示行数(1枚目)
        /// </summary>
        public static string Nouhinsho_FirstPageRow
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["Nouhinsho_FirstPageRow"];
            }
        }
        /// <summary>
        /// 納品書印刷時の表示行数(2枚目)
        /// </summary>
        public static string Nouhinsho_ElsePageRow
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["Nouhinsho_ElsePageRow"];
            }
        }

        /// <summary>
        /// 受領書印刷時の表示行数(1枚目)
        /// </summary>
        public static string Jyuryosho_FirstPageRow
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["Jyuryosho_FirstPageRow"];
            }
        }
        /// <summary>
        /// 受領書印刷時の表示行数(2枚目)
        /// </summary>
        public static string Jyuryosho_ElsePageRow
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["Jyuryosho_ElsePageRow"];
            }
        }

        /// <summary>
        /// 受領書印刷時の表示行数(1枚目)
        /// </summary>
        public static string Hacchuusho_FirstPageRow
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["Hacchuusho_FirstPageRow"];
            }
        }
        /// <summary>
        /// 受領書印刷時の表示行数(2枚目)
        /// </summary>
        public static string Hacchuusho_ElsePageRow
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["Hacchuusho_ElsePageRow"];
            }
        }





    }
}