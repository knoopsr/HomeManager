using Microsoft.Data.SqlClient;
using System.Data;

namespace HomeManager.DAL
{
    /// <summary>
    /// Algemene Data Access Layer (DAL) voor database-operaties via Stored Procedures.
    /// </summary>
    public class clsDAL
    {
        /// <summary>
        /// Bevat de connection string naar de database.
        /// </summary>
        private static string _myConnenctionString = Properties.Connection.Default.ConnectionDB.ToString();

        /// <summary>
        /// Geeft de connection string naar de database terug.
        /// </summary>
        public static string MyConnenctionString => _myConnenctionString;

        /// <summary>
        /// Voert een stored procedure uit zonder parameters en retourneert het resultaat als DataTable.
        /// </summary>
        /// <param name="storedProcedureName">Naam van de stored procedure.</param>
        /// <returns>DataTable met de resultaten.</returns>
        public static DataTable ExecuteDataTable(string storedProcedureName)
        {
            DataTable dt = new DataTable();

            using SqlConnection cnn = new SqlConnection(MyConnenctionString);
            SqlCommand cmd = new SqlCommand(storedProcedureName, cnn)
            {
                CommandType = CommandType.StoredProcedure
            };
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                da.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cnn.Close();
            }

            return dt;
        }

        /// <summary>
        /// Voert een stored procedure uit met parameters en een ReturnValue.
        /// </summary>
        /// <param name="storedProcedureName">Naam van de stored procedure.</param>
        /// <param name="nr">ReturnValue van de procedure (output).</param>
        /// <param name="arrParam">SQL parameters.</param>
        /// <returns>DataTable met de resultaten.</returns>
        public static DataTable ExecuteDataTable(string storedProcedureName, ref int nr, params SqlParameter[] arrParam)
        {
            DataTable dt = new DataTable();
            string ControlValue = string.Empty;

            using SqlConnection cnn = new SqlConnection(MyConnenctionString);
            SqlCommand cmd = new SqlCommand(storedProcedureName, cnn)
            {
                CommandType = CommandType.StoredProcedure
            };

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
            try
            {
                da.Fill(dt);
                if (ControlValue == "ok")
                {
                    nr = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                }
            }
            catch (SqlException ex)
            {
                nr = 9999;
                throw new Exception(ex.Message);
            }
            finally
            {
                cnn.Close();
            }

            return dt;
        }

        /// <summary>
        /// Haalt gegevens op met behulp van een stored procedure en retourneert een SqlDataReader.
        /// </summary>
        /// <param name="storedProcedureName">Naam van de stored procedure.</param>
        /// <param name="arrParam">Optionele parameters.</param>
        /// <returns>SqlDataReader met resultaten (connectie wordt gesloten bij sluiten van reader).</returns>
        public static SqlDataReader GetData(string storedProcedureName, params SqlParameter[] arrParam)
        {
            SqlConnection cnn = new SqlConnection(MyConnenctionString);
            SqlCommand cmd = new SqlCommand(storedProcedureName, cnn)
            {
                CommandType = CommandType.StoredProcedure
            };

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

        /// <summary>
        /// Voert een stored procedure uit met foutafhandeling en returnboodschap.
        /// </summary>
        /// <param name="storedProcedureName">Naam van de stored procedure.</param>
        /// <param name="arrParam">SQL parameters.</param>
        /// <returns>Tuple met DataTable, status (ok) en boodschap.</returns>
        public static (DataTable? dt, bool ok, string boodschap) ExecuteDataTable(string storedProcedureName, params SqlParameter[] arrParam)
        {
            DataTable dt;
            bool IHaveAReturnValue = false;
            int nr = -1;

            using SqlConnection cnn = new SqlConnection(MyConnenctionString);
            using SqlCommand cmd = new SqlCommand(storedProcedureName, cnn)
            {
                CommandType = CommandType.StoredProcedure
            };
            using SqlDataAdapter da = new SqlDataAdapter(cmd)
            {
                SelectCommand = cmd
            };

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
                    return nr switch
                    {
                        1 => (dt, true, "OK"),
                        2 => (null, false, "Concurrency Probleem"),
                        3 => (null, false, "Constraint Probleem"),
                        _ => (null, false, "Onbekende fout"),
                    };
                }

                return (dt, true, "De bewerking is gelukt.");
            }
            catch (SqlException ex)
            {
                return (null, false, ex.Message);
            }
            catch
            {
                return (null, false, "De bewerking is NIET gelukt, gelieve jouw systeembeheerder te contacteren.");
            }
            finally
            {
                cnn.Close();
            }
        }

        /// <summary>
        /// Haalt gegevens op met behulp van een stored procedure zonder parameters.
        /// </summary>
        /// <param name="storedProcedureName">Naam van de stored procedure.</param>
        /// <returns>SqlDataReader met resultaten.</returns>
        public static SqlDataReader GetData(string storedProcedureName)
        {
            SqlConnection cnn = new SqlConnection(MyConnenctionString);
            SqlCommand cmd = new SqlCommand(storedProcedureName, cnn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cnn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// Maakt een SqlParameter aan met naam en waarde.
        /// </summary>
        /// <param name="myParameterName">Naam van de parameter.</param>
        /// <param name="myValue">Waarde van de parameter.</param>
        /// <returns>SqlParameter instantie.</returns>
        public static SqlParameter Parameter(string myParameterName, object myValue)
        {
            SqlParameter myParameter = new SqlParameter
            {
                ParameterName = myParameterName,
                Value = myValue,
                IsNullable = true
            };
            return myParameter;
        }

        /// <summary>
        /// Maakt een SqlParameter aan met opgegeven datatype.
        /// </summary>
        /// <param name="naam">Naam van de parameter.</param>
        /// <param name="waarde">Waarde van de parameter.</param>
        /// <param name="sqlDbType">SQL datatype van de parameter.</param>
        /// <returns>SqlParameter instantie.</returns>
        public static SqlParameter Parameter(string naam, object waarde, SqlDbType sqlDbType)
        {
            SqlParameter parameter = new SqlParameter(naam, sqlDbType)
            {
                Value = waarde ?? DBNull.Value
            };
            return parameter;
        }
    }
}
