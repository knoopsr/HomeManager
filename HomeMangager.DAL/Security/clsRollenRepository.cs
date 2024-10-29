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
    public class clsRollenRepository : IRollenRepository
    {
        private ObservableCollection<clsRollenModel> _mijnCollectie;
        int nr = 0;


        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Rollen);
            _mijnCollectie = new ObservableCollection<clsRollenModel>();

            while (MijnDataReader.Read())
            {
                clsRollenModel m = new clsRollenModel()
                {
                    RolID = (int)MijnDataReader["RolID"],
                    RolName = (string)MijnDataReader["RolName"],
                    Rechten = (string)MijnDataReader["Rechten"],
                    ControlField = MijnDataReader["ControlField"]
                };

                _mijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }



        public bool Delete(clsRollenModel entity)
        {
            (DataTable DT, bool Ok, string Boodschap)=
                clsDAL.ExecuteDataTable(Properties.Resources.D_Rollen,
                clsDAL.Parameter("RolID", entity.RolID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@Returnvalue", 0)
                );
            if (!Ok)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return Ok;

        }

        public clsRollenModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsRollenModel> GetAll()
        {
            GenerateCollection();
            return _mijnCollectie;
        }

        public clsRollenModel GetById(int id)
        {
            if(_mijnCollectie == null)
            {
                GenerateCollection();
            }
            return _mijnCollectie.Where(x => x.RolID == id).FirstOrDefault();
        }

        public clsRollenModel GetFirst()
        {
            if (_mijnCollectie == null)
            {
                GenerateCollection();
            }
            return _mijnCollectie.FirstOrDefault();

        }

        public bool Insert(clsRollenModel entity)
        {
            (DataTable DT, bool Ok, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_Rollen,
                clsDAL.Parameter("RolName", entity.RolName),
                clsDAL.Parameter("Rechten", entity.Rechten),
                clsDAL.Parameter("@Returnvalue", 0)
                );
            if (!Ok)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return Ok;
        }

        public bool Update(clsRollenModel entity)
        {
            (DataTable DT, bool Ok, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_Rollen,
                clsDAL.Parameter("RolID", entity.RolID),
                clsDAL.Parameter("RolName", entity.RolName),
                clsDAL.Parameter("Rechten", entity.Rechten),
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
