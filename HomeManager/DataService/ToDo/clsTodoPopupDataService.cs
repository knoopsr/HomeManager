using HomeManager.DAL.Todo.Popup;
using HomeManager.DAL.ToDo.Popup;
using HomeManager.Model.Todo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.ToDo
{
    internal class clsTodoPopupDataService : ITodoPopupDataService
    {
        ITodoPopupRepository Repo = new clsTodoPopupRepository();

        public bool Delete(clsTodoPopupM entity) => Repo.Delete(entity);
        public clsTodoPopupM Find() => Repo.Find();
        public ObservableCollection<clsTodoPopupM> GetAll() => Repo.GetAll();
        public clsTodoPopupM GetById(int id) => Repo.GetById(id);
        public clsTodoPopupM GetFirst() => Repo.GetFirst();
        public bool Insert(clsTodoPopupM entity) => Repo.Insert(entity);
        public bool Update(clsTodoPopupM entity) => Repo.Update(entity);

    }
}
