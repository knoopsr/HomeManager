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
    public class clsBegunstigdenDataService : IBegunstigdenDataService
    {
        IBegunstigdenRepository Repo = new clsBegunstigdenRepository();

        public bool Delete(clsBegunstigdenModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsBegunstigdenModel Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsBegunstigdenModel> GetAll()
        {
            return Repo.GetAll();
        }

        public clsBegunstigdenModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsBegunstigdenModel GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert (clsBegunstigdenModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsBegunstigdenModel entity)
        {
            return Repo.Update(entity);
        }





    }
}
