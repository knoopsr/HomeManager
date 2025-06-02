using HomeManager.Model.Security;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;


namespace HomeManager.DAL.Security
{
    /// <summary>
    /// Repository voor CRUD-operaties op accountgegevens.
    /// </summary>
    public class clsAccountRepository : IAccountRepository
    {
        #region Velden

        private ObservableCollection<clsAccountModel> _mijnCollectie;

        #endregion

        #region Constructor

        public clsAccountRepository() { }

        #endregion

        #region Private Methods

        /// <summary>
        /// Genereert een collectie van accounts op basis van de databasequery.
        /// </summary>
        private void GenerateCollection()
        {
            SqlDataReader reader = clsDAL.GetData(Properties.Resources.S_Account);
            _mijnCollectie = new ObservableCollection<clsAccountModel>();

            while (reader.Read())
            {
                clsAccountModel model = new clsAccountModel()
                {
                    AccountID = (int)reader["AccountID"],
                    PersoonID = (int)reader["PersoonID"],
                    RolID = (int)reader["RolID"],
                    Login = (string)reader["Login"],
                    Wachtwoord = (string)reader["Wachtwoord"],
                    IsNew = (bool)reader["IsNew"],
                    IsLock = (bool)reader["IsLock"],
                    CountFailLogins = (int)reader["CountFailLogins"],
                    ControlField = reader["ControlField"],
                    Foto = reader["Foto"] != DBNull.Value ? (byte[])reader["Foto"] : null,
                    RolNaam = (string)reader["RolName"]
                };

                _mijnCollectie.Add(model);
            }

            reader.Close();
        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public bool Insert(clsAccountModel entity)
        {
            (DataTable _, bool Ok, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_Account,
                    clsDAL.Parameter("PersoonID", entity.PersoonID),
                    clsDAL.Parameter("RolID", entity.RolID),
                    clsDAL.Parameter("Login", entity.Login),
                    clsDAL.Parameter("Wachtwoord", entity.Wachtwoord),
                    clsDAL.Parameter("IsNew", entity.IsNew),
                    clsDAL.Parameter("IsLock", entity.IsLock),
                    clsDAL.Parameter("CountFailLogins", entity.CountFailLogins),
                    clsDAL.Parameter("@Returnvalue", 0));

            if (!Ok)
                entity.ErrorBoodschap = Boodschap;

            return Ok;
        }

        /// <inheritdoc/>
        public bool Update(clsAccountModel entity)
        {
            (DataTable _, bool Ok, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_Account,
                    clsDAL.Parameter("AccountID", entity.AccountID),
                    clsDAL.Parameter("PersoonID", entity.PersoonID),
                    clsDAL.Parameter("RolID", entity.RolID),
                    clsDAL.Parameter("Login", entity.Login),
                    clsDAL.Parameter("Wachtwoord", entity.Wachtwoord),
                    clsDAL.Parameter("IsNew", entity.IsNew),
                    clsDAL.Parameter("IsLock", entity.IsLock),
                    clsDAL.Parameter("CountFailLogins", entity.CountFailLogins),
                    clsDAL.Parameter("ControlField", entity.ControlField),
                    clsDAL.Parameter("@Returnvalue", 0));

            if (!Ok)
                entity.ErrorBoodschap = Boodschap;

            return Ok;
        }

        /// <inheritdoc/>
        public bool Delete(clsAccountModel entity)
        {
            (DataTable _, bool Ok, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.D_Account,
                    clsDAL.Parameter("AccountID", entity.AccountID),
                    clsDAL.Parameter("ControlField", entity.ControlField),
                    clsDAL.Parameter("@Returnvalue", 0));

            if (!Ok)
                entity.ErrorBoodschap = Boodschap;

            return Ok;
        }

        /// <inheritdoc/>
        public ObservableCollection<clsAccountModel> GetAll()
        {
            GenerateCollection();
            return _mijnCollectie;
        }

        /// <inheritdoc/>
        public clsAccountModel GetById(int id)
        {
            if (_mijnCollectie == null)
                GenerateCollection();

            return _mijnCollectie.FirstOrDefault(x => x.AccountID == id);
        }

        /// <inheritdoc/>
        public clsAccountModel GetFirst()
        {
            if (_mijnCollectie == null)
                GenerateCollection();

            return _mijnCollectie.FirstOrDefault();
        }

        /// <inheritdoc/>
        public clsAccountModel Find()
        {
            throw new NotImplementedException("Find-methode is niet geïmplementeerd.");
        }

        #endregion
    }
}
