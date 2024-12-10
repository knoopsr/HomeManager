using HomeManager.Model.Personen;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Personen
{
    public class clsTelefoonNummersRepository : ITelefoonNummersRepository
    {
        private ObservableCollection<clsTelefoonNummersModel> MijnCollectie;
        int nr = 0;
        public clsTelefoonNummersRepository()
        { }
        public bool Delete(clsTelefoonNummersModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.D_TelefoonNummers,
                    clsDAL.Parameter("TelefoonNummerID", entity.TelefoonNummerID),
                    clsDAL.Parameter("ControlField", entity.ControlField),
                    clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public clsTelefoonNummersModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsTelefoonNummersModel> GetAll()
        {
            GenerateCollection();
            return MijnCollectie;
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_TelefoonNummers);
            MijnCollectie = new ObservableCollection<clsTelefoonNummersModel>();

            while (MijnDataReader.Read())
            {
                clsTelefoonNummersModel m = new clsTelefoonNummersModel()
                {
                    TelefoonNummerID = (int)MijnDataReader["TelefoonNummerID"],
                    PersoonID = (int)MijnDataReader["PersoonID"],
                    TelefoonTypeID = (int)MijnDataReader["TelefoonTypeID"],
                    TelefoonNummer = MijnDataReader["TelefoonNummer"].ToString(),
                    ControlField = MijnDataReader["ControlField"]
                };
                MijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }

        public clsTelefoonNummersModel GetById(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.Where(telefoonnummers => telefoonnummers.TelefoonNummerID == id).FirstOrDefault();
        }

        public clsTelefoonNummersModel GetFirst()
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault();
        }

        public bool Insert(clsTelefoonNummersModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_TelefoonNummers,
                    clsDAL.Parameter("PersoonID", entity.PersoonID),
                    clsDAL.Parameter("TelefoonTypeID", entity.TelefoonTypeID),
                    clsDAL.Parameter("TelefoonNummer", entity.TelefoonNummer),
                    clsDAL.Parameter("@ReturnValue", 0)
                );
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public bool Update(clsTelefoonNummersModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_TelefoonNummers,
                    clsDAL.Parameter("TelefoonNummerID", entity.TelefoonNummerID),
                    clsDAL.Parameter("PersoonID", entity.PersoonID),
                    clsDAL.Parameter("TelefoonTypeID", entity.TelefoonTypeID),
                    clsDAL.Parameter("TelefoonNummer", entity.TelefoonNummer),
                    clsDAL.Parameter("@ReturnValue", 0)
                );
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }
    }
}
