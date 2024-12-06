using HomeManager.Common;
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
    public class clsFunctiesRepository : IFunctiesRepository
    {
        private ObservableCollection<clsFunctiesModel> MijnCollectie;
        int nr = 0;
        public clsFunctiesRepository()
        { }
        public bool Delete(clsFunctiesModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.D_Functies,
                clsDAL.Parameter("FunctieID", entity.FunctieID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public clsFunctiesModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsFunctiesModel> GetAll()
        {
            GenerateCollection();
            return MijnCollectie;
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Functies);
            MijnCollectie = new ObservableCollection<clsFunctiesModel>();

            while (MijnDataReader.Read())
            {
                clsFunctiesModel m = new clsFunctiesModel()
                {
                    FunctieID = (int)MijnDataReader["FunctieID"],
                    Functie = MijnDataReader["Functie"].ToString(),
                    Omschrijving = MijnDataReader["Omschrijving"].ToString(),
                    ControlField = MijnDataReader["ControlField"]
                };
                MijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }

        public clsFunctiesModel GetById(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.Where(functie => functie.FunctieID == id).FirstOrDefault();
        }

        public clsFunctiesModel GetFirst()
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault();
        }

        public bool Insert(clsFunctiesModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_Functies,
                clsDAL.Parameter("Functie", entity.Functie),
                clsDAL.Parameter("Omschrijving", entity.Omschrijving),
                clsDAL.Parameter("@ReturnValue", 0)
                );
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public bool Update(clsFunctiesModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_Functies,
                clsDAL.Parameter("FunctieID", entity.FunctieID),
                clsDAL.Parameter("Functie", entity.Functie),
                clsDAL.Parameter("Omschrijving", entity.Omschrijving),
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
