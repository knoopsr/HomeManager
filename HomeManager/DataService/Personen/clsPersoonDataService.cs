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
    public class clsPersoonDataService : IPersoonDataService
    {
        IPersoonRepository Repo = new clsPersoonRepository();

        public bool Delete(clsPersoonModel entity)
        {
            return Repo.Delete(entity);
        }

        public clsPersoonModel Find()
        {
            return Repo.GetFirst();
        }

        public ObservableCollection<clsPersoonModel> GetAll()
        {
            return Repo.GetAll();
        }

        public ObservableCollection<clsPersoonModel> GetAllApplicationUser()
        {
            return Repo.GetAllApplicationUser();
        }

        public clsPersoonModel GetById(int id)
        {
            return Repo.GetById(id);
        }

        public ObservableCollection<clsPersoonModel> GetByPersoonID(int id)
        {
            return Repo.GetByPersoonID(id);
        }

        public clsPersoonModel GetFirst()
        {
            return Repo.GetFirst();
        }

        public bool Insert(clsPersoonModel entity)
        {
            return Repo.Insert(entity);
        }

        public bool Update(clsPersoonModel entity)
        {
            return Repo.Update(entity);
        }

        public async Task<ObservableCollection<clsPersoonModel>> GetAllAsync()
        {
            // Simuleer een lange operatie
            await Task.Delay(1000);
            return new ObservableCollection<clsPersoonModel>(/* je data hier */);
        }
    }
}
