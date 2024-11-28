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
    public class clsPersoonRepository : IPersoonRepository
    {
        private ObservableCollection<clsPersoonM> MijnCollectie;
        int nr = 0;
        public clsPersoonRepository()
        {
        }

        public bool Delete(clsPersoonM entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.D_Persoon,
                clsDAL.Parameter("PersoonID", entity.PersoonID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public ObservableCollection<clsPersoonM> GetAll()
        {
            GenerateCollection();
            return MijnCollectie;
        }

        public clsPersoonM GetById(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.Where(persoon => persoon.PersoonID == id).FirstOrDefault();
        }

        public clsPersoonM GetFirst()
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault();
        }

        public bool Insert(clsPersoonM entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_Persoon,
                clsDAL.Parameter("Naam", entity.Naam),
                clsDAL.Parameter("Voornaam", entity.Voornaam),
             clsDAL.Parameter("Foto", entity.Foto != null ? (object)entity.Foto : DBNull.Value, SqlDbType.VarBinary),
                clsDAL.Parameter("Geboortedatum", entity.Geboortedatum),
                clsDAL.Parameter("IsApplicationUser", entity.IsApplicationUser),
                clsDAL.Parameter("@ReturnValue", 0)
                );
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public bool Update(clsPersoonM entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_Persoon,
                clsDAL.Parameter("PersoonID", entity.PersoonID),
                clsDAL.Parameter("Naam", entity.Naam),
                clsDAL.Parameter("Voornaam", entity.Voornaam),
                   clsDAL.Parameter("Foto", entity.Foto != null ? (object)entity.Foto : DBNull.Value, SqlDbType.VarBinary),
                clsDAL.Parameter("Geboortedatum", entity.Geboortedatum),
                clsDAL.Parameter("IsApplicationUser", entity.IsApplicationUser),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }


        public clsPersoonM Find()
        {
            throw new NotImplementedException();
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Persoon);
            MijnCollectie = new ObservableCollection<clsPersoonM>();

            while (MijnDataReader.Read())
            {
                clsPersoonM e = new clsPersoonM()
                {
                    PersoonID = (int)MijnDataReader[0],
                    Naam = MijnDataReader[1].ToString(),
                    Voornaam = MijnDataReader[2].ToString(),
                    Foto = MijnDataReader[3] != DBNull.Value ? (byte[])MijnDataReader[3] : null, // Controle op DBNull voordat je het cast
                    Geboortedatum = DateOnly.FromDateTime((DateTime)MijnDataReader[4]),
                    IsApplicationUser = (bool)MijnDataReader[5],
                    ControlField = MijnDataReader[6]
                };
                MijnCollectie.Add(e);
            }
            MijnDataReader.Close();
        }

        public ObservableCollection<clsPersoonM> GetAllApplicationUser()
        { 
            GenerateCollection();
            return new ObservableCollection<clsPersoonM>(
                MijnCollectie.Where(persoon => persoon.IsApplicationUser == true)
            );
        }
    }
}


