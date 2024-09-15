using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMangager.DAL
{
    public class clsDAL
    {


        private static bool _OKNOK;
        public static bool OKNOK
        {
            get { return _OKNOK; }
            set { _OKNOK = value; }
        }

        private static string _MyConnenctionString = Properties.Connection.Default.ConnectionDB.ToString();

        public static string MyConnenctionString
        {
            get { return _MyConnenctionString; }

        }

        public static SqlParameter Parameter(string myParameterName, object myValue)
        {
            SqlParameter myParameter = new SqlParameter();
            myParameter.ParameterName = myParameterName;
            myParameter.Value = myValue;
            myParameter.IsNullable = true;
            return myParameter;
        }




        public static DataTable ExecuteDataTable(string storedProcedureName)
        {
            DataTable dt = new DataTable();
            string ControlValue = string.Empty;
            string MyConnection = string.Empty;


            using (SqlConnection cnn = new SqlConnection(MyConnenctionString))
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = 0;
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedureName;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                int ReturnvValue = 0;

                try
                {
                    dt = new DataTable();
                    da.Fill(dt);
                }
                catch (SqlException ex)
                {

                    throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cnn.Close();
                }

            }

            return dt;
        }


        public static DataTable ExecuteDataTable(string storedProcedureName, ref int nr, params SqlParameter[] arrParam)
        {
            try
            {
                DataTable dt = new DataTable();
                string ControlValue = string.Empty;
                string MyConnection = string.Empty;
                using (SqlConnection cnn = new SqlConnection(MyConnenctionString))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandTimeout = 0;
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedProcedureName;

                    if (arrParam != null)
                    {
                        foreach (SqlParameter param in arrParam)
                        {
                            cmd.Parameters.Add(param);
                            if (param.ParameterName == "@ReturnValue")
                            {
                                ControlValue = "ok";
                                cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
                            }
                        }
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    int ReturnValue = 0;
                    try
                    {
                        dt = new DataTable();
                        da.Fill(dt);

                        if (ControlValue == "ok")
                        {
                            ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                            nr = ReturnValue;
                        }
                    }
                    catch (SqlException ex)
                    {
                        ReturnValue = 9999;
                        nr = ReturnValue;
                        throw new Exception(ex.Message);
                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message);
                    }
                }
                return dt;


            }
            catch (Exception ex2)
            {
                throw new Exception(ex2.ToString());
            }
        }

    }
}

