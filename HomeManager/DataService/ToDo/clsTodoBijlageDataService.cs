using HomeManager.DAL.Todo.Bijlage;
using HomeManager.Model.Todo;
using System.Collections.ObjectModel;

namespace HomeManager.DataService.ToDo
{
    internal class clsTodoBijlageDataService : ITodoBijlageDataService
    {
        private readonly ITodoBijlageRepository _repo = new clsTodoBijlageRepository();
        public bool Delete(clsTodoBijlageM entity) => _repo.Delete(entity);
        public clsTodoBijlageM Find() => _repo.Find();
        public ObservableCollection<clsTodoBijlageM> GetAll() => _repo.GetAll();
        public clsTodoBijlageM GetById(int id) => _repo.GetById(id);
        public clsTodoBijlageM GetFirst() => _repo.GetFirst();
        public bool Insert(clsTodoBijlageM entity) => _repo.Insert(entity);
        public bool Update(clsTodoBijlageM entity) => _repo.Update(entity);
    }
}
