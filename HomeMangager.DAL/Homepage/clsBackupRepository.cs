using HomeManager.Model.Homepage;
using HomeManager.Model.Security;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace HomeManager.DAL.Homepage
{
    public class clsBackupRepository : IBackupRepository
    {
        public clsBackupRepository() { }
        private ObservableCollection<clsBackupModel> _mijnCollectie;
        int nr = 0;

        private async Task GenerateCollection()
        {
            _mijnCollectie = new ObservableCollection<clsBackupModel>();

            // Maak de verbinding
            using (SqlConnection connection = new SqlConnection(Connection.Default.ConnectionDB))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(Properties.Resources.BackupHomeManagerDB, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Voeg de @Path parameter toe (optioneel)
                    command.Parameters.AddWithValue("@Path", "/var/opt/mssql/backups/");

                    // Voeg de @FilePathName output parameter toe
                    SqlParameter outputParam = new SqlParameter("@FilePathName", SqlDbType.NVarChar, 400)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    // Voer de stored procedure asynchroon uit
                    await command.ExecuteNonQueryAsync();

                    // Haal de waarde van de output-parameter op
                    string filePathName = outputParam.Value.ToString();

                    // Voeg de filePathName toe aan de collectie
                    clsBackupModel backupModel = new clsBackupModel()
                    {
                        Path = filePathName
                    };

                    _mijnCollectie.Add(backupModel);
                }
            }
        }





        public bool Delete(clsBackupModel entity)
        {
            throw new NotImplementedException();
        }

        public clsBackupModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsBackupModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsBackupModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public clsBackupModel GetFirst()
        {
            throw new NotImplementedException();
        }

        public bool Insert(clsBackupModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(clsBackupModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ObservableCollection<clsBackupModel>> CreateBackup()
        {
            await GenerateCollection();
            return _mijnCollectie;
        }
    }
}
