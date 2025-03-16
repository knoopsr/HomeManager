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
    public class clsCredentialManagementRepository : ICredentialManagementRepository
    {
        private ObservableCollection<clsCredentialManagementModel> _mijnCollectie;
        int nr = 0;
       

        public clsCredentialManagementRepository()
        {
        }

        private void GenerateCollection()
        {
            // Roep de GetData-methode aan met de stored procedure en de parameter
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Wachtwoord,
               clsDAL.Parameter("@AccountID", clsLoginModel.Instance.AccountID));
            _mijnCollectie = new ObservableCollection<clsCredentialManagementModel>();

            while (MijnDataReader.Read())
            {
                clsCredentialManagementModel m = new clsCredentialManagementModel()
                {
                    WachtwoordID = (int)MijnDataReader["WachtwoordID"],
                    WachtwoordGroepID = (int)MijnDataReader["WachtwoordGroepID"],
                    WachtwoordGroepNaam = (string)MijnDataReader["WachtwoordGroepNaam"],
                    WachtwoordNaam = (string)MijnDataReader["WachtwoordNaam"],
                    WachtwoordOmschrijving = (string)MijnDataReader["WachtwoordOmschrijving"],
                    Login = (string)MijnDataReader["Login"],
                    Wachtwoord = (string)MijnDataReader["Wachtwoord"],
                    ControlField = MijnDataReader["ControlField"]
                };

                _mijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }

        public bool Delete(clsCredentialManagementModel entity)
        {
            (DataTable DT, bool Ok, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.D_Wachtwoord,
                clsDAL.Parameter("WachtwoordID", entity.WachtwoordID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@Returnvalue", 0)
                );
            if (!Ok)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return Ok;
        }

        public clsCredentialManagementModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsCredentialManagementModel> GetAll()
        {
            GenerateCollection();
            return _mijnCollectie;
        }

        public clsCredentialManagementModel GetById(int id)
        {
            if (_mijnCollectie == null)
            {
                GenerateCollection();
            }
            return _mijnCollectie.Where(x => x.WachtwoordID == id).FirstOrDefault();
        }

        public clsCredentialManagementModel GetFirst()
        {
            if (_mijnCollectie == null)
            {
                GenerateCollection();
            }
            return _mijnCollectie.FirstOrDefault();
        }

        public bool Insert(clsCredentialManagementModel entity)
        {
            (DataTable DT, bool Ok, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_Wachtwoord,
                  clsDAL.Parameter("AccountID", clsLoginModel.Instance.AccountID),
                clsDAL.Parameter("WachtwoordGroepID", entity.WachtwoordGroepID),
                clsDAL.Parameter("WachtwoordNaam", entity.WachtwoordNaam),
                clsDAL.Parameter("WachtwoordOmschrijving", entity.WachtwoordOmschrijving),
                clsDAL.Parameter("Login", entity.Login),
                clsDAL.Parameter("Wachtwoord", entity.Wachtwoord),
                clsDAL.Parameter("@Returnvalue", 0)
                );
            if (!Ok)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return Ok;
        }

        public bool Update(clsCredentialManagementModel entity)
        {
            (DataTable DT, bool Ok, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_Wachtwoord,
                clsDAL.Parameter("WachtwoordID", entity.WachtwoordID),
                clsDAL.Parameter("WachtwoordGroepID", entity.WachtwoordGroepID),
                clsDAL.Parameter("WachtwoordNaam", entity.WachtwoordNaam),
                clsDAL.Parameter("WachtwoordOmschrijving", entity.WachtwoordOmschrijving),
                clsDAL.Parameter("Login", entity.Login),
                clsDAL.Parameter("Wachtwoord", entity.Wachtwoord),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@Returnvalue", 0)
                );
            if (!Ok)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return Ok;
        }
    }
}
