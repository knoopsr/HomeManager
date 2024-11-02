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
    public class clsCategorieDataService : ICategorieDataService
    {
        ICategorieRepository Repo = new clsCategorieRepository();

        public bool Delete(clsCategorieModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsCategorieModel Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsCategorieModel> GetAll()
        {
            return Repo.GetAll();
        }

        public clsCategorieModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsCategorieModel GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert (clsCategorieModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsCategorieModel entity)
        {
            return Repo.Update(entity);
        }





    }
}
