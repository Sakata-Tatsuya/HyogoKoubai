using System;
using System.Runtime.Serialization;
using System.Web;

using KoubaiDAL;


namespace Koubai
{
    public class SessionManager
    {
        public const string SESSION_LOGIN_ID = "SESSION_LOGIN_ID";     �@�@     // ���O�C��ID
        public const string SESSION_USER_KUBUN = "SESSION_USER_KUBUN";     �@�@ // ���[�U�[�敪
        public const string SESSION_KAISHA_CODE = "SESSION_KAISHA_CODE";�@�@�@�@// ��ЃR�[�h
        public const string SESSION_JIGYOUSHO_KUBUN = "SESSION_JIGYOUSHO_KUBUN"; // ���Ə��敪
        public const string SESSION_KANRISHA_FLAG = "SESSION_KANRISHA_FLAG"; �@ // �}�X�^�Ǘ��ҋ敪
        public const string SESSION_TANTOUSHA_CODE = "SESSION_TANTOUSHA_CODE";�@// �S���҃R�[�h
        public const string SESSION_TANTOUSHA_NAME = "SESSION_TANTOUSHA_NAME";�@// �S���Җ�
        public const string SESSION_USER = "SESSION_LOGIN_USER";

        public const string SESSION_KENSYU_FLAG = "SESSION_KENSYU_FLAG"; �@     // �������
        public const string SESSION_KAISHA_KOUSHIN_FLG = "SESSION_KAISHA_KOUSHIN_FLG";       // ��Џ�񋖉t���O
        /// <summary>
        /// ���O�C�������Z�b�V�����Ɋi�[
        /// </summary>
        /// <param name="dr"></param>
        public static void Login(KoubaiDataSet.M_LoginRow dr, string strLanguage)
        {
            System.Web.Security.FormsAuthentication.SetAuthCookie(dr.LoginID, false);
            System.Web.HttpContext.Current.Session[SESSION_USER_KUBUN] = dr.UserKubun;              // ���[�U�[�敪
            System.Web.HttpContext.Current.Session[SESSION_TANTOUSHA_CODE] = dr.TantoushaCode;      // �S���҃R�[�h
            System.Web.HttpContext.Current.Session[SESSION_KANRISHA_FLAG] = dr.KanrishaFlg;         // �}�X�^�Ǘ��ҋ敪
            System.Web.HttpContext.Current.Session[SESSION_LOGIN_ID] = dr.LoginID;                  // ���O�C��ID
            System.Web.HttpContext.Current.Session[SESSION_TANTOUSHA_NAME] = dr.Name;               // �S���Җ�
            System.Web.HttpContext.Current.Session[SESSION_KAISHA_CODE] = dr.KaishaCode;            // ��ЃR�[�h
            System.Web.HttpContext.Current.Session[SESSION_JIGYOUSHO_KUBUN] = dr.JigyoushoKubun;    // ���Ə��敪
            LoginUser u = LoginUser.New(dr.LoginID, strLanguage);
            if (null != u)
            {
                System.Web.HttpContext.Current.Session[SESSION_USER] = u;
            }
        }

        public class LoginUser : Core.Web.WebUser
        {
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {

            }

            private KoubaiDataSet.M_LoginRow _drUser = null;
            //private MasterDataSet.M_TorihikisakiRow _drTorihikisaki = null;

            private System.Collections.Hashtable _tblSessionData = new System.Collections.Hashtable();
            private System.Collections.Hashtable _tblUserView = new System.Collections.Hashtable(); // ���g�̕\���ݒ�(�L�[�̓��X�gID)

            public string UserID
            {
                get
                {
                    return this._drUser.LoginID;
                }
            }

            public string UserName
            {
                get
                {
                    return this._drUser.Name;
                }
            }
            //public string UserBusho
            //{
            //    get
            //    {
            //        if (null == _drTantousha)
            //            return "";
            //        return this._drTantousha.BushoCode;
            //    }
            //}

            public KoubaiDataSet.M_LoginRow M_User
            {
                get { return this._drUser; }
                set { this._drUser = value; }
            }

            //public MasterDataSet.M_TorihikisakiRow M_TorihikisakiRow
            //{
            //    get { return this._drTorihikisaki; }
            //    set { this._drTorihikisaki = value; }
            //}

            public string TwoLetterISOLanguageName
            {
                get;
                set;
            }

            public System.Globalization.CultureInfo CultureInfo
            {
                get
                {
                    string str = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
                    switch (this.TwoLetterISOLanguageName)
                    {
                        case "ja":
                            str = "ja-JP";
                            break;
                        case "en":
                            str = "en-US";
                            break;
                        case "zh":
                            str = "zh-CN";
                            break;
                    }
                    return new System.Globalization.CultureInfo(str);
                }
            }
            public string PDFFontName
            {
                get
                {
                    switch (this.TwoLetterISOLanguageName)
                    {
                        case "ja": return "�l�r �S�V�b�N";
                        case "en": return "Times-Roman";
                        case "zh": return "SimSun";
                    }
                    return "�l�r �S�V�b�N";
                }
            }

            private LoginUser()
            {

            }

