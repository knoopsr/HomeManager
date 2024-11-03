using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Model.Personen;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace HomeManager.DAL.Personen
{
    public class clsEmailTypeRepository : IEmailTypeRepository
    {
        private ObservableCollection<clsEmailTypeM> MijnCollectie;
        int nr = 0;
        public clsEmailTypeRepository()
        { }
        public bool Delete(clsEmailTypeM entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.D_EmailType,
                clsDAL.Parameter("EmailTypeID", entity.EmailTypeID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public clsEmailTypeM Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsEmailTypeM> GetAll()
        {
            GenerateCollection();
            return MijnCollectie;
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_EmailType);
            MijnCollectie = new ObservableCollection<clsEmailTypeM>();

            while (MijnDataReader.Read())
            {
                clsEmailTypeM m = new clsEmailTypeM()
                {
                    EmailTypeID = (int)MijnDataReader["EmailTypeID"],
                    EmailType = MijnDataReader["EmailType"].ToString(),
                    Omschrijving = MijnDataReader["Omschrijving"].ToString(),
                    ControlField = MijnDataReader["ControlField"]
                };
                MijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }

        public clsEmailTypeM GetById(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.Where(emailtype => emailtype.EmailTypeID == id).FirstOrDefault();
        }

        public clsEmailTypeM GetFirst()
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault();
        }

        public bool Insert(clsEmailTypeM entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_EmailType,
                clsDAL.Parameter("EmailType", entity.EmailType),
                clsDAL.Parameter("Omschrijving", entity.Omschrijving),
                clsDAL.Parameter("@ReturnValue", 0)
                );
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public bool Update(clsEmailTypeM entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_EmailType,
                clsDAL.Parameter("EmailTypeID", entity.EmailTypeID),
                clsDAL.Parameter("EmailType", entity.EmailType),
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
