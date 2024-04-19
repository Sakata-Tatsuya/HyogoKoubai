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

namespace m2mKoubai
{
    public class AppCommon
    {

        // ���Ђ̉�ЃR�[�h
        public const string Jisha_KAISHACODE = "0";

        public const string NO_DATA = "�Y������f�[�^�͂���܂���";


        /// <summary>
        /// �L��/�����̕\��
        /// </summary>
        /// <param name="bSakujo"></param>
        /// <returns></returns> 
        public static string YukouMukouText(bool bSakujo)
        {
            if (bSakujo)
                return "����";
            else
                return "�L��";
        }
       
        // �����ȊO�̓��͕s��
        public static void NumOnly(TextBox tbx)
        {
            tbx.Attributes["onkeydown"] = "return KeyCodeCheck();";
        }

        /// <summary>
        /// �i�ڂ̊���Ȗږ�
        /// </summary>
        /// <param name="nKamokuCode"></param>
        /// <returns></returns>
        public static string KamokuMei(int nKamokuCode)
        {
            if (nKamokuCode == 174)
                return "���ޗ�";
            else if (nKamokuCode == 176)
                return "�����i";
            else
                return "";
        }

        /// <summary>
        /// �i�ڂ̔�p����Ȗږ�
        /// </summary>
        /// <param name="nHiyouCode"></param>
        /// <returns></returns>
        public static string HiyouMei(int nHiyouCode)
        {
            if (nHiyouCode == 723)
                return "������";
            else if (nHiyouCode == 752)
                return "���Օi��";
            else if (nHiyouCode == 722)
                return "�⏕�ޗ���";
            else if (nHiyouCode == 740)
                return "���R����";
            else
                return "";
        }

        /// <summary>
        /// �i�ڂ̕⏕����Ȗږ�
        /// </summary>
        /// <param name="nHojyoNo"></param>
        /// <returns></returns>
        public static string HojyoMei(int nHojyoNo)
        {
            if (nHojyoNo == 1)
                return "�匴����";
            else if (nHojyoNo == 2)
                return "��������";
            else if (nHojyoNo == 3)
                return "���R����";
            else if (nHojyoNo == 4)
                return "����ޗ���";
            else if (nHojyoNo == 5)
                return "SP�t���i";
            else if (nHojyoNo == 6)
                return "���ڏ��Օi��";
            else
                return "";
        }



        //�x������
        public static string ShiharaiShimebi(int Shimebi)
        {
            switch (Shimebi)
            {

                case 0:
                    return "����";
                case 1:
                    return "5��";
                case 2:
                    return "10��";
                case 3:
                    return "15��";
                case 4:
                    return "20��";
                case 5:
                    return "25��";
                default:
                    return "������";
            }
        
        }

        //�x���\���
        public static string ShiharaiYoteibi(int Yoteibi)
        {
            switch (Yoteibi)
            {
                case 0:
                    return "��������";
                case 1:
                    return "����5��";
                case 2:
                    return "����10��";
                case 3:
                    return "����15��";
                case 4:
                    return "����20��";
                case 5:
                    return "����25��";
                case 6:
                    return "����������";
                case 7:
                    return "������5��";
                case 8:
                    return "������10��";
                case 9:
                    return "������15��";
                case 10:
                    return "������20��";
                case 11:
                    return "������25��";
                default:
                    return "������";
            }

        

        }

        public static int ShiharaiShimebiUp(string Shimebi)
        {
            if (Shimebi == "����")
                return 0;
            if (Shimebi == "5��")
                return 1;
            if (Shimebi == "10��")
                return 2;
            if (Shimebi == "15��")
                return 3;
            if (Shimebi == "20��")
                return 4;
            if (Shimebi == "25��")
                return 5;
            else
                return 6;
         
        }

        public static int ShiharaiYoteibiUp(string Yoteibi)
        {
            if (Yoteibi == "10")
                return 0;
            if (Yoteibi == "11")
                return 1;
            if (Yoteibi == "12")
                return 2;
            if (Yoteibi == "13")
                return 3;
            if (Yoteibi == "14")
                return 4;
            if (Yoteibi == "15")
                return 5;
            if (Yoteibi == "20")
                return 6;
            if (Yoteibi == "21")
                return 7;
            if (Yoteibi == "22")
                return 8;
            if (Yoteibi == "23")
                return 9;
            if (Yoteibi == "24")
                return 10;
            if (Yoteibi == "25")
                return 11;
            else
                return 12;
        }

