using HomeManager.Model.Security;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Security
{
    public class clsRechtenRepository : IRechtenRepository
    {
        public clsRechtenRepository() { }
        private ObservableCollection<clsRechtenModel> _mijnCollectie;
        int nr = 0;

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Rechten);
            _mijnCollectie = new ObservableCollection<clsRechtenModel>();

            while (MijnDataReader.Read())
            {
                clsRechtenModel m = new clsRechtenModel()
                {
                    RechtenID = (int)MijnDataReader["RechtenID"],
                    RechtenName = (string)MijnDataReader["RechtenName"],
                    RechtenCode = (int)MijnDataReader["RechtenCode"],
                    RechtenCatogorieID = (int)MijnDataReader["RechtenCatogorieID"]
                };

                _mijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }




        public bool Delete(clsRechtenModel entity)
        {
            throw new NotImplementedException();
        }

        public clsRechtenModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsRechtenModel> GetAll()
        {
            GenerateCollection();
            return _mijnCollectie;
        }

        public clsRechtenModel GetById(int id)
        {
            return _mijnCollectie.Where(x => x.RechtenID == id).FirstOrDefault();
        }


        public clsRechtenModel GetByCatogorieID(int id)
        {
            return _mijnCollectie.Where(x => x.RechtenCatogorieID == id).FirstOrDefault();
        }



        public clsRechtenModel GetFirst()
        {
            throw new NotImplementedException();
        }

        public bool Insert(clsRechtenModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(clsRechtenModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
