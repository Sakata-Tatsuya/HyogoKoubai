using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace m2mKoubai
{
    public partial class CtlTabMain : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // ログアウト                
                this.TabLO.Attributes["onclick"] =
                    string.Format("if(confirm('ログアウトしますか？'))location.href = '{0}';", Global.LoginPageURL);

                if (!SessionManager.KanrishaFlag)
                {
                    // マスタ管理
                    this.Tab.Tabs[(int)MainMenu.Master].Visible = false;
                }
            }
        }

        // メインメニュー
        public enum MainMenu
        {
            // 発注情報
            Hacchu_Jyouhou = 0,
            // 発注入力
            Hacchu_Nyuuryoku = 1,
            // 検収情報
            Kenshu_Jyouhou = 2,
            // 納品
            Nouhin = 3,
            // パスワード変更
            PassChange = 4,
            // アップロード
            Upload = 5,
            // ダウンロード
            Download = 6,
            // マスタ管理
            Master = 7,
            // ログアウト
            LogOut = 8,
        }
        // マスタ管理メニュー
        public enum Master
        {

            // 仕入先
            Shiiresaki = 0,
            // 担当者
            Account = 1,
            // 部品
            Buhin = 2,
            // メッセージ登録
            Message = 3,
            // 納入場所
            NounyuBasho = 4,
            // 会社情報
            KaishaJyouhou = 5,
        }
        //
        public enum Account
        {
            Shanai = 0,
            Shiiresaki = 1,
        }
        // 検収情報メニュー
        public enum Kenshu_Jyouhou
        {
            // 検収情報一覧
            Yichiran = 0,
            // 検収情報集計
            Shukei = 1,
        }
        // 発注入力メニュー 追加 09/07/24
        public enum Hacchu_Nyuuryoku
        {
            Single = 0,
            Multi = 1,
        }
        // メインメニュー
        public MainMenu Menu
        {
            get
            {
                return (MainMenu)this.Tab.SelectedIndex;
            }
            set
            {
                Tab.SelectedIndex = (int)value;
            }
        }
        // マスタ管理メニュー
        public Master MasterMenu
        {
            get
            {
                return (Master)this.Tab.Tabs[(int)MainMenu.Master].SelectedIndex;
            }
            set
            {
                Tab.Tabs[(int)MainMenu.Master].SelectedIndex = (int)value;
            }
        }
        // アカウントのサブメニュー
        public Account AccoutMenu
        {
            get
            {
                return (Account)this.Tab.Tabs[(int)MainMenu.Master].Tabs[(int)Master.Account].SelectedIndex;
            }
            set
            {
                Tab.Tabs[(int)MainMenu.Master].Tabs[(int)Master.Account].SelectedIndex = (int)value;
            }
        }
        // 検収情報のサブメニュー
        public Kenshu_Jyouhou KenshuMenu
        {
            get
            {
                return (Kenshu_Jyouhou)this.Tab.Tabs[(int)MainMenu.Kenshu_Jyouhou].SelectedIndex;
            }
            set
            {
                Tab.Tabs[(int)MainMenu.Kenshu_Jyouhou].SelectedIndex = (int)value;
            }
        }
        // 発注入力のサブメニュー
        public Hacchu_Nyuuryoku Hacchu_NyuuryokuMenu
        {
            get
            {
                return (Hacchu_Nyuuryoku)this.Tab.Tabs[(int)MainMenu.Hacchu_Nyuuryoku].SelectedIndex;
            }
            set
            {
                Tab.Tabs[(int)MainMenu.Hacchu_Nyuuryoku].SelectedIndex = (int)value;
            }
        }

    }
}