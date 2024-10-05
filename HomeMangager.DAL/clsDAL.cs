using Microsoft.Data.SqlClient;
using System.Data;

namespace HomeManager.DAL
{
    public class clsDAL
    {
        /// <summary>
        /// DAL is als in de app COUNTRY pagina 346 van de cursus C#-MVVM .NET 6.0
        /// </summary>



        private static string _myConnenctionString = Properties.Connection.Default.ConnectionDB;

        public static string MyConnenctionString
        {
            get { return _myConnenctionString; }

        }

        public static DataTable ExecuteDataTable(string storedProcedureName)
        {
            DataTable dt = new DataTable();
            string ControlValue = string.Empty;



            using (SqlConnection cnn = new SqlConnection(MyConnenctionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedureName;

                SqlDataAdapter da = new SqlDataAdapter(cmd);

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
            DataTable dt = new DataTable();
            string ControlValue = string.Empty;

            using (SqlConnection cnn = new SqlConnection(MyConnenctionString))
            {
                SqlCommand cmd = new SqlCommand();
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
                finally
                {
                    cnn.Close();
                }
            }
            return dt;

        }

        public static SqlDataReader GetData(string storedProcedureName, params SqlParameter[] arrParam)
        {
            SqlConnection cnn = new SqlConnection(MyConnenctionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storedProcedureName;
            cmd.Connection = cnn;
            if (arrParam != null)
            {
                foreach (SqlParameter param in arrParam)
                {
                    cmd.Parameters.Add(param);
                }
            }
            cnn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static (DataTable? dt, bool ok, string boodschap) ExecuteDataTable(string storedProcedureName, params SqlParameter[] arrParam)
        {
            DataTable dt;
            bool IHaveAReturnValue = false;
            int nr = -1;


            using (SqlConnection cnn = new SqlConnection(MyConnenctionString))
            {
                using (SqlCommand cmd = new SqlCommand(storedProcedureName, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        if (arrParam != null)
                        {
                            foreach (SqlParameter param in arrParam)
                            {
                                cmd.Parameters.Add(param);
                                if (param.ParameterName == "@ReturnValue")
                                {
                                    IHaveAReturnValue = true;
                                    cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
                                }
                            }
                        }
                        try
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                            if (IHaveAReturnValue)
                            {
                                nr = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                                switch (nr)
                                {
                                    case 1:
                                        return (dt, true, "OK");
                                    case 2:
                                        return (null, false, "Concurrency Probleem");
                                    case 3:
                                        return (null, false, "Constraint Probleem");
                                    default:
                                        return (null, false, "Onbekende fout");
                                }
                            }
                            return (dt, true, "De bewerking is gelukt.");
                        }
                        catch (SqlException ex)
                        {
                            return (null, false, "De bewerking is NIET gelukt, gelieve jouw systeembeheerder te contacteren.");
                            throw new Exception(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            return (null, false, "De bewerking is NIET gelukt, gelieve jouw systeembeheerder te contacteren.");
                            throw new Exception(ex.Message);
                        }
                        finally
                        {
                            cnn.Close();
                        }

                    }
                }

            }
        }

        public static SqlDataReader GetData(string storedProcedureName)
        {
            SqlConnection cnn = new SqlConnection(MyConnenctionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storedProcedureName;
            cmd.Connection = cnn;
            cnn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static SqlParameter Parameter(string myParameterName, object myValue)
        {
            SqlParameter myParameter = new SqlParameter();
            myParameter.ParameterName = myParameterName;
            myParameter.Value = myValue;
            myParameter.IsNullable = true;
            return myParameter;
        }
    }
}

