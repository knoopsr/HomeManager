using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Model.Personen;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace HomeManager.DAL.Personen
{
    public class clsLandRepository : ILandRepository
    {
        private ObservableCollection<clsLandModel> MijnCollectie;
        int nr = 0;
        public clsLandRepository()
        {
        }
        public bool Delete(clsLandModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.D_Land,
                clsDAL.Parameter("LandID", entity.LandID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public clsLandModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsLandModel> GetAll()
        {
            GenerateCollection();
            return MijnCollectie;
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Land);
            MijnCollectie = new ObservableCollection<clsLandModel>();

            while (MijnDataReader.Read())
            {
                clsLandModel e = new clsLandModel()
                {
                    LandID = (int)MijnDataReader[0],
                    Land = MijnDataReader[1].ToString(),
                    LandCode = MijnDataReader[2].ToString(),
                    Vlag = MijnDataReader[3] != DBNull.Value ? (byte[])MijnDataReader[3] : null, // Controle op DBNull voordat je het cast
                    ControlField = MijnDataReader[4]
                };
                MijnCollectie.Add(e);
            }
            MijnDataReader.Close();
        }

        public clsLandModel GetById(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.Where(land => land.LandID == id).FirstOrDefault();
        }

        public clsLandModel GetFirst()
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault();
        }

        public bool Insert(clsLandModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_Land,
                clsDAL.Parameter("Land", entity.Land),
                clsDAL.Parameter("LandCode", entity.LandCode),
                clsDAL.Parameter("Vlag", entity.Vlag != null ? (object)entity.Vlag : DBNull.Value, SqlDbType.VarBinary),
                clsDAL.Parameter("@ReturnValue", 0)
                );
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public bool Update(clsLandModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_Land,
                clsDAL.Parameter("LandID", entity.LandID),
                clsDAL.Parameter("Land", entity.Land),
                clsDAL.Parameter("LandCode", entity.LandCode),
                clsDAL.Parameter("Vlag", entity.Vlag != null ? (object)entity.Vlag : DBNull.Value, SqlDbType.VarBinary),
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
