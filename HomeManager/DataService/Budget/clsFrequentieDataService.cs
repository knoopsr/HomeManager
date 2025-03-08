using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Common;
using HomeManager.DAL;
using HomeManager.DAL.Budget;
using HomeManager.Model.Budget;
using Microsoft.Data.SqlClient;


namespace HomeManager.DataService.Budget
{
    public class clsFrequentieDataService : IFrequentieDataService
    {
        IFrequentieRepository Repo = new clsFrequentieRepository();

        public bool Delete(clsFrequentieModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsFrequentieModel Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsFrequentieModel> GetAll()
        {
            return Repo.GetAll();
        }

        public clsFrequentieModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsFrequentieModel GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert (clsFrequentieModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsFrequentieModel entity)
        {
            return Repo.Update(entity);
        }





    }
}
