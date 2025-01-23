using HomeManager.DAL.Agenda;
using HomeManager.Model.Agenda;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Agenda.DataService
{
    public class clsAgendaCategoryDataService : IAgendaCategoryDataService
    {
        IAgendaCategoryRepository _repo = new clsAgendaCategoryRepository();
        public bool Delete(clsAgendaCategoryModel entity)
        {
            return _repo.Delete(entity);
        }

        public clsAgendaCategoryModel Find()
        {
            return _repo.Find();
        }

        public ObservableCollection<clsAgendaCategoryModel> GetAll()
        {
            return _repo.GetAll();
        }

        public clsAgendaCategoryModel GetById(int id)
        {
            return _repo.GetById(id);
        }

        public clsAgendaCategoryModel GetFirst()
        {
            return _repo.GetFirst();
        }

        public bool Insert(clsAgendaCategoryModel entity)
        {
            return _repo.Insert(entity);
        }

        public bool Update(clsAgendaCategoryModel entity)
        {
            return _repo.Update(entity);
        }
    }
}
