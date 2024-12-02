using HomeManager.DAL.Security;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.Security
{
    public class clsRechtenCatogorieDataService : IRechtenCatogorieDataService
    {
        IRechtenCatogorieRepository _repo = new clsRechtenCatogorieRepository();

        public bool Delete(clsRechtenCatogorieModel entity)
        {
            return _repo.Delete(entity);
        }

        public clsRechtenCatogorieModel Find()
        {
            return _repo.Find();
        }

        public ObservableCollection<clsRechtenCatogorieModel> GetAll()
        {
            return _repo.GetAll();
        }

        public clsRechtenCatogorieModel GetById(int id)
        {
            return _repo.GetById(id);
        }

        public clsRechtenCatogorieModel GetFirst()
        {
            return _repo.GetFirst();
        }

        public bool Insert(clsRechtenCatogorieModel entity)
        {
            return _repo.Insert(entity);
        }

        public bool Update(clsRechtenCatogorieModel entity)
        {
            return _repo.Update(entity);
        }
    }
}
