using HomeManager.Common;
using HomeManager.DAL.Security;
using HomeManager.DAL.Todo.Todo;
using HomeManager.Model.Todo;
using HomeManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.ToDo
{
    internal class clsTodoDataService : ITodoDataService
    {
        ITodoRepository Repo = new clsTodoRepository();

        public bool Delete(clsTodoM entity)
        {
            throw new NotImplementedException();
        }

        public clsTodoM Find()
        {
            throw new NotImplementedException();
        }

        //public ObservableCollection<clsCollectiesM> GetAll() => Repo.GetAll();

        public clsTodoM GetById(int id)
        {
            throw new NotImplementedException();
        }

        public clsTodoM GetFirst()
        {
            throw new NotImplementedException();
        }

        public bool Insert(clsTodoM entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(clsTodoM entity)
        {
            throw new NotImplementedException();
        }

        ObservableCollection<clsTodoM> IDataService<clsTodoM>.GetAll() => Repo.GetAll();

    }
}