        public static string YoteibiDdl1Select(int Yoteibi)
        {
             switch (Yoteibi)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    return "1";
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                    return "2";
                default:
                    return "0";
            }
        }

        public static string YoteibiDdl2Select(int Yoteibi)
        {           
            switch (Yoteibi)
            {
                case 0:
                case 6:
                    return "0";
                case 1:
                case 7:
                    return "1";
                case 2:
                case 8:
                    return "2";
                case 3:
                case 9:
                    return "3";
                case 4:
                case 10:
                    return "4";
                case 5:
                case 11:
                    return "5";
                /*
                case 6:
                    return "6";
                    break;
                case 7:
                    return "7";
                    break;
                case 8:
                    return "8";
                    break;
                case 9:
                    return "9";
                    break;
                case 10:
                    return "10";
                    break;
                case 11:
                    return "11";
                    break;
                */ 
                default:
                    return "6";
            }
        }

        /// <summary>
        /// ���[�h�^�C���̒P�ʂ̕\���`��
        /// </summary>
        /// <param name="Shimebi"></param>
        /// <returns></returns>
        public static string LT_Tani(byte bLT_Tani)
        {
            switch (bLT_Tani)
            {
                case 1:
                    return LeadTime_Tani_Txt.Day;
                case 2:
                    return LeadTime_Tani_Txt.Week;
                case 3:
                    return LeadTime_Tani_Txt.Month;
                case 4:
                    return LeadTime_Tani_Txt.Year;
                default:
                    return "---";
            }
        }

