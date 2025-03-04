using HomeManager.Model.Security;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Security
{
    public class clsAccountRepository : IAccountRepository
    {
        private ObservableCollection<clsAccountModel> _mijnCollectie;
        int nr = 0;

        public clsAccountRepository()
        {
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Account);
            _mijnCollectie = new ObservableCollection<clsAccountModel>();

            while (MijnDataReader.Read())
            {
                clsAccountModel m = new clsAccountModel()
                {
                    AccountID = (int)MijnDataReader["AccountID"],
                    PersoonID = (int)MijnDataReader["PersoonID"],
                    RolID = (int)MijnDataReader["RolID"],             
                    Login = (string)MijnDataReader["Login"],
                    Wachtwoord = (string)MijnDataReader["Wachtwoord"],
                    IsNew = (bool)MijnDataReader["IsNew"],
                    IsLock = (bool)MijnDataReader["IsLock"],
                    CountFailLogins = (int)MijnDataReader["CountFailLogins"],
                    ControlField = MijnDataReader["ControlField"]
                };

                _mijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }


        public bool Delete(clsAccountModel entity)
        {
            (DataTable DT, bool Ok, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.D_Account,
                clsDAL.Parameter("AccountID", entity.AccountID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@Returnvalue", 0));
            if (!Ok)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return Ok;
        }

        public clsAccountModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsAccountModel> GetAll()
        {
            GenerateCollection();
            return _mijnCollectie;
        }

        public clsAccountModel GetById(int id)
        {
            if (_mijnCollectie == null)
            {
                GenerateCollection();
            }
            return _mijnCollectie.Where(x => x.AccountID == id).FirstOrDefault();
        }

        public clsAccountModel GetFirst()
        {
            if (_mijnCollectie == null)
            {
                GenerateCollection();
            }
            return _mijnCollectie.FirstOrDefault();

        }

        public bool Insert(clsAccountModel entity)
        {
            (DataTable DT, bool Ok, string Boodschap) =
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
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return Ok;
        }

        public bool Update(clsAccountModel entity)
        {
            (DataTable DT, bool Ok, string Boodschap) =
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
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return Ok;
        }
    }
}
