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
    public class clsBijlageRepository : IBijlageRepository
    {
        private ObservableCollection<clsBijlageModel> MijnCollectie;

        public clsBijlageRepository()
        {

        }

        public bool Delete(clsBijlageModel entity)
        {
            //Het deleten van de bijlages gebeurd door de FK cascade in SQL = NO D_BudgetBijlage
            throw new NotImplementedException();

        }

        public clsBijlageModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsBijlageModel> GetAll()
        {
            GenerateCollection();
            return MijnCollectie;
        }

        public ObservableCollection<clsBijlageModel> GetAll(int BudgetTransactionID)
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_BudgetBijlageByBudgetTransactionID, 
                clsDAL.Parameter("BudgetTransactionID", BudgetTransactionID));
            MijnCollectie = new ObservableCollection<clsBijlageModel>();
            while (MijnDataReader.Read())
            {
                clsBijlageModel m = new clsBijlageModel()
                {
                    BudgetBijlageID = (int)MijnDataReader["BudgetBijlageID"],
                    BudgetTransactionID = (int)MijnDataReader["BudgetTransactionID"],
                    BijlageNaam = MijnDataReader["BijlageNaam"].ToString(),
                    Bijlage = MijnDataReader["Bijlage"] != DBNull.Value ? (byte[])MijnDataReader["Bijlage"] : null,
                };
                MijnCollectie.Add(m);
            }
            MijnDataReader.Close();
            return MijnCollectie;
        }


        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_BudgetBijlage);
            MijnCollectie = new ObservableCollection<clsBijlageModel>();

            while (MijnDataReader.Read())
            {
                clsBijlageModel m = new clsBijlageModel()
                {
                    BudgetBijlageID = (int)MijnDataReader["BudgetBijlageID"],
                    BudgetTransactionID = (int)MijnDataReader["BudgetTransactionID"],
                    BijlageNaam = MijnDataReader["BijlageNaam"].ToString(),
                    Bijlage = MijnDataReader["Bijlage"] != DBNull.Value ? (byte[])MijnDataReader["Bijlage"] : null,

                };
                MijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }

        public clsBijlageModel GetById(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault(); 

        }

        public clsBijlageModel GetFirst()
        {
            if(MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault();
        }

        

        public bool Insert(clsBijlageModel entity)
        {


            (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.I_BudgetBijlage,
                clsDAL.Parameter("BudgetTransactionID", entity.BudgetTransactionID),
                clsDAL.Parameter("BijlageNaam", entity.BijlageNaam),
                clsDAL.Parameter("Bijlage", entity.Bijlage),
                clsDAL.Parameter("@ReturnValue", 0));

            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public bool Update(clsBijlageModel entity)
        {


            (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.U_BudgetBijlage,
                clsDAL.Parameter("BudgetTransactionID", entity.BudgetTransactionID),
                clsDAL.Parameter("BijlageNaam", entity.BijlageNaam),
                clsDAL.Parameter("Bijlage", entity.Bijlage),
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
