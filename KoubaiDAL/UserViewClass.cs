using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Core;
using Core.Type;

namespace KoubaiDAL
{
    public class UserViewClass
    {
        public enum EnumType
        {
            VIEW = 0, GROUP_VIEW = 1,
        }

        public static ShareDataSet.T_UserViewDataTable
            getT_UserViewDataTable(int nListID, string strUserID, EnumType type, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "select * from T_UserView where ListID=@id and UserID=@u and Type=@tp";
            da.SelectCommand.Parameters.AddWithValue("@id", nListID);
            da.SelectCommand.Parameters.AddWithValue("@u", strUserID);
            da.SelectCommand.Parameters.AddWithValue("@tp", (byte)type);
            ShareDataSet.T_UserViewDataTable dt = new ShareDataSet.T_UserViewDataTable();
            da.Fill(dt);
            return dt;
        }


        public static ShareDataSet.T_UserViewRow
            getT_UserViewRow(int nListID, string strUserID, EnumType type, string strTypeMei, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "select * from T_UserView where ListID=@id and UserID=@u and Type=@tp and TypeMei=@tm";
            da.SelectCommand.Parameters.AddWithValue("@id", nListID);
            da.SelectCommand.Parameters.AddWithValue("@u", strUserID);
            da.SelectCommand.Parameters.AddWithValue("@tp", (byte)type);
            da.SelectCommand.Parameters.AddWithValue("@tm", strTypeMei);

            ShareDataSet.T_UserViewDataTable dt = new ShareDataSet.T_UserViewDataTable();
            da.Fill(dt);
            if (1 == dt.Count)
                return dt[0];
            else
                return null;
        }

        public static Core.Error SaveSort(int nListID, string strUserID, EnumType type, string strTypeMei, string strSort, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "select * from T_UserView where ListID=@id and UserID=@u and Type=@tp and TypeMei=@tm";
            da.SelectCommand.Parameters.AddWithValue("@id", nListID);
            da.SelectCommand.Parameters.AddWithValue("@u", strUserID);
            da.SelectCommand.Parameters.AddWithValue("@tp", (byte)type);
            da.SelectCommand.Parameters.AddWithValue("@tm", strTypeMei);

            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();


            ShareDataSet.T_UserViewDataTable dt = new ShareDataSet.T_UserViewDataTable();

            try
            {
                sqlConn.Open();

                da.Fill(dt);

                ShareDataSet.T_UserViewRow dr = null;

                if (0 == dt.Count)
                {
                    dr = dt.NewT_UserViewRow();
                    dr.ListID = nListID;
                    dr.UserID = strUserID;
                    dr.Columns = "";
                    dr.Sort = strSort;
                    dr.Type = (byte)type;
                    dr.TypeMei = strTypeMei;
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt[0];
                    dr.Sort = strSort;
                }

                da.Update(dt);

                return null;
            }
            catch (Exception e)
            {
                return new Core.Error(e);
            }
            finally
            {
                sqlConn.Close();
            }
        }

        private enum EnumMode
        {
            Insert, Update, Upsert
        }


        public static Core.Error Save(
            int nListID, string strUserID, EnumType type, string strTypeMei,
            string strColumns, bool bSaveSort, string strSort, SqlConnection sqlConn)
        {
            return Save(EnumMode.Upsert, nListID, strUserID, type, strTypeMei, "", strColumns, bSaveSort, strSort, sqlConn);
        }

        public static Core.Error Insert(
            int nListID, string strUserID, EnumType type, string strTypeMei,
            string strColumns, string strSort, SqlConnection sqlConn)
        {
            return Save(EnumMode.Insert, nListID, strUserID, type, strTypeMei, "", strColumns, true, strSort, sqlConn);
        }

        public static Core.Error Update(
            int nListID, string strUserID, EnumType type, string strTypeMei, string strNewTypeMei,
            string strColumns, string strSort, SqlConnection sqlConn)
        {
            return Save(EnumMode.Update, nListID, strUserID, type, strTypeMei, strNewTypeMei, strColumns, true, strSort, sqlConn);
        }


        private static Core.Error Save(EnumMode mode,
            int nListID, string strUserID, EnumType type, string strTypeMei, string strNewTypeMei,
            string strColumns, bool bSaveSort, string strSort, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "select * from T_UserView where ListID=@id and UserID=@u and Type=@tp and TypeMei=@tm";

            da.SelectCommand.Parameters.AddWithValue("@id", nListID);
            da.SelectCommand.Parameters.AddWithValue("@u", strUserID);
            da.SelectCommand.Parameters.AddWithValue("@tp", (byte)type);
            da.SelectCommand.Parameters.AddWithValue("@tm", strTypeMei);
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            ShareDataSet.T_UserViewDataTable dt = new ShareDataSet.T_UserViewDataTable();

            try
            {
                sqlConn.Open();

                da.Fill(dt);

                ShareDataSet.T_UserViewRow dr = null;

                if (0 == dt.Count)
                {
                    if (mode == EnumMode.Update)
                    {
                        return new Error("該当のデータはありません。");

                    }
                    dr = dt.NewT_UserViewRow();
                    dr.ListID = nListID;
                    dr.UserID = strUserID;
                    dr.Type = (byte)type;
                    dr.TypeMei = strTypeMei;
                    dr.Columns = strColumns;
                    dr.Sort = strSort;
                    dt.Rows.Add(dr);
                }
                else
                {
                    if (mode == EnumMode.Insert)
                        return new Error("この名称は既に登録されています。");

                    dr = dt[0];

                    if (mode == EnumMode.Update)
                    {
                        if (!string.IsNullOrEmpty(strNewTypeMei))
                        {
                            if (!dr.TypeMei.Equals(strNewTypeMei))
                            {
                                da.SelectCommand.Parameters["@tm"].Value = strNewTypeMei;
                                DataTable dtSame = new DataTable();
                                da.Fill(dtSame);
                                if (0 < dtSame.Rows.Count)
                                    return new Error(string.Format("{0}は既に登録されれています。", strNewTypeMei));
                                dr.TypeMei = strNewTypeMei;
                            }
                        }
                    }

                    dr.Columns = strColumns;
                    if (bSaveSort)
                        dr.Sort = strSort;
                }

                da.Update(dt);

                return null;
            }
            catch (Exception e)
            {
                return new Core.Error(e);
            }
            finally
            {
                sqlConn.Close();
            }
        }


        public static Core.Error Delete(int nListID, string strUserID, EnumType type, string strTypeMei, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "select * from T_UserView where ListID=@id and UserID=@u and Type=@tp and TypeMei=@tm";

            da.SelectCommand.Parameters.AddWithValue("@id", nListID);
            da.SelectCommand.Parameters.AddWithValue("@u", strUserID);
            da.SelectCommand.Parameters.AddWithValue("@tp", (byte)type);
            da.SelectCommand.Parameters.AddWithValue("@tm", strTypeMei);


            da.DeleteCommand = (new SqlCommandBuilder(da)).GetDeleteCommand();

            ShareDataSet.T_UserViewDataTable dt = new ShareDataSet.T_UserViewDataTable();

            try
            {
                sqlConn.Open();

                da.Fill(dt);

                if (0 < dt.Count)
                {
                    dt[0].Delete();
                    da.Update(dt);
                }

                return null;
            }
            catch (Exception e)
            {
                return new Core.Error(e);
            }
            finally
            {
                sqlConn.Close();
            }
        }
    }
}
