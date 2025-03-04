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
    public class clsBegunstigdenRepository : IBegunstigdenRepository
    {
        private ObservableCollection<clsBegunstigdenModel> MijnCollectie;

        public clsBegunstigdenRepository()
        {

        }

        public bool Delete(clsBegunstigdenModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.D_Begunstigden,
                clsDAL.Parameter("BegunstigdeID", entity.BegunstigdeID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@ReturnValue", 0));
            if (OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public clsBegunstigdenModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsBegunstigdenModel> GetAll()
        {
            GenerateCollection();
            return MijnCollectie;
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Begunstigden);
            MijnCollectie = new ObservableCollection<clsBegunstigdenModel>();

            while (MijnDataReader.Read())
            {
                clsBegunstigdenModel m = new clsBegunstigdenModel()
                {
                    BegunstigdeID = (int)MijnDataReader["BegunstigdeID"],
                    Begunstigde = MijnDataReader["Begunstigde"].ToString(),
                    Opmerking = MijnDataReader["Opmerking"].ToString(),
                    ControlField = MijnDataReader["ControlField"]

                };
                MijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }

        public clsBegunstigdenModel GetById(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault(); //nog te bepalen hoe ID te noemen

        }

        public clsBegunstigdenModel GetFirst()
        {
            if(MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault();
        }

        public bool Insert(clsBegunstigdenModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_Begunstigden,
                clsDAL.Parameter("Begunstigde", entity.Begunstigde),
                clsDAL.Parameter("Opmerking", entity.Opmerking),
                clsDAL.Parameter("@ReturnValue", 0));

            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;  
            }
            return OK;
        }

        public bool Update(clsBegunstigdenModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_Begunstigden,
                clsDAL.Parameter("BegunstigdeID", entity.BegunstigdeID),
                clsDAL.Parameter("Begunstigde", entity.Begunstigde),
                clsDAL.Parameter("Opmerking", entity.Opmerking),
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
