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
    public class clsRechtenCatogorieRepository : IRechtenCatogorieRepository
    {
        public clsRechtenCatogorieRepository() { }
        private ObservableCollection<clsRechtenCatogorieModel> _mijnCollectie;
        int nr = 0;




        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_RechtenCatogorie);
            _mijnCollectie = new ObservableCollection<clsRechtenCatogorieModel>();

            while (MijnDataReader.Read())
            {



                clsRechtenCatogorieModel m = new clsRechtenCatogorieModel()
                {
                    RechtenCatogorieID = (int)MijnDataReader["RechtenCatogorieID"],
                    CatogorieNaam = (string)MijnDataReader["CatogorieNaam"],
                    Rechten = new ObservableCollection<clsRechtenModel>()
                    {

                    }
                };

                _mijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }

        public bool Delete(clsRechtenCatogorieModel entity)
        {
            throw new NotImplementedException();
        }

        public clsRechtenCatogorieModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsRechtenCatogorieModel> GetAll()
        {
            GenerateCollection();
            return _mijnCollectie;
        }

        public clsRechtenCatogorieModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public clsRechtenCatogorieModel GetFirst()
        {
            throw new NotImplementedException();
        }

        public bool Insert(clsRechtenCatogorieModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(clsRechtenCatogorieModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
