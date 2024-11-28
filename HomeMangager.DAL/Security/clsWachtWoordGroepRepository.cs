using HomeManager.Common;
using HomeManager.Model.Security;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Security
{
    public class clsWachtWoordGroepRepository : IWachtWoordGroepRepository
    {
        private ObservableCollection<clsWachtWoordGroepModel> _mijnCollectie;
        int nr = 0;


        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_WachtWoordenGroep);
            _mijnCollectie = new ObservableCollection<clsWachtWoordGroepModel>();

            while (MijnDataReader.Read())
            {
                clsWachtWoordGroepModel m = new clsWachtWoordGroepModel()
                {
                    WachtwoordGroepID = (int)MijnDataReader["WachtwoordGroepID"],
                    WachtwoordGroep = (string)MijnDataReader["WachtwoordGroep"],        
                    ControlField = MijnDataReader["ControlField"]
                };

                _mijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }



        public bool Delete(clsWachtWoordGroepModel entity)
        {
            (DataTable DT, bool Ok, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.D_WachtWoordenGroep,
                clsDAL.Parameter("WachtwoordGroepID", entity.WachtwoordGroepID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@Returnvalue", 0)
                );
            if (!Ok)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return Ok;
        }

        public clsWachtWoordGroepModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsWachtWoordGroepModel> GetAll()
        {
            GenerateCollection();
            return _mijnCollectie;
        }

        public clsWachtWoordGroepModel GetById(int id)
        {
            if (_mijnCollectie == null)
            {
                GenerateCollection();
            }
            return _mijnCollectie.Where(x => x.WachtwoordGroepID == id).FirstOrDefault();
        }

        public clsWachtWoordGroepModel GetFirst()
        {
            if (_mijnCollectie == null)
            {
                GenerateCollection();
            }
            return _mijnCollectie.FirstOrDefault();
        }

        public bool Insert(clsWachtWoordGroepModel entity)
        {
            (DataTable DT, bool Ok, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_WachtWoordenGroep,
                clsDAL.Parameter("WachtwoordGroep", entity.WachtwoordGroep),
                clsDAL.Parameter("@Returnvalue", 0)
                );
            if (!Ok)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return Ok;
        }

        public bool Update(clsWachtWoordGroepModel entity)
        {
            (DataTable DT, bool Ok, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_WachtWoordenGroep,
                clsDAL.Parameter("WachtwoordGroepID", entity.WachtwoordGroepID),
                clsDAL.Parameter("WachtwoordGroep", entity.WachtwoordGroep),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@Returnvalue", 0)
                );
            if (!Ok)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return Ok;
        }
    }
}
