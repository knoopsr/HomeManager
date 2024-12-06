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
    public class clsProvincieRepository : IProvincieRepository
    {

        private ObservableCollection<clsProvincieModel> MijnCollectie;
        int nr = 0;
        public clsProvincieRepository()
        {
        }

        public bool Delete(clsProvincieModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.D_Provincie,
                clsDAL.Parameter("ProvincieID", entity.ProvincieID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public clsProvincieModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsProvincieModel> GetAll()
        {
            GenerateCollection();
            return MijnCollectie;
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Provincie);
            MijnCollectie = new ObservableCollection<clsProvincieModel>();

            while (MijnDataReader.Read())
            {
                clsProvincieModel e = new clsProvincieModel()
                {
                    ProvincieID = (int)MijnDataReader[0],
                    Provincie = MijnDataReader[1].ToString(),
                    LandID = (int)MijnDataReader[2],
                    LandCode = (string)MijnDataReader[3],
                    ControlField = MijnDataReader[4]
                };
                MijnCollectie.Add(e);
            }
            MijnDataReader.Close();
        }

        public clsProvincieModel GetById(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.Where(provincie => provincie.ProvincieID == id).FirstOrDefault();
        }

        public clsProvincieModel GetFirst()
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault();
        }

        public bool Insert(clsProvincieModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_Provincie,
                clsDAL.Parameter("Provincie", entity.Provincie),
                clsDAL.Parameter("LandID", entity.LandID),
                clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public bool Update(clsProvincieModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_Provincie,
                    clsDAL.Parameter("ProvincieID", entity.ProvincieID),
                    clsDAL.Parameter("Provincie", entity.Provincie),
                    clsDAL.Parameter("LandID", entity.LandID),
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
