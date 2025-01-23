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
    public class clsCategorieRepository : ICategorieRepository
    {
        private ObservableCollection<clsCategorieModel> MijnCollectie;

        public clsCategorieRepository()
        {

        }

        public bool Delete(clsCategorieModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.D_BudgetCategorie,
                clsDAL.Parameter("BudgetCategorieID", entity.BudgetCategorieID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@ReturnValue", 0));
            if (OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public clsCategorieModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsCategorieModel> GetAll()
        {
            GenerateCollection();
            return MijnCollectie;
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_BudgetCategorie);
            MijnCollectie = new ObservableCollection<clsCategorieModel>();

            while (MijnDataReader.Read())
            {
                clsCategorieModel m = new clsCategorieModel()
                {
                    BudgetCategorieID = (int)MijnDataReader["BudgetCategorieID"],
                    BudgetCategorie = MijnDataReader["BudgetCategorie"].ToString(),
                    ControlField = MijnDataReader["ControlField"]

                };
                MijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }

        public clsCategorieModel GetById(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault(); //nog te bepalen hoe ID te noemen

        }

        public clsCategorieModel GetFirst()
        {
            if(MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault();
        }

        public bool Insert(clsCategorieModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_BudgetCategorie,
                clsDAL.Parameter("BudgetCategorie", entity.BudgetCategorie),
                clsDAL.Parameter("@ReturnValue", 0));

            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;  
            }
            return OK;
        }

        public bool Update(clsCategorieModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_BudgetCategorie,
                clsDAL.Parameter("BudgetCategorieID", entity.BudgetCategorieID),
                clsDAL.Parameter("BudgetCategorie", entity.BudgetCategorie),
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
