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
    public class clsTelefoonTypeRepository : ITelefoonTypeRepository
    {
        private ObservableCollection<clsTelefoonTypeModel> MijnCollectie;
        int nr = 0;
        public clsTelefoonTypeRepository()
        { }
        public bool Delete(clsTelefoonTypeModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.D_TelefoonType,
                clsDAL.Parameter("TelefoonTypeID", entity.TelefoonTypeID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public clsTelefoonTypeModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsTelefoonTypeModel> GetAll()
        {
            GenerateCollection();
            return MijnCollectie;
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_TelefoonType);
            MijnCollectie = new ObservableCollection<clsTelefoonTypeModel>();

            while (MijnDataReader.Read())
            {
                clsTelefoonTypeModel m = new clsTelefoonTypeModel()
                {
                    TelefoonTypeID = (int)MijnDataReader["TelefoonTypeID"],
                    TelefoonType = MijnDataReader["TelefoonType"].ToString(),
                    ControlField = MijnDataReader["ControlField"]
                };
                MijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }

        public clsTelefoonTypeModel GetById(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.Where(telefoon => telefoon.TelefoonTypeID == id).FirstOrDefault();
        }

        public clsTelefoonTypeModel GetFirst()
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault();
        }

        public bool Insert(clsTelefoonTypeModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_TelefoonType,
                clsDAL.Parameter("TelefoonType", entity.TelefoonType),
                clsDAL.Parameter("@ReturnValue", 0)
                );
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public bool Update(clsTelefoonTypeModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_TelefoonType,
                clsDAL.Parameter("TelefoonTypeID", entity.TelefoonTypeID),
                clsDAL.Parameter("TelefoonType", entity.TelefoonType),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }
    }
}
