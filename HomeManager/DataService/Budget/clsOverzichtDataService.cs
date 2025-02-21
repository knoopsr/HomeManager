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
    public class clsOverzichtDataService : IOverzichtDataService
    {
        IOverzichtRepository Repo = new clsOverzichtRepository();

        public bool Delete(clsOverzichtModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsOverzichtModel Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsOverzichtModel> GetAll()
        {
            return Repo.GetAll();
        }
        public ObservableCollection<clsOverzichtModel> GetInkomsten()
        {
            return Repo.GetInkomsten();
        }
        public ObservableCollection<clsOverzichtModel> GetUitgaven()
        {
            return Repo.GetUitgaven();
        }

        public clsOverzichtModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsOverzichtModel GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsOverzichtModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsOverzichtModel entity)
        {
            return Repo.Update(entity);
        }


    }
}
