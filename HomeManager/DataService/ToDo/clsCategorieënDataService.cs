using HomeManager.DAL.ToDo.Categorieën;
using HomeManager.Model.Todo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.ToDo
{
    class clsCategorieënDataService : ICategorieënDataService
    {
        ICategorieënRepository Repo = new clsCategorieënRepository();

        public bool Delete(clsCategorieënM entity) => Repo.Delete(entity);
        public clsCategorieënM Find() => Repo.Find();
        public ObservableCollection<clsCategorieënM> GetAll() => Repo.GetAll();
        public clsCategorieënM GetById(int id) => Repo.GetById(id);
        public clsCategorieënM GetFirst() => Repo.GetFirst();
        public bool Insert(clsCategorieënM entity) => Repo.Insert(entity);
        public bool Update(clsCategorieënM entity) => Repo.Update(entity);
    }
}
