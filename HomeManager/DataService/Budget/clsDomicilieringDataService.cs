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
    public class clsDomicilieringDataService : IDomicilieringDataService
    {
        IDomicilieringRepository Repo = new clsDomicilieringRepository();

        public bool Delete(clsDomicilieringModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsDomicilieringModel Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsDomicilieringModel> GetAll()
        {
            return Repo.GetAll();
        }

        public clsDomicilieringModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsDomicilieringModel GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert (clsDomicilieringModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsDomicilieringModel entity)
        {
            return Repo.Update(entity);
        }





    }
}
