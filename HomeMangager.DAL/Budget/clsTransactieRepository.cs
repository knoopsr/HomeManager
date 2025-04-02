using HomeManager.Model.Budget;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace HomeManager.DAL.Budget
{
    public class clsTransactieRepository : ITransactieRepository
    {
        private ObservableCollection<clsTransactieModel> MijnCollectie;
        
        private IEnumerable<clsBijlageModel> bijlagen;

        public clsTransactieRepository()
        {

        }

        public bool Delete(clsTransactieModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.D_BudgetTransaction,
                clsDAL.Parameter("BudgetTransactionID", entity.BudgetTransactionID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@ReturnValue", 0));
            if (OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public clsTransactieModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsTransactieModel> GetAll()
        {
            GenerateCollection();
            return MijnCollectie;
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_BudgetTransaction);
            MijnCollectie = new ObservableCollection<clsTransactieModel>();

            while (MijnDataReader.Read())
            {
                clsTransactieModel m = new clsTransactieModel()
                {
                    BudgetTransactionID = (int)MijnDataReader["BudgetTransactionID"],
                    IsUitgaven = (bool)MijnDataReader["IsUitgaven"],
                    Bedrag = MijnDataReader["Bedrag"] as decimal? ?? 0,
                    Datum = DateOnly.FromDateTime((DateTime)MijnDataReader["Datum"]),
                    Onderwerp = MijnDataReader["Onderwerp"].ToString(),
                    BegunstigdeID = (int)MijnDataReader["BegunstigdenID"],
                    Begunstigde = MijnDataReader["Begunstigde"].ToString(),
                    BudgetCategorieID = (int)MijnDataReader["BudgetCategorieID"],
                    BudgetCategorie = MijnDataReader["BudgetCategorie"].ToString(),
                    ControlField = MijnDataReader["ControlField"]

                };
                MijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }

        

        public clsTransactieModel GetById(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault();

        }

        public clsTransactieModel GetFirst()
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault();
        }


        public clsTransactieModel Insert2(clsTransactieModel entity)
        {


            (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.I_BudgetTransaction,
                clsDAL.Parameter("IsUitgaven", entity.IsUitgaven),
                clsDAL.Parameter("Bedrag", entity.Bedrag),
                clsDAL.Parameter("Datum", entity.Datum),
                clsDAL.Parameter("Onderwerp", entity.Onderwerp),
                clsDAL.Parameter("BegunstigdenID", entity.BegunstigdeID),
                clsDAL.Parameter("BudgetCategorieID", entity.BudgetCategorieID),
                clsDAL.Parameter("@NewBudgetTransactionID", 0),
                clsDAL.Parameter("@ReturnValue", 0));

            clsTransactieModel test = new clsTransactieModel();


            foreach (DataRow dr in DT.Rows)
            {
                test.BudgetTransactionID = (int)dr["BudgetTransactionID"];
                test.ControlField = dr["ControlField"];

            }


            if (!OK)
            {
                test.ErrorBoodschap = Boodschap;
            }


            return test;
        }



        public bool Insert(clsTransactieModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(clsTransactieModel entity)
        {

            (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.U_BudgetTransaction,
                clsDAL.Parameter("BudgetTransactionID", entity.BudgetTransactionID),
                clsDAL.Parameter("IsUitgaven", entity.IsUitgaven),
                clsDAL.Parameter("Bedrag", entity.Bedrag),
                clsDAL.Parameter("Datum", entity.Datum),
                clsDAL.Parameter("Onderwerp", entity.Onderwerp),
                clsDAL.Parameter("BegunstigdenID", entity.BegunstigdeID),
                clsDAL.Parameter("BudgetCategorieID", entity.BudgetCategorieID),    
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
