using HomeManager.Model.Personen;
using HomeManager.Model.Security;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace HomeManager.DAL.Security
{
    public class clsLockedAccountRepository : ILockedAccountRepository
    {
        public clsLockedAccountRepository() { }
        private ObservableCollection<clsLockedAccountModel> _mijnCollectie;
        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_LockedUsers);
            _mijnCollectie = new ObservableCollection<clsLockedAccountModel>();

            while (MijnDataReader.Read())
            {
                clsLockedAccountModel m = new clsLockedAccountModel()
                {
                    Account = new clsAccountModel
                    {
                        AccountID = (int)MijnDataReader["AccountID"],
                        Login = MijnDataReader["Login"].ToString()
                    },
                    Persoon = new clsPersoonModel
                    {
                        PersoonID = (int)MijnDataReader["PersoonID"],
                        Voornaam = MijnDataReader["Voornaam"].ToString(),
                        Naam = MijnDataReader["Naam"].ToString(),
                        Foto = MijnDataReader["Foto"] != DBNull.Value ? (byte[])MijnDataReader["Foto"] : null
                    }
                };

                _mijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }
        public bool Delete(clsLockedAccountModel entity)
        {
            throw new NotImplementedException();
        }

        public clsLockedAccountModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsLockedAccountModel> GetAll()
        {
            GenerateCollection();
            return _mijnCollectie;

        }

        public clsLockedAccountModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public clsLockedAccountModel GetFirst()
        {
            throw new NotImplementedException();
        }

        public bool Insert(clsLockedAccountModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(clsLockedAccountModel entity)
        {
            throw new NotImplementedException();
        }

        public bool UnLockUsers(clsLockedAccountModel model)
        {
            //(DataTable DT, bool Ok, string Boodschap) =
            //     clsDAL.ExecuteDataTable(Properties.Resources.U_LockedUsers,
            //     clsDAL.Parameter("AccountsIds", AccountsIds.SelectedItems),
            //     clsDAL.Parameter("@Returnvalue", 0)
            //     );
            //if (!Ok)
            //{
            //    AccountsIds.Account.ErrorBoodschap = Boodschap;
            //}
            //return Ok;


            // Voorbereiden van de DataTable
            DataTable inputTable = new DataTable();
            inputTable.Columns.Add("AccountID", typeof(int));
            inputTable.Columns.Add("Wachtwoord", typeof(string));

            foreach (var item in model.SelectedItemsList)
            {
                inputTable.Rows.Add(item.AccountID, item.Wachtwoord);
            }

            // Maak de TVP-parameter
            SqlParameter tvpParam = new SqlParameter("@Accounts", SqlDbType.Structured)
            {
                TypeName = "AccountIDTable", // Geef het TypeName mee
                Value = inputTable
            };

            // Roep clsDAL.ExecuteDataTable aan met de TVP en andere parameters
            (DataTable? DT, bool Ok, string Boodschap) = clsDAL.ExecuteDataTable(
                Properties.Resources.U_LockedUsers,
                tvpParam,
                clsDAL.Parameter("@ReturnValue", 0) // Andere parameters
            );

            if (!Ok)
            {
                model.ErrorBoodschap = Boodschap;
            }

            return Ok;


        }
    }
}
