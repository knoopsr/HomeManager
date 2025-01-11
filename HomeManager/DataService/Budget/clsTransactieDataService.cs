using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    public class clsTransactieDataService : ITransactieDataService
    {
        ITransactieRepository Repo = new clsTransactieRepository();

        public bool Delete(clsTransactieModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsTransactieModel Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsTransactieModel> GetAll()
        {
            return Repo.GetAll();
        }

        public clsTransactieModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsTransactieModel GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert (clsTransactieModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsTransactieModel entity)
        {
            return Repo.Update(entity);
        }





    }
}
