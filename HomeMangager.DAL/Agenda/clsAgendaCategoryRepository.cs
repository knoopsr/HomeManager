using HomeManager.Model.Agenda;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Agenda
{
    public class clsAgendaCategoryRepository : IAgendaCategoryRepository
    {
        private ObservableCollection<clsAgendaCategoryModel> _mijnCollectie;
        int nr = 0;

        public clsAgendaCategoryRepository()
        {
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_AgendaCategory);
            _mijnCollectie = new ObservableCollection<clsAgendaCategoryModel>();

            while (MijnDataReader.Read())
            {
                clsAgendaCategoryModel m = new clsAgendaCategoryModel()
                {
                    CategoryID = (int)MijnDataReader["CategoryID"],
                    CategoryName = (string)MijnDataReader["CategoryName"],
                    CategoryDescription = (string)MijnDataReader["CategoryDescription"],
                    BackgroundColor = (string)MijnDataReader["BackgroundColor"],
                    BorderColor = (string)MijnDataReader["BorderColor"],
                    ControlField = MijnDataReader["ControlField"]
                };



                _mijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }
        public bool Delete(clsAgendaCategoryModel entity)
        {
            throw new NotImplementedException();
        }

        public clsAgendaCategoryModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsAgendaCategoryModel> GetAll()
        {
            GenerateCollection();
            return _mijnCollectie;
        }

        public clsAgendaCategoryModel GetById(int id)
        {
            if (_mijnCollectie == null)
            {
                GenerateCollection();
            }
            return _mijnCollectie.Where(x => x.CategoryID == id).FirstOrDefault();
        }

        public clsAgendaCategoryModel GetFirst()
        {
            if (_mijnCollectie == null)
            {
                GenerateCollection();
            }
            return _mijnCollectie.FirstOrDefault();
        }

        public bool Insert(clsAgendaCategoryModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(clsAgendaCategoryModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
