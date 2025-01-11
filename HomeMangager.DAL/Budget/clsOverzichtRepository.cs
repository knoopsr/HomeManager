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
    public class clsOverzichtRepository  /* : IOverzichtRepository */
    {
        //private ObservableCollection<clsTransactieModel> MijnCollectie;

        //public clsTransactieRepository()
        //{

        //}

        //public bool Delete(clsTransactieModel entity)
        //{
        //    (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.D_BudgetTransaction,
        //        clsDAL.Parameter("BudgetTransactionID", entity.BudgetTransactionID),
        //        clsDAL.Parameter("ControlField", entity.ControlField),
        //        clsDAL.Parameter("@ReturnValue", 0));
        //    if (OK)
        //    {
        //        entity.ErrorBoodschap = Boodschap;
        //    }
        //    return OK;
        //}

        //public clsTransactieModel Find()
        //{
        //    throw new NotImplementedException();
        //}

        //public ObservableCollection<clsTransactieModel> GetAll()
        //{
        //    GenerateCollection();
        //    return MijnCollectie;
        //}

        //private void GenerateCollection()
        //{
        //    SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_BudgetTransaction);
        //    MijnCollectie = new ObservableCollection<clsTransactieModel>();

        //    while (MijnDataReader.Read())
        //    {
        //        clsTransactieModel m = new clsTransactieModel()
        //        {
        //            BudgetTransactionID = (int)MijnDataReader["BudgetTransactionID"],
        //            IsUitgaven = (bool)MijnDataReader["IsUitgaven"],
        //            Bedrag = MijnDataReader["Bedrag"] as decimal? ?? 0,
        //            Datum = DateOnly.FromDateTime((DateTime)MijnDataReader["Datum"]),
        //            Onderwerp = MijnDataReader["Onderwerp"].ToString(),
        //            BegunstigdeID = (int)MijnDataReader["BegunstigdenID"],
        //            Begunstigde = MijnDataReader["Begunstigde"].ToString(),
        //            BudgetCategorieID = (int)MijnDataReader["BudgetCategorieID"],
        //            BudgetCategorie = MijnDataReader["BudgetCategorie"].ToString(), 
        //            Bijlage = MijnDataReader["Bijlage"] != DBNull.Value ? (byte[])MijnDataReader["Bijlage"] : null,
        //            ControlField = MijnDataReader["ControlField"]

        //        };
        //        MijnCollectie.Add(m);
        //    }
        //    MijnDataReader.Close();
        //}

        //public clsTransactieModel GetById(int id)
        //{
        //    if (MijnCollectie == null)
        //    {
        //        GenerateCollection();
        //    }
        //    return MijnCollectie.FirstOrDefault(); //nog te bepalen hoe ID te noemen

        //}

        //public clsTransactieModel GetFirst()
        //{
        //    if(MijnCollectie == null)
        //    {
        //        GenerateCollection();
        //    }
        //    return MijnCollectie.FirstOrDefault();
        //}

        //public bool Insert(clsTransactieModel entity)
        //{
        //    (DataTable DT, bool OK, string Boodschap) =
        //        clsDAL.ExecuteDataTable(Properties.Resources.I_BudgetTransaction,
        //        clsDAL.Parameter("IsUitgaven", entity.IsUitgaven),
        //        clsDAL.Parameter("Bedrag", entity.Bedrag),
        //        clsDAL.Parameter("Datum", entity.Datum),
        //         clsDAL.Parameter("Onderwerp", entity.Onderwerp),
        //        clsDAL.Parameter("BegunstigdenID", entity.BegunstigdeID),
        //        clsDAL.Parameter("BudgetCategorieID", entity.BudgetCategorieID),
        //        clsDAL.Parameter("Bijlage",entity.Bijlage != null ? (object)entity.Bijlage : DBNull.Value, SqlDbType.VarBinary),
        //        clsDAL.Parameter("@ReturnValue", 0)) ;

        //    if (!OK)
        //    {
        //        entity.ErrorBoodschap = Boodschap;  
        //    }
        //    return OK;
        //}

        //public bool Update(clsTransactieModel entity)
        //{
        //    (DataTable DT, bool OK, string Boodschap) =
        //        clsDAL.ExecuteDataTable(Properties.Resources.U_BudgetTransaction,
        //        clsDAL.Parameter("BudgetTransactionID", entity.BudgetTransactionID),
        //        clsDAL.Parameter("IsUitgaven", entity.IsUitgaven),
        //        clsDAL.Parameter("Bedrag", entity.Bedrag),
        //        clsDAL.Parameter("Datum", entity.Datum),
        //        clsDAL.Parameter("Onderwerp", entity.Onderwerp),
        //        clsDAL.Parameter("BegunstigdenID", entity.BegunstigdeID),
        //        clsDAL.Parameter("BudgetCategorieID", entity.BudgetCategorieID),
        //        clsDAL.Parameter("Bijlage", entity.Bijlage != null ? (object)entity.Bijlage : DBNull.Value, SqlDbType.VarBinary),
        //        clsDAL.Parameter("ControlField", entity.ControlField),
        //        clsDAL.Parameter("@ReturnValue", 0));

        //    if (!OK)
        //    {
        //        entity.ErrorBoodschap = Boodschap;
        //    }
        //    return OK;
        //}

    }
}
