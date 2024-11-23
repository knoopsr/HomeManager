using HomeManager.Model.Security;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace HomeManager.DAL.Security
{
    public class clsLoginRepository : ILoginRepository
    {

        private ObservableCollection<clsLoginModel> _mijnCollectie;
        int nr = 0;

        public bool Delete(clsLoginModel entity)
        {
            throw new NotImplementedException();
        }

        public clsLoginModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsLoginModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsLoginModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public clsLoginModel GetByLogin(string login, string wachtwoord)
        {
            var _login = clsLoginModel.Instance;


            using (SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Login,
                clsDAL.Parameter("@Login", login),
                clsDAL.Parameter("@Wachtwoord", wachtwoord)))
            {
                // Controleer of er resultaten zijn en maak een instantie van clsLoginModel
                if (MijnDataReader.Read())
                {
                    // Instantieer een nieuw object van clsLoginModel

                    _login.AccountID = (int)MijnDataReader["AccountID"];
                    _login.PersoonID = (int)MijnDataReader["PersoonID"];
                    _login.Naam = MijnDataReader["Naam"].ToString();
                    _login.Foto = MijnDataReader["Foto"] != DBNull.Value ? (byte[])MijnDataReader["Foto"] : null;
                    _login.VoorNaam = MijnDataReader["VoorNaam"].ToString();
                    _login.RolID = (int)MijnDataReader["RolID"];
                    _login.RolName = MijnDataReader["RolName"].ToString();
                    _login.CountFailLogins = (int)MijnDataReader["CountFailLogins"];
                    _login.IsNew = (bool)MijnDataReader["IsNew"];
                    _login.IsLock = (bool)MijnDataReader["IsLock"];
                    _login.RechtenCodes = MijnDataReader["RechtenCodes"].ToString();
                    _login.ControlField = MijnDataReader["ControlField"];
                }

            }

            return _login;
        }


        public clsLoginModel GetFirst()
        {
            throw new NotImplementedException();
        }

        public bool Insert(clsLoginModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(clsLoginModel entity)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePassWord(clsLoginModel entity, string Pass1)
        {
            (DataTable DT, bool Ok, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_Login,
                clsDAL.Parameter("AccountID", entity.AccountID),
                clsDAL.Parameter("PersoonID", entity.PersoonID),
                clsDAL.Parameter("Wachtwoord", Pass1),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@Returnvalue", 0));
            if (!Ok)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return true;
        }
    }
}
