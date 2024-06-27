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
        /// ���[�����M���ɕK�v�ȍ���
        /// (���[�����M���A���̃C���X�^���X���쐬���A�S�Ă̍��ڂɒl���Z�b�g����)
        /// </summary>
        public class MailParam
        {
            public string _MailTo = "";   // ���[�����M��
            public string _MailFrom = ""; // ���[�����M��
            public string _ToCC = "";     // CC
            public string _Subject = "";  // ����
            public string _Body = "";     // �{��
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

                    // ���M���Ƒ��M���\����
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

                    // html�`���ɂ���
                    mm.IsBodyHtml = false;

                    System.Net.Mail.SmtpClient sc = new System.Net.Mail.SmtpClient(p._SMTP_Server);

                    sc.Send(mm);

                    // �����������������Ƀ��[�����M
                    System.Net.Mail.MailMessage mm2 = new System.Net.Mail.MailMessage();
                    mm2.From = new System.Net.Mail.MailAddress("dev@m2m-asp.com");
                    mm2.To.Add("sakata@m2m-asp.com");
                    mm2.Subject = "���M����";
                    mm2.Body = p._Body;

                    // html�`���ɂ���
                    mm2.IsBodyHtml = false;

                    System.Net.Mail.SmtpClient sc2 = new System.Net.Mail.SmtpClient(p._SMTP_Server);
                    sc2.Send(mm2);
                }
                /*
                else
                {
                    System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();
                    // ���M���Ƒ��M���\����
                    mm.From = new System.Net.Mail.MailAddress("dev@m2m-asp.com");
                    mm.To.Add("sakata@m2m-asp.com");
                    mm.Subject = "���M���s";
                    mm.Body = "���M��: " + p._MailFrom + "\r\n\r\n"
                                + "���M��: " + p._MailTo + "\r\n\r\n"
                                + "����: " + p._Subject + "\r\n\r\n"
                                + "���e: " + p._Body + "\r\n\r\n"
                                + "SMTP: " + p._SMTP_Server + "\r\n\r\n";
                    //+ "������ID: " + strLoginID + "\r\n\r\n"
                    //+ "�d����R�[�h: " + strShiire;

                    // html�`���ɂ���
                    mm.IsBodyHtml = false;

                    System.Net.Mail.SmtpClient sc = new System.Net.Mail.SmtpClient(p._SMTP_Server);
                    sc.Send(mm);
                }
                */
            }
            catch (Exception e)
            {
                System.Net.Mail.MailMessage mm1 = new System.Net.Mail.MailMessage();
                // ���M���Ƒ��M���\����
                mm1.From = new System.Net.Mail.MailAddress("dev@m2m-asp.com");
                mm1.To.Add("sakata@m2m-asp.com");
                mm1.Subject = "���M���s";
                mm1.Body = "���M��: " + p._MailFrom + "\r\n\r\n"
                            + "���M��: " + p._MailTo + "\r\n\r\n"
                            + "����: " + p._Subject + "\r\n\r\n"
                            + "���e: " + p._Body + "\r\n\r\n"
                            + "SMTP: " + p._SMTP_Server + "\r\n\r\n"
                            + "��O�̗��R�F" + e.ToString();
                // + "������ID: " + strLoginID + "\r\n\r\n"
                //+ "�d����R�[�h: " + strShiire;

                // html�`���ɂ���
                mm1.IsBodyHtml = false;

                System.Net.Mail.SmtpClient sc1 = new System.Net.Mail.SmtpClient(p._SMTP_Server);
                sc1.Send(mm1);
            }
        }

        /// <summary>
        /// ���[���{���i�����̎���j
        /// </summary>
        /// <returns></returns>
        public static string GetBody_Cancel(ChumonDataSet.V_MailInfoRow dr)
        {
            string strBody =
                "���������b�ɂȂ��Ă��܂��B\r\n"
                + "��Ђ��āA�����i�ɑ΂������̎�����s���܂����B\r\n"
                + "���LURL�Ƀ��O�C�����������A���e�̂��m�F�����肢�v���܂��B\r\n"
                + _SystemURL + "\r\n"
                + "**************************************************************\r\n"
                + "�����̃��[���Ɋւ��邨�₢���킹�́A���L�S���҂܂ł��A���������B\r\n"
                + dr.KaishaMei + " " + dr.EigyouSho + "\r\n"
                + dr.Name + "\r\n"
                + "TEL�F" + dr.Tel + "\r\n"
                + "FAX�F" + dr.Fax + "\r\n"
                + "E-mail�F" + dr.Mail_Y;

            return strBody;

        }

        /// <summary>
        /// ���[���{��(�V�K�������������[��)
        /// </summary>
        /// <returns></returns>
        public static string GetBody_ShinkiChumon(ChumonDataSet.V_MailInfoRow dr)
        {
            string strBody =
                "���������b�ɂȂ��Ă��܂��B\r\n"
                + "��Ђ��āA�����������ē��v���܂��B\r\n"
                + "���LURL�Ƀ��O�C�����������A���e�̂��m�F�����肢�v���܂��B\r\n"
                + "�܂��A�{�����ɑ΂��[���񓚂̓o�^�����肢���܂��B\r\n"
                + _SystemURL + "\r\n"
                + "�[���񓚂�1�T�ԓ��ɓo�^����Ă��Ȃ��ꍇ\r\n"
                + "�����I�ɍÑ����[�����z�M����܂��B\r\n"
                + "**************************************************************\r\n"
                + "�����̃��[���Ɋւ��邨�₢���킹�́A���L�S���҂܂ł��A���������B\r\n"
                + dr.KaishaMei + " " + dr.EigyouSho + "\r\n"
                + dr.Name + "\r\n"
                + "TEL�F" + dr.Tel + "\r\n"
                + "FAX�F" + dr.Fax + "\r\n"
                + "E-mail�F" + dr.Mail_Y;

            return strBody;

        }

        /// <summary>
        /// ���[���{��(�������ʂ̕ύX)
        /// </summary>
        /// <returns></returns>
        public static string GetBody_Suuryou(ChumonDataSet.V_MailInfoRow dr)
        {
            string strBody =
                "���������b�ɂȂ��Ă��܂��B\r\n"
                + "��Ђ��āA�����i�ɑ΂��������ʂ̕ύX���s���܂����B\r\n"
                + "���LURL�Ƀ��O�C�����������A���e�̂��m�F�����肢�v���܂��B\r\n"
                + _SystemURL + "\r\n"
                + "**************************************************************\r\n"
                + "�����̃��[���Ɋւ��邨�₢���킹�́A���L�S���҂܂ł��A���������B\r\n"
                + dr.KaishaMei + " " + dr.EigyouSho + "\r\n"
                + dr.Name + "\r\n"
                + "TEL�F " + dr.Tel + "\r\n"
                + "FAX �F" + dr.Fax + "\r\n"
                + "E-mail�F" + dr.Mail_Y;

            return strBody;

        }

        /// <summary>
        /// ���[���{��(�w��[���ύX���̃��[��)
        /// </summary>
        /// <returns></returns>
        public static string GetBody_NoukiHenkou(ChumonDataSet.V_MailInfoRow dr)
        {
            string strBody =
              "���������b�ɂȂ��Ă��܂��B\r\n"
              + "��Ђ��āA�����i�ɑ΂��w��[���̕ύX��o�^���Ă��܂��B\r\n"
              + "���LURL�Ƀ��O�C�����������A���e�̂��m�F�����肢�v���܂��B\r\n"
              + "�܂��A�ύX�[���ɑ΂��񓚂̓o�^�����肢���܂��B\r\n"
              + _SystemURL + "\r\n"
              + "�[���񓚂�1�T�ԓ��ɓo�^����Ă��Ȃ��ꍇ\r\n"
              + "�����I�ɍÑ����[�����z�M����܂��B\r\n"
              + "**************************************************************\r\n"
              + "�����̃��[���Ɋւ��邨�₢���킹�́A���L�S���҂܂ł��A���������B\r\n"
              + dr.KaishaMei + " " + dr.EigyouSho + "\r\n"
              + dr.Name + "\r\n"
              + "TEL�F" + dr.Tel + "\r\n"
              + "FAX�F" + dr.Fax + "\r\n"
              + "E-mail�F" + dr.Mail_Y;

            return strBody;
        }

        /// <summary>
        /// ���[���{��(�y��Џ��X�V���[���z)
        /// </summary>
        /// <returns></returns>
        public static string GetBody_KaishaInfo_Koushin(ChumonDataSet.V_Chumon_MailRow dr, m2mKoubaiDataSet.M_ShiiresakiRow drShiire)
        {
            string strBody =
             "��Џ�񂪍X�V����܂����B\r\n"
             + "���LURL�Ƀ��O�C�����������A���e�̂��m�F�����肢�v���܂��B\r\n"
             + "�d����R�[�h�F" + drShiire.ShiiresakiCode + "\r\n"
             + "�d���於�F" + drShiire.ShiiresakiMei + "\r\n"
             + _SystemURL + "\r\n";

            return strBody;
        }

        /// <summary>
        /// ���[���{��(�[���񓚓o�^���̃��[��)
        /// </summary>
        /// <returns></returns>
        public static string GetBody_NoukiKaitou_Shinki(m2mKoubaiDataSet.M_ShiiresakiRow dr)
        {
            string strBody =
              "�[���񓚂��o�^����܂����B\r\n"
              + "���LURL�Ƀ��O�C�����������A���e�̂��m�F�����肢�v���܂��B\r\n"
              + "�d����R�[�h�F" + dr.ShiiresakiCode + "\r\n"
              + "�d���於�F" + dr.ShiiresakiMei + "\r\n"
              + _SystemURL + "\r\n";
            return strBody;
        }

        /// <summary>
        /// ���[���{��(�[���񓚕ύX���̃��[��)
        /// </summary>
        /// <returns></returns>
        public static string GetBody_NoukiKaitou_Koushin(m2mKoubaiDataSet.M_ShiiresakiRow dr)
        {
            string strBody =
              "�[���񓚂��ύX����܂����B\r\n"
              + "���LURL�Ƀ��O�C�����������A���e�̂��m�F�����肢�v���܂��B\r\n"
              + "�d����R�[�h�F" + dr.ShiiresakiCode + "\r\n"
              + "�d���於�F" + dr.ShiiresakiMei + "\r\n"
              + _SystemURL + "\r\n";
            return strBody;
        }
    }
}

