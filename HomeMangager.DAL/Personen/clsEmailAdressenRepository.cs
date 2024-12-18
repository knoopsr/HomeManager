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
    public class clsEmailAdressenRepository : IEmailAdressenRepository
    {
        private ObservableCollection<clsEmailAdressenModel> MijnCollectie;
        int nr = 0;

        public clsEmailAdressenRepository()
        {
        }
        public bool Delete(clsEmailAdressenModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
            clsDAL.ExecuteDataTable(Properties.Resources.D_EmailAdressen,
                clsDAL.Parameter("EmailAdresID", entity.EmailAdresID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public clsEmailAdressenModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsEmailAdressenModel> GetAll()
        {
            GenerateCollection();
            return MijnCollectie;
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_EmailAdressen);
            MijnCollectie = new ObservableCollection<clsEmailAdressenModel>();

            while (MijnDataReader.Read())
            {
                clsEmailAdressenModel m = new clsEmailAdressenModel()
                {
                    EmailAdresID = (int)MijnDataReader["EmailAdresID"],
                    Emailadres = MijnDataReader["Emailadres"].ToString(),
                    PersoonID = (int)MijnDataReader["PersoonID"],
                    EmailTypeID = (int)MijnDataReader["EmailTypeID"],
                    ControlField = MijnDataReader["ControlField"]
                };
                MijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }

        public clsEmailAdressenModel GetById(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.Where(emailadress => emailadress.EmailAdresID == id).FirstOrDefault();
        }

        public clsEmailAdressenModel GetFirst()
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault();
        }

        public bool Insert(clsEmailAdressenModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_EmailAdressen,
                    clsDAL.Parameter("Emailadres", entity.Emailadres),
                    clsDAL.Parameter("PersoonID", entity.PersoonID),
                    clsDAL.Parameter("EmailTypeID", entity.EmailTypeID),
                    clsDAL.Parameter("@ReturnValue", 0)
                );
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public bool Update(clsEmailAdressenModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_EmailAdressen,
                    clsDAL.Parameter("EmailAdresID", entity.EmailAdresID),
                    clsDAL.Parameter("Emailadres", entity.Emailadres),
                    clsDAL.Parameter("PersoonID", entity.PersoonID),
                    clsDAL.Parameter("EmailTypeID", entity.EmailTypeID),
                    clsDAL.Parameter("ControlField", entity.ControlField),
                    clsDAL.Parameter("@ReturnValue", 0)
                );
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }


        //chatgpt
        public ObservableCollection<clsEmailAdressenModel> GetEmailAdressenForPersoon(int persoonId)
        {
            ObservableCollection<clsEmailAdressenModel> emailAdressen = new ObservableCollection<clsEmailAdressenModel>();

            // Gebruik een query om de e-mailadressen voor de opgegeven persoon op te halen
            using (SqlDataReader reader = clsDAL.GetData("SELECT * FROM EmailAdressen WHERE PersoonID = @PersoonID",
                clsDAL.Parameter("@PersoonID", persoonId)))
            {
                while (reader.Read())
                {
                    clsEmailAdressenModel email = new clsEmailAdressenModel
                    {
                        EmailAdresID = (int)reader["EmailAdresID"],
                        Emailadres = reader["Emailadres"].ToString(),
                        PersoonID = (int)reader["PersoonID"],
                        EmailTypeID = (int)reader["EmailTypeID"],
                        ControlField = reader["ControlField"].ToString()
                    };
                    emailAdressen.Add(email);
                }
            }

            return emailAdressen;
        }


    }
}
