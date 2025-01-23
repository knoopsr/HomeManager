using HomeManager.DAL.Personen;
using HomeManager.Model.Personen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.Personen
{
    public class clsEmailAdressenDataService : IEmailAdressenDataService
    {
        IEmailAdressenRepository Repo = new clsEmailAdressenRepository();
        public bool Delete(clsEmailAdressenModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsEmailAdressenModel Find()
        {
           return Repo.Find();
        }

        public ObservableCollection<clsEmailAdressenModel> GetAll()
        {
            return Repo.GetAll();
        }

        public ObservableCollection<clsEmailAdressenModel> GetAllbyRollName(string rolName)
        {
            return Repo.GetAllbyRollName(rolName);
        }

        public clsEmailAdressenModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public ObservableCollection<clsEmailAdressenModel> GetByPersoonID(int id)
        {
            return Repo.GetByPersoonID(id);
        }

        public clsEmailAdressenModel GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsEmailAdressenModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsEmailAdressenModel entity)
        {
            return Repo.Update(entity);
        }
    }
}
