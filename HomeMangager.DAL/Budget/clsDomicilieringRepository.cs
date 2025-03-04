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
using System.Data.SqlTypes;

namespace HomeManager.DAL.Budget
{
    public class clsDomicilieringRepository : IDomicilieringRepository
    {
        private ObservableCollection<clsDomicilieringModel> MijnCollectie;

        public clsDomicilieringRepository()
        {

        }

        public bool Delete(clsDomicilieringModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.D_DomicilieringMetTransacties,
                clsDAL.Parameter("DomicilieringID", entity.DomicilieringID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@ReturnValue", 0));
            if (OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public clsDomicilieringModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsDomicilieringModel> GetAll()
        {
            GenerateCollection();
            return MijnCollectie;
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Domiciliering);
            MijnCollectie = new ObservableCollection<clsDomicilieringModel>();

            while (MijnDataReader.Read())
            {
                clsDomicilieringModel m = new clsDomicilieringModel()
                {
                    DomicilieringID = (int)MijnDataReader["DomicilieringID"],
                    FrequentieID = (int)MijnDataReader["FrequentieID"],
                    BegunstigdeID = (int)MijnDataReader["BegunstigdenID"],
                    Begunstigde = MijnDataReader["Begunstigde"].ToString(),
                    BudgetCategorieID = (int)MijnDataReader["BudgetCategorieID"],
                    BudgetCategorie = MijnDataReader["BudgetCategorie"].ToString(),
                    Onderwerp = MijnDataReader["Onderwerp"].ToString(),
                    IsUitgaven = (bool)MijnDataReader["IsUitgaven"],
                    Bedrag = MijnDataReader["Bedrag"] as decimal? ?? 0,
                    VanDatum = DateOnly.FromDateTime((DateTime)MijnDataReader["VanDatum"]),
                    TotDatum = DateOnly.FromDateTime((DateTime)MijnDataReader["TotDatum"]),
                    ControlField = MijnDataReader["ControlField"]

                };
                MijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }

        public clsDomicilieringModel GetById(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault(); //nog te bepalen hoe ID te noemen

        }

        public clsDomicilieringModel GetFirst()
        {
            if(MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault();
        }

        public bool Insert(clsDomicilieringModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_DomicilieringMetTransacties,
                clsDAL.Parameter("IsUitgaven", entity.IsUitgaven),
                clsDAL.Parameter("Bedrag", entity.Bedrag),
                clsDAL.Parameter("VanDatum", entity.VanDatum),
                clsDAL.Parameter("TotDatum", entity.TotDatum),
                clsDAL.Parameter("Onderwerp", entity.Onderwerp),
                clsDAL.Parameter("FrequentieID", entity.FrequentieID),
                clsDAL.Parameter("BegunstigdenID", entity.BegunstigdeID),
                clsDAL.Parameter("BudgetCategorieID", entity.BudgetCategorieID),
                clsDAL.Parameter("@ReturnValue", 0)) ;

            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;  
            }
            return OK;
        }

        public bool Update(clsDomicilieringModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_Domiciliering,
                clsDAL.Parameter("DomicilieringID", entity.DomicilieringID),
                clsDAL.Parameter("FrequentieID", entity.FrequentieID),
                clsDAL.Parameter("BegunstigdenID", entity.BegunstigdeID),
                clsDAL.Parameter("BudgetCategorieID", entity.BudgetCategorieID),
                clsDAL.Parameter("Onderwerp", entity.Onderwerp),
                clsDAL.Parameter("IsUitgaven", entity.IsUitgaven),
                clsDAL.Parameter("Bedrag", entity.Bedrag),
                clsDAL.Parameter("VanDatum", entity.VanDatum),
                clsDAL.Parameter("TotDatum", entity.TotDatum),
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
