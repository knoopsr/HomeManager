using HomeManager.DAL.Todo.Details;
using HomeManager.Model.Todo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.ToDo
{
    class clsTodoDetailsDataService : ITodoDetailsDataService
    {
        ITodoDetailsRepository Repo = new clsTodoDetailsRepository();

        public bool Delete(clsTodoDetailsM entity)
        {
            return Repo.Delete(entity);
        }

        public clsTodoDetailsM Find()
        {
            return Repo.Find();
        }

        public ObservableCollection<clsTodoDetailsM> GetAll()
        {
            return Repo.GetAll();
        }

        public clsTodoDetailsM GetById(int id)
        {
            return Repo.GetById(id);
        }

        public clsTodoDetailsM GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsTodoDetailsM entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsTodoDetailsM entity)
        {
            return Repo.Update(entity);
        }
    }
}