            public static LoginUser New(string strUserID, string strTwoLetterISOLanguageName)
            {
                LoginUser u = new LoginUser();
                u._drUser = LoginClass.getM_LoginRow(strUserID, Global.GetConnection());
                if (null == u._drUser) return null;

                u.TwoLetterISOLanguageName = strTwoLetterISOLanguageName;

                return u;
            }

            public string Honyaku(string str)
            {
                return Global.HonyakuManager.GetString(str, this.TwoLetterISOLanguageName);
            }

            /// <summary>
            /// ���ݎg�p���Ă��錾��
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public string LanguageCode
            {
                get
                {
                    return this.TwoLetterISOLanguageName;
                }
            }

        }
        /// <summary>
        /// �d����̐ݒ�
        /// </summary>
        /// <param name="drKensyu"></param>
        public static void LoginShiiresaki(LoginDataSet.V_Shiiresaki_FlgRow drKensyu)
        {
            System.Web.HttpContext.Current.Session[SESSION_KENSYU_FLAG] = drKensyu.KensyukoukaiFlg;
            System.Web.HttpContext.Current.Session[SESSION_KAISHA_KOUSHIN_FLG] = drKensyu.KousinKyokaFlg;
        }

        // ���[�U�敪
        public static byte UserKubun
        {
            get
            {
                try
                {
                    object obj = System.Web.HttpContext.Current.Session[SESSION_USER_KUBUN];
                    if (null == obj) throw new Exception("SessionOut");
                    return Convert.ToByte(obj);
                }
                catch
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return 0;
                }
            }
        }
        // ��ЃR�[�h
        public static string KaishaCode
        {
            get
            {
                try
                {
                    object obj = System.Web.HttpContext.Current.Session[SESSION_KAISHA_CODE];
                    if (null == obj) throw new Exception("SessionOut");
                    return Convert.ToString(obj);
                }
                catch
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return null;
                }
            }
        }
        // ���Ə��敪
        public static int JigyoushoKubun
        {
            get
            {
                try
                {
                    object obj = System.Web.HttpContext.Current.Session[SESSION_JIGYOUSHO_KUBUN];
                    if (null == obj) throw new Exception("SessionOut");
                    return Convert.ToInt32(obj);
                }
                catch
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return 0;
                }
            }
        }
        // ���O�C��ID
        public static string LoginID
        {
            get
            {
                try
                {
                    object obj = System.Web.HttpContext.Current.Session[SESSION_LOGIN_ID];
                    if (null == obj) throw new Exception("SessionOut");
                    string strLoginID = Convert.ToString(obj);
                    if (null == strLoginID || "" == strLoginID) throw new Exception("SessionOut");
                    return strLoginID;
                }
                catch
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return null;
                }
            }
        }
        public static LoginUser User
        {
            get
            {
                try
                {
                    object obj = System.Web.HttpContext.Current.Session[SESSION_USER];
                    if (null == obj) throw new Exception("SessionOut");
                    return obj as LoginUser;
                }
                catch
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return null;
                }
            }
        }

        // �S���҃R�[�h
        public static string TantoushaCode
        {
            get
            {
                try
                {
                    object obj = System.Web.HttpContext.Current.Session[SESSION_TANTOUSHA_CODE];

                    if (null == obj) throw new Exception("SessionOut");
                    string strTantoushaCode = Convert.ToString(obj);
                    if (null == strTantoushaCode || "" == strTantoushaCode) throw new Exception("SessionOut");
                    return strTantoushaCode;
                }
                catch
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return null;
                }
            }
        }
        // �S���Җ�
        public static string TantoushaName
        {
            get
            {
                try
                {
                    object obj = System.Web.HttpContext.Current.Session[SESSION_TANTOUSHA_NAME];

                    if (null == obj) throw new Exception("SessionOut");
                    string strTantoushaName = Convert.ToString(obj);
                    if (null == strTantoushaName || "" == strTantoushaName) throw new Exception("SessionOut");
                    return strTantoushaName;
                }
                catch
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return null;
                }
            }
        }
       
       

        // �Ǘ��Ҍ���
        public static bool KanrishaFlag
        {
            get
            {
                try
                {
                    object obj = System.Web.HttpContext.Current.Session[SESSION_KANRISHA_FLAG];
                    if (null == obj) throw new Exception("SessionOut");
                    return Convert.ToBoolean(obj);
                }
                catch
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return false;
                }
            }
        }

        //�������
        public static bool KensyukoukaiFlg
        {
            get
            {
                try
                {
                    object obj = System.Web.HttpContext.Current.Session[SESSION_KENSYU_FLAG];
                    if (null == obj) throw new Exception("SessionOut");
                    return Convert.ToBoolean(obj);
                }
                catch
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return false;
                }
            }
        }

        //�������
        public static bool KaishaKoushinFlg
        {
            get
            {
                try
                {
                    object obj = System.Web.HttpContext.Current.Session[SESSION_KAISHA_KOUSHIN_FLG];
                    if (null == obj) throw new Exception("SessionOut");
                    return Convert.ToBoolean(obj);
                }
                catch
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return false;
                }
            }
        }

        // ���O�A�E�g
        public static void Logout()
        {
            System.Web.HttpContext.Current.Session.Abandon();
            System.Web.Security.FormsAuthentication.SignOut();
        }
    }
}
