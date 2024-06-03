using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace m2mKoubaiDAL
{
    public class LibError : Core.Error
    {
        public LibError()
            : base()
        {
        }

        public LibError(string strMessage)
            : base(strMessage)
        {
        }

        public LibError(System.Exception e)
            : base(e)
        {
        }
    }

    public enum UserKubun : byte
    {
        Owner = 1,      // ������
        Shiiresaki = 2, // �d����
    }

    //public enum JigyoushoKubun : int
    //{
    //    Himeji = 1, // �P�H
    //    Osaka = 2,  // ���
    //}
    
    public class KanrishaKubun
    {
        public const string Kanrisha = "1";
        public const string None = "";
    }
    

    /// <summary>
    /// ���[�h�^�C���̒P�ʃR�[�h
    /// </summary>
    public enum LeadTimeKubun : byte
    {
        Day = 1, 
        Week = 2, 
        Month = 3, 
        Year = 4, 
    }

    /// <summary>
    /// ���[�h�^�C���̒P��
    /// </summary>
    public class LeadTime_Tani_Txt
    {
        public const string Day = "day";
        public const string Week = "week";
        public const string Month = "month";
        public const string Year = "year";
    }

    /// <summary>
    /// ���[�h�^�C���̓���
    /// </summary>
    public enum LeadTime_Nissu : int
    {
        Day = 1,
        Week = 7,
        Month = 30,
        Year = 365,
    }

}
