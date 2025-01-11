using HomeManager.DAL.ToDo.Kleuren;
using HomeManager.Model.Todo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.ToDo
{
    internal class clsKleurenDataService : IKleurenDataService
    {
        IKleurenRepository Repo = new clsKleurenRepository();

        public bool Delete(clsKleurenM entity) => Repo.Delete(entity);
        public clsKleurenM Find() => Repo.Find();
        public ObservableCollection<clsKleurenM> GetAll() => Repo.GetAll();
        public clsKleurenM GetById(int id) => Repo.GetById(id);
        public clsKleurenM GetFirst() => Repo.GetFirst();
        public bool Insert(clsKleurenM entity) => Repo.Insert(entity);
        public bool Update(clsKleurenM entity) => Repo.Update(entity);
    }
}
