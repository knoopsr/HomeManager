using HomeManager.Model.Security;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;


namespace HomeManager.DAL.Security
{
    /// <summary>
    /// Repository voor beheer van credentials (wachtwoorden) gekoppeld aan gebruikersaccounts.
    /// </summary>
    public class clsCredentialManagementRepository : ICredentialManagementRepository
    {
        #region Velden

        private ObservableCollection<clsCredentialManagementModel> _mijnCollectie;

        #endregion

        #region Constructor

        public clsCredentialManagementRepository() { }

        #endregion

        #region Private Methods

        /// <summary>
        /// Laadt alle wachtwoorden van de ingelogde gebruiker in de collectie.
        /// </summary>
        private void GenerateCollection()
        {
            SqlDataReader reader = clsDAL.GetData(Properties.Resources.S_Wachtwoord,
                clsDAL.Parameter("@AccountID", clsLoginModel.Instance.AccountID));

            _mijnCollectie = new ObservableCollection<clsCredentialManagementModel>();

            while (reader.Read())
            {
                clsCredentialManagementModel m = new clsCredentialManagementModel()
                {
                    WachtwoordID = (int)reader["WachtwoordID"],
                    WachtwoordGroepID = (int)reader["WachtwoordGroepID"],
                    WachtwoordGroepNaam = (string)reader["WachtwoordGroepNaam"],
                    WachtwoordNaam = (string)reader["WachtwoordNaam"],
                    WachtwoordOmschrijving = (string)reader["WachtwoordOmschrijving"],
                    Login = (string)reader["Login"],
                    Wachtwoord = (string)reader["Wachtwoord"],
                    ControlField = reader["ControlField"]
                };

                _mijnCollectie.Add(m);
            }

            reader.Close();
        }

        #endregion

        #region CRUD-Implementatie

        /// <inheritdoc/>
        public bool Insert(clsCredentialManagementModel entity)
        {
            (DataTable _, bool Ok, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_Wachtwoord,
                    clsDAL.Parameter("AccountID", clsLoginModel.Instance.AccountID),
                    clsDAL.Parameter("WachtwoordGroepID", entity.WachtwoordGroepID),
                    clsDAL.Parameter("WachtwoordNaam", entity.WachtwoordNaam),
                    clsDAL.Parameter("WachtwoordOmschrijving", entity.WachtwoordOmschrijving),
                    clsDAL.Parameter("Login", entity.Login),
                    clsDAL.Parameter("Wachtwoord", entity.Wachtwoord),
                    clsDAL.Parameter("@Returnvalue", 0));

            if (!Ok)
                entity.ErrorBoodschap = Boodschap;

            return Ok;
        }

        /// <inheritdoc/>
        public bool Update(clsCredentialManagementModel entity)
        {
            (DataTable _, bool Ok, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_Wachtwoord,
                    clsDAL.Parameter("WachtwoordID", entity.WachtwoordID),
                    clsDAL.Parameter("WachtwoordGroepID", entity.WachtwoordGroepID),
                    clsDAL.Parameter("WachtwoordNaam", entity.WachtwoordNaam),
                    clsDAL.Parameter("WachtwoordOmschrijving", entity.WachtwoordOmschrijving),
                    clsDAL.Parameter("Login", entity.Login),
                    clsDAL.Parameter("Wachtwoord", entity.Wachtwoord),
                    clsDAL.Parameter("ControlField", entity.ControlField),
                    clsDAL.Parameter("@Returnvalue", 0));

            if (!Ok)
                entity.ErrorBoodschap = Boodschap;

            return Ok;
        }

        /// <inheritdoc/>
        public bool Delete(clsCredentialManagementModel entity)
        {
            (DataTable _, bool Ok, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.D_Wachtwoord,
                    clsDAL.Parameter("WachtwoordID", entity.WachtwoordID),
                    clsDAL.Parameter("ControlField", entity.ControlField),
                    clsDAL.Parameter("@Returnvalue", 0));

            if (!Ok)
                entity.ErrorBoodschap = Boodschap;

            return Ok;
        }

        /// <inheritdoc/>
        public ObservableCollection<clsCredentialManagementModel> GetAll()
        {
            GenerateCollection();
            return _mijnCollectie;
        }

        /// <inheritdoc/>
        public clsCredentialManagementModel GetById(int id)
        {
            if (_mijnCollectie == null)
                GenerateCollection();

            return _mijnCollectie.FirstOrDefault(x => x.WachtwoordID == id);
        }

        /// <inheritdoc/>
        public clsCredentialManagementModel GetFirst()
        {
            if (_mijnCollectie == null)
                GenerateCollection();

            return _mijnCollectie.FirstOrDefault();
        }

        /// <inheritdoc/>
        public clsCredentialManagementModel Find()
        {
            throw new NotImplementedException("Find-methode is niet geïmplementeerd.");
        }

        #endregion
    }
}
