using HomeManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Common;
using HomeManager.Model.Budget;
using Microsoft.Data.SqlClient;

namespace HomeManager.DAL.Budget
{
    public class clsFrequentieRepository : IFrequentieRepository
    {
        private ObservableCollection<clsFrequentieModel> MijnCollectie;

        public clsFrequentieRepository()
        {

        }

        public bool Delete(clsFrequentieModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.D_Frequentie,
                clsDAL.Parameter("FrequentieID", entity.FrequentieID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@ReturnValue", 0));
            if (OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public clsFrequentieModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsFrequentieModel> GetAll()
        {
            GenerateCollection();
            return MijnCollectie;
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Frequentie);
            MijnCollectie = new ObservableCollection<clsFrequentieModel>();

            while (MijnDataReader.Read())
            {
                clsFrequentieModel m = new clsFrequentieModel()
                {
                    FrequentieID = (int)MijnDataReader["FrequentieID"],
                    Frequentie = MijnDataReader["Frequentie"].ToString(),
                    AantalDagen = (int)MijnDataReader["AantalDagen"],
                    ControlField = MijnDataReader["ControlField"]

                };
                MijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }

        public clsFrequentieModel GetById(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault(); //nog te bepalen hoe ID te noemen

        }

        public clsFrequentieModel GetFirst()
        {
            if(MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault();
        }

        public bool Insert(clsFrequentieModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_Frequentie,
                clsDAL.Parameter("Frequentie", entity.Frequentie),
                clsDAL.Parameter("AantalDagen", entity.AantalDagen),
                clsDAL.Parameter("@ReturnValue", 0));

            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;  
            }
            return OK;
        }

        public bool Update(clsFrequentieModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_Frequentie,
                clsDAL.Parameter("FrequentieID", entity.FrequentieID),
                clsDAL.Parameter("Frequentie", entity.Frequentie),
                clsDAL.Parameter("AantalDagen", entity.AantalDagen),
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
