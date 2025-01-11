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
    public class clsBijlageDataService : IBijlageDataService
    {
        IBijlageRepository Repo = new clsBijlageRepository();

        public bool Delete(clsBijlageModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsBijlageModel Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsBijlageModel> GetAll()
        {
            return Repo.GetAll();
        }

        public clsBijlageModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsBijlageModel GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert (clsBijlageModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsBijlageModel entity)
        {
            return Repo.Update(entity);
        }





    }
}
