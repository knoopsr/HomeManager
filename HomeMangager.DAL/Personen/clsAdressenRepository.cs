﻿using HomeManager.Model.Personen;
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
    public class clsAdressenRepository : IAdressenRepository
    {
        private ObservableCollection<clsAdressenModel> MijnCollectie;
        int nr = 0;
        public clsAdressenRepository()
        {
        }
        public bool Delete(clsAdressenModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_Adressen,
                clsDAL.Parameter("AdresID", entity.AdresID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public clsAdressenModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsAdressenModel> GetAll()
        {
            GenerateCollection();
            return MijnCollectie;
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Adressen);
            MijnCollectie = new ObservableCollection<clsAdressenModel>();

            while (MijnDataReader.Read())
            {
                clsAdressenModel e = new clsAdressenModel()
                {
                    Straat = MijnDataReader[0].ToString(),
                    Nummer = MijnDataReader[1].ToString(),
                    GemeenteID = (int)MijnDataReader[2],
                    FunctieID = (int)MijnDataReader[3],
                    ControlField = MijnDataReader[4]
                };
                MijnCollectie.Add(e);
            }
            MijnDataReader.Close();
        }

        public clsAdressenModel GetById(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.Where(adres => adres.AdresID == id).FirstOrDefault();
        }

        public clsAdressenModel GetFirst()
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault();
        }

        public bool Insert(clsAdressenModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_Adressen,
                clsDAL.Parameter("Straat", entity.Straat),
                clsDAL.Parameter("Nummer", entity.Nummer),
                clsDAL.Parameter("FunctieID", entity.FunctieID),
                clsDAL.Parameter("Gemeente", entity.GemeenteID),
                clsDAL.Parameter("FunctieID", entity.FunctieID),
                clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public bool Update(clsAdressenModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_Adressen,
                    clsDAL.Parameter("AdresID", entity.AdresID),
                    clsDAL.Parameter("PersoonID", entity.PersoonID),
                    clsDAL.Parameter("GemeenteID", entity.GemeenteID),
                    clsDAL.Parameter("FunctieID", entity.FunctieID),
                    clsDAL.Parameter("Straat", entity.Straat),
                    clsDAL.Parameter("Nummer", entity.Nummer),
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
