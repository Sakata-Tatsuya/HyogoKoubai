using Core.Type;
using System;
using System.Web.UI.WebControls;

namespace m2mKoubai.Common
{
    public partial class CtlNengappiFromTo : System.Web.UI.UserControl
    {
        protected string GetResource(string str)
        {
            return SessionManager.User.Honyaku(str);
        }

        public Telerik.Web.UI.RadDatePicker RadDatePickerFrom
        {
            get
            {
                return RdpFrom;
            }
        }

        public Telerik.Web.UI.RadDatePicker RadDatePickerTo
        {
            get
            {
                return RdpTo;
            }
        }

        public Nengappi From
        {
            get
            {
                if (!RdpFrom.SelectedDate.HasValue)
                    return null;
                return new Nengappi((DateTime)RdpFrom.SelectedDate);
            }
            set
            {
                if (null != value)
                {
                    RdpFrom.SelectedDate = value.ToDateTime();
                }
            }
        }

        public Nengappi To
        {
            get
            {
                if (!RdpTo.SelectedDate.HasValue)
                    return null;
                return new Nengappi((DateTime)RdpTo.SelectedDate);
            }
            set
            {
                if (null != value)
                {
                    RdpTo.SelectedDate = value.ToDateTime();
                }
            }
        }

        /// <summary>
        /// カレンダーの共有
        /// ページサイズ削減の為
        /// </summary>
        public Telerik.Web.UI.RadCalendar SharedCalendar
        {
            set
            {
                this.RdpFrom.SharedCalendar = this.RdpTo.SharedCalendar = value;
            }
        }

        public Core.Type.NengappiKikan.EnumKikanType KikanType
        {
            get
            {
                return (Core.Type.NengappiKikan.EnumKikanType)int.Parse(this.DdlKikan.SelectedValue);
            }
            set
            {
                Core.Web.Utility.ListControl_SelectItemByValue(this.DdlKikan, ((int)value).ToString());
            }
        }

        public void RemoveFromTo()
        {
            ListItem item = this.DdlKikan.Items.FindByValue("4");
            if (null != item) this.DdlKikan.Items.Remove(item);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //
        }

        public NengappiKikan GetNengappiKikan()
        {
            if (this.KikanType == Core.Type.NengappiKikan.EnumKikanType.NONE) return null;
            if (this.From == null || this.To == null)
            {
                return null;
            }
            var k = new NengappiKikan();

            k.From = this.From;

            k.To = this.To;

            k.KikanType = this.KikanType;

            return k;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!this.RdpFrom.SelectedDate.HasValue)
                this.RdpFrom.SelectedDate = DateTime.Today;
            if (!this.RdpTo.SelectedDate.HasValue)
                this.RdpTo.SelectedDate = DateTime.Today;

            // ajaxで本コントロールがクライアントにロードされた時、
            // HTML内に書いたjavascriptが反映されない問題があり、また本コントロールの複数使用によるjvascript関数の重複をしないように
            // javascriptはタグの中に記述するようにした。
            string strDdlKikanOnChange =
                @"
	document.getElementById('{0}').style.display = ('0' == this.options[this.selectedIndex].value)? 'none' : '';
	document.getElementById('{1}').style.display = ('4' == this.options[this.selectedIndex].value)? '' : 'none';
";
            this.DdlKikan.Attributes["onchange"] =
                string.Format(strDdlKikanOnChange, this.TblFrom.ClientID, this.TblTo.ClientID);

            if ("4" != this.DdlKikan.SelectedValue)
                TblTo.Style.Add("display", "none");
            else
                TblTo.Style.Add("display", "");

            if ("0" == this.DdlKikan.SelectedValue)
                TblFrom.Style.Add("display", "none");
            else
                TblFrom.Style.Add("display", "");

            // 2007/1/1は中国語Cultureでは2007-1-1だが、
            // 毎回ここで設定してやらないと、postback時に標準設定に戻ってしまう。
            this.RdpFrom.Culture = this.RdpTo.Culture = SessionManager.User.CultureInfo;
            if (null != this.RdpFrom.SharedCalendar) this.RdpFrom.SharedCalendar.CultureInfo = SessionManager.User.CultureInfo;
            if (null != this.RdpTo.SharedCalendar) this.RdpFrom.SharedCalendar.CultureInfo = SessionManager.User.CultureInfo;

            this.RadDatePickerFrom.DatePopupButton.ToolTip = this.RadDatePickerTo.DatePopupButton.ToolTip = this.GetResource("カレンダーを表示します。");
        }

    }
}
