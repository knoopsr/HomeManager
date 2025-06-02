using HomeManager.Model.Personen;
using HomeManager.Model.Security;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace HomeManager.DAL.Security
{
    /// <summary>
    /// Repository voor het beheren van geblokkeerde accounts in het systeem.
    /// </summary>
    public class clsLockedAccountRepository : ILockedAccountRepository
    {
        #region Velden

        private ObservableCollection<clsLockedAccountModel> _mijnCollectie;

        #endregion

        #region Constructor

        public clsLockedAccountRepository() { }

        #endregion

        #region Private Methods

        /// <summary>
        /// Laadt de lijst van geblokkeerde accounts op uit de database.
        /// </summary>
        private void GenerateCollection()
        {
            SqlDataReader reader = clsDAL.GetData(Properties.Resources.S_LockedUsers);
            _mijnCollectie = new ObservableCollection<clsLockedAccountModel>();

            while (reader.Read())
            {
                clsLockedAccountModel m = new clsLockedAccountModel()
                {
                    Account = new clsAccountModel
                    {
                        AccountID = (int)reader["AccountID"],
                        Login = reader["Login"].ToString()
                    },
                    Persoon = new clsPersoonModel
                    {
                        PersoonID = (int)reader["PersoonID"],
                        Voornaam = reader["Voornaam"].ToString(),
                        Naam = reader["Naam"].ToString(),
                        Foto = reader["Foto"] != DBNull.Value ? (byte[])reader["Foto"] : null
                    }
                };

                _mijnCollectie.Add(m);
            }
            reader.Close();
        }

        #endregion

        #region CRUD-implementaties (niet geïmplementeerd)

        public bool Delete(clsLockedAccountModel entity) => throw new NotImplementedException();
        public clsLockedAccountModel Find() => throw new NotImplementedException();
        public clsLockedAccountModel GetById(int id) => throw new NotImplementedException();
        public clsLockedAccountModel GetFirst() => throw new NotImplementedException();
        public bool Insert(clsLockedAccountModel entity) => throw new NotImplementedException();
        public bool Update(clsLockedAccountModel entity) => throw new NotImplementedException();

        #endregion

        #region Public Methods

        /// <summary>
        /// Geeft alle geblokkeerde accounts terug.
        /// </summary>
        public ObservableCollection<clsLockedAccountModel> GetAll()
        {
            GenerateCollection();
            return _mijnCollectie;
        }

        /// <summary>
        /// Ontgrendelt meerdere accounts op basis van een lijst met AccountID's en wachtwoorden.
        /// </summary>
        /// <param name="model">Het model met de geselecteerde gebruikers om te ontgrendelen.</param>
        /// <returns>True als succesvol, anders false met foutmelding in model.ErrorBoodschap.</returns>
        public bool UnLockUsers(clsLockedAccountModel model)
        {
            DataTable inputTable = new DataTable();
            inputTable.Columns.Add("AccountID", typeof(int));
            inputTable.Columns.Add("Wachtwoord", typeof(string));

            foreach (var item in model.SelectedItemsList)
            {
                inputTable.Rows.Add(item.AccountID, item.Wachtwoord);
            }

            SqlParameter tvpParam = new SqlParameter("@Accounts", SqlDbType.Structured)
            {
                TypeName = "AccountIDTable",
                Value = inputTable
            };

            (DataTable? _, bool Ok, string Boodschap) = clsDAL.ExecuteDataTable(
                Properties.Resources.U_LockedUsers,
                tvpParam,
                clsDAL.Parameter("@ReturnValue", 0)
            );

            if (!Ok)
                model.ErrorBoodschap = Boodschap;

            return Ok;
        }

        #endregion
    }
}