        /// <summary>
        /// ���[�h�^�C���̂��������
        /// </summary>
        /// <param name="Shimebi"></param>
        /// <returns></returns>
        public static int LT_Suuji(byte bLT_Tani)
        {
            if (bLT_Tani == (byte)LeadTimeKubun.Day)
            {
                return (int)LeadTime_Nissu.Day;
            }
            else if (bLT_Tani == (byte)LeadTimeKubun.Week)
            {
                return (int)LeadTime_Nissu.Week;
            }
            else if (bLT_Tani == (byte)LeadTimeKubun.Month)
            {
                return (int)LeadTime_Nissu.Month;
            }
            else if (bLT_Tani == (byte)LeadTimeKubun.Year)
            {
                return (int)LeadTime_Nissu.Year;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// ���[�h�^�C������[��������o��
        /// </summary>
        /// <param name="nLT_Suuji"></param>
        /// <param name="bLT_Tani"></param>
        /// <returns></returns>
        public static DateTime GetNouki(int nLT_Suuji, byte bLT_Tani)
        {
            // �������v�Z����
            int nAddDays = nLT_Suuji * LT_Suuji(bLT_Tani);
            // ���݂̓��t��
            DateTime dateForm = DateTime.Now;
            // ���[�h�^�C���̓��t��
            DateTime dateTo = DateTime.Now.AddDays(nAddDays);
            // ���t����j���ɕϊ�
            DayOfWeek weekForm = dateForm.DayOfWeek;
            // ���t����j���ɕϊ�
            DayOfWeek weekTo = dateTo.DayOfWeek;
            // �T�Ԑ����擾
            int n = nAddDays / 7;         
            if (n == 0) // ��T�Ԗ����̏ꍇ
            {
                // ���`�y�A�΁`���A���`���̏ꍇ�́A���� + 1
                if ((weekForm == DayOfWeek.Monday && weekTo == DayOfWeek.Saturday) ||
                    (weekForm == DayOfWeek.Sunday && weekTo == DayOfWeek.Friday)�@|| 
                    (weekTo == DayOfWeek.Saturday))
                {
                    nAddDays++;
                }
                // ���`���A�؁`�΁A�y�`�؂̏ꍇ�́A���� + 2
                else if ((weekForm == DayOfWeek.Wednesday && weekTo == DayOfWeek.Monday) ||
                    (weekForm == DayOfWeek.Thursday && weekTo == DayOfWeek.Tuesday) ||
                      (weekForm == DayOfWeek.Tuesday && weekTo == DayOfWeek.Sunday) ||
                    (weekForm == DayOfWeek.Saturday && weekTo == DayOfWeek.Thursday) ||
                    (weekTo == DayOfWeek.Sunday))
                {
                    nAddDays = nAddDays + 2;
                }
            �@  
            }
            else// ��T�Ԉȏ�̏ꍇ
            {
                int nNew = nAddDays % 7;                
                if (nNew == 0)
                {

                    // �[�����y�̏ꍇ
                    if (weekForm == DayOfWeek.Saturday)
                        nAddDays = nAddDays + ((n + 1) * 2 - 1);
                    // �[�������̏ꍇ
                    else if (weekForm == DayOfWeek.Sunday)
                        nAddDays = nAddDays + ((n + 1) * 2);
                    else
                        nAddDays = nAddDays + n * 2;
                }
                else
                {
                    // �[�����y�̏ꍇ
                    if (weekTo == DayOfWeek.Saturday)
                        nAddDays = nAddDays + (n * 2 + 1);
                    // �[�������̏ꍇ
                    else if (weekTo == DayOfWeek.Sunday)
                        nAddDays = nAddDays + ((n + 1) * 2);
                    else
                        nAddDays = nAddDays + n * 2;
                }              

            }
            // �ŏI�[�����y�j���̏ꍇ�A�ŏI�[���{2
            if(DateTime.Now.AddDays(nAddDays).DayOfWeek == DayOfWeek.Saturday)
                return DateTime.Now.AddDays(nAddDays + 2);
            // �ŏI�[�������j���̏ꍇ�A�ŏI�[���{1
            else if (DateTime.Now.AddDays(nAddDays).DayOfWeek == DayOfWeek.Sunday)
                return DateTime.Now.AddDays(nAddDays + 1);
            else
                return DateTime.Now.AddDays(nAddDays);

               
        }

        

        // ���Ӑ�́Y���ɂ����ԍ쐬
        public static void CreateKikan(int nYear, int nMonth, int nShimeBi, ref int nSNen, ref int nENen)
        {
            // �Y���ɂ���ĕω� 
            int[] nDayAry = new int[2];

            int[] nDayAry1 = { 1, 31 };
            int[] nDayAry2 = { 6, 5 };
            int[] nDayAry3 = { 11, 10 };
            int[] nDayAry4 = { 16, 15 };
            int[] nDayAry5 = { 21, 20 };
            int[] nDayAry6 = { 26, 25 };

            if (nShimeBi == (int)ShiiresakiClass.ShimeBi.MATUJITU)
                nDayAry = nDayAry1;
            else if (nShimeBi == (int)ShiiresakiClass.ShimeBi.GO)
                nDayAry = nDayAry2;
            else if (nShimeBi == (int)ShiiresakiClass.ShimeBi.JYU)
                nDayAry = nDayAry3;
            else if (nShimeBi == (int)ShiiresakiClass.ShimeBi.JYUGO)
                nDayAry = nDayAry4;
            else if (nShimeBi == (int)ShiiresakiClass.ShimeBi.NIJYU)
                nDayAry = nDayAry5;
            else
                nDayAry = nDayAry6;

            /*
            // �J�n����I���܂ł̓��t 
            int nUseYear = nYear;
            int nUseMonth = nMonth;
            */

            // 15���A20���A25�����Y���̏ꍇ
            if (nShimeBi == (int)ShiiresakiClass.ShimeBi.JYUGO || nShimeBi == (int)ShiiresakiClass.ShimeBi.NIJYU ||
                nShimeBi == (int)ShiiresakiClass.ShimeBi.NIJYUGO)
            {
                // �N���𒲐�      
                SetYearMonth(ref nYear, ref nMonth);
            }

            nSNen = CreateNengappi(nYear, nMonth, nDayAry[0]);
            if (nDayAry[1] < nDayAry[0])
            {
                // �N���𒲐�  
                SetYearMonthPlus(ref nYear, ref nMonth);
            }

            // ���ێg�p���錟����
            int nDay = CreateDay(nYear, nMonth, nDayAry[1]);
            nENen = CreateNengappi(nYear, nMonth, nDay);
        }

        private static int CreateNengappi(int nYear, int nMonth, int nDay)
        {
            return int.Parse(string.Format("{0:0000}{1:00}{2:00}", nYear, nMonth, nDay));
        }

        // �N������
        public static void SetYearMonth(ref int nYear, ref int nMonth)
        {
            nMonth--;
            if (nMonth == 0)
            {
                nMonth = 12;
                nYear -= 1;
            }
        }

        // �N������
        public static void SetYearMonthPlus(ref int nYear, ref int nMonth)
        {
            nMonth++;
            if (nMonth == 13)
            {
                nMonth = 1;
                nYear += 1;
            }
        }

        // ���ۂɎg�p�������Ԃ�
        private static int CreateDay(int nYear, int nMonth, int nDay)
        {
            int nNewDay = 0;
            if (nDay == 31)
            {
                nNewDay = DateTime.DaysInMonth(nYear, nMonth);
            }
            else
            {
                nNewDay = nDay;
            }

            return nNewDay;
        }

    }
}
