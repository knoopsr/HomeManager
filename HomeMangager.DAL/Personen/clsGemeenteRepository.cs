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
    public class clsGemeenteRepository : IGemeenteRepository
    {
        private ObservableCollection<clsGemeenteM> MijnCollectie;
        int nr = 0;
        public clsGemeenteRepository()
        {
        }
        public bool Delete(clsGemeenteM entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.D_Gemeente,
                clsDAL.Parameter("GemeenteID", entity.GemeenteID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public clsGemeenteM Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsGemeenteM> GetAll()
        {
            GenerateCollection();
            return MijnCollectie;
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Gemeente);
            MijnCollectie = new ObservableCollection<clsGemeenteM>();

            while (MijnDataReader.Read())
            {
                clsGemeenteM e = new clsGemeenteM()
                {
                    GemeenteID = (int)MijnDataReader[0],
                    Gemeente = MijnDataReader[1].ToString(),
                    PostCode = MijnDataReader[2].ToString(),
                    ProvincieID = (int)MijnDataReader[3],
                    ControlField = MijnDataReader[4]
                };
                MijnCollectie.Add(e);
            }
            MijnDataReader.Close();
        }

        public clsGemeenteM GetById(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.Where(gemeente => gemeente.GemeenteID == id).FirstOrDefault();
        }

        public clsGemeenteM GetFirst()
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault();
        }

        public bool Insert(clsGemeenteM entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_Gemeente,
                clsDAL.Parameter("Gemeente", entity.Gemeente),
                clsDAL.Parameter("PostCode", entity.PostCode),
                clsDAL.Parameter("ProvincieID", entity.ProvincieID),
                clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public bool Update(clsGemeenteM entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_Gemeente,
                    clsDAL.Parameter("GemeenteID", entity.GemeenteID),
                    clsDAL.Parameter("Gemeente", entity.Gemeente),
                    clsDAL.Parameter("PostCode", entity.PostCode),
                    clsDAL.Parameter("ProvincieID", entity.ProvincieID),
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

