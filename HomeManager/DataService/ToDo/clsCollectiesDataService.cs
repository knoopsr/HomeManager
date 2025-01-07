using HomeManager.DAL.ToDo.Collecties;
using HomeManager.Model.Todo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.ToDo
{
    internal class clsCollectiesDataService : ICollectiesDataService
    {
        ICollectiesRepository Repo = new clsCollectiesRepository();

        public bool Delete(clsCollectiesM entity) => Repo.Delete(entity);
        public clsCollectiesM Find() => Repo.Find();
        public ObservableCollection<clsCollectiesM> GetAll() => Repo.GetAll();
        public clsCollectiesM GetById(int id) => Repo.GetById(id);
        public clsCollectiesM GetFirst() => Repo.GetFirst();
        public bool Insert(clsCollectiesM entity) => Repo.Insert(entity);
        public bool Update(clsCollectiesM entity) => Repo.Update(entity);
    }
}
